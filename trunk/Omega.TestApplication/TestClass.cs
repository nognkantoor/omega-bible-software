using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;

namespace Omega.TestApplication
{
    [Flags]
    public enum KeyModifier
    {
        Alt = 1,
        Control = 2,
        Shift = 4,
        Win = 8
    }

    public class GlobalHotkey : IDisposable
    {
        #region Interop methods

        [DllImport("user32", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool RegisterHotKey(IntPtr hwnd, int id, uint fsModifiers, uint vk);
        [DllImport("user32", SetLastError = true)]
        public static extern int UnregisterHotKey(IntPtr hwnd, int id);
        [DllImport("kernel32", SetLastError = true)]
        public static extern short GlobalAddAtom(string lpString);
        [DllImport("kernel32", SetLastError = true)]
        public static extern short GlobalDeleteAtom(short nAtom);

        #endregion Interop methods

        #region Protected properties

        /// <summary>Handle of the current process</summary>
        protected IntPtr Handle
        {
            get;
            set;
        }

        /// <summary>The ID for the hotkey</summary>
        public short HotkeyID
        {
            get;
            set;
        }

        #endregion Protected properties

        #region Constructors

        public GlobalHotkey()
        {
            this.Handle = Process.GetCurrentProcess().Handle;
        }

        #endregion Constructors


        /// <summary>Register the hotkey</summary>
        public void RegisterGlobalHotKey(int hotkey, KeyModifier modifier, IntPtr handle)
        {
            UnregisterGlobalHotKey();
            this.Handle = handle;
            RegisterGlobalHotKey(hotkey, modifier);
        }

        /// <summary>Register the hotkey</summary>
        public void RegisterGlobalHotKey(int hotkey, KeyModifier modifier)
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
                if (!RegisterHotKey(this.Handle, HotkeyID, (uint)modifier, (uint)hotkey))
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
