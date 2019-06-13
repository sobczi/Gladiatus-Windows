using System;
using System.Threading;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Windows.Forms;
using System.IO;

namespace Gladiatus_35
{
    public static class Bottings
    {
        public static void Run()
        {
            BasicTasks.AlreadyRunning();
            try
            {
                Thread.Sleep(5000);
                while (!Form1.startChrome) { Thread.Sleep(500); }
                var driverOptions = new ChromeOptions();
                driverOptions.AddExtension(@"C:\Users\danie\Documents\Visual Studio 2017\Resources\Gladiatus_bots\GladiatusTools.crx");
                if (Properties.Settings.Default.headless)
                {
                    driverOptions.AddArgument("--headless");
                    driverOptions.AddArgument("--disable-gpu");
                    driverOptions.AddArgument("--window-size=1920,1080");
                }
                if (Screen.AllScreens.Length > 1) { driverOptions.AddArgument("--window-position=1750,50"); }
                var driverService = ChromeDriverService.CreateDefaultService();
                driverService.HideCommandPromptWindow = true;
                Form1.driver = new ChromeDriver(driverService, driverOptions);

                if (!Properties.Settings.Default.headless) { Form1.driver.Manage().Window.Maximize(); }

                using (Form1.driver)
                {
                    bool logged_in = Login();
                    Form1.bot_running = true;
                    Tests.RunTests();
                    bool non_stop = true;
                    int expedition_points = 0;
                    int dungeon_points = 0;
                    bool dungeon_event = true;
                    while (non_stop)
                    {
                        bool first_extract = true;
                        while (Form1.botAction)
                        {
                            do
                            {
                                if (!Form1.botAction) { break; }
                                if (first_extract)
                                {
                                    General.ExtractItems();
                                    General.Store_Components();
                                    first_extract = false;
                                }

                                expedition_points = Farming.Expedition(false);
                                dungeon_points = Farming.Dungeon(false);
                                if(dungeon_event)
                                    dungeon_event = Farming.DungeonEvent();
                                Farming.Arena35();
                                Farming.Turma35();
                                General.Training();
                                Gold.Pack_Gold();

                                expedition_points = BasicTasks.ReturnInt("//span[@id='expeditionpoints_value_point']");
                                dungeon_points = BasicTasks.ReturnInt("//span[@id='dungeonpoints_value_point']");

                                if (expedition_points == 0 && !Properties.Settings.Default.dungeonsChecked && Form1.british_land && !Properties.Settings.Default.farm_arenas ||
                                    expedition_points == 0 && Form1.british_land && !Properties.Settings.Default.farm_arenas ||
                                    dungeon_points == 0 && !Properties.Settings.Default.expeditionsChecked && Form1.british_land && !Properties.Settings.Default.farm_arenas)
                                { if (!Hades.Take_Pater_Costume() /*&& !Hades.DoHades()*/) { break; } }

                            } while (dungeon_points > 0 || expedition_points > 0 || Properties.Settings.Default.farm_arenas);

                            if (!Form1.botAction)
                                break;

                            Gold.Search_Pack();
                            General.Get_Items_For_Extract();
                            General.ExtractItems();
                            General.SellItems(false);
                            Gold.Pack_Gold();
                            General.BuyFood(false);
                            General.Buy_Auction_House();
                            General.MovingFood();
                            BasicTasks.Wait_For_Exit();
                        }

                        while (Form1.moveGoldAction)
                        {
                            Gold.Pack_Gold();
                            Form1.moveGoldAction = false;
                            Form1.turn_off_func = true;
                            Form1.notification_type = 1;
                        }

                        while (Form1.sellItemsAction)
                        {
                            General.SellItems(true);
                            Form1.sellItemsAction = false;
                            Form1.turn_off_func = true;
                            Form1.notification_type = 2;
                        }

                        while (Form1.takeGoldAction)
                        {
                            Gold.TakeGold();
                            Form1.takeGoldAction = false;
                            Form1.turn_off_func = true;
                            Form1.notification_type = 3;
                        }

                        while (Form1.buyRingsAction)
                        {
                            General.Buy_Auction_House();
                            Form1.buyRingsAction = false;
                            Form1.turn_off_func = true;
                            Form1.notification_type = 4;
                        }

                        while (Form1.downloadPackages)
                        {
                            General.Download_Packages();
                            Form1.downloadPackages = false;
                            Form1.turn_off_func = true;
                            Form1.notification_type = 5;
                        }

                        while (!Form1.botAction && !Form1.moveGoldAction && !Form1.sellItemsAction && !Form1.takeGoldAction && !Form1.buyRingsAction && !Form1.downloadPackages)
                        {
                            Thread.Sleep(1500);
                            Form1.currently_running = "Waiting for actions..";
                        }
                    }
                }
            }
            catch (Exception _exception)
            {
                BasicTasks.Save_Exception(_exception, Helpers.Switch_World());
                Run();
            }
        }
        private static bool Login()
        {
            Form1.currently_running = "Logging-in..";
            Form1.driver.Navigate().GoToUrl("https://pl.gladiatus.gameforge.com/game/");

            BasicTasks.CheckEvenets();

            IWebElement loginElement = BasicTasks.GetElement("//input[@id='login_username']");
            loginElement.SendKeys(Properties.Settings.Default.nickname);

            IWebElement passwordElement = BasicTasks.GetElement("//input[@id='login_password']");
            passwordElement.SendKeys(Properties.Settings.Default.password);

            BasicTasks.SelectElement("//select[@id='login_server']", Helpers.Switch_World());

            BasicTasks.Click("//input[@id='loginsubmit']");

            if (BasicTasks.Search("//div[@class='contentItem_content'][contains(text(),'błędne')]")) { return false; }
            else { return true; }
        }
    }
}
