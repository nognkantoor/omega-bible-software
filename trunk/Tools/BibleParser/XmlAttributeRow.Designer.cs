namespace Omega.Tools.BibleParser
{
    partial class XmlAttributeRow
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
            this.components = new System.ComponentModel.Container();
            this.lblNumber = new System.Windows.Forms.Label();
            this.txtAttribute = new System.Windows.Forms.TextBox();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.errorToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // lblNumber
            // 
            this.lblNumber.AutoSize = true;
            this.lblNumber.Location = new System.Drawing.Point(3, 3);
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Size = new System.Drawing.Size(14, 13);
            this.lblNumber.TabIndex = 0;
            this.lblNumber.Text = "#";
            // 
            // txtAttribute
            // 
            this.txtAttribute.Location = new System.Drawing.Point(23, 0);
            this.txtAttribute.Name = "txtAttribute";
            this.txtAttribute.Size = new System.Drawing.Size(100, 20);
            this.txtAttribute.TabIndex = 1;
            this.txtAttribute.Text = "name";
            this.txtAttribute.Enter += new System.EventHandler(this.txtBoxEnter);
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(129, 0);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(162, 20);
            this.txtValue.TabIndex = 2;
            this.txtValue.Text = "value";
            this.txtValue.Enter += new System.EventHandler(this.txtBoxEnter);
            // 
            // btnRemove
            // 
            this.btnRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnRemove.Location = new System.Drawing.Point(297, 2);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(18, 18);
            this.btnRemove.TabIndex = 3;
            this.btnRemove.Text = "-";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(321, 2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(18, 18);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "+";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // XmlAttributeRow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.txtAttribute);
            this.Controls.Add(this.lblNumber);
            this.Name = "XmlAttributeRow";
            this.Size = new System.Drawing.Size(353, 24);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNumber;
        private System.Windows.Forms.TextBox txtAttribute;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ToolTip errorToolTip;
    }
}
