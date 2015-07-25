using System;
using System.Collections.Generic;
using System.Text;

namespace FC2Shell.Dunia
{
    public class EditorObjectPivot
    {
        // Fields
        public Vec3 normal;
        public Vec3 normalUp;
        public Vec3 position;

        // Methods
        public void Unapply(EditorObject obj)
        {
            CoordinateSystem system = CoordinateSystem.FromAngles(obj.Angles);
            AABB localBounds = obj.LocalBounds;
            Vec3 vec = (Vec3)((localBounds.max + localBounds.min) * 0.5f);
            Vec3 vec2 = (Vec3)(localBounds.Length * 0.5f);
            this.position -= (Vec3)((obj.Position + (vec.X * system.axisX)) + (vec.Y * system.axisY));
            this.position = system.ConvertFromWorld(this.position);
            this.normal = system.ConvertFromWorld(this.normal);
            this.normalUp = system.ConvertFromWorld(this.normalUp);
            this.position.X /= vec2.X;
            this.position.Y /= vec2.Y;
            if (this.position.X > 1f)
            {
                this.position.X = 1f;
            }
            else if (this.position.X < -1f)
            {
                this.position.X = -1f;
            }
            if (this.position.Y > 1f)
            {
                this.position.Y = 1f;
            }
            else if (this.position.Y < -1f)
            {
                this.position.Y = -1f;
            }
            if (this.position.Z > 1f)
            {
                this.position.Z = 1f;
            }
            else if (this.position.Z < -1f)
            {
                this.position.Z = -1f;
            }
            this.normal.Z = 0f;
            this.normalUp = new Vec3(0f, 0f, 1f);
        }
    }


}
