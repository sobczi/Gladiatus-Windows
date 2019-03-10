using System;
using System.Windows.Forms;

namespace Gladiatus_35
{
    public partial class Botting_Settings : Form
    {
        public Botting_Settings()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void Botting_Settings_Load(object sender, EventArgs e)
        {
            if (checkBox8.Checked)
            {
                checkBox9.Enabled = true;
                checkBox18.Enabled = true;
                checkBox19.Enabled = true;
            }
            else
            {
                checkBox9.Enabled = false;
                checkBox18.Enabled = false;
                checkBox19.Enabled = false;
            }

            checkBox12.Checked = Properties.Settings.Default.farm_arenas;
            checkBox2.Checked = Properties.Settings.Default.sleepModeChecked;
            textBox2.Enabled = checkBox5.Checked = Properties.Settings.Default.pakujChecked;
            checkBox6.Checked = Properties.Settings.Default.arena35Checked;
            checkBox7.Checked = Properties.Settings.Default.circusTurma35Checked;
            checkBox10.Checked = Properties.Settings.Default.itemsSell;
            checkBox11.Checked = Properties.Settings.Default.moveFood;
            checkBox13.Checked = Properties.Settings.Default.eventWar;
            comboBox1.Enabled = checkBox15.Checked = Properties.Settings.Default.expeditionsChecked;
            comboBox2.Enabled = checkBox14.Checked = Properties.Settings.Default.dungeonsChecked;
            comboBox3.Enabled = checkBox16.Checked = Properties.Settings.Default.trainingChecked;
            comboBox6.Enabled = checkBox17.Checked = Properties.Settings.Default.extractItems;
            textBox1.Enabled = checkBox1.Checked = Properties.Settings.Default.gold_limit;
            textBox1.Text = Properties.Settings.Default.gold_level;
            textBox2.Text = Properties.Settings.Default.minimum_gold_pack;
            checkBox3.Checked = textBox3.Enabled = Properties.Settings.Default.heal_me;
            checkBox4.Checked = Properties.Settings.Default.send_components;
            checkBox8.Checked = Properties.Settings.Default.get_for_extract;
            checkBox9.Checked = Properties.Settings.Default.purple_extracting;
            checkBox18.Checked = Properties.Settings.Default.orange_extracing;
            checkBox19.Checked = Properties.Settings.Default.red_extracting;
            checkBox20.Checked = Properties.Settings.Default.hades;

            comboBox1.SelectedIndex = Properties.Settings.Default.expeditionOption;
            comboBox2.SelectedIndex = Properties.Settings.Default.dungeonsOption;
            comboBox3.SelectedIndex = Properties.Settings.Default.trainingOption;
            comboBox4.SelectedIndex = Properties.Settings.Default.foodBackpack;
            comboBox5.SelectedIndex = Properties.Settings.Default.itemsBackpack;
            comboBox6.SelectedIndex = Properties.Settings.Default.extractBackpack;

            textBox3.Text = Convert.ToString(Properties.Settings.Default.healthLevel);
            textBox4.Enabled = Properties.Settings.Default.hades;
            textBox4.Text = Convert.ToString(Properties.Settings.Default.maximum_rubles_hades);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.send_components = checkBox4.Checked;
            Properties.Settings.Default.farm_arenas = checkBox12.Checked;
            Properties.Settings.Default.extractItems = checkBox17.Checked;
            Properties.Settings.Default.foodBackpack = comboBox4.SelectedIndex;
            Properties.Settings.Default.itemsBackpack = comboBox5.SelectedIndex;
            Properties.Settings.Default.extractBackpack = comboBox6.SelectedIndex;
            Properties.Settings.Default.sleepModeChecked = checkBox2.Checked;
            Properties.Settings.Default.pakujChecked = checkBox5.Checked;
            Properties.Settings.Default.arena35Checked = checkBox6.Checked;
            Properties.Settings.Default.circusTurma35Checked = checkBox7.Checked;
            Properties.Settings.Default.itemsSell = checkBox10.Checked;
            Properties.Settings.Default.moveFood = checkBox11.Checked;
            Properties.Settings.Default.eventWar = checkBox13.Checked;
            Properties.Settings.Default.expeditionsChecked = checkBox15.Checked;
            Properties.Settings.Default.dungeonsChecked = checkBox14.Checked;
            Properties.Settings.Default.trainingChecked = checkBox16.Checked;
            Properties.Settings.Default.expeditionOption = comboBox1.SelectedIndex;
            Properties.Settings.Default.dungeonsOption = comboBox2.SelectedIndex;
            Properties.Settings.Default.trainingOption = comboBox3.SelectedIndex;
            Properties.Settings.Default.minimum_gold_pack = textBox2.Text;
            Properties.Settings.Default.healthLevel = Convert.ToInt32(textBox3.Text);
            Properties.Settings.Default.heal_me = checkBox3.Checked;
            Properties.Settings.Default.hades = checkBox20.Checked;

            if (Properties.Settings.Default.get_for_extract)
            {
                Properties.Settings.Default.get_for_extract = checkBox8.Checked;
                Properties.Settings.Default.purple_extracting = checkBox9.Checked;
                Properties.Settings.Default.orange_extracing = checkBox18.Checked;
                Properties.Settings.Default.red_extracting = checkBox19.Checked;
            }
            else
                Properties.Settings.Default.get_for_extract = false;

            Properties.Settings.Default.gold_limit = checkBox1.Checked;
            Properties.Settings.Default.gold_level = textBox1.Text;
            Properties.Settings.Default.maximum_rubles_hades = Convert.ToInt32(textBox4.Text);

            Properties.Settings.Default.Save();

            this.Close();
        }

        private void checkBox17_CheckedChanged(object sender, EventArgs e)
        {
            if(!checkBox17.Checked)
            { comboBox6.Enabled = false; }
            else { comboBox6.Enabled = true; }
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void checkBox15_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox15.Checked)
            { comboBox1.Enabled = false; }
            else { comboBox1.Enabled = true; }
        }

        private void checkBox14_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox14.Checked)
            { comboBox2.Enabled = false; }
            else { comboBox2.Enabled = true; }
        }

        private void checkBox16_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox16.Checked)
            { comboBox3.Enabled = false; }
            else { comboBox3.Enabled = true; }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked) { textBox1.Enabled = true; }
            else { textBox1.Enabled = false; }
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

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox5.Checked)
            { textBox2.Enabled = true; }
            else { textBox2.Enabled = false; }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            { e.Handled = true; }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox3.Checked) { textBox3.Enabled = true; comboBox4.Enabled = true; }
            else if(!checkBox11.Checked && !checkBox3.Checked) { comboBox4.Enabled = false; textBox3.Enabled = false; }
            else { textBox3.Enabled = false; }
        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox12.Checked && !checkBox7.Checked && !checkBox6.Checked)
            { MessageBox.Show("You should choose at least one arena to farm!"); }
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox11.Checked) { comboBox4.Enabled = true; }
            else if(!checkBox11.Checked && !checkBox3.Checked) { comboBox4.Enabled = false; }
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox8.Checked)
            {
                checkBox9.Enabled = true;
                checkBox18.Enabled = true;
                checkBox19.Enabled = true;
            }
            else
            {
                checkBox9.Enabled = false;
                checkBox18.Enabled =false;
                checkBox19.Enabled =false;
            }
        }

        private void checkBox20_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox20.Checked)
                textBox4.Enabled = true;
            else
                textBox4.Enabled = false;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            { e.Handled = true; }
        }
    }
}
