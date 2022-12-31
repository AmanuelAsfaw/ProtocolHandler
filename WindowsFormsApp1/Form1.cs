using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private string[] argsList;
        private string str_text = "";
        public Form1(string[] args)
        {
            argsList = args;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach(string arg in argsList)
            {
                str_text = str_text + arg;
            }
            label1.Text = str_text;
        }
    }
}
