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
    public partial class FormPreview : Form
    {
        public FormPreview()
        {
            InitializeComponent();
        }

        private FormMain fm = null;

        public FormPreview(Form callingForm)
        {
            fm = callingForm as FormMain;
            InitializeComponent();
        }

        private void FormPreview_Load(object sender, EventArgs e)
        {
            if (File.Exists(CommunSNCF.scriptFile))
                txtPreview.Text = File.ReadAllText(CommunSNCF.scriptFile, Encoding.UTF8);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Annulation de l'aperçu");
            if (File.Exists(CommunSNCF.scriptFile))
            {
                FileInfo fi = new FileInfo(CommunSNCF.scriptFile);
                string path = fi.DirectoryName;
                Directory.Delete(path, true);
            }
            CommunSNCF.scriptFile = "";
            CommunSNCF.finalDestination = "";
            fm.DeletetempContent();
            FormMain.isNewFile = true;
            this.Close();
            this.Dispose();
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CommunSNCF.finalDestination != "") {
                //SaveFile(rtb);
                this.Close();
                this.Dispose();
            } else
            {
                FormMain fm = new FormMain();
                SaveFileDialog sfd = new SaveFileDialog();
                //sfd.InitialDirectory = fm.GetTxtFolderPath;
                //sfd.FileName = fm.GetTxtFileName + ".wsf";
                sfd.Filter = "Fichiers wsf (*.wsf)|*.wsf|Tous les fichiers (*.*)|*.*|Fichiers text (*.txt) | *.txt|Fichiers vbs (*.vbs) | *.vbs";
                sfd.Title = "Enregistrer le script sous ...";
                if (sfd.ShowDialog() != DialogResult.Cancel)
                {

                    // If the file name is not an empty string open it for saving.  
                    if (sfd.FileName != "")
                    {
                        //fm.CreateTempFile();
                        FormPreview fp = new FormPreview();
                        FileInfo fi = new FileInfo(sfd.FileName);
                        CommunSNCF.finalDestination = fi.DirectoryName;
                        //fp.SaveFileAs(fi.Name);
                        fm.DeletetempContent();
                    }
                } else
                {
                    fm.DeletetempContent();
                }
            }
            FormMain.isNewFile = true;
        }
    }
}

