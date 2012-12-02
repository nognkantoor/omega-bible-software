using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Common.Controls.Forms
{
    public enum InputType
    {
        Text,
        Combo
    }

    public partial class InputControl : UserControl
    {
        #region Private fields

        /// <summary>
        /// Holds the left offset of the input object.
        /// </summary>
        private int _inputOffset = 200;

        /// <summary>
        /// Holds the witdh of the input control.
        /// </summary>
        private int _inputWidth = 50;

        /// <summary>
        /// Holds the type of the input.
        /// </summary>
        private InputType _inputType = InputType.Text;

        /// <summary>
        /// Holds the text of the input label.
        /// </summary>
        private string _labelText;

        /// <summary>
        /// Holds reference to the current input control.
        /// </summary>
        private Control _inputControl;
        
        #endregion Private fields

        #region Private methods

        /// <summary>
        /// Sets the visibility of the input controls
        /// based on the input type.
        /// </summary>
        /// <param name="type"></param>
        private void SetInputType(InputType type)
        {
            switch (type)
            {
                case Forms.InputType.Text:
                    txtInput.Visible = true;
                    cmbInput.Visible = false;
                    _inputControl = txtInput;
                    break;
                case Forms.InputType.Combo:
                    txtInput.Visible = false;
                    cmbInput.Visible = true;
                    _inputControl = cmbInput;
                    break;
            }
        }

        #endregion Private methods

        #region Constuctors

        /// <summary>
        /// Initializes a new instance of the InputControl class.
        /// </summary>
        public InputControl()
        {
            InitializeComponent();
            SetInputType(InputType);
            InputOffset = InputOffset;
            InputWidth = InputWidth;
        }
        
        #endregion Constuctors

        #region Public properties

        /// <summary>
        /// Gets or sets the type of input for this control.
        /// </summary>
        [Category("Appearance")]
        [Description("Type of input field")]
        public InputType InputType
        {
            get { return _inputType; }
            set
            {
                _inputType = value;
                SetInputType(value);
            }
        }

        /// <summary>
        /// Gets or sets the left offset of the input control.
        /// Usefull when using a column layout.
        /// </summary>
        [Category("Appearance")]
        [Description("Pixel offset of the input field")]
        public int InputOffset
        {
            get
            {
                if (_inputControl != null)
                {
                    return _inputControl.Left;
                }

                return _inputOffset;
            }

            set
            {
                if (value >= 0)
                {
                    _inputOffset = value;
                    if (_inputControl != null)
                    {
                        _inputControl.Left = value;
                    }
                    if (lblName != null)
                    {
                        lblName.Width = value;
                    }
                }
            }
        }

        
        /// <summary>
        /// Gets or sets the width of the input control.
        /// </summary>
        [Category("Appearance")]
        [Description("Width of the input control.")]
        public int InputWidth
        {
            get
            {
                if (_inputControl != null)
                {
                    return _inputControl.Width;
                }
                return _inputWidth;
            }

            set
            {
                if (value >= 0)
                {
                    _inputWidth = value;
                    if (_inputControl != null)
                    {
                        _inputControl.Width = value;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the label with the name.
        /// </summary>
        public ExtendedLabel NameLabel
        {
            get
            {
                return lblName;
            }
        }

        public string LabelText
        {
            get
            {
                if (lblName != null)
                {
                    return lblName.TranslationKey;
                }

                return _labelText;
            }

            set
            {
                _labelText = value;
                if (lblName != null)
                {
                    lblName.TranslationKey = _labelText;
                }
            }
        }

        /// <summary>
        /// Gets the reference to the input control.
        /// </summary>
        public Control ControlInput
        {
            get
            {
                return _inputControl;
            }
        }

        /// <summary>
        /// Gets the value inserted in this input.
        /// </summary>
        public object InputValue
        {
            get
            {
                switch (InputType)
                {
                    case Forms.InputType.Text:
                        return txtInput != null ? txtInput.Text : null;
                    case Forms.InputType.Combo:
                        return cmbInput != null ? cmbInput.SelectedItem : null;
                }

                return null;
            }
        }

        #endregion Public properties
    }
}
