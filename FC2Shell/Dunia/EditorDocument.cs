using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.InteropServices;

using System.Windows.Forms;

using System.IO;

namespace FC2Shell.Dunia
{
    public class EditorDocument
    {
        // Fields
        private static LoadCompletedCallback m_loadCompletedCallback;
        private static SaveCompletedCallback m_saveCompletedCallback;

        // Methods
        public static void ClearSnapshot()
        {
            FCE_Document_ClearSnapshot();
        }

        public static void Export(string mapFile, string exportPath, bool toConsole)
        {
            FCE_Document_Export(mapFile, exportPath, toConsole);
        }

        [DllImport("Dunia.dll")]
        private static extern void FCE_Document_ClearSnapshot();
        [DllImport("Dunia.dll")]
        private static extern void FCE_Document_Export(string mapFile, string exportPath, bool toConsole);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Document_FinalizeMap();
        [DllImport("Dunia.dll")]
        private static extern IntPtr FCE_Document_GetAuthorName();
        [DllImport("Dunia.dll")]
        private static extern BattlefieldSizes FCE_Document_GetBattlefieldSize();
        [DllImport("Dunia.dll")]
        private static extern IntPtr FCE_Document_GetCreatorName();
        [DllImport("Dunia.dll")]
        private static extern IntPtr FCE_Document_GetMapName();
        [DllImport("Dunia.dll")]
        private static extern PlayerSizes FCE_Document_GetPlayerSize();
        [DllImport("Dunia.dll")]
        private static extern void FCE_Document_GetSnapshotAngle(out float x, out float y, out float z);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Document_GetSnapshotPos(out float x, out float y, out float z);
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport("Dunia.dll")]
        private static extern bool FCE_Document_IsSnapshotSet();
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport("Dunia.dll")]
        private static extern bool FCE_Document_Load(byte[] mapPath, byte[] mapName);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Document_Reset();
        [DllImport("Dunia.dll")]
        private static extern void FCE_Document_Save(byte[] mapPath, byte[] mapName);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Document_SetAuthorName(string name);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Document_SetBattlefieldSize(BattlefieldSizes size);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Document_SetCreatorName(string name);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Document_SetMapName(string name);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Document_SetPlayerSize(PlayerSizes size);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Document_SetSnapshotAngle(float x, float y, float z);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Document_SetSnapshotPos(float x, float y, float z);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Document_TakeSnapshot(IntPtr snapshot);
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport("Dunia.dll")]
        private static extern bool FCE_Document_Validate();
        public static void FinalizeMap()
        {
            FCE_Document_FinalizeMap();
        }

        public static bool Load(string fileName, LoadCompletedCallback callback)
        {
            string s = Path.GetDirectoryName(fileName) + Path.DirectorySeparatorChar;
            string str2 = Path.GetFileName(fileName);
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            byte[] mapName = Encoding.UTF8.GetBytes(str2);
            m_loadCompletedCallback = callback;
            return FCE_Document_Load(bytes, mapName);
        }

        public static void OnLoadCompleted(Editor.ResultCode resultCode)
        {
            bool success = resultCode == Editor.ResultCode.Succeeded;
            if (!success)
            {
                MessageBox.Show(Localizer.Localize("ERROR_LOAD_FAILED"), Localizer.Localize("ERROR"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
                //MainForm.Instance.ClearMapPath();
            }
            if (m_loadCompletedCallback != null)
            {
                m_loadCompletedCallback(success);
            }
        }

        public static void OnSaveCompleted(Editor.ResultCode resultCode)
        {
            bool success = resultCode == Editor.ResultCode.Succeeded;
            if (!success)
            {
                MessageBox.Show(Localizer.Localize("ERROR_SAVE_FAILED"), Localizer.Localize("ERROR"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            if (m_saveCompletedCallback != null)
            {
                m_saveCompletedCallback(success);
            }
        }

        public static void Reset()
        {
            FCE_Document_Reset();
            string userNameHelper = Win32.GetUserNameHelper();
            if (userNameHelper == null)
            {
                userNameHelper = Localizer.Localize("PARAM_UNDEFINED");
            }
            if (userNameHelper == null)
            {
                userNameHelper = "";
            }
            AuthorName = userNameHelper;
            string str2 = Localizer.Localize("EDITOR_UNTITLED");
            if (str2 != null)
            {
                MapName = str2;
            }
        }

        public static void Save(string fileName, SaveCompletedCallback callback)
        {
            string s = Path.GetDirectoryName(fileName) + Path.DirectorySeparatorChar;
            string str2 = Path.GetFileName(fileName);
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            byte[] mapName = Encoding.UTF8.GetBytes(str2);
            m_saveCompletedCallback = callback;
            FCE_Document_Save(bytes, mapName);
        }

        public static void TakeSnapshot(Snapshot snapshot)
        {
            FCE_Document_TakeSnapshot(snapshot.Pointer);
        }

        public static bool Validate()
        {
            return FCE_Document_Validate();
        }

        // Properties
        public static string AuthorName
        {
            get
            {
                return Marshal.PtrToStringAnsi(FCE_Document_GetAuthorName());
            }
            set
            {
                FCE_Document_SetAuthorName(value);
            }
        }

        public static BattlefieldSizes BattlefieldSize
        {
            get
            {
                return FCE_Document_GetBattlefieldSize();
            }
            set
            {
                FCE_Document_SetBattlefieldSize(value);
            }
        }

        public static string CreatorName
        {
            get
            {
                return Marshal.PtrToStringAnsi(FCE_Document_GetCreatorName());
            }
            set
            {
                FCE_Document_SetCreatorName(value);
            }
        }

        public static bool IsSnapshotSet
        {
            get
            {
                return FCE_Document_IsSnapshotSet();
            }
        }

        public static string MapName
        {
            get
            {
                return Marshal.PtrToStringAnsi(FCE_Document_GetMapName());
            }
            set
            {
                FCE_Document_SetMapName(value);
            }
        }

        public static PlayerSizes PlayerSize
        {
            get
            {
                return FCE_Document_GetPlayerSize();
            }
            set
            {
                FCE_Document_SetPlayerSize(value);
            }
        }

        public static Vec3 SnapshotAngle
        {
            get
            {
                float num;
                float num2;
                float num3;
                FCE_Document_GetSnapshotAngle(out num, out num2, out num3);
                return new Vec3(num, num2, num3);
            }
            set
            {
                FCE_Document_SetSnapshotAngle(value.X, value.Y, value.Z);
            }
        }

        public static Vec3 SnapshotPos
        {
            get
            {
                float num;
                float num2;
                float num3;
                FCE_Document_GetSnapshotPos(out num, out num2, out num3);
                return new Vec3(num, num2, num3);
            }
            set
            {
                FCE_Document_SetSnapshotPos(value.X, value.Y, value.Z);
            }
        }

        // Nested Types
        public enum BattlefieldSizes
        {
            Small,
            Medium,
            Large
        }

        public delegate void LoadCompletedCallback(bool success);

        public enum PlayerSizes
        {
            Small,
            Medium,
            Large,
            XLarge
        }

        public delegate void SaveCompletedCallback(bool success);
    }


}
