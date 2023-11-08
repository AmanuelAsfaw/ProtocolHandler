using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Properties;

namespace WindowsFormsApp1
{
    public partial class Albet : Form
    {
        private string[] argsList;
        private string str_text = "";
        public bool isShow = false;
        public Albet(string[] args = null)
        {
            if(args != null)
            {
                argsList = args;
            }
            else
            {
                argsList= new string[0];
            }
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public  void changeText(string text)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AlbetSettings.Default.printer_name = textBox1.Text.ToString();
            AlbetSettings.Default.Save();
        }

        private void getButton_Click(object sender, EventArgs e)
        {
            label1.Text = AlbetSettings.Default.printer_name.ToString();
        }
    }
}
