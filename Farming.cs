using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using System.Threading;

namespace Gladiatus_35
{
    public static class Farming
    {
        public static int Dungeon(bool error_packing)
        {
            Form1.currently_running = "Dungeon..";
            int dungeon_points = BasicTasks.ReturnInt("//span[@id='dungeonpoints_value_point']");
            if (dungeon_points > 0 && Properties.Settings.Default.dungeonsChecked || error_packing && !Form1.british_land)
            {
                if (BasicTasks.Search("//div[@id='cooldown_bar_dungeon']/a[@class='cooldown_bar_link']"))
                {
                    IWebElement element = Form1.driver.FindElementByXPath("//div[@id='cooldown_bar_dungeon']/a[@class='cooldown_bar_link']");
                    if (!element.Displayed)
                    {
                        Form1.british_land = true;
                        return dungeon_points;
                    }
                }

                #region WaitForAvalibe
                BasicTasks.WaitForXPath("//div[@id='cooldown_bar_text_dungeon'][text() = 'Do lochów']");
                BasicTasks.Click("//div[@id='cooldown_bar_dungeon']/a[@class='cooldown_bar_link']");
                #endregion
                #region CheckNew
                if (BasicTasks.Search("//input[@value='normalne']") || BasicTasks.Search("//input[@value='zaawansowane']"))
                {
                    switch (Properties.Settings.Default.dungeonsOption)
                    {
                        case 0:
                            BasicTasks.Click("//input[@value='normalne']");
                            break;
                        case 1:
                            if (!BasicTasks.Search("//input[@value='zaawansowane'][@disabled='disabled']"))
                                BasicTasks.Click("//input[@value='zaawansowane']");
                            else
                                BasicTasks.Click("//input[@value='normalne']");
                            break;
                    }
                }
                #endregion
                #region Attack
                BasicTasks.WaitForXPath("//img[contains(@src,'combatloc.gif')]");
                BasicTasks.Click("//img[contains(@src,'combatloc.gif')]");
                #endregion
                error_packing = false;
                Thread.Sleep(2000);
                return dungeon_points;
            }
            Thread.Sleep(2000);
            return dungeon_points - 1;
        }
        public static int Expedition(bool error_packing)
        {
            Form1.currently_running = "Expedition..";
            int expedition_points = BasicTasks.ReturnInt("//span[@id='expeditionpoints_value_point']");
            if (expedition_points > 0 && Properties.Settings.Default.expeditionsChecked || error_packing)
            {
                if (BasicTasks.Search("//a[contains(@class,'menuitem')][text() = 'Opuść Hades']"))
                {
                    Form1.hades = true;
                    return expedition_points;
                }
                #region CheckForHealth
                General.HealMe(Properties.Settings.Default.healthLevel);
                #endregion
                #region WaitForAvalibe
                BasicTasks.WaitForXPath("//div[@id='cooldown_bar_expedition']/div[@class='cooldown_bar_text']");
                BasicTasks.Click("//div[@id='cooldown_bar_expedition']/a[@class='cooldown_bar_link']");
                #endregion
                #region ChooseEnemy
                IReadOnlyCollection<IWebElement> list_buttons = Form1.driver.FindElementsByXPath("//button[contains(@class,'expedition_button')]");
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
                }
                Thread.Sleep(2000);
                return expedition_points - 1;
                #endregion
            }
            Thread.Sleep(2000);
            return expedition_points;
        }
        public static bool DungeonEvent()
        {
            Form1.currently_running = "Event war..";
            if (!Properties.Settings.Default.eventWar)
                return false;

            bool first, second = false;
            first = BasicTasks.Search("//a[contains(@class,'menuitem glow eyecatcher')]");
            if (!first) { second = BasicTasks.Search("//a[contains(@class,'menuitem active glow eyecatcher')]"); }

            if (!first && !second)
                return false;

            int eventPoints = 0;
            BasicTasks.Click("//div[@id='cooldown_bar_expedition']/a[@class='cooldown_bar_link']");
            if (first)
            { BasicTasks.Click("//div[@id='submenu2']/a[contains(@class,'menuitem glow eyecatcher')]"); }
            else
            { BasicTasks.Click("//a[contains(@class,'menuitem active glow eyecatcher')]"); }

            if (!BasicTasks.Search("//div[@class='section-header']/p[2]"))
                return false;
            string element = Form1.driver.FindElementByXPath("//div[@class='section-header']/p[2]").GetAttribute("textContent");
            string[] separated = element.Split(' ');

            for (int i = 0; i <= separated.Length; i++)
            {
                try { eventPoints = Convert.ToInt32(separated[i]); break; }
                catch { }
            }

            if (eventPoints == 0)
                return false;

            if (eventPoints > 0 && BasicTasks.Search("//button[@class='expedition_button awesome-button ']"))
                BasicTasks.Click("//button[@class='expedition_button awesome-button ']");
            return true;
        }
        public static void Arena35()
        {
            Form1.currently_running = "Arena between servers..";
            if (Properties.Settings.Default.arena35Checked)
            {
                General.HealMe(Properties.Settings.Default.healthLevel);
                BasicTasks.WaitForXPath("//div[@id='cooldown_bar_arena']/div[@class='cooldown_bar_text']");
                BasicTasks.Click("//div[@id='cooldown_bar_arena']");
                if (!BasicTasks.Search("//a[contains(@href,'serverArena&aType=2')][@class='awesome-tabs current']"))
                    BasicTasks.Click("//a[contains(@href,'serverArena&aType=2')]");
                BasicTasks.Click("//div[@class='attack']");
                if (BasicTasks.Search("//div[@id='header_bod']"))
                {
                    if (Form1.driver.FindElementByXPath("//div[@id='header_bod']").Displayed &&
                        Form1.driver.FindElementByXPath("//div[@id='header_bod']").Enabled)
                        BasicTasks.Click("//input[@value='Jazda!']");
                }
                while (!BasicTasks.Search("//body[@id='reportsPage']"))
                {
                    if (BasicTasks.Search("//div[@id='errorText']"))
                        return;
                    Thread.Sleep(500);
                }
            }
        }
        public static void Turma35()
        {
            Form1.currently_running = "Turma between servers..";
            if (Properties.Settings.Default.circusTurma35Checked)
            {
                BasicTasks.WaitForXPath("//div[@id='cooldown_bar_ct']/div[@class='cooldown_bar_text']");
                BasicTasks.Click("//div[@id='cooldown_bar_ct']");
                if (!BasicTasks.Search("//a[contains(@href,'serverArena&aType=3')][@class='awesome-tabs current']"))
                    BasicTasks.Click("//a[contains(@href,'serverArena&aType=3')]");
                BasicTasks.Click("//div[@class='attack']");
                if (BasicTasks.Search("//div[@id='header_bod']"))
                {
                    if (Form1.driver.FindElementByXPath("//div[@id='header_bod']").Displayed &&
                        Form1.driver.FindElementByXPath("//div[@id='header_bod']").Enabled)
                        BasicTasks.Click("//input[@value='Jazda!']");
                }
                for (int i = 0; i < 5; i++)
                {
                    if (BasicTasks.Search("//div[@id='errorText']"))
                        return;
                    if (!BasicTasks.Search("//body[@id='reportsPage']"))
                        Thread.Sleep(1000);
                    else
                        return;
                    Thread.Sleep(500);
                }
            }
        }
    }
}
