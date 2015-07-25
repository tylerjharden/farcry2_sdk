using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.InteropServices;

namespace FC2Shell.Dunia
{
    [StructLayout(LayoutKind.Sequential)]
    public struct EditorObject
    {
        public static EditorObject Null;
        private IntPtr m_objPtr;
        public EditorObject(IntPtr objPtr)
        {
            this.m_objPtr = objPtr;
        }

        public static EditorObject CreateFromEntry(ObjectInventory.Entry entry, bool managed)
        {
            return new EditorObject(FCE_Object_Create_FromEntry(entry.Pointer, managed));
        }

        public void Acquire()
        {
            FCE_Object_AddRef(this.m_objPtr);
        }

        public void Release()
        {
            FCE_Object_Release(this.m_objPtr);
        }

        public void Destroy()
        {
            FCE_Object_Destroy(this.m_objPtr);
            this.m_objPtr = IntPtr.Zero;
        }

        public bool IsValid
        {
            get
            {
                return (this.Pointer != IntPtr.Zero);
            }
        }
        public IntPtr Pointer
        {
            get
            {
                return this.m_objPtr;
            }
        }
        public EditorObject Clone()
        {
            return new EditorObject(FCE_Object_Clone(this.m_objPtr));
        }

        public bool IsLoaded
        {
            get
            {
                return FCE_Object_IsLoaded(this.m_objPtr);
            }
        }
        public ObjectInventory.Entry Entry
        {
            get
            {
                return new ObjectInventory.Entry(FCE_Object_GetEntry(this.m_objPtr));
            }
        }
        public Vec3 Position
        {
            get
            {
                Vec3 vec;
                FCE_Object_GetPos(this.m_objPtr, out vec.X, out vec.Y, out vec.Z);
                return vec;
            }
            set
            {
                FCE_Object_SetPos(this.m_objPtr, value.X, value.Y, value.Z);
            }
        }
        public Vec3 Angles
        {
            get
            {
                Vec3 vec;
                FCE_Object_GetAngles(this.m_objPtr, out vec.X, out vec.Y, out vec.Z);
                return vec;
            }
            set
            {
                FCE_Object_SetAngles(this.m_objPtr, value.X, value.Y, value.Z);
            }
        }
        public CoordinateSystem Axis
        {
            get
            {
                return CoordinateSystem.FromAngles(this.Angles);
            }
        }
        public Vec3 GetPivotPoint(Pivot pivot)
        {
            AABB localBounds;
            if (this.IsLoaded)
            {
                localBounds = this.LocalBounds;
            }
            else
            {
                localBounds = new AABB();
            }
            return this.Axis.GetPivotPoint(this.Position, localBounds, pivot);
        }

        public AABB LocalBounds
        {
            get
            {
                AABB aabb;
                FCE_Object_GetBounds(this.m_objPtr, false, out aabb.min.X, out aabb.min.Y, out aabb.min.Z, out aabb.max.X, out aabb.max.Y, out aabb.max.Z);
                return aabb;
            }
        }
        public AABB WorldBounds
        {
            get
            {
                AABB aabb;
                FCE_Object_GetBounds(this.m_objPtr, true, out aabb.min.X, out aabb.min.Y, out aabb.min.Z, out aabb.max.X, out aabb.max.Y, out aabb.max.Z);
                return aabb;
            }
        }
        public bool Visible
        {
            get
            {
                return FCE_Object_IsVisible(this.m_objPtr);
            }
            set
            {
                FCE_Object_SetVisible(this.m_objPtr, value);
            }
        }
        public bool HighlightState
        {
            set
            {
                FCE_Object_SetHighlight(this.m_objPtr, value);
            }
        }
        public bool Frozen
        {
            set
            {
                FCE_Object_SetFreeze(this.m_objPtr, value);
            }
        }
        public void DropToGround(bool physics)
        {
            FCE_Object_DropToGround(this.m_objPtr, physics);
        }

        public void ComputeAutoOrientation(ref Vec3 pos, out Vec3 angles, Vec3 normal)
        {
            angles = new Vec3();
            FCE_Object_ComputeAutoOrientation(this.m_objPtr, ref pos.X, ref pos.Y, ref pos.Z, out angles.X, out angles.Y, out angles.Z, normal.X, normal.Y, normal.Z);
        }

        public bool GetPivot(int idx, out EditorObjectPivot pivot)
        {
            pivot = new EditorObjectPivot();
            return FCE_Object_GetPivot(this.m_objPtr, idx, out pivot.position.X, out pivot.position.Y, out pivot.position.Z, out pivot.normal.X, out pivot.normal.Y, out pivot.normal.Z, out pivot.normalUp.X, out pivot.normalUp.Y, out pivot.normalUp.Z);
        }

        public bool GetClosestPivot(Vec3 pos, out EditorObjectPivot pivot)
        {
            return this.GetClosestPivot(pos, out pivot, float.MaxValue);
        }

        public bool GetClosestPivot(Vec3 pos, out EditorObjectPivot pivot, float minDist)
        {
            pivot = new EditorObjectPivot();
            return FCE_Object_GetClosestPivot(this.m_objPtr, pos.X, pos.Y, pos.Z, out pivot.position.X, out pivot.position.Y, out pivot.position.Z, out pivot.normal.X, out pivot.normal.Y, out pivot.normal.Z, out pivot.normalUp.X, out pivot.normalUp.Y, out pivot.normalUp.Z, minDist);
        }

        public void SnapToClosestObject()
        {
            FCE_Object_SnapToClosestObject(this.m_objPtr);
        }

        public void GetPhysEntities(PhysEntityVector vector)
        {
            FCE_Object_GetPhysEntities(this.m_objPtr, vector.Pointer);
        }

        [DllImport("Dunia.dll")]
        private static extern IntPtr FCE_Object_Create_FromEntry(IntPtr entry, [MarshalAs(UnmanagedType.U1)] bool managed);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Object_Destroy(IntPtr obj);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Object_AddRef(IntPtr obj);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Object_Release(IntPtr obj);
        [DllImport("Dunia.dll")]
        private static extern IntPtr FCE_Object_Clone(IntPtr obj);
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport("Dunia.dll")]
        private static extern bool FCE_Object_IsLoaded(IntPtr obj);
        [DllImport("Dunia.dll")]
        private static extern IntPtr FCE_Object_GetEntry(IntPtr obj);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Object_GetPos(IntPtr obj, out float x, out float y, out float z);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Object_SetPos(IntPtr obj, float x, float y, float z);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Object_GetAngles(IntPtr obj, out float x, out float y, out float z);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Object_SetAngles(IntPtr obj, float x, float y, float z);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Object_GetBounds(IntPtr obj, [MarshalAs(UnmanagedType.U1)] bool world, out float x1, out float y1, out float z1, out float x2, out float y2, out float z2);
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport("Dunia.dll")]
        private static extern bool FCE_Object_IsVisible(IntPtr obj);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Object_SetVisible(IntPtr obj, [MarshalAs(UnmanagedType.U1)] bool visible);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Object_SetHighlight(IntPtr obj, [MarshalAs(UnmanagedType.U1)] bool highlight);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Object_SetFreeze(IntPtr obj, [MarshalAs(UnmanagedType.U1)] bool freeze);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Object_DropToGround(IntPtr obj, [MarshalAs(UnmanagedType.U1)] bool physics);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Object_ComputeAutoOrientation(IntPtr obj, ref float x, ref float y, ref float z, out float angleX, out float angleY, out float angleZ, float normX, float normY, float normZ);
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport("Dunia.dll")]
        private static extern bool FCE_Object_GetPivot(IntPtr obj, int idx, out float x, out float y, out float z, out float normX, out float normY, out float normZ, out float normUpX, out float normUpY, out float normUpZ);
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport("Dunia.dll")]
        private static extern bool FCE_Object_GetClosestPivot(IntPtr obj, float posX, float posY, float posZ, out float pivotX, out float pivotY, out float pivotZ, out float normX, out float normY, out float normZ, out float normUpX, out float normUpY, out float normUpZ, float minDist);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Object_SnapToClosestObject(IntPtr obj);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Object_GetPhysEntities(IntPtr obj, IntPtr vector);
        static EditorObject()
        {
            Null = new EditorObject(IntPtr.Zero);
        }
    }


}
