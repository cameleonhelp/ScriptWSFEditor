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
    public partial class frmInformation : Form
    {
        public frmInformation()
        {
            InitializeComponent();
        }

        private void frmInformation_Load(object sender, EventArgs e)
        {
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Ajout d'information au script");
            lblDescr.Text = lblDescr.Text.Replace("xxx", CommunSNCF.pbMethode);
            if (CommunSNCF.pbAvancement >= 100)
                CommunSNCF.pbAvancement = CommunSNCF.pbAvancement - 100;
            nAvancement.Value += CommunSNCF.pbAvancement;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Annulation des informations et de l'aperçu du script");
            CommunSNCF.pbInformation = null;
            CommunSNCF.pbAvancement = -1;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtInformation.Text != "" && nAvancement.Value != 0)
            {
                CommunSNCF.formInformationValid = true;
                CommunSNCF.pbInformation = txtInformation.Text;
                CommunSNCF.pbAvancement = nAvancement.Value;
                this.Close();
            } else
            {
                CommunSNCF.formInformationValid = false;
                string msg = "Ces information sont indispensables,\r\nvous devez renseigner ces valeurs.";
                string titre = "Informations inexactes";
                MessageBox.Show(msg, titre, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Annulation des informations et de l'aperçu du script");
            this.Close();
            this.Dispose();
        }
    }
}
