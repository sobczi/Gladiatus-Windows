using System;
using System.Windows.Forms;

namespace Gladiatus_35
{
    public partial class Buying_Settings : Form
    {
        public Buying_Settings()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void Buying_Settings_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = Properties.Settings.Default.buyRings;
            checkBox2.Checked = Properties.Settings.Default.buyAmulets;
            checkBox3.Checked = Properties.Settings.Default.buyBoosters;
            checkBox4.Checked = Properties.Settings.Default.buyFood;
            textBox1.Text = Convert.ToString(Properties.Settings.Default.foodPack);
            textBox2.Text = Convert.ToString(Properties.Settings.Default.differencePrice);
            textBox3.Text = Convert.ToString(Properties.Settings.Default.boostersPack);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.buyRings = checkBox1.Checked;
            Properties.Settings.Default.buyAmulets = checkBox2.Checked;
            Properties.Settings.Default.buyBoosters = checkBox3.Checked;
            Properties.Settings.Default.buyFood = checkBox4.Checked;
            Properties.Settings.Default.foodPack = Convert.ToInt32(textBox1.Text);
            Properties.Settings.Default.differencePrice = Convert.ToInt32(textBox2.Text);
            Properties.Settings.Default.boostersPack = Convert.ToInt32(textBox3.Text);

            Properties.Settings.Default.Save();
            Form1.update_data = true;
            this.Close();
        }

        private void CheckAny()
        {
            if(!checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked && !checkBox4.Checked)
            { textBox2.Enabled = false; }
            else { textBox2.Enabled = true; }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckAny();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            CheckAny();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            CheckAny();
            if(checkBox3.Checked)
            { textBox3.Enabled = true; }
            else { textBox3.Enabled = false; }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            CheckAny();
            if(checkBox4.Checked)
            { textBox1.Enabled = true; }
            else { textBox1.Enabled = false; }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            { e.Handled = true; }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            { e.Handled = true; }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            { e.Handled = true; }
        }
    }
}
