using System;
using System.Windows.Forms;
using System.Threading;
using OpenQA.Selenium.Chrome;
using System.Diagnostics;
using System.Reflection;

namespace Gladiatus_35
{
    public partial class Form1 : Form
    {
        #region VARIABLES
        /*
        public static bool expedition_checked;
        public static bool dungeon_checked;
        public static bool update_data;

        public static string server_string;
        private static Tasks _Tasks;
        private static Tests _Tests;*/
        #endregion
        public static bool killEverything = true;
        public static bool turn_off_func = false;
        public static bool turn_off_notification = false;
        public static int notification_type = -1;
        public static string currently_running;
        public static bool british_land;
        public static bool botAction = true;
        public static bool buyRingsAction = false;
        public static bool moveGoldAction = false;
        public static bool takeGoldAction = false;
        public static bool downloadPackages = false;
        public static bool sellItemsAction = false;
        public static bool startChrome = true;
        public static bool hades;
        public static bool bot_running;
        public static bool turn_off;
        public static ChromeDriver driver;

        #region BUTTONS&FORMS
        public Form1()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            BackgroundImageLayout = ImageLayout.Stretch;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            turn_off = Properties.Settings.Default.sleepModeChecked;
            currently_running = "Waiting..";
            switch (Properties.Settings.Default.worldOption)
            {
                case 0:
                    this.Text = "NoLife (1)";
                    break;
                case 1:
                    this.Text = "NoLife (25)";
                    break;
                case 2:
                    this.Text = "NoLife (34)";
                    break;
                case 3:
                    this.Text = "NoLife (35)";
                    break;
                case 4:
                    this.Text = "NoLife (36)";
                    break;
                case 5:
                    this.Text = "NoLife (37)";
                    break;
                case 6:
                    this.Text = "NoLife (38)";
                    break;
                case 7:
                    this.Text = "NoLife (39)";
                    break;
                default:
                    this.Text = "NoLife (??)";
                    break;
            }
            Hide_To_Tray();
            Thread main_bot = new Thread(Bottings.Run);
            main_bot.Start();

            Thread life_tray = new Thread(Life_Tray_Info);
            life_tray.Start();

            if (Properties.Settings.Default.main_bot)
            {
                Thread search_mouse = new Thread(BasicTasks.Catch_Mouse);
                search_mouse.Start();
            }

            Thread watch_updates = new Thread(Check_for_updates);
            watch_updates.Start();
        }
        void Form1_Resize(object sender, EventArgs e)
        {
            Hide_To_Tray();
        }
        public void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            notifyIcon1.Visible = false;
            if(killEverything)
                Environment.Exit(0);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (!buyRingsAction) { Start_Botting(); TurnOff(); buyRingsAction = true; button1.Text = " BUYING RINGS.."; }
            else { MessageBox.Show("WAIT UNTILL TASK ENDS.."); }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (turn_off == true) { turn_off = false; button2.Text = "TURN OFF: FALSE"; }
            else { turn_off = true; button2.Text = "TURN OFF: TRUE"; }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (!botAction) { Start_Botting(); botAction = true; }
            else { botAction = false; startChrome = false; button3.Text = "START BOTTING"; }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (!moveGoldAction)
            {
                TurnOff();
                moveGoldAction = true;
                button4.Text = "PACKING GOLD..";
                Start_Botting();
            }
            else
            {
                MessageBox.Show("WAIT UNTILL TASK ENDS..");
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {

        }
        private void button6_Click(object sender, EventArgs e)
        {
            if (!sellItemsAction)
            {
                TurnOff();
                sellItemsAction = true;
                Start_Botting();
                button6.Text = "SELLING ITEMS..";
            }
            else
            {
                MessageBox.Show("WAIT UNTILL TASK ENDS..");
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            if (!downloadPackages)
            {
                TurnOff();
                downloadPackages = true;
                Start_Botting();
                button7.Text = "DOWNLOADING PACKAGES..";
            }
            else { MessageBox.Show("WAIT UNTILL TASK ENDS.."); }
        }
        private void button8_Click_1(object sender, EventArgs e)
        {
            Settings_Menu settings = new Settings_Menu();
            settings.Show();
        }
        private void button10_Click(object sender, EventArgs e)
        {
            if (!takeGoldAction)
            {
                TurnOff();
                takeGoldAction = true;
                Start_Botting();
                button10.Text = "TAKING GOLD..";
            }
            else
            {
                takeGoldAction = false;
                MessageBox.Show("WAIT UNTILL TASK ENDS..");
            }
        }
        void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show_From_Tray();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BasicTasks.Exit(false);
        }
        private void SetNotifyIconText(NotifyIcon ni, string text)
        {
            if (text.Length >= 128) throw new ArgumentOutOfRangeException("Text limited to 127 characters");
            Type t = typeof(NotifyIcon);
            BindingFlags hidden = BindingFlags.NonPublic | BindingFlags.Instance;
            t.GetField("text", hidden).SetValue(ni, text);
            if ((bool)t.GetField("added", hidden).GetValue(ni))
                t.GetMethod("UpdateIcon", hidden).Invoke(ni, new object[] { true });
        }
        #endregion
        public void TurnOff()
        {
            botAction = false;
            moveGoldAction = false;
            sellItemsAction = false;
            downloadPackages = false;

            button1.Text = "BUY RINGS";
            button3.Text = "START BOTTING";
            button4.Text = "PACK GOLD";
            button5.Text = "WATCH AUCTIONS";
            button6.Text = "SELL ITEMS";
            button10.Text = "TAKE OUT GOLD";
            button7.Text = "DOWNLOAD PACKAGES";
        }
        void Send_Notification(int notification_case)
        {
            switch(notification_case)
            {
                case 1:
                    notifyIcon1.BalloonTipText = "MOVING GOLD: READY! (" + Helpers.Switch_World() + ")"; notifyIcon1.ShowBalloonTip(1000);
                    break;
                case 2:
                    notifyIcon1.BalloonTipText = "SELLINGS ITEMS: READY! (" + Helpers.Switch_World() + ")"; notifyIcon1.ShowBalloonTip(1000);
                    break;
                case 3:
                    notifyIcon1.BalloonTipText = "TAKING GOLD OUT: READY! (" + Helpers.Switch_World() + ")"; notifyIcon1.ShowBalloonTip(1000);
                    break;
                case 4:
                    notifyIcon1.BalloonTipText = "BUY RINGS: READY! (" + Helpers.Switch_World() + ")"; notifyIcon1.ShowBalloonTip(1000);
                    break;
                case 5:
                    notifyIcon1.BalloonTipText = "DOWNLOADING PACKAGES: READY! (" + Helpers.Switch_World() + ")"; notifyIcon1.ShowBalloonTip(1000);
                    break;
                default:
                    return;
            }
        }
        void Turn_Off_Disabled()
        {
            turn_off = false;
            notifyIcon1.BalloonTipText = "TURN OFF: DISABLED";
            notifyIcon1.ShowBalloonTip(1000);
        }
        void Hide_To_Tray()
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon1.Visible = true;
            }
        }
        void Show_From_Tray()
        {
            Update_Turn_Off_Button();
            notifyIcon1.Visible = false;
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }
        void Update_Turn_Off_Button()
        {
            if (!turn_off) { button2.Text = "TURN OFF: FALSE"; }
            else { button2.Text = "TURN OFF: TRUE"; }
        }
        void Life_Tray_Info()
        {
            while (!bot_running) { Thread.Sleep(500); }
            bool non_stop = true;
            while (non_stop)
            {
                string expedition_points = BasicTasks.Return_String("//span[@id='expeditionpoints_value_point']");
                string dungeon_points = BasicTasks.Return_String("//span[@id='dungeonpoints_value_point']");
                string expedition_points_max = BasicTasks.Return_String("//span[@id='expeditionpoints_value_pointmax']");
                string dungeon_points_max = BasicTasks.Return_String("//span[@id='dungeonpoints_value_pointmax']");
                string gold_level = BasicTasks.Return_String("//div[@id='sstat_gold_val']");
                string rubles_level = BasicTasks.Return_String("//div[@id='sstat_ruby_val']");

                string final_string = "NoLife (" + Helpers.Switch_World() + ")" +
                    Environment.NewLine + "Expedition: " + expedition_points + "/" + expedition_points_max +
                    Environment.NewLine + "Dungeon: " + dungeon_points + "/" + dungeon_points_max +
                    Environment.NewLine + "Gold: " + gold_level + Environment.NewLine + "Rubles: " + rubles_level +
                    Environment.NewLine + "Current: " + currently_running;
                SetNotifyIconText(notifyIcon1, final_string);
                Thread.Sleep(500);
            }
        }
        void Start_Botting()
        {
            startChrome = true;
            button3.Text = "BOTTING..";
        }
        private void Check_for_updates()
        {
            while(true)
            {
                if (turn_off_func)
                {
                    TurnOff();
                    turn_off_func = !turn_off_func;
                }

                if (notification_type != -1)
                {
                    Send_Notification(notification_type);
                    notification_type = 0;
                }

                if(turn_off_notification)
                {
                    turn_off_notification = false;
                    Turn_Off_Disabled();
                }
                Thread.Sleep(5000);
            }
        }
    }
}
