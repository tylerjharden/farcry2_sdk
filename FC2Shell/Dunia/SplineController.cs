using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;

using System.Runtime.InteropServices;

namespace FC2Shell.Dunia
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SplineController
    {
        public static SplineController Null;
        private IntPtr m_controllerPtr;
        public SplineController(IntPtr ptr)
        {
            this.m_controllerPtr = ptr;
        }

        public static SplineController Create()
        {
            return new SplineController(FCE_SplineController_Create());
        }

        public void Dispose()
        {
            FCE_SplineController_Destroy(this.m_controllerPtr);
        }

        public void SetSpline(Spline spline)
        {
            FCE_SplineController_SetSpline(this.m_controllerPtr, spline.Pointer);
        }

        public void ClearSelection()
        {
            FCE_SplineController_ClearSelection(this.m_controllerPtr);
        }

        public bool IsSelected(int index)
        {
            return FCE_SplineController_IsSelected(this.m_controllerPtr, index);
        }

        public void SetSelected(int index, bool selected)
        {
            FCE_SplineController_SetSelected(this.m_controllerPtr, index, selected);
        }

        public void SelectFromScreenRect(RectangleF rect, float penWidth, SelectMode selectMode)
        {
            FCE_SplineController_SelectFromScreenRect(this.m_controllerPtr, rect.X, rect.Y, rect.Right, rect.Bottom, penWidth, selectMode);
        }

        public void MoveSelection(Vec2 delta)
        {
            FCE_SplineController_MoveSelection(this.m_controllerPtr, delta.X, delta.Y);
        }

        public void DeleteSelection()
        {
            FCE_SplineController_DeleteSelection(this.m_controllerPtr);
        }

        public IntPtr Pointer
        {
            get
            {
                return this.m_controllerPtr;
            }
        }
        [DllImport("Dunia.dll")]
        private static extern IntPtr FCE_SplineController_Create();
        [DllImport("Dunia.dll")]
        private static extern void FCE_SplineController_Destroy(IntPtr controller);
        [DllImport("Dunia.dll")]
        private static extern void FCE_SplineController_SetSpline(IntPtr controller, IntPtr spline);
        [DllImport("Dunia.dll")]
        private static extern void FCE_SplineController_ClearSelection(IntPtr controller);
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport("Dunia.dll")]
        private static extern bool FCE_SplineController_IsSelected(IntPtr controller, int index);
        [DllImport("Dunia.dll")]
        private static extern void FCE_SplineController_SetSelected(IntPtr controller, int index, [MarshalAs(UnmanagedType.U1)] bool selected);
        [DllImport("Dunia.dll")]
        private static extern void FCE_SplineController_SelectFromScreenRect(IntPtr controller, float x1, float y1, float x2, float y2, float penWidth, SelectMode selectMode);
        [DllImport("Dunia.dll")]
        private static extern void FCE_SplineController_MoveSelection(IntPtr controller, float x, float y);
        [DllImport("Dunia.dll")]
        private static extern void FCE_SplineController_DeleteSelection(IntPtr controller);
        static SplineController()
        {
            Null = new SplineController(IntPtr.Zero);
        }
        // Nested Types
        public enum SelectMode
        {
            Replace,
            Add,
            Toggle
        }
    }


}
