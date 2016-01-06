using System.Collections.Generic;

namespace UnityRoguelike
{
    public class Bresenham
    {
        private class BresenhamData
        {
            public int stepx;
            public int stepy;
            public int e;
            public int deltax;
            public int deltay;
            public int origx;
            public int origy;
            public int destx;
            public int desty; 
        }

        private BresenhamData data;
        private int XCur;
        private int YCur;

        public Bresenham()
        {
            
        }

        private Bresenham(int xFrom, int yFrom, int xTo, int yTo)
        {
            data = new BresenhamData();
            data.origx = xFrom;
            data.origy = yFrom;
            data.destx = xTo;
            data.desty = yTo;
            data.deltax = xTo - xFrom; 
            data.deltay = yTo - yFrom;

            if (data.deltax > 0)
            {
                data.stepx = 1;
            }
            else if (data.deltax < 0)
            {
                data.stepx = -1;
            }
            else data.stepx = 0;
            if (data.deltay > 0)
            {
                data.stepy = 1;
            }
            else if (data.deltay < 0)
            {
                data.stepy = -1;
            }
            else data.stepy = 0;
            if (data.stepx * data.deltax > data.stepy * data.deltay)
            {
                data.e = data.stepx * data.deltax;
                data.deltax *= 2;
                data.deltay *= 2;
            }
            else
            {
                data.e = data.stepy * data.deltay;
                data.deltax *= 2;
                data.deltay *= 2;
            }
        }

        private bool Step()
        {
            if (data.stepx * data.deltax > data.stepy * data.deltay)
            {
                if (data.origx == data.destx) return true;
                data.origx += data.stepx;
                data.e -= data.stepy * data.deltay;
                if (data.e < 0)
                {
                    data.origy += data.stepy;
                    data.e += data.stepx * data.deltax;
                }
            }
            else
            {
                if (data.origy == data.desty) return true;
                data.origy += data.stepy;
                data.e -= data.stepx * data.deltax;
                if (data.e < 0)
                {
                    data.origx += data.stepx;
                    data.e += data.stepy * data.deltay;
                }
            }
            XCur = data.origx;
            YCur = data.origy;
            return false;
        }

        public IEnumerable<Vec> Steps(int xFrom, int yFrom, int xTo, int yTo)
        {
            var b = new Bresenham(xFrom,yFrom,xTo,yTo);
            while (!b.Step())
            {
                yield return new Vec(b.XCur,b.YCur);
            }
            //yield return new Vec(b.XCur, b.YCur);
        }

        public IEnumerable<Vec> Steps(Vec from, Vec to)
        {
            var b = new Bresenham(from.x, from.y, to.x, to.y);
            while (!b.Step())
            {
                yield return new Vec(b.XCur, b.YCur);
            }
            //yield return new Vec(b.XCur, b.YCur);
        }
    }
}