namespace Omega.Tools.BibleParser
{
    partial class BiblePageParser
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblPageEndings = new System.Windows.Forms.Label();
            this.txtPageEndings = new System.Windows.Forms.TextBox();
            this.dlgChoosePageEndingsDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnLoadPageEndingsFromFile = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.tabControlContainer = new System.Windows.Forms.TabControl();
            this.tpageParserSettings = new System.Windows.Forms.TabPage();
            this.tpageParserTest = new System.Windows.Forms.TabPage();
            this.txtParserResult = new System.Windows.Forms.TextBox();
            this.lblBaseAddress = new System.Windows.Forms.Label();
            this.txtBaseAddress = new System.Windows.Forms.TextBox();
            this.containerAttributes = new Omega.Tools.BibleParser.XmlAttributePanel();
            this.tabControlContainer.SuspendLayout();
            this.tpageParserSettings.SuspendLayout();
            this.tpageParserTest.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPageEndings
            // 
            this.lblPageEndings.AutoSize = true;
            this.lblPageEndings.Location = new System.Drawing.Point(15, 51);
            this.lblPageEndings.Name = "lblPageEndings";
            this.lblPageEndings.Size = new System.Drawing.Size(72, 13);
            this.lblPageEndings.TabIndex = 1;
            this.lblPageEndings.Text = "Page endings";
            // 
            // txtPageEndings
            // 
            this.txtPageEndings.Location = new System.Drawing.Point(132, 44);
            this.txtPageEndings.Multiline = true;
            this.txtPageEndings.Name = "txtPageEndings";
            this.txtPageEndings.Size = new System.Drawing.Size(398, 93);
            this.txtPageEndings.TabIndex = 2;
            // 
            // btnLoadPageEndingsFromFile
            // 
            this.btnLoadPageEndingsFromFile.Location = new System.Drawing.Point(18, 67);
            this.btnLoadPageEndingsFromFile.Name = "btnLoadPageEndingsFromFile";
            this.btnLoadPageEndingsFromFile.Size = new System.Drawing.Size(102, 23);
            this.btnLoadPageEndingsFromFile.TabIndex = 3;
            this.btnLoadPageEndingsFromFile.Text = "Load from file";
            this.btnLoadPageEndingsFromFile.UseVisualStyleBackColor = true;
            this.btnLoadPageEndingsFromFile.Click += new System.EventHandler(this.btnLoadPageEndingsFromFile_Click);
            // 
            // btnTest
            // 
            this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTest.Location = new System.Drawing.Point(441, 348);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(89, 23);
            this.btnTest.TabIndex = 10;
            this.btnTest.Text = "Test regex";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // tabControlContainer
            // 
            this.tabControlContainer.Controls.Add(this.tpageParserSettings);
            this.tabControlContainer.Controls.Add(this.tpageParserTest);
            this.tabControlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlContainer.Location = new System.Drawing.Point(0, 0);
            this.tabControlContainer.Name = "tabControlContainer";
            this.tabControlContainer.SelectedIndex = 0;
            this.tabControlContainer.Size = new System.Drawing.Size(562, 416);
            this.tabControlContainer.TabIndex = 11;
            // 
            // tpageParserSettings
            // 
            this.tpageParserSettings.AutoScroll = true;
            this.tpageParserSettings.Controls.Add(this.containerAttributes);
            this.tpageParserSettings.Controls.Add(this.txtBaseAddress);
            this.tpageParserSettings.Controls.Add(this.lblBaseAddress);
            this.tpageParserSettings.Controls.Add(this.txtPageEndings);
            this.tpageParserSettings.Controls.Add(this.btnTest);
            this.tpageParserSettings.Controls.Add(this.lblPageEndings);
            this.tpageParserSettings.Controls.Add(this.btnLoadPageEndingsFromFile);
            this.tpageParserSettings.Location = new System.Drawing.Point(4, 22);
            this.tpageParserSettings.Name = "tpageParserSettings";
            this.tpageParserSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tpageParserSettings.Size = new System.Drawing.Size(554, 390);
            this.tpageParserSettings.TabIndex = 0;
            this.tpageParserSettings.Text = "Parser setting";
            this.tpageParserSettings.UseVisualStyleBackColor = true;
            // 
            // tpageParserTest
            // 
            this.tpageParserTest.Controls.Add(this.txtParserResult);
            this.tpageParserTest.Location = new System.Drawing.Point(4, 22);
            this.tpageParserTest.Name = "tpageParserTest";
            this.tpageParserTest.Padding = new System.Windows.Forms.Padding(3);
            this.tpageParserTest.Size = new System.Drawing.Size(554, 390);
            this.tpageParserTest.TabIndex = 1;
            this.tpageParserTest.Text = "Parser result";
            this.tpageParserTest.UseVisualStyleBackColor = true;
            // 
            // txtParserResult
            // 
            this.txtParserResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtParserResult.Location = new System.Drawing.Point(3, 3);
            this.txtParserResult.Multiline = true;
            this.txtParserResult.Name = "txtParserResult";
            this.txtParserResult.Size = new System.Drawing.Size(548, 384);
            this.txtParserResult.TabIndex = 0;
            // 
            // lblBaseAddress
            // 
            this.lblBaseAddress.AutoSize = true;
            this.lblBaseAddress.Location = new System.Drawing.Point(16, 16);
            this.lblBaseAddress.Name = "lblBaseAddress";
            this.lblBaseAddress.Size = new System.Drawing.Size(71, 13);
            this.lblBaseAddress.TabIndex = 11;
            this.lblBaseAddress.Text = "Base address";
            // 
            // txtBaseAddress
            // 
            this.txtBaseAddress.Location = new System.Drawing.Point(132, 13);
            this.txtBaseAddress.Name = "txtBaseAddress";
            this.txtBaseAddress.Size = new System.Drawing.Size(398, 20);
            this.txtBaseAddress.TabIndex = 12;
            // 
            // containerAttributes
            // 
            this.containerAttributes.Header = "Header";
            this.containerAttributes.Location = new System.Drawing.Point(6, 152);
            this.containerAttributes.Name = "containerAttributes";
            this.containerAttributes.Size = new System.Drawing.Size(392, 63);
            this.containerAttributes.TabIndex = 13;
            // 
            // BiblePageParser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 416);
            this.Controls.Add(this.tabControlContainer);
            this.MinimumSize = new System.Drawing.Size(570, 450);
            this.Name = "BiblePageParser";
            this.Text = "Omega tools";
            this.TranslationKey = "Omega tools";
            this.tabControlContainer.ResumeLayout(false);
            this.tpageParserSettings.ResumeLayout(false);
            this.tpageParserSettings.PerformLayout();
            this.tpageParserTest.ResumeLayout(false);
            this.tpageParserTest.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblPageEndings;
        private System.Windows.Forms.TextBox txtPageEndings;
        private System.Windows.Forms.OpenFileDialog dlgChoosePageEndingsDialog;
        private System.Windows.Forms.Button btnLoadPageEndingsFromFile;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.TabControl tabControlContainer;
        private System.Windows.Forms.TabPage tpageParserSettings;
        private System.Windows.Forms.TabPage tpageParserTest;
        private System.Windows.Forms.TextBox txtParserResult;
        private System.Windows.Forms.TextBox txtBaseAddress;
        private System.Windows.Forms.Label lblBaseAddress;
        private XmlAttributePanel containerAttributes;

    }
}

