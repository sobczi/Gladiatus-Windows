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
        public static bool hades;
        public static int expedition_points;
        public static int dungeon_points;
        public static bool british_land;
        public static bool expedition_checked;
        public static bool dungeon_checked;
        public static bool update_data;
        public static bool startChrome;
        public static bool watch_auctions;

        public static bool killEverything = true;
        public static bool botAction = true;
        public static bool moveGoldAction = false;
        public static bool buyFoodAction = false;
        public static bool sellItemsAction = false;
        public static bool arenaFarmAction = false;
        public static bool takeGoldAction = false;
        public static bool hadesHardAction = false;
        public static bool buyRingsAction = false;
        public static bool downloadPackages = false;

        public static string currently_running;

        public static string server_string;
        private static bool bot_running;
        private static bool turn_off;
        private static ChromeDriver driver;
        private static BasicTasks _BasicTasks;
        private static Tasks _Tasks;
        private static Tests _Tests;
        #endregion

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
            _BasicTasks = new BasicTasks();
            expedition_checked = Properties.Settings.Default.expeditionsChecked;
            dungeon_checked = Properties.Settings.Default.dungeonsChecked;
            update_data = false;
            turn_off = Properties.Settings.Default.sleepModeChecked;
            startChrome = true;
            bot_running = false;
            british_land = false;
            hades = false;
            currently_running = "Waiting..";
            int world_option = Properties.Settings.Default.worldOption;
            switch (world_option)
            {
                case 0:
                    this.Text = "NoLife (1)";
                    server_string = "1";
                    break;
                case 1:
                    this.Text = "NoLife (25)";
                    server_string = "25";
                    break;
                case 2:
                    this.Text = "NoLife (34)";
                    server_string = "34";
                    break;
                case 3:
                    this.Text = "NoLife (35)";
                    server_string = "35";
                    break;
                case 4:
                    this.Text = "NoLife (36)";
                    server_string = "36";
                    break;
                case 5:
                    this.Text = "NoLife (37)";
                    server_string = "37";
                    break;
                case 6:
                    this.Text = "NoLife (38)";
                    server_string = "38";
                    break;
                case 7:
                    this.Text = "NoLife (39)";
                    server_string = "39";
                    break;
                default:
                    this.Text = "NoLife (??)";
                    break;
            }
            Hide_To_Tray();
            Thread nowyThread = new Thread(Run);
            nowyThread.Start();
            if (Properties.Settings.Default.main_bot)
            {
                Thread search_mouse = new Thread(Catch_Mouse);
                search_mouse.Start();
            }
            Thread life_tray = new Thread(Life_Tray_Info);
            life_tray.Start();
        }
        void Form1_Resize(object sender, EventArgs e) { Hide_To_Tray(); }
        public void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            notifyIcon1.Visible = false;
            if (killEverything) { Environment.Exit(0); }
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
            if (!watch_auctions) { TurnOff(); watch_auctions = true; Start_Botting(); }
            else { MessageBox.Show("WAIT UNTILL TASK ENDS.."); }
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
        void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e) { Show_From_Tray(); }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _BasicTasks.Exit(turn_off);
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
            buyFoodAction = false;
            sellItemsAction = false;
            watch_auctions = false;
            downloadPackages = false;

            button1.Text = "BUY RINGS";
            button3.Text = "START BOTTING";
            button4.Text = "PACK GOLD";
            button5.Text = "WATCH AUCTIONS";
            button6.Text = "SELL ITEMS";
            button10.Text = "TAKE OUT GOLD";
            button7.Text = "DOWNLOAD PACKAGES";
        }
        public void Run()
        {
            _BasicTasks.AlreadyRunning();
            try
            {
                Thread.Sleep(5000);
                while (!startChrome) { Thread.Sleep(500); }
                var driverOptions = new ChromeOptions();
                if (Properties.Settings.Default.headless)
                {
                    driverOptions.AddArgument("--headless");
                    driverOptions.AddArgument("--disable-gpu");
                    driverOptions.AddArgument("--window-size=1920,1080");
                }
                if (Screen.AllScreens.Length > 1) { driverOptions.AddArgument("--window-position=1750,50"); }
                var driverService = ChromeDriverService.CreateDefaultService();
                driverService.HideCommandPromptWindow = true;
                driver = new ChromeDriver(driverService, driverOptions);

                if (!Properties.Settings.Default.headless) { driver.Manage().Window.Maximize(); }

                _Tasks = new Tasks(driver);
                _BasicTasks = new BasicTasks(driver);
                _Tests = new Tests(driver);

                int expedition_points_private;
                int dungeon_points_private;
                using (driver)
                {
                    bool logged_in = _Tasks.Login();
                    if (!logged_in) { notifyIcon1.BalloonTipText = "CAN'T LOG-IN INTO SERVER " + server_string + "!"; return; }
                    bot_running = true;
                    _Tests.RunTests();
                    bool non_stop = true;
                    while (non_stop)
                    {
                        bool first_extract = true;
                        while (botAction)
                        {
                            do
                            {
                                if (!botAction) { break; }
                                Check_Updates();
                                if (first_extract) { _Tasks.ExtractItems(); first_extract = false; _Tasks.Store_Components(); }

                                expedition_points_private = expedition_points = _Tasks.ReturnInt("//span[@id='expeditionpoints_value_point']");
                                dungeon_points_private = dungeon_points = _Tasks.ReturnInt("//span[@id='dungeonpoints_value_point']");

                                _Tasks.Expedition();
                                _Tasks.Dungeon();
                                _Tasks.DungeonEvent();
                                _Tasks.Arena35();
                                _Tasks.Turma35();
                                _Tasks.Training();
                                _Tasks.Pack_Gold();
                                _Tasks.Search_Pack();

                                if (hades)
                                    break;

                                expedition_points = _Tasks.ReturnInt("//span[@id='expeditionpoints_value_point']");
                                dungeon_points = _Tasks.ReturnInt("//span[@id='dungeonpoints_value_point']");

                                if (expedition_points == 0 && !Properties.Settings.Default.dungeonsChecked && british_land && !Properties.Settings.Default.farm_arenas ||
                                    expedition_points == 0 && british_land && !Properties.Settings.Default.farm_arenas ||
                                    dungeon_points == 0 && !Properties.Settings.Default.expeditionsChecked && british_land && !Properties.Settings.Default.farm_arenas)
                                { if (!_Tasks.Take_Pater_Costume()) { break; } }
                            } while (dungeon_points > 0 || expedition_points > 0 || Properties.Settings.Default.farm_arenas);

                            if (!botAction) { break; }
                            _Tasks.Get_Items_For_Extract();
                            _Tasks.ExtractItems();
                            _Tasks.SellItems(false);
                            _Tasks.Pack_Gold();
                            _Tasks.BuyFood();
                            _Tasks.Buy_Auction_House2();
                            _Tasks.MovingFood();
                            Wait_For_Exit();
                        }

                        while (moveGoldAction)
                        {
                            Check_Updates();
                            _Tasks.Pack_Gold();
                            moveGoldAction = false;
                            TurnOff();
                            Send_Notification(1);
                        }

                        while (sellItemsAction)
                        {
                            Check_Updates();
                            _Tasks.SellItems(true);
                            sellItemsAction = false;
                            TurnOff();
                            Send_Notification(2);
                        }

                        while (takeGoldAction)
                        {
                            Check_Updates();
                            _Tasks.TakeGold();
                            takeGoldAction = false;
                            TurnOff();
                            Send_Notification(3);
                        }

                        while (buyRingsAction)
                        {
                            Check_Updates();
                            _Tasks.Buy_Auction_House2();
                            buyRingsAction = false;
                            TurnOff();
                            Send_Notification(3);
                        }

                        while (watch_auctions)
                        {
                            Check_Updates();
                            _Tasks.Waith_Auction_House();
                            watch_auctions = false;
                            TurnOff();
                            Send_Notification(4);
                        }

                        while(downloadPackages)
                        {
                            Check_Updates();
                            _Tasks.Download_Packages();
                            downloadPackages = false;
                            TurnOff();
                            Send_Notification(5);
                        }

                        while (!botAction && !moveGoldAction && !sellItemsAction && !takeGoldAction && !buyRingsAction && !downloadPackages)
                        {
                            Check_Updates();
                            Thread.Sleep(1500);
                            currently_running = "Waiting for actions..";
                        }
                    }
                }
            }
            catch (Exception _exception)
            {
                notifyIcon1.BalloonTipText = "RESTARTING BOT (" + server_string + ")";
                notifyIcon1.ShowBalloonTip(1000);
                _BasicTasks.Save_Exception(_exception, server_string);
                Run();
            }
        }
        void Send_Notification(int notification_case)
        {
            switch(notification_case)
            {
                case 1:
                    notifyIcon1.BalloonTipText = "MOVING GOLD: READY! (" + server_string + ")"; notifyIcon1.ShowBalloonTip(1000);
                    break;
                case 2:
                    notifyIcon1.BalloonTipText = "SELLINGS ITEMS: READY! (" + server_string + ")"; notifyIcon1.ShowBalloonTip(1000);
                    break;
                case 3:
                    notifyIcon1.BalloonTipText = "TAKING GOLD OUT: READY! (" + server_string + ")"; notifyIcon1.ShowBalloonTip(1000);
                    break;
                case 4:
                    notifyIcon1.BalloonTipText = "BUY RINGS: READY! (" + server_string + ")"; notifyIcon1.ShowBalloonTip(1000);
                    break;
                case 5:
                    notifyIcon1.BalloonTipText = "DOWNLOADING PACKAGES: READY! (" + server_string + ")"; notifyIcon1.ShowBalloonTip(1000);
                    break;
                default:
                    return;
            }
        }
        void Catch_Mouse()
        {
            bool non_stop = true;
            while (non_stop)
            {
                int first_variable = Cursor.Position.X;
                Thread.Sleep(100);
                int second_variable = Cursor.Position.X;
                if (first_variable != second_variable) { Turn_Off_Disabled(); return; }
            }
        }
        void Turn_Off_Disabled()
        {
            turn_off = false; notifyIcon1.BalloonTipText = "TURN OFF: DISABLED"; notifyIcon1.ShowBalloonTip(1000);
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
        void Wait_For_Exit()
        {
            if (turn_off && Properties.Settings.Default.main_bot)
            {
                Form1.currently_running = "Waiting for exit..";
                string[] processes = new string[7];
                processes[0] = "Gladiatus_1";
                processes[1] = "Gladiatus_25";
                processes[2] = "Gladiatus_34";
                processes[3] = "Gladiatus_36";
                processes[4] = "Gladiatus_37";
                processes[5] = "Gladiatus_38";
                processes[6] = "Gladiatus_39";

                bool found_process = false;
                do
                {
                    Thread.Sleep(60000);
                    found_process = false;
                    Process[] proceses = Process.GetProcesses();
                    foreach (Process proces in proceses)
                    {
                        for (int i = 0; i < processes.Length; i++)
                        {
                            if (proces.ProcessName == processes[i] && proces.ProcessName != Process.GetCurrentProcess().ProcessName)
                            {
                                found_process = true;
                                break;
                            }
                        }

                        if(found_process)
                        {
                            break;
                        }
                    }
                } while (found_process && turn_off);
            }
            _BasicTasks.Exit(turn_off);
        }
        void Life_Tray_Info()
        {
            while (!bot_running) { Thread.Sleep(500); }
            bool non_stop = true;
            while (non_stop)
            {
                string expedition_points = _Tasks.Return_String("//span[@id='expeditionpoints_value_point']");
                string dungeon_points = _Tasks.Return_String("//span[@id='dungeonpoints_value_point']");
                string expedition_points_max = _Tasks.Return_String("//span[@id='expeditionpoints_value_pointmax']");
                string dungeon_points_max = _Tasks.Return_String("//span[@id='dungeonpoints_value_pointmax']");
                string gold_level = _Tasks.Return_String("//div[@id='sstat_gold_val']");
                string rubles_level = _Tasks.Return_String("//div[@id='sstat_ruby_val']");

                string final_string = "NoLife" + _Tasks.server_number +
                    Environment.NewLine + "Expedition: " + expedition_points + "/" + expedition_points_max +
                    Environment.NewLine + "Dungeon: " + dungeon_points + "/" + dungeon_points_max +
                    Environment.NewLine + "Gold: " + gold_level + Environment.NewLine + "Rubles: " + rubles_level +
                    Environment.NewLine + "Current: " + currently_running;
                SetNotifyIconText(notifyIcon1, final_string);
                Thread.Sleep(500);
            }
        }
        void Check_Updates()
        {
            if (update_data)
            {
                _Tasks = new Tasks(driver); _BasicTasks = new BasicTasks(driver);
                _Tests = new Tests(driver); update_data = false;
            }
        }
        void Start_Botting()
        {
            startChrome = true; button3.Text = "BOTTING..";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (!downloadPackages) { Start_Botting(); TurnOff(); downloadPackages = true; button7.Text = "DOWNLOADING PACKAGES.."; }
            else { MessageBox.Show("WAIT UNTILL TASK ENDS.."); }
        }
    }
}
