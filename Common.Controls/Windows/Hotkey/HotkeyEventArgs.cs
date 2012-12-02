using System;

namespace Common.Controls.Windows.Hotkey
{
    /// <summary>
    /// EventArgs for a hotkey event. Contain information about the hotkey being pressed.
    /// </summary>
    public class HotkeyEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the HotkeyEventArgs class with a global hotkey.
        /// </summary>
        /// <param name="hotkey">Hotkey being the source of the event.</param>
        public HotkeyEventArgs(GlobalHotkey hotkey)
        {
            Hotkey = hotkey;
        }

        /// <summary>
        /// Gets the hotkey being the event source.
        /// </summary>
        public GlobalHotkey Hotkey
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the key without modifier - one part of the hotkey.
        /// </summary>
        public System.Windows.Forms.Keys Key
        {
            get
            {
                return Hotkey.Key;
            }
        }

        /// <summary>
        /// Gets the modifiers of the hotkey.
        /// </summary>
        public KeyModifier Modifiers
        {
            get
            {
                return Hotkey.Modifiers;
            }
        }
    }
}
