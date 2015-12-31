namespace UnityRoguelike
{
    public class Direction : Vec
    {
        public static Direction none = new Direction(0, 0);

        public static Direction n = new Direction(0, -1);
        public static Direction ne = new Direction(1, -1);
        public static Direction e = new Direction(1, 0);
        public static Direction se = new Direction(1, 1);
        public static Direction s = new Direction(0, 1);
        public static Direction sw = new Direction(-1, 1);
        public static Direction w = new Direction(-1, 0);
        public static Direction nw = new Direction(-1, -1);

        /// The eight cardinal and intercardinal directions.
        public static Direction[] all = {n, ne, e, se, s, sw, w, nw};

        /// The four cardinal directions: north, south, east, and west.
        public static Direction[] cardinal = {n, e, s, w};

        /// The four directions between the cardinal ones: northwest, northeast,
        /// southwest and southeast.
        public static Direction[] intercardinal = {ne, se, sw, nw};

        public Direction(int x, int y) : base(x, y)
        {

        }
    }
}