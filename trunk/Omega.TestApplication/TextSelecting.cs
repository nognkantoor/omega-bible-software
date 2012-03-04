using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Diagnostics;
using System.Threading;

namespace Omega.TestApplication
{
    class TextSelecting
    {

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("kernel32.dll")]
        static extern uint GetCurrentThreadId();

        [DllImport("user32.dll")]
        static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

        [DllImport("user32.dll")]
        static extern IntPtr GetFocus();

        [DllImport("user32.dll")]
        static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, StringBuilder lParam);

        [DllImport("user32.dll")]
        static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        // second overload of SendMessage
        [DllImport("user32.dll")]
        static extern int SendMessage(IntPtr hWnd, uint Msg, out int wParam, out int lParam);

        const uint WM_GETTEXT = 0x0D;
        const uint WM_GETTEXTLENGTH = 0x0E;
        const uint EM_GETSEL = 0xB0;
        const uint WM_COPY = 0x0301;

        // code needed to get selected text - returns empty string if nothing selected

        public static string GetTheText()
        {
            System.Windows.Forms.SendKeys.SendWait("^c");
            string selectedText = Clipboard.GetText();

            //IntPtr hWnd = GetForegroundWindow();
            //Console.WriteLine(hWnd.ToInt32());
            //uint processId;

            //uint activeThreadId = GetWindowThreadProcessId(hWnd, out processId);
            //uint currentThreadId = GetCurrentThreadId();

            //AttachThreadInput(activeThreadId, currentThreadId, true);

            //IntPtr focusedHandle = GetFocus();
            //Console.WriteLine(focusedHandle.ToInt32());
            //AttachThreadInput(activeThreadId, currentThreadId, false);
            ////int i = SendMessage(focusedHandle, WM_COPY, 0, 0);
            //int len = SendMessage(focusedHandle, WM_GETTEXTLENGTH, 0, null);
            //if (len == 0)
            //{
            //    len = 8;
            //}
            //StringBuilder sb = new StringBuilder(len);
            //int numChars = SendMessage(focusedHandle, WM_GETTEXT, len+1, sb);
            //int start, next;
            //SendMessage(focusedHandle, EM_GETSEL, out start, out next);
            //string selectedText = sb.ToString().Substring(start, next - start);
            //string selectedText = Clipboard.GetText();
            //string selectedText = sb.ToString();

            return selectedText;
        }


        //-------------------------------------------------------------------
        #region fields
        public static int MOD_ALT = 0x1;
        public static int MOD_CONTROL = 0x2;
        public static int MOD_SHIFT = 0x4;
        public static int MOD_WIN = 0x8;
        public static int WM_HOTKEY = 0x312;
        #endregion

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private static int keyId;
        public static void RegisterHotKey(Window w, Key key)
        {
            IntPtr handle = new WindowInteropHelper(w).Handle;
            int modifiers = 0;

            if ((key & Key.LeftAlt) == Key.LeftAlt)
                modifiers = modifiers | TextSelecting.MOD_ALT;

            if ((key & Key.LeftCtrl) == Key.LeftCtrl)
                modifiers = modifiers | TextSelecting.MOD_CONTROL;

            if ((key & Key.LeftShift) == Key.LeftShift)
                modifiers = modifiers | TextSelecting.MOD_SHIFT;

            Key k = key & ~Key.LeftCtrl & ~Key.LeftShift & ~Key.LeftAlt;
            keyId = w.GetHashCode(); // this should be a key unique ID, modify this if you want more than one hotkey
            RegisterHotKey(handle, keyId, (int)modifiers, (int)k);
        }

        private delegate void Func();

        public static void UnregisterHotKey(Window w)
        {
            try
            {
                IntPtr handle = new WindowInteropHelper(w).Handle;
                UnregisterHotKey(handle, keyId); // modify this if you want more than one hotkey
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }

    public class GlobalHotkeys : IDisposable
    {
        [DllImport("user32", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool RegisterHotKey(IntPtr hwnd, int id, uint fsModifiers, uint vk);
        [DllImport("user32", SetLastError = true)]
        public static extern int UnregisterHotKey(IntPtr hwnd, int id);
        [DllImport("kernel32", SetLastError = true)]
        public static extern short GlobalAddAtom(string lpString);
        [DllImport("kernel32", SetLastError = true)]
        public static extern short GlobalDeleteAtom(short nAtom);

        public const int MOD_ALT = 1;
        public const int MOD_CONTROL = 2;
        public const int MOD_SHIFT = 4;
        public const int MOD_WIN = 8;

        public const int WM_HOTKEY = 0x312;

        public GlobalHotkeys()
        {
            this.Handle = Process.GetCurrentProcess().Handle;
        }

        /// <summary>Handle of the current process</summary>
        public IntPtr Handle;

        /// <summary>The ID for the hotkey</summary>
        public short HotkeyID { get; private set; }

        /// <summary>Register the hotkey</summary>
        public void RegisterGlobalHotKey(int hotkey, int modifiers, IntPtr handle)
        {
            UnregisterGlobalHotKey();
            this.Handle = handle;
            RegisterGlobalHotKey(hotkey, modifiers);
        }

        /// <summary>Register the hotkey</summary>
        public void RegisterGlobalHotKey(int hotkey, int modifiers)
        {
            UnregisterGlobalHotKey();

            try
            {
                // use the GlobalAddAtom API to get a unique ID (as suggested by MSDN)
                string atomName = Thread.CurrentThread.ManagedThreadId.ToString("X8") + this.GetType().FullName;
                HotkeyID = GlobalAddAtom(atomName);
                if (HotkeyID == 0)
                    throw new Exception("Unable to generate unique hotkey ID. Error: " + Marshal.GetLastWin32Error().ToString());

                // register the hotkey, throw if any error
                if (!RegisterHotKey(this.Handle, HotkeyID, (uint)modifiers, (uint)hotkey))
                    throw new Exception("Unable to register hotkey. Error: " + Marshal.GetLastWin32Error().ToString());

            }
            catch (Exception ex)
            {
                // clean up if hotkey registration failed
                Dispose();
                Console.WriteLine(ex);
            }
        }

        /// <summary>Unregister the hotkey</summary>
        public void UnregisterGlobalHotKey()
        {
            if (this.HotkeyID != 0)
            {
                UnregisterHotKey(this.Handle, HotkeyID);
                // clean up the atom list
                GlobalDeleteAtom(HotkeyID);
                HotkeyID = 0;
            }
        }

        public void Dispose()
        {
            UnregisterGlobalHotKey();
        }
    }

}
