using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;

namespace FC2Shell.Dunia
{
    public interface IInputSink
    {
        // Methods
        void OnEditorEvent(uint eventType, IntPtr eventPtr);
        void OnInputAcquire();
        void OnInputRelease();

        bool OnKeyEvent(Editor.KeyEvent keyEvent, KeyEventArgs keyEventArgs);
        bool OnMouseEvent(Editor.MouseEvent mouseEvent, MouseEventArgs mouseEventArgs);

        void Update(float dt);
    }


}
