using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.InteropServices;

namespace FC2Shell.Dunia
{
    public class EditorSettings
    {
        // Fields
        private static bool m_invertMousePan;
        private static bool m_invertMouseView;
        private static float m_viewportQuality = 1f;

        // Methods
        [return: MarshalAs(UnmanagedType.U4)]
        [DllImport("Dunia.dll")]
        private static extern QualityLevel FCE_EditorSettings_GetEngineQuality();
        [DllImport("Dunia.dll")]
        private static extern int FCE_EditorSettings_GetGridResolution();
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport("Dunia.dll")]
        private static extern bool FCE_EditorSettings_IsAutoSnappingObjects();
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport("Dunia.dll")]
        private static extern bool FCE_EditorSettings_IsAutoSnappingObjectsRotation();
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport("Dunia.dll")]
        private static extern bool FCE_EditorSettings_IsAutoSnappingObjectsTerrain();
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport("Dunia.dll")]
        private static extern bool FCE_EditorSettings_IsCameraClippedTerrain();
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport("Dunia.dll")]
        private static extern bool FCE_EditorSettings_IsCollectionVisible();
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport("Dunia.dll")]
        private static extern bool FCE_EditorSettings_IsFogVisible();
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport("Dunia.dll")]
        private static extern bool FCE_EditorSettings_IsGridVisible();
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport("Dunia.dll")]
        private static extern bool FCE_EditorSettings_IsIconsVisible();
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport("Dunia.dll")]
        private static extern bool FCE_EditorSettings_IsInvincible();
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport("Dunia.dll")]
        private static extern bool FCE_EditorSettings_IsKillDistanceOverride();
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport("Dunia.dll")]
        private static extern bool FCE_EditorSettings_IsShadowVisible();
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport("Dunia.dll")]
        private static extern bool FCE_EditorSettings_IsSnappingObjectsToTerrain();
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport("Dunia.dll")]
        private static extern bool FCE_EditorSettings_IsSoundEnabled();
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport("Dunia.dll")]
        private static extern bool FCE_EditorSettings_IsWaterVisible();
        [DllImport("Dunia.dll")]
        private static extern void FCE_EditorSettings_SetAutoSnappingObjects([MarshalAs(UnmanagedType.U1)] bool snap);
        [DllImport("Dunia.dll")]
        private static extern void FCE_EditorSettings_SetAutoSnappingObjectsRotation([MarshalAs(UnmanagedType.U1)] bool snap);
        [DllImport("Dunia.dll")]
        private static extern void FCE_EditorSettings_SetAutoSnappingObjectsTerrain([MarshalAs(UnmanagedType.U1)] bool snap);
        [DllImport("Dunia.dll")]
        private static extern void FCE_EditorSettings_SetCameraClipTerrain([MarshalAs(UnmanagedType.U1)] bool clip);
        [DllImport("Dunia.dll")]
        private static extern void FCE_EditorSettings_SetEngineQuality([MarshalAs(UnmanagedType.U4)] QualityLevel engineQuality);
        [DllImport("Dunia.dll")]
        private static extern void FCE_EditorSettings_SetGridResolution(int resolution);
        [DllImport("Dunia.dll")]
        private static extern void FCE_EditorSettings_SetInvincible([MarshalAs(UnmanagedType.U1)] bool invincible);
        [DllImport("Dunia.dll")]
        private static extern void FCE_EditorSettings_SetKillDistanceOverride([MarshalAs(UnmanagedType.U1)] bool value);
        [DllImport("Dunia.dll")]
        private static extern void FCE_EditorSettings_SetSnapObjectsToTerrain([MarshalAs(UnmanagedType.U1)] bool snap);
        [DllImport("Dunia.dll")]
        private static extern void FCE_EditorSettings_SetSoundEnabled([MarshalAs(UnmanagedType.U1)] bool enabled);
        [DllImport("Dunia.dll")]
        private static extern void FCE_EditorSettings_ShowCollections([MarshalAs(UnmanagedType.U1)] bool show);
        [DllImport("Dunia.dll")]
        private static extern void FCE_EditorSettings_ShowFog([MarshalAs(UnmanagedType.U1)] bool show);
        [DllImport("Dunia.dll")]
        private static extern void FCE_EditorSettings_ShowGrid([MarshalAs(UnmanagedType.U1)] bool show);
        [DllImport("Dunia.dll")]
        private static extern void FCE_EditorSettings_ShowIcons([MarshalAs(UnmanagedType.U1)] bool show);
        [DllImport("Dunia.dll")]
        private static extern void FCE_EditorSettings_ShowShadow([MarshalAs(UnmanagedType.U1)] bool show);
        [DllImport("Dunia.dll")]
        private static extern void FCE_EditorSettings_ShowWater([MarshalAs(UnmanagedType.U1)] bool show);

        // Properties
        public static bool AutoSnappingObjects
        {
            get
            {
                return FCE_EditorSettings_IsAutoSnappingObjects();
            }
            set
            {
                FCE_EditorSettings_SetAutoSnappingObjects(value);
            }
        }

        public static bool AutoSnappingObjectsRotation
        {
            get
            {
                return FCE_EditorSettings_IsAutoSnappingObjectsRotation();
            }
            set
            {
                FCE_EditorSettings_SetAutoSnappingObjectsRotation(value);
            }
        }

        public static bool AutoSnappingObjectsTerrain
        {
            get
            {
                return FCE_EditorSettings_IsAutoSnappingObjectsTerrain();
            }
            set
            {
                FCE_EditorSettings_SetAutoSnappingObjectsTerrain(value);
            }
        }

        public static bool CameraClipTerrain
        {
            get
            {
                return FCE_EditorSettings_IsCameraClippedTerrain();
            }
            set
            {
                FCE_EditorSettings_SetCameraClipTerrain(value);
            }
        }

        public static QualityLevel EngineQuality
        {
            get
            {
                return FCE_EditorSettings_GetEngineQuality();
            }
            set
            {
                FCE_EditorSettings_SetEngineQuality(value);
            }
        }

        public static int GridResolution
        {
            get
            {
                return FCE_EditorSettings_GetGridResolution();
            }
            set
            {
                FCE_EditorSettings_SetGridResolution(value);
            }
        }

        public static bool InvertMousePan
        {
            get
            {
                return m_invertMousePan;
            }
            set
            {
                m_invertMousePan = value;
            }
        }

        public static bool InvertMouseView
        {
            get
            {
                return m_invertMouseView;
            }
            set
            {
                m_invertMouseView = value;
            }
        }

        public static bool Invincible
        {
            get
            {
                return FCE_EditorSettings_IsInvincible();
            }
            set
            {
                FCE_EditorSettings_SetInvincible(value);
            }
        }

        public static bool KillDistanceOverride
        {
            get
            {
                return FCE_EditorSettings_IsKillDistanceOverride();
            }
            set
            {
                FCE_EditorSettings_SetKillDistanceOverride(value);
            }
        }

        public static bool ShowCollections
        {
            get
            {
                return FCE_EditorSettings_IsCollectionVisible();
            }
            set
            {
                FCE_EditorSettings_ShowCollections(value);
            }
        }

        public static bool ShowFog
        {
            get
            {
                return FCE_EditorSettings_IsFogVisible();
            }
            set
            {
                FCE_EditorSettings_ShowFog(value);
            }
        }

        public static bool ShowGrid
        {
            get
            {
                return FCE_EditorSettings_IsGridVisible();
            }
            set
            {
                FCE_EditorSettings_ShowGrid(value);
            }
        }

        public static bool ShowIcons
        {
            get
            {
                return FCE_EditorSettings_IsIconsVisible();
            }
            set
            {
                FCE_EditorSettings_ShowIcons(value);
            }
        }

        public static bool ShowShadow
        {
            get
            {
                return FCE_EditorSettings_IsShadowVisible();
            }
            set
            {
                FCE_EditorSettings_ShowShadow(value);
            }
        }

        public static bool ShowWater
        {
            get
            {
                return FCE_EditorSettings_IsWaterVisible();
            }
            set
            {
                FCE_EditorSettings_ShowWater(value);
            }
        }

        public static bool SnapObjectsToTerrain
        {
            get
            {
                return FCE_EditorSettings_IsSnappingObjectsToTerrain();
            }
            set
            {
                FCE_EditorSettings_SetSnapObjectsToTerrain(value);
            }
        }

        public static bool SoundEnabled
        {
            get
            {
                return FCE_EditorSettings_IsSoundEnabled();
            }
            set
            {
                FCE_EditorSettings_SetSoundEnabled(value);
            }
        }

        public static float ViewportQuality
        {
            get
            {
                return m_viewportQuality;
            }
            set
            {
                m_viewportQuality = value;
            }
        }

        // Nested Types
        public enum QualityLevel
        {
            Low,
            Medium,
            High,
            VeryHigh,
            UltraHigh,
            Optimal,
            Custom
        }
    }


}
