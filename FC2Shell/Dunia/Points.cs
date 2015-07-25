using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.InteropServices;

namespace FC2Shell.Dunia
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Points
    {
        public static Points Null;
        private IntPtr m_pointsPtr;
        public Points(IntPtr pointsPtr)
        {
            this.m_pointsPtr = pointsPtr;
        }

        public static Points Create()
        {
            return new Points(FCE_Core_Points_Create());
        }

        public void Destroy()
        {
            FCE_Core_Points_Destroy(this.m_pointsPtr);
            this.m_pointsPtr = IntPtr.Zero;
        }

        public IntPtr Pointer
        {
            get
            {
                return this.m_pointsPtr;
            }
        }
        [DllImport("Dunia.dll")]
        private static extern IntPtr FCE_Core_Points_Create();
        [DllImport("Dunia.dll")]
        private static extern void FCE_Core_Points_Destroy(IntPtr points);
        static Points()
        {
            Null = new Points(IntPtr.Zero);
        }
    }


}
