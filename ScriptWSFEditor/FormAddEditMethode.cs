using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScriptWSFEditor
{
    public partial class FormAddEditMethode : Form
    {
        public FormAddEditMethode()
        {
            InitializeComponent();
        }

        private FormMain fm = null;

        public FormAddEditMethode(Form callingForm)
        {
            fm = callingForm as FormMain;
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Annulation de l'ajout ou modification de méthodes");
            fm = new FormMain();
            fm.TSMIRefreshMethodesList_Click(sender, e);
            this.Close();
            this.Dispose();
        }

        private void FormAddEditMethode_Load(object sender, EventArgs e)
        {
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Ouverture de l'éditeur de méthode");
            if (File.Exists(CommunSNCF.methodeFile))
                txtMethode.Text = File.ReadAllText(CommunSNCF.methodeFile, Encoding.UTF8);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Enregistrement de la méthode");
            FileInfo fi = new FileInfo(CommunSNCF.methodeFile);
            if (File.Exists(Path.Combine(this.fm.GetPathMethodes,fi.Name)))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(txtMethode.Text);
                File.WriteAllText(CommunSNCF.methodeFile, sb.ToString(), Encoding.UTF8);
                File.Copy(CommunSNCF.methodeFile, Path.Combine(this.fm.GetPathMethodes, fi.Name), true);
            }
            else
            {
                //Déplacer le fichier de tmp dans méthode
                File.Move(CommunSNCF.methodeFile, Path.Combine(this.fm.GetPathMethodes, fi.Name));
                //this.fm.ListBoxMethodes.Items.Add(fi.Name);
            }
            //CommunSNCF.FillListBoxFromDirectory(this.fm.ListBoxMethodes, this.fm.GetPathMethodes);
            fm.TSMIRefreshMethodesList_Click(sender, e);
            this.Close();
            this.Dispose();
        }

        private void FormAddEditMethode_FormClosed(object sender, FormClosedEventArgs e)
        {
            CommunSNCF.methodeFile = "";
        }

        private void txtMethode_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
