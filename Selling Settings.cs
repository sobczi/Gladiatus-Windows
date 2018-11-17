using System;
using System.Windows.Forms;

namespace Gladiatus_35
{
    public partial class Selling_Settings : Form
    {
        public Selling_Settings()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            if(Properties.Settings.Default.itemsSell)
            {
                bronie.Enabled = true;
                tarcze.Enabled = true;
                napierśniki.Enabled = true;
                hełmy.Enabled = true;
                rekawice.Enabled = true;
                buty.Enabled = true;
                pierścienie.Enabled = true;
                amulety.Enabled = true;
                przyszpieszacze.Enabled = true;
                bonusy.Enabled = true;
                błogosławieństwa.Enabled = true;
                zwój.Enabled = true;
            }
            else
            {
                bronie.Enabled = false;
                tarcze.Enabled = false;
                napierśniki.Enabled = false;
                hełmy.Enabled = false;
                rekawice.Enabled = false;
                buty.Enabled = false;
                pierścienie.Enabled = false;
                amulety.Enabled = false;
                przyszpieszacze.Enabled = false;
                bonusy.Enabled = false;
                błogosławieństwa.Enabled = false;
                zwój.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.bronieChecked = bronie.Checked;
            Properties.Settings.Default.tarczeChecked = tarcze.Checked;
            Properties.Settings.Default.napierśnikiChecked= napierśniki.Checked;
            Properties.Settings.Default.hełmyChecked= hełmy.Checked;
            Properties.Settings.Default.rękawiceChecked= rekawice.Checked;
            Properties.Settings.Default.butyChecked = buty.Checked;
            Properties.Settings.Default.pierścienieChecked = pierścienie.Checked;
            Properties.Settings.Default.amuletyChecked = amulety.Checked;
            Properties.Settings.Default.przyspieszaczeChecked= przyszpieszacze.Checked;
            Properties.Settings.Default.bonusyChecked = bonusy.Checked;
            Properties.Settings.Default.błogosławieństwaChecked = błogosławieństwa.Checked;
            Properties.Settings.Default.zwójChecked= zwój.Checked;

            Properties.Settings.Default.bronieSellChecked = bronieSell.Checked;
            Properties.Settings.Default.tarczeSellChecked = tarczeSell.Checked;
            Properties.Settings.Default.napierśnikiSellChecked = napierśnikiSell.Checked;
            Properties.Settings.Default.hełmySellChecked = hełmySell.Checked;
            Properties.Settings.Default.rękawiceSellChecked = rekawiceSell.Checked;
            Properties.Settings.Default.butySellChecked = butySell.Checked;
            Properties.Settings.Default.pierścienieSellChecked = pierścienieSell.Checked;
            Properties.Settings.Default.amuletySellChecked = amuletySell.Checked;
            Properties.Settings.Default.przyspieszaceSellChecked= przyspieszaczeSell.Checked;
            Properties.Settings.Default.bonusySellChecked = bonusySell.Checked;
            Properties.Settings.Default.błogosławieństwaSellChecked = błogosławieństwaSell.Checked;
            Properties.Settings.Default.zwójSellChecked = zwójSell.Checked;

            Properties.Settings.Default.Save();
            Form1.update_data = true;
            this.Close();
            }

        private void Form2_Load(object sender, EventArgs e)
        {
            bronie.Checked = Properties.Settings.Default.bronieChecked;
            tarcze.Checked = Properties.Settings.Default.tarczeChecked;
            napierśniki.Checked = Properties.Settings.Default.napierśnikiChecked;
            hełmy.Checked = Properties.Settings.Default.hełmyChecked;
            rekawice.Checked = Properties.Settings.Default.rękawiceChecked;
            buty.Checked = Properties.Settings.Default.butyChecked;
            pierścienie.Checked = Properties.Settings.Default.pierścienieChecked;
            amulety.Checked = Properties.Settings.Default.amuletyChecked;
            przyszpieszacze.Checked = Properties.Settings.Default.przyspieszaczeChecked;
            bonusy.Checked = Properties.Settings.Default.bonusyChecked;
            błogosławieństwa.Checked = Properties.Settings.Default.błogosławieństwaChecked;
            zwój.Checked = Properties.Settings.Default.zwójChecked;

            bronieSell.Checked = Properties.Settings.Default.bronieSellChecked;
            tarczeSell.Checked = Properties.Settings.Default.tarczeSellChecked;
            napierśnikiSell.Checked = Properties.Settings.Default.napierśnikiSellChecked;
            hełmySell.Checked = Properties.Settings.Default.hełmySellChecked;
            rekawiceSell.Checked = Properties.Settings.Default.rękawiceSellChecked;
            butySell.Checked = Properties.Settings.Default.butySellChecked;
            pierścienieSell.Checked = Properties.Settings.Default.pierścienieSellChecked;
            amuletySell.Checked = Properties.Settings.Default.amuletySellChecked;
            przyspieszaczeSell.Checked = Properties.Settings.Default.przyspieszaceSellChecked;
            bonusySell.Checked = Properties.Settings.Default.bonusySellChecked;
            błogosławieństwaSell.Checked = Properties.Settings.Default.błogosławieństwaSellChecked;
            zwójSell.Checked = Properties.Settings.Default.zwójSellChecked;
        }
    }
}
