using System.Windows;
using Common.Controls.WPF;

namespace Omega.TestApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            this.SetStyleDictionaries("/Resources/Styles/", "AlternativeColors.xaml");

            Window win = this.MainWindow;
            this.MainWindow = new MainWindow();
            this.MainWindow.Show();
            if (win != null) win.Close();
        }
    }
}
