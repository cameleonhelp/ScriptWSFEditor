using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScriptWSFEditor
{
    public partial class FormSplashScreen : Form
    {
        public FormSplashScreen()
        {
            InitializeComponent();
            this.progressBar1.Maximum = 100;
        }

        private void FormSplashScreen_Load(object sender, EventArgs e)
        {
                var productInfo = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location);
                label1.Text = "Version : " + productInfo.FileVersion + " - " + productInfo.LegalCopyright;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
