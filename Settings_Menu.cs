using System;
using System.Windows.Forms;

namespace Gladiatus_35
{
    public partial class Settings_Menu : Form
    {
        public Settings_Menu()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            General_Settings gSettings = new General_Settings();
            gSettings.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Selling_Settings sSettings = new Selling_Settings();
            sSettings.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Botting_Settings bSettings = new Botting_Settings();
            bSettings.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Debugging_Mode dMode = new Debugging_Mode();
            dMode.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Buying_Settings bSettings = new Buying_Settings();
            bSettings.Show();
        }
    }
}
