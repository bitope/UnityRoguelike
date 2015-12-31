using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityRoguelike
{
    [Serializable]
    public class Rect
    {
        [SerializeField]
        private Vec pos;

        [SerializeField]
        private Vec size;

        public int x { get { return pos.x; } }
        public int y { get { return pos.y; } }
        public int width { get { return size.x; } }
        public int height { get { return size.y; } }

        public int left { get { return Math.Min(x, x + width); } }
        public int right { get { return Math.Max(x, x + width); } }
        public int top { get { return Math.Min(y, y + height); } }
        public int bottom { get { return Math.Max(y, y + height); } }

        public Rect(int x, int y, int width, int height)
        {
            this.pos = new Vec(x,y);
            this.size = new Vec(width,height);
        }

        public int DistanceTo(Rect other)
        {
            int vertical;
            if (top >= other.bottom)
            {
                vertical = top - other.bottom;
            }
            else if (bottom <= other.top)
            {
                vertical = other.top - bottom;
            }
            else
            {
                vertical = -1;
            }

            int horizontal;
            if (left >= other.right)
            {
                horizontal = left - other.right;
            }
            else if (right <= other.left)
            {
                horizontal = other.left - right;
            }
            else
            {
                horizontal = -1;
            }

            if ((vertical == -1) && (horizontal == -1)) return -1;
            if (vertical == -1) return horizontal;
            if (horizontal == -1) return vertical;
            return horizontal + vertical;
        }

        public IEnumerable<Vec> Iterate()
        {
            for (int iy = top; iy < bottom; iy++)
            {
                for (int ix = left; ix < right; ix++)
                {
                    yield return new Vec(ix,iy);
                }
            }
        }

        public Rect Inflate(int distance)
        {
            return new Rect(x - distance, y - distance, width + (distance * 2), height + (distance * 2));
        }

        public bool Contains(Vec point)
        {
            if (point.x < pos.x) return false;
            if (point.x >= pos.x + size.x) return false;
            if (point.y < pos.y) return false;
            if (point.y >= pos.y + size.y) return false;

            return true;
        }

        public bool Contains(Rect rect)
        {
            if (rect.left < left) return false;
            if (rect.right > right) return false;
            if (rect.top < top) return false;
            if (rect.bottom > bottom) return false;
            return true;
        }

        public static Rect Intersect(Rect a, Rect b)
        {
            var left = Math.Max(a.left, b.left);
            var right = Math.Min(a.right, b.right);
            var top = Math.Max(a.top, b.top);
            var bottom = Math.Min(a.bottom, b.bottom);

            var width = Math.Max(0, right - left);
            var height = Math.Max(0, bottom - top);

            return new Rect(left,top, width, height);
        }

        public Rect Intersect(Rect b)
        {
            return Intersect(this, b);
        }

        private static Rect PosAndSize(Vec pos, Vec size)
        {
            var r = new Rect(0, 0, 0, 0);
            r.pos.x = pos.x;
            r.pos.y = pos.y;
            r.size.x = size.x;
            r.size.y = size.y;
            return r;
        }

        public Rect CenterIn(Rect toCenter, Rect main)
        {
            var p = main.pos + ((main.size - toCenter.size)/2);
            return PosAndSize(p, toCenter.size);
        }

        /// <summary>
        /// Returns an UNORDERED list of all cells around the outer edge. 
        /// (ie. they are NOT clockwise)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Vec> Trace()
        {
            if ((width > 1) && (height > 1))
            {
                for (int x = left; x < right; x++)
                {
                    yield return new Vec(x,top);
                    yield return new Vec(x, bottom-1);
                }

                for (int y = top +1; y < bottom -1; y++)
                {
                    yield return new Vec(left, y);
                    yield return new Vec(right-1,y);
                }
            }
            else if ((width >= 1) && (height == 1))
            {
                //single row
                for (int x = left; x < right; x++)
                {
                    yield return new Vec(x,top);
                }
            }
            else if ((height >= 1) && (width == 1))
            {
                //single column
                for (int y = top; y < bottom; y++)
                {
                    yield return new Vec(left, y);
                }
            }
        }

        public override string ToString()
        {
            return String.Format("Pos: {0}, Size: {1}", pos, size);
        }
    }
}