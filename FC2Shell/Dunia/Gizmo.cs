using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.InteropServices;

namespace FC2Shell.Dunia
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Gizmo : IDisposable
    {
        public static Gizmo Null;
        private IntPtr m_gizmoPtr;
        public Gizmo(IntPtr ptr)
        {
            this.m_gizmoPtr = ptr;
        }

        public static Gizmo Create()
        {
            return new Gizmo(FCE_Gizmo_Create());
        }

        public void Dispose()
        {
            FCE_Gizmo_Destroy(this.m_gizmoPtr);
        }

        public Vec3 Position
        {
            get
            {
                Vec3 vec = new Vec3();
                FCE_Gizmo_GetPos(this.m_gizmoPtr, out vec.X, out vec.Y, out vec.Z);
                return vec;
            }
            set
            {
                FCE_Gizmo_SetPos(this.m_gizmoPtr, value.X, value.Y, value.Z);
            }
        }
        public CoordinateSystem Axis
        {
            get
            {
                CoordinateSystem system = new CoordinateSystem();
                FCE_Gizmo_GetAxis(this.m_gizmoPtr, out system.axisX.X, out system.axisX.Y, out system.axisX.Z, out system.axisY.X, out system.axisY.Y, out system.axisY.Z, out system.axisZ.X, out system.axisZ.Y, out system.axisZ.Z);
                return system;
            }
            set
            {
                FCE_Gizmo_SetAxis(this.m_gizmoPtr, value.axisX.X, value.axisX.Y, value.axisX.Z, value.axisY.X, value.axisY.Y, value.axisY.Z, value.axisZ.X, value.axisZ.Y, value.axisZ.Z);
            }
        }
        public Axis Active
        {
            get
            {
                return FCE_Gizmo_GetActive(this.m_gizmoPtr);
            }
            set
            {
                FCE_Gizmo_SetActive(this.m_gizmoPtr, value);
            }
        }
        public void Redraw()
        {
            FCE_Gizmo_Redraw(this.m_gizmoPtr);
        }

        public void Hide()
        {
            FCE_Gizmo_Hide(this.m_gizmoPtr);
        }

        public Axis HitTest(Vec3 raySrc, Vec3 rayDir)
        {
            return FCE_Gizmo_HitTest(this.m_gizmoPtr, raySrc.X, raySrc.Y, raySrc.Z, rayDir.X, rayDir.Y, rayDir.Z);
        }

        public bool IsValid
        {
            get
            {
                return (this.m_gizmoPtr != IntPtr.Zero);
            }
        }
        public IntPtr Pointer
        {
            get
            {
                return this.m_gizmoPtr;
            }
        }
        [DllImport("Dunia.dll")]
        private static extern IntPtr FCE_Gizmo_Create();
        [DllImport("Dunia.dll")]
        private static extern void FCE_Gizmo_Destroy(IntPtr ptr);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Gizmo_GetPos(IntPtr ptr, out float x, out float y, out float z);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Gizmo_SetPos(IntPtr ptr, float x, float y, float z);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Gizmo_GetAxis(IntPtr ptr, out float x1, out float y1, out float z1, out float x2, out float y2, out float z2, out float x3, out float y3, out float z3);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Gizmo_SetAxis(IntPtr ptr, float x1, float y1, float z1, float x2, float y2, float z2, float x3, float y3, float z3);
        [DllImport("Dunia.dll")]
        private static extern Axis FCE_Gizmo_GetActive(IntPtr ptr);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Gizmo_SetActive(IntPtr ptr, Axis axis);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Gizmo_Redraw(IntPtr ptr);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Gizmo_Hide(IntPtr ptr);
        [DllImport("Dunia.dll")]
        private static extern Axis FCE_Gizmo_HitTest(IntPtr ptr, float raySrcX, float raySrcY, float raySrcZ, float rayDirX, float rayDirY, float rayDirZ);
        static Gizmo()
        {
            Null = new Gizmo(IntPtr.Zero);
        }
    }


}
