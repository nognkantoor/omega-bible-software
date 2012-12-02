using System;

namespace Common.Controls.Windows.Hotkey
{
    /// <summary>
    /// Defines key modifiers for a hotkey. 
    /// These modifiers are castable to int and 
    /// handle bitwise operations.
    /// </summary>
    [Flags]
    public enum KeyModifier
    {
        /// <summary>
        /// No modifier is used.
        /// </summary>
        None = 0,

        /// <summary>
        /// Alt (Alternative) key is the modifier.
        /// </summary>
        Alt = 1,

        /// <summary>
        /// Ctrl (Control) key is the modifier.
        /// </summary>
        Control = 2,

        /// <summary>
        /// Shift key is the modifier.
        /// </summary>
        Shift = 4,

        /// <summary>
        /// Win (Windows) key is the modifier.
        /// </summary>
        Win = 8
    }
}
