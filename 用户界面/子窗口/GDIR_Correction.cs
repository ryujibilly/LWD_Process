using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LWD_DataProcess
{
    public partial class GDIR_Correction : Form
    {
        #region 字段
        private float mudRes;
        private float borehole;
        private float tb;
        private float sbr;
        #endregion
        public GDIR_Correction()
        {
            InitializeComponent();
        }

        private void GDIR_Correction_Load(object sender, EventArgs e)
        {

        }

        private void button_SelectFolder_Click(object sender, EventArgs e)
        {

        }

        private void button_Load_Click(object sender, EventArgs e)
        {

        }

        private void comboBox_ToolSize_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBox_Save2Root_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox_WellName_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox_Folder_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox_BoreHole_SelectedIndexChanged(object sender, EventArgs e)
        {
            borehole = float.Parse(comboBox_BoreHole.SelectedText);
            GDIR._gdir.Borehole = borehole;
            Config.CfgInfo.GDIR_Borehole = GDIR._gdir.Borehole;
            Properties.Settings.Default.GDIR_BoreHole = borehole.ToString("F3");
        }

        private void numericUpDown_MudResistivity_ValueChanged(object sender, EventArgs e)
        {
            mudRes = (float)numericUpDown_MudResistivity.Value;
            GDIR._gdir.MudRes = mudRes;
            Config.CfgInfo.GDIR_MudRes = mudRes;
            Properties.Settings.Default.GDIR_MudRes = numericUpDown_MudResistivity.Value;
        }

        private void numericUpDown_SBR_ValueChanged(object sender, EventArgs e)
        {
            sbr = (float)numericUpDown_SBR.Value;
            GDIR._gdir.Sbr = sbr;
            Config.CfgInfo.GDIR_SBR = sbr;
            Properties.Settings.Default.GDIR_SBR = numericUpDown_SBR.Value;
        }

        private void numericUpDown_BedThickness_ValueChanged(object sender, EventArgs e)
        {
            tb = (float)numericUpDown_BedThickness.Value;
            GDIR._gdir.Tb = tb;
            Config.CfgInfo.GDIR_Tb = sbr;
            Properties.Settings.Default.GDIR_Tb = numericUpDown_BedThickness.Value;
        }
    }
}
