using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace Common.Controls.Windows.Hotkey
{
    /// <summary>
    /// GlobalHotkey class provides functionality for listening
    /// for key combinations outside out the application using it,
    /// over the whole system.
    /// </summary>
    /// <example>
    /// <para><see cref="GlobalHotkey"/> class can be used 
    /// through creating an instance with the choosen global hotkey combination.
    /// By listening for the HotkeyPressed 
    /// </para>
    /// <para>The following code example shows the usage for a key combination Ctrl+E.</para>
    /// <code>
    /// using System.Windows;
    /// using Common.Controls.Windows.Hotkey;
    /// using System.Windows.Forms;
    /// 
    /// public class HotkeyTest
    /// {
    ///     public HotkeyTest()
    ///     {
    ///         GlobalHotkey hotkey = new GlobalHotkey(KeyModifier.Control, Keys.E);
    ///         hotkey.Pressed += new EventHandler<HotkeyEventArgs>(HotkeyPressed);
    ///     }
    /// 
    ///     private void HotkeyPressed(object sender, HotkeyEventArgs e)
    ///     {
    ///         MessageBox.Show("There was a CTRL+E combination pressed");
    ///     }
    /// }
    /// </code>
    /// </example>
    public sealed class GlobalHotkey : IDisposable
    {
        #region Static

        #region Internal and private

        /// <summary>
        /// Holds the dictionary of hotkeys registered to the system.
        /// </summary>
        private static Dictionary<short, GlobalHotkey> _registeredHotkeys = new Dictionary<short, GlobalHotkey>();

        /// <summary>
        /// Holds the helper object for providng window handle and WndProc message handling.
        /// </summary>
        private static WindowApplication _helper = new WindowApplication();

        /// <summary>
        /// Registers a hotkey to the global message receiving queue.
        /// </summary>
        /// <param name="hotKey">Hot key to register.</param>
        internal static void RegisterNewHotkey(GlobalHotkey hotKey)
        {
            if (hotKey != null &&  hotKey.ID != 0)
            {
                _registeredHotkeys[hotKey.ID] = hotKey;
            }
        }

        /// <summary>
        /// Unregisters a hotkey from the message receiving queue.
        /// </summary>
        /// <param name="hotKey">Hotkey to unregister.</param>
        internal static void UnregisterHotkey(GlobalHotkey hotKey)
        {
            if (hotKey != null)
            {
                GlobalHotkey.UnregisterHotkey(hotKey.ID);
            }
        }

        /// <summary>
        /// Unregisters a hotkey from the message receiving queue.
        /// </summary>
        /// <param name="id">Hotkey ID to unregister.</param>
        internal static void UnregisterHotkey(short id)
        {
            if (_registeredHotkeys.ContainsKey(id))
            {
                _registeredHotkeys.Remove(id);
            }
        }

        /// <summary>
        /// Initializes listening for the WndProc message.
        /// </summary>
        static GlobalHotkey()
        {
            AppDomain.CurrentDomain.DomainUnload += new EventHandler(CurrentDomainUnload);
            _helper.WndProcMessageReceived += new EventHandler<WndProcMessageEventArgs>(WndProcMessageReceived);
        }

        /// <summary>
        /// Handles disposal of the window application helper when the domain unloads.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void CurrentDomainUnload(object sender, EventArgs e)
        {
            if (_helper != null)
            {
                _helper.Dispose();
                _helper = null;
            }
        }

        /// <summary>
        /// Handles the WndProc message and checks for a hotkey message.
        /// </summary>
        static void WndProcMessageReceived(object sender, WndProcMessageEventArgs e)
        {
            const int WM_HOTKEY = 0x312;

            switch (e.Message)
            {
                case WM_HOTKEY:
                    {
                        if (_registeredHotkeys.ContainsKey((short)e.WParameter))
                        {
                            _registeredHotkeys[(short)e.WParameter].InvokeHotkey();
                        }
                        break;
                    }
            }
        }

        #endregion Internal and private

        #region Public

        /// <summary>
        /// Gets a list of registered hotkeys in this application instance.
        /// Unregistering hotkeys can be done by invoking the UnregisterGlobalHotkey method
        /// on a particular <see cref="GlobalHotkey"/> instance.
        /// </summary>
        public static ReadOnlyCollection<GlobalHotkey> RegisteredHotkeys
        {
            get
            {
                return _registeredHotkeys.Values.ToList().AsReadOnly();
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
        private static extern bool RegisterHotKey(IntPtr hwnd, int id, uint fsModifiers, uint vk);

        /// <summary>
        /// Unregisters a global hotkey by an interop method.
        /// </summary>
        /// <param name="hwnd">Handle to the owner process.</param>
        /// <param name="id">Id of the hotkey to unregister.</param>
        /// <returns>Result code.</returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        /// <summary>
        /// Adds a global atom string to the global atom table. 
        /// This table contains a dictionary of string-int identifiers
        /// available globaly to windows.
        /// </summary>
        /// <param name="lpString">String key</param>
        /// <returns>Error code.</returns>
        [DllImport("kernel32", SetLastError = true)]
        private static extern short GlobalAddAtom(string lpString);

        /// <summary>
        /// Removes a global atom string from the global atom table. 
        /// This table contains a dictionary of string-int identifiers
        /// available globaly to windows.
        /// </summary>
        /// <param name="nAtom">Atom identifier to remove.</param>
        /// <returns>Error code.</returns>
        [DllImport("kernel32", SetLastError = true)]
        private static extern short GlobalDeleteAtom(short nAtom);

        #endregion Interop methods

        #region Protected methods

        /// <summary>
        /// Registers the hotkey.
        /// </summary>
        /// <param name="modifier">Hotkey modifier.</param>
        /// <param name="key">Hotkey key.</param>
        private void RegisterGlobalHotkey(KeyModifier modifier, System.Windows.Forms.Keys key)
        {
            UnregisterGlobalHotkey();

            try
            {
                // use the GlobalAddAtom API to get a unique ID (as suggested by MSDN)
                string atomName = Thread.CurrentThread.ManagedThreadId.ToString("X8") + this.GetType().FullName;
                ID = GlobalAddAtom(atomName);
                if (ID == 0)
                    throw new Exception("Unable to generate unique hotkey ID. Error: " + Marshal.GetLastWin32Error().ToString());

                // register the hotkey, throw if any error
                if (!RegisterHotKey(this.Helper.Handle, ID, (uint)modifier, (uint)key))
                    throw new Exception("Unable to register hotkey. Error: " + Marshal.GetLastWin32Error().ToString());

                Key = key;
                Modifiers = modifier;
                GlobalHotkey.RegisterNewHotkey(this);
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
        private void UnregisterGlobalHotkey()
        {
            if (this.ID != 0)
            {
                GlobalHotkey.UnregisterHotkey(this);
                UnregisterHotKey(this.Helper.Handle, ID);
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
        /// Initializes a new instance of the GlobalHotkey class.
        /// </summary>
        public GlobalHotkey()
        {
            Helper = new WindowApplication();
        }

        /// <summary>
        /// Initializes a new instance of the GlobalHotkey class with the given key and modifier.
        /// </summary>
        /// <param name="modifiers">Hotkey key modifiers.</param>
        /// <param name="key">Hotkey key.</param>
        public GlobalHotkey(KeyModifier modifiers, System.Windows.Forms.Keys key) : this()
        {
            RegisterGlobalHotkey(modifiers, key);
        }

        /// <summary>
        /// Disposes the GlobalHotkey class instance.
        /// </summary>
        ~GlobalHotkey()
        {
            Dispose();
        }

        #endregion Constructors

        #region Public properties

        /// <summary>
        /// Gets the ID of this hotkey
        /// </summary>
        public short ID
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the key used in the hotkey combination.
        /// </summary>
        public System.Windows.Forms.Keys Key
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the modifiers (Alt,Ctrl,Shift,Win) used in the hotkey combination.
        /// </summary>
        public KeyModifier Modifiers
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the helper window application object providing the window handle and WndProc message handling.
        /// </summary>
        internal WindowApplication Helper
        {
            get;
            set;
        }

        #endregion Public properties

        #region Public methods

        /// <summary>
        /// Sets this hotkey combination.
        /// </summary>
        /// <param name="key">Key used in this hotkey combination.</param>
        /// <param name="modifiers">Modifier key used in this hotkey combination.</param>
        public void SetHotkey(System.Windows.Forms.Keys key, KeyModifier modifiers)
        {
            RegisterGlobalHotkey(modifiers, key);
        }

        /// <summary>
        /// Unsets this hotkey by removing it from the queue listening for global hotkey events.
        /// </summary>
        public void UnsetHotkey()
        {
            UnregisterGlobalHotkey();
        }

        /// <summary>
        /// Disposes this hotkey.
        /// </summary>
        public void Dispose()
        {
            UnregisterGlobalHotkey();
        }

        #endregion Public methods

        #region Events

        /// <summary>
        /// Invokes the hotkey pressed event internally.
        /// </summary>
        internal void InvokeHotkey()
        {
            if (Pressed != null)
            {
                Pressed(this, new HotkeyEventArgs(this));
            }
        }

        /// <summary>
        /// Event occuring when this global hotkey combination is pressed in the system.
        /// </summary>
        public event EventHandler<HotkeyEventArgs> Pressed;

        #endregion Events
    }
}
