using System;
using System.Windows.Forms;

namespace Gladiatus_35
{
    public partial class General_Settings : Form
    {
        public General_Settings()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.nickname = textBox1.Text;
            Properties.Settings.Default.password = textBox2.Text;
            Properties.Settings.Default.worldOption = comboBox1.SelectedIndex;
            Properties.Settings.Default.headless = checkBox2.Checked;
            Properties.Settings.Default.main_bot = checkBox1.Checked;
            Properties.Settings.Default.Save();
            Form1.update_data = true;
            this.Close();
        }

        private void General_Settings_Load(object sender, EventArgs e)
        {
            textBox1.Text = Properties.Settings.Default.nickname;
            textBox2.Text = Properties.Settings.Default.password;
            comboBox1.SelectedIndex = Properties.Settings.Default.worldOption;
            checkBox1.Checked = Properties.Settings.Default.main_bot;
            checkBox2.Checked = Properties.Settings.Default.headless;
        }
    }
}
