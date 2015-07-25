using System;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FC2Shell.Dunia
{
    public static class Engine
    {
        // Fields
        private static List<InvokeDelegate> m_delayedCallbacks = new List<InvokeDelegate>();
        private static MessagePumpCallbackDelegate m_delegateMessagePumpCallback;
        private static bool m_initialized = false;

        // Methods
        public static void Close()
        {
            CloseDuniaEngine();
        }

        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport("Dunia.dll")]
        private static extern bool CloseDuniaEngine();
        [DllImport("Dunia.dll")]
        private static extern void FCE_Engine_AutoAcquireInput([MarshalAs(UnmanagedType.U1)] bool autoAcquire);
        [DllImport("Dunia.dll")]
        private static extern IntPtr FCE_Engine_GetPersonalPath();
        [DllImport("Dunia.dll")]
        private static extern float FCE_Engine_GetStormFactor();
        [DllImport("Dunia.dll")]
        private static extern void FCE_Engine_GetTimeOfDay(out int hour, out int minute, out int second);
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport("Dunia.dll")]
        private static extern bool FCE_Engine_IsConsoleOpen();
        [DllImport("Dunia.dll")]
        private static extern void FCE_Engine_SetStormFactor(float stormFactor);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Engine_SetTimeOfDay(int hour, int minute, int second);
        [DllImport("Dunia.dll")]
        private static extern void FCE_Engine_UpdateViewport(int sizeX, int sizeY);
        public static bool Init(Form mainWindow, Control viewport)
        {
            m_delegateMessagePumpCallback = new MessagePumpCallbackDelegate(Engine.MessagePumpCallback);
            string[] commandLineArgs = Environment.GetCommandLineArgs();
            int startIndex = 1;
            if (!InitDuniaEngine(Process.GetCurrentProcess().MainModule.BaseAddress, mainWindow.Handle, viewport.Handle, string.Join(" ", commandLineArgs, startIndex, commandLineArgs.Length - startIndex) + " -editorpc -RenderProfile_Quality optimal -3dplatform d3d9", true, true, m_delegateMessagePumpCallback))
            {
                return false;
            }
            FCE_Engine_AutoAcquireInput(true);
            //Editor.Init();
            if (!Directory.Exists(PersonalPath))
            {
                Directory.CreateDirectory(PersonalPath);
            }
            m_initialized = true;
            return true;
        }

        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport("Dunia.dll")]
        private static extern bool InitDuniaEngine(IntPtr hInstance, IntPtr focusWnd, IntPtr renderWnd, [MarshalAs(UnmanagedType.LPStr)] string cmdLine, [MarshalAs(UnmanagedType.U1)] bool launchGame, [MarshalAs(UnmanagedType.U1)] bool forceGpuSynchronization, MessagePumpCallbackDelegate messagePumpCallback);
        public static void Invoke(InvokeDelegate callback)
        {
            lock (m_delayedCallbacks)
            {
                m_delayedCallbacks.Add(callback);
            }
        }

        private static void MessagePumpCallback(bool deferQuit, bool blockRenderer)
        {
        }

        public static void Run()
        {
            //while (!MainForm.Instance.IsDisposed)
            while (!Process.GetCurrentProcess().HasExited)
            {
                bool isActive = true/*Editor.IsActive*/;
                if (m_delayedCallbacks.Count > 0)
                {
                    isActive = true;
                    lock (m_delayedCallbacks)
                    {
                        foreach (InvokeDelegate delegate2 in m_delayedCallbacks)
                        {
                            delegate2();
                        }
                        m_delayedCallbacks.Clear();
                    }
                }
                if (isActive)
                {
                    TickDuniaEngine();
                }
                else
                {
                    Thread.Sleep(50);
                }                
            }
        }

        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport("Dunia.dll")]
        private static extern bool RunDuniaEngine();
        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport("Dunia.dll")]
        public static extern bool TickDuniaEngine();
        public static void UpdateResolution(Size size)
        {
            if (Initialized)
            {
                FCE_Engine_UpdateViewport(size.Width, size.Height);
            }
        }

        // Properties
        public static bool ConsoleOpened
        {
            get
            {
                return FCE_Engine_IsConsoleOpen();
            }
        }

        public static bool Initialized
        {
            get
            {
                return m_initialized;
            }
        }

        public static string PersonalPath
        {
            get
            {
                return Marshal.PtrToStringAnsi(FCE_Engine_GetPersonalPath());
            }
        }

        public static float StormFactor
        {
            get
            {
                return FCE_Engine_GetStormFactor();
            }
            set
            {
                FCE_Engine_SetStormFactor(value);
            }
        }

        public static TimeSpan TimeOfDay
        {
            get
            {
                int num;
                int num2;
                int num3;
                FCE_Engine_GetTimeOfDay(out num, out num2, out num3);
                return new TimeSpan(num, num2, num3);
            }
            set
            {
                FCE_Engine_SetTimeOfDay(value.Hours, value.Minutes, value.Seconds);
            }
        }

        // Nested Types
        public delegate void InvokeDelegate();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void MessagePumpCallbackDelegate(bool deferQuit, bool blockRenderer);

    }
}
