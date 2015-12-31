using System;

namespace UnityRoguelike
{
    /// <summary>
    /// Integer-based 2d-vector. 
    /// </summary>
    [Serializable]
    public class Vec
    {

        public int x;
        public int y;

        public Vec(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Vec() : this(0, 0)
        {
        }

        public static Vec operator +(Vec a, Vec b)
        {
            return new Vec(a.x + b.x, a.y + b.y);
        }

        public static Vec operator -(Vec a, Vec b)
        {
            return new Vec(a.x - b.x, a.y - b.y);
        }

        public static Vec operator *(Vec a, int b)
        {
            return new Vec(a.x * b , a.y * b);
        }

        public static Vec operator /(Vec a, int b)
        {
            return new Vec(a.x / b, a.y / b);
        }

        public static bool operator <(Vec a, int b)
        {
            return a.LengthSquared() < b * b;
        }

        public static bool operator >(Vec a, int b)
        {
            return a.LengthSquared() > b * b;
        }

        public static bool operator <(Vec a, Vec b)
        {
            return a.LengthSquared() < b.LengthSquared();
        }

        public static bool operator >(Vec a, Vec b)
        {
            return a.LengthSquared() > b.LengthSquared();
        }

        public static bool operator ==(Vec a, Vec b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (ReferenceEquals(null, a)) return false;
            return a.Equals((object)b);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Vec) obj);
        }

        protected bool Equals(Vec other)
        {
            return x == other.x && y == other.y;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (x * 397) ^ y;
            }
        }

        public static bool operator !=(Vec a, Vec b)
        {
            return !(a == b);
        }

        public int LengthSquared()
        {
            return x*x + y*y;
        }

        public override string ToString()
        {
            return String.Format("{0},{1}", x.ToString("D2"), y.ToString("D2"));
        }
    }
}