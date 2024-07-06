using Linearstar.Windows.RawInput;

namespace DualKeyboarder.Window
{
    internal class RawInputReceiverWindow : NativeWindow
    {
        public event EventHandler<RawInputEventArgs>? Input;

        public RawInputReceiverWindow()
        {
            CreateHandle(new CreateParams
            {
                ExStyle = 0x00000008,  //TOP_MOST
                X = 0,
                Y = 0,
                Width = 0,
                Height = 0,
                Style = 0x800000,
            });
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_INPUT = 0x00FF;

            if (m.Msg == WM_INPUT)
            {
                var data = RawInputData.FromHandle(m.LParam);
                Input?.Invoke(this, new RawInputEventArgs(data));
            }
            else
            {
                base.WndProc(ref m);
            }
        }
    }
}