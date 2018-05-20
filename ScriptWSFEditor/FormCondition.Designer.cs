namespace ScriptWSFEditor
{
    partial class FormCondition
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCondition));
            this.btnSave = new System.Windows.Forms.Button();
            this.txtCondition = new System.Windows.Forms.TextBox();
            this.txtConditionName = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlNewCondition = new System.Windows.Forms.Panel();
            this.pnlEditCondition = new System.Windows.Forms.Panel();
            this.cboConditions = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtKeyWords = new System.Windows.Forms.TextBox();
            this.pnlNewCondition.SuspendLayout();
            this.pnlEditCondition.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(12, 260);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(205, 38);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "        Enregistrer la condition";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtCondition
            // 
            this.txtCondition.AcceptsReturn = true;
            this.txtCondition.AcceptsTab = true;
            this.txtCondition.Location = new System.Drawing.Point(12, 58);
            this.txtCondition.Multiline = true;
            this.txtCondition.Name = "txtCondition";
            this.txtCondition.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCondition.Size = new System.Drawing.Size(416, 157);
            this.txtCondition.TabIndex = 57;
            this.txtCondition.TextChanged += new System.EventHandler(this.txtCondition_TextChanged);
            // 
            // txtConditionName
            // 
            this.txtConditionName.Location = new System.Drawing.Point(58, 12);
            this.txtConditionName.Name = "txtConditionName";
            this.txtConditionName.Size = new System.Drawing.Size(315, 22);
            this.txtConditionName.TabIndex = 58;
            this.txtConditionName.Text = "Nom_de_la_condition";
            this.txtConditionName.TextChanged += new System.EventHandler(this.txtConditionName_TextChanged);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(223, 260);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(205, 38);
            this.btnClose.TabIndex = 59;
            this.btnClose.Text = "       Fermer";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 16);
            this.label3.TabIndex = 60;
            this.label3.Text = "Nom :";
            // 
            // pnlNewCondition
            // 
            this.pnlNewCondition.Controls.Add(this.label3);
            this.pnlNewCondition.Controls.Add(this.txtConditionName);
            this.pnlNewCondition.Location = new System.Drawing.Point(2, 3);
            this.pnlNewCondition.Name = "pnlNewCondition";
            this.pnlNewCondition.Size = new System.Drawing.Size(412, 48);
            this.pnlNewCondition.TabIndex = 61;
            // 
            // pnlEditCondition
            // 
            this.pnlEditCondition.Controls.Add(this.cboConditions);
            this.pnlEditCondition.Controls.Add(this.label1);
            this.pnlEditCondition.Location = new System.Drawing.Point(2, 4);
            this.pnlEditCondition.Name = "pnlEditCondition";
            this.pnlEditCondition.Size = new System.Drawing.Size(412, 48);
            this.pnlEditCondition.TabIndex = 62;
            // 
            // cboConditions
            // 
            this.cboConditions.FormattingEnabled = true;
            this.cboConditions.Location = new System.Drawing.Point(85, 12);
            this.cboConditions.Name = "cboConditions";
            this.cboConditions.Size = new System.Drawing.Size(288, 24);
            this.cboConditions.TabIndex = 54;
            this.cboConditions.SelectedIndexChanged += new System.EventHandler(this.cboConditions_SelectedIndexChanged);
            this.cboConditions.SelectedValueChanged += new System.EventHandler(this.cboConditions_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 16);
            this.label1.TabIndex = 53;
            this.label1.Text = "Conditions :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 228);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 16);
            this.label2.TabIndex = 63;
            this.label2.Text = "Mots clés :";
            // 
            // txtKeyWords
            // 
            this.txtKeyWords.Location = new System.Drawing.Point(89, 225);
            this.txtKeyWords.Name = "txtKeyWords";
            this.txtKeyWords.Size = new System.Drawing.Size(339, 22);
            this.txtKeyWords.TabIndex = 64;
            // 
            // FormCondition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 310);
            this.ControlBox = false;
            this.Controls.Add(this.txtKeyWords);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pnlEditCondition);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtCondition);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.pnlNewCondition);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormCondition";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Gestionnaire des conditions";
            this.Load += new System.EventHandler(this.FormCondition_Load);
            this.pnlNewCondition.ResumeLayout(false);
            this.pnlNewCondition.PerformLayout();
            this.pnlEditCondition.ResumeLayout(false);
            this.pnlEditCondition.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtCondition;
        private System.Windows.Forms.TextBox txtConditionName;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnlNewCondition;
        private System.Windows.Forms.Panel pnlEditCondition;
        private System.Windows.Forms.ComboBox cboConditions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtKeyWords;
    }
}