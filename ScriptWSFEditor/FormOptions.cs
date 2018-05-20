using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScriptWSFEditor
{
    public partial class FormOptions : Form
    {
        public FormOptions()
        {
            InitializeComponent();
        }

        private void FormOptions_Load(object sender, EventArgs e)
        {
            txtKeywordsHighlight.Text = CommunSNCF.ReadRegParam(CommunSNCF.HKLMApplication, "KeyWordsHighLight");
            txtPathMethode.Text = CommunSNCF.ReadRegParam(CommunSNCF.HKLMApplication, "PathMethodes");
            txtSeparatorKeywords.Text = CommunSNCF.ReadRegParam(CommunSNCF.HKLMApplication, "KeyWordsSeparator");
            txtTemplateFile.Text = CommunSNCF.ReadRegParam(CommunSNCF.HKLMApplication, "Template");
            txtVBKeyWords.Text = CommunSNCF.ReadRegParam(CommunSNCF.HKLMApplication, "KeyWordsVB");
            txtWSFkeyWords.Text = CommunSNCF.ReadRegParam(CommunSNCF.HKLMApplication, "KeyWordsWSFScript");
            txtCmdLineForMDT.Text = CommunSNCF.ReadRegParam(CommunSNCF.HKLMApplication, "CmdLineForMDT");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FormMain fm = new FormMain();
            CommunSNCF.SaveRegParam("KeyWordsHighLight", txtKeywordsHighlight.Text);
            CommunSNCF.SaveRegParam("PathMethodes", txtPathMethode.Text);
            CommunSNCF.SaveRegParam("KeyWordsSeparator", txtSeparatorKeywords.Text);
            CommunSNCF.SaveRegParam("Template", txtTemplateFile.Text);
            CommunSNCF.SaveRegParam("KeyWordsVB", txtVBKeyWords.Text);
            CommunSNCF.SaveRegParam("KeyWordsWSFScript", txtWSFkeyWords.Text);
            CommunSNCF.SaveRegParam("CmdLineForMDT", txtCmdLineForMDT.Text);
            FormMain.regPathMethodes = CommunSNCF.ReadRegParam(CommunSNCF.HKLMApplication, "PathMethodes");
            FormMain.regTemplate = CommunSNCF.ReadRegParam(CommunSNCF.HKLMApplication, "Template");
            FormMain.regKWScript = CommunSNCF.ReadRegParam(CommunSNCF.HKLMApplication, "KeyWordsWSFScript");
            FormMain.regKWVB = CommunSNCF.ReadRegParam(CommunSNCF.HKLMApplication, "KeyWordsVB");
            FormMain.regKWSeparator = CommunSNCF.ReadRegParam(CommunSNCF.HKLMApplication, "KeyWordsSeparator");
            FormMain.regKWHigh = CommunSNCF.ReadRegParam(CommunSNCF.HKLMApplication, "KeyWordsHighLight");
            this.Close();
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ofd.ShowDialog();
            if(ofd.FileName != "")
            {
                txtTemplateFile.Text = ofd.FileName;
            }
        }

        private void btnCloseError_Click(object sender, EventArgs e)
        {
            if (fbd.ShowDialog() != DialogResult.Cancel && fbd.SelectedPath != "")
                txtPathMethode.Text = fbd.SelectedPath;
        }
    }
}
