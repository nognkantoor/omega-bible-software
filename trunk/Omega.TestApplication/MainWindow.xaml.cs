using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Interop;

namespace Omega.TestApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GlobalHotkey hotkey;
        public MainWindow()
        {
            InitializeComponent();

            
            //hotkey.UnregisterGlobalHotKey();
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            hotkey.UnregisterGlobalHotKey();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            hotkey = new GlobalHotkey();
            IntPtr handle = new WindowInteropHelper(this).Handle;
            
            //hotkey.RegisterGlobalHotKey(0x45, GlobalHotkeys.MOD_CONTROL, handle);
            hotkey.RegisterGlobalHotKey((int)System.Windows.Forms.Keys.E, KeyModifier.Control, handle);
            //69
            //48
            var k = Key.E;

            HwndSource source = PresentationSource.FromVisual(this) as HwndSource;
            source.AddHook(WndProc);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_HOTKEY = 0x312;

            switch (msg)
            {
                case WM_HOTKEY:
                    {
                        if ((short)wParam == hotkey.HotkeyID)
                        {
                            OutputBlock.Text += "Pressed CTRL + E\n";
                            //string t = TextSelecting.GetTheText();
                            //OutputBlock.Text += (string.IsNullOrEmpty(t) ? "-" : (t + "\n"));
                        }
                        break;
                    }
            }

            return IntPtr.Zero;
        }
    }
}
