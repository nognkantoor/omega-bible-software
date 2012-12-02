//#define CLIPBOARD
//#define SWITCHCOLORS

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Interop;
using Common.Controls.Windows;
using Common.Controls.Windows.Hotkey;
using System.Diagnostics;
using Common.Controls.WPF;
using Omega.Model.BibleText;


namespace Omega.TestApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       

        public MainWindow()
        {
            InitializeComponent();
            
#if CLIPBOARD
            Loaded += new RoutedEventHandler(MainWindow_Loaded); // For 'Clipboard tests'
#endif
#if SWITCHCOLORS
            PreviewKeyUp += new KeyEventHandler(CheckForSwitchStyleCombination); // For 'Switch colors tests'
#endif

            List<IVerse> verses = new List<IVerse>();

        }

      
        #region Switch colors tests

#if SWITCHCOLORS

        private string mainColorsStyle = "Colors.xaml";
        private string alternativeColorsStyle = "AlternativeColors.xaml";
        private string mainColorsStylePath = "/Omega.Main;component/Resources/Styles/";
        private string alternativeColorsPath = "Resources/Styles/";
        private bool isMainColorStyle = true;

        void CheckForSwitchStyleCombination(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Q && Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
            {
                if (isMainColorStyle)
                {
                    Application.Current.SwitchStyleDictionaries(mainColorsStyle, alternativeColorsPath + alternativeColorsStyle);
                }
                else
                {
                    Application.Current.SwitchStyleDictionaries(alternativeColorsStyle, mainColorsStylePath + mainColorsStyle);
                }
                isMainColorStyle = !isMainColorStyle;
            }
        }

#endif

        #endregion Switch colors tests



        #region Clipboard tests

#if CLIPBOARD
        Common.Controls.Windows.Clipboard clipboard = null;

        string clipboardData = null;

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GlobalHotkey hotkey = new GlobalHotkey(KeyModifier.Control, System.Windows.Forms.Keys.G);
            hotkey.Pressed += new EventHandler<Common.Controls.Windows.Hotkey.HotkeyEventArgs>(hotkey_Pressed);

            clipboard = new Common.Controls.Windows.Clipboard();

        }

        void clipboard_Changed(object sender, EventArgs e)
        {
            clipboard.Changed -= clipboard_Changed;
            string result = System.Windows.Clipboard.GetText();
            try
            {
                System.Windows.Clipboard.SetText(clipboardData);
            }
            catch (Exception ex)
            { }
            outputText.Text += result + System.Environment.NewLine;
        }



        void hotkey_Pressed(object sender, Common.Controls.Windows.Hotkey.HotkeyEventArgs e)
        {
            clipboard.Changed += new EventHandler(clipboard_Changed);
            clipboardData = System.Windows.Clipboard.GetText();
            clipboard.SendCopyCommand();

        }

#endif

        #endregion Clipboard tests
    }
}
