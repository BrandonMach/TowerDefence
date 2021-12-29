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
    public partial class Form1 : Form
    {

       
        public string PlayerName
        {
            get;
            private set;
        }


        public Form1()
        {
            InitializeComponent();
            PlayerName = ""; //Empty string in the beginig
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Det som står i test boxen
            PlayerName = textBoxName.Text;
            this.Hide();
            
        }
    }
}
