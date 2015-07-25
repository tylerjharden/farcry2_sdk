using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.InteropServices;

namespace FC2Shell.Dunia
{
    public class Spline : IDisposable
    {
        // Fields
        protected IntPtr m_splinePtr;
        public static Spline Null = new Spline(IntPtr.Zero);

        // Methods
        public Spline(IntPtr ptr)
        {
            this.m_splinePtr = ptr;
        }

        public void AddPoint(Vec2 point)
        {
            FCE_Spline_AddPoint(this.m_splinePtr, point.X, point.Y);
        }

        public void Clear()
        {
            FCE_Spline_Clear(this.m_splinePtr);
        }

        public static Spline Create()
        {
            return new Spline(FCE_Spline_Create());
        }

        public void Dispose()
        {
            FCE_Spline_Destroy(this.m_splinePtr);
        }

        public void Draw(float penWidth, SplineController controller)
        {
            FCE_Spline_Draw(this.m_splinePtr, penWidth, controller.Pointer);
        }

        [DllImport("Dunia.dll")]
        protected static extern void FCE_Spline_AddPoint(IntPtr spline, float x, float y);
        [DllImport("Dunia.dll")]
        protected static extern void FCE_Spline_Clear(IntPtr spline);
        [DllImport("Dunia.dll")]
        protected static extern IntPtr FCE_Spline_Create();
        [DllImport("Dunia.dll")]
        protected static extern void FCE_Spline_Destroy(IntPtr spline);
        [DllImport("Dunia.dll")]
        protected static extern void FCE_Spline_Draw(IntPtr spline, float penWidth, IntPtr controller);
        [DllImport("Dunia.dll")]
        protected static extern void FCE_Spline_FinalizeSpline(IntPtr spline);
        [DllImport("Dunia.dll")]
        protected static extern int FCE_Spline_GetNumPoints(IntPtr spline);
        [DllImport("Dunia.dll")]
        protected static extern void FCE_Spline_GetPoint(IntPtr spline, int i, out float x, out float y);
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport("Dunia.dll")]
        protected static extern bool FCE_Spline_HitTestPoints(IntPtr spline, float x, float y, float penWidth, float hitWidth, out int hitIndex, out float hitX, out float hitY);
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport("Dunia.dll")]
        protected static extern bool FCE_Spline_HitTestSegments(IntPtr spline, float centerX, float centerY, float radius, out int hitIndex, out float hitX, out float hitY);
        [DllImport("Dunia.dll")]
        protected static extern void FCE_Spline_InsertPoint(IntPtr spline, float x, float y, int index);
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport("Dunia.dll")]
        protected static extern bool FCE_Spline_OptimizePoint(IntPtr spline, int index);
        [DllImport("Dunia.dll")]
        protected static extern void FCE_Spline_RemovePoint(IntPtr spline, int index);
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport("Dunia.dll")]
        protected static extern bool FCE_Spline_RemoveSimilarPoints(IntPtr spline);
        [DllImport("Dunia.dll")]
        protected static extern void FCE_Spline_SetPoint(IntPtr spline, int i, float x, float y);
        [DllImport("Dunia.dll")]
        protected static extern void FCE_Spline_UpdateSpline(IntPtr spline);
        [DllImport("Dunia.dll")]
        protected static extern void FCE_Spline_UpdateSplineHeight(IntPtr spline);
        public void FinalizeSpline()
        {
            FCE_Spline_FinalizeSpline(this.m_splinePtr);
        }

        public bool HitTestPoints(Vec2 point, float penWidth, float hitWidth, out int hitIndex, out Vec2 hitPos)
        {
            return FCE_Spline_HitTestPoints(this.m_splinePtr, point.X, point.Y, penWidth, hitWidth, out hitIndex, out hitPos.X, out hitPos.Y);
        }

        public bool HitTestSegments(Vec2 center, float radius, out int hitIndex, out Vec2 hitPos)
        {
            return FCE_Spline_HitTestSegments(this.m_splinePtr, center.X, center.Y, radius, out hitIndex, out hitPos.X, out hitPos.Y);
        }

        public void InsertPoint(Vec2 point, int index)
        {
            FCE_Spline_InsertPoint(this.m_splinePtr, point.X, point.Y, index);
        }

        public bool OptimizePoint(int index)
        {
            return FCE_Spline_OptimizePoint(this.m_splinePtr, index);
        }

        public void RemovePoint(int index)
        {
            FCE_Spline_RemovePoint(this.m_splinePtr, index);
        }

        public bool RemoveSimilarPoints()
        {
            return FCE_Spline_RemoveSimilarPoints(this.m_splinePtr);
        }

        public void UpdateSpline()
        {
            FCE_Spline_UpdateSpline(this.m_splinePtr);
        }

        public void UpdateSplineHeight()
        {
            FCE_Spline_UpdateSplineHeight(this.m_splinePtr);
        }

        // Properties
        public int Count
        {
            get
            {
                return FCE_Spline_GetNumPoints(this.m_splinePtr);
            }
        }

        public bool IsValid
        {
            get
            {
                return (this.Pointer != IntPtr.Zero);
            }
        }

        public Vec2 this[int index]
        {
            get
            {
                Vec2 vec = new Vec2();
                FCE_Spline_GetPoint(this.m_splinePtr, index, out vec.X, out vec.Y);
                return vec;
            }
            set
            {
                FCE_Spline_SetPoint(this.m_splinePtr, index, value.X, value.Y);
            }
        }

        public IntPtr Pointer
        {
            get
            {
                return this.m_splinePtr;
            }
        }
    }
}
