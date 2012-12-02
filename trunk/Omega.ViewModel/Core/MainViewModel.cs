using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Core.MVVM;
using log4net;

namespace Omega.ViewModel.Core
{
    public class MainViewModel : ViewModelBase
    {
        #region Fields

        private static ILog Log = LogManager.GetLogger(typeof(MainViewModel));

        private string _windowTitle = "Omega";

	    #endregion Fields

        private MainViewModel()
        {
            Log.Info("Initializing MainViewModel");
        }

        #region Window Presentation

        /// <summary>
        /// Gets or sets the title of the main window.
        /// </summary>
        public string WindowTitle
        {
            get { return _windowTitle; }
            set
            {
                _windowTitle = value;
                OnPropertyChanged("WindowTitle");
            }
        }

        #endregion Window Presentation
    }
}
