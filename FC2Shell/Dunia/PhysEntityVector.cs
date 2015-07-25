using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.InteropServices;

namespace FC2Shell.Dunia
{
    [StructLayout(LayoutKind.Sequential)]
    public struct PhysEntityVector : IDisposable
    {
        public static PhysEntityVector Null;
        private IntPtr m_pointer;
        public static PhysEntityVector Create()
        {
            return new PhysEntityVector(FCE_PhysEntityVector_Create());
        }

        public PhysEntityVector(IntPtr ptr)
        {
            this.m_pointer = ptr;
        }

        public void Dispose()
        {
            FCE_PhysEntityVector_Destroy(this.m_pointer);
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
        private static extern IntPtr FCE_PhysEntityVector_Create();
        [DllImport("Dunia.dll")]
        private static extern void FCE_PhysEntityVector_Destroy(IntPtr ptr);
        static PhysEntityVector()
        {
            Null = new PhysEntityVector();
        }
    }


}
