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
        private readonly int[,] tiles;
        
        public readonly List<Rect> RoomRects;

        public Actor Player;
        public List<Actor> Creatures; 

        public Stage(int width, int height)
        {
            this.width = width;
            this.height = height;
            tiles = new int[width,height];
            RoomRects = new List<Rect>();
            Creatures = new List<Actor>();
        }

        public Tiles this[int x, int y]
        {
            get { return (Tiles) tiles[x, y]; }
            set { tiles[x, y] = (int) value; }
        }

        public IEnumerable<Vec> GetAll(Tiles tile)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (tiles[x,y]==(int)tile)
                        yield return new Vec(x,y);
                }
            }
        }

        public Rect Bounds { get{ return new Rect(0, 0, width, height); } }

        public bool isOpenSpace(int x, int y)
        {
            var p = new Vec(x, y);
            bool isFloor = tiles[p.x, p.y] != (int) Tiles.Wall;
            bool isOccupied = Creatures.Any(c => c.Position == p);
            bool isReserved = Creatures.Any(c => c.NextPosition == p);
            bool isPlayer = Player!=null && Player.Position == p;

            return (isFloor && !isOccupied && !isReserved && !isPlayer);
        }

        public bool BlocksVision(Vec tile)
        {
            var blocksVision = new[] {Tiles.Wall, Tiles.Pillar, Tiles.ClosedDoor_NS, Tiles.ClosedDoor_EW};
            if (blocksVision.Contains((Tiles) tiles[tile.x, tile.y]))
                return true;

            var isOccupied = Creatures.Any(c => c.Position == tile);
            return isOccupied;
        }
    }
}
