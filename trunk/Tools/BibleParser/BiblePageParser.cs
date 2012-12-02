using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Omega.Tools.Utilities;
using Common.Core.Pattern;
using System.IO;
using System.Net;

namespace Omega.Tools.BibleParser
{
    public partial class BiblePageParser : Common.Controls.Forms.ExtendedForm
    {
        public BiblePageParser()
        {
            InitializeComponent();
#if DEBUG
            txtBaseAddress.Text = @"http://www.biblestudytools.com/nas/";
            txtPageEndings.Lines = new string[] { "genesis/1.html" };
#endif
        }

        private void btnLoadPageEndingsFromFile_Click(object sender, EventArgs e)
        {
            DialogResult result = dlgChoosePageEndingsDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                if (File.Exists(dlgChoosePageEndingsDialog.FileName))
                {
                    txtPageEndings.Lines = File.ReadAllLines(dlgChoosePageEndingsDialog.FileName);
                }
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(new Uri((string)txtBaseAddress.Text));
            myRequest.Method = "GET";
            WebResponse myResponse = myRequest.GetResponse();
            StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
            string content = sr.ReadToEnd();
            sr.Close();
            myResponse.Close();

            //TextParser parser = new TextParser(txtRegexContainer.Text, txtRegexVerse.Text, txtRegexWord.Text);
            //parser.Parse(content);

        }
    }
}
