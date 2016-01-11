using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnityRoguelike
{
    public class DrunkardWalk
    {
        private readonly MersenneTwister mt;

        public DrunkardWalk(int seed)
        {
            mt = new MersenneTwister(seed);
        }

        public void Perform(Stage t, float percentFull)
        {
            fill(t, Tiles.Wall);

            var cp = new Vec(t.width/2, t.height/2); // currentPosition
            var cell = t[cp.x, cp.y] = Tiles.Floor;
            var step = mt.PickOne(Direction.cardinal)+cp;

            int empty = 0;
            while (empty < percentFull * t.width*t.height)
            {
                if (t.Bounds.Inflate(-1).Contains(step) && !CanWalk(t, step))
                {
                    t[step.x, step.y]=Tiles.Floor;
                    empty++;
                }
                else if (!t.Bounds.Inflate(-1).Contains(step))
                {
                    // pick another direction that is inside bounds.
                    step = mt.PickOne(Direction.cardinal) + cp;
                    continue;
                }

                // else just update step.
                cp = step;
                step = mt.PickOne(Direction.cardinal) + cp;
            }
        }

        private void fill(Stage stage, Tiles tile)
        {
            for (var y = 0; y < stage.height; y++)
            {
                for (var x = 0; x < stage.width; x++)
                {
                    stage[x, y] = tile;
                }
            }
        }

        public bool CanWalk(Stage s, Vec p)
        {
            if (!s.Bounds.Contains(p))
                return false;

            if (s[p.x, p.y] == Tiles.Wall)
                return false;

            if (s.Creatures.Any(i => i.Position == p && i.IsBlocking()))
                return false;

            return true;
        }

    }


}
