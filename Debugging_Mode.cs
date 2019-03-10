using System;
using System.Windows.Forms;

namespace Gladiatus_35
{
    public partial class Debugging_Mode : Form
    {
        public Debugging_Mode()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form1.killEverything = false;
            Application.Exit();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if(Form1.startChrome){ Form1.startChrome = false; button9.Text = "START CHROME: OFF"; }
            else{ Form1.startChrome = true; button9.Text = "START CHROME: ON"; }
            this.Close();
        }

        private void Debugging_Mode_Load(object sender, EventArgs e)
        {
            if (!Form1.startChrome) { button9.Text = "START CHROME: OFF"; }
        }
    }
}
