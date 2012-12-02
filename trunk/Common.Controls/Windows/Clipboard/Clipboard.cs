using System;
using System.Runtime.InteropServices;

namespace Common.Controls.Windows
{
    #region Interop structs

    /// <summary>
    /// Structure for a keyboard input (KEYBDINPUT)
    /// </summary>
    internal struct KeyboardInput
    {
        /// <summary>
        /// Holds the key code for the keyboard input ASCII 1-254
        /// </summary>
        public ushort KeyCode;

        /// <summary>
        /// Holds a hardware scan code for the key. If 'Flags' specifies KEYEVENTF_UNICODE, 
        /// wScan specifies a Unicode character which is to be sent to the foreground 
        /// application
        /// </summary>
        public ushort Scan;

        /// <summary>
        /// Specifies various aspects of a keystroke.  See the KEYEVENTF_ constants for 
        /// more information
        /// </summary>
        public uint Flags;

        /// <summary>
        /// The time stamp for the event, in milliseconds. If this parameter is zero, 
        /// the system will provide its own time stamp
        /// </summary>
        public long Time;

        /// <summary>
        /// An additional value associated with the keystroke. 
        /// Use the GetMessageExtraInfo function to obtain this information.
        /// </summary>
        public IntPtr ExtraInfo;
    };

    /// <summary>
    /// Input (INPUT) structure for the SendInput user32 function.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 28)]
    internal struct Input
    {
        /// <summary>
        /// Input type
        /// </summary>
        [FieldOffset(0)]
        public uint Type;

        /// <summary>
        /// Keyboard input
        /// </summary>
        [FieldOffset(sizeof(uint))]
        public KeyboardInput KeyboardInput;
    };

    #endregion Interop structs

    /// <summary>
    /// The Common.Core.Utils.Clipboard class is a extended
    /// clipboard manager with clipboard listening events,
    /// hotkey selected text copying functionality etc.
    /// </summary>
    public sealed class Clipboard : IDisposable
    {
        #region Interop methods

        /// <summary>
        /// Adds a window application into clipboard messaging chain.
        /// </summary>
        /// <param name="viewerToAddHandle">Handle of the appliation window to add into the messaging chain.</param>
        /// <returns>Handle to the next clipboard viewer for sending the message in the chain.</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SetClipboardViewer(IntPtr viewerToAddHandle);

        /// <summary>
        /// Removes a window application from clipboard viewing chain.
        /// </summary>
        /// <param name="hWndRemove">Handle to the viewer to be removed.</param>
        /// <param name="hWndNewNext">Handle to the next viewer in the chain.</param>
        /// <returns>True if success.</returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

        /// <summary>
        /// Sends an keyboard input message to the currently active application.
        /// This method works like those keys were pressed on the real keyboard
        /// and mocks those keys.
        /// </summary>
        /// <param name="nInputs">Number of input elements.</param>
        /// <param name="pInputs">Array of input elements</param>
        /// <param name="cbSize">Size of an input element.</param>
        /// <returns>Error code. Zero is OK.</returns>
        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint nInputs, Input[] pInputs, int cbSize);

        /// <summary>
        /// Gets some extra message info for the input structure (?).
        /// </summary>
        /// <returns>Handle to the message info.</returns>
        [DllImport("user32.dll")]
        private static extern IntPtr GetMessageExtraInfo();

        #endregion Interop methods

        #region Interop const

        private const int INPUT_MOUSE = 0;
        private const int INPUT_KEYBOARD = 1;
        private const int INPUT_HARDWARE = 2;
        private const uint KEYEVENTF_EXTENDEDKEY = 0x0001;
        private const uint KEYEVENTF_KEYUP = 0x0002;
        private const uint KEYEVENTF_UNICODE = 0x0004;
        private const uint KEYEVENTF_SCANCODE = 0x0008;
        private const uint XBUTTON1 = 0x0001;
        private const uint XBUTTON2 = 0x0002;
        //private const uint MOUSEEVENTF_MOVE = 0x0001;
        //private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        //private const uint MOUSEEVENTF_LEFTUP = 0x0004;
        //private const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        //private const uint MOUSEEVENTF_RIGHTUP = 0x0010;
        //private const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        //private const uint MOUSEEVENTF_MIDDLEUP = 0x0040;
        //private const uint MOUSEEVENTF_XDOWN = 0x0080;
        //private const uint MOUSEEVENTF_XUP = 0x0100;
        //private const uint MOUSEEVENTF_WHEEL = 0x0800;
        //private const uint MOUSEEVENTF_VIRTUALDESK = 0x4000;
        //private const uint MOUSEEVENTF_ABSOLUTE = 0x8000;

        public enum Modifier : ushort
        {
            SHIFT = 0x10,
            CONTROL = 0x11,
            MENU = 0x12,
            ESCAPE = 0x1B,
            BACK = 0x08,
            TAB = 0x09,
            RETURN = 0x0D,
            PRIOR = 0x21,
            NEXT = 0x22,
            END = 0x23,
            HOME = 0x24,
            LEFT = 0x25,
            UP = 0x26,
            RIGHT = 0x27,
            DOWN = 0x28,
            SELECT = 0x29,
            PRINT = 0x2A,
            EXECUTE = 0x2B,
            SNAPSHOT = 0x2C,
            INSERT = 0x2D,
            DELETE = 0x2E,
            HELP = 0x2F,
            NUMPAD0 = 0x60,
            NUMPAD1 = 0x61,
            NUMPAD2 = 0x62,
            NUMPAD3 = 0x63,
            NUMPAD4 = 0x64,
            NUMPAD5 = 0x65,
            NUMPAD6 = 0x66,
            NUMPAD7 = 0x67,
            NUMPAD8 = 0x68,
            NUMPAD9 = 0x69,
            MULTIPLY = 0x6A,
            ADD = 0x6B,
            SEPARATOR = 0x6C,
            SUBTRACT = 0x6D,
            DECIMAL = 0x6E,
            DIVIDE = 0x6F,
            F1 = 0x70,
            F2 = 0x71,
            F3 = 0x72,
            F4 = 0x73,
            F5 = 0x74,
            F6 = 0x75,
            F7 = 0x76,
            F8 = 0x77,
            F9 = 0x78,
            F10 = 0x79,
            F11 = 0x7A,
            F12 = 0x7B,
            OEM_1 = 0xBA,   // ',:' for US
            OEM_PLUS = 0xBB,   // '+' any country
            OEM_COMMA = 0xBC,   // ',' any country
            OEM_MINUS = 0xBD,   // '-' any country
            OEM_PERIOD = 0xBE,   // '.' any country
            OEM_2 = 0xBF,   // '/?' for US
            OEM_3 = 0xC0,   // '`~' for US
            MEDIA_NEXT_TRACK = 0xB0,
            MEDIA_PREV_TRACK = 0xB1,
            MEDIA_STOP = 0xB2,
            MEDIA_PLAY_PAUSE = 0xB3,
            LWIN = 0x5B,
            RWIN = 0x5C
        }

        /// <summary>
        /// Message ID for a clipboard copying event.
        /// </summary>
        private const int WM_DRAWCLIPBOARD = 0x0308;

        /// <summary>
        /// Message ID for a change in clipboard viewer chain.
        /// </summary>
        private const int WM_CHANGECBCHAIN = 0x030D;

        #endregion Interop const

        #region Private properties

        private IntPtr _nextClipboardViewer;

        /// <summary>
        /// Gets or sets Interop window application helper.
        /// </summary>
        private WindowApplication Helper
        {
            get;
            set;
        }

        #endregion Private properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Clipboard class.
        /// </summary>
        public Clipboard()
        {
            Helper = new WindowApplication();
            _nextClipboardViewer = SetClipboardViewer(Helper.Handle);
            Helper.WndProcMessageReceived += new EventHandler<WndProcMessageEventArgs>(ClipboardMessage);
        }

        /// <summary>
        /// Destroys the Clipboard class instance.
        /// </summary>
        ~Clipboard()
        {
            Dispose();
        }

        #endregion Constructors

        #region Private methods

        /// <summary>
        /// Handles WndProc clipboard message.
        /// </summary>
        /// <param name="sender">Message sender</param>
        /// <param name="e">WndProc message event arguments.</param>
        private void ClipboardMessage(object sender, WndProcMessageEventArgs e)
        {
            if (e.Message == WM_DRAWCLIPBOARD)
            {
                if (Changed != null)
                {
                    Changed(this, EventArgs.Empty);
                }

                if (_nextClipboardViewer != null)
                {
                    Helper.SentWindowProcesMessage(_nextClipboardViewer, e.Message, e.WParameter, e.LParameter);
                }
            }
            else if (e.Message == WM_CHANGECBCHAIN)
            {
                // Repair the chain
                if (e.WParameter == _nextClipboardViewer)
                {
                    _nextClipboardViewer = e.LParameter;
                }
                // Otherwise, pass the message to the next link.
                else if (_nextClipboardViewer != null)
                {
                    Helper.SentWindowProcesMessage(_nextClipboardViewer, e.Message, e.WParameter, e.LParameter);
                }
            }
        }

        /// <summary>
        /// Send a mock of pressed keyboard combination.
        /// Use this method for sending copy/paste/cut commands.
        /// </summary>
        /// <param name="modifier">Key modifier.</param>
        /// <param name="key">The function key.</param>
        private void SendKey(Modifier modifier, System.Windows.Forms.Keys key)
        {
            Input[] inputs = new Input[4];

            // Modifier key down
            Input structInput = new Input();
            structInput.Type = INPUT_KEYBOARD;
            structInput.KeyboardInput.Scan = 0;
            structInput.KeyboardInput.Time = 0;
            structInput.KeyboardInput.Flags = 0;
            structInput.KeyboardInput.ExtraInfo = GetMessageExtraInfo();
            structInput.KeyboardInput.KeyCode = (ushort)modifier;
            inputs[0] = structInput;

            // Function key down
            structInput = new Input();
            structInput.Type = INPUT_KEYBOARD;
            structInput.KeyboardInput.Scan = 0;
            structInput.KeyboardInput.Time = 0;
            structInput.KeyboardInput.Flags = 0;
            structInput.KeyboardInput.ExtraInfo = GetMessageExtraInfo();
            structInput.KeyboardInput.KeyCode = (ushort)key;
            inputs[1] = structInput;

            // Modifier key up
            structInput = new Input();
            structInput.Type = INPUT_KEYBOARD;
            structInput.KeyboardInput.Scan = 0;
            structInput.KeyboardInput.Time = 0;
            structInput.KeyboardInput.Flags = KEYEVENTF_KEYUP;
            structInput.KeyboardInput.ExtraInfo = GetMessageExtraInfo();
            structInput.KeyboardInput.KeyCode = (ushort)modifier;
            inputs[2] = structInput;

            // Function key up
            structInput = new Input();
            structInput.Type = INPUT_KEYBOARD;
            structInput.KeyboardInput.Scan = 0;
            structInput.KeyboardInput.Time = 0;
            structInput.KeyboardInput.Flags = KEYEVENTF_KEYUP;
            structInput.KeyboardInput.ExtraInfo = GetMessageExtraInfo();
            structInput.KeyboardInput.KeyCode = (ushort)key;
            inputs[3] = structInput;

            uint returnInt = SendInput(4, inputs, Marshal.SizeOf(structInput));
        }

        #endregion Private methods
        
        #region Public methods

        /// <summary>
        /// This method will send a copy command to the currently active application.
        /// </summary>
        public void SendCopyCommand()
        {
            SendKey(Modifier.CONTROL, System.Windows.Forms.Keys.C);
        }

        /// <summary>
        /// This method will send a paste command to the currently active application.
        /// </summary>
        public void SendPasteCommand()
        {
            SendKey(Modifier.CONTROL, System.Windows.Forms.Keys.V);
        }

        /// <summary>
        /// This method will send a cut command to the currently active application.
        /// </summary>
        public void SendCutCommand()
        {
            SendKey(Modifier.CONTROL, System.Windows.Forms.Keys.X);
        }

        #endregion Public methods

        #region Events

        /// <summary>
        /// Indicates the event of changes in the clipboard content.
        /// </summary>
        public event EventHandler Changed;

        #endregion Events

        #region IDisposable members

        /// <summary>
        /// Disposes Clipboard class instance by removing this window 
        /// from the clipboard viewer chain in Windows system
        /// </summary>
        public void Dispose()
        {
            ChangeClipboardChain(Helper.Handle, _nextClipboardViewer);   
        }

        #endregion IDisposable members
    }


}
