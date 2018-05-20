using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace ScriptWSFEditor
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
        }

        private void btnCloseAbout_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void FormAbout_Load(object sender, EventArgs e)
        {
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Affichage de la boite a propos");
            var productInfo = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location);
            label1.Text = "Version : " + productInfo.FileVersion + " - " + productInfo.LegalCopyright;
        }

        private void FormAbout_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
