namespace Common.Controls.Forms
{
    partial class InputControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Common.Core.Translation.Translator translator1 = new Common.Core.Translation.Translator();
            this.lblName = new Common.Controls.Forms.ExtendedLabel();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.cmbInput = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(3, 10);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(62, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Label name";
            this.lblName.TranslationKey = "Label name";
            translator1.IsGatheringMissingKeys = false;
            this.lblName.Translator = translator1;
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(81, 7);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(100, 20);
            this.txtInput.TabIndex = 1;
            // 
            // cmbInput
            // 
            this.cmbInput.FormattingEnabled = true;
            this.cmbInput.Location = new System.Drawing.Point(81, 7);
            this.cmbInput.Name = "cmbInput";
            this.cmbInput.Size = new System.Drawing.Size(100, 21);
            this.cmbInput.TabIndex = 2;
            // 
            // InputControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbInput);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.lblName);
            this.Name = "InputControl";
            this.Size = new System.Drawing.Size(278, 32);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ExtendedLabel lblName;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.ComboBox cmbInput;
    }
}
