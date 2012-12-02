namespace Omega.Tools.BibleParser
{
    partial class XmlAttributePanel
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
            this.listPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.txtHtmlElement = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblHeader = new Common.Controls.Forms.ExtendedLabel();
            this.SuspendLayout();
            // 
            // listPanel
            // 
            this.listPanel.Location = new System.Drawing.Point(6, 33);
            this.listPanel.Name = "listPanel";
            this.listPanel.Size = new System.Drawing.Size(358, 29);
            this.listPanel.TabIndex = 2;
            // 
            // txtHtmlElement
            // 
            this.txtHtmlElement.Location = new System.Drawing.Point(264, 6);
            this.txtHtmlElement.Name = "txtHtmlElement";
            this.txtHtmlElement.Size = new System.Drawing.Size(100, 20);
            this.txtHtmlElement.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(181, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "HTML element";
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblHeader.Location = new System.Drawing.Point(3, 6);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(48, 13);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "Header";
            this.lblHeader.TranslationKey = "Header";
            translator1.IsGatheringMissingKeys = false;
            this.lblHeader.Translator = translator1;
            // 
            // XmlAttributePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtHtmlElement);
            this.Controls.Add(this.listPanel);
            this.Controls.Add(this.lblHeader);
            this.Name = "XmlAttributePanel";
            this.Size = new System.Drawing.Size(367, 65);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Common.Controls.Forms.ExtendedLabel lblHeader;
        private System.Windows.Forms.FlowLayoutPanel listPanel;
        private System.Windows.Forms.TextBox txtHtmlElement;
        private System.Windows.Forms.Label label1;
    }
}
