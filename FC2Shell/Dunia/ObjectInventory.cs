using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;

using System.Runtime.InteropServices;

namespace FC2Shell.Dunia
{
    public class ObjectInventory : Inventory
    {
        // Fields
        private static ObjectInventory s_instance = new ObjectInventory();

        // Methods
        [DllImport("Dunia.dll")]
        private static extern void FCE_Inventory_Object_AddPivot(IntPtr entry, float posX, float posY, float posZ, float normX, float normY, float normZ, float normUpX, float normUpY, float normUpZ);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Inventory_Object_ClearPivots(IntPtr entry);
        [DllImport("Dunia.dll")]
        private static extern IntPtr FCE_Inventory_Object_GetChild(IntPtr entry, int index);
        [DllImport("Dunia.dll")]
        private static extern int FCE_Inventory_Object_GetChildCount(IntPtr entry);
        [DllImport("Dunia.dll")]
        private static extern IntPtr FCE_Inventory_Object_GetDisplay(IntPtr entry);
        [DllImport("Dunia.dll")]
        private static extern uint FCE_Inventory_Object_GetId(IntPtr entry);
        [DllImport("Dunia.dll")]
        private static extern IntPtr FCE_Inventory_Object_GetParent(IntPtr entry);
        [DllImport("Dunia.dll")]
        private static extern int FCE_Inventory_Object_GetPivotCount(IntPtr entry);
        [DllImport("Dunia.dll")]
        private static extern IntPtr FCE_Inventory_Object_GetRoot();
        [DllImport("Dunia.dll")]
        private static extern float FCE_Inventory_Object_GetZOffset(IntPtr entry);
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport("Dunia.dll")]
        private static extern bool FCE_Inventory_Object_IsAutoOrientation(IntPtr entry);
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport("Dunia.dll")]
        private static extern bool FCE_Inventory_Object_IsAutoPivot(IntPtr entry);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Inventory_Object_SavePivots();
        [DllImport("Dunia.dll")]
        private static extern void FCE_Inventory_Object_SetAutoPivot(IntPtr entry, [MarshalAs(UnmanagedType.U1)] bool autoPivot);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Inventory_Object_SetPivot(IntPtr entry, int idx, float posX, float posY, float posZ, float normX, float normY, float normZ, float normUpX, float normUpY, float normUpZ);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Inventory_Object_SetPivots(IntPtr entry, float minX, float maxX, float minY, float maxY);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Inventory_Object_SetZOffset(IntPtr entry, float offset);
        public void SavePivots()
        {
            FCE_Inventory_Object_SavePivots();
        }

        // Properties
        public static ObjectInventory Instance
        {
            get
            {
                return s_instance;
            }
        }

        public override Inventory.Entry Root
        {
            get
            {
                return new Entry(FCE_Inventory_Object_GetRoot());
            }
        }

        // Nested Types
        public class Entry : Inventory.Entry
        {
            // Methods
            public Entry(IntPtr ptr)
                : base(ptr)
            {
            }

            public void AddPivot(EditorObjectPivot pivot)
            {
                ObjectInventory.FCE_Inventory_Object_AddPivot(base.m_entryPtr, pivot.position.X, pivot.position.Y, pivot.position.Z, pivot.normal.X, pivot.normal.Y, pivot.normal.Z, pivot.normalUp.X, pivot.normalUp.Y, pivot.normalUp.Z);
            }

            public void ClearPivots()
            {
                ObjectInventory.FCE_Inventory_Object_ClearPivots(base.m_entryPtr);
            }

            public void SetPivot(int idx, EditorObjectPivot pivot)
            {
                ObjectInventory.FCE_Inventory_Object_SetPivot(base.m_entryPtr, idx, pivot.position.X, pivot.position.Y, pivot.position.Z, pivot.normal.X, pivot.normal.Y, pivot.normal.Z, pivot.normalUp.X, pivot.normalUp.Y, pivot.normalUp.Z);
            }

            public void SetPivots(float minX, float maxX, float minY, float maxY)
            {
                ObjectInventory.FCE_Inventory_Object_SetPivots(base.m_entryPtr, minX, maxX, minY, maxY);
            }

            // Properties
            public bool AutoOrientation
            {
                get
                {
                    return ObjectInventory.FCE_Inventory_Object_IsAutoOrientation(base.m_entryPtr);
                }
            }

            public bool AutoPivot
            {
                get
                {
                    return ObjectInventory.FCE_Inventory_Object_IsAutoPivot(base.m_entryPtr);
                }
                set
                {
                    ObjectInventory.FCE_Inventory_Object_SetAutoPivot(base.m_entryPtr, value);
                }
            }

            public override Inventory.Entry[] Children
            {
                get
                {
                    int count = this.Count;
                    Inventory.Entry[] entryArray = new Inventory.Entry[count];
                    for (int i = 0; i < count; i++)
                    {
                        entryArray[i] = new ObjectInventory.Entry(ObjectInventory.FCE_Inventory_Object_GetChild(base.m_entryPtr, i));
                    }
                    return entryArray;
                }
            }

            public override int Count
            {
                get
                {
                    return ObjectInventory.FCE_Inventory_Object_GetChildCount(base.m_entryPtr);
                }
            }

            public override string DisplayName
            {
                get
                {
                    return Marshal.PtrToStringUni(ObjectInventory.FCE_Inventory_Object_GetDisplay(base.m_entryPtr));
                }
            }

            public override Image Icon
            {
                get
                {
                    if (this.Count <= 0)
                    {
                        // NOTE: No resources recovered yet
                        //return Resources.icon_object;
                        return null;
                    }
                    // NOTE: No resources recovered yet
                    //return Resources.icon_folder;
                    return null;
                }
            }

            public override string IconName
            {
                get
                {
                    if (this.Count <= 0)
                    {
                        return "icon_object";
                    }
                    return "icon_folder";
                }
            }

            public uint Id
            {
                get
                {
                    return ObjectInventory.FCE_Inventory_Object_GetId(base.m_entryPtr);
                }
            }

            public bool IsDirectory
            {
                get
                {
                    return (this.Count > 0);
                }
            }

            public override Inventory.Entry Parent
            {
                get
                {
                    return new ObjectInventory.Entry(ObjectInventory.FCE_Inventory_Object_GetParent(base.m_entryPtr));
                }
            }

            public int PivotCount
            {
                get
                {
                    return ObjectInventory.FCE_Inventory_Object_GetPivotCount(base.m_entryPtr);
                }
            }

            public float ZOffset
            {
                get
                {
                    return ObjectInventory.FCE_Inventory_Object_GetZOffset(base.m_entryPtr);
                }
                set
                {
                    ObjectInventory.FCE_Inventory_Object_SetZOffset(base.m_entryPtr, value);
                }
            }
        }
    }


}
