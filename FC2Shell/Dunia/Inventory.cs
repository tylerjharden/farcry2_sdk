using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;

namespace FC2Shell.Dunia
{
    public abstract class Inventory
    {
        // Methods
        protected Inventory()
        {
        }

        // Properties
        public abstract Entry Root { get; }

        // Nested Types
        public abstract class Entry
        {
            // Fields
            protected IntPtr m_entryPtr;

            // Methods
            public Entry(IntPtr ptr)
            {
                this.m_entryPtr = ptr;
            }

            public override bool Equals(object obj)
            {
                Inventory.Entry entry = obj as Inventory.Entry;
                if (entry == null)
                {
                    return base.Equals(obj);
                }
                return (this.Pointer == entry.Pointer);
            }

            public override int GetHashCode()
            {
                return this.Pointer.ToInt32();
            }

            // Properties
            public abstract Inventory.Entry[] Children { get; }

            public abstract int Count { get; }

            public abstract string DisplayName { get; }

            public abstract Image Icon { get; }

            public abstract string IconName { get; }

            public bool IsValid
            {
                get
                {
                    return (this.m_entryPtr != IntPtr.Zero);
                }
            }

            public abstract Inventory.Entry Parent { get; }

            public IntPtr Pointer
            {
                get
                {
                    return this.m_entryPtr;
                }
            }
        }

    }
}
