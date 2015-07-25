using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.InteropServices;

namespace FC2Shell.Dunia
{
   [StructLayout(LayoutKind.Sequential)]
public struct EditorObjectSelection : IDisposable
{
    public static EditorObject Null;
    private IntPtr m_selPtr;
    public EditorObjectSelection(IntPtr ptr)
    {
        this.m_selPtr = ptr;
    }

    public static EditorObjectSelection Create()
    {
        return new EditorObjectSelection(FCE_ObjectSelection_Create());
    }

    public void Dispose()
    {
        FCE_ObjectSelection_Destroy(this.m_selPtr);
    }

    public IntPtr Pointer
    {
        get
        {
            return this.m_selPtr;
        }
    }
    [DllImport("Dunia.dll")]
    private static extern IntPtr FCE_ObjectSelection_Create();
    [DllImport("Dunia.dll")]
    private static extern void FCE_ObjectSelection_Destroy(IntPtr ptr);
    public int Count
    {
        get
        {
            return FCE_ObjectSelection_GetCount(this.m_selPtr);
        }
    }
    public EditorObject this[int index]
    {
        get
        {
            return new EditorObject(FCE_ObjectSelection_Get(this.m_selPtr, index));
        }
    }
    
    public void Clear()
    {
        FCE_ObjectSelection_Clear(this.m_selPtr);
    }

    public void AddObject(EditorObject obj)
    {
        FCE_ObjectSelection_Add(this.m_selPtr, obj.Pointer);
    }

    public void AddSelection(EditorObjectSelection selection)
    {
        FCE_ObjectSelection_AddSelection(this.m_selPtr, selection.Pointer);
    }

    public void GetValidObjects(EditorObjectSelection selection)
    {
        FCE_ObjectSelection_GetValidObjects(this.m_selPtr, selection.Pointer);
    }

    public void RemoveInvalidObjects()
    {
        FCE_ObjectSelection_RemoveInvalidObjects(this.m_selPtr);
    }

    public void Clone(EditorObjectSelection newSelection, bool cloneObjects)
    {
        FCE_ObjectSelection_Clone(this.m_selPtr, newSelection.Pointer, cloneObjects);
    }

    public void Delete()
    {
        FCE_ObjectSelection_Delete(this.m_selPtr);
    }

    public void ToggleObject(EditorObject obj)
    {
        FCE_ObjectSelection_ToggleObject(this.m_selPtr, obj.Pointer);
    }

    public void ToggleSelection(EditorObjectSelection selection)
    {
        FCE_ObjectSelection_ToggleSelection(this.m_selPtr, selection.Pointer);
    }

    public int IndexOf(EditorObject obj)
    {
        for (int i = 0; i < this.Count; i++)
        {
            EditorObject obj2 = this[i];
            if (obj2.Pointer == obj.Pointer)
            {
                return i;
            }
        }
        return -1;
    }

    public bool Contains(EditorObject obj)
    {
        return (this.IndexOf(obj) != -1);
    }

    public Vec3 Center
    {
        get
        {
            Vec3 vec = new Vec3();
            FCE_ObjectSelection_GetCenter(this.m_selPtr, out vec.X, out vec.Y, out vec.Z);
            return vec;
        }
        set
        {
            FCE_ObjectSelection_SetCenter(this.m_selPtr, value.X, value.Y, value.Z);
        }
    }
    public Vec3 GetComputeCenter()
    {
        Vec3 vec = new Vec3();
        FCE_ObjectSelection_GetComputeCenter(this.m_selPtr, out vec.X, out vec.Y, out vec.Z);
        return vec;
    }

    public void ComputeCenter()
    {
        FCE_ObjectSelection_ComputeCenter(this.m_selPtr);
    }

    public AABB WorldBounds
    {
        get
        {
            AABB aabb = new AABB();
            FCE_ObjectSelection_GetWorldBounds(this.m_selPtr, out aabb.min.X, out aabb.min.Y, out aabb.min.Z, out aabb.max.X, out aabb.max.Y, out aabb.max.Z);
            return aabb;
        }
    }
    public void MoveTo(Vec3 pos, MoveMode mode)
    {
        FCE_ObjectSelection_MoveTo(this.m_selPtr, pos.X, pos.Y, pos.Z, mode);
    }

    public void Rotate(float angle, Vec3 axis, Vec3 pivot, bool affectCenter)
    {
        FCE_ObjectSelection_Rotate(this.m_selPtr, angle, axis.X, axis.Y, axis.Z, pivot.X, pivot.Y, pivot.Z, affectCenter);
    }

    public void Rotate(Vec3 angles, Vec3 axis, Vec3 pivot, bool affectCenter)
    {
        FCE_ObjectSelection_Rotate3(this.m_selPtr, angles.X, angles.Y, angles.Z, axis.X, axis.Y, axis.Z, pivot.X, pivot.Y, pivot.Z, affectCenter);
    }

    public void RotateCenter(float angle, Vec3 axis)
    {
        FCE_ObjectSelection_RotateCenter(this.m_selPtr, angle, axis.X, axis.Y, axis.Z);
    }

    public void RotateLocal(Vec3 angles)
    {
        FCE_ObjectSelection_RotateLocal3(this.m_selPtr, angles.X, angles.Y, angles.Z);
    }

    public void RotateGimbal(Vec3 angles)
    {
        FCE_ObjectSelection_RotateGimbal(this.m_selPtr, angles.X, angles.Y, angles.Z);
    }

    public void SetPos(Vec3 pos)
    {
        //foreach (EditorObject obj2 in this.GetObjects())
        //{
            //obj2.Position = pos;
        //}
    }

    public void SetAngles(Vec3 angles)
    {
        //foreach (EditorObject obj2 in this.GetObjects())
        //{
            //obj2.Angles = angles;
        //}
    }

    public void DropToGround(bool physics, bool group)
    {
        FCE_ObjectSelection_DropToGround(this.m_selPtr, physics, group);
    }

    public void SnapToPivot(EditorObjectPivot source, EditorObjectPivot target, bool preserveOrientation, float snapAngle)
    {
        FCE_ObjectSelection_SnapToPivot(this.m_selPtr, source.position.X, source.position.Y, source.position.Z, source.normal.X, source.normal.Y, source.normal.Z, source.normalUp.X, source.normalUp.Y, source.normalUp.Z, target.position.X, target.position.Y, target.position.Z, target.normal.X, target.normal.Y, target.normal.Z, target.normalUp.X, target.normalUp.Y, target.normalUp.Z, preserveOrientation, snapAngle);
    }

    public void SnapToClosestObjects()
    {
        FCE_ObjectSelection_SnapToClosestObjects(this.m_selPtr);
    }

    public void GetPhysEntities(PhysEntityVector vector)
    {
        FCE_ObjectSelection_GetPhysEntities(this.m_selPtr, vector.Pointer);
    }

    public void ClearState()
    {
        FCE_ObjectSelection_ClearState(this.m_selPtr);
    }

    public void LoadState()
    {
        FCE_ObjectSelection_LoadState(this.m_selPtr);
    }

    public void SaveState()
    {
        FCE_ObjectSelection_SaveState(this.m_selPtr);
    }

    [DllImport("Dunia.dll")]
    private static extern int FCE_ObjectSelection_GetCount(IntPtr ptr);
    [DllImport("Dunia.dll")]
    private static extern IntPtr FCE_ObjectSelection_Get(IntPtr ptr, int index);
    [DllImport("Dunia.dll")]
    private static extern void FCE_ObjectSelection_Clear(IntPtr ptr);
    [DllImport("Dunia.dll")]
    private static extern void FCE_ObjectSelection_Add(IntPtr ptr, IntPtr obj);
    [DllImport("Dunia.dll")]
    private static extern void FCE_ObjectSelection_AddSelection(IntPtr ptr, IntPtr obj);
    [DllImport("Dunia.dll")]
    private static extern void FCE_ObjectSelection_Clone(IntPtr ptr, IntPtr selection, [MarshalAs(UnmanagedType.U1)] bool cloneObjects);
    [DllImport("Dunia.dll")]
    private static extern void FCE_ObjectSelection_Delete(IntPtr ptr);
    [DllImport("Dunia.dll")]
    private static extern void FCE_ObjectSelection_ToggleObject(IntPtr ptr, IntPtr obj);
    [DllImport("Dunia.dll")]
    private static extern void FCE_ObjectSelection_ToggleSelection(IntPtr ptr, IntPtr selection);
    [DllImport("Dunia.dll")]
    private static extern void FCE_ObjectSelection_GetValidObjects(IntPtr ptr, IntPtr selection);
    [DllImport("Dunia.dll")]
    private static extern void FCE_ObjectSelection_RemoveInvalidObjects(IntPtr ptr);
    [DllImport("Dunia.dll")]
    private static extern void FCE_ObjectSelection_GetCenter(IntPtr ptr, out float x, out float y, out float z);
    [DllImport("Dunia.dll")]
    private static extern void FCE_ObjectSelection_SetCenter(IntPtr ptr, float x, float y, float z);
    [DllImport("Dunia.dll")]
    private static extern void FCE_ObjectSelection_GetComputeCenter(IntPtr ptr, out float x, out float y, out float z);
    [DllImport("Dunia.dll")]
    private static extern void FCE_ObjectSelection_ComputeCenter(IntPtr ptr);
    [DllImport("Dunia.dll")]
    private static extern void FCE_ObjectSelection_GetWorldBounds(IntPtr ptr, out float x1, out float y1, out float z1, out float x2, out float y2, out float z2);
    [DllImport("Dunia.dll")]
    private static extern void FCE_ObjectSelection_MoveTo(IntPtr ptr, float x, float y, float z, MoveMode mode);
    [DllImport("Dunia.dll")]
    private static extern void FCE_ObjectSelection_Rotate(IntPtr ptr, float angle, float axisX, float axisY, float axisZ, float pivotX, float pivotY, float pivotZ, [MarshalAs(UnmanagedType.U1)] bool affectCenter);
    [DllImport("Dunia.dll")]
    private static extern void FCE_ObjectSelection_Rotate3(IntPtr ptr, float angleX, float angleY, float angleZ, float axisX, float axisY, float axisZ, float pivotX, float pivotY, float pivotZ, [MarshalAs(UnmanagedType.U1)] bool affectCenter);
    [DllImport("Dunia.dll")]
    private static extern void FCE_ObjectSelection_RotateCenter(IntPtr ptr, float angle, float x, float y, float z);
    [DllImport("Dunia.dll")]
    private static extern void FCE_ObjectSelection_RotateLocal3(IntPtr ptr, float angleX, float angleY, float angleZ);
    [DllImport("Dunia.dll")]
    private static extern void FCE_ObjectSelection_RotateGimbal(IntPtr ptr, float x, float y, float z);
    [DllImport("Dunia.dll")]
    private static extern void FCE_ObjectSelection_DropToGround(IntPtr ptr, [MarshalAs(UnmanagedType.U1)] bool physics, [MarshalAs(UnmanagedType.U1)] bool group);
    [DllImport("Dunia.dll")]
    private static extern void FCE_ObjectSelection_SnapToPivot(IntPtr ptr, float sourcePosX, float sourcePosY, float sourcePosZ, float sourceNormX, float sourceNormY, float sourceNormZ, float sourceNormUpX, float sourceNormUpY, float sourceNormUpZ, float targetPosX, float targetPosY, float targetPosZ, float targetNormX, float targetNormY, float targetNormZ, float targetNormUpX, float targetNormUpY, float targetNormUpZ, [MarshalAs(UnmanagedType.U1)] bool preserveOrientation, float snapAngle);
    [DllImport("Dunia.dll")]
    private static extern void FCE_ObjectSelection_SnapToClosestObjects(IntPtr ptr);
    [DllImport("Dunia.dll")]
    private static extern void FCE_ObjectSelection_GetPhysEntities(IntPtr ptr, IntPtr vector);
    [DllImport("Dunia.dll")]
    private static extern void FCE_ObjectSelection_ClearState(IntPtr ptr);
    [DllImport("Dunia.dll")]
    private static extern void FCE_ObjectSelection_LoadState(IntPtr ptr);
    [DllImport("Dunia.dll")]
    private static extern void FCE_ObjectSelection_SaveState(IntPtr ptr);
    static EditorObjectSelection()
    {
        Null = new EditorObject(IntPtr.Zero);
    }
    
    public enum MoveMode
    {
        MoveNormal,
        MoveKeepHeight,
        MoveSnapToTerrain,
        MoveKeepAboveTerrain
    }
}


}
