using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UnityRoguelike
{
    [Serializable]
    public class Stage
    {
        public int width;
        public int height;

        [SerializeField]
        private readonly Tile[,] tiles;

        public readonly List<Rect> RoomRects;

        public Actor Player;
        public List<Actor> Creatures;

        public Stage(int width, int height)
        {
            this.width = width;
            this.height = height;
            tiles = new Tile[width, height];
            InitializeTiles();
            RoomRects = new List<Rect>();
            Creatures = new List<Actor>();
        }

        public void InitializeTiles()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    tiles[x, y] = new Tile();
                }
            }
        }

        public Tiles this[int x, int y]
        {
            get { return tiles[x, y].Type; }
            set { tiles[x, y].Type = value; }
        }

        public IEnumerable<Vec> GetAll(Tiles tile)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (tiles[x, y].Type == tile)
                        yield return new Vec(x, y);
                }
            }
        }

        public Rect Bounds { get { return new Rect(0, 0, width, height); } }

        public bool IsOpenSpace(int x, int y)
        {
            var p = new Vec(x, y);
            bool isFloor = tiles[p.x, p.y].Type != Tiles.Wall;
            bool isOccupied = Creatures.Any(c => c.Position == p);
            bool isReserved = Creatures.Any(c => c.NextPosition == p);
            bool isPlayer = Player != null && Player.Position == p;

            return (isFloor && !isOccupied && !isReserved && !isPlayer);
        }

        public bool IsOpenSpace(Vec pos)
        {
            return IsOpenSpace(pos.x, pos.y);
        }

        public bool HasDoorAtCardinalDirection(Vec pos)
        {
            var x = Direction.cardinal.Select(i => i + pos).Where(i => (tiles[i.x, i.y].Type).ToString().Contains("Door")).ToList();
            return x.Any();
        }

        public bool BlocksVision(Vec tile)
        {
            var blocksVision = new[] { Tiles.Wall, Tiles.Pillar, Tiles.ClosedDoor_NS, Tiles.ClosedDoor_EW };
            if (blocksVision.Contains(tiles[tile.x, tile.y].Type))
                return true;

            var isOccupied = Creatures.Any(c => c.Position == tile);
            return isOccupied;
        }

        public bool CheckLineOfSight(Vec start, Vec end)
        {
            var b = new Bresenham();
            var dest = new Vec();
            foreach (var step in b.Steps(start, end))
            {
                dest = step;
                if (BlocksVision(step))
                    break;
            }
            return dest == end;
        }
    }

    [Serializable]
    public class Tile
    {
        public Tiles Type;

    }

    [Serializable]
    public class Dungeon
    {
        public List<Stage> stages;

        public Dungeon()
        {
            stages = new List<Stage>();
        }
    }

}
