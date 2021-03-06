﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnityRoguelike
{
    public class DecorateRoomEventArgs : EventArgs
    {
        public Rect Room { get; private set; }

        public DecorateRoomEventArgs(Rect room)
        {
            Room = room;
        }
    }

    /// <summary>
    /// Based on the work of Bob Nystrom. 
    /// http://journal.stuffwithstuff.com/2014/12/21/rooms-and-mazes/
    /// </summary>
    public class Generator
    {
        private Stage stage;
        private Rect bounds;
        private List<Rect> _rooms;
        /// For each open position in the dungeon, the index of the connected region
        /// that that position is a part of.
        private int[,] _regions;
        /// The index of the current region being carved.
        private int _currentRegion = 0;

        private readonly MersenneTwister rng;

        public int numRoomTries = 40;
        /// The inverse chance of adding a connector between two regions that have
        /// already been joined. Increasing this leads to more loosely connected
        /// dungeons.
        public int extraConnectorChance = 20;
        /// Increasing this allows rooms to be larger.
        public int roomExtraSize = 0;
        public int windingPercent = 50;
        
        public event EventHandler<DecorateRoomEventArgs> DecorateRoom;

        public Generator(int seed = 0)
        {
            rng = new MersenneTwister(seed);

            _rooms = new List<Rect>();
        }

        public void generate(Stage stage)
        {
            if (stage.width%2 == 0 || stage.height%2 == 0)
            {
                throw new ArgumentException("The stage must be odd-sized.");
            }

            bindStage(stage);

            fill(Tiles.Wall);
            _regions = new int[stage.width, stage.height];

            _addRooms();
            // Fill in all of the empty space with mazes.
            for (var y = 1; y < bounds.height; y += 2)
            {
                for (var x = 1; x < bounds.width; x += 2)
                {
                    var pos = new Vec(x, y);
                    if (GetTile(pos) != Tiles.Wall) continue;
                    _growMaze(pos);
                }
            }

            _connectRegions();
            _removeDeadEnds();

            stage.RoomRects.AddRange(_rooms);
            _rooms.ForEach(OnDecorateRoom);
        }

        private void fill(Tiles tile)
        {
            for (var y = 0; y < stage.height; y++)
            {
                for (var x = 0; x < stage.width; x++)
                {
                    SetTile(new Vec(x, y), tile);
                }
            }
        }

        private void bindStage(Stage stage)
        {
            this.stage = stage;
            bounds = stage.Bounds;
        }

        public void OnDecorateRoom(Rect room)
        {
            if (DecorateRoom != null)
            {
                DecorateRoom(this, new DecorateRoomEventArgs(room));
            }
        }

        /// Implementation of the "growing tree" algorithm from here:
        /// http://www.astrolog.org/labyrnth/algrithm.htm.
        private void _growMaze(Vec start)
        {
            var cells = new List<Vec>();
            Direction lastDir = null;

            _startRegion();
            _carve(start);

            cells.Add(start);
            while (cells.Any())
            {
                //Picking the last cell gives long straight corridors.
                //var cell = cells.Last(); 
                var cell = rng.PickOne(cells);

                // See which adjacent cells are open.
                var unmadeCells = new List<Direction>();

                foreach (var dir in Direction.cardinal)
                {
                    if (_canCarve(cell, dir)) unmadeCells.Add(dir);
                }

                if (unmadeCells.Any())
                {
                    // Based on how "windy" passages are, try to prefer carving in the
                    // same direction.
                    Direction dir;
                    if (lastDir != null && unmadeCells.Contains(lastDir) && rng.Next(100) > windingPercent)
                    {
                        dir = lastDir;
                    }
                    else
                    {
                        dir = rng.PickOne(unmadeCells);
                    }

                    _carve(cell + dir);
                    _carve(cell + dir*2);

                    cells.Add(cell + dir*2);
                    lastDir = dir;
                }
                else
                {
                    // No adjacent uncarved cells.
                    //cells.RemoveAt(cells.Count - 1);
                    cells.Remove(cell);

                    // This path has ended.
                    lastDir = null;
                }
            }
        }

        /// Places rooms ignoring the existing maze corridors.
        private void _addRooms()
        {
            for (var i = 0; i < numRoomTries; i++)
            {
                // Pick a random room size. The funny math here does two things:
                // - It makes sure rooms are odd-sized to line up with maze.
                // - It avoids creating rooms that are too rectangular: too tall and
                //   narrow or too wide and flat.
                // TODO: This isn't very flexible or tunable. Do something better here.
                var size = rng.Next(1, 3 + roomExtraSize)*2 + 1;
                var rectangularity = rng.Next(0, (1 + size)/2)*2;
                var width = size;
                var height = size;
                if (rng.Next(2) == 0)
                {
                    width += rectangularity;
                }
                else
                {
                    height += rectangularity;
                }

                var x = rng.Next((bounds.width - width)/2)*2 + 1;
                var y = rng.Next((bounds.height - height)/2)*2 + 1;

                var room = new Rect(x, y, width, height);

                var overlaps = false;
                foreach (var other in _rooms)
                {
                    if (room.DistanceTo(other) > 0)
                        continue;

                    overlaps = true;
                    break;
                }

                if (overlaps)
                    continue;

                _rooms.Add(room);

                _startRegion();
                var ttt = new Rect(x, y, width, height);

                foreach (var pos in ttt.Iterate())
                {
                    _carve(pos);
                }
            }
        }

        private void _connectRegions()
        {
            // Find all of the tiles that can connect two (or more) regions.
            var connectorRegions = new Dictionary<Vec, HashSet<int>>();

            foreach (var pos in bounds.Inflate(-1).Iterate())
            {
                // Can't already be part of a region.
                if (GetTile(pos) != Tiles.Wall) 
                    continue;

                var regions = new HashSet<int>();
                foreach (var dir in Direction.cardinal)
                {
                    var tmp = pos + dir;
                    var region = _regions[tmp.x, tmp.y];

                    if (region != 0)
                        regions.Add(region);
                }

                if (regions.Count < 2)
                    continue;

                connectorRegions[pos] = regions;
            }

            var connectors = connectorRegions.Keys.ToList();

            // Keep track of which regions have been merged. This maps an original
            // region index to the one it has been merged to.
            var merged = new Dictionary<int, int>();

            var openRegions = new HashSet<int>();
            for (var i = 1; i <= _currentRegion; i++)
            {
                merged[i] = i;
                openRegions.Add(i);
            }

            // Keep connecting regions until we're down to one.
            while (openRegions.Count > 1)
            {
                //var connector = connectors[rng.Next(connectors.Count)];
                var connector = rng.PickOne(connectors);

                // Carve the connection.
                _addJunction(connector);

                // Merge the connected regions. We'll pick one region (arbitrarily) and
                // map all of the other regions to its index.
                var regions = connectorRegions[connector].Select((region) => merged[region]).ToList();
                var dest = regions.First();
                var sources = regions.Skip(1).ToList();

                // Merge all of the affected regions. We have to look at *all* of the
                // regions because other regions may have previously been merged with
                // some of the ones we're merging now.
                for (var i = 1; i <= _currentRegion; i++)
                {
                    if (sources.Contains(merged[i]))
                    {
                        merged[i] = dest;
                    }
                }

                // The sources are no longer in use.
                openRegions.RemoveWhere(sources.Contains);

                // Remove any connectors that aren't needed anymore.
                connectors.RemoveAll((pos) =>
                {
                    // Don't allow connectors right next to each other.
                    if ((connector - pos) < 2)
                    {
                        return true;
                    }

                    // If the connector no long spans different regions, we don't need it.
                    var tmpReg = connectorRegions[pos].Select((region) => merged[region]).Distinct().ToList();
                    
                    if (tmpReg.Count > 1) 
                        return false;

                    // This connecter isn't needed, but connect it occasionally so that the
                    // dungeon isn't singly-connected.
                    //if (rng.Next(extraConnectorChance) == 0)
                    //    _addJunction(pos);

                    return true;
                }
                    );
            }
        }

        private void _addJunction(Vec pos)
        {
            if (GetTile(pos + Direction.n) == Tiles.Floor && GetTile(pos + Direction.s) == Tiles.Floor)
                SetTile(pos, Tiles.ClosedDoor_NS);

            else if (GetTile(pos + Direction.e) == Tiles.Floor && GetTile(pos + Direction.w) == Tiles.Floor)
                SetTile(pos, Tiles.ClosedDoor_EW);

                //if (rng.Next(4) == 0)
                //{
                //    SetTile(pos, rng.Next(3) == 0 ? Tiles.OpenDoor : Tiles.Floor);
                //}
            else
            {
                SetTile(pos, Tiles.Floor);
            }
        }

        private void _removeDeadEnds()
        {
            var done = false;

            while (!done)
            {
                done = true;

                foreach (var pos in bounds.Inflate(-1).Iterate())
                {
                    if (GetTile(pos) == Tiles.Wall)
                        continue;

                    // If it only has one exit, it's a dead end.
                    var exits = 0;
                    foreach (var dir in Direction.cardinal)
                    {
                        if (GetTile(pos + dir) != Tiles.Wall)
                            exits++;
                    }

                    if (exits != 1)
                        continue;

                    done = false;

                    SetTile(pos, Tiles.Wall);
                    _regions[pos.x, pos.y] = 0;
                }
            }
        }


        /// <summary>
        /// Gets whether or not an opening can be carved from the given starting
        /// [Cell] at [pos] to the adjacent Cell facing [direction]. 
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="direction"></param>
        /// <returns>Returns 'true'
        /// if the starting Cell is in bounds and the destination Cell is filled
        /// (or out of bounds).</returns>
        private bool _canCarve(Vec pos, Direction direction)
        {
            // Must end in bounds.
            if (!bounds.Contains(pos + (direction*3))) return false;

            // Destination must not be open.
            return GetTile(pos + (direction*2)) == Tiles.Wall;
        }

        private void _startRegion()
        {
            _currentRegion++;
        }

        private void _carve(Vec pos, Tiles type = Tiles.Floor)
        {
            //if (type == null)
            //    type = Tiles.Floor;

            SetTile(pos, type);
            _regions[pos.x, pos.y] = _currentRegion;
        }

        public Tiles GetTile(Vec s)
        {
            return stage[s.x, s.y];
        }

        public void SetTile(Vec pos, Tiles type)
        {
            stage[pos.x, pos.y] = type;
        }

        public void _DebugRegions()
        {
            for (int y = 0; y < stage.height; y++)
            {
                for (int x = 0; x < stage.width; x++)
                {
                    Console.Write(_regions[x,y].ToString("D2"));
                }
                Console.Write('\n');
            }
        }
    }
}
