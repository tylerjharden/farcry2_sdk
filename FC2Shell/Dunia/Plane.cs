using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.InteropServices;

namespace FC2Shell.Dunia
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Plane
    {
        public Vec3 normal;
        public float dist;
        public static Plane FromPoints(Vec3 p1, Vec3 p2, Vec3 p3)
        {
            Plane plane = new Plane();
            Vec3 vec = p2 - p1;
            Vec3 vec2 = p2 - p3;
            Vec3 vec3 = Vec3.Cross(vec, vec2);
            vec3.Normalize();
            plane.normal = vec3;
            plane.dist = Vec3.Dot(vec3, p1);
            return plane;
        }

        public static Plane FromPointNormal(Vec3 pt, Vec3 normal)
        {
            Plane plane = new Plane();
            plane.normal = normal;
            plane.dist = Vec3.Dot(normal, pt);
            return plane;
        }

        public bool RayIntersect(Vec3 raySrc, Vec3 rayDir, out Vec3 pt)
        {
            float num = Vec3.Dot(this.normal, rayDir);
            if (Math.Abs(num) < 0.0001f)
            {
                pt = new Vec3();
                return false;
            }
            float num2 = Vec3.Dot(this.normal, ((Vec3)(this.dist * this.normal)) - raySrc) / num;
            pt = raySrc + ((Vec3)(num2 * rayDir));
            return true;
        }
    }


}
