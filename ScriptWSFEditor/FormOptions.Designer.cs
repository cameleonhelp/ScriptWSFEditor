namespace ScriptWSFEditor
{
    partial class FormOptions
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOptions));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtWSFkeyWords = new System.Windows.Forms.TextBox();
            this.txtVBKeyWords = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSeparatorKeywords = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtKeywordsHighlight = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPathMethode = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTemplateFile = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnCloseError = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.txtCmdLineForMDT = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(762, 459);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(91, 33);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Annuler";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(665, 459);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(91, 33);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Valider";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Mots clés du script WSF : ";
            // 
            // txtWSFkeyWords
            // 
            this.txtWSFkeyWords.ForeColor = System.Drawing.Color.DarkRed;
            this.txtWSFkeyWords.Location = new System.Drawing.Point(248, 13);
            this.txtWSFkeyWords.Multiline = true;
            this.txtWSFkeyWords.Name = "txtWSFkeyWords";
            this.txtWSFkeyWords.Size = new System.Drawing.Size(606, 77);
            this.txtWSFkeyWords.TabIndex = 3;
            // 
            // txtVBKeyWords
            // 
            this.txtVBKeyWords.ForeColor = System.Drawing.Color.Blue;
            this.txtVBKeyWords.Location = new System.Drawing.Point(248, 96);
            this.txtVBKeyWords.Multiline = true;
            this.txtVBKeyWords.Name = "txtVBKeyWords";
            this.txtVBKeyWords.Size = new System.Drawing.Size(606, 77);
            this.txtVBKeyWords.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Mots clés Visual Basic : ";
            // 
            // txtSeparatorKeywords
            // 
            this.txtSeparatorKeywords.ForeColor = System.Drawing.Color.Blue;
            this.txtSeparatorKeywords.Location = new System.Drawing.Point(248, 179);
            this.txtSeparatorKeywords.Multiline = true;
            this.txtSeparatorKeywords.Name = "txtSeparatorKeywords";
            this.txtSeparatorKeywords.Size = new System.Drawing.Size(606, 77);
            this.txtSeparatorKeywords.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 179);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(151, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Mots clés Séparateurs : ";
            // 
            // txtKeywordsHighlight
            // 
            this.txtKeywordsHighlight.BackColor = System.Drawing.Color.Yellow;
            this.txtKeywordsHighlight.ForeColor = System.Drawing.Color.Red;
            this.txtKeywordsHighlight.Location = new System.Drawing.Point(248, 262);
            this.txtKeywordsHighlight.Multiline = true;
            this.txtKeywordsHighlight.Name = "txtKeywordsHighlight";
            this.txtKeywordsHighlight.Size = new System.Drawing.Size(606, 77);
            this.txtKeywordsHighlight.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 262);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(164, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "Mots clés en surbrillance : ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Yellow;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(13, 287);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "[EXEMPLE]";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Blue;
            this.label6.Location = new System.Drawing.Point(13, 204);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 16);
            this.label6.TabIndex = 11;
            this.label6.Text = "EXAMPLE < >";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Blue;
            this.label7.Location = new System.Drawing.Point(13, 121);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(149, 16);
            this.label7.TabIndex = 12;
            this.label7.Text = "EXAMPLE If Then End If";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.DarkRed;
            this.label8.Location = new System.Drawing.Point(12, 38);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(141, 16);
            this.label8.TabIndex = 13;
            this.label8.Text = "EXAMPLE script job id";
            // 
            // txtPathMethode
            // 
            this.txtPathMethode.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtPathMethode.Location = new System.Drawing.Point(248, 349);
            this.txtPathMethode.Name = "txtPathMethode";
            this.txtPathMethode.Size = new System.Drawing.Size(569, 22);
            this.txtPathMethode.TabIndex = 15;
            this.txtPathMethode.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 352);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(229, 16);
            this.label9.TabIndex = 14;
            this.label9.Text = "Chemin de stockage des méthodes : ";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // txtTemplateFile
            // 
            this.txtTemplateFile.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtTemplateFile.Location = new System.Drawing.Point(248, 388);
            this.txtTemplateFile.Name = "txtTemplateFile";
            this.txtTemplateFile.Size = new System.Drawing.Size(569, 22);
            this.txtTemplateFile.TabIndex = 17;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 391);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(115, 16);
            this.label10.TabIndex = 16;
            this.label10.Text = "Modèle à utiliser : ";
            // 
            // btnCloseError
            // 
            this.btnCloseError.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCloseError.Image = ((System.Drawing.Image)(resources.GetObject("btnCloseError.Image")));
            this.btnCloseError.Location = new System.Drawing.Point(823, 345);
            this.btnCloseError.Name = "btnCloseError";
            this.btnCloseError.Size = new System.Drawing.Size(30, 30);
            this.btnCloseError.TabIndex = 38;
            this.btnCloseError.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCloseError.UseVisualStyleBackColor = true;
            this.btnCloseError.Click += new System.EventHandler(this.btnCloseError_Click);
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(823, 384);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(30, 30);
            this.button1.TabIndex = 39;
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ofd
            // 
            this.ofd.DefaultExt = "wsft";
            this.ofd.Filter = "Modèles WSF (*.wsft)|*.wsft |Script WSF (*.wsf)|*.wsf";
            this.ofd.InitialDirectory = "Application.StartPath + \"\\\\methodes\"";
            this.ofd.Title = "Choisir le fichier modèle à utiliser";
            // 
            // txtCmdLineForMDT
            // 
            this.txtCmdLineForMDT.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtCmdLineForMDT.Location = new System.Drawing.Point(248, 425);
            this.txtCmdLineForMDT.Name = "txtCmdLineForMDT";
            this.txtCmdLineForMDT.Size = new System.Drawing.Size(605, 22);
            this.txtCmdLineForMDT.TabIndex = 41;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(12, 425);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(230, 37);
            this.label11.TabIndex = 40;
            this.label11.Text = "Modèle de la ligne de commande MDT : ";
            // 
            // FormOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 504);
            this.ControlBox = false;
            this.Controls.Add(this.txtCmdLineForMDT);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnCloseError);
            this.Controls.Add(this.txtTemplateFile);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtPathMethode);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtKeywordsHighlight);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtSeparatorKeywords);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtVBKeyWords);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtWSFkeyWords);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormOptions";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.FormOptions_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtWSFkeyWords;
        private System.Windows.Forms.TextBox txtVBKeyWords;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSeparatorKeywords;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtKeywordsHighlight;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPathMethode;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtTemplateFile;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnCloseError;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FolderBrowserDialog fbd;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.TextBox txtCmdLineForMDT;
        private System.Windows.Forms.Label label11;
    }
}