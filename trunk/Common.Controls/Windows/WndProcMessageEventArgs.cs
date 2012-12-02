using System;

namespace Common.Controls.Windows
{
    public class WndProcMessageEventArgs : EventArgs
    {
        public WndProcMessageEventArgs(System.Windows.Forms.Message message)
        {
            WindowHandle = message.HWnd;
            LParameter = message.LParam;
            WParameter = message.WParam;
            Result = message.Result;
            Message = message.Msg;
        }

        public WndProcMessageEventArgs(IntPtr windowHandle, int message, IntPtr wParameter, IntPtr lParameter)
        {
            WindowHandle = windowHandle;
            LParameter = lParameter;
            WParameter = wParameter;
            Message = message;
        }

        /// <summary>
        /// Gets or sets the window handle of the message.
        /// </summary>
        public IntPtr WindowHandle { get; set; }

        /// <summary>
        /// Specifies the System.Windows.Forms.Message.LParam field of the message.
        /// </summary>
        public IntPtr LParameter { get; set; }

        /// <summary>
        /// Gets or sets the ID number for the message.
        /// </summary>
        public int Message { get; set; }
        
        /// <summary>
        /// Specifies the value that is returned to Windows in response to handling the
        //  message.
        /// </summary>
        public IntPtr Result { get; set; }
        
        /// <summary>
        /// Gets or sets the System.Windows.Forms.Message.WParam field of the message.
        /// </summary>
        public IntPtr WParameter { get; set; }
    }
}
