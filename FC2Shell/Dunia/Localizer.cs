using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.InteropServices;

namespace FC2Shell.Dunia
{
    public static class Localizer
    {
        // Methods
        public static string Localize(string key)
        {
            return LocalizeInternal("InGameEditor_PC", key);
        }

        
        public static string Localize(string section, string key)
        {
            if (!Engine.Initialized)
            {
                return "!DLL_NOT_LOADED";
            }
            return LocalizeInternal(section, key);
        }

        public static string LocalizeCommon(string key)
        {
            return LocalizeInternal("InGameEditor", key);
        }

        private static string LocalizeInternal(string section, string key)
        {
            return Marshal.PtrToStringUni(LocalizeText(section, key));
        }

        [DllImport("Dunia.dll")]
        private static extern IntPtr LocalizeText([MarshalAs(UnmanagedType.LPStr)] string section, [MarshalAs(UnmanagedType.LPStr)] string text);
    }


}
