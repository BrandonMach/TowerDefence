using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm
{
    public partial class NameMenu : Form
    {

        public string PlayerName
        {
            get;
            private set;
        }
        public NameMenu()
        {
            InitializeComponent();
            PlayerName = "";
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            PlayerName = textBoxName.Text;
            this.Hide();
        }
    }
}
