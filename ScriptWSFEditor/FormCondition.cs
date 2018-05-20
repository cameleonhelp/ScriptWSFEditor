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
    public partial class FormCondition : Form
    {
        public FormCondition()
        {
            InitializeComponent();
        }

        private void FormCondition_Load(object sender, EventArgs e)
        {
            if (CommunSNCF.IsNewCondition)
            {
                pnlEditCondition.Visible = false;
                pnlNewCondition.Visible = true;
                btnSave.Enabled = false;
                txtKeyWords.Text = "ex. If (,Then,Else,End If";
            } else
            {
                List<string> lConditions = CommunSNCF.ReadRegAllSubkeys();
                lConditions.Add("");
                lConditions.Sort();
                string[] Conditions = lConditions.ToArray();
                cboConditions.DataSource = Conditions;
                pnlEditCondition.Visible = true;
                pnlNewCondition.Visible = false;
                btnSave.Enabled = false;
                txtKeyWords.Text = "";
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Fermeture des conditions");
            this.Close();
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Enregistrement des conditions");
            try
            {
                if (CommunSNCF.IsNewCondition)
                {
                    string HKCUConditions = CommunSNCF.HKLMApplication + @"\Conditions\";
                    CommunSNCF.SaveRegParam(HKCUConditions, txtConditionName.Text, String.Join("|", txtCondition.Lines));
                    string HKCUConditionsKeyWords = CommunSNCF.HKLMApplication + @"\Conditions_KeyWords\";
                    CommunSNCF.SaveRegParam(HKCUConditionsKeyWords, txtConditionName.Text, txtKeyWords.Text);
                    SetKeyWordsInRegistry();
                    FormMain fm = new FormMain();
                    fm.InsertConditionsInMenu(true);
                }
                else
                {
                    string HKCUConditions = CommunSNCF.HKLMApplication + @"\Conditions\";
                    CommunSNCF.SaveRegParam(HKCUConditions, cboConditions.Text, String.Join("|", txtCondition.Lines));
                    string HKCUConditionsKeyWords = CommunSNCF.HKLMApplication + @"\Conditions_KeyWords\";
                    CommunSNCF.SaveRegParam(HKCUConditionsKeyWords, cboConditions.Text, txtKeyWords.Text);
                    SetKeyWordsInRegistry();
                }
            } catch(Exception ex)
            {
                CommunSNCF.Log((int)CommunSNCF.logtype.Error, "Enregistrement des conditions " + ex.Message);
            }
            finally
            {
                this.Close();
            }
        }

        public string SetKeyWordsInRegistry()
        {
            CommunSNCF.Log((int)CommunSNCF.logtype.Information, "Sauvegarde de mots clés des conditions");
            string result = CommunSNCF.ReadRegParam("ConditionsKeyWords");
            string[] allSubKeyKeyWords = CommunSNCF.ReadRegAllSubkeys(CommunSNCF.HKLMApplication.Replace("HKEY_CURRENT_USER\\", "") + "\\Conditions_KeyWords");
            foreach (string keyword in allSubKeyKeyWords)
            {
                string lKeyWords = CommunSNCF.ReadRegParam(CommunSNCF.HKLMApplication + "\\Conditions_KeyWords", keyword);
                foreach (string word in lKeyWords.Split(','))
                {
                    if (!result.Contains(word))
                    {
                        if (result == "")
                            result = word;
                        else
                            result += "," + word;
                    }
                }
            }
            return result;
        }

        private void txtConditionName_TextChanged(object sender, EventArgs e)
        {
            if (CommunSNCF.IsNewCondition)
            {
                if (txtConditionName.Text != "" && txtCondition.Text != "")
                    btnSave.Enabled = true;
                else
                    btnSave.Enabled = false;
            } else
            {
                if (cboConditions.Text != "" && txtCondition.Text != "")
                    btnSave.Enabled = true;
                else
                    btnSave.Enabled = false;
            }
        }

        private void txtCondition_TextChanged(object sender, EventArgs e)
        {
            if (CommunSNCF.IsNewCondition)
            {
                if (txtConditionName.Text != "" && txtCondition.Text != "")
                    btnSave.Enabled = true;
                else
                    btnSave.Enabled = false;
            }
            else
            {
                if (cboConditions.Text != "" && txtCondition.Text != "")
                    btnSave.Enabled = true;
                else
                    btnSave.Enabled = false;
            }
        }

        private void cboConditions_SelectedValueChanged(object sender, EventArgs e)
        {
            if(cboConditions.SelectedItem.ToString() != "")
            {
                txtKeyWords.Text = "";
                string content = CommunSNCF.ReadRegParam(CommunSNCF.HKLMApplication + "\\Conditions", cboConditions.SelectedItem.ToString());
                txtCondition.Lines = content.Split('|');
                string keywords = CommunSNCF.ReadRegParam(CommunSNCF.HKLMApplication + "\\Conditions_KeyWords", cboConditions.SelectedItem.ToString());
                txtKeyWords.Text = keywords;
            }
            else
            {
                btnSave.Enabled = false;
                txtCondition.Text = "";
                txtKeyWords.Text = "";
            }
        }

        private void cboConditions_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
