using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.InteropServices;

namespace FC2Shell.Dunia
{
    [StructLayout(LayoutKind.Sequential)]
    public struct PaintBrush
    {
        private IntPtr m_pointer;
        public PaintBrush(IntPtr ptr)
        {
            this.m_pointer = ptr;
        }

        public static PaintBrush Create(bool circle, float radius, float hardness, float opacity, float distortion)
        {
            return new PaintBrush(FCE_Brush_Create(circle, radius, hardness, opacity, distortion));
        }

        public void Destroy()
        {
            FCE_Brush_Destroy(this.m_pointer);
            this.m_pointer = IntPtr.Zero;
        }

        public bool IsValid
        {
            get
            {
                return (this.m_pointer != IntPtr.Zero);
            }
        }
        public IntPtr Pointer
        {
            get
            {
                return this.m_pointer;
            }
        }
        [DllImport("Dunia.dll")]
        private static extern IntPtr FCE_Brush_Create(bool circle, float radius, float hardness, float opacity, float distortion);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Brush_Destroy(IntPtr brush);
    }


}
