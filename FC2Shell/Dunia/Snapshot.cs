using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;
using System.Drawing.Imaging;

using System.Runtime.InteropServices;

namespace FC2Shell.Dunia
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Snapshot
    {
        private IntPtr m_pointer;
        public Snapshot(IntPtr ptr)
        {
            this.m_pointer = ptr;
        }

        public static Snapshot Create(int width, int height)
        {
            return new Snapshot(FCE_Snapshot_Create(width, height));
        }

        public void Destroy()
        {
            FCE_Snapshot_Destroy(this.m_pointer);
            this.m_pointer = IntPtr.Zero;
        }

        public Image GetImage()
        {
            IntPtr ptr;
            int num;
            int num2;
            int num3;
            FCE_Snapshot_GetData(this.m_pointer, out ptr, out num, out num2, out num3);
            Bitmap bitmap = new Bitmap(num, num2);
            BitmapData bitmapdata = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            for (int i = 0; i < bitmap.Height; i++)
            {
                Win32.RtlMoveMemory((IntPtr)(bitmapdata.Scan0.ToInt32() + (i * bitmapdata.Stride)), (IntPtr)(ptr.ToInt32() + (i * num3)), bitmap.Width * 4);
            }
            bitmap.UnlockBits(bitmapdata);
            return bitmap;
        }

        public IntPtr Pointer
        {
            get
            {
                return this.m_pointer;
            }
        }
        [DllImport("Dunia.dll")]
        private static extern IntPtr FCE_Snapshot_Create(int width, int height);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Snapshot_Destroy(IntPtr snapshot);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Snapshot_GetData(IntPtr snapshot, out IntPtr data, out int width, out int height, out int pitch);
    }
}
