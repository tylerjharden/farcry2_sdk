using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.InteropServices;

using System.Windows.Forms;

using Microsoft.Win32;

using FC2Shell.UI;

namespace FC2Shell.Dunia
{
    public static class Editor
{
    // Fields
    private static EnableUICallbackDelegate m_delegateEnableUICallback;
    private static EventCallbackDelegate m_delegateEventCallback;
    private static LoadCompletedCallbackDelegate m_delegateLoadCompletedCallback;
    private static SaveCompletedCallbackDelegate m_delegateSaveCompletedCallback;
    private static UpdateCallbackDelegate m_delegateUpdateCallback;
    private static List<IInputSink> m_inputStack = new List<IInputSink>();

    // Methods
    public static void ApplyScreenDeltaToWorldPos(Vec2 screenDelta, ref Vec3 worldPos)
    {
        Vec3 frontVector = Camera.FrontVector;
        if ((Math.Abs(frontVector.X) < 0.001) && (Math.Abs(frontVector.Y) < 0.001))
        {
            frontVector = Camera.UpVector;
        }
        Vec2 vec2 = -(frontVector.XY);
        vec2.Normalize();
        Vec2 vec3 = new Vec2(-vec2.Y, vec2.X);
        float num = (float) ((Vec3.Dot(worldPos - Camera.Position, Camera.FrontVector) * Math.Tan((double) Camera.HalfFOV)) * 2.0);
        worldPos.X += ((num * screenDelta.X) * vec3.X) + ((num * screenDelta.Y) * vec2.X);
        worldPos.Y += ((num * screenDelta.X) * vec3.Y) + ((num * screenDelta.Y) * vec2.Y);
    }

    private static void EnableUICallback(bool enable)
    {
        //MainForm.Instance.EnableUI(enable);
    }

    private static void EventCallback(uint eventType, IntPtr eventPtr)
    {
        OnEditorEvent(eventType, eventPtr);
    }

    [DllImport("Dunia.dll")]
    private static extern void FCE_Editor_EnableUI_Callback(EnableUICallbackDelegate eventCallback);
    [DllImport("Dunia.dll")]
    private static extern void FCE_Editor_Event_Callback(EventCallbackDelegate eventCallback);
    [DllImport("Dunia.dll")]
    private static extern float FCE_Editor_GetFrameTime();
    [return: MarshalAs(UnmanagedType.U1)]
    [DllImport("Dunia.dll")]
    private static extern bool FCE_Editor_GetScreenPointFromWorldPos(float worldX, float worldY, float worldZ, out float screenX, out float screenY);
    [DllImport("Dunia.dll")]
    private static extern void FCE_Editor_GetWorldRayFromScreenPoint(float screenX, float screenY, out float raySrcX, out float raySrcY, out float raySrcZ, out float rayDirX, out float rayDirY, out float rayDirZ);
    [return: MarshalAs(UnmanagedType.U1)]
    [DllImport("Dunia.dll")]
    private static extern bool FCE_Editor_IsIngame();
    [return: MarshalAs(UnmanagedType.U1)]
    [DllImport("Dunia.dll")]
    private static extern bool FCE_Editor_IsInitialized();
    [return: MarshalAs(UnmanagedType.U1)]
    [DllImport("Dunia.dll")]
    private static extern bool FCE_Editor_IsLoadPending();
    [DllImport("Dunia.dll")]
    private static extern void FCE_Editor_LoadCompleted_Callback(LoadCompletedCallbackDelegate eventCallback);
    [return: MarshalAs(UnmanagedType.U1)]
    [DllImport("Dunia.dll")]
    private static extern bool FCE_Editor_RayCastPhysics(float raySrcX, float raySrcY, float raySrcZ, float rayDirX, float rayDirY, float rayDirZ, IntPtr ignore, out float hitX, out float hitY, out float hitZ, out float hitDist, out float hitNormX, out float hitNormY, out float hitNormZ);
    [return: MarshalAs(UnmanagedType.U1)]
    [DllImport("Dunia.dll")]
    private static extern bool FCE_Editor_RayCastPhysics2(float raySrcX, float raySrcY, float raySrcZ, float rayDirX, float rayDirY, float rayDirZ, IntPtr ignore, out float hitX, out float hitY, out float hitZ, out float hitDist, out float hitNormX, out float hitNormY, out float hitNormZ);
    [return: MarshalAs(UnmanagedType.U1)]
    [DllImport("Dunia.dll")]
    private static extern bool FCE_Editor_RayCastTerrain(float raySrcX, float raySrcY, float raySrcZ, float rayDirX, float rayDirY, float rayDirZ, out float hitX, out float hitY, out float hitZ, out float hitDist);
    [DllImport("Dunia.dll")]
    private static extern void FCE_Editor_SaveCompleted_Callback(SaveCompletedCallbackDelegate eventCallback);
    [DllImport("Dunia.dll")]
    private static extern void FCE_Editor_ToggleIngame();
    [DllImport("Dunia.dll")]
    private static extern void FCE_Editor_Update_Callback(UpdateCallbackDelegate updateCallback);
    [return: MarshalAs(UnmanagedType.U1)]
    [DllImport("Dunia.dll")]
    private static extern bool FCE_Editor_ValidateIngame();
    
    public static int GetRegistryInt(string name, int defaultValue)
    {
        using (RegistryKey key = GetRegistrySettings())
        {
            return GetRegistryInt(key, name, defaultValue);
        }
    }

    public static int GetRegistryInt(RegistryKey key, string name, int defaultValue)
    {
        object obj2 = key.GetValue(name);
        if (obj2 is int)
        {
            return (int) obj2;
        }
        return defaultValue;
    }

    public static RegistryKey GetRegistrySettings()
    {
        return Registry.CurrentUser.CreateSubKey(@"Software\Ubisoft\Far Cry 2\Editor");
    }

    public static string GetRegistryString(RegistryKey key, string name, string defaultValue)
    {
        object obj2 = key.GetValue(name);
        if (obj2 is string)
        {
            return (string) obj2;
        }
        return defaultValue;
    }

    public static bool GetScreenPointFromWorldPos(Vec3 worldPos, out Vec2 screenPoint)
    {
        return GetScreenPointFromWorldPos(worldPos, out screenPoint, false);
    }

    public static bool GetScreenPointFromWorldPos(Vec3 worldPos, out Vec2 screenPoint, bool clipped)
    {
        bool flag = FCE_Editor_GetScreenPointFromWorldPos(worldPos.X, worldPos.Y, worldPos.Z, out screenPoint.X, out screenPoint.Y);
        if (flag && clipped)
        {
            screenPoint.X = Math.Min(Math.Max(0f, screenPoint.X), 1f);
            screenPoint.Y = Math.Min(Math.Max(0f, screenPoint.Y), 1f);
        }
        return flag;
    }

    public static void GetWorldRayFromScreenPoint(Vec2 screenPoint, out Vec3 raySrc, out Vec3 rayDir)
    {
        FCE_Editor_GetWorldRayFromScreenPoint(screenPoint.X, screenPoint.Y, out raySrc.X, out raySrc.Y, out raySrc.Z, out rayDir.X, out rayDir.Y, out rayDir.Z);
    }

    public static void Init()
    {
        m_delegateUpdateCallback = new UpdateCallbackDelegate(Editor.UpdateCallback);
        FCE_Editor_Update_Callback(m_delegateUpdateCallback);
        m_delegateEventCallback = new EventCallbackDelegate(Editor.EventCallback);
        FCE_Editor_Event_Callback(m_delegateEventCallback);
        m_delegateLoadCompletedCallback = new LoadCompletedCallbackDelegate(Editor.LoadCompletedCallback);
        FCE_Editor_LoadCompleted_Callback(m_delegateLoadCompletedCallback);
        m_delegateSaveCompletedCallback = new SaveCompletedCallbackDelegate(Editor.SaveCompletedCallback);
        FCE_Editor_SaveCompleted_Callback(m_delegateSaveCompletedCallback);
        m_delegateEnableUICallback = new EnableUICallbackDelegate(Editor.EnableUICallback);
        FCE_Editor_EnableUI_Callback(m_delegateEnableUICallback);
        while (!FCE_Editor_IsInitialized())
        {
            Engine.TickDuniaEngine();
        }
    }

    private static void LoadCompletedCallback(ResultCode resultCode)
    {
        EditorDocument.OnLoadCompleted(resultCode);
    }

    public static void OnEditorEvent(uint eventType, IntPtr eventPtr)
    {
        foreach (IInputSink sink in m_inputStack)
        {
            sink.OnEditorEvent(eventType, eventPtr);
        }
    }

    public static void OnKeyEvent(KeyEvent keyEvent, KeyEventArgs keyEventArgs)
    {
        if (IsIngame)
        {
            if ((keyEvent == KeyEvent.KeyUp) && (keyEventArgs.KeyCode == Keys.Escape))
            {
                ToggleIngame();
            }
        }
        else
        {
            foreach (IInputSink sink in m_inputStack)
            {
                if (sink.OnKeyEvent(keyEvent, keyEventArgs))
                {
                    break;
                }
            }
        }
    }

    public static void OnMouseEvent(MouseEvent mouseEvent, MouseEventArgs mouseEventArgs)
    {
        foreach (IInputSink sink in m_inputStack)
        {
            if (sink.OnMouseEvent(mouseEvent, mouseEventArgs))
            {
                break;
            }
        }
    }

    public static void OnUpdate(float dt)
    {
        foreach (IInputSink sink in m_inputStack)
        {
            sink.Update(dt);
        }
    }

    public static void PopInput(IInputSink input)
    {
        int index = m_inputStack.LastIndexOf(input);
        if (index != -1)
        {
            m_inputStack[m_inputStack.Count - 1].OnInputRelease();
            m_inputStack.RemoveRange(index, m_inputStack.Count - index);
            if (m_inputStack.Count > 0)
            {
                m_inputStack[m_inputStack.Count - 1].OnInputAcquire();
            }
        }
    }

    public static void PushInput(IInputSink input)
    {
        System.Diagnostics.Trace.Assert(!m_inputStack.Contains(input));
        if (m_inputStack.Count > 0)
        {
            m_inputStack[m_inputStack.Count - 1].OnInputRelease();
        }
        m_inputStack.Add(input);
        input.OnInputAcquire();
    }

    public static bool RayCastPhysics(Vec3 raySrc, Vec3 rayDir, EditorObject ignore, out Vec3 hitPos, out float hitDist)
    {
        Vec3 vec;
        return RayCastPhysics(raySrc, rayDir, ignore, out hitPos, out hitDist, out vec);
    }

    public static bool RayCastPhysics(Vec3 raySrc, Vec3 rayDir, EditorObjectSelection ignore, out Vec3 hitPos, out float hitDist)
    {
        Vec3 vec;
        return RayCastPhysics(raySrc, rayDir, ignore, out hitPos, out hitDist, out vec);
    }

    public static bool RayCastPhysics(Vec3 raySrc, Vec3 rayDir, EditorObject ignore, out Vec3 hitPos, out float hitDist, out Vec3 hitNormal)
    {
        return FCE_Editor_RayCastPhysics(raySrc.X, raySrc.Y, raySrc.Z, rayDir.X, rayDir.Y, rayDir.Z, ignore.Pointer, out hitPos.X, out hitPos.Y, out hitPos.Z, out hitDist, out hitNormal.X, out hitNormal.Y, out hitNormal.Z);
    }

    public static bool RayCastPhysics(Vec3 raySrc, Vec3 rayDir, EditorObjectSelection ignore, out Vec3 hitPos, out float hitDist, out Vec3 hitNormal)
    {
        return FCE_Editor_RayCastPhysics2(raySrc.X, raySrc.Y, raySrc.Z, rayDir.X, rayDir.Y, rayDir.Z, ignore.Pointer, out hitPos.X, out hitPos.Y, out hitPos.Z, out hitDist, out hitNormal.X, out hitNormal.Y, out hitNormal.Z);
    }

    public static bool RayCastPhysicsFromMouse(out Vec3 hitPos)
    {
        return RayCastPhysicsFromScreenPoint(Viewport.NormalizedMousePos, out hitPos);
    }

    public static bool RayCastPhysicsFromScreenPoint(Vec2 screenPoint, out Vec3 hitPos)
    {
        Vec3 vec;
        Vec3 vec2;
        float num;
        GetWorldRayFromScreenPoint(screenPoint, out vec, out vec2);
        return RayCastPhysics(vec, vec2, EditorObject.Null, out hitPos, out num);
    }

    public static bool RayCastTerrain(Vec3 raySrc, Vec3 rayDir, out Vec3 hitPos, out float hitDist)
    {
        return FCE_Editor_RayCastTerrain(raySrc.X, raySrc.Y, raySrc.Z, rayDir.X, rayDir.Y, rayDir.Z, out hitPos.X, out hitPos.Y, out hitPos.Z, out hitDist);
    }

    public static bool RayCastTerrainFromMouse(out Vec3 hitPos)
    {
        return RayCastTerrainFromScreenPoint(Viewport.NormalizedMousePos, out hitPos);
    }

    public static bool RayCastTerrainFromScreenPoint(Vec2 screenPoint, out Vec3 hitPos)
    {
        Vec3 vec;
        Vec3 vec2;
        float num;
        GetWorldRayFromScreenPoint(screenPoint, out vec, out vec2);
        return RayCastTerrain(vec, vec2, out hitPos, out num);
    }

    private static void SaveCompletedCallback(ResultCode resultCode)
    {
        EditorDocument.OnSaveCompleted(resultCode);
    }

    public static void SetRegistryInt(string name, int value)
    {
        using (RegistryKey key = GetRegistrySettings())
        {
            SetRegistryInt(key, name, value);
        }
    }

    public static void SetRegistryInt(RegistryKey key, string name, int value)
    {
        key.SetValue(name, value);
    }

    public static void ToggleIngame()
    {
        if (!FCE_Editor_ValidateIngame())
        {
            MessageBox.Show(Localizer.LocalizeCommon("MSG_DESC_INGAME_INVALID_OBJECTS"), Localizer.Localize("WARNING"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        FCE_Editor_ToggleIngame();
        //MainForm.Instance.ToggleIngame();
    }

    private static void UpdateCallback(float dt)
    {
        OnUpdate(dt);
    }

    // Properties
    public static float FrameTime
    {
        get
        {
            return FCE_Editor_GetFrameTime();
        }
    }

    public static bool IsActive
    {
        get
        {
            return (Win32.GetActiveWindow() != IntPtr.Zero);
        }
    }

    public static bool IsIngame
    {
        get
        {
            return FCE_Editor_IsIngame();
        }
    }

    public static bool IsLoadPending
    {
        get
        {
            return FCE_Editor_IsLoadPending();
        }
    }

    public static ViewportControl Viewport
    {
        get
        {
            return null;
        }
    }
            
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate void EnableUICallbackDelegate(bool enable);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate void EventCallbackDelegate(uint eventType, IntPtr eventPtr);

    public enum KeyEvent
    {
        KeyDown,
        KeyChar,
        KeyUp
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate void LoadCompletedCallbackDelegate(Editor.ResultCode resultCode);

    public enum MouseEvent
    {
        MouseDown,
        MouseUp,
        MouseMove,
        MouseMoveDelta,
        MouseWheel,
        MouseEnter,
        MouseLeave
    }

    public enum ResultCode
    {
        None,
        Succeeded,
        Busy,
        CanceledByUser,
        Failed,
        MissingDLC,
        FileCorrupt
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate void SaveCompletedCallbackDelegate(Editor.ResultCode resultCode);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate void UpdateCallbackDelegate(float dt);
}


}

