using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows;

namespace Common.Controls.Windows.Hotkey
{
    public sealed class GlobalHotkey : IDisposable
    {
        #region Static

        #region Internal and private

        /// <summary>
        /// Holds the dictionary of hotkeys registered to the system.
        /// </summary>
        private static Dictionary<short, GlobalHotkey> _registeredHotKeys = new Dictionary<short, GlobalHotkey>();

        /// <summary>
        /// Registers a hotkey to the global message receiving queue.
        /// </summary>
        /// <param name="hotKey">Hot key to register.</param>
        internal void RegisterHotKey(GlobalHotkey hotKey)
        {
            if (hotKey != null &&  hotKey.ID != 0)
            {
                _registeredHotKeys[hotKey.ID] = hotKey;
            }
        }


        /// <summary>
        /// Unregisters a hotkey from the message receiving queue.
        /// </summary>
        /// <param name="hotKey">Hotkey to unregister.</param>
        internal static void UnregisterHotKey(GlobalHotkey hotKey)
        {
            if (hotKey != null)
            {
                GlobalHotkey.UnregisterHotKey(hotKey.ID);
            }
        }

        /// <summary>
        /// Unregisters a hotkey from the message receiving queue.
        /// </summary>
        /// <param name="id">Hotkey ID to unregister.</param>
        internal static void UnregisterHotKey(short id)
        {
            if (_registeredHotKeys.ContainsKey(id))
            {
                _registeredHotKeys.Remove(id);
            }
        }

        /// <summary>
        /// Handles the WndProc message and checks for a hotkey message.
        /// </summary>
        /// <param name="msg">Message ID (checking for hotkey message 0x312)</param>
        /// <param name="wParam">Message parameter (if this is a hotkey message, wParam should contain hotkey ID)</param>
        private static void HandleWndProc(int msg, IntPtr wParam)
        {
            const int WM_HOTKEY = 0x312;

            switch (msg)
            {
                case WM_HOTKEY:
                    {
                        if (_registeredHotKeys.ContainsKey((short)wParam))
                        {
                            _registeredHotKeys[(short)wParam].InvokeHotKey();
                        }
                        break;
                    }
            }
        }


        /// <summary>
        /// Handles comming external message from a extended form.
        /// </summary>
        /// <param name="sender">ExtendedForm form window as the owner of the message.</param>
        /// <param name="e">Message event args.</param>
        private static void FormExternalMessageComming(object sender, Forms.WndProcMessageEventArgs e)
        {
            HandleWndProc(e.Message.Msg, e.Message.WParam);
        }

        /// <summary>
        /// Handles a comming WndProc message in search of a hotkey message.
        /// </summary>
        /// <param name="hwnd">Handle to the window owner.</param>
        /// <param name="msg">Message id.</param>
        /// <param name="wParam">First parameter.</param>
        /// <param name="lParam">Second parameter.</param>
        /// <param name="handled">Handled reference</param>
        /// <returns>Result of the operation.</returns>
        private static IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            HandleWndProc(msg, wParam);
            return IntPtr.Zero;
        }

        #endregion Internal and private

        #region Public

        /// <summary>
        /// Gets the value indicating whether the global hot key mechanism 
        /// was initialized.
        /// </summary>
        public static bool Initialized
        {
            get;
            protected set;
        }

        /// <summary>
        /// Initializes the global hotkey mechanism for a windows form ExtendedForm application.
        /// </summary>
        /// <param name="form">ExtendedForm having a public event for receiving WndProc messages.</param>
        public static void Initialize(Common.Controls.Forms.ExtendedForm form)
        {
            if (form != null)
            {
                form.ExternalMessageComming += new EventHandler<Forms.WndProcMessageEventArgs>(FormExternalMessageComming);
                Initialized = true;
            }
        }

        /// <summary>
        /// Initializes the global hotkey mechanism for a WPF application.
        /// </summary>
        /// <param name="visual">The applications main window visual.</param>
        public static void Initialize(System.Windows.Media.Visual visual)
        {
            if (visual != null)
            {
                HwndSource source = PresentationSource.FromVisual(visual) as HwndSource;
                source.AddHook(WndProc);
                Initialized = true;
            }
        } 

        #endregion Public

        #endregion Static

        #region Interop methods

        /// <summary>
        /// Registers a hot key by an interop method.
        /// </summary>
        /// <param name="hwnd">Handle to the owner process.</param>
        /// <param name="id">ID of the hotkey.</param>
        /// <param name="fsModifiers">Code of the hotkey modifiers.</param>
        /// <param name="vk">Virtual key.</param>
        /// <returns>True if the operation was successfull.</returns>
        [DllImport("user32", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        protected static extern bool RegisterHotKey(IntPtr hwnd, int id, uint fsModifiers, uint vk);

        /// <summary>
        /// Unregisters a global hotkey by an interop method.
        /// </summary>
        /// <param name="hwnd">Handle to the owner process.</param>
        /// <param name="id">Id of the hotkey to unregister.</param>
        /// <returns>Result code.</returns>
        [DllImport("user32", SetLastError = true)]
        protected static extern int UnregisterHotKey(IntPtr hwnd, int id);

        /// <summary>
        /// Adds a global atom string to the global atom table. 
        /// This table contains a dictionary of string-int identifiers
        /// available globaly to windows.
        /// </summary>
        /// <param name="lpString">String key</param>
        /// <returns>Error code.</returns>
        [DllImport("kernel32", SetLastError = true)]
        protected static extern short GlobalAddAtom(string lpString);

        /// <summary>
        /// Removes a global atom string from the global atom table. 
        /// This table contains a dictionary of string-int identifiers
        /// available globaly to windows.
        /// </summary>
        /// <param name="nAtom">Atom identifier to remove.</param>
        /// <returns>Error code.</returns>
        [DllImport("kernel32", SetLastError = true)]
        protected static extern short GlobalDeleteAtom(short nAtom);

        #endregion Interop methods

        #region Protected properties

        /// <summary>
        /// Handle of the current process
        /// </summary>
        protected IntPtr Handle
        {
            get;
            set;
        }

        #endregion Protected properties

        #region Protected methods

        /// <summary>
        /// Registers the hotkey.
        /// </summary>
        /// <param name="hotkey">Hotkey key.</param>
        /// <param name="modifier">Hotkey modifier.</param>
        /// <param name="handle">Handle to a particular process.</param>
        protected void RegisterGlobalHotKey(System.Windows.Forms.Keys hotkey, KeyModifier modifier, IntPtr handle)
        {
            UnregisterGlobalHotKey();
            this.Handle = handle;
            RegisterGlobalHotKey(hotkey, modifier);
        }

        /// <summary>
        /// Registers the hotkey.
        /// </summary>
        /// <param name="key">Hotkey key.</param>
        /// <param name="modifier">Hotkey modifier.</param>
        protected void RegisterGlobalHotKey(System.Windows.Forms.Keys key, KeyModifier modifier)
        {
            UnregisterGlobalHotKey();

            try
            {
                // use the GlobalAddAtom API to get a unique ID (as suggested by MSDN)
                string atomName = Thread.CurrentThread.ManagedThreadId.ToString("X8") + this.GetType().FullName;
                ID = GlobalAddAtom(atomName);
                if (ID == 0)
                    throw new Exception("Unable to generate unique hotkey ID. Error: " + Marshal.GetLastWin32Error().ToString());

                // register the hotkey, throw if any error
                if (!RegisterHotKey(this.Handle, ID, (uint)modifier, (uint)key))
                    throw new Exception("Unable to register hotkey. Error: " + Marshal.GetLastWin32Error().ToString());

                Key = key;
                Modifiers = modifier;
                GlobalHotkey.UnregisterHotKey(this);
            }
            catch (Exception ex)
            {
                // clean up if hotkey registration failed
                Dispose();
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// Unregisters this global key.
        /// </summary>
        protected void UnregisterGlobalHotKey()
        {
            if (this.ID != 0)
            {
                GlobalHotkey.UnregisterHotKey(this);
                UnregisterHotKey(this.Handle, ID);
                // clean up the atom list
                GlobalDeleteAtom(ID);
                ID = 0;
                Key = System.Windows.Forms.Keys.None;
                Modifiers = KeyModifier.None;
            }
        }

        #endregion Protected methods

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the GlobalHotKey class.
        /// </summary>
        public GlobalHotkey()
        {
            if (GlobalHotkey.Initialized)
            {
                throw new InvalidOperationException(
                @"Cannot create a hotkey without initialization.
                  Initialize global hotkeys first with the static method GlobalHotkey.Initialize."
                );
            }
            this.Handle = Process.GetCurrentProcess().Handle;
        }

        /// <summary>
        /// Initializes a new instance of the GlobalHotkey class with the given key and modifier.
        /// </summary>
        /// <param name="key">Hotkey key.</param>
        /// <param name="modifiers">Hotkey key modifiers.</param>
        public GlobalHotkey(System.Windows.Forms.Keys key, KeyModifier modifiers) : this()
        {
            RegisterGlobalHotKey(key, modifiers);
        }

        #endregion Constructors

        #region Public properties

        /// <summary>
        /// Gets the ID of this hotkey
        /// </summary>
        public short ID
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the key used in the hotkey combination.
        /// </summary>
        public System.Windows.Forms.Keys Key
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the modifiers (Alt,Ctrl,Shift,Win) used in the hotkey combination.
        /// </summary>
        public KeyModifier Modifiers
        {
            get;
            protected set;
        }

        #endregion Public properties

        #region Public methods

        /// <summary>
        /// Sets this hotkey combination.
        /// </summary>
        /// <param name="key">Key used in this hotkey combination.</param>
        /// <param name="modifiers">Modifier key used in this hotkey combination.</param>
        public void SetHotKey(System.Windows.Forms.Keys key, KeyModifier modifiers)
        {
            RegisterGlobalHotKey(key, modifiers);
        }

        /// <summary>
        /// Unsets this hotkey by removing it from the queue listening for global hotkey events.
        /// </summary>
        public void UnsetHotKey()
        {
            UnregisterGlobalHotKey();
        }

        /// <summary>
        /// Disposes this hotkey.
        /// </summary>
        public void Dispose()
        {
            UnregisterGlobalHotKey();
        }

        #endregion Public methods

        #region Events

        /// <summary>
        /// Invokes the hotkey pressed event internally.
        /// </summary>
        internal void InvokeHotKey()
        {
            if (HotKeyPressed != null)
            {
                HotKeyPressed(this, new HotkeyEventArgs(this));
            }
        }

        /// <summary>
        /// Event occuring when this global hotkey combination is pressed in the system.
        /// </summary>
        public event EventHandler<HotkeyEventArgs> HotKeyPressed;

        #endregion Events
    }
}
