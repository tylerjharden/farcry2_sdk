using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.InteropServices;

namespace FC2Shell.Dunia
{
    public class Camera
    {
        // Methods
        [DllImport("Dunia.dll")]
        private static extern void FCE_Camera_GetAngles(out float x, out float y, out float z);
        [DllImport("Dunia.dll")]
        private static extern float FCE_Camera_GetFOV();
        [DllImport("Dunia.dll")]
        private static extern void FCE_Camera_GetFrontVector(out float x, out float y, out float z);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Camera_GetPos(out float x, out float y, out float z);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Camera_GetRightVector(out float x, out float y, out float z);
        [DllImport("Dunia.dll")]
        private static extern float FCE_Camera_GetSpeed();
        [DllImport("Dunia.dll")]
        private static extern void FCE_Camera_GetUpVector(out float x, out float y, out float z);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Camera_Input_Forward(float input);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Camera_Input_Lateral(float input);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Camera_Rotate(float pitch, float roll, float yaw);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Camera_SetAngles(float x, float y, float z);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Camera_SetPos(float x, float y, float z);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Camera_SetSpeed(float speed);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Camera_SetSpeedFactor(float input);      
      
        public static void Focus(EditorObject obj)
        {
            if (obj.IsValid)
            {
                AABB worldBounds = obj.WorldBounds;
                Vec3 center = worldBounds.Center;
                worldBounds -= center;
                Vec3 vec2 = (Vec3)(worldBounds.Length * 0.5f);
                Vec3 vec3 = -(FrontVector);
                Vec3 vec4 = vec3 * vec2;
                Position = center + ((Vec3)((vec3 * vec4.Length) * 4f));
            }
        }

        public static void Rotate(float pitch, float roll, float yaw)
        {
            FCE_Camera_Rotate(pitch, roll, yaw);
        }

        // Properties
        public static Vec3 Angles
        {
            get
            {
                Vec3 vec = new Vec3();
                FCE_Camera_GetAngles(out vec.X, out vec.Y, out vec.Z);
                return vec;
            }
            set
            {
                FCE_Camera_SetAngles(value.X, value.Y, value.Z);
            }
        }

        public static CoordinateSystem Axis
        {
            get
            {
                return new CoordinateSystem(RightVector, FrontVector, UpVector);
            }
        }

        public static float ForwardInput
        {
            set
            {
                FCE_Camera_Input_Forward(value);
            }
        }

        public static float FOV
        {
            get
            {
                return FCE_Camera_GetFOV();
            }
        }

        public static Vec3 FrontVector
        {
            get
            {
                Vec3 vec = new Vec3();
                FCE_Camera_GetFrontVector(out vec.X, out vec.Y, out vec.Z);
                return vec;
            }
        }

        public static float HalfFOV
        {
            get
            {
                return (FOV * 0.5f);
            }
        }

        public static float LateralInput
        {
            set
            {
                FCE_Camera_Input_Lateral(value);
            }
        }

        public static Vec3 Position
        {
            get
            {
                Vec3 vec = new Vec3();
                FCE_Camera_GetPos(out vec.X, out vec.Y, out vec.Z);
                return vec;
            }
            set
            {
                FCE_Camera_SetPos(value.X, value.Y, value.Z);
            }
        }

        public static Vec3 RightVector
        {
            get
            {
                Vec3 vec = new Vec3();
                FCE_Camera_GetRightVector(out vec.X, out vec.Y, out vec.Z);
                return vec;
            }
        }

        public static float Speed
        {
            get
            {
                return FCE_Camera_GetSpeed();
            }
            set
            {
                FCE_Camera_SetSpeed(value);
            }
        }

        public static float SpeedFactor
        {
            set
            {
                FCE_Camera_SetSpeedFactor(value);
            }
        }

        public static Vec3 UpVector
        {
            get
            {
                Vec3 vec = new Vec3();
                FCE_Camera_GetUpVector(out vec.X, out vec.Y, out vec.Z);
                return vec;
            }
        }
    }


}
