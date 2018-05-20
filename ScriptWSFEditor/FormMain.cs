using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Reflection;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text.RegularExpressions;

namespace ScriptWSFEditor
{
    public partial class FormMain : Form
    {
        [DllImport("user32.dll")] // import lockwindow to remove flashing
        public static extern bool LockWindowUpdate(IntPtr hWndLock);

        //variables globales
        private Thread t;
        public static string logfile = @"C:\e.SNCF\logs\" + Application.ProductName + ".log";
        public static string pathtmp = Path.Combine(Application.StartupPath, "tmp");
        public static string conditionCheked = "";
        public static bool isNewFile = true;
        public static string _title = "";
        public static FileVersionInfo productInfo = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location);

        //Boolean for enable menu if is not empty
        public static bool pathFolderIsEmpty = true;
        public static bool methodeUsedIsEmpty = true;
        public static bool variableUsedIsEmpty = true;
        public static bool runUsedIsEmpty = true;

        public static string regPathMethodes = CommunSNCF.ReadRegParam(CommunSNCF.HKLMApplication, "PathMethodes");
        public static string regTemplate = CommunSNCF.ReadRegParam(CommunSNCF.HKLMApplication, "Template");
        public static string regKWScript = CommunSNCF.ReadRegParam(CommunSNCF.HKLMApplication, "KeyWordsWSFScript");
        public static string regKWVB = CommunSNCF.ReadRegParam(CommunSNCF.HKLMApplication, "KeyWordsVB");
        public static string regKWSeparator = CommunSNCF.ReadRegParam(CommunSNCF.HKLMApplication, "KeyWordsSeparator");
        public static string regKWHigh = CommunSNCF.ReadRegParam(CommunSNCF.HKLMApplication, "KeyWordsHighLight");

        public static string pathMethodes = regPathMethodes;

        public static int index = 0;

        //Declaration du document pour imprimer
        private System.Drawing.Printing.PrintDocument docToPrint = new System.Drawing.Printing.PrintDocument();

        public FormMain()
        {
            t = new Thread(new ThreadStart(CommunSNCF.StartSplashScreen));
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            if (File.Exists(logfile))
                File.Delete(logfile);
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Chargement de ScriptWSFEditor");
            t.Start();
            try
            {
                Application.DoEvents();
                Thread.Sleep(1500);
                CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Création du dossier Methodes si inexistant");
                if (!Directory.Exists(regPathMethodes))
                    Directory.CreateDirectory(regPathMethodes);
                CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Création du dossier de travail Tmp si inexistant");
                if (!Directory.Exists(pathtmp))
                    Directory.CreateDirectory(pathtmp);
                CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Initialisation de la base de registre avec les Methodes et les Functions");
                MyFunctions mf = new MyFunctions();
                mf.RefreshSubListInRegistry(regPathMethodes);
                mf.RefreshAllMethodeListInRegistry(regPathMethodes);
                Thread.Sleep(10);
                CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Remplissage de la liste des Methodes");
                CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Remplissage des conditions avec vidage");
                InsertConditionsInMenu(true);
                InsertMethodesInMenu(true);
                Thread.Sleep(10);
                CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Récupération du dernier dossier utilisé");
                Thread.Sleep(10);
            }
            catch (Exception ex)
            {
                CommunSNCF.Log((int)CommunSNCF.logtype.Error, "Chargement de ScriptWSFEditor " + ex.Message);
            }
            finally
            {
                t.Abort();


                CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Déclenchement des timers");
                timer1.Interval = (1000) * (60);              // Timer will tick evert second
                timer1.Enabled = true;                       // Enable the timer
                timer1.Start();
                timer2.Interval = (1000) * (15);              // Timer will tick evert 1/4 second
                timer2.Enabled = true;                       // Enable the timer
                timer2.Start();
            }
        }

        public void InsertConditionsInMenu(bool emptyBefore)
        {
            try
            {
                ToolStripMenuItem mnConditions = menuStrip1.Items[5] as ToolStripMenuItem;
                ToolStripMenuItem mnConditionsList = mnConditions.DropDownItems[6] as ToolStripMenuItem;
                if (emptyBefore)
                    mnConditionsList.DropDownItems.Clear();
                List<string> conditions = CommunSNCF.ReadRegAllSubkeys();
                if (conditions.Count > 0)
                {
                    conditions.Sort();
                    foreach (string subkey in conditions)
                    {
                        var submenu = new ToolStripMenuItem();
                        mnConditionsList.DropDownItems.Add(submenu);
                        submenu.Name = "TSMI" + subkey;
                        submenu.Text = subkey;
                        EventArgs e = new EventArgs();
                        submenu.Click += new System.EventHandler(OnSubmenuClick);
                    }
                    TSMIEditCondition.Enabled = true;
                }
                else
                {
                    var submenu = new ToolStripMenuItem();
                    mnConditionsList.DropDownItems.Add(submenu);
                    submenu.Name = "TSMIEmpty";
                    submenu.Text = "Liste vide";
                    submenu.Enabled = false;
                    TSMIEditCondition.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                CommunSNCF.Log((int)CommunSNCF.logtype.Error, "Erreur lors du chargement des conditions dans le menu. " + ex.Message);
            }
        }

        public void InsertMethodesInMenu(bool emptyBefore)
        {
            try
            {
                ToolStripMenuItem mnMethodes = menuStrip1.Items[2] as ToolStripMenuItem;
                ToolStripMenuItem mnMethodesList = mnMethodes.DropDownItems[5] as ToolStripMenuItem;
                ToolStripMenuItem mnModifyMethode = mnMethodes.DropDownItems[2] as ToolStripMenuItem;
                ToolStripMenuItem mnDeleteMethode = mnMethodes.DropDownItems[3] as ToolStripMenuItem;
                if (emptyBefore)
                {
                    mnMethodesList.DropDownItems.Clear();
                    mnModifyMethode.DropDownItems.Clear();
                    mnDeleteMethode.DropDownItems.Clear();
                }
                List<string> fileEntries = Directory.GetFiles(regPathMethodes).ToList<string>();
                if (fileEntries.Count > 0)
                {
                    fileEntries.Sort();
                    foreach (string subkey in fileEntries)
                    {
                        var submenu = new ToolStripMenuItem();
                        var submenuModify = new ToolStripMenuItem();
                        var submenuDelete = new ToolStripMenuItem();
                        mnMethodesList.DropDownItems.Add(submenu);
                        mnModifyMethode.DropDownItems.Add(submenuModify);
                        mnDeleteMethode.DropDownItems.Add(submenuDelete);
                        FileInfo fi = new FileInfo(subkey);
                        submenu.Name = "TSMI" + fi.Name;
                        submenu.Text = fi.Name;
                        submenuModify.Text = fi.Name;
                        submenuDelete.Text = fi.Name;
                        EventArgs e = new EventArgs();
                        submenu.Click += new System.EventHandler(OnMethodeClick);
                        submenuModify.Click += new System.EventHandler(OnModifyMethodeClick);
                        submenuDelete.Click += new System.EventHandler(OnDeleteMethodeClick);
                        submenu.CheckOnClick = true;
                    }
                }
                else
                {
                    var submenu = new ToolStripMenuItem();
                    mnMethodesList.DropDownItems.Add(submenu);
                    var submenuModify = new ToolStripMenuItem();
                    var submenuDelete = new ToolStripMenuItem();
                    submenu.Name = "TSMIEmpty";
                    submenu.Text = "Liste vide";
                    submenuModify.Text = "Liste vide";
                    submenuDelete.Text = "Liste vide";
                    submenuModify.Enabled = false;
                    submenuDelete.Enabled = false;
                    submenu.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                CommunSNCF.Log((int)CommunSNCF.logtype.Error, "Erreur lors du chargement des methodes dans le menu. " + ex.Message);
            }
        }

        public void OnModifyMethodeClick(object sender, System.EventArgs e) {
            string val = sender.ToString();
            CommunSNCF.methodeFile = Path.Combine(regPathMethodes, val);
            FormAddEditMethode faem = new FormAddEditMethode(this);
            faem.Show();
        }

        public void OnDeleteMethodeClick(object sender, System.EventArgs e) {
            string val = sender.ToString();
            string sMsg = "Confirmer la suppresssion de la méthode " + val;
            string titre = "Confirmation";
            if(MessageBox.Show(sMsg,titre,MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                File.Delete(Path.Combine(regPathMethodes,val));
            }
            TSMIRefreshMethodesList_Click(sender, e);
        }

        public void checkMethodeSubMenu(string file, bool isrefresh)
        {
            if (file != "")
            {
                MyFunctions mf = new MyFunctions();
                string[] methodes = mf.FindAllMethodesInOldFile(file, true, isrefresh);
                ToolStripMenuItem mnMethodes = menuStrip1.Items[2] as ToolStripMenuItem;
                ToolStripMenuItem mnMethodesList = mnMethodes.DropDownItems[5] as ToolStripMenuItem;
                ToolStripItemCollection submenuMethodes = mnMethodesList.DropDownItems;
                foreach (ToolStripMenuItem item in submenuMethodes)
                {
                    if (Array.IndexOf(methodes, item.ToString()) > -1)
                    {
                        item.Checked = true;
                    }
                }
            }
        }

        uint howManyTabs(string s)
        {
            return (uint)s.Count(ch => ch == '\t');
        }

        public void OnMethodeClick(object sender, System.EventArgs e)
        {
            try
            {
                string val = sender.ToString();
                ToolStripMenuItem item = (ToolStripMenuItem)sender;
                //item.Enabled = false;
                CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Click sur la methode " + val);
                string pathfile = Path.Combine(regPathMethodes, val);
                if (File.Exists(pathfile))
                {
                    if (item.Checked)
                    {
                        string[] lines = File.ReadAllLines(pathfile);
                        //Ajout de la méthode sélectionnée dans la bloc METHODES
                        int index = CommunSNCF.GetLineIndexContains(rtbx, "==METHODES==") + 1;
                        //index = rtbx.GetLineFromCharIndex(rtbx.SelectionStart);
                        List<string> tmpRTBX = rtbx.Lines.ToList<string>();
                        tmpRTBX.Insert(index, "");
                        index++;
                        foreach (string line in lines)
                        {
                            if (!line.StartsWith("#"))
                            {
                                tmpRTBX.Insert(index, line);
                                index++;
                            }
                        }

                        //Affichage dans le control richTextBox
                        rtbx.Text = "";
                        rtbx.Lines = tmpRTBX.ToArray();
                    }
                    //insertion des variables et de l'exécution
                    string methode = CommunSNCF.ReadFirstLineFile(pathfile);
                    string[] variables = Array.Empty<string>();
                    if (methode.Contains("Sub "))
                    {
                        methode = methode.Replace("Sub ", "");
                        variables = methode.Substring(methode.IndexOf('(') + 1, methode.IndexOf(')') - (methode.IndexOf('(') + 1)).Split(',');
                    }
                    else
                    {
                        methode = methode.Replace("Function ", "");
                        variables = methode.Substring(methode.IndexOf('(') + 1, methode.IndexOf(')') - (methode.IndexOf('(') + 1)).Split(',');
                        methode = "ZTIProcess=" + methode;
                    }
                    index = 0;
                    List<string> tmpresult = rtbx.Lines.ToList<string>();
                    foreach (string line in rtbx.Lines)
                    {
                        if (line.Contains("==FIN TORUN=="))
                        {
                            uint i = howManyTabs(line);
                            string indentation = new String('\t', (int)i);
                            index--;
                            frmInformation fi = new frmInformation();
                            string msg = "[A RENSEIGNER]";
                            string pourcent = "[POURCENTAGE]";
                            string maxpourcent = "[POURCENTAGE]";
                            CommunSNCF.pbMethode = methode;
                            CommunSNCF.pbAvancement = 0;
                            if (fi.ShowDialog() == DialogResult.OK)
                            {
                                msg = "\"" +CommunSNCF.pbInformation + "\"";
                                if (CommunSNCF.pbAvancement != 0)
                                {
                                    pourcent = CommunSNCF.pbAvancement.ToString();
                                    maxpourcent = (CommunSNCF.pbAvancement + 1).ToString();
                                }
                                if (Convert.ToInt32(maxpourcent) > 100)
                                    maxpourcent = "100";
                            }
                            tmpresult.Insert(index, indentation + "oLogging.ReportProgress " + msg + "," + (Convert.ToInt32(pourcent) + 1).ToString());
                            tmpresult.Insert(index, indentation + methode);
                            tmpresult.Insert(index, indentation + "oLogging.ReportProgress " + msg + "," + pourcent);
                            tmpresult.Insert(index, indentation + "oLogging.CreateEntry oUtility.ScriptName & \":\" & " + msg + ", LogTypeInfo");
                            foreach (string var in variables)
                            {
                                string sMsg = "Saisir le contenu de la variable " + var;
                                string titre = "Renseignement de la variable";
                                string asDefault = "[A RENSEIGNER]";
                                string input = Microsoft.VisualBasic.Interaction.InputBox(sMsg, titre, asDefault, -1, -1);
                                tmpresult.Insert(index, indentation + var + "=\"" + input + "\"");
                            }
                            tmpresult.Insert(index, indentation + "sMsg=" + msg);
                            tmpresult.Insert(index, indentation + "'-----" + msg + "-----");
                            tmpresult.Insert(index, "");
                        }
                        if (line.Contains("Dim sSource,sIniSrc"))
                        {
                            string tmpline = line;
                            foreach (string var in variables)
                            {
                                if (!tmpline.Contains(var))
                                    tmpline += "," + var;
                            }
                            tmpresult[index] = tmpline;
                        }
                        index++;
                    }
                    rtbx.Text = "";
                    rtbx.Lines = tmpresult.ToArray();
                    item.Checked = true;
                } else
                {
                    string sMsg = "La méthode " + val + " est introuvable.";
                    string titre = "Méthode non trouvée";
                    MessageBox.Show(sMsg, titre, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    InsertMethodesInMenu(true);
                }
            }
            catch (Exception ex)
            {
                CommunSNCF.Log((int)CommunSNCF.logtype.Error, "Erreur lors du click sur une condition. " + ex.Message);
            }
            TSSLBToSave.Visible = true;
        }

        public void OnSubmenuClick(object sender, System.EventArgs e)
        {
            try
            {
                string val = sender.ToString();
                CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Click sur la condition " + val);
                string toinsert = CommunSNCF.ReadRegParam(CommunSNCF.HKLMApplication + "\\Conditions\\", val);

                index = 0;
                List<string> tmpresult = rtbx.Lines.ToList<string>();
                foreach (string line in rtbx.Lines)
                {
                    if (line.Contains("==FIN TORUN=="))
                    {
                        uint i = howManyTabs(line);
                        string indentation = new String('\t', (int)i);
                        index--;
                        tmpresult.Insert(index, indentation + toinsert.Replace("|", "\r\n" + indentation));
                        tmpresult.Insert(index, indentation  +" ");
                    }
                    index++;
                }
                rtbx.Text = "";
                rtbx.Lines = tmpresult.ToArray();

            }
            catch (Exception ex)
            {
                CommunSNCF.Log((int)CommunSNCF.logtype.Error, "Erreur lors du click sur une condition. " + ex.Message);
            }
            TSSLBToSave.Visible = true;
        }

        public string titleForm
        {
            get { return this.Text; }
            set {
                    this.Text = _title;
                    this.Refresh();
                }              
        }

        public string GetPathMethodes
        {
            get { return regPathMethodes; }
        }

        public string GetTempPath
        {
            get { return pathtmp; }
        }

        public string openFile { get; private set; }

        public static bool IsClipboardEmpty()
        {
            var dataFormats = typeof(DataFormats).GetFields(BindingFlags.Public | BindingFlags.Static)
                               .Select(f => f.Name);
            var containsSomething = dataFormats.Any(x => Clipboard.ContainsData(x));

            return (!containsSomething);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                MyFunctions mf = new MyFunctions();
                mf.RefreshSubListInRegistry(regPathMethodes);
                mf.RefreshAllMethodeListInRegistry(regPathMethodes);
            }
            catch (Exception ex)
            {
                ex.GetType();
            }
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            Thread.Sleep(5);
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Chargement aprés ouverture");
            if (File.Exists(Path.Combine(Application.StartupPath, "changelog.txt")))
            {
                if (CommunSNCF.ReadRegParam("Version").Trim() != CommunSNCF.ReadFirstLineFile(Path.Combine(Application.StartupPath, "changelog.txt")).Trim())
                {
                    FormNews fn = new FormNews();
                    fn.ShowDialog();
                }
            }
            CommunSNCF.SaveRegParam("Version", ProductVersion);
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Nettoyage de sauvegarde avant fermeture de ScriptWSFEditor");
            DeletetempContent();
            CommunSNCF.SaveRegParam("currentfile", "");
            CommunSNCF.SaveRegParam("AllMethodes", "");
            CommunSNCF.SaveRegParam("SubList", "");
            CommunSNCF.SaveRegParam("AppsPath", "");    
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Fermeture de ScriptWSFEditor");
        }

        private void TSMIClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void TSMINew_Click(object sender, EventArgs e)
        {
            if (regTemplate != "")
            {
                CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Creation d'un nouveau script");
                CommunSNCF.SaveRegParam("currentfile", "");
                isNewFile = true;
                List<string> tmp = File.ReadAllLines(regTemplate).ToList<string>();
                string sMsg = "Saisir l'identifiant du job :";
                string titre = "Identifiant du job";
                string asDefault = "install-[A RENSEIGNER]";
                string input = Microsoft.VisualBasic.Interaction.InputBox(sMsg, titre, asDefault, -1, -1);
                if (input != "")
                    tmp[0] = tmp[0].Replace("id=\"[A RENSEIGNER]\"", "id=\"" + input + "\"");
                CommunSNCF.jobid = input;
                int index = CommunSNCF.GetLineIndexContains(tmp, "</script>");
                tmp.Insert(index, "'##### Generated with " + Application.ProductName + " - " + Application.ProductVersion + " - " + productInfo.LegalCopyright + "#####");
                rtbx.Text = "";
                rtbx.Lines = tmp.ToArray();
                this.Text = "Editeur de scripts WSF" + " : Nouveau script";
                CommunSNCF.scriptFile = "";
                TSMICleanScript.Enabled = false;
            } else
            {
                string sMsg = "Vous devez indiquer le template à utiliser dans les options du menu Edition.";
                string titre = "Options non conforme";
                MessageBox.Show(sMsg, titre, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TSMICopy_Click(object sender, EventArgs e)
        {
            if (rtbx.SelectedText != "")
                Clipboard.SetDataObject(rtbx.SelectedText);
        }

        private void TSMIOpen_Click(object sender, EventArgs e)
        {
            if (regPathMethodes != "")
            {
                isNewFile = false;
                MyFunctions mf = new MyFunctions();
                mf.RefreshSubListInRegistry(regPathMethodes);
                mf.RefreshAllMethodeListInRegistry(regPathMethodes);
                OpenFileDialog ofd = new OpenFileDialog();
                Stream myStream = null;
                string openfile = null;
                ofd.Filter = "Fichiers wsf (*.wsf)|*.wsf|Tous les fichiers (*.*)|*.*|Fichiers text (*.txt) | *.txt|Fichiers vbs (*.vbs) | *.vbs";
                ofd.FilterIndex = 1;
                ofd.RestoreDirectory = true;
                CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Fermeture de ScriptWSFEditor");
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    TSMICleanScript.Enabled = true;
                    if ((myStream = ofd.OpenFile()) != null)
                    {
                        FileStream fs = myStream as FileStream;
                        if (fs != null)
                        {
                            openfile = fs.Name;
                            this.Text = "Editeur de scripts WSF" + " : " + openfile;
                            CommunSNCF.scriptFile = openfile;
                        }
                        rtbx.Text = "";
                        rtbx.Lines = File.ReadAllLines(openfile);
                        checkMethodeSubMenu(openfile, false);
                        TSMIRefreshMethodesList_Click(sender, e);
                    }
                }
            } else
            {
                string sMsg = "Vous devez indiquer le chemin des méthodes à utiliser dans les options du menu Edition.";
                string titre = "Options non conforme";
                MessageBox.Show(sMsg, titre, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void TSMIRefreshMethodesList_Click(object sender, EventArgs e)
        {
            if (regPathMethodes != "")
            {
                //Rafraichi la liste des méthodes et les informations en base de donnée
                MyFunctions mf = new MyFunctions();
                mf.RefreshSubListInRegistry(regPathMethodes);
                mf.RefreshAllMethodeListInRegistry(regPathMethodes);
                InsertMethodesInMenu(true);
                checkMethodeSubMenu(CommunSNCF.scriptFile, true);
            } else
            {
                string sMsg = "Vous devez indiquer le chemin des méthodes à utiliser dans les options du menu Edition.";
                string titre = "Options non conforme";
                MessageBox.Show(sMsg, titre, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TSMIAddMethode_Click(object sender, EventArgs e)
        {
            string msg = "Indiquer le nom de la méthode que vous voulez ajouter.";
            string titre = "Nouvelle méthode";
            string newMethode = Interaction.InputBox(msg, titre, "Nouvelle Méthode", -1, -1);
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Ajout de la méthode " + newMethode);
            if (newMethode != "")
            {
                string tmpNewMethode = Path.Combine(pathtmp, newMethode);
                string path = Path.Combine(regPathMethodes, newMethode);
                CommunSNCF.scriptFile = tmpNewMethode;
                if (!File.Exists(tmpNewMethode))
                {
                    using (var tw = new StreamWriter(tmpNewMethode, true))
                    {
                        tw.WriteLine("Function " + newMethode + "()");
                        tw.WriteLine("\t'Ajouter votre code ici");
                        tw.WriteLine("\t");
                        tw.WriteLine("End Function");
                        tw.WriteLine("#Méthode générée avec " + Application.ProductName + " - " + ProductVersion);
                        tw.WriteLine("#Ajouter ci-dessous les méthodes que vous avez utilisées dans votre code en commençant par #D:<Nom de la méthode>");
                        //[ADDNEWFONCTIONNALITY] si necessaire ajouter une ligne pour la prise en charge des conditions en exemple si besoin
                    }
                }
                FormAddEditMethode faem = new FormAddEditMethode(this);
                faem.Text = "Nouvelle méthode";
                faem.Show();
            }
        }

        private void TSMIAbout_Click(object sender, EventArgs e)
        {
            FormAbout fa = new FormAbout();
            fa.ShowDialog();
        }

        private void TSMIAddCondition_Click(object sender, EventArgs e)
        {
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Ajout d'une nouvelle condition");
            CommunSNCF.IsNewCondition = true;
            FormCondition fc = new FormCondition();
            fc.ShowDialog();
        }

        private void TSMISave_Click(object sender, EventArgs e)
        {
            MyFunctions mf = new MyFunctions();
            if (isNewFile || CommunSNCF.scriptFile == "")
            {
                mf.SaveFileAs(rtbx.Lines);
            } else
            {
                mf.SaveFile(rtbx.Lines);
            }
            isNewFile = false;
            if(CommunSNCF.scriptFile != "")
                this.Text = "Editeur de scripts WSF" + " : " + CommunSNCF.scriptFile;
            else
                this.Text = "Editeur de scripts WSF" + " : " + "Nouveau script";
            TSSLBToSave.Visible = false;
        }

        public void DeletetempContent()
        {
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Suppression du contenu du dossier de travail temporaire");
            if (Directory.Exists(pathtmp))
            {
                System.IO.DirectoryInfo di = new DirectoryInfo(pathtmp);

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }
            }
        }

        private void TSMISaveAs_Click(object sender, EventArgs e)
        {
            MyFunctions mf = new MyFunctions();
            mf.SaveFileAs(rtbx.Lines);
            isNewFile = false;
            if (CommunSNCF.scriptFile != "")
                this.Text = "Editeur de scripts WSF" + " : " + CommunSNCF.scriptFile;
            else
                this.Text = "Editeur de scripts WSF" + " : " + "Nouveau script";
            TSSLBToSave.Visible = false;
        }

        private void TSMIPrint_Click(object sender, EventArgs e)
        {
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Impression du script");
            try
            {
                CreateTemptxtFile();
                PrintDialog printDlg = new PrintDialog();
                if (printDlg.ShowDialog() == DialogResult.OK)
                {
                    //path is your documents to print location 
                    ProcessStartInfo info = new ProcessStartInfo(CommunSNCF.scriptFile.Replace(".wsf", ".txt"));
                    info.Arguments = "\"" + printDlg.PrinterSettings.PrinterName + "\"";
                    info.CreateNoWindow = true;
                    info.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    info.UseShellExecute = true;
                    info.Verb = "PrintTo";
                    System.Diagnostics.Process.Start(info).WaitForExit();
                }
            }
            catch (Exception ex)
            {
                CommunSNCF.Log((int)CommunSNCF.logtype.Error, "Echec d'impression du script " + ex.Message);
            }
            finally
            {
                CommunSNCF.scriptFile = "";
                DeletetempContent();
            }
        }

        private void CreateTemptxtFile()
        {
            if(CommunSNCF.scriptFile == "")
            {
                MyFunctions mf = new MyFunctions();
                mf.CreateScript(pathtmp, Path.Combine(pathtmp, "_tmp.txt"), rtbx.Lines);
                CommunSNCF.scriptFile = Path.Combine(pathtmp, "_tmp.txt");
            } 
        }

        private void TSMICut_Click(object sender, EventArgs e)
        {
            if (rtbx.SelectedText != "")
                Clipboard.SetDataObject(rtbx.SelectedText);
            rtbx.SelectedText = "";
        }

        private void TSMIPaste_Click(object sender, EventArgs e)
        {
            IDataObject iData = Clipboard.GetDataObject();
            if (iData.GetDataPresent(DataFormats.Text))
            {
                rtbx.SelectedText = (String)iData.GetData(DataFormats.Text);
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (IsClipboardEmpty())
            {
                TSMIPaste.Enabled = false;
                CMS_TSMI_Paste.Enabled = false;
                TSMIEmptyClipboard.Enabled = false;
                TSSLBClipboard.Visible = false;
            }
            else { 
                TSMIPaste.Enabled = true;
                CMS_TSMI_Paste.Enabled = true;
                TSMIEmptyClipboard.Enabled = true;
                TSSLBClipboard.Visible = true;
            }
        }

        private void TSMIEmptyClipboard_Click(object sender, EventArgs e)
        {
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Vidage du presse-papier");
            Clipboard.Clear();
            TSMIPaste.Enabled = false;
            TSMIEmptyClipboard.Enabled = false;
            TSSLBClipboard.Visible = false;
        }

        private void TSMISavePDF_Click(object sender, EventArgs e)
        {
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Enregistrement au format PDF");
            Document doc = new Document(iTextSharp.text.PageSize.A4);
            CreateTemptxtFile();
            var text = File.ReadAllText(CommunSNCF.scriptFile);
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = CommunSNCF.scriptFile.Replace(".txt","").Replace(".wsf","") + ".pdf";
            sfd.Filter = "Fichiers pdf (*.pdf)|*.pdf";
            sfd.Title = "Enregistrer le script en PDF sous ...";
            if (sfd.ShowDialog() != DialogResult.Cancel)
            {
                PdfWriter pdfw = PdfWriter.GetInstance(doc, new FileStream(sfd.FileName, FileMode.Create));
                doc.Open();
                Paragraph p = new Paragraph(text);
                doc.AddCreator(Application.ProductName + " Version : " + Application.ProductVersion);
                doc.AddTitle("Script WSF " + sfd.FileName.Replace(".pdf",""));
                doc.Add(p);
                doc.Close();
            }
            CommunSNCF.scriptFile = "";
            DeletetempContent();
        }

        private void TSMIImportConditionsReg_Click(object sender, EventArgs e)
        {
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Import des conditions au format REG");
            OpenFileDialog ofd = new OpenFileDialog();
            //ofd.InitialDirectory = txtFolderApps.Text;
            ofd.Filter = "Fichiers reg (*.reg)|*.reg";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string file = ofd.FileName;
                int exitcode = 0;
                string result = CommunSNCF.ExecuteCmdCommand("REG IMPORT \"" + file + "\"", ref exitcode);
                TSMIRefreshConditionsList_Click(sender, e);
                if (exitcode != 0)
                {
                    string msg = "Erreur : " + exitcode.ToString() + "\r\n" + result;
                    string titre = "Erreur d'exportation";
                    MessageBox.Show(msg, titre, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void TSMIExportConditionsAs_Click(object sender, EventArgs e)
        {
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Export des conditions au format REG");
            SaveFileDialog sfd = new SaveFileDialog();
            //sfd.InitialDirectory = txtFolderApps.Text;
            sfd.FileName = "Export-conditions.reg";
            sfd.Filter = "Fichiers reg (*.reg)|*.reg";
            sfd.Title = "Exporter les conditions sous ...";
            if (sfd.ShowDialog() != DialogResult.Cancel)
            {
                string path = sfd.FileName;
                string key = CommunSNCF.HKLMApplication + "\\Conditions";
                int exitcode = 0;
                string result = CommunSNCF.ExecuteCmdCommand("REG EXPORT \"" + key + "\" \"" + path + "\" /y", ref exitcode);
                if (exitcode != 0)
                {
                    string msg = "Erreur : " + exitcode.ToString() + "\r\n" + result;
                    string titre = "Erreur d'exportation";
                    MessageBox.Show(msg, titre, MessageBoxButtons.OK, MessageBoxIcon.Error);
                } else
                {
                    CommunSNCF.AddLineAppendFile(sfd.FileName, "");
                    CommunSNCF.AddLineAppendFile(sfd.FileName,@"[HKEY_CURRENT_USER\SOFTWARE\e.SNCF\ScriptWSFEditor\Settings]");
                    CommunSNCF.AddLineAppendFile(sfd.FileName, "\"ConditionsKeyWords\"=\"" + CommunSNCF.ReadRegParam("ConditionsKeyWords") + "\"");
                }
            }
        }

        private void TSMIEditCondition_Click(object sender, EventArgs e)
        {
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Modification de conditions");
            CommunSNCF.IsNewCondition = false;
            FormCondition fc = new FormCondition();
            fc.ShowDialog();
        }

        private void TSMIRefreshConditionsList_Click(object sender, EventArgs e)
        {
            InsertConditionsInMenu(true);
        }

        private void TSMIOptions_Click(object sender, EventArgs e)
        {
            FormOptions fo = new FormOptions();
            if (fo.ShowDialog() == DialogResult.OK)
            {
                ReloadTmpFile();
            }
        }

        public void RTBXRefresh()
        {
            string[] keysScript = regKWScript.Split(',');
            HighlightText(rtbx, keysScript, Color.DarkRed);

            string[] keysVB = regKWVB.Split(',');
            HighlightText(rtbx, keysVB, Color.Blue);

            string[] keysSeparator = regKWSeparator.Split(',');
            HighlightText(rtbx, keysSeparator, Color.Blue);

            string[] keysInteger = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            HighlightText(rtbx, keysInteger, Color.DarkRed);

            string[] keysSurbrillance = regKWHigh.Split(',');
            HighlightText(rtbx, keysSurbrillance, Color.Red, Color.Yellow);

            string[] keysComment = { "'" };
            HighlightLineStartWith(rtbx, keysComment);
        }

        public void HighlightText(RichTextBox rtb, string[] words, Color fontcolor, Color backcolor)
        {
            try
            {
                LockWindowUpdate(rtb.Handle);

                label1.Focus();
                foreach (string word in words)
                {
                    int s_start = rtb.SelectionStart, startIndex = 0, idx;

                    while ((idx = rtb.Text.IndexOf(word, startIndex)) != -1)
                    {
                        rtb.Select(idx, word.Length);
                        rtb.SelectionColor = fontcolor;
                        rtb.SelectionBackColor = backcolor;

                        startIndex = idx + word.Length;
                    }

                    rtb.SelectionStart = s_start;
                    rtb.SelectionLength = 0;
                    rtb.SelectionColor = Color.Black;
                    rtb.ScrollToCaret();
                }
                rtb.Focus();
            }
            finally { LockWindowUpdate(IntPtr.Zero); }

        }

        public void HighlightText(RichTextBox rtb, string[] words, Color color)
        {
            try
            {
                LockWindowUpdate(rtb.Handle);

                label1.Focus();
                foreach (string word in words)
                {
                    int s_start = rtb.SelectionStart, startIndex = 0, idx;

                    while ((idx = rtb.Text.IndexOf(word, startIndex)) != -1)
                    {
                        rtb.Select(idx, word.Length);
                        rtb.SelectionColor = color;

                        startIndex = idx + word.Length;
                    }

                    rtb.SelectionStart = s_start;
                    rtb.SelectionLength = 0;
                    rtb.SelectionColor = Color.Black;
                    rtb.ScrollToCaret();
                }
                rtb.Focus();
            }
            finally { LockWindowUpdate(IntPtr.Zero); }

        }

        public void ReloadTmpFile()
        {
            MyFunctions mf = new MyFunctions();
            mf.CreateScript(pathtmp, Path.Combine(pathtmp, "_tmp.txt"), rtbx.Lines);
            List<string> tmp = File.ReadAllLines(Path.Combine(pathtmp, "_tmp.txt")).ToList<string>();
            rtbx.Text = "";
            rtbx.Clear();
            rtbx.Lines = tmp.ToArray();
            DeletetempContent();
        }
        public void DisabledHighLightText()
        {
            ReloadTmpFile();
        }

        public void HighlightLineStartWith(RichTextBox rtb, string[] words)
        {
            try
            {
                LockWindowUpdate(rtb.Handle);

                label1.Focus();
                foreach (string word in words)
                {
                    int s_start = rtb.SelectionStart, startIndex = 0, idx;

                    while ((idx = rtb.Text.IndexOf(word, startIndex)) != -1)
                    {
                        index = rtb.GetLineFromCharIndex(idx);
                        rtb.Select(idx, rtb.Lines[index].Length);
                        rtb.SelectionColor = Color.Green;

                        startIndex = idx + rtb.Lines[index].Length;
                    }

                    rtb.SelectionStart = s_start;
                    rtb.SelectionLength = 0;
                    rtb.SelectionColor = Color.Black;
                    rtb.ScrollToCaret();
                }
                rtb.Focus();
            }
            finally { LockWindowUpdate(IntPtr.Zero); }

        }

        public void rtbx_TextChanged(object sender, EventArgs e)
        {
            RTBXRefresh();
        }

        private void rtbx_SelectionChanged(object sender, EventArgs e)
        {
            index = rtbx.GetLineFromCharIndex(rtbx.SelectionStart);
            // Get the line.

            // Get the column.
            int firstChar = rtbx.GetFirstCharIndexFromLine(index);
            int column = rtbx.SelectionStart - firstChar;
            TSSLBPosition.Text = "Ligne = " + index.ToString() + " : Caractère = " + column.ToString();
        }

        private void TSMIListeMethodes_Click(object sender, EventArgs e)
        {

        }

        private void CMS_TSMI_Cut_Click(object sender, EventArgs e)
        {
            TSMICut_Click(sender, e);
        }

        private void CMS_TSMI_Copy_Click(object sender, EventArgs e)
        {
            TSMICopy_Click(sender, e);
        }

        private void CMS_TSMI_Paste_Click(object sender, EventArgs e)
        {
            TSMIPaste_Click(sender, e);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void rtbx_KeyDown(object sender, KeyEventArgs e)
        {
            TSSLBToSave.Visible = true;
        }

        private void TSMINews_Click(object sender, EventArgs e)
        {
            FormNews fn = new FormNews();
            fn.ShowDialog();
        }

        private void TSMIHelp_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "file://" + Path.Combine(Application.StartupPath, "ScriptWSFEditor-Help.chm"));
        }

        private void TSMICleanScript_Click(object sender, EventArgs e)
        {
            MyFunctions mf = new MyFunctions();
            if (!isNewFile && CommunSNCF.scriptFile != "")
            {
                string[] fileMethodes = mf.FindAllMethodesInOldFile(CommunSNCF.scriptFile, true, false);
                List<string> methodesToRemove = fileMethodes.ToList<string>();
                string[] fileMethodesUsed = mf.FindAllTodosInOldFile(CommunSNCF.scriptFile);
                foreach(string methode in fileMethodes)
                {
                    if (mf.StringExistInArray(methode, fileMethodesUsed))
                        methodesToRemove.Remove(methode);
                }
                mf.RemoveBlockFromTo(rtbx.Lines, methodesToRemove, rtbx,TSProgressBar);
            }
        }

        private void TSMICommandLine_Click(object sender, EventArgs e)
        {
            if (CommunSNCF.scriptFile != "")
            {
                FileInfo fi = new FileInfo(CommunSNCF.scriptFile);
                Clipboard.SetData(DataFormats.Text, CommunSNCF.ReadRegParam(CommunSNCF.HKLMApplication, "CmdLineForMDT").Trim() + " " + fi.Name);
                TSSLBClipboard.Visible = true;
            }
            else
                MessageBox.Show("Le nom du script doit être défini, vous devez enregistrer le script", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public class MyFunctions
    {
        public bool StringExistInArray(string search, Array items)
        {
            bool result = false;
            foreach (string item in items)
            {
                if (item.Contains(search))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public void SaveFile(string[] lines)
        {
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Sauvegarde du script");
            try
            {
                FileInfo fi = new FileInfo(CommunSNCF.scriptFile);
                string path = fi.DirectoryName;
                if (fi.FullName != "")
                {
                    CommunSNCF.finalDestination = fi.DirectoryName;
                    if (!Directory.Exists(CommunSNCF.finalDestination))
                    {
                        Directory.CreateDirectory(CommunSNCF.finalDestination);
                    }
                    CreateScript(CommunSNCF.finalDestination, fi.FullName, lines);
                }
            }
            catch (Exception ex)
            {
                CommunSNCF.Log((int)CommunSNCF.logtype.Error, "Sauvegarde du script depuis l'aperçu " + ex.Message);
            }
        }

        public void SaveFileAs(string[] lines)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Fichiers wsf (*.wsf)|*.wsf|Tous les fichiers (*.*)|*.*|Fichiers text (*.txt) | *.txt|Fichiers vbs (*.vbs) | *.vbs";
                sfd.Title = "Enregistrer le script sous ...";
                sfd.FileName = CommunSNCF.jobid;
                if (sfd.ShowDialog() != DialogResult.Cancel)
                {
                    CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Sauvegarde du script sous");
                    if (sfd.FileName != "")
                    {
                        FileInfo fi = new FileInfo(sfd.FileName);
                        CommunSNCF.finalDestination = fi.DirectoryName;
                        if (!Directory.Exists(CommunSNCF.finalDestination))
                        {
                            Directory.CreateDirectory(CommunSNCF.finalDestination);
                        }
                        CreateScript(CommunSNCF.finalDestination, fi.FullName, lines);
                    }
                    CommunSNCF.scriptFile = sfd.FileName;
                } else
                {
                    CommunSNCF.scriptFile = "";
                }
            }
            catch (Exception ex)
            {
                CommunSNCF.Log((int)CommunSNCF.logtype.Error, "Sauvegarde du script depuis l'aperçu sous " + ex.Message);
            }
        }

        public void CreateScript(string directory, string file, string[] lines)
        {
            string newSourceAppFolder = Path.Combine(directory,"Sources");
            if (!Directory.Exists(newSourceAppFolder))
                Directory.CreateDirectory(newSourceAppFolder);
            if (File.Exists(file))
                File.Delete(file);
            using (var tw = new StreamWriter(file, true))
            {
                foreach (string line in lines)
                    tw.WriteLine(line);
            }

        }

        public string GetAllSetObject(string oFile)
        {
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Récupération des objects SET");
            string result = "";
            List<string> tmpresult = new List<string>();
            List <string> tmp = CommunSNCF.ReadFileFromToExtract(oFile, "oEnv(\"SEE_MASK_NOZONECHECKS\")", "Finished installation\"","SET ");
            string[] keys = new string[] { "Set", "set", "SET" };
            string setvar = "";
            foreach (string line in tmp)
            {
                string sKeyResult = keys.FirstOrDefault<string>(s => line.Contains(s));
                switch (sKeyResult)
                {
                    case "Set":
                        setvar = line.Split('=')[0].Replace("Set", "").Trim();
                        break;
                    case "set":
                        setvar = line.Split('=')[0].Replace("set", "").Trim();
                        break;
                    case "SET":
                        setvar = line.Split('=')[0].Replace("SET", "").Trim();
                        break;
                }
                if (setvar != "")
                    tmpresult.Add(setvar);
            }
            if (tmpresult.Count() > 0)
                result = string.Join(",", tmpresult);
            return result;
        }

        public string[] ValidToruns(string[] toruns)
        {
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Validation des executions");
            List<string> tmp = toruns.ToList<string>();
            List<string> result = new List<string>();
            string[] methodes = CommunSNCF.ReadRegParam("AllMethodes").Split(',');
            Array.Sort(methodes);
            List<string> keywords = CommunSNCF.ReadRegParam("ConditionsKeyWords").Split(',').ToList<string>();
            keywords.Add("ztiprocess=");
            string[] keys = keywords.ToArray();
            string sKeyResult = "";
            int index = 0;
            foreach (string val in toruns)
            {
                if (val != "")
                {
                    sKeyResult = methodes.FirstOrDefault<string>(s => val.Contains(s));
                    switch (sKeyResult)
                    {
                        case null:
                            result.Add(val);
                            break;
                        case "GetEnvVariable":
                            if(val.ToLower().Contains("if"))
                                result.Add(val);
                            else
                            {
                                index = tmp.IndexOf(val) + 1;
                                if (index < tmp.Count() && tmp[index].Trim() != "")
                                {
                                    result.Add("");
                                }
                            }

                            break;
                        default:
                            result.Add(val);
                            index = tmp.IndexOf(val) + 1;
                            if (index < tmp.Count() && tmp[index].Trim() != "")
                            {
                                result.Add("");
                            }
                            break;
                    }
                }
                else
                {
                    result.Add(val);
                    continue;
                }
            }
            return result.ToArray();
        }

        public string[] FindDependency(string pathfile)
        {
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Recherche des dependances");
            List<string> result = Array.Empty<string>().ToList<string>();
            string[] tmp = File.ReadLines(pathfile).ToArray<string>();
            foreach(string line in tmp)
            {
                if (line.StartsWith("#D:"))
                    result.Add(line.Substring(3, line.Length - 3));
            }
            return result.ToArray();
        }

        public string GetDim(string[] variables)
        {
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Recherche des declarations dim");
            string result = "Dim sSource,sIniSrc";
            foreach(string variable in variables)
            {
                result += "," + variable;
            }

            if (GetAllSetObject(CommunSNCF.ReadRegParam("currentfile")) != "") {
                foreach (string v in GetAllSetObject(CommunSNCF.ReadRegParam("currentfile")).Split(',')) {
                    if (!result.Contains(v))
                        result += "," + v;
                }
            }
            result += "\r\n\t\r\n\tsSource = oUtility.ScriptDir & \"\\Sources\\\"\r\n\tsIniSrc = \"C:\\Sncf\\Deploiement\\AffMessageXPLD\\\"";
            return result;
        }

        public string GetToRun(string[] implementations, string[] functions, string ofile)
        {
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Creation des executiosn qui seront dans le script final");
            MyFunctions mf = new MyFunctions();
            string[] methodesUsed = new List<string>().ToArray();
            if (ofile != "")
                methodesUsed = mf.MethodesUsedInFile(ofile);
            else
                methodesUsed = CommunSNCF.ReadRegParam("AllMethodes").Split(',');
            string result = "";
            List<string> tmpImplement = Array.Empty<string>().ToList<string>();
            string oldpb = "";
            foreach (string implementation in implementations)
            {
                List<string> tmp = implementation.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
                List<string> tmpResult = Array.Empty<string>().ToList<string>();
                string tab = "";
                foreach (string item in tmp)
                {
                    bool isfirst = false;
                    bool islast = false;
                    if (implementations.First() == implementation)
                        isfirst = true;
                    if (implementations.Last() == implementation)
                        islast = true;
                    bool ismethode = false;
                    bool isfunction = false;
                    string tmp2 = item.Trim();
                    foreach (string val in item.Replace('(', ' ').Split(' '))
                    {
                        if (ismethode = methodesUsed.Contains(val.Replace("ZTIProcess=", "").Trim().TrimStart('\t')))
                        {
                            isfunction = false;
                            if (!CommunSNCF.ReadRegParam("SubList").Contains(val.Replace("ZTIProcess=", "").Trim().TrimStart('\t')))
                            {
                                tmp2 = "ZTIProcess=" + item.Replace("ZTIProcess=", "").Trim();
                                isfunction = true;
                            }
                            else
                            {
                                tmp2 = item.Replace("ZTIProcess=", "").Trim();
                            }
                            break;
                        }
                    }

                    string tmp3 = tmp2;
                    List<string> keywords = CommunSNCF.ReadRegParam("ConditionsKeyWords").Split(',').ToList<string>();
                    keywords.Add("ztiprocess=");
                    string[] keys = keywords.ToArray();
                    string sKeyResult = "";
                    if (tmp2 != null && ismethode)
                    {
                        frmInformation fi = new frmInformation();
                        CommunSNCF.pbMethode = tmp2.TrimStart('\t').Trim();
                        switch (implementations.Count())
                        {
                            case 0:
                            case 1:
                                CommunSNCF.pbAvancement = 99;
                                break;
                            case 2:
                                CommunSNCF.pbAvancement = CommunSNCF.pbAvancement + Math.Floor(Convert.ToDecimal(95 / 2) - 1);
                                break;
                            default:
                                CommunSNCF.pbAvancement = CommunSNCF.pbAvancement + Math.Floor(Convert.ToDecimal((95 / (implementations.Count()) - 1)));
                                break;
                        }
                        if (isfunction)
                        {
                            sKeyResult = keys.FirstOrDefault<string>(s => item.Trim().Contains(s));
                            switch (sKeyResult)
                            {
                                case "":
                                     if (item.Trim().Contains("ZTIProcess="))
                                        tmp2 = item;
                                    else
                                        tmp2 = "ZTIProcess=" + item.Trim();
                                    break;
                                default:
                                    tmp2 = item;
                                    break;
                           }
                        }
                        else
                        {
                            tmp2 = item;
                        }
                        if (!tmp2.ToLower().StartsWith("if") && !tmp2.ToLower().StartsWith("else") && !tmp2.ToLower().StartsWith("end if"))
                        {
                            if (fi.ShowDialog() != DialogResult.Ignore)
                            {
                                if (CommunSNCF.formInformationValid)
                                {
                                    if (CommunSNCF.pbInformation != null && CommunSNCF.pbAvancement != -1)
                                    {
                                        string firstpb = "";
                                        string pbline = "";
                                        if (isfirst)
                                        {
                                            firstpb = "oLogging.ReportProgress \"" + CommunSNCF.pbInformation + "\",5";
                                            pbline = "oLogging.ReportProgress \"" + CommunSNCF.pbInformation + "\"," + CommunSNCF.pbAvancement.ToString();
                                            oldpb = (CommunSNCF.pbAvancement + 1).ToString();
                                        }
                                        if (islast)
                                        {
                                            firstpb = "oLogging.ReportProgress \"" + CommunSNCF.pbInformation + "\"," + CommunSNCF.pbAvancement.ToString();
                                            pbline = "oLogging.ReportProgress \"" + CommunSNCF.pbInformation + "\",100";
                                        }
                                        if (!isfirst && !islast)
                                        {
                                            firstpb = "oLogging.ReportProgress \"" + CommunSNCF.pbInformation + "\"," + oldpb;
                                            pbline = "oLogging.ReportProgress \"" + CommunSNCF.pbInformation + "\"," + CommunSNCF.pbAvancement.ToString();
                                            oldpb = (CommunSNCF.pbAvancement + 1).ToString();
                                        }
                                        string comment = "'--- " + CommunSNCF.pbInformation + " ---";
                                        string logInfo = "oLogging.CreateEntry oUtility.ScriptName  & \" : " + CommunSNCF.pbInformation + "\", LogTypeInfo";
                                        tmp3 = "\t" + comment + "\r\n\r\n\t" + tab + logInfo + "\r\n\t" + firstpb + "\r\n\t" + tab + tmp2 + "\r\n\t" + tab + pbline;
                                    }
                                }
                            }
                            else
                            {
                                return null;
                            }
                        }
                        else
                        {
                            tmp3 = tmp2;
                        }
                    }
                    if (tmp3 != "\t")
                    {
                        sKeyResult = keys.FirstOrDefault<string>(s => item.Contains(s));
                        switch (sKeyResult)
                        {
                            case "if (":
                            case "if(":
                            case "else":
                            case "end if":
                                tab = "";
                                break;
                            default:
                                tab = "\t";
                                break;
                        }
                        tmpResult.Add(tab + tmp3);
                    }
                    if (tmp3.Contains("if(") || tmp3.Contains("if (") || tmp3.Contains("else"))
                    {
                        tab = "\t";
                    }

                }
                tmpImplement.Add(CommunSNCF.ConvertArrayToString(tmpResult.ToArray(), "\r\n") + "\r\n\r\n\t");
            }
            List<string> tmpresult = AddTabInArray(tmpImplement.ToArray());
            result = CommunSNCF.ConvertArrayToString(tmpresult.ToArray(), "");
            return result.TrimEnd('\t').TrimEnd('\n').TrimEnd('\r').TrimEnd('\n').TrimEnd('\r');
        }

        public string GetToRun(string[] implementations, string[] functions, string ofile,bool isDialogVisible)
        {
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Creation des executions avec ou sans boite de dialogue qui seront dans le script final");
            MyFunctions mf = new MyFunctions();
            string[] methodesUsed = new List<string>().ToArray();
            if (ofile != "")
                methodesUsed = mf.MethodesUsedInFile(ofile);
            else
                methodesUsed = CommunSNCF.ReadRegParam("AllMethodes").Split(',');
            string result = "";
            List<string> tmpImplement = Array.Empty<string>().ToList<string>();
            string oldpb = "";
            foreach (string implementation in implementations)
            {
                //string tmp1 = implementation.Replace("||", "\r\n\t\t").Replace("|", "\r\n\t") + "\r\n\r\n\t";
                List<string> tmp = implementation.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
                List<string> tmpResult = Array.Empty<string>().ToList<string>();
                string tab = "";
                foreach (string item in tmp)
                {
                    bool isfirst = false;
                    bool islast = false;
                    if (implementations.First() == implementation)
                        isfirst = true;
                    if (implementations.Last() == implementation)
                        islast = true;
                    bool ismethode = false;
                    bool isfunction = false;
                    string tmp2 = item.Trim();
                    foreach (string val in item.Replace('(', ' ').Split(' '))
                    {
                        if (ismethode = methodesUsed.Contains(val.Replace("ZTIProcess=", "").Trim().TrimStart('\t')))
                        {
                            isfunction = false;
                            if (!CommunSNCF.ReadRegParam("SubList").Contains(val.Replace("ZTIProcess=", "").Trim().TrimStart('\t')))
                            {
                                tmp2 = "ZTIProcess=" + item.Replace("ZTIProcess=", "").Trim();
                                isfunction = true;
                            }
                            else
                            {
                                tmp2 = item.Replace("ZTIProcess=", "").Trim();
                            }
                            break;
                        }
                    }

                    string tmp3 = tmp2;
                    List<string> keywords = CommunSNCF.ReadRegParam("ConditionsKeyWords").Split(',').ToList<string>();
                    keywords.Add("ztiprocess=");
                    string[] keys = keywords.ToArray();
                    string sKeyResult = "";
                    if (tmp2 != null && ismethode)
                    {
                        frmInformation fi = new frmInformation();
                        CommunSNCF.pbMethode = tmp2.TrimStart('\t').Trim();
                        switch (implementations.Count())
                        {
                            case 0:
                            case 1:
                                CommunSNCF.pbAvancement = 99;
                                break;
                            case 2:
                                CommunSNCF.pbAvancement = CommunSNCF.pbAvancement + Math.Floor(Convert.ToDecimal(95 / 2) - 1);
                                break;
                            default:
                                CommunSNCF.pbAvancement = CommunSNCF.pbAvancement + Math.Floor(Convert.ToDecimal((95 / (implementations.Count()) - 1)));
                                break;
                        }
                        if (isfunction)
                        {
                            sKeyResult = keys.FirstOrDefault<string>(s => item.Trim().Contains(s));
                            switch (sKeyResult)
                            {
                                case "":
                                    if (item.Trim().Contains("ZTIProcess="))
                                        tmp2 = item;
                                    else
                                        tmp2 = "ZTIProcess=" + item.Trim();
                                    break;
                                default:
                                    tmp2 = item;
                                    break;
                            }
                        }
                        else
                        {
                            tmp2 = item;
                        }
                        if (!tmp2.ToLower().StartsWith("if") && !tmp2.ToLower().StartsWith("else") && !tmp2.ToLower().StartsWith("end if"))
                        {
                            if (isDialogVisible)
                            {
                                if (fi.ShowDialog() != DialogResult.Ignore)
                                {
                                    if (CommunSNCF.formInformationValid)
                                    {
                                        if (CommunSNCF.pbInformation != null && CommunSNCF.pbAvancement != -1)
                                        {
                                            string firstpb = "";
                                            string pbline = "";
                                            if (isfirst)
                                            {
                                                firstpb = "oLogging.ReportProgress \"" + CommunSNCF.pbInformation + "\",5";
                                                pbline = "oLogging.ReportProgress \"" + CommunSNCF.pbInformation + "\"," + CommunSNCF.pbAvancement.ToString();
                                                oldpb = (CommunSNCF.pbAvancement + 1).ToString();
                                            }
                                            if (islast)
                                            {
                                                firstpb = "oLogging.ReportProgress \"" + CommunSNCF.pbInformation + "\"," + CommunSNCF.pbAvancement.ToString();
                                                pbline = "oLogging.ReportProgress \"" + CommunSNCF.pbInformation + "\",100";
                                            }
                                            if (!isfirst && !islast)
                                            {
                                                firstpb = "oLogging.ReportProgress \"" + CommunSNCF.pbInformation + "\"," + oldpb;
                                                pbline = "oLogging.ReportProgress \"" + CommunSNCF.pbInformation + "\"," + CommunSNCF.pbAvancement.ToString();
                                                oldpb = (CommunSNCF.pbAvancement + 1).ToString();
                                            }
                                            string comment = "'--- " + CommunSNCF.pbInformation + " ---";
                                            string logInfo = "oLogging.CreateEntry oUtility.ScriptName  & \" : " + CommunSNCF.pbInformation + "\", LogTypeInfo";
                                            tmp3 = "\t" + comment + "\r\n\r\n\t" + tab + logInfo + "\r\n\t" + firstpb + "\r\n\t" + tab + tmp2 + "\r\n\t" + tab + pbline;
                                        }
                                    }
                                    else
                                    {
                                        return null;
                                    }
                                }
                            }
                       }
                        else
                        {
                            tmp3 = tmp2;
                        }
                    }
                    if (tmp3 != "\t")
                    {
                        sKeyResult = keys.FirstOrDefault<string>(s => item.Contains(s));
                        switch (sKeyResult)
                        {
                            case "if (":
                            case "if(":
                            case "else":
                            case "end if":
                                tab = "";
                                break;
                            default:
                                tab = "\t";
                                break;
                        }
                        tmpResult.Add(tab + tmp3);
                    }
                    if (tmp3.Contains("if(") || tmp3.Contains("if (") || tmp3.Contains("else"))
                    {
                        tab = "\t";
                    }

                }
                tmpImplement.Add(CommunSNCF.ConvertArrayToString(tmpResult.ToArray(), "\r\n") + "\r\n\r\n\t");
            }
            List<string> tmpresult = AddTabInArray(tmpImplement.ToArray());
            result = CommunSNCF.ConvertArrayToString(tmpresult.ToArray(), "");
            return result.TrimEnd('\t').TrimEnd('\n').TrimEnd('\r').TrimEnd('\n').TrimEnd('\r');
        }

        public List<string> AddTabInArray(string[] ar)
        {
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Gestion des tabulations");
            List<string> result = new List<string>();
            foreach (string item in ar)
            {
                string[] itemfirst = item.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.None);
                List<string> keywords = CommunSNCF.ReadRegParam("ConditionsKeyWords").Split(',').ToList<string>();
                keywords.Add("ztiprocess=");
                keywords.Add("\tif");
                keywords.Add("if(");
                keywords.Add("\telse");
                keywords.Add("\tend if");
                keywords.Add("\t\t");
                keywords.Add("\t");
                string[] keys = keywords.ToArray();
                string sKeyResult = "";
                string tab = "";
                foreach (string i in itemfirst)
                {
                    //[TODEBUG] a tester correctement surtout avec de nouvelle condition
                    sKeyResult = keys.FirstOrDefault<string>(s => i.ToLower().StartsWith(s));
                    switch (sKeyResult)
                    {
                        case "\t":
                            if(itemfirst.First() != i) 
                                result.Add("\r\n" + tab + i);
                            else
                                result.Add(tab + i.TrimStart('\t'));
                            tab = "";
                            break;
                        case "\t\t":
                            if (itemfirst.First() != i)
                                result.Add("\r\n\t" + tab + i.TrimStart('\t'));
                            else
                                result.Add( tab + i.TrimStart('\t'));
                            tab = "";
                            break;
                        case null:
                            if (itemfirst.First() != i)
                                result.Add("\r\n");
                            tab = "";
                            break;
                        case "\tif":
                            if (itemfirst.First() != i)
                                result.Add("\r\n\t" + tab + i.TrimStart('\t'));
                            else
                                result.Add(tab + i.TrimStart('\t'));
                            tab = "\t";
                            break;
                        case "\telse":
                            if (itemfirst.First() != i)
                                result.Add("\r\n" + tab + i);
                            else
                                result.Add(tab + i);
                            tab = "\t";
                            break;
                        case "\tend if":
                            if (itemfirst.First() != i)
                                result.Add("\r\n" + tab + i);
                            else
                                result.Add(tab + i);
                            tab = "";
                            break;
                        default:
                            result.Add(tab + i);
                            break;
                    }
                }
            }
            if (result.Count > 0)
            {
                while (result.Last() == "\r\n\t")
                {
                    result.RemoveAt(result.Count - 1);
                }
            }
            return result;
        }

        public string[] GetMethode(string pathfile)
        {
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Recuperation des methodes dans un fichier existant");
            List<string> result = Array.Empty<string>().ToList<string>();
            try
            {
                string[] tmp = File.ReadLines(pathfile).ToArray<string>();
                foreach (string line in tmp)
                {
                    if (!line.StartsWith("#D:") && !line.StartsWith("#"))
                        result.Add(line);
                    //.Substring(3, line.Length - 3)
                }
            } catch (Exception ex)
            {
                CommunSNCF.Log(3, "Erreur non gérée - " + ex.Message);
                string msg = "Une erreur est remontée sur la méthode " + pathfile + "\r\n" + ex.Message;
                string titre = "Erreur";
                MessageBox.Show(msg, titre, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result.ToArray();
        }

        public string GetMethodes(string[] methodes)
        {
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Listage des méthodes sous forme de string");
            string result = "";
            foreach (string methode in methodes)
            {
                result += (string.Join("\r\n", GetMethode(Path.Combine(FormMain.regPathMethodes, methode))) + "\r\n\r\n");
            }
            return result.TrimEnd('\n').TrimEnd('\r').TrimEnd('\n').TrimEnd('\r');
        }

        public string[] FindAllMethodesInFile(string strFilePath)
        {
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Recherche des methodes dans un script");
            List<string> result = Array.Empty<string>().ToList<string>();

            string line = "";
            string text = "";
            string tmpmethode = "";
            bool start = false;
            StreamReader file = new StreamReader(strFilePath);
            while (!file.EndOfStream)
            {
                while ((line = file.ReadLine()) != null)
                {
                    if (line.ToLower().Contains("function ") || start)
                    {
                        start = true;
                        if(line.ToLower().Contains("function "))
                            tmpmethode = line.Remove(line.IndexOf("(")).Remove(0, 9);
                        text += line;
                    }
                    if (line.ToLower().Contains("end function"))
                    {
                        start = false;
                        break;
                    }

                    if (line.ToLower().Contains("sub ") || start)
                    {
                        start = true;
                        if (line.ToLower().Contains("sub "))
                            tmpmethode = line.Remove(line.IndexOf("(")).Remove(0, 4);
                        text += line;
                    }
                    if (line.ToLower().Contains("end sub"))
                    {
                        start = false;
                        break;
                    }
                }
                if (!text.Contains("Function ZTIProcess()") && text != "")
                {
                    string allMethodesWhoExists = CommunSNCF.ReadRegParam("AllMethodes");
                    if (!allMethodesWhoExists.Contains(tmpmethode))
                    {
                        string msg = "La méthode " + tmpmethode.Trim() + " qui se trouve dans votre fichier,\r\nn'existe pas dans votre référentiel.\r\nVoulez-vous l'ajouter à votre référentiel ?";
                        string titre = "Méthode inexistante";
                        if (MessageBox.Show(msg, titre, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            bool isfunction = true;
                            if (line.ToLower().Contains("sub "))
                                isfunction = false;
                            ExtractMethodeAndSave(strFilePath, isfunction, tmpmethode.Trim(), FormMain.regPathMethodes);
                        }
                        else
                        {
                            CommunSNCF.errorMsg += "Méthode " + tmpmethode.Trim() + " inconnue.\r\n";
                        }
                    }
                    result.Add(text);
                    text = "";
                }
            }
            file.Close();
            return result.ToArray();
        }

        public string FindAllDeclarationInFile(string strFilePath)
        {
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Recherche des declarations dim dans un fichier");
            string result = null;

            string line;
            StreamReader file = new StreamReader(strFilePath);
            while ((line = file.ReadLine()) != null)
            {
                if (line.ToLower().Contains("dim ssource,sinisrc,"))
                {
                    result = line;
                    break;
                }
            }
            file.Close();
            return result;
        }

        public string[] FindAllTodosInFile(string strFilePath)
        {
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Recherche des executions dans un script");
            List<string> result = Array.Empty<string>().ToList<string>();

            string line;
            bool start = false;
            StreamReader file = new StreamReader(strFilePath);
            while ((line = file.ReadLine()) != null)
            {
                string allMethodesWhoExists = CommunSNCF.ReadRegParam("AllMethodes");
                if (line.ToLower().Contains("=torun=") || start)
                {
                    start = true;
                    if (!line.ToLower().Contains("=torun=") && !line.ToLower().Contains("=fin torun=") && !line.ToLower().Contains("ologging") && !line.ToLower().Contains("'---") && !line.ToLower().StartsWith("\t'") && line != "")
                    {
                        if (line.ToLower().Contains("ztiprocess="))
                        {
                            line = "\t" + line.Replace("ZTIProcess=", "").Trim();
                            string[] tmp0 = line.Replace("ZTIProcess=", "").Replace("ZTIProcess =", "").Replace("\t", "").Split('(');
                            if (!allMethodesWhoExists.Contains(tmp0[0].Trim()))
                            {
                                CommunSNCF.errorMsg += "Exécution la méthode " + tmp0[0].Trim() + " utilisée est  inconnue.\r\n";
                            }
                            result.Add(line);
                            line = "";
                        }
                        result.Add(line);
                    }
                }
                if (line.ToLower().Contains("=fin torun=") || line.Contains("Finished "))
                {
                    start = false;
                    break;
                }
            }
            file.Close();
            return result.ToArray();
        }

        public string FindJobFor(string strFilePath)
        {
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Recherche du nom du job");
            string result = null;
            StreamReader file = new StreamReader(strFilePath);
            string line = file.ReadLine();
            if (line.Contains("Install-"))
            {
                result = line.Remove(0, 17).Replace("\">", "");
            } else
            {
                result = line.Replace("\">", "").Substring(9);
            }
            
            file.Close();
            return result;
        }

        public bool MethodeExistsInRefentiel(string methode)
        {
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Test pour savoir si la methode est deja dans le referentiel");
            bool result = false;
            string referentiel = CommunSNCF.ReadRegParam("AllMethodes");
            if (referentiel.Contains(methode))
                result = true;
            return result;
        }

        public string[] MethodesUsedInFile(string ofile)
        {
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Recherche de la liste des methodes utilisees dans le script");
            List<string> result = Array.Empty<string>().ToList<string>();
            string line;
            bool start = false;
            if (ofile != "")
            {
                StreamReader file = new StreamReader(ofile);
                while (!file.EndOfStream)
                {
                    while ((line = file.ReadLine()) != null)
                    {
                        if (line.ToLower().Contains("dim iretval, iretvalcopy") || start)
                        {
                            if ((line.ToLower().Contains("function ") || line.ToLower().Contains("sub ")) && !line.StartsWith("'"))
                            {
                                Application.DoEvents();
                                string tmpmethode = "";
                                if (line.ToLower().Contains("function "))
                                {
                                    tmpmethode = line.Remove(line.IndexOf("(")).Remove(0, 9);
                                }
                                if (line.ToLower().Contains("sub "))
                                {
                                    tmpmethode = line.Remove(line.IndexOf("(")).Remove(0, 4);
                                }
                                if (!result.Contains(tmpmethode))
                                    result.Add(tmpmethode);
                                break;
                            }
                            start = true;
                            if (line.Contains("On Error Resume Next"))
                            {
                                start = false;
                                break;
                            }
                        }
                    }
                }
                file.Close();
            }
            result.Sort();
            return result.ToArray();
        }

        public string[] FindAllMethodesInOldFile(string openfile, bool court, bool isrefresh)
        {
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Recherche de la liste des methodes utilisees dans le script ancienne formule");
            List<string> result = Array.Empty<string>().ToList<string>();
            string line;
            bool start = false;
            StreamReader file = new StreamReader(openfile);
            string allMethodesWhoExists = CommunSNCF.ReadRegParam("AllMethodes");
            while (!file.EndOfStream)
            {
                while ((line = file.ReadLine()) != null)
                {
                    if (line.ToLower().Contains("dim iretval, iretvalcopy") || start)
                    {
                        string tmpmethode = "";
                        if ((line.ToLower().Contains("function ") || line.ToLower().Contains("sub ")) && !line.StartsWith("'"))
                        {
                            Application.DoEvents();
                            if (line.ToLower().Contains("function "))
                            {
                                tmpmethode = line.Remove(line.IndexOf("(")).Remove(0, 9);
                            }
                            if (line.ToLower().Contains("sub "))
                            {
                                tmpmethode = line.Remove(line.IndexOf("(")).Remove(0, 4);
                            }
                            if (!allMethodesWhoExists.Contains(tmpmethode.Trim()) && !isrefresh)
                            {
                                string msg = "La méthode " + tmpmethode.Trim() + " qui se trouve dans votre fichier,\r\nn'existe pas dans votre référentiel.\r\nVoulez-vous l'ajouter à votre référentiel ?";
                                string titre = "Méthode inexistante";
                                if (MessageBox.Show(msg, titre, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    bool isfunction = true;
                                    if (line.ToLower().Contains("sub "))
                                        isfunction = false;
                                    ExtractMethodeAndSave(openfile, isfunction, tmpmethode.Trim(), FormMain.regPathMethodes);
                                } else
                                {
                                    CommunSNCF.errorMsg += "Méthode " + tmpmethode.Trim() + " inconnue.\r\n";
                                }
                            }
                            if (court)
                            {
                                if (!result.Contains(tmpmethode))
                                    result.Add(tmpmethode);
                            }
                            else
                            {
                                if (!result.Contains(line))
                                    result.Add(line);
                            }
                            break;
                        }

                        if (line.ToLower().Contains("sub ") && !line.StartsWith("'"))
                        {
                            if (court)
                            {
                                if (!result.Contains(tmpmethode))
                                    result.Add(tmpmethode);
                            }
                            else
                            {
                                if (!result.Contains(line))
                                    result.Add(line);
                            }
                            break;
                        }
                        start = true;
                        if (line.Contains("On Error Resume Next"))
                        {
                            start = false;
                            break;
                        }
                    }
                }
            }          
            file.Close();
            return result.ToArray();
        }

        public void ExtractMethodeAndSave(string openfile, bool isfunction, string tmpmethode, string regPathMethodes)
        {
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Extraction de la méthode et sauvegarde dans le referentiel");
            string tosearch = "";
            string limit = "";
            List<string> methode = new List<string>();
            if (isfunction)
            {
                tosearch = "Function " + tmpmethode.Trim();
                limit = "End Function";
                methode = CommunSNCF.ReadFileFromTo(openfile, tosearch, limit);
            }
            else
            {
                tosearch = "Sub " + tmpmethode.Trim();
                limit = "End Sub";
                methode = CommunSNCF.ReadFileFromTo(openfile, tosearch, limit);
            }

            string path = Path.Combine(regPathMethodes, tmpmethode.Trim());
            if (!File.Exists(path))
            {
                string content = "";
                foreach (string line in methode)
                {
                    content += line +"\r\n";
                }
                File.AppendAllText(path, content, Encoding.UTF8);
            }
        }

        public string FindAllDeclarationInOldFile(string openfile)
        {
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Recherche des declarations dans le script ancienne formule");
            List<string> result = Array.Empty<string>().ToList<string>();

            string line;
            string text = "";
            bool start = false;
            StreamReader file = new StreamReader(openfile);
            while (!file.EndOfStream)
            {
                while ((line = file.ReadLine()) != null)
                {
                    if (line.ToLower().Contains("function ztiprocess()") || start)
                    {
                        start = true;
                        if (line.ToLower().Contains("dim "))
                            text += line;
                    }
                    if (line.ToLower().Contains("end function"))
                    {
                        start = false;
                        break;
                    }
                }
            }
            file.Close();
            result = text.TrimStart('\t').Replace("Dim ","").Replace("\t", ",").Replace("'", "").Split(',').ToList<string>();
            result.Remove("sSource");
            result.Remove("sIniSrc");
            return CommunSNCF.ConvertArrayToString(result.ToArray(),",");
        }

        public void RefreshSubListInRegistry(string path)
        {
            if (path != "")
            {
                CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Actualisation des methodes SUB du script dans la base de registre");
                CommunSNCF.SaveRegParam("SubList", "");
                List<string> result = Array.Empty<string>().ToList<string>();
                try
                {
                    string line;
                    string text = "";
                    string[] dirs = Directory.GetFiles(path);
                    foreach (string item in dirs)
                    {
                        Application.DoEvents();
                        StreamReader file = new StreamReader(item);
                        while (!file.EndOfStream)
                        {
                            while ((line = file.ReadLine()) != null)
                            {
                                if (line.ToLower().Contains("sub "))
                                {
                                    text = line.Remove(line.IndexOf("(")).Remove(0, 4);
                                    result.Add(text);
                                }
                            }
                        }
                        file.Close();
                    }
                    CommunSNCF.SaveRegParam("SubList", CommunSNCF.ConvertArrayToString(result.ToArray(), ","));
                }
                catch (Exception ex)
                {
                    CommunSNCF.Log((int)CommunSNCF.logtype.Error, "Actualisation des methodes SUB dans le registre " + ex.Message);
                }
            } else
            {
                string sMsg = "Vous devez indiquer le chemin des méthodes à utiliser dans les options du menu Edition.";
                string titre = "Options non conforme";
                MessageBox.Show(sMsg, titre, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void RefreshAllMethodeListInRegistry(string path)
        {
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Actualisation des methodes FUNCTION du script dans la base de registre");
            CommunSNCF.SaveRegParam("AllMethodes", "");
            List<string> result = Array.Empty<string>().ToList<string>();

            string[] dirs = Directory.GetFiles(path);
            foreach (string item in dirs)
            {
                Application.DoEvents();
                FileInfo fi = new FileInfo(item);
                result.Add(fi.Name);
            }
            CommunSNCF.SaveRegParam("AllMethodes", CommunSNCF.ConvertArrayToString(result.ToArray(), ","));
        }

        public string[] FindAllTodosInOldFile(string openfile)
        {
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Recherche de la liste des executions dans le script ancienne formule");
            List<string> result = Array.Empty<string>().ToList<string>();

            string sublist = CommunSNCF.ReadRegParam("SubList");
            string allMethodesWhoExists = CommunSNCF.ReadRegParam("AllMethodes");
            string tmp = "";
            string line;
            bool start = false;
            StreamReader file = new StreamReader(openfile);
            while (!file.EndOfStream)
            {
                while ((line = file.ReadLine()) != null)
                {
                    bool end = false;
                    if (line.Contains("oEnv(\"SEE_MASK_NOZONECHECKS\") = 1") || start)
                    {
                        Application.DoEvents();
                        if (!line.Contains("oLogging.") && !line.StartsWith("'") && !line.StartsWith("\t'") && line != "\t" && line != "\t\t" && line != "") {
                            if (start)
                            {
                                Application.DoEvents();
                                if (!end)
                                {
                                    //Tets si la méthode existe
                                    Application.DoEvents();
                                    if (line.Contains("ZTIProcess=") || line.Contains("ZTIProcess ="))
                                    {
                                        string[] tmp0 = line.Replace("ZTIProcess=", "").Replace("ZTIProcess =", "").Replace("\t", "").Split('(');
                                        if (!allMethodesWhoExists.Contains(tmp0[0].Trim()))
                                        {
                                            CommunSNCF.errorMsg += "Exécution la méthode " + tmp0[0].Trim() + " utilisée est inconnue.\r\n";
                                        }
                                    }
                                    Application.DoEvents();
                                    string tmp1 = line.Replace("ZTIPRocess=", "").Replace("ZTIProcess =", "").Replace("\t", "");
                                    tmp += tmp1 + "|";
                                    if (line.Contains("ZTIProcess") || sublist.Contains(tmp1.Split(' ')[0].Trim()))
                                    {
                                        end = true;
                                    }
                               }
                               if (tmp != "" && end)
                               {
                                    result.Add(tmp.TrimEnd('|'));
                                    tmp = "";
                                    break;
                               }
                            }
                        }
                        start = true;
                        if (line.Contains("oEnv.Remove(\"SEE_MASK_NOZONECHECKS\")"))
                        {
                            start = false;
                            break;
                        }
                    }
                }
            }
            file.Close();
            return result.ToArray();
        }

        internal void RemoveBlockFromTo(string[] lines, List<string> methodes, RichTextBox rtb, ToolStripProgressBar tspb)
        {
            try
            {
                FormMain.LockWindowUpdate(rtb.Handle);
                tspb.Visible = true;
                List<string> resulttmp = rtb.Lines.ToList<string>();
                List<string> variables = new List<string>();
                //SUppression de tous les commentaires
                foreach (string line in lines)
                {
                    if (line.StartsWith("'"))
                    {
                        resulttmp.Remove(line);
                    }
                }
                rtb.Clear();
                rtb.Lines = resulttmp.ToArray();

                List<string> result = rtb.Lines.ToList<string>();
                decimal step1 = 85 / methodes.Count;
                decimal step = Math.Round(step1) - 1;
                //Suppression des blocs de méthodes et des lignes vides en trop
                foreach (string methode in methodes)
                {
                    //recuperation des varaible de la méthode
                    string completemethode = CommunSNCF.ReadFirstLineFile(Path.Combine(FormMain.regPathMethodes, methode));
                    string varsToRemove = completemethode.Substring(completemethode.IndexOf('(') + 1, completemethode.IndexOf(')') - (completemethode.IndexOf('(') + 1));
                    foreach (string var in varsToRemove.Split(','))
                    {
                        variables.Add(var);
                    }

                    bool start = false;
                    bool startRemove = false;
                    string from = "Dim iRetVal, iRetValCopy";
                    string to = "Function ZTIProcess()";
                    int i = 0;
                    foreach (string line in rtb.Lines)
                    {
                        Application.DoEvents();
                        //SI on se trouve au début du block méthode
                        if (line.ToLower().Contains(from.ToLower()) || start)
                        {
                            string nline = line;
                            start = true;
                            while (i < result.Count())
                            {
                                nline = result[i];
                                Application.DoEvents();
                                string decmethode = "Function ";
                                if (nline.Contains("Sub "))
                                    decmethode = "Sub";
                                if (nline.Contains(methode) || startRemove)
                                {
                                    startRemove = true;
                                    result.RemoveAt(i);
                                    i--;

                                    if (nline == "End " + decmethode.Trim() && startRemove)
                                    {
                                        startRemove = false;
                                        start = false;
                                        break;
                                    }
                                }
                                i++;
                            }

                            //Si on se trouve à la fin du block méthodes
                            if (line.ToLower() == to.ToLower() && start)
                            {
                                start = false;
                            }
                        }
                        i++;
                    }
                    rtb.Clear();
                    rtb.Lines = result.ToArray();
                    tspb.Value += (int)step;
                    result = rtb.Lines.ToList<string>();
                }
                //suppression des lignes blanches superflues  
                //TODO A perfectionner       
                int j = 0;
                foreach (string line in rtb.Lines)
                {
                    Application.DoEvents();
                    if (j < rtb.Lines.Count() && line.Trim('\t') == "" && rtb.Lines[j + 1].Trim('\t') == "")
                    {
                        result.RemoveAt(j);
                        j--;
                    }
                    j++;
                }
                rtb.Clear();
                rtb.Lines = result.ToArray();
                tspb.Value = 90;
                result = rtb.Lines.ToList<string>();

                //supprimer des variables
                //TODO A perfectionner
                j = 0;
                foreach (string line in result)
                {
                    if (line.Contains("Function ZTIProcess"))
                    {
                        string newline = result[j + 1];
                        foreach (string variable in variables)
                        {
                            if (newline.Contains("Dim ") && variable != "")
                                newline = newline.Replace(variable, "");
                            newline = newline.Replace(",,", ",");
                        }
                        result[j + 1] = "";
                        result[j + 1] = newline;
                        break;
                    }
                    j++;
                }
                rtb.Clear();
                rtb.Lines = result.ToArray();
                tspb.Value = 95;
                result = rtb.Lines.ToList<string>();

                //rajoute les mêmes tags que dans le template
                int index = CommunSNCF.GetLineIndexContains(result, "</script>");
                FileVersionInfo productInfo = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location);
                result.Insert(index, "'##### Generated with " + Application.ProductName + " - " + Application.ProductVersion + " - " + productInfo.LegalCopyright + "#####");

                index = CommunSNCF.GetLineIndexContains(result, "oEnv.Remove(\"SEE_MASK_NOZONECHECKS\")");
                result.Insert(index - 1, "\t'/ =====FIN TORUN=====");

                index = CommunSNCF.GetLineIndexContains(result, "oEnv(\"SEE_MASK_NOZONECHECKS\")");
                result.Insert(index + 1, "\t'/ =====TORUN=====");

                index = CommunSNCF.GetLineIndexContains(result, "sIniSrc", "=");
                result.Insert(index + 1, "\t'/ =====FIN DECLARATIONS=====");

                index = CommunSNCF.GetLineIndexContains(result, "\tDim ");
                result.Insert(index, "\t'/ =====DECLARATIONS=====");

                index = CommunSNCF.GetLineIndexContains(result, "iRetVal = ZTIProcess");
                result.Insert(index - 2, "'/ =====FIN METHODES=====");

                index = CommunSNCF.GetLineIndexContains(result, "Dim iRetVal, iRetValCopy ");
                result.Insert(index + 1, "'/ =====METHODES=====");

                rtb.Clear();
                rtb.Lines = result.ToArray();
            }      
            finally {
                FormMain.LockWindowUpdate(IntPtr.Zero);
                tspb.Value = 100;
                Thread.Sleep(1000);
                MessageBox.Show("Nettoyage terminé.\r\nVérifiez qu'il ne manque pas de variables.\r\nVous pouvez ajouter ou supprimer des lignes blanches pour plus de clarté.\r\nVous pouvez ajouter des commentaires si cela vous paraît nécessaire.", "Nettoyage", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tspb.Value = 0;
                tspb.Visible = false;
            }

        }
    }

    public static class ModifyProgressBarColor
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);
        public static void SetState(this ProgressBar pBar, int state)
        {
            SendMessage(pBar.Handle, 1040, (IntPtr)state, IntPtr.Zero);
        }
    }
}
