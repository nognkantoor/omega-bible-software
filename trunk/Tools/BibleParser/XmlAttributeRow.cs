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
    public partial class XmlAttributeRow : UserControl, IDataContext
    {
        private string _error;

        public XmlAttributeRow()
        {
            InitializeComponent();
            if (Parent != null && Parent is IDataContext)
            {
                DataContext = ((IDataContext)Parent).DataContext;
            }

            this.DataBindings.Add("Attribute", DataContext, "Name");
            this.DataBindings.Add("Value", DataContext, "Value");
        }

        public object DataContext
        {
            get;
            set;
        }

        public string Attribute
        {
            get
            {
                if (txtAttribute != null)
                {
                    return txtAttribute.Text;
                }
                return null;
            }

            set
            {
                if (txtAttribute != null)
                {
                    txtAttribute.Text = value;
                }
            }
        }

        public XmlAttributePanel ParentXmlAttributePanel
        {
            get
            {
                return Parent.Parent as XmlAttributePanel;
            }
        }

        public string Value
        {
            get
            {
                if (txtValue != null)
                {
                    return txtValue.Text;
                }
                return null;
            }

            set
            {
                if (txtValue != null)
                {
                    txtValue.Text = value;
                }
            }
        }

        public string NumberString
        {
            get
            {
                if (lblNumber != null)
                {
                    return lblNumber.Text;
                }
                return null;
            }

            set
            {
                if (lblNumber != null)
                {
                    lblNumber.Text = value;
                }
            }
        }

        public int Number
        {
            get
            {
                if (lblNumber != null)
                {
                    string t = lblNumber.Text;
                    int n = 0;
                    if (int.TryParse(t, out n))
                    {
                        return n;
                    }
                }
                return 0;
            }

            set
            {
                if (lblNumber != null)
                {
                    lblNumber.Text = value.ToString();
                }
            }
        }

        public bool IsAddButtonVisible
        {
            get
            {
                if (btnAdd != null)
                {
                    return btnAdd.Visible;
                }

                return false;
            }

            set
            {
                if (btnAdd != null)
                {
                    btnAdd.Visible = value;
                }
            }
        }

        public bool IsRemoveButtonVisible
        {
            get
            {
                if (btnRemove != null)
                {
                    return btnRemove.Visible;
                }

                return false;
            }

            set
            {
                if (btnRemove != null)
                {
                    btnRemove.Visible = value;
                }
            }
        }

        public string Error
        {
            get { return _error; }
            set
            {
                _error = value;
                if (_error != null)
                {
                    txtAttribute.BackColor = Color.Red;
                    txtValue.BackColor = Color.Red;
                    errorToolTip.Show(_error, this);
                    errorToolTip.AutomaticDelay = 3000;
                }
            }
        }

        private void txtBoxEnter(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            ParentXmlAttributePanel.RemoveAttribute(this);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ParentXmlAttributePanel.AddAttribute();
        }


    }
}
