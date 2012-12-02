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
using Omega.Model.BibleText;
using Omega.View.Bible;

namespace Omega.View.Viewer
{
    /// <summary>
    /// Interaction logic for TextViewer.xaml
    /// </summary>
    public partial class TextViewer : UserControl
    {
        public TextViewer()
        {
            InitializeComponent();
        }

        protected void OnVerseSourceChanged(DependencyPropertyChangedEventArgs args)
        { 
            FlowDocument document = new FlowDocument();
            if(VerseSource != null)
            {
                foreach(var verse in VerseSource)
                {
                    document.Blocks.Add(new BlockUIContainer(new Verse() { DataContext = verse }));
                }
            }
        }

        /// <summary>
        /// Gets or sets the source for the verses.
        /// </summary>
        public List<IVerse> VerseSource
        {
            get { return (List<IVerse>)GetValue(VerseSourceProperty); }
            set { SetValue(VerseSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VerseSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VerseSourceProperty =
            DependencyProperty.Register("VerseSource", 
            typeof(List<IVerse>), typeof(TextViewer), 
            new UIPropertyMetadata(null, (s,args) => ((TextViewer)s).OnVerseSourceChanged(args)));


        /// <summary>
        /// Gets or sets the content of the document to view. Dependent on VerseSource.
        /// </summary>
        public FlowDocument TextContent
        {
            get { return (FlowDocument)GetValue(TextContentProperty); }
            set { SetValue(TextContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextContentProperty =
            DependencyProperty.Register("TextContent", typeof(FlowDocument), typeof(TextViewer), new UIPropertyMetadata(null));

        
        
    }
}
