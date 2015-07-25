using System;
using System.Collections.Generic;
using System.Text;

using FC2Shell.Dunia;
using FC2Shell.Helper;

using System.Drawing;

using System.Windows.Forms;

using System.ComponentModel;

using Microsoft.Win32;

using System.IO;

namespace FC2Shell.UI
{
    public class ViewportControl : UserControl
    {
        // Fields
        private IContainer components;
        private const float kSpeedBoost = 5f;
        private bool m_blockNextKeyRepeats;
        private bool m_cameraEnabled = true;
        private CameraModes m_cameraMode;
        private bool m_captureMouse;
        private Point m_captureMousePos;
        private bool m_captureWheel;
        private Cursor m_defaultCursor = Cursors.Default;
        private bool m_forceRefresh;
        private Cursor m_invisibleCursor;
        private bool m_mouseOver;
        private Vec2 m_normalizedMousePos;

        // Methods
        public ViewportControl()
        {
            this.InitializeComponent();
            this.BackColor = SystemColors.AppWorkspace;
            base.MouseWheel += new MouseEventHandler(this.ViewportControl_MouseWheel);
            //this.m_invisibleCursor = new Cursor(new MemoryStream(Resources.invisible_cursor));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Name = "ViewportControl";
            base.MouseDown += new MouseEventHandler(this.ViewportControl_MouseDown);
            base.MouseMove += new MouseEventHandler(this.Viewport_MouseMove);
            base.Leave += new EventHandler(this.Viewport_Leave);
            base.Resize += new EventHandler(this.ViewportControl_Resize);
            base.MouseEnter += new EventHandler(this.ViewportControl_MouseEnter);
            base.Paint += new PaintEventHandler(this.ViewportControl_Paint);
            base.MouseLeave += new EventHandler(this.ViewportControl_MouseLeave);
            base.MouseUp += new MouseEventHandler(this.ViewportControl_MouseUp);
            base.ResumeLayout(false);
        }

        protected override bool IsInputKey(Keys keyData)
        {
            return true;
        }

        public override bool PreProcessMessage(ref Message msg)
        {
            bool flag = (msg.Msg == 0x100) || (msg.Msg == 260);
            bool flag2 = (msg.LParam.ToInt32() & 0x40000000) != 0;
            if (flag)
            {
                if (!flag2)
                {
                    this.BlockNextKeyRepeats = false;
                }
                else if (this.BlockNextKeyRepeats)
                {
                    return true;
                }
            }
            return base.PreProcessMessage(ref msg);
        }

        protected override bool ProcessKeyMessage(ref Message msg)
        {
            bool flag = (msg.Msg == 0x100) || (msg.Msg == 260);
            bool flag2 = (msg.Msg == 0x101) || (msg.Msg == 0x105);
            bool flag3 = (msg.LParam.ToInt32() & 0x40000000) != 0;
            Keys keyData = ((Keys)msg.WParam.ToInt32()) | Control.ModifierKeys;
            KeyEventArgs keyEventArgs = new KeyEventArgs(keyData);
            if (!Editor.IsIngame)
            {
                this.UpdateCameraState();
            }
            if (!Engine.ConsoleOpened)
            {
                if (flag)
                {
                    if (!flag3)
                    {
                        Editor.OnKeyEvent(Editor.KeyEvent.KeyDown, keyEventArgs);
                    }
                    Editor.OnKeyEvent(Editor.KeyEvent.KeyChar, keyEventArgs);
                }
                else if (flag2)
                {
                    Editor.OnKeyEvent(Editor.KeyEvent.KeyUp, keyEventArgs);
                }
            }
            return base.ProcessKeyMessage(ref msg);
        }

        private void ResetCameraState()
        {
            Camera.ForwardInput = 0f;
            Camera.LateralInput = 0f;
            Camera.SpeedFactor = 1f;
        }

        private void UpdateCameraMode()
        {
            if (this.CameraMode != CameraModes.None)
            {
                this.CaptureMouse = true;
                this.UpdateCameraState();
            }
            else
            {
                this.CaptureMouse = false;
                Camera.ForwardInput = 0f;
                Camera.LateralInput = 0f;
            }
        }

        private void UpdateCameraState()
        {
            if (Engine.Initialized)
            {
                if (Engine.ConsoleOpened || !this.Focused)
                {
                    this.ResetCameraState();
                }
                else
                {
                    IntPtr keyboardLayout = Win32.GetKeyboardLayout(0);
                    int nVirtKey = Win32.MapVirtualKeyEx(0x11, 1, keyboardLayout);
                    int num2 = Win32.MapVirtualKeyEx(0x1f, 1, keyboardLayout);
                    int num3 = Win32.MapVirtualKeyEx(30, 1, keyboardLayout);
                    int num4 = Win32.MapVirtualKeyEx(0x20, 1, keyboardLayout);
                    if (Win32.IsKeyDown(nVirtKey))
                    {
                        Camera.ForwardInput = 1f;
                    }
                    else if (Win32.IsKeyDown(num2))
                    {
                        Camera.ForwardInput = -1f;
                    }
                    else
                    {
                        Camera.ForwardInput = 0f;
                    }
                    if (Win32.IsKeyDown(num3))
                    {
                        Camera.LateralInput = -1f;
                    }
                    else if (Win32.IsKeyDown(num4))
                    {
                        Camera.LateralInput = 1f;
                    }
                    else
                    {
                        Camera.LateralInput = 0f;
                    }
                    if (Win32.IsKeyDown(160) || Win32.IsKeyDown(0xa1))
                    {
                        Camera.SpeedFactor = 5f;
                    }
                    else
                    {
                        Camera.SpeedFactor = 1f;
                    }
                }
            }
        }

        private void UpdateCaptureMouse()
        {
            if (this.CaptureMouse)
            {
                this.Cursor = this.m_invisibleCursor;
                this.m_captureMousePos = Cursor.Position;
                Cursor.Position = base.PointToScreen(new Point(base.Width / 2, base.Height / 2));
            }
            else
            {
                Cursor.Position = this.m_captureMousePos;
                this.Cursor = this.m_defaultCursor;
            }
        }

        public void UpdateFocus()
        {
            //if (MainForm.IsActive)
            //{
                if (this.CaptureMouse)
                {
                    Cursor.Position = base.PointToScreen(new Point(base.Width / 2, base.Height / 2));
                    this.Cursor = this.m_invisibleCursor;
                }
            //}
            if (this.CaptureMouse)
            {
                this.Cursor = this.m_defaultCursor;
            }
        }

        public void UpdateSize()
        {
            if ((base.ParentForm != null) && (base.ParentForm.WindowState != FormWindowState.Minimized))
            {
                Size clientSize = base.ClientSize;
                clientSize.Width = (int)(clientSize.Width * EditorSettings.ViewportQuality);
                clientSize.Height = (int)(clientSize.Height * EditorSettings.ViewportQuality);
                if (clientSize.Width < 0x10)
                {
                    clientSize.Width = 0x10;
                }
                if (clientSize.Height < 0x10)
                {
                    clientSize.Height = 0x10;
                }
                Engine.UpdateResolution(clientSize);
            }
        }

        private void Viewport_Leave(object sender, EventArgs e)
        {
            if (this.CameraMode != CameraModes.None)
            {
                this.CameraMode = CameraModes.None;
            }
            this.ResetCameraState();
        }

        private void Viewport_MouseMove(object sender, MouseEventArgs e)
        {
            if (!this.CaptureMouse)
            {
                this.m_normalizedMousePos = new Vec2(((float)e.X) / ((float)base.ClientSize.Width), ((float)e.Y) / ((float)base.ClientSize.Height));
                Editor.OnMouseEvent(Editor.MouseEvent.MouseMove, e);
            }
            else if (true)
            {
                Point point = base.PointToScreen(new Point(base.Width / 2, base.Height / 2));
                int x = Cursor.Position.X - point.X;
                int y = Cursor.Position.Y - point.Y;
                if ((x != 0) || (y != 0))
                {
                    switch (this.CameraMode)
                    {
                        case CameraModes.Lookaround:
                            Camera.Rotate((EditorSettings.InvertMouseView ? ((float)y) : ((float)-y)) * 0.005f, 0f, -x * 0.005f);
                            break;

                        case CameraModes.Panning:
                            Camera.Position += (Vec3)(((Camera.RightVector * x) * 0.125f) + ((Camera.UpVector * (EditorSettings.InvertMousePan ? ((float)y) : ((float)-y))) * 0.125f));
                            break;

                        default:
                            Editor.OnMouseEvent(Editor.MouseEvent.MouseMoveDelta, new MouseEventArgs(e.Button, e.Clicks, x, y, e.Delta));
                            break;
                    }
                    Cursor.Position = point;
                }
            }
        }

        private void ViewportControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.CameraMode == CameraModes.None)
            {
                MouseButtons button = e.Button;
                if (button != MouseButtons.Left)
                {
                    if (button != MouseButtons.Right)
                    {
                        if ((button == MouseButtons.Middle) && (!Editor.IsIngame && this.CameraEnabled))
                        {
                            this.CameraMode = CameraModes.Panning;
                        }
                    }
                    else if (!Editor.IsIngame && this.CameraEnabled)
                    {
                        this.CameraMode = CameraModes.Lookaround;
                    }
                }
                else
                {
                    Editor.OnMouseEvent(Editor.MouseEvent.MouseDown, e);
                }
            }
        }

        private void ViewportControl_MouseEnter(object sender, EventArgs e)
        {
            if (true)
            {
                base.Focus();
            }
            this.m_mouseOver = true;
            Editor.OnMouseEvent(Editor.MouseEvent.MouseEnter, null);
        }

        private void ViewportControl_MouseLeave(object sender, EventArgs e)
        {
            if (this.CameraMode != CameraModes.None)
            {
                this.CameraMode = CameraModes.None;
            }
            this.m_mouseOver = false;
            Editor.OnMouseEvent(Editor.MouseEvent.MouseLeave, null);
        }

        private void ViewportControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.CameraMode == CameraModes.None)
            {
                if (e.Button == MouseButtons.Left)
                {
                    Editor.OnMouseEvent(Editor.MouseEvent.MouseUp, e);
                }
            }
            else if ((e.Button == MouseButtons.Middle) || (e.Button == MouseButtons.Right))
            {
                this.CameraMode = CameraModes.None;
            }
        }

        private void ViewportControl_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!this.m_captureWheel)
            {
                if (!Editor.IsIngame)
                {
                    Camera.Position += (Vec3)((Camera.FrontVector * e.Delta) * 0.0625f);
                }
            }
            else
            {
                Editor.OnMouseEvent(Editor.MouseEvent.MouseWheel, e);
            }
        }

        private void ViewportControl_Paint(object sender, PaintEventArgs e)
        {
        }

        private void ViewportControl_Resize(object sender, EventArgs e)
        {
            this.UpdateSize();
        }

        protected override void WndProc(ref Message m)
        {
            if ((!this.ForceRefresh && Editor.IsActive) && (m.Msg == 20))
            {
                m.Result = new IntPtr(1);
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        // Properties
        public bool BlockNextKeyRepeats
        {
            get
            {
                return this.m_blockNextKeyRepeats;
            }
            set
            {
                this.m_blockNextKeyRepeats = value;
            }
        }

        public bool CameraEnabled
        {
            get
            {
                return this.m_cameraEnabled;
            }
            set
            {
                this.m_cameraEnabled = value;
            }
        }

        private CameraModes CameraMode
        {
            get
            {
                return this.m_cameraMode;
            }
            set
            {
                this.m_cameraMode = value;
                this.UpdateCameraMode();
            }
        }

        public bool CaptureMouse
        {
            get
            {
                return this.m_captureMouse;
            }
            set
            {
                if (this.m_captureMouse != value)
                {
                    this.m_captureMouse = value;
                    this.UpdateCaptureMouse();
                }
            }
        }

        public Vec2 CaptureMousePos
        {
            set
            {
                this.m_captureMousePos = base.PointToScreen(new Point((int)(value.X * base.ClientSize.Width), (int)(value.Y * base.ClientSize.Height)));
            }
        }

        public bool CaptureWheel
        {
            get
            {
                return this.m_captureWheel;
            }
            set
            {
                this.m_captureWheel = value;
            }
        }

        public Cursor DefaultCursor
        {
            get
            {
                return this.m_defaultCursor;
            }
            set
            {
                if (this.Cursor == this.m_defaultCursor)
                {
                    this.Cursor = value;
                }
                this.m_defaultCursor = value;
            }
        }

        public bool ForceRefresh
        {
            get
            {
                return this.m_forceRefresh;
            }
            set
            {
                this.m_forceRefresh = value;
            }
        }

        public bool MouseOver
        {
            get
            {
                return this.m_mouseOver;
            }
        }

        public Vec2 NormalizedMousePos
        {
            get
            {
                return this.m_normalizedMousePos;
            }
            set
            {
                Cursor.Position = base.PointToScreen(new Point((int)(value.X * base.ClientSize.Width), (int)(value.Y * base.ClientSize.Height)));
            }
        }

        // Nested Types
        private enum CameraModes
        {
            None,
            Lookaround,
            Panning
        }
    }


}
