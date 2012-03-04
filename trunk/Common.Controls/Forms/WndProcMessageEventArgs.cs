using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Controls.Forms
{
    public class WndProcMessageEventArgs : EventArgs
    {
        public WndProcMessageEventArgs(System.Windows.Forms.Message message)
        {
            Message = message;
        }

        public System.Windows.Forms.Message Message
        {
            get;
            protected set;
        }

        public bool Handled
        {
            get;
            set;
        }
    }
}
