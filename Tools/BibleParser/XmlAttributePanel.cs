using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Interfaces.Controls.Forms;

namespace Omega.Tools.BibleParser
{
    public partial class XmlAttributePanel : UserControl, IDataContext
    {
        private IList<HtmlAttribute> _attributes = new List<HtmlAttribute>();

        public XmlAttributePanel()
        {
            InitializeComponent();
            listPanel.FlowDirection = FlowDirection.TopDown;
            listPanel.WrapContents = false;
            listPanel.ControlAdded += new ControlEventHandler(listPanelControlChanged);
            listPanel.ControlRemoved += new ControlEventHandler(listPanelControlChanged);
            listPanel.Controls.Add(new XmlAttributeRow());

            if(Parent != null && Parent is IDataContext)
            {
                DataContext = ((IDataContext)Parent).DataContext;
            }

            this.DataBindings.Add("HtmlElement", DataContext, "Tag");
            this.DataBindings.Add("HtmlAttributes", DataContext, "HtmlAttributes");
            
        }

        public object DataContext
        {
            get;
            set;
        }

        [Category("Appearance")]
        [Description("Header for the panel")]
        public string Header
        {
            get
            {
                if (lblHeader != null)
                {
                    return lblHeader.Text;
                }

                return null;
            }

            set
            {
                if (lblHeader != null)
                {
                    lblHeader.Text = value;
                }
            }
        }

        void listPanelControlChanged(object sender, ControlEventArgs e)
        {
            int index = 1;
            XmlAttributeRow lastRow = null;
            _attributes.Clear();
            foreach (Control control in listPanel.Controls)
            {
                if (control is XmlAttributeRow)
                {
                    lastRow = (XmlAttributeRow)control;
                    lastRow.IsAddButtonVisible = false;
                    lastRow.IsRemoveButtonVisible = true;
                    lastRow.Number = index++;
                }
            }

            if (lastRow != null)
            {
                lastRow.IsAddButtonVisible = true;
                if (listPanel.Controls.Count == 1)
                {
                    lastRow.IsRemoveButtonVisible = false;
                }
            }
            listPanel.Height = listPanel.PreferredSize.Height;
            this.Height = this.PreferredSize.Height;
        }

        public void RemoveAttribute(XmlAttributeRow row)
        {
            if (listPanel.Controls.Count > 1)
            {
                listPanel.Controls.Remove(row);
            }
        }

        public string HtmlElement
        {
            get
            {
                if (txtHtmlElement != null)
                {
                    return txtHtmlElement.Text;
                }

                return null;
            }
        }

        public IList<HtmlAttribute> HtmlAttributes
        {
            get
            {
                return _attributes;
            }
        }


        public void AddAttribute()
        {
            listPanel.Controls.Add(new XmlAttributeRow());

        }
    }
}
