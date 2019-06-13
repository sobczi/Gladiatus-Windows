using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using System.Text.RegularExpressions;

namespace Gladiatus_35
{
    static class Hades
    {
        public static bool DoHades()
        {
            if (BasicTasks.Search("//div[contains(@onmousemove,'Czas regeneracji')]") || !Properties.Settings.Default.hades)
                return false;

            int maximum_rubles = Properties.Settings.Default.maximum_rubles_hades;
            bool first_attack = true;
            string[] items = new string[4];
            BasicTasks.Click("//div[@id='cooldown_bar_expedition']/a[@class='cooldown_bar_link']");
            bool first = false;
            bool second = false;
            if (BasicTasks.Search("//a[contains(@title,'Czas podróży')]"))
                first = true;
            else if (BasicTasks.Search("//a[text() = 'Pustelnik']"))
                second = true;

            if (first || second)
            {
                //enter hades
                if (!first)
                {
                    General.HealMe(100);
                    BasicTasks.Click("//div[@id='cooldown_bar_expedition']/a[@class='cooldown_bar_link']");
                    BasicTasks.Click("//a[contains(text(),'Pustelnik')]");
                    BasicTasks.Click("//a[contains(text(), 'zaświaty')]");
                    BasicTasks.Click("//input[@value='normalne']");
                }

                BasicTasks.WaitForXPath("//div[contains(text(),'Na wyprawę')]");

                //first fight, take off gear
                BasicTasks.Click("//a[@title='Podgląd']");
                Navigation.FreeBackpack();

                //reading values
                items[0] = Hades_Get_Class("3");
                items[1] = Hades_Get_Class("4");
                items[2] = Hades_Get_Class("5");
                items[3] = Hades_Get_Class("10");


                foreach (string item in items)
                {
                    BasicTasks.MoveMoveElement("//div[contains(@class,'" + item + "')]", "//a[@class='awesome-tabs']");
                    BasicTasks.ReleaseElement("//div[@id='inv']//div[@class='ui-droppable grid-droparea image-grayed active']");
                }
            }
            else
                first_attack = false;

            int wasted_rubles = 0;
            bool no_points = false;
            int last_stage = -1;
            int current_stage = 0;
            while (true)
            {
                Gold.Pack_Gold();
                if (!no_points)
                {
                    int points = Convert.ToInt32(BasicTasks.GetElement("//span[@id='expeditionpoints_value_point']").GetAttribute("textContent"));
                    if (points == 0)
                        no_points = true;
                }

                Hades_Heal_Guild();
                current_stage = Hades_Get_Stage();
                if (current_stage != last_stage)
                {
                    Hades_Take_Boosters(current_stage);
                    last_stage = Hades_Get_Stage();
                }

                if (no_points)
                    wasted_rubles++;

                if (maximum_rubles >= wasted_rubles)
                {
                    BasicTasks.WaitForXPath("//div[@id='cooldown_bar_expedition']/div[@class='cooldown_bar_text']");
                    IList<IWebElement> click_buttons = Form1.driver.FindElementsByXPath("//button[contains(@id,'expedition_button')]");
                    foreach (IWebElement button in click_buttons)
                    {
                        if (button.Displayed)
                        {
                            button.Click();
                            break;
                        }
                    }
                }
                else
                    Hades_Exit();
                if (BasicTasks.Search("//div[contains(text(),'Dokonało się!')]"))
                {
                    BasicTasks.Click("//input[@id='linkcancelnotification'][@value='Nie']");
                    return true;
                }

                if (first_attack && !BasicTasks.Search("//div[@class='reportLose']"))
                {
                    Hades_Get_Gear(items);
                    first_attack = false;
                }
            }
        }
        public static bool Take_Pater_Costume()
        {
            if (Convert.ToInt32(Form1.driver.FindElementByXPath("//div[@id='header_values_level']").GetAttribute("textContent")) < 100)
                return false;
            if (BasicTasks.Search("//div[contains(@onmousemove,'Zbroja Disa Patera')]"))
                return false;
            BasicTasks.Click("//a[@title='Podgląd']");
            BasicTasks.Click("//input[@value='zmień']");
            if (BasicTasks.Search("//input[contains(@onclick,'Zbroja Disa Patera')]"))
            {
                BasicTasks.Click("//input[contains(@onclick,'Zbroja Disa Patera')]");
                BasicTasks.Click("//td[@id='buttonleftchangeCostume']/input[@value='Tak']");
                return true;
            }
            return false;
        }
        private static void Hades_Exit()
        {
            Navigation.Packages();
            Navigation.FreeBackpack();
            string exit_item = "//div[@id='inv']//div[contains(concat(' ', normalize-space(@class), ' '), ' " + "item-23-1" + " ')]";
            while (true)
            {
                if (BasicTasks.Search(exit_item))
                {
                    BasicTasks.MoveMoveElement(exit_item, "//a[@class='awesome-tabs']");
                    BasicTasks.ReleaseElement("//div[@class='ui-droppable grid-droparea image-grayed active']");
                    break;
                }
                else
                    BasicTasks.Click("//a[@class='paging_button paging_right_step']");
            }

            exit_item = "//div[@id='inv']//div[contains(concat(' ', normalize-space(@class), ' '), ' " + "item-23-1" + " ')]";
            BasicTasks.Click("//a[@class='cooldown_bar_link']");
            BasicTasks.Click("//a[contains(text(),'Opuść Hades')]");
            BasicTasks.MoveMoveElement(exit_item, "//a[@class='awesome-tabs']");
            BasicTasks.ReleaseElement("//div[@id='underworld_targetbox']/div[@class='ui-droppable active']");
            BasicTasks.Click("//input[@id='linkunderworldLeaveConfirm'][@value='Opuść Hades']");
        }
        private static void Hades_Take_Boosters(int stage)
        {
            string[] names = new string[5];
            bool[] got = new bool[5];
            switch (stage)
            {
                case 3:
                    names[0] = "item-i-11-4";
                    names[1] = "item-i-11-8";
                    names[2] = "item-i-11-12";
                    names[3] = "item-i-11-20";
                    names[4] = "item-i-11-27";
                    break;
                case 2:
                    names[0] = "item-i-11-3";
                    names[1] = "item-i-11-7";
                    names[2] = "item-i-11-11";
                    names[3] = "item-i-11-19";
                    names[4] = "item-i-11-26";
                    break;
                case 1:
                    names[0] = "item-i-11-2";
                    names[1] = "item-i-11-6";
                    names[2] = "item-i-11-10";
                    names[3] = "item-i-11-18";
                    names[4] = "item-i-11-25";
                    break;
                case 0:
                    names[0] = "item-i-11-1";
                    names[1] = "item-i-11-5";
                    names[2] = "item-i-11-9";
                    names[3] = "item-i-11-17";
                    names[4] = "item-i-11-24";
                    break;
            }

            Navigation.Review();
            for (int i = 0; i < 5; i++)
            {
                string temporary = "//div[@id='inv']//div[contains(concat(' ', normalize-space(@class), ' '), ' " + names[i] + " ')]";
                if (BasicTasks.Search(temporary))
                    got[i] = true;
            }

            Navigation.Packages();
            BasicTasks.SelectElement("//select[@name='f']", "Przyspieszacze");
            BasicTasks.Click("//input[@value='Filtr']");
            while (BasicTasks.Search("//a[@class='paging_button paging_right_step']"))
                BasicTasks.Click("//a[@class='paging_button paging_right_step']");

            Navigation.FreeBackpack();
            while (true)
            {
                for (int i = 0; i < 5; i++)
                {
                    string temporary = "//div[@id='packages']//div[contains(concat(' ', normalize-space(@class), ' '), ' " + names[i] + " ')]";
                    if (!got[i] && BasicTasks.Search(temporary))
                    {
                        BasicTasks.MoveMoveElement(temporary, "//a[@class='awesome-tabs']");
                        BasicTasks.ReleaseElement("//div[@class='ui-droppable grid-droparea image-grayed active']");
                        got[i] = true;
                    }
                }

                bool ready = true;
                foreach (bool element in got)
                {
                    if (!element)
                    {
                        ready = false;
                        break;
                    }
                }

                if (ready)
                    break;

                if (BasicTasks.Search("//a[@class='paging_button paging_left_step']"))
                    BasicTasks.Click("//a[@class='paging_button paging_left_step']");
                else
                    break;
            }

            Navigation.Review();
            for (int i = 0; i < 5; i++)
            {
                if (got[i])
                    Hades_Eat_Booster(names[i]);
            }
        }
        private static int Hades_Get_Stage()
        {
            BasicTasks.Click("//div[@id='cooldown_bar_expedition']/a[@class='cooldown_bar_link']");

            string[] locations = new string[4];
            locations[0] = "Erebus";
            locations[1] = "Tartarus";
            locations[2] = "Sąd";
            locations[3] = "Wejście";

            for (int i = 0; i < 4; i++)
            {
                if (BasicTasks.Search("//a[contains(text(),'" + locations[i] + "')]"))
                {
                    BasicTasks.Click("//a[contains(text(),'" + locations[i] + "')]");
                    return i;
                }
            }
            return -1;
        }
        private static string Hades_Get_Class(string container)
        {
            string regex = @"\bitem.*";
            IWebElement temporary = Form1.driver.FindElementByXPath("//div[@data-container-number='" + container + "']/div");
            return Regex.Match(temporary.GetAttribute("class"), regex).Value;
        }
        private static void Hades_Get_Gear(string[] items)
        {
            BasicTasks.Click("//a[@title='Podgląd']");
            Navigation.FreeBackpack();
            int[] numbers = new int[4];
            numbers[0] = 3;
            numbers[1] = 4;
            numbers[2] = 5;
            numbers[3] = 10;

            int iterator = 0;
            foreach (string item in items)
            {
                BasicTasks.MoveMoveElement("//div[contains(@class,'" + item + "')]", "//a[@class='awesome-tabs']");
                BasicTasks.ReleaseElement("//div[@class='ui-droppable active'][@data-container-number='" + Convert.ToString(numbers[iterator]) + "']");
                iterator++;
            }
        }
        private static void Hades_Heal_Guild()
        {
            if (Properties.Settings.Default.healthLevel > General.Health_Level())
            {
                Navigation.Guild_Medic();
                if (BasicTasks.Search("//a[contains(text(),'Lecz teraz!')]"))
                    BasicTasks.Click("//a[contains(text(),'Lecz teraz!')]");
            }
        }
        private static void Hades_Eat_Booster(string name)
        {
            BasicTasks.MoveMoveElement("//div[@id='inv']//div[contains(@class,'" + name + "')]", "//a[@class='awesome-tabs']");
            BasicTasks.ReleaseElement("//div[@id='avatar']/div[@class='ui-droppable active']");
        }
    }
}
