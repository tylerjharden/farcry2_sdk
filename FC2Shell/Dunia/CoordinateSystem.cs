using System;
using System.Runtime.InteropServices;

namespace FC2Shell.Dunia
{
    [StructLayout(LayoutKind.Sequential)]
    public struct CoordinateSystem
    {
        public static CoordinateSystem Standard;
        public Vec3 axisX;
        public Vec3 axisY;
        public Vec3 axisZ;
        public CoordinateSystem(Vec3 x, Vec3 y, Vec3 z)
        {
            this.axisX = x;
            this.axisY = y;
            this.axisZ = z;
        }

        public static CoordinateSystem FromAngles(Vec3 angles)
        {
            CoordinateSystem system = new CoordinateSystem();
            FCE_Core_GetAxisFromAngles(angles.X, angles.Y, angles.Z, out system.axisX.X, out system.axisX.Y, out system.axisX.Z, out system.axisY.X, out system.axisY.Y, out system.axisY.Z, out system.axisZ.X, out system.axisZ.Y, out system.axisZ.Z);
            return system;
        }

        public Vec3 ToAngles()
        {
            Vec3 vec = new Vec3();
            FCE_Core_GetAnglesFromAxis(out vec.X, out vec.Y, out vec.Z, this.axisX.X, this.axisX.Y, this.axisX.Z, this.axisY.X, this.axisY.Y, this.axisY.Z, this.axisZ.X, this.axisZ.Y, this.axisZ.Z);
            return vec;
        }

        public Vec3 ConvertFromWorld(Vec3 pos)
        {
            return new Vec3(Vec3.Dot(pos, this.axisX), Vec3.Dot(pos, this.axisY), Vec3.Dot(pos, this.axisZ));
        }

        public Vec3 ConvertToWorld(Vec3 pos)
        {
            return (Vec3)(((pos.X * this.axisX) + (pos.Y * this.axisY)) + (pos.Z * this.axisZ));
        }

        public Vec3 ConvertFromSystem(Vec3 pos, CoordinateSystem coords)
        {
            Vec3 vec = coords.ConvertToWorld(pos);
            return this.ConvertFromWorld(vec);
        }

        public Vec3 ConvertToSystem(Vec3 pos, CoordinateSystem coords)
        {
            Vec3 vec = this.ConvertToWorld(pos);
            return coords.ConvertFromWorld(vec);
        }

        public Vec3 GetPivotPoint(Vec3 center, AABB bounds, Pivot pivot)
        {
            Vec3 vec = center;
            switch (pivot)
            {
                case Pivot.Left:
                    return (vec + ((Vec3)(this.axisX * bounds.min.X)));

                case Pivot.Right:
                    return (vec + ((Vec3)(this.axisX * bounds.max.X)));

                case Pivot.Down:
                    return (vec + ((Vec3)(this.axisY * bounds.min.Y)));

                case Pivot.Up:
                    return (vec + ((Vec3)(this.axisY * bounds.max.Y)));
            }
            return vec;
        }

        [DllImport("Dunia.dll")]
        private static extern void FCE_Core_GetAxisFromAngles(float angleX, float angleY, float angleZ, out float x1, out float y1, out float z1, out float x2, out float y2, out float z2, out float x3, out float y3, out float z3);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Core_GetAnglesFromAxis(out float angleX, out float angleY, out float angleZ, float x1, float y1, float z1, float x2, float y2, float z2, float x3, float y3, float z3);
        static CoordinateSystem()
        {
            Standard = new CoordinateSystem(new Vec3(1f, 0f, 0f), new Vec3(0f, 1f, 0f), new Vec3(0f, 0f, 1f));
        }
    }


}
