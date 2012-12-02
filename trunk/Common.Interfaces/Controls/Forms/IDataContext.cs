using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Interfaces.Controls.Forms
{
    public interface IDataContext
    {
        object DataContext
        {
            get;
            set;
        }
    }
}
