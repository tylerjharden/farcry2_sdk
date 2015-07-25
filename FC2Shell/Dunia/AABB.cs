using System;
using System.Runtime.InteropServices;

namespace FC2Shell.Dunia
{
    [StructLayout(LayoutKind.Sequential)]
    public struct AABB
    {
        public Vec3 min;
        public Vec3 max;
        public AABB(Vec3 min, Vec3 max)
        {
            this.min = min;
            this.max = max;
        }

        public static AABB operator -(AABB a, Vec3 b)
        {
            Vec3 min = a.min - b;
            return new AABB(min, a.max - b);
        }

        public Vec3 Length
        {
            get
            {
                return (this.max - this.min);
            }
        }
        public Vec3 Center
        {
            get
            {
                return (Vec3)((this.max + this.min) * 0.5f);
            }
        }
        public override string ToString()
        {
            Vec3 length = this.Length;
            return (length.X.ToString("F1") + " x " + length.Y.ToString("F1") + " x " + length.Z.ToString("F1") + " m");
        }
    }
}

