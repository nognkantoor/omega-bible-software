using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using Common.Controls.Forms;

namespace Common.Controls.Windows
{
    /// <summary>
    /// Helper class for handling a window application functionality.
    /// Provides a handle to the window and message receiving and sending functionality.
    /// </summary>
    internal class WindowApplication : IDisposable
    {
        #region Interop

        /// <summary>
        /// Send WndProc message
        /// </summary>
        /// <param name="receiverHandler">Handler to the receiver application window.</param>
        /// <param name="message">Message ID</param>
        /// <param name="wParameter">W parameter</param>
        /// <param name="lParameter">L parameter</param>
        /// <returns>Result of the operation</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr receiverHandler, UInt32 message, IntPtr wParameter, IntPtr lParameter);
        
        #endregion Interop

        #region Fields

        /// <summary>
        /// Holds the event handler for the WndProc event of a window application.
        /// </summary>
        private event EventHandler<WndProcMessageEventArgs> _wndProcMessageReceived;

        /// <summary>
        /// Holds the source for WndProc hook.
        /// </summary>
        private HwndSource _source;

        /// <summary>
        /// Holds the handle to the window application if not zero.
        /// </summary>
        private IntPtr _handle;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the WindowApplication class.
        /// </summary>
        public WindowApplication()
        { 
        }

        /// <summary>
        /// Disposes the WindowApplication helper.
        /// </summary>
        ~WindowApplication()
        {
            Dispose();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the handle of the current window
        /// </summary>
        public IntPtr Handle
        {
            get
            {
                if (_handle != null && (int)_handle != 0)
                {
                    return _handle;
                }

                _handle = (IntPtr)0;
                try
                {
                    _handle = Process.GetCurrentProcess().MainWindowHandle;
                    if ((int)_handle == 0)
                    {
                        throw new Exception("Not able to take the window handle. "+
                            "The application is either windowless, "+
                            "or hidden in tray or you have tried to invoke "+
                            "this functionality before window Loaded event.");
                    }
                    return _handle;
                }
                catch (Exception invalid)
                {
                    throw new Exception("Cannot take the handle of the application window. See inner exception for details", invalid);
                }
            }
        }

        #endregion Properties

        #region WndProc message event

        /// <summary>
        /// Send WndProc message
        /// </summary>
        /// <param name="receiverHandler">Handler to the receiver application window.</param>
        /// <param name="message">Message ID</param>
        /// <param name="wParameter">W parameter</param>
        /// <param name="lParameter">L parameter</param>
        /// <returns>Result of the operation</returns>
        public IntPtr SentWindowProcesMessage(IntPtr receiverHandler, int message, IntPtr wParameter, IntPtr lParameter)
        {
            return WindowApplication.SendMessage(receiverHandler, (UInt32)message, wParameter, lParameter);
        }

        /// <summary>
        /// Handles a comming WndProc message.
        /// </summary>
        /// <param name="hwnd">Handle to the window owner.</param>
        /// <param name="msg">Message id.</param>
        /// <param name="wParam">First parameter.</param>
        /// <param name="lParam">Second parameter.</param>
        /// <param name="handled">Handled reference</param>
        /// <returns>Result of the operation.</returns>
        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (_wndProcMessageReceived != null)
            {
                _wndProcMessageReceived(this, new WndProcMessageEventArgs(hwnd, msg, wParam, lParam));
            }

            return IntPtr.Zero;
        }

        /// <summary>
        /// Event hooked to the WndProc message event of a window application.
        /// </summary>
        public event EventHandler<WndProcMessageEventArgs> WndProcMessageReceived
        {
            add 
            { 
                _wndProcMessageReceived += value;
                if (_source == null)
                {
                    _source = HwndSource.FromHwnd(Handle);
                    _source.AddHook(WndProc);
                }
            }
            remove { _wndProcMessageReceived -= value; }
        }

        #endregion WndProc message event

        #region IDisposable members

        /// <summary>
        /// Handles the disposal of the WndProc hook.
        /// </summary>
        public void Dispose()
        {
            if (_source != null)
            {
                _source.RemoveHook(WndProc);
            }
        }
        
        #endregion IDisposable members
    }
}
