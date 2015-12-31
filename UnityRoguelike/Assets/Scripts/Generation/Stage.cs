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

        public Stage(int width, int height)
        {
            this.width = width;
            this.height = height;
            tiles = new int[width,height];
            RoomRects = new List<Rect>();
        }

        public Tiles this[int x, int y]
        {
            get { return (Tiles) tiles[x, y]; }
            set { tiles[x, y] = (int) value; }
        }

        public Rect Bounds { get{ return new Rect(0, 0, width, height); } }

    }
}
