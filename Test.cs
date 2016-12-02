using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace LWD_DataProcess
{
    public partial class Test : Form
    {
        String pattern = "";
        String example = "";
        public Test()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            pattern = textBox1.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(Regex.IsMatch(example, pattern))
            {
                label3.BackColor = Color.Lime;
                label3.Text = "√";
            }
            else
            {
                label3.BackColor = Color.Red;
                label3.Text = "×";
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            example = textBox2.Text;
        }

        private void Test_Load(object sender, EventArgs e)
        {

        }
    }
}
