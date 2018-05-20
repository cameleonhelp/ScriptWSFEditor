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
    public partial class FormNews : Form
    {
        public FormNews()
        {
            InitializeComponent();
        }

        private void FormNews_Load(object sender, EventArgs e)
        {
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Affichage des nouveautés");
            try
            {
                if (File.Exists(Path.Combine(Application.StartupPath, "changelog.txt")))
                    txtChangeLog.Text = File.ReadAllText(Path.Combine(Application.StartupPath, "changelog.txt"), Encoding.UTF8);
            } catch (Exception ex)
            {
                CommunSNCF.Log((int)CommunSNCF.logtype.Error, "Problème d'ouverture des nouveautés " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
