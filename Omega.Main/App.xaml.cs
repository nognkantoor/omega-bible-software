using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Common.Core.Pattern;
using Omega.ViewModel.Core;
using Common.Controls.WPF;
using Common.Core;
using log4net;
using Common.Core.Application;
using System.IO;

namespace Omega.Main
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static ILog Log = LogManager.GetLogger(typeof(App));

        public App()
        {
            Log.Info("Application start");
            Log.InfoFormat("Current directory: {0}", Directory.GetCurrentDirectory());
            InitializeBegin();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            InitializeWindow();
            InitializeEnd();
        }

        protected void InitializeWindow()
        {
            Log.Info("Initialize window");
            Window win = this.MainWindow;
            this.MainWindow = new OmegaMain();
            this.MainWindow.Show();
            this.MainWindow.DataContext = Singleton<MainViewModel>.Instance;
            if (win != null) win.Close();
        }

        public void InitializeEnd()
        {
            Log.Info("Initializing ends");
        }

        private void InitializeBegin()
        {
            Log.Info("Initializing begins");
        }
    }
}
