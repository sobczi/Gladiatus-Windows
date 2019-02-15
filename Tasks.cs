using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Chrome;
using System.Text.RegularExpressions;
using System.IO;

namespace Gladiatus_35
{
    class Tasks
    {
        #region VARIABLES
        private static ChromeDriver driver;
        private static BasicTasks _BasicTasks;

        public string server_number;
        private static string server;
        private static string food_backpack_string;
        private static string extract_backpack_string;
        private static string items_backpack_string;

        private static int difference_price;
        private static int food_pages;
        private static int boosters_per_type;
        private static int health_level;

        private static bool error_packing;
        private static bool need_food;
        private static bool food_exist_packages;
        private static bool food_exist_backpack;
        private static bool dungeon_event_ready;

        private static bool sell_weapons;
        private static bool sell_breastplates;
        private static bool sell_shields;
        private static bool sell_helmets;
        private static bool sell_gloves;
        private static bool sell_shoes;
        private static bool sell_accelerators;
        private static bool sell_bonuses;
        private static bool sell_blessings;
        private static bool sell_scrolls;
        private static bool sell_rings;
        private static bool sell_amulets;

        private static bool sell_weapons_click;
        private static bool sell_breastplates_click;
        private static bool sell_shields_click;
        private static bool sell_helmets_click;
        private static bool sell_gloves_click;
        private static bool sell_shoes_click;
        private static bool sell_accelerators_click;
        private static bool sell_bonuses_click;
        private static bool sell_blessings_click;
        private static bool sell_scrolls_click;
        private static bool sell_rings_click;
        private static bool sell_amulets_click;
        #endregion

        #region PUBLIC
        public Tasks(ChromeDriver _driver)
        {
            driver = _driver;

            error_packing = false;
            need_food = false;
            food_exist_packages = true;
            food_exist_backpack = true;
            dungeon_event_ready = false;

            difference_price = Properties.Settings.Default.differencePrice;
            food_pages = Properties.Settings.Default.foodPack;
            boosters_per_type = Properties.Settings.Default.boostersPack;
            health_level = Convert.ToInt32(Properties.Settings.Default.healthLevel);

            sell_weapons = Properties.Settings.Default.bronieChecked;
            sell_breastplates = Properties.Settings.Default.napierśnikiChecked;
            sell_shields = Properties.Settings.Default.tarczeChecked;
            sell_helmets = Properties.Settings.Default.hełmyChecked;
            sell_gloves = Properties.Settings.Default.rękawiceChecked;
            sell_shoes = Properties.Settings.Default.butyChecked;
            sell_accelerators = Properties.Settings.Default.przyspieszaczeChecked;
            sell_bonuses = Properties.Settings.Default.bonusyChecked;
            sell_blessings = Properties.Settings.Default.błogosławieństwaChecked;
            sell_scrolls = Properties.Settings.Default.zwójChecked;
            sell_rings = Properties.Settings.Default.pierścienieChecked;
            sell_amulets = Properties.Settings.Default.amuletyChecked;

            sell_weapons_click = Properties.Settings.Default.bronieSellChecked;
            sell_breastplates_click = Properties.Settings.Default.napierśnikiSellChecked;
            sell_shields_click = Properties.Settings.Default.tarczeSellChecked;
            sell_helmets_click = Properties.Settings.Default.hełmySellChecked;
            sell_gloves_click = Properties.Settings.Default.rękawiceSellChecked;
            sell_shoes_click = Properties.Settings.Default.butySellChecked;
            sell_accelerators_click = Properties.Settings.Default.przyspieszaceSellChecked;
            sell_bonuses_click = Properties.Settings.Default.bonusySellChecked;
            sell_blessings_click = Properties.Settings.Default.błogosławieństwaSellChecked;
            sell_scrolls_click = Properties.Settings.Default.zwójSellChecked;
            sell_rings_click = Properties.Settings.Default.pierścienieSellChecked;
            sell_amulets_click = Properties.Settings.Default.amuletySellChecked;

            int world_option = Properties.Settings.Default.worldOption;
            switch (world_option)
            {
                case 0:
                    server = "Prowincja 1";
                    server_number = "(1)";
                    break;
                case 1:
                    server = "Prowincja 25";
                    server_number = "(25)";
                    break;
                case 2:
                    server = "Prowincja 34";
                    server_number = "(34)";
                    break;
                case 3:
                    server = "Prowincja 35";
                    server_number = "(35)";
                    break;
                case 4:
                    server = "Prowincja 36";
                    server_number = "(36)";
                    break;
                case 5:
                    server = "Prowincja 37";
                    server_number = "(37)";
                    break;
                case 6:
                    server = "Prowincja 38";
                    server_number = "(38)";
                    break;
                case 7:
                    server = "Prowincja 39";
                    server_number = "(39)";
                    break;
                default:
                    break;
            }

            BackpackSwitch();
            _BasicTasks = new BasicTasks(driver);
        }
        public Tasks(ChromeDriver _driver, int _server)
        {
            driver = _driver;

            error_packing = false;
            need_food = false;
            food_exist_packages = true;
            food_exist_backpack = true;
            dungeon_event_ready = false;

            difference_price = Properties.Settings.Default.differencePrice;
            food_pages = Properties.Settings.Default.foodPack;
            boosters_per_type = Properties.Settings.Default.boostersPack;
            health_level = Convert.ToInt32(Properties.Settings.Default.healthLevel);

            sell_weapons = Properties.Settings.Default.bronieChecked;
            sell_breastplates = Properties.Settings.Default.napierśnikiChecked;
            sell_shields = Properties.Settings.Default.tarczeChecked;
            sell_helmets = Properties.Settings.Default.hełmyChecked;
            sell_gloves = Properties.Settings.Default.rękawiceChecked;
            sell_shoes = Properties.Settings.Default.butyChecked;
            sell_accelerators = Properties.Settings.Default.przyspieszaczeChecked;
            sell_bonuses = Properties.Settings.Default.bonusyChecked;
            sell_blessings = Properties.Settings.Default.błogosławieństwaChecked;
            sell_scrolls = Properties.Settings.Default.zwójChecked;
            sell_rings = Properties.Settings.Default.pierścienieChecked;
            sell_amulets = Properties.Settings.Default.amuletyChecked;

            sell_weapons_click = Properties.Settings.Default.bronieSellChecked;
            sell_breastplates_click = Properties.Settings.Default.napierśnikiSellChecked;
            sell_shields_click = Properties.Settings.Default.tarczeSellChecked;
            sell_helmets_click = Properties.Settings.Default.hełmySellChecked;
            sell_gloves_click = Properties.Settings.Default.rękawiceSellChecked;
            sell_shoes_click = Properties.Settings.Default.butySellChecked;
            sell_accelerators_click = Properties.Settings.Default.przyspieszaceSellChecked;
            sell_bonuses_click = Properties.Settings.Default.bonusySellChecked;
            sell_blessings_click = Properties.Settings.Default.błogosławieństwaSellChecked;
            sell_scrolls_click = Properties.Settings.Default.zwójSellChecked;
            sell_rings_click = Properties.Settings.Default.pierścienieSellChecked;
            sell_amulets_click = Properties.Settings.Default.amuletySellChecked;

            int world_option = Properties.Settings.Default.worldOption;
            switch (world_option)
            {
                case 0:
                    server = "Prowincja 1";
                    server_number = "(1)";
                    break;
                case 1:
                    server = "Prowincja 25";
                    server_number = "(25)";
                    break;
                case 2:
                    server = "Prowincja 34";
                    server_number = "(34)";
                    break;
                case 3:
                    server = "Prowincja 35";
                    server_number = "(35)";
                    break;
                case 4:
                    server = "Prowincja 36";
                    server_number = "(36)";
                    break;
                case 5:
                    server = "Prowincja 37";
                    server_number = "(37)";
                    break;
                case 6:
                    server = "Prowincja 38";
                    server_number = "(38)";
                    break;
                case 7:
                    server = "Prowincja 39";
                    server_number = "(39)";
                    break;
                default:
                    break;
            }

            BackpackSwitch();
            _BasicTasks = new BasicTasks(driver);
        }
        public bool Login()
        {
            Form1.currently_running = "Logging-in..";
            driver.Navigate().GoToUrl("https://pl.gladiatus.gameforge.com/game/");

            _BasicTasks.CheckEvenets();

            IWebElement loginElement = _BasicTasks.GetElement("//input[@id='login_username']");
            loginElement.SendKeys(Properties.Settings.Default.nickname);

            IWebElement passwordElement = _BasicTasks.GetElement("//input[@id='login_password']");
            passwordElement.SendKeys(Properties.Settings.Default.password);

            _BasicTasks.SelectElement("//select[@id='login_server']", server);

            _BasicTasks.Click("//input[@id='loginsubmit']");

            if (_BasicTasks.Search("//div[@class='contentItem_content'][contains(text(),'błędne')]")) { return false; }
            else { return true; }
        }
        public void Dungeon()
        {
            Form1.currently_running = "Dungeon..";
            if (Form1.dungeon_points > 0 && Properties.Settings.Default.dungeonsChecked || error_packing && !Form1.british_land)
            {
                if (_BasicTasks.Search("//div[@id='cooldown_bar_dungeon']/a[@class='cooldown_bar_link']"))
                {
                    IWebElement element = driver.FindElementByXPath("//div[@id='cooldown_bar_dungeon']/a[@class='cooldown_bar_link']");
                    if (!element.Displayed) { Form1.british_land = true; return; }
                }

                #region WaitForAvalibe
                _BasicTasks.WaitForXPath("//div[@id='cooldown_bar_text_dungeon'][text() = 'Do lochów']");
                _BasicTasks.Click("//div[@id='cooldown_bar_dungeon']/a[@class='cooldown_bar_link']");
                #endregion
                #region CheckNew
                if (_BasicTasks.Search("//input[@value='normalne']") || _BasicTasks.Search("//input[@value='zaawansowane']"))
                {
                    switch (Properties.Settings.Default.dungeonsOption)
                    {
                        case 0:
                            _BasicTasks.Click("//input[@value='normalne']");
                            break;
                        case 1:
                            if (_BasicTasks.Search("//input[@value='zaawansowane'][@disabled='disabled']"))
                            { _BasicTasks.Click("//input[@value='zaawansowane']"); }
                            else { _BasicTasks.Click("//input[@value='normalne']"); }
                            break;
                        default:
                            return;
                    }
                }
                #endregion
                #region Attack
                _BasicTasks.WaitForXPath("//img[contains(@src,'combatloc.gif')]");
                _BasicTasks.Click("//img[contains(@src,'combatloc.gif')]");
                #endregion
                error_packing = false;
            }
            Thread.Sleep(2000);
        }
        public void Expedition()
        {
            Form1.currently_running = "Expedition..";
            if (Form1.expedition_points > 0 && Properties.Settings.Default.expeditionsChecked || error_packing)
            {
                if (_BasicTasks.Search("//a[contains(@class,'menuitem')][text() = 'Opuść Hades']"))
                {
                    Form1.hades = true;
                    return;
                }
                #region CheckForHealth
                HealMe();
                #endregion
                #region WaitForAvalibe
                _BasicTasks.WaitForXPath("//div[@id='cooldown_bar_expedition']/div[@class='cooldown_bar_text']");
                _BasicTasks.Click("//div[@id='cooldown_bar_expedition']/a[@class='cooldown_bar_link']");
                #endregion
                #region ChooseEnemy
                IReadOnlyCollection<IWebElement> list_buttons = driver.FindElementsByXPath("//button[contains(@class,'expedition_button')]");
                int choose_properties = Properties.Settings.Default.expeditionOption;
                while (choose_properties >= list_buttons.Count)
                { choose_properties--; }

                switch (choose_properties)
                {
                    case 0:
                        list_buttons.ElementAt(0).Click();
                        break;
                    case 1:
                        list_buttons.ElementAt(1).Click();
                        break;
                    case 2:
                        list_buttons.ElementAt(2).Click();
                        break;
                    case 3:
                        list_buttons.ElementAt(3).Click();
                        break;
                    default:
                        return;
                }
                if (error_packing) { error_packing = false; }
                #endregion
            }
            Thread.Sleep(2000);
        }
        public void Pack_Gold()
        {
            string file_path = 
                @"C:\Users\danie\Documents\Visual Studio 2017\Resources\Gladiatus_bots\Items .txt files for Gladiatus_bot" + @"\items" + server_number + ".txt";
            if (!File.Exists(file_path)) { return; }
            while (Properties.Settings.Default.pakujChecked &&
                Gold_Level() >= Convert.ToInt32(Properties.Settings.Default.minimum_gold_pack))
            {
                Form1.currently_running = "Packing gold..";
                int lineCount = File.ReadLines(file_path).Count();
                if (lineCount == 0) { return; }
                string[] lines = File.ReadAllLines(file_path);
                string[] class_items = new string[lines.Length];
                string[] soulbound_items = new string[lines.Length];
                string[] price_items = new string[lines.Length];
                string[] level_items = new string[lines.Length];
                string[] types = new string[lines.Length];
                string[] quality = new string[lines.Length];
                string[] amount = new string[lines.Length];

                int iterator = 0;
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] separated_line = lines[i].Split(' ');
                    if (separated_line.Length == 8)
                    {
                        class_items[iterator] = separated_line[0];
                        soulbound_items[iterator] = separated_line[1];
                        price_items[iterator] = separated_line[2];
                        types[iterator] = separated_line[3];
                        quality[iterator] = separated_line[4];
                        level_items[iterator] = separated_line[5];
                        amount[iterator] = separated_line[6];
                        iterator++;
                    }
                }

                bool changed = true;
                while(changed)
                {
                    changed = false;
                    for(int i=0; i<lines.Length - 1; i++)
                    {
                        if(Convert.ToInt32(price_items[i])<Convert.ToInt32(price_items[i+1]))
                        {
                            changed = true;
                            string temporary = price_items[i];
                            price_items[i] = price_items[i + 1];
                            price_items[i + 1] = temporary;

                            temporary = class_items[i];
                            class_items[i] = class_items[i + 1];
                            class_items[i + 1] = temporary;

                            temporary = soulbound_items[i];
                            soulbound_items[i] = soulbound_items[i + 1];
                            soulbound_items[i + 1] = temporary;

                            temporary = types[i];
                            types[i] = types[i + 1];
                            types[i + 1] = temporary;

                            temporary = quality[i];
                            quality[i] = quality[i + 1];
                            quality[i + 1] = temporary;

                            temporary = level_items[i];
                            level_items[i] = level_items[i + 1];
                            level_items[i + 1] = temporary;

                            temporary = amount[i];
                            amount[i] = amount[i + 1];
                            amount[i + 1] = temporary;
                        }
                    }
                }

                Guild_Market();
                if (!_BasicTasks.Search("//input[@value='Kup']")) { return; }

                int found_case = 0;
                int orginal_case = 0;
                for (int i = 0; i < class_items.Length; i++)
                {
                    if (Gold_Level() - Convert.ToInt32(price_items[i]) > 0)
                    { found_case = i; orginal_case = i; break; }
                }

                loop_label:
                int first_iterator = driver.FindElementsByXPath("//input[@value='Kup']").Count();
                int second_iterator = driver.FindElementsByXPath("//input[@value='Anuluj']").Count();
                iterator = first_iterator + second_iterator;
                int price_item_work = Convert.ToInt32(price_items[found_case]);
                string name_item_work = class_items[found_case];
                string soulbound_work = soulbound_items[found_case];
                string level_work = level_items[found_case];
                string item_quality = quality[found_case];
                string amount_work = amount[found_case];
                bool by_name = false;
                bool by_amount = false;
                bool by_soulbound = false;
                bool by_level = false;
                bool by_quality = false;
                bool bought = false;
                if (amount_work != "null")
                    by_amount = true;
                if (name_item_work != "null")
                    by_name = true;
                if (soulbound_work != "null")
                    by_soulbound = true;
                if (level_work != "null")
                    by_level = true;
                if (item_quality != "null")
                    by_quality = true;
                for (int i = 2; i <= iterator; i++)
                {
                    if (_BasicTasks.Search("//section[@id='market_table']//tr[position()='" + i + "']/td[@align='center']/input[@value='Kup']"))
                    {
                        IWebElement element = driver.FindElementByXPath("//section[@id='market_table']//tr[position()='" + i + "']/td[@style]/div[@style]");
                        string soulbound = element.GetAttribute("data-soulbound-to");
                        string name_item = element.GetAttribute("class");
                        string price_item_string = driver.FindElementByXPath
                            ("//section[@id='market_table']//tr[position()='" + i + "']/td[position()='3']").GetAttribute("textContent");
                        string level = element.GetAttribute("data-level");
                        string quality_level = element.GetAttribute("data-quality");
                        string amount_loop = element.GetAttribute("data-amount");

                        int price_item = Convert.ToInt32(new String(price_item_string.Where(Char.IsDigit).ToArray()));

                        if (price_item_work == price_item)
                        {
                            if (by_name && name_item != name_item_work ||
                                by_soulbound && soulbound != soulbound_work ||
                                 by_level && level != level_work || by_quality && quality_level != item_quality || by_amount && amount_loop != amount_work)
                                continue;

                            int gold_before = Gold_Level();
                            _BasicTasks.Click("//section[@id='market_table']//tr[position()='" + i + "']/td[@align='center']/input[@value='Kup']");
                            if (gold_before - Gold_Level() == price_item_work) { bought = true; }
                            break;
                        }
                    }
                }

                if (!bought && found_case != class_items.Length - 1) { found_case++; goto loop_label; }
                else if (!bought && _BasicTasks.Search("//a[contains(text(),'Następna strona')]") && found_case == class_items.Length - 1)
                { _BasicTasks.Click("//a[contains(text(),'Następna strona')]"); found_case = orginal_case; goto loop_label; }
                else if (!bought && !_BasicTasks.Search("//a[contains(text(),'Następna strona')]") && found_case == class_items.Length - 1) { return; }

                string path = "//div[@class='packageItem']//div";
                string path2 = "//div[@id='inv']//div";

                if (by_name)
                {
                    path = path + "[contains(concat(' ', normalize-space(@class), ' '), ' " + class_items[found_case] + " ')]";
                    path2 = path2 + "[contains(concat(' ', normalize-space(@class), ' '), ' " + class_items[found_case] + " ')]";
                }

                if (by_soulbound)
                {
                    path += "[@data-soulbound-to='" + soulbound_work + "']";
                    path2 += "[@data-soulbound-to='" + soulbound_work + "']";
                }

                if (by_level)
                {
                    path += "[@data-level='" + level_work + "']";
                    path2 += "[@data-level='" + level_work + "']";
                }

                if(by_quality)
                {
                    path += "[@data-quality='" + item_quality + "']";
                    path2 += "[@data-quality='" + item_quality + "']";
                }

                if(by_amount)
                {
                    path += "[@data-amount='" + amount_work + "']";
                    path2 += "[@data-amount='" + amount_work + "']";
                }

                bool successMarket = false;
                while (!successMarket)
                {
                    try
                    {
                        takeOut:
                        Packages();
                        if (_BasicTasks.Search("//section[@style='display: none;']")) { _BasicTasks.Click("//h2[@class='section-header'][contains(text(), 'Opcje')]"); }
                        _BasicTasks.SelectElement("//select[@name='f']", Type_Pack(types[found_case]));
                        _BasicTasks.SelectElement("//select[@name='fq']", Quality_Pack(quality[found_case]));
                        _BasicTasks.Click("//input[@value='Filtr']");
                        if (!FreeBackpack()) { return; }

                        if (!_BasicTasks.Search(path) && _BasicTasks.Search(path2)) { goto sell; }
                        else if (!_BasicTasks.Search(path) && !_BasicTasks.Search(path2))
                        {
                            bool found = false;
                            while (_BasicTasks.Search("//a[@class='paging_button paging_right_step']") && !found)
                            {
                                _BasicTasks.Click("//a[@class='paging_button paging_right_step']");
                                if (_BasicTasks.Search(path)) { found = true; }
                            }
                            if (!found) { break; }
                        }
                        FreeBackpack();
                        _BasicTasks.MoveMoveElement(path, "//div[@id='inv']");
                        if (_BasicTasks.Search("//div[@class='ui-droppable grid-droparea image-grayed active']"))
                        { _BasicTasks.ReleaseElement("//div[@class='ui-droppable grid-droparea image-grayed active']"); }
                        else { _BasicTasks.ReleaseElement("//input[@name='show-item-info']"); return; }

                        sell:
                        Guild_Market();
                        FreeBackpack();

                        while (_BasicTasks.Search("//div[@id='market_sell_box']//section[@style='display: none;']"))
                        { _BasicTasks.Click("//h2[@class='section-header'][text() = 'sprzedaj']"); }

                        if (_BasicTasks.Search(path2))
                        { _BasicTasks.MoveReleaseElement(path2, "//div[@id='market_sell']/div[@class='ui-droppable']"); }
                        else { goto takeOut; }
                        _BasicTasks.SelectElement("//select[@name='dauer']", "24 h");
                        var cena = _BasicTasks.GetElement("//input[@name='preis']");
                        cena.SendKeys(OpenQA.Selenium.Keys.Control + "a");
                        cena.SendKeys(OpenQA.Selenium.Keys.Delete);
                        cena.SendKeys(Convert.ToString(price_item_work));
                        _BasicTasks.Click("//input[@value='Oferta']");

                        if (_BasicTasks.Search("//div[@class='message fail']"))
                        { try { error_packing = true; Expedition(); goto sell; } catch { } }
                        else { error_packing = false; successMarket = true; }
                    }
                    catch { successMarket = false; }
                }
            }
        }
        public void Search_Pack()
        {
            string file_path = 
                @"C:\Users\danie\Documents\Visual Studio 2017\Resources\Gladiatus_bots\Items .txt files for Gladiatus_bot" + @"\items" + server_number + ".txt";
            if (!Properties.Settings.Default.pakujChecked ||
                !File.Exists(file_path))
                return;

            bool found = false;
            IReadOnlyCollection<IWebElement> items;

            int lineCount = File.ReadLines(file_path).Count();
            if (lineCount == 0) { return; }
            string[] lines = File.ReadAllLines(file_path);
            string[] class_items = new string[lines.Length];
            string[] soulbound_items = new string[lines.Length];
            string[] price_items = new string[lines.Length];
            string[] level_items = new string[lines.Length];
            string[] types = new string[lines.Length];
            string[] qualities_items = new string[lines.Length];
            string[] amount_work = new string[lines.Length];
            string[] already_sold = new string[lines.Length];

            int iterator = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                string[] separated_line = lines[i].Split(' ');
                if (separated_line.Length == 7)
                {
                    class_items[iterator] = separated_line[0];
                    soulbound_items[iterator] = separated_line[1];
                    price_items[iterator] = separated_line[2];
                    types[iterator] = separated_line[3];
                    qualities_items[iterator] = separated_line[4];
                    level_items[iterator] = separated_line[5];
                    amount_work[iterator] = separated_line[6];
                    already_sold[iterator] = separated_line[7];
                    iterator++;
                }
            }

            string type = types[0];
            string quality = qualities_items[0];
            bool types_good = true;
            bool quality_good = true;
            if (types.Contains("null")) { types_good = false; }
            if (qualities_items.Contains("null")) { quality_good = false; }
            if (types_good || quality_good)
            {
                for (int i = 0; i < class_items.Length; i++)
                {
                    if (type != types[i])
                        types_good = false;
                    if (quality != qualities_items[i])
                        quality_good = false;
                }
            }

            bool first = false;
            bool second = false;
            bool third = false;
            bool fourth = false;
            bool fifth = false;

            bool names = false;
            bool levels = false;
            bool soulbounds = false;
            bool qualities = false;
            bool by_amount = false;
            int found_case = 0;

            do
            {
                found = false;
                Packages();
                if (_BasicTasks.Search("//section[@style='display: none;']"))
                { _BasicTasks.Click("//h2[@class='section-header'][contains(text(), 'Opcje')]"); }
                if (types_good) { _BasicTasks.SelectElement("//select[@name='f']", Type_Pack(type)); }
                if (quality_good) { _BasicTasks.SelectElement("//select[@name='fq']", Quality_Pack(quality)); }
                if (types_good || quality_good) { _BasicTasks.Click("//input[@value='Filtr']"); }
                items = driver.FindElementsByXPath("//div[@id='packages']//div[contains(@class,'ui-draggable')]");

                do
                {
                    for (int i = 0; i < items.Count; i++)
                    {
                        string soul_bound = items.ElementAt(i).GetAttribute("data-soulbound-to");
                        string name_class = items.ElementAt(i).GetAttribute("class");
                        string level = items.ElementAt(i).GetAttribute("data-level");
                        quality = items.ElementAt(i).GetAttribute("data-quality");
                        string amount_temp = items.ElementAt(i).GetAttribute("data-amount");
                        for (int j = 0; j < class_items.Length; j++)
                        {
                            first = false;
                            second = false;
                            third = false;
                            fourth = false;
                            fifth = false;
                            if (class_items[j] != "null") { names = true; } else { first = true; }
                            if (level_items[j] != "null") { levels = true; } else { second = true; }
                            if (soulbound_items[j] != "null") { soulbounds = true; } else { third = true; }
                            if (qualities_items[j] != "null") { qualities = true; } else { fourth = true; }
                            if(amount_work[j] != "null") { by_amount = true; } else { fifth = true; }

                            var regex = new Regex(@"\s*\b" + class_items[j] + @"\s*\b");
                            if (names && regex.IsMatch(name_class)) { first = true; }
                            if (levels && level_items[j] == level) { second = true; }
                            if (soulbounds && soulbound_items[j] == soul_bound) { third = true; }
                            if(qualities && qualities_items[j] == quality) { fourth = true; }
                            if(by_amount && amount_work[j] == amount_temp) { fifth = true; }

                            if (first && second && third && fourth && fifth) { found = true; found_case = j; break; }
                        }
                        if (found) { break; }
                    }
                    if (found) { break; }
                    if (_BasicTasks.Search("//a[@class='paging_button paging_right_step']"))
                    { _BasicTasks.Click("//a[@class='paging_button paging_right_step']"); }
                    items = driver.FindElementsByXPath("//div[@id='packages']//div[contains(@class,'ui-draggable')]");
                } while (_BasicTasks.Search("//a[@class='paging_button paging_right_step']"));
                if (!found) { continue; }
                if (!FreeBackpack()) { return; }
                string path1 = "//div[@id='packages']//div";
                string path2 = "//div[@id='inv']//div";
                if (names)
                {
                    path1 = path1 + "[contains(concat(' ', normalize-space(@class), ' '), ' " + class_items[found_case] + " ')]";
                    path2 = path2 + "[contains(concat(' ', normalize-space(@class), ' '), ' " + class_items[found_case] + " ')]";
                }
                if (levels)
                {
                    path1 += "[@data-level='" + level_items[found_case] + "']";
                    path2 += "[@data-level='" + level_items[found_case] + "']";
                }
                if (soulbounds)
                {
                    path1 += "[@data-soulbound-to='" + soulbound_items[found_case] + "']";
                    path2 += "[@data-soulbound-to='" + soulbound_items[found_case] + "']";
                }
                if(qualities)
                {
                    path1 += "[@data-quality='" + qualities_items[found_case] + "']";
                    path2 += "[@data-quality='" + qualities_items[found_case] + "']";
                }

                if (by_amount)
                {
                    path1 += "[@data-amount='" + amount_work[found_case] + "']";
                    path2 += "[@data-amount='" + amount_work[found_case] + "']";
                }

                _BasicTasks.MoveMoveElement(path1, "//input[@name='show-item-info']");
                if (_BasicTasks.Search("//div[@class='ui-droppable grid-droparea image-grayed active']"))
                {   
                    _BasicTasks.ReleaseElement("//div[@class='ui-droppable grid-droparea image-grayed active']");
                    if (!_BasicTasks.Search(path2)) { return; }
                }
                else { _BasicTasks.ReleaseElement("//input[@name='show-item-info']"); return; }

                sell:
                Guild_Market();
                FreeBackpack();

                while (_BasicTasks.Search("//div[@id='market_sell_box']//section[@style='display: none;']"))
                { _BasicTasks.Click("//h2[@class='section-header'][text() = 'sprzedaj']"); }

                _BasicTasks.MoveReleaseElement(path2, "//div[@id='market_sell']/div[@class='ui-droppable']");
                _BasicTasks.SelectElement("//select[@name='dauer']", "24 h");
                var cena = _BasicTasks.GetElement("//input[@name='preis']");
                cena.SendKeys(OpenQA.Selenium.Keys.Control + "a");
                cena.SendKeys(OpenQA.Selenium.Keys.Delete);
                cena.SendKeys(Convert.ToString(price_items[found_case]));
                _BasicTasks.Click("//input[@value='Oferta']");

                if (_BasicTasks.Search("//div[@class='message fail']"))
                { try { error_packing = true; Expedition(); goto sell; } catch { } }
                else { error_packing = false; }
            } while (found);
            
            found = false;
            FreeBackpack();
            items = driver.FindElementsByXPath("//div[@id='inv']//div[contains(@class,'ui-draggable')]");

            for (int i = 0; i < items.Count; i++)
            {
                string soul_bound = items.ElementAt(i).GetAttribute("data-soulbound-to");
                string name_class = items.ElementAt(i).GetAttribute("class");
                string level = items.ElementAt(i).GetAttribute("data-level");
                quality = items.ElementAt(i).GetAttribute("data-quality");
                string amount_temp = items.ElementAt(i).GetAttribute("data-amount");
                for (int j = 0; j < class_items.Length; j++)
                {
                    first = false;
                    second = false;
                    third = false;
                    if (class_items[j] != "null") { names = true; } else { first = true; }
                    if (level_items[j] != "null") { levels = true; } else { second = true; }
                    if (soulbound_items[j] != "null") { soulbounds = true; } else { third = true; }
                    if (qualities_items[j] != "null") { qualities = true; } else { fourth = true; }
                    if (amount_work[j] != "null") { by_amount = true; } else { fifth = true; }

                    var regex = new Regex(@"\s*\b" + class_items[j] + @"\s*\b");
                    if (names && regex.IsMatch(name_class)) { first = true; }
                    if (levels && level_items[j] == level) { second = true; }
                    if (soulbounds && soulbound_items[j] == soul_bound) { third = true; }
                    if (qualities && qualities_items[j] == quality) { fourth = true; }
                    if (by_amount && amount_work[j] == amount_temp) { fifth = true; }

                    if (first && second && third && fourth && fifth) { found = true; found_case = j; break; }
                }
                if (found) { break; }
            }

            while (found)
            {
                sell_inv:
                found = false;

                Guild_Market();
                FreeBackpack();
                while (_BasicTasks.Search("//div[@id='market_sell_box']//section[@style='display: none;']"))
                { _BasicTasks.Click("//h2[@class='section-header'][text() = 'sprzedaj']"); }

                items = driver.FindElementsByXPath("//div[@id='inv']//div[contains(@class,'ui-draggable')]");
                for (int i = 0; i < items.Count; i++)
                {
                    string soul_bound = items.ElementAt(i).GetAttribute("data-soulbound-to");
                    string name_class = items.ElementAt(i).GetAttribute("class");
                    string level = items.ElementAt(i).GetAttribute("data-level");
                    quality = items.ElementAt(i).GetAttribute("data-quality");
                    string amount_temp = items.ElementAt(i).GetAttribute("data-amount");
                    for (int j = 0; j < class_items.Length; j++)
                    {
                        first = false;
                        second = false;
                        third = false;
                        fourth = false;
                        fifth = false;
                        if (class_items[j] != "null") { names = true; } else { first = true; }
                        if (level_items[j] != "null") { levels = true; } else { second = true; }
                        if (soulbound_items[j] != "null") { soulbounds = true; } else { third = true; }
                        if (qualities_items[j] != "null") { qualities = true; } else { fourth = true; }
                        if (amount_work[j] != "null") { by_amount = true; } else { fifth = true; }

                        var regex = new Regex(@"\s*\b" + class_items[j] + @"\s*\b");
                        if (names && regex.IsMatch(name_class)) { first = true; }
                        if (levels && level_items[j] == level) { second = true; }
                        if (soulbounds && soulbound_items[j] == soul_bound) { third = true; }
                        if (qualities && qualities_items[j] == quality) { fourth = true; }
                        if (by_amount && amount_work[j] == amount_temp) { fifth = true; }

                        if (first && second && third && fourth && fifth) { found = true; found_case = j; break; }
                    }
                    if (found) { break; }
                }
                if (!found) { continue; }
                string path1 = "//div[@id='packages']//div";
                string path2 = "//div[@id='inv']//div";
                if (names)
                {
                    path1 = path1 + "[contains(concat(' ', normalize-space(@class), ' '), ' " + class_items[found_case] + " ')]";
                    path2 = path2 + "[contains(concat(' ', normalize-space(@class), ' '), ' " + class_items[found_case] + " ')]";
                }
                if (levels)
                {
                    path1 += "[@data-level='" + level_items[found_case] + "']";
                    path2 += "[@data-level='" + level_items[found_case] + "']";
                }
                if (soulbounds)
                {
                    path1 += "[@data-soulbound-to='" + soulbound_items[found_case] + "']";
                    path2 += "[@data-soulbound-to='" + soulbound_items[found_case] + "']";
                }
                if (qualities)
                {
                    path1 += "[@data-quality='" + qualities_items[found_case] + "']";
                    path2 += "[@data-quality='" + qualities_items[found_case] + "']";
                }

                if (by_amount)
                {
                    path1 += "[@data-amount='" + amount_work[found_case] + "']";
                    path2 += "[@data-amount='" + amount_work[found_case] + "']";
                }

                int already_sold_current = Convert.ToInt32(already_sold[found_case]);
                bool already_sold_needed = false;
                if (already_sold_current == 1)
                    already_sold_needed = true;

                _BasicTasks.MoveTo(path2);
                if(_BasicTasks.Search("//p[contains(text(),'Wskazówka')]") != already_sold_needed)
                {
                    Malefica_Seller();
                    _BasicTasks.MoveMoveElement(path2, "//a[@class='awesome-tabs current']");
                    _BasicTasks.ReleaseElement("//div[@id='shop']//div[@class='ui-droppable grid-droparea image-grayed active']");
                    Search_Pack();
                    return;
                }
                else
                {
                    _BasicTasks.ReleaseElement("//div[@id='market_sell']/div[@class='ui-droppable']");
                }

                //_BasicTasks.MoveReleaseElement(path2, "//div[@id='market_sell']/div[@class='ui-droppable']");
                _BasicTasks.SelectElement("//select[@name='dauer']", "24 h");
                var cena_2 = _BasicTasks.GetElement("//input[@name='preis']");
                cena_2.SendKeys(OpenQA.Selenium.Keys.Control + "a");
                cena_2.SendKeys(OpenQA.Selenium.Keys.Delete);
                cena_2.SendKeys(Convert.ToString(price_items[found_case]));
                _BasicTasks.Click("//input[@value='Oferta']");

                if (_BasicTasks.Search("//div[@class='message fail']"))
                { try { error_packing = true; Expedition(); goto sell_inv; } catch { } }
                else { error_packing = false; }
            }
        }
        public void BuyFood()
        {
            Form1.currently_running = "Buying food..";
            if (Properties.Settings.Default.buyFood || Form1.buyFoodAction || need_food)
            {
                Packages();
                if (_BasicTasks.Search("//section[@style='display: none;']")) { _BasicTasks.Click("//h2[@class='section-header'][contains(text(), 'Opcje')]"); }
                _BasicTasks.SelectElement("//select[@name='f']", "Jadalne");
                _BasicTasks.Click("//input[@value='Filtr']");

                for (int i = food_pages; i < food_pages * 10; i++)
                {
                    if (driver.FindElementsByXPath("//div[@class='paging_numbers']//a[text() = '" + i + "']").Count != 0) { return; }
                }
                string[] auctionForms;
                AuctionHouse();
                _BasicTasks.SelectElement("//select[@name='itemType']", "Jadalne");
                _BasicTasks.Click("//input[@value='Filtr']");

                IReadOnlyCollection<IWebElement> list = driver.FindElementsByXPath("//div[@id='auction_table']//form[@method='post']");
                auctionForms = new string[list.Count];
                for (int i = 0; i < list.Count; i++)
                {
                    auctionForms[i] = Convert.ToString(list.ElementAt(i).GetAttribute("id"));
                }

                for (int i = 0; i < list.Count; i++)
                {
                    string helperString = "//div[@id='auction_table']//form[@id='" + auctionForms[i] + "']";
                    string noOffers = driver.FindElementByXPath(helperString + "//div[@class='auction_bid_div']/div").GetAttribute("textContent");
                    noOffers = noOffers.Trim();

                    if (noOffers == "Brak ofert") { _BasicTasks.Click(helperString + "//input[@value='Licytuj']"); }
                    if (driver.FindElementsByXPath("//div[@class='message fail']").Count != 0) { return; }
                    list = driver.FindElementsByXPath("//input[@value='Licytuj']");
                }
            }
            Thread.Sleep(2000);
        }
        public void Buy_Auction_House()
        {
            Form1.currently_running = "Buying from Auction House..";
            if (Properties.Settings.Default.buyRings || Properties.Settings.Default.buyAmulets || Properties.Settings.Default.buyBoosters || Form1.buyRingsAction)
            {
                int RingOrAmulet = 0;
                bool boughtAmulets = false;
                bool boughtBoosters = false;

                int[] i_strength = new int[4];
                string[] strength = new string[4];
                strength[0] = "item-i-11-4 ui-draggable ui-droppable ui-draggable-handle";
                strength[1] = "item-i-11-3 ui-draggable ui-droppable ui-draggable-handle";
                strength[2] = "item-i-11-2 ui-draggable ui-droppable ui-draggable-handle";
                strength[3] = "item-i-11-1 ui-draggable ui-droppable ui-draggable-handle";

                int[] i_mastery = new int[4];
                string[] mastery = new string[4];
                mastery[0] = "item-i-11-8 ui-draggable ui-droppable ui-draggable-handle";
                mastery[1] = "item-i-11-7 ui-draggable ui-droppable ui-draggable-handle";
                mastery[2] = "item-i-11-6 ui-draggable ui-droppable ui-draggable-handle";
                mastery[3] = "item-i-11-5 ui-draggable ui-droppable ui-draggable-handle";

                int[] i_skill = new int[4];
                string[] skill = new string[4];
                skill[0] = "item-i-11-12 ui-draggable ui-droppable ui-draggable-handle";
                skill[1] = "item-i-11-11 ui-draggable ui-droppable ui-draggable-handle";
                skill[2] = "item-i-11-10 ui-draggable ui-droppable ui-draggable-handle";
                skill[3] = "item-i-11-9 ui-draggable ui-droppable ui-draggable-handle";

                int[] i_physic = new int[4];
                string[] physic = new string[4];
                physic[0] = "item-i-11-16 ui-draggable ui-droppable ui-draggable-handle";
                physic[1] = "item-i-11-15 ui-draggable ui-droppable ui-draggable-handle";
                physic[2] = "item-i-11-14 ui-draggable ui-droppable ui-draggable-handle";
                physic[3] = "item-i-11-13 ui-draggable ui-droppable ui-draggable-handle";

                int[] i_charisma = new int[4];
                string[] charisma = new string[4];
                charisma[0] = "item-i-11-20 ui-draggable ui-droppable ui-draggable-handle";
                charisma[1] = "item-i-11-19 ui-draggable ui-droppable ui-draggable-handle";
                charisma[2] = "item-i-11-18 ui-draggable ui-droppable ui-draggable-handle";
                charisma[3] = "item-i-11-17 ui-draggable ui-droppable ui-draggable-handle";

                int[] i_inteligence = new int[4];
                string[] inteligence = new string[4];
                inteligence[0] = "item-i-11-27 ui-draggable ui-droppable ui-draggable-handle";
                inteligence[1] = "item-i-11-26 ui-draggable ui-droppable ui-draggable-handle";
                inteligence[2] = "item-i-11-25 ui-draggable ui-droppable ui-draggable-handle";
                inteligence[3] = "item-i-11-24 ui-draggable ui-droppable ui-draggable-handle";

                int[] i_health = new int[3];
                string[] health = new string[3];
                health[0] = "item-i-11-23 ui-draggable ui-droppable ui-draggable-handle";
                health[1] = "item-i-11-22 ui-draggable ui-droppable ui-draggable-handle";
                health[2] = "item-i-11-21 ui-draggable ui-droppable ui-draggable-handle";

                if (Properties.Settings.Default.buyRings || Form1.buyRingsAction) { RingOrAmulet = 1; }
                else if (Properties.Settings.Default.buyAmulets || Form1.buyRingsAction) { RingOrAmulet = 2; }
                else if (Properties.Settings.Default.buyBoosters) { RingOrAmulet = 3; }

                chooseOneMoreTime:
                string[] auctionForms;
                switch (RingOrAmulet)
                {
                    case 1:
                        AuctionHouse();
                        _BasicTasks.SelectElement("//select[@name='itemType']", "Pierścienie");
                        break;
                    case 2:
                        AuctionHouse();
                        _BasicTasks.SelectElement("//select[@name='itemType']", "Amulety");
                        break;
                    case 3:
                        Packages();
                        if (_BasicTasks.Search("//section[@style='display: none;']")) { _BasicTasks.Click("//h2[@class='section-header'][contains(text(), 'Opcje')]"); }
                        _BasicTasks.SelectElement("//select[@name='f']", "Przyspieszacze");
                        _BasicTasks.Click("//input[@value='Filtr']");
                        bool first_loop = true;
                        do
                        {
                            if (!first_loop)
                                _BasicTasks.Click("//a[@class='paging_button paging_right_step']");

                            for (int i = 0; i < 3; i++)
                            {
                                i_strength[i] += driver.FindElementsByXPath("//div[@class='" + strength[i] + "']").Count;
                                i_mastery[i] += driver.FindElementsByXPath("//div[@class='" + mastery[i] + "']").Count;
                                i_skill[i] += driver.FindElementsByXPath("//div[@class='" + skill[i] + "']").Count;
                                i_physic[i] += driver.FindElementsByXPath("//div[@class='" + physic[i] + "']").Count;
                                i_charisma[i] += driver.FindElementsByXPath("//div[@class='" + charisma[i] + "']").Count;
                                i_inteligence[i] += driver.FindElementsByXPath("//div[@class='" + inteligence[i] + "']").Count;
                                if (i != 3)
                                    i_health[i] += driver.FindElementsByXPath("//div[@class='" + health[i] + "']").Count;
                            }

                            first_loop = false;
                        } while (_BasicTasks.Search("//a[@class='paging_button paging_right_step']"));

                        bool need_boosters = false;
                        bool changed = false;
                        for (int i = 0; i < 3; i++)
                        {
                            changed = false;
                            if (i_strength[i] >= boosters_per_type) { i_strength[i] = 0; changed = true; }
                            if (!changed) { i_strength[i] -= boosters_per_type; i_strength[i] *= -1; need_boosters = true; }

                            changed = false;
                            if (i_mastery[i] >= boosters_per_type) { i_mastery[i] = 0; changed = true; }
                            if (!changed) { i_mastery[i] -= boosters_per_type; i_mastery[i] *= -1; need_boosters = true; }

                            changed = false;
                            if (i_skill[i] >= boosters_per_type) { i_skill[i] = 0; changed = true; }
                            if (!changed) { i_skill[i] -= boosters_per_type; i_skill[i] *= -1; need_boosters = true; }

                            changed = false;
                            if (i_physic[i] >= boosters_per_type) { i_physic[i] = 0; changed = true; }
                            if (!changed) { i_physic[i] -= boosters_per_type; i_physic[i] *= -1; need_boosters = true; }

                            changed = false;
                            if (i_charisma[i] >= boosters_per_type) { i_charisma[i] = 0; changed = true; }
                            if (!changed) { i_charisma[i] -= boosters_per_type; i_charisma[i] *= -1; need_boosters = true; }

                            changed = false;
                            if (i_inteligence[i] >= boosters_per_type) { i_inteligence[i] = 0; changed = true; }
                            if (!changed) { i_inteligence[i] -= boosters_per_type; i_inteligence[i] *= -1; need_boosters = true; }
                            
                            if (i != 3)
                            {
                                changed = false;
                                if (i_health[i] >= boosters_per_type) { i_health[i] = 0; changed = true; }
                                if (!changed) { i_health[i] -= boosters_per_type; i_health[i] *= -1; need_boosters = true; }
                            }
                        }
                        if (!need_boosters) { return; }

                        AuctionHouse();
                        _BasicTasks.SelectElement("//select[@name='itemType']", "Przyspieszacze");
                        break;
                    default:
                        return;
                }

                _BasicTasks.Click("//input[@value='Filtr']");
                IReadOnlyCollection<IWebElement> list = driver.FindElementsByXPath("//div[@id='auction_table']//form[@method='post']");
                auctionForms = new string[list.Count];

                for (int i = 0; i < list.Count; i++)
                    auctionForms[i] = Convert.ToString(list.ElementAt(i).GetAttribute("id"));

                if (RingOrAmulet == 3)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        string helper_string = "//div[@id='auction_table']//form[@id='" + auctionForms[i] + "']";
                        for (int j = 0; j < 3; j++)
                        {
                            string no_offers = driver.FindElementByXPath(helper_string + "//div[@class='auction_bid_div']/div").GetAttribute("textContent");
                            if (_BasicTasks.Search(helper_string + "//div[contains(@class,'" + strength[j] + "')]") && i_strength[j] != 0 && no_offers == "Brak ofert")
                            { _BasicTasks.Click(helper_string + "//input[@value='Licytuj']"); i_strength[j]--; }

                            if (_BasicTasks.Search(helper_string + "//div[contains(@class,'" + mastery[j] + "')]") && i_mastery[j] != 0 && no_offers == "Brak ofert")
                            { _BasicTasks.Click(helper_string + "//input[@value='Licytuj']"); i_mastery[j]--; }

                            if (_BasicTasks.Search(helper_string + "//div[contains(@class,'" + skill[j] + "')]") && i_skill[j] != 0 && no_offers == "Brak ofert")
                            { _BasicTasks.Click(helper_string + "//input[@value='Licytuj']"); i_skill[j]--; }

                            if (_BasicTasks.Search(helper_string + "//div[contains(@class,'" + physic[j] + "')]") && i_physic[j] != 0 && no_offers == "Brak ofert")
                            { _BasicTasks.Click(helper_string + "//input[@value='Licytuj']"); i_physic[j]--; }

                            if (_BasicTasks.Search(helper_string + "//div[contains(@class,'" + charisma[j] + "')]") && i_charisma[j] != 0 && no_offers == "Brak ofert")
                            { _BasicTasks.Click(helper_string + "//input[@value='Licytuj']"); i_charisma[j]--; }

                            if (_BasicTasks.Search(helper_string + "//div[contains(@class,'" + inteligence[j] + "')]") && i_inteligence[j] != 0 && no_offers == "Brak ofert")
                            { _BasicTasks.Click(helper_string + "//input[@value='Licytuj']"); i_inteligence[j]--; }

                            if (i != 3)
                                if (_BasicTasks.Search(helper_string + "//div[contains(@class,'" + health[j] + "')]") && i_health[j] != 0 && no_offers == "Brak ofert")
                                { _BasicTasks.Click(helper_string + "//input[@value='Licytuj']"); i_health[j]--; }
                        }
                    }
                    return;
                }

                for (int i = 0; i < list.Count; i++)
                {
                    string helperString = "//div[@id='auction_table']//form[@id='" + auctionForms[i] + "']";
                    string startPriceString = _BasicTasks.GetElement(helperString + "//input[@name='bid_amount']").GetAttribute("value");
                    int startPrice = Convert.ToInt32(new String(startPriceString.Where(char.IsDigit).ToArray()));

                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", driver.FindElementByXPath(helperString + "//div[@data-price-gold]"));
                    _BasicTasks.MoveTo(helperString + "//div[@data-price-gold]");
                    string valueString = _BasicTasks.GetElement("//p[@style='color:#DDDDDD']").GetAttribute("textContent");
                    int value = Convert.ToInt32(new String(valueString.Where(Char.IsDigit).ToArray()));

                    int helper = startPrice - value;

                    if (helper < 0)
                        helper *= -1;


                    string noOffers = driver.FindElementByXPath(helperString + "//div[@class='auction_bid_div']/div").GetAttribute("textContent");
                    noOffers = noOffers.Trim();

                    if (helper <= difference_price && noOffers == "Brak ofert")
                        _BasicTasks.Click(helperString + "//input[@value='Licytuj']");

                    if (_BasicTasks.Search("//div[@class='message fail']"))
                        break;
                }

                if (RingOrAmulet == 1 && Properties.Settings.Default.buyAmulets || Form1.buyRingsAction && !boughtAmulets)
                {
                    RingOrAmulet = 2;
                    boughtAmulets = true;
                    goto chooseOneMoreTime;
                }
                else if (RingOrAmulet == 1 || RingOrAmulet == 2 && Properties.Settings.Default.buyBoosters || Form1.buyRingsAction && !boughtBoosters)
                {
                    RingOrAmulet = 3;
                    boughtBoosters = true;
                    goto chooseOneMoreTime;
                }
            }
            Thread.Sleep(2000);
        }
        public void Buy_Auction_House2()
        {
            Form1.currently_running = "Buying from Auction House..";
            if (Properties.Settings.Default.buyRings || Properties.Settings.Default.buyAmulets || Properties.Settings.Default.buyBoosters || Form1.buyRingsAction)
            {
                if(Properties.Settings.Default.buyBoosters || Form1.buyRingsAction)
                {
                    int[] i_strength = new int[4];
                    string[] strength = new string[4];
                    strength[0] = "item-i-11-4 ui-draggable ui-droppable ui-draggable-handle";
                    strength[1] = "item-i-11-3 ui-draggable ui-droppable ui-draggable-handle";
                    strength[2] = "item-i-11-2 ui-draggable ui-droppable ui-draggable-handle";
                    strength[3] = "item-i-11-1 ui-draggable ui-droppable ui-draggable-handle";

                    int[] i_mastery = new int[4];
                    string[] mastery = new string[4];
                    mastery[0] = "item-i-11-8 ui-draggable ui-droppable ui-draggable-handle";
                    mastery[1] = "item-i-11-7 ui-draggable ui-droppable ui-draggable-handle";
                    mastery[2] = "item-i-11-6 ui-draggable ui-droppable ui-draggable-handle";
                    mastery[3] = "item-i-11-5 ui-draggable ui-droppable ui-draggable-handle";

                    int[] i_skill = new int[4];
                    string[] skill = new string[4];
                    skill[0] = "item-i-11-12 ui-draggable ui-droppable ui-draggable-handle";
                    skill[1] = "item-i-11-11 ui-draggable ui-droppable ui-draggable-handle";
                    skill[2] = "item-i-11-10 ui-draggable ui-droppable ui-draggable-handle";
                    skill[3] = "item-i-11-9 ui-draggable ui-droppable ui-draggable-handle";

                    int[] i_physic = new int[4];
                    string[] physic = new string[4];
                    physic[0] = "item-i-11-16 ui-draggable ui-droppable ui-draggable-handle";
                    physic[1] = "item-i-11-15 ui-draggable ui-droppable ui-draggable-handle";
                    physic[2] = "item-i-11-14 ui-draggable ui-droppable ui-draggable-handle";
                    physic[3] = "item-i-11-13 ui-draggable ui-droppable ui-draggable-handle";

                    int[] i_charisma = new int[4];
                    string[] charisma = new string[4];
                    charisma[0] = "item-i-11-20 ui-draggable ui-droppable ui-draggable-handle";
                    charisma[1] = "item-i-11-19 ui-draggable ui-droppable ui-draggable-handle";
                    charisma[2] = "item-i-11-18 ui-draggable ui-droppable ui-draggable-handle";
                    charisma[3] = "item-i-11-17 ui-draggable ui-droppable ui-draggable-handle";

                    int[] i_inteligence = new int[4];
                    string[] inteligence = new string[4];
                    inteligence[0] = "item-i-11-27 ui-draggable ui-droppable ui-draggable-handle";
                    inteligence[1] = "item-i-11-26 ui-draggable ui-droppable ui-draggable-handle";
                    inteligence[2] = "item-i-11-25 ui-draggable ui-droppable ui-draggable-handle";
                    inteligence[3] = "item-i-11-24 ui-draggable ui-droppable ui-draggable-handle";

                    int[] i_health = new int[3];
                    string[] health = new string[3];
                    health[0] = "item-i-11-23 ui-draggable ui-droppable ui-draggable-handle";
                    health[1] = "item-i-11-22 ui-draggable ui-droppable ui-draggable-handle";
                    health[2] = "item-i-11-21 ui-draggable ui-droppable ui-draggable-handle";
                    string[] auctionForms;
                    Packages();
                    if (_BasicTasks.Search("//section[@style='display: none;']")) { _BasicTasks.Click("//h2[@class='section-header'][contains(text(), 'Opcje')]"); }
                    _BasicTasks.SelectElement("//select[@name='f']", "Przyspieszacze");
                    _BasicTasks.Click("//input[@value='Filtr']");
                    bool first_loop = true;
                    do
                    {
                        if (!first_loop)
                            _BasicTasks.Click("//a[@class='paging_button paging_right_step']");

                        for (int i = 0; i <= 3; i++)
                        {
                            i_strength[i] += driver.FindElementsByXPath("//div[@class='" + strength[i] + "']").Count;
                            i_mastery[i] += driver.FindElementsByXPath("//div[@class='" + mastery[i] + "']").Count;
                            i_skill[i] += driver.FindElementsByXPath("//div[@class='" + skill[i] + "']").Count;
                            i_physic[i] += driver.FindElementsByXPath("//div[@class='" + physic[i] + "']").Count;
                            i_charisma[i] += driver.FindElementsByXPath("//div[@class='" + charisma[i] + "']").Count;
                            i_inteligence[i] += driver.FindElementsByXPath("//div[@class='" + inteligence[i] + "']").Count;
                            if (i != 3)
                                i_health[i] += driver.FindElementsByXPath("//div[@class='" + health[i] + "']").Count;
                        }

                        first_loop = false;
                    } while (_BasicTasks.Search("//a[@class='paging_button paging_right_step']"));

                    bool need_boosters = false;
                    bool changed = false;
                    for (int i = 0; i < 3; i++)
                    {
                        changed = false;
                        if (i_strength[i] >= boosters_per_type) { i_strength[i] = 0; changed = true; }
                        if (!changed) { i_strength[i] -= boosters_per_type; i_strength[i] *= -1; need_boosters = true; }

                        changed = false;
                        if (i_mastery[i] >= boosters_per_type) { i_mastery[i] = 0; changed = true; }
                        if (!changed) { i_mastery[i] -= boosters_per_type; i_mastery[i] *= -1; need_boosters = true; }

                        changed = false;
                        if (i_skill[i] >= boosters_per_type) { i_skill[i] = 0; changed = true; }
                        if (!changed) { i_skill[i] -= boosters_per_type; i_skill[i] *= -1; need_boosters = true; }

                        changed = false;
                        if (i_physic[i] >= boosters_per_type) { i_physic[i] = 0; changed = true; }
                        if (!changed) { i_physic[i] -= boosters_per_type; i_physic[i] *= -1; need_boosters = true; }

                        changed = false;
                        if (i_charisma[i] >= boosters_per_type) { i_charisma[i] = 0; changed = true; }
                        if (!changed) { i_charisma[i] -= boosters_per_type; i_charisma[i] *= -1; need_boosters = true; }

                        changed = false;
                        if (i_inteligence[i] >= boosters_per_type) { i_inteligence[i] = 0; changed = true; }
                        if (!changed) { i_inteligence[i] -= boosters_per_type; i_inteligence[i] *= -1; need_boosters = true; }

                        if (i != 3)
                        {
                            changed = false;
                            if (i_health[i] >= boosters_per_type) { i_health[i] = 0; changed = true; }
                            if (!changed) { i_health[i] -= boosters_per_type; i_health[i] *= -1; need_boosters = true; }
                        }
                    }
                    if (!need_boosters) { goto exit_boosters; }
                    AuctionHouse();
                    _BasicTasks.SelectElement("//select[@name='itemType']", "Przyspieszacze");
                    _BasicTasks.Click("//input[@value='Filtr']");
                    IReadOnlyCollection<IWebElement> list = driver.FindElementsByXPath("//div[@id='auction_table']//form[@method='post']");
                    auctionForms = new string[list.Count];
                    for (int i = 0; i < list.Count; i++)
                        auctionForms[i] = Convert.ToString(list.ElementAt(i).GetAttribute("id"));
                    for (int i = 0; i < list.Count; i++)
                    {
                        string helper_string = "//div[@id='auction_table']//form[@id='" + auctionForms[i] + "']";
                        for (int j = 0; j < 3; j++)
                        {
                            string no_offers = driver.FindElementByXPath(helper_string + "//div[@class='auction_bid_div']/div").GetAttribute("textContent");
                            no_offers = no_offers.Trim();
                            if (_BasicTasks.Search(helper_string + "//div[contains(@class,'" + strength[j] + "')]") && i_strength[j] != 0 && no_offers == "Brak ofert")
                            { _BasicTasks.Click(helper_string + "//input[@value='Licytuj']"); i_strength[j]--; }

                            if (_BasicTasks.Search(helper_string + "//div[contains(@class,'" + mastery[j] + "')]") && i_mastery[j] != 0 && no_offers == "Brak ofert")
                            { _BasicTasks.Click(helper_string + "//input[@value='Licytuj']"); i_mastery[j]--; }

                            if (_BasicTasks.Search(helper_string + "//div[contains(@class,'" + skill[j] + "')]") && i_skill[j] != 0 && no_offers == "Brak ofert")
                            { _BasicTasks.Click(helper_string + "//input[@value='Licytuj']"); i_skill[j]--; }

                            if (_BasicTasks.Search(helper_string + "//div[contains(@class,'" + physic[j] + "')]") && i_physic[j] != 0 && no_offers == "Brak ofert")
                            { _BasicTasks.Click(helper_string + "//input[@value='Licytuj']"); i_physic[j]--; }

                            if (_BasicTasks.Search(helper_string + "//div[contains(@class,'" + charisma[j] + "')]") && i_charisma[j] != 0 && no_offers == "Brak ofert")
                            { _BasicTasks.Click(helper_string + "//input[@value='Licytuj']"); i_charisma[j]--; }

                            if (_BasicTasks.Search(helper_string + "//div[contains(@class,'" + inteligence[j] + "')]") && i_inteligence[j] != 0 && no_offers == "Brak ofert")
                            { _BasicTasks.Click(helper_string + "//input[@value='Licytuj']"); i_inteligence[j]--; }

                            if (i != 3)
                                if (_BasicTasks.Search(helper_string + "//div[contains(@class,'" + health[j] + "')]") && i_health[j] != 0 && no_offers == "Brak ofert")
                                { _BasicTasks.Click(helper_string + "//input[@value='Licytuj']"); i_health[j]--; }

                            if (_BasicTasks.Search("//div[@class='message fail']"))
                                break;
                        }
                    }
                    goto exit_boosters;
                }
                exit_boosters:

                if(Properties.Settings.Default.buyRings || Form1.buyRingsAction)
                {
                    string[] auctionForms;
                    AuctionHouse();
                    _BasicTasks.SelectElement("//select[@name='itemType']", "Pierścienie");
                    _BasicTasks.Click("//input[@value='Filtr']");
                    IReadOnlyCollection<IWebElement> list = driver.FindElementsByXPath("//div[@id='auction_table']//form[@method='post']");
                    auctionForms = new string[list.Count];

                    for (int i = 0; i < list.Count; i++)
                        auctionForms[i] = Convert.ToString(list.ElementAt(i).GetAttribute("id"));
                    for (int i = 0; i < list.Count; i++)
                    {
                        string helperString = "//div[@id='auction_table']//form[@id='" + auctionForms[i] + "']";
                        string startPriceString = _BasicTasks.GetElement(helperString + "//input[@name='bid_amount']").GetAttribute("value");
                        int startPrice = Convert.ToInt32(new String(startPriceString.Where(char.IsDigit).ToArray()));

                        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", driver.FindElementByXPath(helperString + "//div[@data-price-gold]"));
                        _BasicTasks.MoveTo(helperString + "//div[@data-price-gold]");
                        string valueString = _BasicTasks.GetElement("//p[@style='color:#DDDDDD']").GetAttribute("textContent");
                        int value = Convert.ToInt32(new String(valueString.Where(Char.IsDigit).ToArray()));

                        int helper = startPrice - value;

                        if (helper < 0)
                            helper *= -1;


                        string noOffers = driver.FindElementByXPath(helperString + "//div[@class='auction_bid_div']/div").GetAttribute("textContent");
                        noOffers = noOffers.Trim();

                        if (helper <= difference_price && noOffers == "Brak ofert")
                            _BasicTasks.Click(helperString + "//input[@value='Licytuj']");

                        if (_BasicTasks.Search("//div[@class='message fail']"))
                            break;
                    }
                    goto exit_rings;
                }
                exit_rings:

                if(Properties.Settings.Default.buyAmulets || Form1.buyRingsAction)
                {
                    string[] auctionForms;
                    AuctionHouse();
                    _BasicTasks.SelectElement("//select[@name='itemType']", "Amulety");
                    _BasicTasks.Click("//input[@value='Filtr']");
                    IReadOnlyCollection<IWebElement> list = driver.FindElementsByXPath("//div[@id='auction_table']//form[@method='post']");
                    auctionForms = new string[list.Count];

                    for (int i = 0; i < list.Count; i++)
                        auctionForms[i] = Convert.ToString(list.ElementAt(i).GetAttribute("id"));
                    for (int i = 0; i < list.Count; i++)
                    {
                        string helperString = "//div[@id='auction_table']//form[@id='" + auctionForms[i] + "']";
                        string startPriceString = _BasicTasks.GetElement(helperString + "//input[@name='bid_amount']").GetAttribute("value");
                        int startPrice = Convert.ToInt32(new String(startPriceString.Where(char.IsDigit).ToArray()));

                        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", driver.FindElementByXPath(helperString + "//div[@data-price-gold]"));
                        _BasicTasks.MoveTo(helperString + "//div[@data-price-gold]");
                        string valueString = _BasicTasks.GetElement("//p[@style='color:#DDDDDD']").GetAttribute("textContent");
                        int value = Convert.ToInt32(new String(valueString.Where(Char.IsDigit).ToArray()));

                        int helper = startPrice - value;

                        if (helper < 0)
                            helper *= -1;


                        string noOffers = driver.FindElementByXPath(helperString + "//div[@class='auction_bid_div']/div").GetAttribute("textContent");
                        noOffers = noOffers.Trim();

                        if (helper <= difference_price && noOffers == "Brak ofert")
                            _BasicTasks.Click(helperString + "//input[@value='Licytuj']");

                        if (_BasicTasks.Search("//div[@class='message fail']"))
                            break;
                    }
                }
            }
        }
        public void SellItems(bool clickSell)
        {
            Form1.currently_running = "Selling items..";
            if (Properties.Settings.Default.itemsSell || Form1.sellItemsAction)
            {
                #region Variables
                int categoryNumber = 1;
                int shop = 1;
                bool first = true;
                string file_path = @"C:\Users\danie\Documents\Visual Studio 2017\Resources\Gladiatus_bots\Selling items\selling_items" + server_number + ".txt";
                start:

                bool gotAtLeastOne = false;
                bool secondTabWarrior = false;
                string variable = "";
                IReadOnlyCollection<IWebElement> list;
                List<string> collectionReady = new List<string>();
                List<string> collectionSelling = new List<string>();
                List<string> collectionReadyClass = new List<string>();
                #endregion

                if(File.Exists(file_path))
                {
                    _BasicTasks.Click("//a[@title='Podgląd']");
                    if (!FreeBackpack())
                        return;
                    string[] lines = File.ReadAllLines(file_path);
                    for(int i=0; i<lines.Length; i++)
                    {
                        if (_BasicTasks.Search("//div[@id='inv']//div[@data-hash='" + lines[i] + "']"))
                            collectionSelling.Add(lines[i]);
                    }
                }

                #region ChooseCategory
                Packages();
                if (!_BasicTasks.Search("//section[@style='display: none;']"))
                { _BasicTasks.Click("//h2[contains(text(),'Opcje')]"); }

                if (!clickSell)
                {
                    switch (categoryNumber)
                    {
                        case 1:
                            if (sell_weapons) { variable = "Bronie"; }
                            else { categoryNumber++; goto start; }
                            break;
                        case 2:
                            if (sell_shields) { variable = "Tarcze"; }
                            else { categoryNumber++; goto start; }
                            break;
                        case 3:
                            if (sell_breastplates) { variable = "Napierśniki"; }
                            else { categoryNumber++; goto start; }
                            break;
                        case 4:
                            if (sell_helmets) { variable = "Hełmy"; }
                            else { categoryNumber++; goto start; }
                            break;
                        case 5:
                            if (sell_gloves) { variable = "Rękawice"; }
                            else { categoryNumber++; goto start; }
                            break;
                        case 6:
                            if (sell_shoes) { variable = "Buty"; }
                            else { categoryNumber++; goto start; }
                            break;
                        case 7:
                            if (sell_rings) { variable = "Pierścienie"; }
                            else { categoryNumber++; goto start; }
                            break;
                        case 8:
                            if (sell_amulets) { variable = "Amulety"; }
                            else { categoryNumber++; goto start; }
                            break;
                        case 9:
                            if (sell_accelerators) { variable = "Przyspieszacze"; }
                            else { categoryNumber++; goto start; }
                            break;
                        case 10:
                            if (sell_bonuses) { variable = "Bonusy"; }
                            else { categoryNumber++; goto start; }
                            break;
                        case 11:
                            if (sell_blessings) { variable = "Błogosławieństwa"; }
                            else { categoryNumber++; goto start; }
                            break;
                        case 12:
                            if (sell_scrolls) { variable = "Zwój"; }
                            else { categoryNumber++; goto start; }
                            break;
                        default:
                            if (File.Exists(file_path))
                                File.Delete(file_path);
                            return;
                    }
                }
                else
                {
                    if (Gold_Level() > Convert.ToInt32(Properties.Settings.Default.gold_level) && Properties.Settings.Default.gold_limit)
                        return;
                    switch (categoryNumber)
                    {
                        case 1:
                            if (sell_weapons_click) { variable = "Bronie"; }
                            else { categoryNumber++; goto start; }
                            break;
                        case 2:
                            if (sell_shields_click) { variable = "Tarcze"; }
                            else { categoryNumber++; goto start; }
                            break;
                        case 3:
                            if (sell_breastplates_click) { variable = "Napierśniki"; }
                            else { categoryNumber++; goto start; }
                            break;
                        case 4:
                            if (sell_helmets_click) { variable = "Hełmy"; }
                            else { categoryNumber++; goto start; }
                            break;
                        case 5:
                            if (sell_gloves_click) { variable = "Rękawice"; }
                            else { categoryNumber++; goto start; }
                            break;
                        case 6:
                            if (sell_shoes_click) { variable = "Buty"; }
                            else { categoryNumber++; goto start; }
                            break;
                        case 7:
                            if (first) { shop = 1; first = false; }
                            if (sell_rings_click) { variable = "Pierścienie"; }
                            else { categoryNumber++; goto start; }
                            break;
                        case 8:
                            if (first) { shop = 1; first = false; }
                            if (sell_amulets_click) { variable = "Amulety"; }
                            else { categoryNumber++; goto start; }
                            break;
                        case 9:
                            if (first) { shop = 1; first = false; }
                            if (sell_accelerators_click) { variable = "Przyspieszacze"; }
                            else { categoryNumber++; goto start; }
                            break;
                        case 10:
                            if (first) { shop = 1; first = false; }
                            if (sell_bonuses_click) { variable = "Bonusy"; }
                            else { categoryNumber++; goto start; }
                            break;
                        case 11:
                            if (first) { shop = 1; first = false; }
                            if (sell_blessings_click) { variable = "Błogosławieństwa"; }
                            else { categoryNumber++; goto start; }
                            break;
                        case 12:
                            if (first) { shop = 1; first = false; }
                            if (sell_scrolls_click) { variable = "Zwój"; }
                            else { categoryNumber++; goto start; }
                            break;
                        default:
                            if (File.Exists(file_path))
                                File.Delete(file_path);
                            return;
                    }
                }

                if (_BasicTasks.Search("//section[@style='display: none;']"))
                { _BasicTasks.Click("//h2[@class='section-header'][contains(text(), 'Opcje')]"); }
                _BasicTasks.SelectElement("//select[@name='f']", variable);
                _BasicTasks.Click("//input[@value='Filtr']");
                #endregion

                #region GetItemsFromPackages
                int clickedTimes = 0;
                if (_BasicTasks.Search("//a[@class='paging_button paging_right_full']"))
                { _BasicTasks.Click("//a[@class='paging_button paging_right_full']"); }
                for (int i = 0; i < 3; i++)
                {
                    if (_BasicTasks.Search("//a[@class='paging_button paging_left_step']"))
                    { _BasicTasks.Click("//a[@class='paging_button paging_left_step']"); }
                }
                list = driver.FindElementsByXPath("//div[@id='packages']//div[contains(@class,'ui-draggable')]");
                for (int i = 0; i < 3; i++)
                {
                    if (_BasicTasks.Search("//div[@id='packages']//div[contains(@class,'ui-draggable')]"))
                    {
                        list = driver.FindElementsByXPath("//div[@id='packages']//div[contains(@class,'ui-draggable')]");
                        collectionReady.AddRange(FindReadyObjects(list, categoryNumber));
                        collectionReadyClass.AddRange(FindReadyObjectsClass(list, categoryNumber));
                    }

                    if (_BasicTasks.Search("//a[@class='paging_button paging_right_step']"))
                    { _BasicTasks.Click("//a[@class='paging_button paging_right_step']"); clickedTimes++; }
                    else { break; }
                }

                for (int i = 0; i < clickedTimes; i++)
                { _BasicTasks.Click("//a[@class='paging_button paging_left_step']"); }

                if (collectionReady.Count == 0)
                { categoryNumber++; goto start; }

                if (!FreeBackpack()) { return; }
                if (!_BasicTasks.Search("//section[@style='display: none;']")) { _BasicTasks.Click("//h2[@class='section-header'][contains(text(), 'Opcje')]"); }
                for (int i = 0; i < collectionReady.Count; i++)
                {
                    bool done = false;
                    bool noPlace = false;
                    while (!done)
                    {
                        if (_BasicTasks.Search("//div[@id='packages']//div[@data-hash='" + collectionReady.ElementAt(i) + "']"))
                        { _BasicTasks.MoveMoveElement("//div[@id='packages']//div[@data-hash='" + collectionReady.ElementAt(i) + "']", "//input[@name='show-item-info']"); }
                        else { done = true; continue; }

                        if (!_BasicTasks.Search("//body[@id='packagesPage']/div[contains(@class,'" + collectionReadyClass.ElementAt(i) + "')]"))
                        { _BasicTasks.ReleaseElement("//input[@name='show-item-info']"); driver.Navigate().Refresh(); FreeBackpack(); continue; }

                        if (_BasicTasks.Search("//div[@class='ui-droppable grid-droparea image-grayed active']"))
                        {
                            _BasicTasks.ReleaseElement("//div[@class='ui-droppable grid-droparea image-grayed active']");
                            if (_BasicTasks.Search("//div[@id='inv']//div[@data-hash='" + collectionReady.ElementAt(i) + "']"))
                            {
                                collectionSelling.Add(collectionReady.ElementAt(i));
                                gotAtLeastOne = true;
                                done = true;
                                continue;
                            }
                        }
                        else { _BasicTasks.ReleaseElement("//input[@name='show-item-info']"); noPlace = true; break; }
                    }
                    if (noPlace && !gotAtLeastOne) { return; }
                    else if (noPlace) { break; }
                }
                if (!gotAtLeastOne) { categoryNumber++; goto start; }
                #endregion
                if (File.Exists(file_path))
                {
                    string[] temporary_lines = File.ReadAllLines(file_path);
                    List<string> save_to_file = new List<string>();
                    for (int i = 0; i < collectionSelling.Count; i++)
                    {
                        if (!temporary_lines.Contains(collectionSelling.ElementAt(i)))
                            save_to_file.Add(collectionSelling.ElementAt(i));
                    }
                    File.AppendAllLines(file_path, save_to_file);
                }
                else
                {
                    File.AppendAllLines(file_path, collectionSelling);
                }
                #region Shop Choose
                changeShop:
                gotAtLeastOne = false;
                if (categoryNumber >= 7)
                {
                    switch (shop)
                    {
                        case 1:
                            Weapon_Seller();
                            Third_Tab_Sellers();
                            break;
                        case 2:
                            Breastplates_Seller();
                            Third_Tab_Sellers();
                            break;
                        case 3:
                            Shopkeeper_Seller();
                            Second_Tab_Sellers();
                            break;
                        case 4:
                            Shopkeeper_Seller();
                            Third_Tab_Sellers();
                            break;
                        case 5:
                            Alchemic_Seller();
                            break;
                        case 6:
                            Alchemic_Seller();
                            Second_Tab_Sellers();
                            break;
                        case 7:
                            Alchemic_Seller();
                            Third_Tab_Sellers();
                            break;
                        case 8:
                            Soldier_Seller();
                            break;
                        case 9:
                            Soldier_Seller();
                            Second_Tab_Sellers();
                            break;
                        case 10:
                            Soldier_Seller();
                            Third_Tab_Sellers();
                            break;
                        case 11:
                            Malefica_Seller();
                            break;
                        case 12:
                            Malefica_Seller();
                            Second_Tab_Sellers();
                            break;
                        case 13:
                            Malefica_Seller();
                            Third_Tab_Sellers();
                            break;
                        default:
                            return;
                    }
                }
                else
                {
                    switch (shop)
                    {
                        case 1:
                            Weapon_Seller();
                            Third_Tab_Sellers();
                            break;
                        case 2:
                            Breastplates_Seller();
                            Third_Tab_Sellers();
                            break;
                        case 3:
                            Shopkeeper_Seller();
                            Third_Tab_Sellers();
                            break;
                        case 4:
                            Alchemic_Seller();
                            Third_Tab_Sellers();
                            break;
                        case 5:
                            Soldier_Seller();
                            Third_Tab_Sellers();
                            break;
                        case 6:
                            Malefica_Seller();
                            Third_Tab_Sellers();
                            break;
                        default:
                            return;
                    }
                }
                #endregion

                #region Selling Shop
                FreeBackpack();

                string rublesString = _BasicTasks.GetElement("//div[@id='sstat_ruby_val']").GetAttribute("textContent");
                int rubles = Convert.ToInt32(rublesString);

                FreeBackpack();
                notMovedYet:
                while (collectionSelling.Count != 0)
                {
                    if (_BasicTasks.Search("//div[@id='shop']//div[@data-hash='" + collectionSelling.ElementAt(0) + "']") 
                        || !_BasicTasks.Search("//div[@id='inv']//div[@data-hash='" + collectionSelling.ElementAt(0) + "']"))
                    { collectionSelling.RemoveAt(0); continue; }
                    _BasicTasks.MoveMoveElement("//div[@id='inv']//div[@data-hash='" + collectionSelling.ElementAt(0) + "']", "//a[@class='awesome-tabs current']");
                    if (_BasicTasks.Search("//div[@id='shop']//div[@class='ui-droppable grid-droparea image-grayed active']"))
                    {
                        _BasicTasks.ReleaseElement("//div[@id='shop']//div[@class='ui-droppable grid-droparea image-grayed active']");
                    }
                    else
                    {
                        _BasicTasks.ReleaseElement("//a[@class='awesome-tabs current']");
                        if (shop != 13 && categoryNumber >= 7)
                        { shop++; goto changeShop; }
                        else if (rubles != 0 && collectionSelling.Count != 0 && categoryNumber >= 7)
                        { _BasicTasks.Click("//input[@value='Nowe towary']"); shop = 1; goto changeShop; }
                        else if (shop == 5 && !secondTabWarrior && collectionSelling.Count != 0)
                        { secondTabWarrior = true; _BasicTasks.Click("//div[@class='shopTab'][text() = 'Ⅱ']"); goto notMovedYet; }
                        else if (shop == 6 && collectionSelling.Count != 0 && rubles != 0)
                        { _BasicTasks.Click("//input[@value='Nowe towary']"); shop = 1; goto changeShop; }
                        else
                        { shop++; goto changeShop; }
                    }
                }
                collectionSelling.Clear();
                bool found = false;
                for (int i = 0; i < collectionReady.Count; i++)
                {
                    if (_BasicTasks.Search("//div[@id='inv']//div[@data-hash='" + collectionReady.ElementAt(i) + "']"))
                    { collectionSelling.Add(collectionReady.ElementAt(i)); found = true; }
                }
                if (found) { goto notMovedYet; }
                #endregion

                goto start;
            }
        }
        public void MovingFood()
        {
            Form1.currently_running = "Moving food..";
            if (Properties.Settings.Default.moveFood)
            {
                bool firstTime = true;
                #region Filtr
                if (firstTime)
                {
                    firstTime = false;
                    Packages();
                    if (_BasicTasks.Search("//section[@style='display: none;']")) { _BasicTasks.Click("//h2[contains(text(),'Opcje')]"); }
                    _BasicTasks.SelectElement("//select[@name='f']", "Jadalne");
                    _BasicTasks.Click("//input[@value='Filtr']");
                    if (_BasicTasks.Search("//a[@class='paging_button paging_right_full']")) { _BasicTasks.Click("//a[@class='paging_button paging_right_full']"); }
                }
                #endregion Filtr
                #region Moving
                if (!FoodBackpack()) { return; }
                while (_BasicTasks.Search("//div[@class='packageItem']//div[contains(@data-content-type,'64')]"))
                {
                    food_exist_packages = true;
                    if (_BasicTasks.Search(food_backpack_string + "[@data-available='false']")) { return; }
                    if (!_BasicTasks.Search(food_backpack_string + "[@class='awesome-tabs current']")) { FoodBackpack(); }
                    _BasicTasks.MoveMoveElement("//div[@class='packageItem']//div[contains(@data-content-type,'64')]", "//div[@id='inv']");
                    if (_BasicTasks.Search("//div[@id='inv']//div[@class='ui-droppable grid-droparea image-grayed active']"))
                    { _BasicTasks.ReleaseElement("//div[@id='inv']//div[@class='ui-droppable grid-droparea image-grayed active']"); }
                    else { return; }
                }
                if (!_BasicTasks.Search("//div[@class='packageItem']//div[contains(@data-content-type,'64')]"))
                {
                    BuyFood();
                    food_exist_packages = false;
                    return;
                }
                #endregion
            }
        }
        public void Arena35()
        {
            Form1.currently_running = "Arena between servers..";
            if (Properties.Settings.Default.arena35Checked)
            {
                HealMe();
                _BasicTasks.WaitForXPath("//div[@id='cooldown_bar_arena']/div[@class='cooldown_bar_text']");
                _BasicTasks.Click("//div[@id='cooldown_bar_arena']");
                if (!_BasicTasks.Search("//a[contains(@href,'serverArena&aType=2')][@class='awesome-tabs current']"))
                { _BasicTasks.Click("//a[contains(@href,'serverArena&aType=2')]"); }
                _BasicTasks.Click("//div[@class='attack']");
                if (_BasicTasks.Search("//div[@id='header_bod']"))
                {
                    if (driver.FindElementByXPath("//div[@id='header_bod']").Displayed &&
                        driver.FindElementByXPath("//div[@id='header_bod']").Enabled)
                    { _BasicTasks.Click("//input[@value='Jazda!']"); }
                }
                while (!_BasicTasks.Search("//body[@id='reportsPage']"))
                {
                    if (_BasicTasks.Search("//div[@id='errorText']")) { return; }
                    Thread.Sleep(500);
                }
            }
        }
        public void Turma35()
        {
            Form1.currently_running = "Turma between servers..";
            if (Properties.Settings.Default.circusTurma35Checked)
            {
                _BasicTasks.WaitForXPath("//div[@id='cooldown_bar_ct']/div[@class='cooldown_bar_text']");
                _BasicTasks.Click("//div[@id='cooldown_bar_ct']");
                if (!_BasicTasks.Search("//a[contains(@href,'serverArena&aType=3')][@class='awesome-tabs current']"))
                { _BasicTasks.Click("//a[contains(@href,'serverArena&aType=3')]"); }
                _BasicTasks.Click("//div[@class='attack']");
                if (_BasicTasks.Search("//div[@id='header_bod']"))
                {
                    if (driver.FindElementByXPath("//div[@id='header_bod']").Displayed &&
                        driver.FindElementByXPath("//div[@id='header_bod']").Enabled)
                    { _BasicTasks.Click("//input[@value='Jazda!']"); }
                }
                for (int i = 0; i < 5; i++)
                {
                    if (_BasicTasks.Search("//div[@id='errorText']")) { return; }
                    if (!_BasicTasks.Search("//body[@id='reportsPage']")) { Thread.Sleep(1000); }
                    else { return; }
                    Thread.Sleep(500);
                }
            }
        }
        public void ExtractItems()
        {
            Form1.currently_running = "Extracting items..";
            if (!Properties.Settings.Default.extractItems) { return; }
            Arena();
            _BasicTasks.Click("//a[@class='menuitem '][text() = 'Roztapiarka']");

            ExtractBackpack();
            for (int i = 0; i < 2; i++)
            {
                if (_BasicTasks.Search("//div[contains(@class,'forge_closed " + i + "')]") && _BasicTasks.Search("//div[@id='inv']//div[contains(@class,'ui-draggable')]"))
                {
                    if (!ExtractBackpack()) { return; }
                    _BasicTasks.Click("//div[contains(@class,'forge_closed " + Convert.ToString(i) + "')]");
                    _BasicTasks.MoveReleaseElement("//div[@id='inv']//div[contains(@class,'ui-draggable')]", "//fieldset[@id='crafting_input']//div[@class='ui-droppable']");
                    _BasicTasks.Click("//div[@class='icon_gold']");
                }
                else if (_BasicTasks.Search("//div[contains(@class,'forge_finished-succeeded " + Convert.ToString(i) + "')]") && _BasicTasks.Search("//div[@id='inv']//div[contains(@class,'ui-draggable')]"))
                {
                    if (!ExtractBackpack()) { return; }
                    _BasicTasks.Click("//div[contains(@class,'forge_finished-succeeded " + Convert.ToString(i) + "')]");
                    _BasicTasks.Click("//div[@class='awesome-button'][text() = 'Wyślij jako pakiet']");
                    _BasicTasks.MoveReleaseElement("//div[@id='inv']//div[contains(@class,'ui-draggable')]", "//fieldset[@id='crafting_input']//div[@class='ui-droppable']");
                    _BasicTasks.Click("//div[@class='icon_gold']");
                }
            }
        }
        public void TakeGold()
        {
            Form1.currently_running = "Taking out gold..";
            Packages();
            _BasicTasks.SelectElement("//select[@name='f']", "Złoto");
            _BasicTasks.Click("//input[@value='Filtr']");
            if (_BasicTasks.Search("//a[@class='paging_button paging_right_full']")) { _BasicTasks.Click("//a[@class='paging_button paging_right_full']"); }
            if (!Properties.Settings.Default.gold_limit)
            {
                while (_BasicTasks.Search("//div[@class='packageItem']//div[contains(@class,'ui-draggable')]") && Form1.takeGoldAction)
                {
                    _BasicTasks.MoveMoveElement("//div[@class='packageItem']//div[contains(@class,'ui-draggable')]", "//div[@id='inv']");
                    if (_BasicTasks.Search("//div[contains(@class,'ui-droppable')]")) { _BasicTasks.ReleaseElement("//div[contains(@class,'ui-droppable')]"); }
                    else { return; }
                }
            }
            else
            {
                int needed_gold = Convert.ToInt32(Properties.Settings.Default.gold_level);
                while (Gold_Level() < needed_gold)
                {
                    int gold_before = Gold_Level();
                    IWebElement element = driver.FindElementByXPath("//div[@class='packageItem']//div[contains(@class,'ui-draggable')]");
                    string current_element_string = element.GetAttribute("data-price-gold");
                    int current_element = Convert.ToInt32(current_element_string);
                    if (Gold_Level() + current_element < needed_gold)
                    {
                        _BasicTasks.MoveMoveElement("//div[@class='packageItem']//div[@data-price-gold='" + current_element_string + "']", "//div[@id='inv']");
                        if (_BasicTasks.Search("//div[contains(@class,'ui-droppable')]"))
                        { _BasicTasks.ReleaseElement("//div[contains(@class,'ui-droppable')]"); }
                        else { continue; }
                    }
                    else
                    {
                        IReadOnlyCollection<IWebElement> list = driver.FindElementsByXPath("//div[@class='packageItem']//div[contains(@class,'ui-draggable')]");
                        string best_choose_string = list.ElementAt(0).GetAttribute("data-price-gold");
                        int best_choose = (Gold_Level() + Convert.ToInt32(list.ElementAt(0).GetAttribute("data-price-gold"))) - needed_gold;
                        int position = 0;
                        for (int i = 0; i < list.Count; i++)
                        {
                            int temporary = (Gold_Level() + Convert.ToInt32(list.ElementAt(i).GetAttribute("data-price-gold"))) - needed_gold;
                            if (temporary < best_choose)
                            {
                                best_choose = temporary;
                                position = i;
                                best_choose_string = list.ElementAt(i).GetAttribute("data-price-gold");
                            }
                        }
                        _BasicTasks.MoveMoveElement("//div[@class='packageItem']//div[@data-price-gold='" + best_choose_string + "']", "//div[@id='inv']");
                        if (_BasicTasks.Search("//div[contains(@class,'ui-droppable')]"))
                        { _BasicTasks.ReleaseElement("//div[contains(@class,'ui-droppable')]"); }
                        else { continue; }
                    }
                    bool changed = false;
                    for (int i = 0; i < 5; i++)
                    {
                        if (Gold_Level() != gold_before)
                        { Thread.Sleep(1000); changed = true; }
                    }
                    if (!changed) { driver.Navigate().Refresh(); }
                }
            }
        }
        public void Training()
        {
            Form1.currently_running = "Training..";
            if (Properties.Settings.Default.trainingChecked)
            {
                string variable = "";
                switch (Properties.Settings.Default.trainingOption)
                {
                    case 0:
                        variable = "1";
                        break;
                    case 1:
                        variable = "2";
                        break;
                    case 2:
                        variable = "3";
                        break;
                    case 3:
                        variable = "4";
                        break;
                    case 4:
                        variable = "5";
                        break;
                    case 5:
                        variable = "6";
                        break;
                    default:
                        return;
                }

                Arena();
                _BasicTasks.Click("//a[@class='menuitem '][text() = 'Trening']");
                while (driver.FindElementsByXPath("//a[contains(@href,'skillToTrain=" + variable + "')]").Count != 0)
                {
                    _BasicTasks.Click("//a[contains(@href,'skillToTrain=" + variable + "')]");
                }
            }
        }
        public void DungeonEvent()
        {
            Form1.currently_running = "Event war..";
            if (dungeon_event_ready || !Properties.Settings.Default.eventWar) { return; }

            bool first, second = false;
            first = _BasicTasks.Search("//a[contains(@class,'menuitem glow eyecatcher')]");
            if (!first) { second = _BasicTasks.Search("//a[contains(@class,'menuitem active glow eyecatcher')]"); }

            if (!first && !second) { dungeon_event_ready = true; return; }

            int eventPoints = 0;
            _BasicTasks.Click("//div[@id='cooldown_bar_expedition']/a[@class='cooldown_bar_link']");
            if (first)
            { _BasicTasks.Click("//div[@id='submenu2']/a[contains(@class,'menuitem glow eyecatcher')]"); }
            else
            { _BasicTasks.Click("//a[contains(@class,'menuitem active glow eyecatcher')]"); }

            if (!_BasicTasks.Search("//div[@class='section-header']/p[2]")) { dungeon_event_ready = true; return; }
            string element = driver.FindElementByXPath("//div[@class='section-header']/p[2]").GetAttribute("textContent");
            string[] separated = element.Split(' ');

            for (int i = 0; i <= separated.Length; i++)
            {
                try { eventPoints = Convert.ToInt32(separated[i]); break; }
                catch { }
            }

            if (eventPoints == 0) { dungeon_event_ready = true; return; }

            if (eventPoints > 0 && _BasicTasks.Search("//button[@class='expedition_button awesome-button ']"))
            {
                _BasicTasks.Click("//button[@class='expedition_button awesome-button ']");
            }
        }
        public int ReturnInt(string ścieżka)
        {
            int value = 0;
            for (int i = 0; i < 2; i++)
            {
                IWebElement element = _BasicTasks.GetElement(ścieżka);
                try { value = Convert.ToInt32(element.GetAttribute("textContent")); return value; }
                catch { _BasicTasks.Refresh(); }
            }
            return value;
        }
        public int Gold_Level()
        {
            string goldBeforeString = _BasicTasks.GetElement("//div[@id='sstat_gold_val']").GetAttribute("textContent");
            goldBeforeString = goldBeforeString.Replace(".", "");
            int gold = Convert.ToInt32(goldBeforeString);
            return gold;
        }
        public string Return_String(string path)
        {
            bool non_stop = true;
            while (non_stop)
            {
                try
                {
                    return _BasicTasks.GetElement(path).GetAttribute("textContent");
                }
                catch { }
            }
            return _BasicTasks.GetElement(path).GetAttribute("textContent");
        }
        public void Waith_Auction_House()
        {
            AuctionHouse();
            bool non_stop = true;
            while (non_stop && Form1.watch_auctions)
            {
                IWebElement element = driver.FindElementByXPath("//span[@class='description_span_right']");
                string element_current = element.GetAttribute("textContent");
                if (element_current == "bardzo krótki")
                {
                    System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"C:\Users\danie\OneDrive\Dokumenty\Visual Studio 2017\Resources\Alien.wav");
                    player.Play();
                    return;
                }
                _BasicTasks.Refresh();
                Thread.Sleep(500);
            }
        }
        public bool Take_Pater_Costume()
        {
            if (Convert.ToInt32(driver.FindElementByXPath("//div[@id='header_values_level']").GetAttribute("textContent")) < 100)
                return false;
            if (_BasicTasks.Search("//div[contains(@onmousemove,'Zbroja Disa Patera')]"))
                return false;
            _BasicTasks.Click("//a[@title='Podgląd']");
            _BasicTasks.Click("//input[@value='zmień']");
            if (_BasicTasks.Search("//input[contains(@onclick,'Zbroja Disa Patera')]"))
            {
                _BasicTasks.Click("//input[contains(@onclick,'Zbroja Disa Patera')]");
                _BasicTasks.Click("//td[@id='buttonleftchangeCostume']/input[@value='Tak']");
                return true;
            }
            return false;
        }
        public void Store_Components()
        {
            if (!Properties.Settings.Default.send_components)
                return;
            Arena();
            _BasicTasks.Click("//a[contains(@class,'menuitem')][text() = 'Magazyn surowców']");
            while (_BasicTasks.Search("//button[@id='store'][@disabled='']"))
                _BasicTasks.Click("//input[@id='from-packages']");
            _BasicTasks.Click("//button[@id='store'][text() = 'Jazda!']");
        }
        public void Download_Packages()
        {
            string file_path =
            @"C:\Users\danie\Documents\Visual Studio 2017\Resources\Gladiatus_bots\Items .txt files for Gladiatus_bot" + @"\items" + server_number + ".txt";
            if (File.Exists(file_path))
                File.Delete(file_path);
            bool first = true;
            do
            {
                Guild_Market();
                if (!first) { _BasicTasks.Click("//a[contains(text(),'Następna strona')]"); }
                int first_iterator = driver.FindElementsByXPath("//input[@value='Kup']").Count();
                int second_iterator = driver.FindElementsByXPath("//input[@value='Anuluj']").Count();
                int iterator = first_iterator + second_iterator;
                for (int i = 2; i <= iterator; i++)
                {
                    IWebElement element = driver.FindElementByXPath("//section[@id='market_table']//tr[position()='" + i + "']/td[@style]/div[@style]");
                    string soulbound = element.GetAttribute("data-soulbound-to");
                    string price_item_string = driver.FindElementByXPath
                        ("//section[@id='market_table']//tr[position()='" + i + "']/td[position()='3']").GetAttribute("textContent");
                    string level = element.GetAttribute("data-level");
                    string quality_level = element.GetAttribute("data-quality");
                    string amount = element.GetAttribute("data-amount");
                    string price_item = new String(price_item_string.Where(Char.IsDigit).ToArray());
                    string category = element.GetAttribute("data-content-type");
                    string class_name = element.GetAttribute("class");

                    string already_sold = "0";
                    _BasicTasks.MoveTo("//section[@id='market_table']//tr[position()='" + i + "']/td[@style]/div[@style]");
                    if (_BasicTasks.Search("//p[contains(text(),'Wskazówka')]"))
                        already_sold = "1";

                    if (soulbound == null)
                        soulbound = "null";
                    if (level == null)
                        level = "null";
                    if (quality_level == null)
                        quality_level = "null";
                    if (amount == null)
                        amount = "null";
                    if (category == null)
                        category = "null";
                    if (class_name == null)
                        category = "null";
                    string ready_line = class_name + " " + soulbound + " " + price_item + " " + category + " " + quality_level + " " + level + " " + amount + " " + already_sold;
                    File.AppendAllText(file_path, ready_line + Environment.NewLine);
                }
                first = false;
            } while (_BasicTasks.Search("//a[contains(text(),'Następna strona')]"));
        }
        #endregion

        #region PRIVATE
        string Type_Pack(string choose)
        {
            if (choose == "null") { return "Wszystko"; }

            int example = Convert.ToInt32(choose);
            switch (example)
            {
                case 2:
                    return "Bronie";
                case 4:
                    return "Tarcze";
                case 8:
                    return "Napierśniki";
                case 1:
                    return "Hełmy";
                case 256:
                    return "Rękawice";
                case 512:
                    return "Buty";
                case 48:
                    return "Pierścienie";
                case 1024:
                    return "Amulety";
                case 4096:
                    return "Bonusy";
                case 8192:
                    return "Błogosławieństwa";
                case 16384:
                    return "Najemnik";
                case 32768:
                    return "Składniki kuźnicze";
                case 65536:
                    return "Dodatki";
                default:
                    return "Wszystko";
            }
        }
        string Quality_Pack(string choose)
        {
            if (choose == "null") { return "Normalny"; }

            int example = Convert.ToInt32(choose);
            switch (example)
            {
                case 0:
                    return "Ceres (zielony)";
                case 1:
                    return "Neptun (niebieski)";
                case 2:
                    return "Mars (purpurowy)";
                case 3:
                    return "Jupiter (pomarańczowy)";
                case 4:
                    return "Olimp (czerwony)";
                default:
                    return "Normalny";
            }
        }
        void HealMe()
        {
                if (!Properties.Settings.Default.heal_me) { return; }
                while (Health_Level() < health_level)
                {
                    if (!food_exist_backpack && !food_exist_packages) { return; }

                    _BasicTasks.Click("//a[@title='Podgląd']");
                    _BasicTasks.Click("//div[@class='charmercpic doll1']");
                    if (!FoodBackpack()) { return; }

                    IReadOnlyCollection<IWebElement> food_elements = driver.FindElementsByXPath("//div[@id='inv']/div[@data-content-type='64']");
                    if (food_elements.Count == 0)
                    {
                        need_food = true;
                        MovingFood();
                        food_exist_backpack = false;
                        return;
                    }
                    else { food_exist_backpack = true; }
                    string[] data_tooltips = new string[food_elements.Count];
                    int[] health_food = new int[food_elements.Count];

                    for (int i = 0; i < food_elements.Count; i++)
                    {
                        data_tooltips[i] = food_elements.ElementAt(i).GetAttribute("data-tooltip");
                    }

                    int current_health = Convert.ToInt32(driver.FindElementByXPath("//div[@id='header_values_hp_bar']").GetAttribute("data-value"));
                    int current_health_max = Convert.ToInt32(driver.FindElementByXPath("//div[@id='header_values_hp_bar']").GetAttribute("data-max-value"));
                    int current_best_health = 0;
                    int best_choice = 0;
                    bool first_time = true;

                    for (int i = 0; i < food_elements.Count; i++)
                    {
                        string[] separated = data_tooltips.ElementAt(i).Split(' ');
                        for (int j = 0; j < separated.Length; j++)
                        {
                            if (separated[j] == "Leczy")
                            {
                                health_food[i] = Convert.ToInt32(separated[j + 1]);
                                break;
                            }
                        }

                    }

                    for (int i = 0; i < food_elements.Count; i++)
                    {
                        if (first_time)
                        {
                            current_best_health = current_health + health_food[i];
                            if (current_best_health < current_health_max)
                            {
                                best_choice = i;
                                break;
                            }
                            else
                            {
                                current_best_health = current_best_health - current_health_max;
                                first_time = false;
                                continue;
                            }
                        }

                        int temporary = health_food[i] + current_health;
                        int temporary_difference = temporary - current_health_max;
                        if (temporary < current_health_max)
                        {
                            best_choice = i;
                            break;
                        }
                        else if (temporary_difference < current_best_health)
                        {
                            best_choice = i;
                        }
                    }

                    IWebElement best_element = food_elements.ElementAt(best_choice);
                    Actions move = new Actions(driver);
                    move.ClickAndHold(best_element);
                    move.Release(_BasicTasks.GetElement("//div[@id='avatar']/div[@class='ui-droppable']"));
                    move.Build().Perform();
                    _BasicTasks.Click("//a[@title='Podgląd']");
                }
        }
        static List<string> FindReadyObjects(IReadOnlyCollection<IWebElement> list, int categoryNumber)
        {
            string[] filtr = new string[list.Count];
            List<string> collectionReady = new List<string>();
            for (int i = 0; i < list.Count; i++)
            {
                string helperString = Convert.ToString(list.ElementAt(i).GetAttribute("data-quality"));
                filtr[i] = Convert.ToString(list.ElementAt(i).GetAttribute("data-hash"));

                if(helperString == "2" && Properties.Settings.Default.purple_selling ||
                    helperString == "3" && Properties.Settings.Default.orange_selling ||
                    helperString == "4" && Properties.Settings.Default.red_selling ||
                    categoryNumber == 11 || categoryNumber == 12)
                {
                    collectionReady.Add(filtr[i]);
                }
                else if (helperString != "2" && helperString != "3" && helperString != "4")
                {
                    collectionReady.Add(filtr[i]);
                }
            }
            return collectionReady;
        }
        static List<string> FindReadyObjectsClass(IReadOnlyCollection<IWebElement> list, int categoryNumber)
        {
            string[] filtr = new string[list.Count];
            List<string> collectionReadyClass = new List<string>();

            for (int i = 0; i < list.Count; i++)
            {
                string temporary_string = "";
                string helperString = Convert.ToString(list.ElementAt(i).GetAttribute("data-quality"));
                filtr[i] = Convert.ToString(list.ElementAt(i).GetAttribute("data-hash"));

                if (helperString == "2" && Properties.Settings.Default.purple_selling ||
                    helperString == "3" && Properties.Settings.Default.orange_selling ||
                    helperString == "4" && Properties.Settings.Default.red_selling ||
                    categoryNumber == 11 || categoryNumber == 12)
                {
                    temporary_string = Convert.ToString(list.ElementAt(i).GetAttribute("class"));
                    string[] temporary_string_array = temporary_string.Split(' ');
                    temporary_string = temporary_string_array[0];
                    collectionReadyClass.Add(temporary_string);
                }
                else if(helperString != "2" && helperString != "3" && helperString != "4")
                {
                    temporary_string = Convert.ToString(list.ElementAt(i).GetAttribute("class"));
                    string[] temporary_string_array = temporary_string.Split(' ');
                    temporary_string = temporary_string_array[0];
                    collectionReadyClass.Add(temporary_string);
                }
            }
            return collectionReadyClass;
        }
        void BackpackSwitch()
        {
            switch (Properties.Settings.Default.foodBackpack)
            {
                default:
                case 0:
                    food_backpack_string = "//a[@data-bag-number='512']";
                    break;
                case 1:
                    food_backpack_string = "//a[@data-bag-number='513']";
                    break;
                case 2:
                    food_backpack_string = "//a[@data-bag-number='514']";
                    break;
                case 3:
                    food_backpack_string = "//a[@data-bag-number='515']";
                    break;
                case 4:
                    food_backpack_string = "//a[@data-bag-number='516']";
                    break;
                case 5:
                    food_backpack_string = "//a[@data-bag-number='517']";
                    break;
                case 6:
                    food_backpack_string = "//a[@data-bag-number='518']";
                    break;
                case 7:
                    food_backpack_string = "//a[@data-bag-number='519']";
                    break;
            }
            switch (Properties.Settings.Default.extractBackpack)
            {
                default:
                case 0:
                    extract_backpack_string = "//a[@data-bag-number='512']";
                    break;
                case 1:
                    extract_backpack_string = "//a[@data-bag-number='513']";
                    break;
                case 2:
                    extract_backpack_string = "//a[@data-bag-number='514']";
                    break;
                case 3:
                    extract_backpack_string = "//a[@data-bag-number='515']";
                    break;
                case 4:
                    extract_backpack_string = "//a[@data-bag-number='516']";
                    break;
                case 5:
                    extract_backpack_string = "//a[@data-bag-number='517']";
                    break;
                case 6:
                    extract_backpack_string = "//a[@data-bag-number='518']";
                    break;
                case 7:
                    extract_backpack_string = "//a[@data-bag-number='519']";
                    break;
            }
            switch (Properties.Settings.Default.itemsBackpack)
            {
                default:
                case 0:
                    items_backpack_string = "//a[@data-bag-number='512']";
                    break;
                case 1:
                    items_backpack_string = "//a[@data-bag-number='513']";
                    break;
                case 2:
                    items_backpack_string = "//a[@data-bag-number='514']";
                    break;
                case 3:
                    items_backpack_string = "//a[@data-bag-number='515']";
                    break;
                case 4:
                    items_backpack_string = "//a[@data-bag-number='516']";
                    break;
                case 5:
                    items_backpack_string = "//a[@data-bag-number='517']";
                    break;
                case 6:
                    items_backpack_string = "//a[@data-bag-number='518']";
                    break;
                case 7:
                    items_backpack_string = "//a[@data-bag-number='519']";
                    break;
            }
        }
        bool FreeBackpack()
        {
            if (!_BasicTasks.Search(items_backpack_string + "[@data-available='false']"))
            { _BasicTasks.Click(items_backpack_string); return true; }
            else { return false; }
        }
        bool ExtractBackpack()
        {
            if (!_BasicTasks.Search(extract_backpack_string + "[@data-available='false']"))
            { _BasicTasks.Click(extract_backpack_string); return true; }
            else { return false; }
        }
        bool FoodBackpack()
        {
            if (!_BasicTasks.Search(food_backpack_string + "[@data-available='false']"))
            { _BasicTasks.Click(food_backpack_string); return true; }
            else { return false; }
        }
        public void Packages()
        {
            _BasicTasks.Click("//a[@id='menue_packages']");
        }
        void Arena()
        {
            bool non_stop = true;
            while (non_stop)
            {
                _BasicTasks.Click("//div[@id='cooldown_bar_arena']/a");
                IWebElement element = driver.FindElementByXPath("//*[@id='mainnav']/li/table/tbody/tr/td[1]/a");
                string element_name = element.GetAttribute("textContent");
                if (element_name == "Arena") { _BasicTasks.Click("//*[@id='mainnav']/li/table/tbody/tr/td[1]/a"); return; }
            }
        }
        void AuctionHouse()
        {
            Arena();
            _BasicTasks.Click("//a[@class='menuitem '][text() = 'Dom aukcyjny']");
        }
        void Weapon_Seller()
        {
            _BasicTasks.Click("//a[contains(@class,'menuitem')][text() = 'Broń']");
        }
        void Breastplates_Seller()
        {
            _BasicTasks.Click("//a[contains(@class,'menuitem')][text() = 'Pancerz']");
        }
        void Shopkeeper_Seller()
        {
            _BasicTasks.Click("//a[contains(@class,'menuitem')][text() = 'Handlarz']");
        }
        void Alchemic_Seller()
        {
            _BasicTasks.Click("//a[contains(@class,'menuitem')][text() = 'Alchemik']");
        }
        void Soldier_Seller()
        {
            _BasicTasks.Click("//a[contains(@class,'menuitem')][text() = 'Żołnierz']");
        }
        void Malefica_Seller()
        {
            _BasicTasks.Click("//a[contains(@class,'menuitem')][text() = 'Malefica']");
        }
        void Second_Tab_Sellers()
        {
            int iterator = 0;
            while(!_BasicTasks.Search("//div[@class='shopTab'][text() = 'Ⅱ']"))
            {
                Thread.Sleep(1000);
                if (iterator == 5) { return; }
                iterator++;
            }
            _BasicTasks.Click("//div[@class='shopTab'][text() = 'Ⅱ']");
        }
        void Third_Tab_Sellers()
        {
            int iterator = 0;
            while(!_BasicTasks.Search("//div[@class='shopTab dynamic']"))
            {
                Thread.Sleep(1000);
                if(iterator == 5) { return; }
                iterator++;
            }
            _BasicTasks.Click("//div[@class='shopTab dynamic']");
        }
        void Guild_Market()
        {
            while (!_BasicTasks.Search("//a[contains(@href,'guildMarket')][@class='map_label']"))
            { _BasicTasks.Click("//a[text() = 'Gildia']"); }
            _BasicTasks.Click("//a[contains(@href,'guildMarket')][@class='map_label']");
            Guild_Market_Show();
        }
        void Guild_Market_Show()
        {
            while (!_BasicTasks.Search("//a[contains(text(),'Cena na rynku')]"))
            {
                _BasicTasks.WaitForXPath("//section[@id='market_table']");
                string helper = driver.FindElementByXPath("//section[@id='market_table']").GetAttribute("style");
                if (helper == "display: none;") { _BasicTasks.Click("//h2[contains(text(),'Przedmiot')]"); }
            }
        }
        int Health_Level()
        {
            string currentHealth = _BasicTasks.GetElement("//div[@id='header_values_hp_percent']").GetAttribute("textContent");
            currentHealth = currentHealth.Substring(0, currentHealth.Length - 1);
            int currentHealthInt = Convert.ToInt32(currentHealth);
            return currentHealthInt;
        }
        #endregion
    }
}
