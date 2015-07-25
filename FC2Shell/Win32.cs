namespace FC2Shell
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class Win32
    {
        public const byte AC_SRC_ALPHA = 1;
        public const byte AC_SRC_OVER = 0;
        public const int CBN_CLOSEUP = 8;
        public const int CBN_DROPDOWN = 7;
        public const int CS_DROPSHADOW = 0x20000;
        public const int DLGC_WANTALLKEYS = 4;
        public const int DT_BOTTOM = 8;
        public const int DT_CALCRECT = 0x400;
        public const int DT_CENTER = 1;
        public const int DT_EXPANDTABS = 0x40;
        public const int DT_EXTERNALLEADING = 0x200;
        public const int DT_INTERNAL = 0x1000;
        public const int DT_LEFT = 0;
        public const int DT_NOCLIP = 0x100;
        public const int DT_NOPREFIX = 0x800;
        public const int DT_RIGHT = 2;
        public const int DT_SINGLELINE = 0x20;
        public const int DT_TABSTOP = 0x80;
        public const int DT_TOP = 0;
        public const int DT_VCENTER = 4;
        public const int DT_WORDBREAK = 0x10;
        public const int GWL_EXSTYLE = -20;
        public const int GWL_STYLE = -16;
        public const int LVM_FIRST = 0x1000;
        public const int LVM_GETITEMSPACING = 0x1033;
        public const int LVM_SETEXTENDEDLISTVIEWSTYLE = 0x1036;
        public const int LVM_SETICONSPACING = 0x1035;
        public const int LVS_EX_BORDERSELECT = 0x8000;
        public const int PS_DASH = 1;
        public const int PS_DASHDOT = 3;
        public const int PS_DASHDOTDOT = 4;
        public const int PS_DOT = 2;
        public const int PS_INSIDEFRAME = 6;
        public const int PS_NULL = 5;
        public const int PS_SOLID = 0;
        public const int RDW_FRAME = 0x400;
        public const int RDW_INVALIDATE = 1;
        public const int SB_BOTTOM = 7;
        public const int SB_CTL = 2;
        public const int SB_ENDSCROLL = 8;
        public const int SB_HORZ = 0;
        public const int SB_LINEDOWN = 1;
        public const int SB_LINEUP = 0;
        public const int SB_PAGEDOWN = 3;
        public const int SB_PAGEUP = 2;
        public const int SB_THUMBPOSITION = 4;
        public const int SB_THUMBTRACK = 5;
        public const int SB_TOP = 6;
        public const int SB_VERT = 1;
        public const int SIF_ALL = 0x1f;
        public const int SIF_DISABLENOSCROLL = 8;
        public const int SIF_PAGE = 2;
        public const int SIF_POS = 4;
        public const int SIF_RANGE = 1;
        public const int SIF_TRACKPOS = 0x10;
        public const int SW_INVALIDATE = 2;
        public const int SW_SHOWNA = 8;
        public const int SWP_FRAMECHANGED = 0x20;
        public const int SWP_NOMOVE = 2;
        public const int SWP_NOSIZE = 1;
        public const int SWP_NOZORDER = 4;
        public const int TBM_GETCHANNELRECT = 0x41a;
        public const int TBM_SETSEL = 0x40a;
        public const int TBM_SETSELEND = 0x40c;
        public const int TBM_SETSELSTART = 0x40b;
        public const int TBS_ENABLESELRANGE = 0x20;
        public const int ULW_ALPHA = 2;
        public const int ULW_COLORKEY = 1;
        public const int ULW_OPAQUE = 4;
        public const int VK_CTRL = 0x11;
        public const int VK_DOWN = 40;
        public const int VK_LEFT = 0x25;
        public const int VK_LSHIFT = 160;
        public const int VK_RIGHT = 0x27;
        public const int VK_RSHIFT = 0xa1;
        public const int VK_UP = 0x26;
        public const int WF_REFLECT = 0x2000;
        public const int WM_CHAR = 0x102;
        public const int WM_COMMAND = 0x111;
        public const int WM_COPYDATA = 0x4a;
        public const int WM_DEADCHAR = 0x103;
        public const int WM_ERASEBKGND = 20;
        public const int WM_GETDLGCODE = 0x87;
        public const int WM_HSCROLL = 0x114;
        public const int WM_KEYDOWN = 0x100;
        public const int WM_KEYUP = 0x101;
        public const int WM_LBUTTONDBLCLK = 0x203;
        public const int WM_LBUTTONDOWN = 0x201;
        public const int WM_LBUTTONUP = 0x202;
        public const int WM_MBUTTONDBLCLK = 0x209;
        public const int WM_MBUTTONDOWN = 0x207;
        public const int WM_MBUTTONUP = 520;
        public const int WM_MOUSEMOVE = 0x200;
        public const int WM_MOUSEWHEEL = 0x20a;
        public const int WM_NCACTIVATE = 0x86;
        public const int WM_NCLBUTTONDBLCLK = 0xa3;
        public const int WM_NCLBUTTONDOWN = 0xa1;
        public const int WM_NCLBUTTONUP = 0xa2;
        public const int WM_NCMBUTTONDBLCLK = 0xa9;
        public const int WM_NCMBUTTONDOWN = 0xa7;
        public const int WM_NCMBUTTONUP = 0xa8;
        public const int WM_NCMOUSEMOVE = 160;
        public const int WM_NCRBUTTONDBLCLK = 0xa6;
        public const int WM_NCRBUTTONDOWN = 0xa4;
        public const int WM_NCRBUTTONUP = 0xa5;
        public const int WM_NCXBUTTONDBLCLK = 0xad;
        public const int WM_NCXBUTTONDOWN = 0xab;
        public const int WM_NCXBUTTONUP = 0xac;
        public const int WM_RBUTTONDBLCLK = 0x206;
        public const int WM_RBUTTONDOWN = 0x204;
        public const int WM_RBUTTONUP = 0x205;
        public const int WM_SETREDRAW = 11;
        public const int WM_SYSCHAR = 0x106;
        public const int WM_SYSDEADCHAR = 0x107;
        public const int WM_SYSKEYDOWN = 260;
        public const int WM_SYSKEYUP = 0x105;
        public const int WM_USER = 0x400;
        public const int WM_VSCROLL = 0x115;
        public const int WM_XBUTTONDBLCLK = 0x20d;
        public const int WM_XBUTTONDOWN = 0x20b;
        public const int WM_XBUTTONUP = 0x20c;
        public const int WS_EX_CLIENTEDGE = 0x200;
        public const int WS_EX_LAYERED = 0x80000;
        public const int WS_EX_NOACTIVATE = 0x8000000;
        public const int WS_EX_TOPMOST = 8;
        public const int WS_HSCROLL = 0x100000;
        public const int WS_POPUP = -2147483648;
        public const int WS_VSCROLL = 0x200000;

        [DllImport("user32.dll")]
        public static extern bool CreateCaret(IntPtr hWnd, IntPtr hBitmap, int nWidth, int nHeight);
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreatePen(int fnPenStyle, int nWidth, uint crColor);
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateSolidBrush(int crColor);
        [DllImport("gdi32.dll")]
        public static extern bool DeleteDC(IntPtr hdc);
        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
        [DllImport("user32.dll")]
        public static extern bool DestroyCaret();
        [DllImport("user32.dll")]
        public static extern bool DispatchMessage(ref Message msg);
        [DllImport("user32.dll", EntryPoint="DrawTextExW", CharSet=CharSet.Unicode)]
        public static extern int DrawTextEx(IntPtr hdc, string lpchText, int cchText, ref Rect lprc, uint dwDTFormat, [In, Out] DrawTextParams lpDTParams);
        [DllImport("user32.dll")]
        public static extern int EnumWindows(EnumWindowsProc ewp, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern int FillRect(IntPtr hDC, ref Rect lprc, IntPtr hbr);
        [DllImport("user32.dll")]
        public static extern IntPtr GetActiveWindow();
        [DllImport("user32.dll")]
        public static extern IntPtr GetCapture();
        [DllImport("user32.dll")]
        public static extern void GetCaretPos(out Point pt);
        [DllImport("gdi32.dll")]
        public static extern bool GetCharABCWidths(IntPtr hdc, uint uFirstChar, uint uLastChar, [Out] ABC[] lpabc);
        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern IntPtr GetFocus();
        [DllImport("user32.dll")]
        public static extern IntPtr GetKeyboardLayout(int idThread);
        [DllImport("user32.dll")]
        public static extern ushort GetKeyState(int nVirtKey);
        [DllImport("user32.dll")]
        public static extern bool GetMessage(out Message msg, IntPtr hWnd, int wMsgFilterMin, int wMsgFilterMax);
        [DllImport("user32.dll")]
        public static extern IntPtr GetParent(IntPtr hWnd);
        public static void GetPrivateProfileStringW(string lpAppName, string lpKeyName, string lpDefault, out string lpReturnedString, string lpFileName)
        {
            IntPtr ptr = Marshal.AllocHGlobal(0x202);
            GetPrivateProfileStringW(lpAppName, lpKeyName, lpDefault, ptr, 0x100, lpFileName);
            lpReturnedString = Marshal.PtrToStringUni(ptr);
            Marshal.FreeHGlobal(ptr);
        }

        [DllImport("kernel32.dll")]
        public static extern void GetPrivateProfileStringW([MarshalAs(UnmanagedType.LPWStr)] string lpAppName, [MarshalAs(UnmanagedType.LPWStr)] string lpKeyName, [MarshalAs(UnmanagedType.LPWStr)] string lpDefault, IntPtr lpReturnedString, int nSize, [MarshalAs(UnmanagedType.LPWStr)] string lpFileName);
        [DllImport("user32.dll")]
        public static extern IntPtr GetProp(IntPtr hWnd, string lpString);
        [DllImport("user32.dll")]
        public static extern int GetScrollInfo(IntPtr hWnd, int nBar, [In] ScrollInfo scrollInfo);
        [DllImport("gdi32.dll")]
        public static extern bool GetTextExtentExPoint(IntPtr hdc, string lpszStr, int cchString, int nMaxExtent, out int lpnFit, IntPtr alpDx, out Size lpSize);
        [DllImport("gdi32.dll")]
        public static extern bool GetTextMetrics(IntPtr hdc, out TextMetric lptm);
        public static string GetUserName()
        {
            string str = null;
            IntPtr lpBuffer = Marshal.AllocHGlobal(0x200);
            uint nSize = 0x100;
            if (GetUserNameW(lpBuffer, ref nSize) != 0)
            {
                str = Marshal.PtrToStringUni(lpBuffer);
            }
            Marshal.FreeHGlobal(lpBuffer);
            return str;
        }

        public static string GetUserNameEx(EXTENDED_NAME_FORMAT NameFormat)
        {
            string str = null;
            IntPtr lpNameBuffer = Marshal.AllocHGlobal(0x200);
            uint nSize = 0x100;
            if (GetUserNameExW(NameFormat, lpNameBuffer, ref nSize) != 0)
            {
                str = Marshal.PtrToStringUni(lpNameBuffer);
            }
            Marshal.FreeHGlobal(lpNameBuffer);
            return str;
        }

        [DllImport("secur32.dll")]
        public static extern int GetUserNameExW(EXTENDED_NAME_FORMAT NameFormat, IntPtr lpNameBuffer, ref uint nSize);
        public static string GetUserNameHelper()
        {
            string userNameEx = GetUserNameEx(EXTENDED_NAME_FORMAT.NameDisplay);
            if (userNameEx == null)
            {
                userNameEx = GetUserName();
            }
            return userNameEx;
        }

        [DllImport("advapi32.dll")]
        public static extern int GetUserNameW(IntPtr lpBuffer, ref uint nSize);
        [DllImport("user32.dll")]
        public static extern uint GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        public static extern bool HideCaret(IntPtr hWnd);
        public static int HiWord(int dw)
        {
            return (dw >> 0x10);
        }

        [DllImport("user32.dll")]
        public static extern bool IsChild(IntPtr hWndParent, IntPtr hWnd);
        public static bool IsKeyDown(int nVirtKey)
        {
            return ((GetKeyState(nVirtKey) & 0x8000) != 0);
        }

        [DllImport("user32.dll")]
        public static extern bool IsWindowEnabled(IntPtr hWnd);
        public static int LoWord(int dw)
        {
            return (dw & 0xffff);
        }

        public static int MakeLong(int lw, int hw)
        {
            return ((lw & 0xffff) | ((hw & 0xffff) << 0x10));
        }

        [DllImport("user32.dll")]
        public static extern int MapVirtualKeyEx(int uCode, int uMapType, IntPtr dwhkl);
        [DllImport("user32.dll")]
        public static extern int MapWindowPoints(IntPtr hWndFrom, IntPtr hWndTo, ref Point pt, int cPoints);
        [DllImport("user32.dll")]
        public static extern bool PostMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, int flags);
        [DllImport("user32.dll")]
        public static extern void ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
        [DllImport("user32.dll")]
        public static extern IntPtr RemoveProp(IntPtr hWnd, string lpString);
        [DllImport("kernel32.dll")]
        public static extern void RtlMoveMemory(IntPtr dest, IntPtr src, int size);
        [DllImport("user32.dll")]
        public static extern int ScrollWindowEx(IntPtr hWnd, int dx, int dy, ref Rect prcScroll, ref Rect prcClip, IntPtr hrgnUpdate, IntPtr prcUpdate, int flags);
        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, ref COPYDATASTRUCT lParam);
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, ref Rect lParam);
        [DllImport("gdi32.dll")]
        public static extern int SetBkColor(IntPtr hdc, int crColor);
        [DllImport("user32.dll")]
        public static extern void SetCapture(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern bool SetCaretPos(int x, int y);
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern bool SetProp(IntPtr hWnd, string lpString, IntPtr hData);
        public static void SetRedraw(Control control, bool redraw)
        {
            SendMessage(control.Handle, 11, redraw ? 1 : 0, 0);
        }

        [DllImport("user32.dll")]
        public static extern int SetScrollInfo(IntPtr hWnd, int nBar, [In] ScrollInfo scrollInfo, bool bRedraw);
        [DllImport("user32.dll")]
        public static extern int SetScrollPos(IntPtr hWnd, int nBar, int nPos, bool bRedraw);
        [DllImport("gdi32.dll")]
        public static extern int SetTextColor(IntPtr hdc, int crColor);
        [DllImport("user32.dll")]
        public static extern uint SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);
        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        [DllImport("user32.dll")]
        public static extern bool ShowCaret(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        public static extern bool TranslateMessage(ref Message msg);
        [DllImport("user32.dll")]
        public static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref Point pptDst, ref Size psize, IntPtr hdcSrc, ref Point pptSrc, int crKey, ref BlendFunction pblend, int dwFlags);

        [StructLayout(LayoutKind.Sequential)]
        public struct ABC
        {
            public int A;
            public int B;
            public int C;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct BlendFunction
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct COPYDATASTRUCT
        {
            public IntPtr dwData;
            public int cbData;
            public IntPtr lpData;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class DrawTextParams
        {
            public int cbSize = Marshal.SizeOf(typeof(Win32.DrawTextParams));
            public int iTabLength;
            public int iLeftMargin;
            public int iRightMargin;
            public uint uiLengthDrawn;
        }

        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        public enum EXTENDED_NAME_FORMAT
        {
            NameCanonical = 7,
            NameCanonicalEx = 9,
            NameDisplay = 3,
            NameDnsDomain = 12,
            NameFullyQualifiedDN = 1,
            NameSamCompatible = 2,
            NameServicePrincipal = 10,
            NameUniqueId = 6,
            NameUnknown = 0,
            NameUserPrincipal = 8
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Message
        {
            public IntPtr hWnd;
            public int message;
            public IntPtr wParam;
            public IntPtr lParam;
            public int time;
            public Win32.Point pt;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Point
        {
            public int x;
            public int y;
            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Rect
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
            public Rect(int left, int top, int width, int height)
            {
                this.left = left;
                this.top = top;
                this.right = left + width;
                this.bottom = top + height;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public class ScrollInfo
        {
            public int cbSize = Marshal.SizeOf(typeof(Win32.ScrollInfo));
            public int fMask;
            public int nMin;
            public int nMax;
            public int nPage;
            public int nPos;
            public int nTrackPos;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Size
        {
            public int cx;
            public int cy;
            public Size(int cx, int cy)
            {
                this.cx = cx;
                this.cy = cy;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct TextMetric
        {
            public int tmHeight;
            public int tmAscent;
            public int tmDescent;
            public int tmInternalLeading;
            public int tmExternalLeading;
            public int tmAveCharWidth;
            public int tmMaxCharWidth;
            public int tmWeight;
            public int tmOverhang;
            public int tmDigitizedAspectX;
            public int tmDigitizedAspectY;
            public char tmFirstChar;
            public char tmLastChar;
            public char tmDefaultChar;
            public char tmBreakChar;
            public byte tmItalic;
            public byte tmUnderlined;
            public byte tmStruckOut;
            public byte tmPitchAndFamily;
            public byte tmCharSet;
        }
    }
}

