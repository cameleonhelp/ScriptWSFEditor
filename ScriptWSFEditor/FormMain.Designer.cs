using System;

namespace ScriptWSFEditor
{
    partial class FormMain
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fichierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMINew = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.TSMISave = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMISaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMISavePDF = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.TSMIPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.TSMIClose = new System.Windows.Forms.ToolStripMenuItem();
            this.editionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMICut = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMICopy = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.TSMIEmptyClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.TSMIOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.TSMICommandLine = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIAddMethode = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.TSMIEditMethode = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIDeleteMethode = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.TSMIListeMethodes = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.TSMIRefreshMethodesList = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIReplaceInEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIInsertMethode = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIDeleteListsSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIPreviewScript = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMICleanScript = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.TSMIAddRunBefore = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIAddRunAfter = new System.Windows.Forms.ToolStripMenuItem();
            this.conditionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIAddCondition = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIImportConditionsReg = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIExportConditionsAs = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIEditCondition = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIRefreshConditionsList = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.TSMIConditionsList = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMINONAME = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMINews = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMIHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.TSMIAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.sfd = new System.Windows.Forms.SaveFileDialog();
            this.printDlg = new System.Windows.Forms.PrintDialog();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.TSSLBPosition = new System.Windows.Forms.ToolStripStatusLabel();
            this.TSSLBToSave = new System.Windows.Forms.ToolStripStatusLabel();
            this.TSSLBClipboard = new System.Windows.Forms.ToolStripStatusLabel();
            this.TSProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.rtbx = new System.Windows.Forms.RichTextBox();
            this.CMS = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CMS_TSMI_Cut = new System.Windows.Forms.ToolStripMenuItem();
            this.CMS_TSMI_Copy = new System.Windows.Forms.ToolStripMenuItem();
            this.CMS_TSMI_Paste = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.CMS.SuspendLayout();
            this.SuspendLayout();
            // 
            // ofd
            // 
            this.ofd.FileName = "openFileDialog1";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fichierToolStripMenuItem,
            this.editionToolStripMenuItem,
            this.toolStripMenuItem7,
            this.toolStripMenuItem8,
            this.toolStripMenuItem6,
            this.conditionsToolStripMenuItem,
            this.TSMINONAME});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1024, 24);
            this.menuStrip1.TabIndex = 47;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // fichierToolStripMenuItem
            // 
            this.fichierToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMINew,
            this.TSMIOpen,
            this.toolStripSeparator1,
            this.TSMISave,
            this.TSMISaveAs,
            this.TSMISavePDF,
            this.toolStripSeparator2,
            this.TSMIPrint,
            this.toolStripSeparator3,
            this.TSMIClose});
            this.fichierToolStripMenuItem.Name = "fichierToolStripMenuItem";
            this.fichierToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.fichierToolStripMenuItem.Text = "Fichier";
            // 
            // TSMINew
            // 
            this.TSMINew.Image = ((System.Drawing.Image)(resources.GetObject("TSMINew.Image")));
            this.TSMINew.Name = "TSMINew";
            this.TSMINew.Size = new System.Drawing.Size(170, 22);
            this.TSMINew.Text = "Nouveau";
            this.TSMINew.Click += new System.EventHandler(this.TSMINew_Click);
            // 
            // TSMIOpen
            // 
            this.TSMIOpen.Image = ((System.Drawing.Image)(resources.GetObject("TSMIOpen.Image")));
            this.TSMIOpen.Name = "TSMIOpen";
            this.TSMIOpen.Size = new System.Drawing.Size(170, 22);
            this.TSMIOpen.Text = "Ouvrir ...";
            this.TSMIOpen.Click += new System.EventHandler(this.TSMIOpen_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(167, 6);
            // 
            // TSMISave
            // 
            this.TSMISave.Image = ((System.Drawing.Image)(resources.GetObject("TSMISave.Image")));
            this.TSMISave.Name = "TSMISave";
            this.TSMISave.Size = new System.Drawing.Size(170, 22);
            this.TSMISave.Text = "Enregistrer";
            this.TSMISave.Click += new System.EventHandler(this.TSMISave_Click);
            // 
            // TSMISaveAs
            // 
            this.TSMISaveAs.Name = "TSMISaveAs";
            this.TSMISaveAs.Size = new System.Drawing.Size(170, 22);
            this.TSMISaveAs.Text = "Enregistrer sous ...";
            this.TSMISaveAs.Click += new System.EventHandler(this.TSMISaveAs_Click);
            // 
            // TSMISavePDF
            // 
            this.TSMISavePDF.Image = ((System.Drawing.Image)(resources.GetObject("TSMISavePDF.Image")));
            this.TSMISavePDF.Name = "TSMISavePDF";
            this.TSMISavePDF.Size = new System.Drawing.Size(170, 22);
            this.TSMISavePDF.Text = "Enregistrer en PDF";
            this.TSMISavePDF.Click += new System.EventHandler(this.TSMISavePDF_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(167, 6);
            // 
            // TSMIPrint
            // 
            this.TSMIPrint.Image = ((System.Drawing.Image)(resources.GetObject("TSMIPrint.Image")));
            this.TSMIPrint.Name = "TSMIPrint";
            this.TSMIPrint.Size = new System.Drawing.Size(170, 22);
            this.TSMIPrint.Text = "Imprimer ...";
            this.TSMIPrint.Click += new System.EventHandler(this.TSMIPrint_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(167, 6);
            // 
            // TSMIClose
            // 
            this.TSMIClose.Image = ((System.Drawing.Image)(resources.GetObject("TSMIClose.Image")));
            this.TSMIClose.Name = "TSMIClose";
            this.TSMIClose.Size = new System.Drawing.Size(170, 22);
            this.TSMIClose.Text = "Quitter";
            this.TSMIClose.Click += new System.EventHandler(this.TSMIClose_Click);
            // 
            // editionToolStripMenuItem
            // 
            this.editionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMICut,
            this.TSMICopy,
            this.TSMIPaste,
            this.toolStripSeparator4,
            this.TSMIEmptyClipboard,
            this.toolStripSeparator10,
            this.TSMIOptions,
            this.toolStripSeparator13,
            this.TSMICommandLine});
            this.editionToolStripMenuItem.Name = "editionToolStripMenuItem";
            this.editionToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.editionToolStripMenuItem.Text = "Edition";
            // 
            // TSMICut
            // 
            this.TSMICut.Image = ((System.Drawing.Image)(resources.GetObject("TSMICut.Image")));
            this.TSMICut.Name = "TSMICut";
            this.TSMICut.Size = new System.Drawing.Size(286, 22);
            this.TSMICut.Text = "Couper";
            this.TSMICut.Click += new System.EventHandler(this.TSMICut_Click);
            // 
            // TSMICopy
            // 
            this.TSMICopy.Image = ((System.Drawing.Image)(resources.GetObject("TSMICopy.Image")));
            this.TSMICopy.Name = "TSMICopy";
            this.TSMICopy.Size = new System.Drawing.Size(286, 22);
            this.TSMICopy.Text = "Copier";
            this.TSMICopy.Click += new System.EventHandler(this.TSMICopy_Click);
            // 
            // TSMIPaste
            // 
            this.TSMIPaste.Enabled = false;
            this.TSMIPaste.Image = ((System.Drawing.Image)(resources.GetObject("TSMIPaste.Image")));
            this.TSMIPaste.Name = "TSMIPaste";
            this.TSMIPaste.Size = new System.Drawing.Size(286, 22);
            this.TSMIPaste.Text = "Coller";
            this.TSMIPaste.Click += new System.EventHandler(this.TSMIPaste_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(283, 6);
            // 
            // TSMIEmptyClipboard
            // 
            this.TSMIEmptyClipboard.Enabled = false;
            this.TSMIEmptyClipboard.Image = ((System.Drawing.Image)(resources.GetObject("TSMIEmptyClipboard.Image")));
            this.TSMIEmptyClipboard.Name = "TSMIEmptyClipboard";
            this.TSMIEmptyClipboard.Size = new System.Drawing.Size(286, 22);
            this.TSMIEmptyClipboard.Text = "Vider le presse papier";
            this.TSMIEmptyClipboard.Click += new System.EventHandler(this.TSMIEmptyClipboard_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(283, 6);
            // 
            // TSMIOptions
            // 
            this.TSMIOptions.Image = ((System.Drawing.Image)(resources.GetObject("TSMIOptions.Image")));
            this.TSMIOptions.Name = "TSMIOptions";
            this.TSMIOptions.Size = new System.Drawing.Size(286, 22);
            this.TSMIOptions.Text = "Options";
            this.TSMIOptions.Click += new System.EventHandler(this.TSMIOptions_Click);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(283, 6);
            // 
            // TSMICommandLine
            // 
            this.TSMICommandLine.Image = ((System.Drawing.Image)(resources.GetObject("TSMICommandLine.Image")));
            this.TSMICommandLine.Name = "TSMICommandLine";
            this.TSMICommandLine.Size = new System.Drawing.Size(286, 22);
            this.TSMICommandLine.Text = "Copier la ligne de commande pour MDT";
            this.TSMICommandLine.Click += new System.EventHandler(this.TSMICommandLine_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMIAddMethode,
            this.toolStripSeparator11,
            this.TSMIEditMethode,
            this.TSMIDeleteMethode,
            this.toolStripSeparator8,
            this.TSMIListeMethodes,
            this.toolStripSeparator5,
            this.TSMIRefreshMethodesList,
            this.TSMIReplaceInEdit,
            this.toolStripSeparator9});
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(72, 20);
            this.toolStripMenuItem7.Text = "Méthodes";
            // 
            // TSMIAddMethode
            // 
            this.TSMIAddMethode.Image = ((System.Drawing.Image)(resources.GetObject("TSMIAddMethode.Image")));
            this.TSMIAddMethode.Name = "TSMIAddMethode";
            this.TSMIAddMethode.Size = new System.Drawing.Size(199, 22);
            this.TSMIAddMethode.Text = "Ajouter une méthode ...";
            this.TSMIAddMethode.Click += new System.EventHandler(this.TSMIAddMethode_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(196, 6);
            // 
            // TSMIEditMethode
            // 
            this.TSMIEditMethode.Image = ((System.Drawing.Image)(resources.GetObject("TSMIEditMethode.Image")));
            this.TSMIEditMethode.Name = "TSMIEditMethode";
            this.TSMIEditMethode.Size = new System.Drawing.Size(199, 22);
            this.TSMIEditMethode.Text = "Modifier ...";
            // 
            // TSMIDeleteMethode
            // 
            this.TSMIDeleteMethode.Image = ((System.Drawing.Image)(resources.GetObject("TSMIDeleteMethode.Image")));
            this.TSMIDeleteMethode.Name = "TSMIDeleteMethode";
            this.TSMIDeleteMethode.Size = new System.Drawing.Size(199, 22);
            this.TSMIDeleteMethode.Text = "Supprimer";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(196, 6);
            // 
            // TSMIListeMethodes
            // 
            this.TSMIListeMethodes.Name = "TSMIListeMethodes";
            this.TSMIListeMethodes.Size = new System.Drawing.Size(199, 22);
            this.TSMIListeMethodes.Text = "Insérer une méthode";
            this.TSMIListeMethodes.Click += new System.EventHandler(this.TSMIListeMethodes_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(196, 6);
            // 
            // TSMIRefreshMethodesList
            // 
            this.TSMIRefreshMethodesList.Image = ((System.Drawing.Image)(resources.GetObject("TSMIRefreshMethodesList.Image")));
            this.TSMIRefreshMethodesList.Name = "TSMIRefreshMethodesList";
            this.TSMIRefreshMethodesList.Size = new System.Drawing.Size(199, 22);
            this.TSMIRefreshMethodesList.Text = "Actualiser la liste";
            this.TSMIRefreshMethodesList.Click += new System.EventHandler(this.TSMIRefreshMethodesList_Click);
            // 
            // TSMIReplaceInEdit
            // 
            this.TSMIReplaceInEdit.Enabled = false;
            this.TSMIReplaceInEdit.Image = ((System.Drawing.Image)(resources.GetObject("TSMIReplaceInEdit.Image")));
            this.TSMIReplaceInEdit.Name = "TSMIReplaceInEdit";
            this.TSMIReplaceInEdit.Size = new System.Drawing.Size(199, 22);
            this.TSMIReplaceInEdit.Text = "Remplacer dans Edition";
            this.TSMIReplaceInEdit.Visible = false;
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(196, 6);
            this.toolStripSeparator9.Visible = false;
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMIInsertMethode,
            this.TSMIDeleteListsSelected});
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(54, 20);
            this.toolStripMenuItem8.Text = "Action";
            this.toolStripMenuItem8.Visible = false;
            // 
            // TSMIInsertMethode
            // 
            this.TSMIInsertMethode.Enabled = false;
            this.TSMIInsertMethode.Image = ((System.Drawing.Image)(resources.GetObject("TSMIInsertMethode.Image")));
            this.TSMIInsertMethode.Name = "TSMIInsertMethode";
            this.TSMIInsertMethode.Size = new System.Drawing.Size(296, 22);
            this.TSMIInsertMethode.Text = "Insérer Fonctions-Variables-Exécutions";
            // 
            // TSMIDeleteListsSelected
            // 
            this.TSMIDeleteListsSelected.Enabled = false;
            this.TSMIDeleteListsSelected.Image = ((System.Drawing.Image)(resources.GetObject("TSMIDeleteListsSelected.Image")));
            this.TSMIDeleteListsSelected.Name = "TSMIDeleteListsSelected";
            this.TSMIDeleteListsSelected.Size = new System.Drawing.Size(296, 22);
            this.TSMIDeleteListsSelected.Text = "Supprimer Fonctions-Varaibles-Exécutions";
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMIPreviewScript,
            this.TSMICleanScript,
            this.toolStripSeparator7,
            this.TSMIAddRunBefore,
            this.TSMIAddRunAfter});
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(49, 20);
            this.toolStripMenuItem6.Text = "Script";
            // 
            // TSMIPreviewScript
            // 
            this.TSMIPreviewScript.Enabled = false;
            this.TSMIPreviewScript.Image = ((System.Drawing.Image)(resources.GetObject("TSMIPreviewScript.Image")));
            this.TSMIPreviewScript.Name = "TSMIPreviewScript";
            this.TSMIPreviewScript.Size = new System.Drawing.Size(222, 22);
            this.TSMIPreviewScript.Text = "Aperçu";
            this.TSMIPreviewScript.Visible = false;
            // 
            // TSMICleanScript
            // 
            this.TSMICleanScript.Enabled = false;
            this.TSMICleanScript.Image = ((System.Drawing.Image)(resources.GetObject("TSMICleanScript.Image")));
            this.TSMICleanScript.Name = "TSMICleanScript";
            this.TSMICleanScript.Size = new System.Drawing.Size(222, 22);
            this.TSMICleanScript.Text = "Nettoyer";
            this.TSMICleanScript.Click += new System.EventHandler(this.TSMICleanScript_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(219, 6);
            this.toolStripSeparator7.Visible = false;
            // 
            // TSMIAddRunBefore
            // 
            this.TSMIAddRunBefore.Image = ((System.Drawing.Image)(resources.GetObject("TSMIAddRunBefore.Image")));
            this.TSMIAddRunBefore.Name = "TSMIAddRunBefore";
            this.TSMIAddRunBefore.Size = new System.Drawing.Size(222, 22);
            this.TSMIAddRunBefore.Text = "Ajouter une exécution avant";
            this.TSMIAddRunBefore.Visible = false;
            // 
            // TSMIAddRunAfter
            // 
            this.TSMIAddRunAfter.Image = ((System.Drawing.Image)(resources.GetObject("TSMIAddRunAfter.Image")));
            this.TSMIAddRunAfter.Name = "TSMIAddRunAfter";
            this.TSMIAddRunAfter.Size = new System.Drawing.Size(222, 22);
            this.TSMIAddRunAfter.Text = "Ajouter une exécution après";
            this.TSMIAddRunAfter.Visible = false;
            // 
            // conditionsToolStripMenuItem
            // 
            this.conditionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMIAddCondition,
            this.TSMIImportConditionsReg,
            this.TSMIExportConditionsAs,
            this.TSMIEditCondition,
            this.TSMIRefreshConditionsList,
            this.toolStripSeparator6,
            this.TSMIConditionsList});
            this.conditionsToolStripMenuItem.Name = "conditionsToolStripMenuItem";
            this.conditionsToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.conditionsToolStripMenuItem.Text = "Conditions";
            // 
            // TSMIAddCondition
            // 
            this.TSMIAddCondition.Image = ((System.Drawing.Image)(resources.GetObject("TSMIAddCondition.Image")));
            this.TSMIAddCondition.Name = "TSMIAddCondition";
            this.TSMIAddCondition.Size = new System.Drawing.Size(212, 22);
            this.TSMIAddCondition.Text = "Ajouter une condition ...";
            this.TSMIAddCondition.Click += new System.EventHandler(this.TSMIAddCondition_Click);
            // 
            // TSMIImportConditionsReg
            // 
            this.TSMIImportConditionsReg.Image = ((System.Drawing.Image)(resources.GetObject("TSMIImportConditionsReg.Image")));
            this.TSMIImportConditionsReg.Name = "TSMIImportConditionsReg";
            this.TSMIImportConditionsReg.Size = new System.Drawing.Size(212, 22);
            this.TSMIImportConditionsReg.Text = "Importer des conditions ...";
            this.TSMIImportConditionsReg.Click += new System.EventHandler(this.TSMIImportConditionsReg_Click);
            // 
            // TSMIExportConditionsAs
            // 
            this.TSMIExportConditionsAs.Image = ((System.Drawing.Image)(resources.GetObject("TSMIExportConditionsAs.Image")));
            this.TSMIExportConditionsAs.Name = "TSMIExportConditionsAs";
            this.TSMIExportConditionsAs.Size = new System.Drawing.Size(212, 22);
            this.TSMIExportConditionsAs.Text = "Exporter vos conditions ...";
            this.TSMIExportConditionsAs.Click += new System.EventHandler(this.TSMIExportConditionsAs_Click);
            // 
            // TSMIEditCondition
            // 
            this.TSMIEditCondition.Enabled = false;
            this.TSMIEditCondition.Image = ((System.Drawing.Image)(resources.GetObject("TSMIEditCondition.Image")));
            this.TSMIEditCondition.Name = "TSMIEditCondition";
            this.TSMIEditCondition.Size = new System.Drawing.Size(212, 22);
            this.TSMIEditCondition.Text = "Modifier une condition ...";
            this.TSMIEditCondition.Click += new System.EventHandler(this.TSMIEditCondition_Click);
            // 
            // TSMIRefreshConditionsList
            // 
            this.TSMIRefreshConditionsList.Image = ((System.Drawing.Image)(resources.GetObject("TSMIRefreshConditionsList.Image")));
            this.TSMIRefreshConditionsList.Name = "TSMIRefreshConditionsList";
            this.TSMIRefreshConditionsList.Size = new System.Drawing.Size(212, 22);
            this.TSMIRefreshConditionsList.Text = "Rafraîchir la liste";
            this.TSMIRefreshConditionsList.Click += new System.EventHandler(this.TSMIRefreshConditionsList_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(209, 6);
            // 
            // TSMIConditionsList
            // 
            this.TSMIConditionsList.Name = "TSMIConditionsList";
            this.TSMIConditionsList.Size = new System.Drawing.Size(212, 22);
            this.TSMIConditionsList.Text = "Liste des conditions";
            // 
            // TSMINONAME
            // 
            this.TSMINONAME.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMINews,
            this.TSMIHelp,
            this.toolStripSeparator12,
            this.TSMIAbout});
            this.TSMINONAME.Name = "TSMINONAME";
            this.TSMINONAME.Size = new System.Drawing.Size(43, 20);
            this.TSMINONAME.Text = "Aide";
            // 
            // TSMINews
            // 
            this.TSMINews.Name = "TSMINews";
            this.TSMINews.Size = new System.Drawing.Size(149, 22);
            this.TSMINews.Text = "Nouveautés ...";
            this.TSMINews.Click += new System.EventHandler(this.TSMINews_Click);
            // 
            // TSMIHelp
            // 
            this.TSMIHelp.Name = "TSMIHelp";
            this.TSMIHelp.Size = new System.Drawing.Size(149, 22);
            this.TSMIHelp.Text = "Fichier d\'aide";
            this.TSMIHelp.Click += new System.EventHandler(this.TSMIHelp_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(146, 6);
            // 
            // TSMIAbout
            // 
            this.TSMIAbout.Name = "TSMIAbout";
            this.TSMIAbout.Size = new System.Drawing.Size(149, 22);
            this.TSMIAbout.Text = "A propos";
            this.TSMIAbout.Click += new System.EventHandler(this.TSMIAbout_Click);
            // 
            // printDlg
            // 
            this.printDlg.UseEXDialog = true;
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSSLBPosition,
            this.TSSLBToSave,
            this.TSSLBClipboard,
            this.TSProgressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 691);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1024, 22);
            this.statusStrip1.TabIndex = 49;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // TSSLBPosition
            // 
            this.TSSLBPosition.AutoSize = false;
            this.TSSLBPosition.Name = "TSSLBPosition";
            this.TSSLBPosition.Size = new System.Drawing.Size(200, 17);
            this.TSSLBPosition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TSSLBToSave
            // 
            this.TSSLBToSave.AutoSize = false;
            this.TSSLBToSave.BackColor = System.Drawing.SystemColors.Control;
            this.TSSLBToSave.ForeColor = System.Drawing.SystemColors.ControlText;
            this.TSSLBToSave.Image = ((System.Drawing.Image)(resources.GetObject("TSSLBToSave.Image")));
            this.TSSLBToSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.TSSLBToSave.Margin = new System.Windows.Forms.Padding(10, 3, 0, 2);
            this.TSSLBToSave.Name = "TSSLBToSave";
            this.TSSLBToSave.Size = new System.Drawing.Size(200, 17);
            this.TSSLBToSave.Text = "  Modifications à enregistrer ...";
            this.TSSLBToSave.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.TSSLBToSave.Visible = false;
            // 
            // TSSLBClipboard
            // 
            this.TSSLBClipboard.Image = ((System.Drawing.Image)(resources.GetObject("TSSLBClipboard.Image")));
            this.TSSLBClipboard.Name = "TSSLBClipboard";
            this.TSSLBClipboard.Size = new System.Drawing.Size(16, 17);
            this.TSSLBClipboard.Visible = false;
            // 
            // TSProgressBar
            // 
            this.TSProgressBar.Name = "TSProgressBar";
            this.TSProgressBar.Size = new System.Drawing.Size(100, 16);
            this.TSProgressBar.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(90, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 50;
            this.label1.Text = "label1";
            // 
            // rtbx
            // 
            this.rtbx.AcceptsTab = true;
            this.rtbx.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbx.AutoWordSelection = true;
            this.rtbx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbx.ContextMenuStrip = this.CMS;
            this.rtbx.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbx.Location = new System.Drawing.Point(0, 27);
            this.rtbx.Name = "rtbx";
            this.rtbx.Size = new System.Drawing.Size(1024, 661);
            this.rtbx.TabIndex = 49;
            this.rtbx.Text = "";
            this.rtbx.WordWrap = false;
            this.rtbx.SelectionChanged += new System.EventHandler(this.rtbx_SelectionChanged);
            this.rtbx.TextChanged += new System.EventHandler(this.rtbx_TextChanged);
            this.rtbx.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtbx_KeyDown);
            // 
            // CMS
            // 
            this.CMS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CMS_TSMI_Cut,
            this.CMS_TSMI_Copy,
            this.CMS_TSMI_Paste});
            this.CMS.Name = "CMS";
            this.CMS.Size = new System.Drawing.Size(114, 70);
            // 
            // CMS_TSMI_Cut
            // 
            this.CMS_TSMI_Cut.Image = ((System.Drawing.Image)(resources.GetObject("CMS_TSMI_Cut.Image")));
            this.CMS_TSMI_Cut.Name = "CMS_TSMI_Cut";
            this.CMS_TSMI_Cut.Size = new System.Drawing.Size(113, 22);
            this.CMS_TSMI_Cut.Text = "Couper";
            this.CMS_TSMI_Cut.Click += new System.EventHandler(this.CMS_TSMI_Cut_Click);
            // 
            // CMS_TSMI_Copy
            // 
            this.CMS_TSMI_Copy.Image = ((System.Drawing.Image)(resources.GetObject("CMS_TSMI_Copy.Image")));
            this.CMS_TSMI_Copy.Name = "CMS_TSMI_Copy";
            this.CMS_TSMI_Copy.Size = new System.Drawing.Size(113, 22);
            this.CMS_TSMI_Copy.Text = "Copier";
            this.CMS_TSMI_Copy.Click += new System.EventHandler(this.CMS_TSMI_Copy_Click);
            // 
            // CMS_TSMI_Paste
            // 
            this.CMS_TSMI_Paste.Enabled = false;
            this.CMS_TSMI_Paste.Image = ((System.Drawing.Image)(resources.GetObject("CMS_TSMI_Paste.Image")));
            this.CMS_TSMI_Paste.Name = "CMS_TSMI_Paste";
            this.CMS_TSMI_Paste.Size = new System.Drawing.Size(113, 22);
            this.CMS_TSMI_Paste.Text = "Coller";
            this.CMS_TSMI_Paste.Click += new System.EventHandler(this.CMS_TSMI_Paste_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 713);
            this.Controls.Add(this.rtbx);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Editeur de scripts WSF";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.CMS.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void lbPreview_SelectedIndexChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion
        private System.Windows.Forms.FolderBrowserDialog fbd;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fichierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TSMINew;
        private System.Windows.Forms.ToolStripMenuItem TSMIOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem TSMISave;
        private System.Windows.Forms.ToolStripMenuItem TSMISaveAs;
        private System.Windows.Forms.ToolStripMenuItem TSMISavePDF;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem TSMIPrint;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem TSMIClose;
        private System.Windows.Forms.ToolStripMenuItem editionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TSMICopy;
        private System.Windows.Forms.ToolStripMenuItem TSMICut;
        private System.Windows.Forms.ToolStripMenuItem TSMIPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem TSMIAddMethode;
        private System.Windows.Forms.ToolStripMenuItem TSMIEditMethode;
        private System.Windows.Forms.ToolStripMenuItem TSMIDeleteMethode;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem TSMIRefreshMethodesList;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem TSMIInsertMethode;
        private System.Windows.Forms.ToolStripMenuItem TSMIDeleteListsSelected;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem TSMIPreviewScript;
        private System.Windows.Forms.ToolStripMenuItem TSMICleanScript;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem TSMIAddRunBefore;
        private System.Windows.Forms.ToolStripMenuItem TSMIAddRunAfter;
        private System.Windows.Forms.ToolStripMenuItem conditionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TSMIAddCondition;
        private System.Windows.Forms.ToolStripMenuItem TSMIEditCondition;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem TSMINONAME;
        private System.Windows.Forms.ToolStripMenuItem TSMIRefreshConditionsList;
        private System.Windows.Forms.SaveFileDialog sfd;
        private System.Windows.Forms.PrintDialog printDlg;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.ToolStripMenuItem TSMIEmptyClipboard;
        private System.Windows.Forms.ToolStripMenuItem TSMIImportConditionsReg;
        private System.Windows.Forms.ToolStripMenuItem TSMIExportConditionsAs;
        private System.Windows.Forms.ToolStripMenuItem TSMIConditionsList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem TSMIReplaceInEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem TSMIListeMethodes;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem TSMIOptions;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripStatusLabel TSSLBPosition;
        private System.Windows.Forms.RichTextBox rtbx;
        private System.Windows.Forms.ContextMenuStrip CMS;
        private System.Windows.Forms.ToolStripMenuItem CMS_TSMI_Cut;
        private System.Windows.Forms.ToolStripMenuItem CMS_TSMI_Copy;
        private System.Windows.Forms.ToolStripMenuItem CMS_TSMI_Paste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripStatusLabel TSSLBToSave;
        private System.Windows.Forms.ToolStripStatusLabel TSSLBClipboard;
        private System.Windows.Forms.ToolStripMenuItem TSMINews;
        private System.Windows.Forms.ToolStripMenuItem TSMIHelp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripMenuItem TSMIAbout;
        private System.Windows.Forms.ToolStripProgressBar TSProgressBar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripMenuItem TSMICommandLine;
    }
}

