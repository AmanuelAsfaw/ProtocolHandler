using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
        }
        public void ReceiveValue(string name, string passedValue)
        {
            MessageBox.Show($"{name}: {passedValue}");
            textBox1.Text = passedValue.ToString();
            // Use the value as needed
        }
    }
}
