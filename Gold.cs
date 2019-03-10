using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using OpenQA.Selenium;
using System.Threading;
using System.Text.RegularExpressions;

namespace Gladiatus_35
{
    static class Gold
    {
        public static void Pack_Gold()
        {
            string file_path =
                @"C:\Users\danie\Documents\Visual Studio 2017\Resources\Gladiatus_bots\Items .txt files for Gladiatus_bot" + @"\items(" + Helpers.Switch_World() + ").txt";
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
                string[] already_sold = new string[lines.Length];

                string regex = "\'(.*?)\'";
                int iterator = 0;
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] separated_line = lines[i].Split(' ');
                    class_items[iterator] = Regex.Match(separated_line[0],regex).Value;
                    soulbound_items[iterator] = Regex.Match(separated_line[1], regex).Value;
                    price_items[iterator] = Regex.Match(separated_line[2], regex).Value;
                    types[iterator] = Regex.Match(separated_line[3], regex).Value;
                    quality[iterator] = Regex.Match(separated_line[4], regex).Value;
                    level_items[iterator] = Regex.Match(separated_line[5], regex).Value;
                    amount[iterator] = Regex.Match(separated_line[6], regex).Value;
                    already_sold[iterator] = Regex.Match(separated_line[7], regex).Value;

                    class_items[iterator] = class_items[iterator].Replace("'", "");
                    soulbound_items[iterator] = soulbound_items[iterator].Replace("'", "");
                    price_items[iterator] = price_items[iterator].Replace("'", "");
                    types[iterator] = types[iterator].Replace("'", "");
                    quality[iterator] = quality[iterator].Replace("'", "");
                    level_items[iterator] = level_items[iterator].Replace("'", "");
                    amount[iterator] = amount[iterator].Replace("'", "");
                    already_sold[iterator] = already_sold[iterator].Replace("'", "");
                    iterator++;
                }

                bool changed = true;
                while (changed)
                {
                    changed = false;
                    for (int i = 0; i < lines.Length - 1; i++)
                    {
                        if (Convert.ToInt32(price_items[i]) < Convert.ToInt32(price_items[i + 1]))
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

                Navigation.Guild_Market();
                if (!BasicTasks.Search("//input[@value='Kup']")) { return; }

                int found_case = 0;
                int orginal_case = 0;
                for (int i = 0; i < class_items.Length; i++)
                {
                    if (Gold_Level() - Convert.ToInt32(price_items[i]) > 0)
                    { found_case = i; orginal_case = i; break; }
                }

                loop_label:
                int first_iterator = Form1.driver.FindElementsByXPath("//input[@value='Kup']").Count();
                int second_iterator = Form1.driver.FindElementsByXPath("//input[@value='Anuluj']").Count();
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
                for (int i = 2; i <= iterator + 1; i++)
                {
                    if (BasicTasks.Search("//section[@id='market_table']//tr[position()='" + i + "']/td[@align='center']/input[@value='Kup']"))
                    {
                        IWebElement element = Form1.driver.FindElementByXPath("//section[@id='market_table']//tr[position()='" + i + "']/td[@style]/div[@style]");
                        string soulbound = element.GetAttribute("data-soulbound-to");
                        string name_item = element.GetAttribute("class");
                        string price_item_string = Form1.driver.FindElementByXPath
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
                            BasicTasks.Click("//section[@id='market_table']//tr[position()='" + i + "']/td[@align='center']/input[@value='Kup']");
                            if (gold_before - Gold_Level() == price_item_work) { bought = true; }
                            break;
                        }
                    }
                }

                if (!bought && found_case != class_items.Length - 1) { found_case++; goto loop_label; }
                else if (!bought && BasicTasks.Search("//a[contains(text(),'Następna strona')]") && found_case == class_items.Length - 1)
                { BasicTasks.Click("//a[contains(text(),'Następna strona')]"); found_case = orginal_case; goto loop_label; }
                else if (!bought && !BasicTasks.Search("//a[contains(text(),'Następna strona')]") && found_case == class_items.Length - 1) { return; }

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

                if (by_quality)
                {
                    path += "[@data-quality='" + item_quality + "']";
                    path2 += "[@data-quality='" + item_quality + "']";
                }

                if (by_amount)
                {
                    path += "[@data-amount='" + amount_work + "']";
                    path2 += "[@data-amount='" + amount_work + "']";
                }

                while (true)
                {
                    takeOut:
                    Navigation.Packages();
                    if (BasicTasks.Search("//section[@style='display: none;']"))
                        BasicTasks.Click("//h2[@class='section-header'][contains(text(), 'Opcje')]");
                    BasicTasks.SelectElement("//select[@name='f']", Helpers.Type_Pack(types[found_case]));
                    BasicTasks.SelectElement("//select[@name='fq']", Helpers.Quality_Pack(quality[found_case]));
                    BasicTasks.Click("//input[@value='Filtr']");
                    if (!Navigation.FreeBackpack())
                        return;
                    if (!BasicTasks.Search(path) && BasicTasks.Search(path2))
                        goto sell;
                    else if (!BasicTasks.Search(path) && !BasicTasks.Search(path2))
                    {
                        bool found = false;
                        while (BasicTasks.Search("//a[@class='paging_button paging_right_step']") && !found)
                        {
                            BasicTasks.Click("//a[@class='paging_button paging_right_step']");
                            if (BasicTasks.Search(path))
                                found = true;
                        }
                        if (!found)
                            break;
                    }
                    Navigation.FreeBackpack();
                    BasicTasks.MoveMoveElement(path, "//div[@id='inv']");
                    if (BasicTasks.Search("//div[@class='ui-droppable grid-droparea image-grayed active']"))
                        BasicTasks.ReleaseElement("//div[@class='ui-droppable grid-droparea image-grayed active']");
                    else
                    {
                        BasicTasks.ReleaseElement("//input[@name='show-item-info']");
                        return;
                    }

                    sell:
                    Navigation.Guild_Market();
                    Navigation.FreeBackpack();

                    while (BasicTasks.Search("//div[@id='market_sell_box']//section[@style='display: none;']"))
                        BasicTasks.Click("//h2[@class='section-header'][text() = 'sprzedaj']");

                    if (BasicTasks.Search(path2))
                        BasicTasks.MoveReleaseElement(path2, "//div[@id='market_sell']/div[@class='ui-droppable']");
                    else
                        goto takeOut;
                    BasicTasks.SelectElement("//select[@name='dauer']", "24 h");
                    var cena = BasicTasks.GetElement("//input[@name='preis']");
                    cena.SendKeys(OpenQA.Selenium.Keys.Control + "a");
                    cena.SendKeys(OpenQA.Selenium.Keys.Delete);
                    cena.SendKeys(Convert.ToString(price_item_work));
                    BasicTasks.Click("//input[@value='Oferta']");
                    Thread.Sleep(2000);

                    if (BasicTasks.Search("//div[@class='message fail']"))
                    {
                        Farming.Expedition(true);
                        goto sell;
                    }
                    else
                        return;
                }
            }
        }
        public static void Search_Pack()
        {
            string file_path =
                @"C:\Users\danie\Documents\Visual Studio 2017\Resources\Gladiatus_bots\Items .txt files for Gladiatus_bot" + @"\items(" + Helpers.Switch_World() + ").txt";
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
            string regex = "\'(.*?)\'";
            for (int i = 0; i < lines.Length; i++)
            {
                string[] separated_line = lines[i].Split(' ');
                class_items[iterator] = Regex.Match(separated_line[0], regex).Value;
                soulbound_items[iterator] = Regex.Match(separated_line[1], regex).Value;
                price_items[iterator] = Regex.Match(separated_line[2], regex).Value;
                types[iterator] = Regex.Match(separated_line[3], regex).Value;
                qualities_items[iterator] = Regex.Match(separated_line[4], regex).Value;
                level_items[iterator] = Regex.Match(separated_line[5], regex).Value;
                amount_work[iterator] = Regex.Match(separated_line[6], regex).Value;
                already_sold[iterator] = Regex.Match(separated_line[7], regex).Value;

                class_items[iterator] = class_items[iterator].Replace("'", "");
                soulbound_items[iterator] = soulbound_items[iterator].Replace("'", "");
                price_items[iterator] = price_items[iterator].Replace("'", "");
                types[iterator] = types[iterator].Replace("'", "");
                qualities_items[iterator] = qualities_items[iterator].Replace("'", "");
                level_items[iterator] = level_items[iterator].Replace("'", "");
                amount_work[iterator] = amount_work[iterator].Replace("'", "");
                already_sold[iterator] = already_sold[iterator].Replace("'", "");
                iterator++;
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
                Navigation.Packages();
                if (BasicTasks.Search("//section[@style='display: none;']"))
                { BasicTasks.Click("//h2[@class='section-header'][contains(text(), 'Opcje')]"); }
                if (types_good) { BasicTasks.SelectElement("//select[@name='f']", Helpers.Type_Pack(type)); }
                if (quality_good) { BasicTasks.SelectElement("//select[@name='fq']", Helpers.Quality_Pack(quality)); }
                if (types_good || quality_good) { BasicTasks.Click("//input[@value='Filtr']"); }
                items = Form1.driver.FindElementsByXPath("//div[@id='packages']//div[contains(@class,'ui-draggable')]");

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
                            if (amount_work[j] != "null") { by_amount = true; } else { fifth = true; }

                            var regex2 = new Regex(@"\s*\b" + class_items[j] + @"\s*\b");
                            if (names && regex2.IsMatch(name_class)) { first = true; }
                            if (levels && level_items[j] == level) { second = true; }
                            if (soulbounds && soulbound_items[j] == soul_bound) { third = true; }
                            if (qualities && qualities_items[j] == quality) { fourth = true; }
                            if (by_amount && amount_work[j] == amount_temp) { fifth = true; }

                            if (first && second && third && fourth && fifth) { found = true; found_case = j; break; }
                        }
                        if (found) { break; }
                    }
                    if (found) { break; }
                    if (BasicTasks.Search("//a[@class='paging_button paging_right_step']"))
                    { BasicTasks.Click("//a[@class='paging_button paging_right_step']"); }
                    items = Form1.driver.FindElementsByXPath("//div[@id='packages']//div[contains(@class,'ui-draggable')]");
                } while (BasicTasks.Search("//a[@class='paging_button paging_right_step']"));
                if (!found)
                    continue;
                if (!Navigation.FreeBackpack())
                    return;
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

                BasicTasks.MoveMoveElement(path1, "//input[@name='show-item-info']");
                if (BasicTasks.Search("//div[@class='ui-droppable grid-droparea image-grayed active']"))
                {
                    BasicTasks.ReleaseElement("//div[@class='ui-droppable grid-droparea image-grayed active']");
                    if (!BasicTasks.Search(path2)) { return; }
                }
                else { BasicTasks.ReleaseElement("//input[@name='show-item-info']"); return; }

                sell:
                Navigation.Guild_Market();
                Navigation.FreeBackpack();

                while (BasicTasks.Search("//div[@id='market_sell_box']//section[@style='display: none;']"))
                { BasicTasks.Click("//h2[@class='section-header'][text() = 'sprzedaj']"); }

                BasicTasks.MoveReleaseElement(path2, "//div[@id='market_sell']/div[@class='ui-droppable']");
                BasicTasks.SelectElement("//select[@name='dauer']", "24 h");
                var cena = BasicTasks.GetElement("//input[@name='preis']");
                cena.SendKeys(OpenQA.Selenium.Keys.Control + "a");
                cena.SendKeys(OpenQA.Selenium.Keys.Delete);
                cena.SendKeys(Convert.ToString(price_items[found_case]));
                BasicTasks.Click("//input[@value='Oferta']");

                if (BasicTasks.Search("//div[@class='message fail']"))
                {
                    Farming.Expedition(true);
                    goto sell;
                }
            } while (found);

            found = false;
            Navigation.FreeBackpack();
            items = Form1.driver.FindElementsByXPath("//div[@id='inv']//div[contains(@class,'ui-draggable')]");

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

                    var regex2 = new Regex(@"\s*\b" + class_items[j] + @"\s*\b");
                    if (names && regex2.IsMatch(name_class)) { first = true; }
                    if (levels && level_items[j] == level) { second = true; }
                    if (soulbounds && soulbound_items[j] == soul_bound) { third = true; }
                    if (qualities && qualities_items[j] == quality) { fourth = true; }
                    if (by_amount && amount_work[j] == amount_temp) { fifth = true; }

                    if (first && second && third && fourth && fifth)
                    {
                        found = true; found_case = j; break;
                    }
                }
                if (found) { break; }
            }

            while (found)
            {
                sell_inv:
                found = false;

                Navigation.Guild_Market();
                Navigation.FreeBackpack();
                while (BasicTasks.Search("//div[@id='market_sell_box']//section[@style='display: none;']"))
                { BasicTasks.Click("//h2[@class='section-header'][text() = 'sprzedaj']"); }

                items = Form1.driver.FindElementsByXPath("//div[@id='inv']//div[contains(@class,'ui-draggable')]");
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

                        var regex2 = new Regex(@"\s*\b" + class_items[j] + @"\s*\b");
                        if (names && regex2.IsMatch(name_class)) { first = true; }
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

                BasicTasks.MoveTo(path2);
                if (BasicTasks.Search("//p[contains(text(),'Wskazówka')]") != already_sold_needed)
                {
                    Navigation.Malefica_Seller();
                    Navigation.Third_Tab_Sellers();

                    BasicTasks.MoveMoveElement(path2, "//a[@class='awesome-tabs current']");
                    if (BasicTasks.Search("//div[@id='shop']//div[@class='ui-droppable grid-droparea image-grayed active']"))
                        BasicTasks.ReleaseElement("//div[@id='shop']//div[@class='ui-droppable grid-droparea image-grayed active']");
                    else
                        return;

                    Search_Pack();
                    return;
                }

                BasicTasks.MoveReleaseElement(path2, "//div[@id='market_sell']/div[@class='ui-droppable']");
                BasicTasks.SelectElement("//select[@name='dauer']", "24 h");
                var cena_2 = BasicTasks.GetElement("//input[@name='preis']");
                cena_2.SendKeys(OpenQA.Selenium.Keys.Control + "a");
                cena_2.SendKeys(OpenQA.Selenium.Keys.Delete);
                cena_2.SendKeys(Convert.ToString(price_items[found_case]));
                BasicTasks.Click("//input[@value='Oferta']");
                Thread.Sleep(2000);

                if (BasicTasks.Search("//div[@class='message fail']"))
                {
                    Farming.Expedition(true);
                    goto sell_inv;
                }
            }
        }
        public static int Gold_Level()
        {
            string goldBeforeString = BasicTasks.GetElement("//div[@id='sstat_gold_val']").GetAttribute("textContent");
            goldBeforeString = goldBeforeString.Replace(".", "");
            int gold = Convert.ToInt32(goldBeforeString);
            return gold;
        }
        public static int Get_Maximum_Gold_Avalibe()
        {
            if (!Properties.Settings.Default.pakujChecked)
                return 100000000;

            string file_path =
            @"C:\Users\danie\Documents\Visual Studio 2017\Resources\Gladiatus_bots\Items .txt files for Gladiatus_bot" + @"\items" + Helpers.Switch_World() + ".txt";
            if (!File.Exists(file_path)) { return 0; }
            int lineCount = File.ReadLines(file_path).Count();
            if (lineCount == 0) { return 0; }

            string[] lines = File.ReadAllLines(file_path);
            string[] class_items = new string[lines.Length];
            string[] soulbound_items = new string[lines.Length];
            string[] price_items = new string[lines.Length];
            string[] level_items = new string[lines.Length];
            string[] types = new string[lines.Length];
            string[] quality = new string[lines.Length];
            string[] amount = new string[lines.Length];
            string[] already_sold = new string[lines.Length];

            int iterator = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                string[] separated_line = lines[i].Split(' ');
                class_items[iterator] = separated_line[0];
                soulbound_items[iterator] = separated_line[1];
                price_items[iterator] = separated_line[2];
                types[iterator] = separated_line[3];
                quality[iterator] = separated_line[4];
                level_items[iterator] = separated_line[5];
                amount[iterator] = separated_line[6];
                already_sold[iterator] = separated_line[7];

                class_items[iterator] = class_items[iterator].Replace("'", "");
                soulbound_items[iterator] = soulbound_items[iterator].Replace("'", "");
                price_items[iterator] = price_items[iterator].Replace("'", "");
                types[iterator] = types[iterator].Replace("'", "");
                quality[iterator] = quality[iterator].Replace("'", "");
                level_items[iterator] = level_items[iterator].Replace("'", "");
                amount[iterator] = amount[iterator].Replace("'", "");
                already_sold[iterator] = already_sold[iterator].Replace("'", "");
                iterator++;
            }

            int total_price = 0;
            Navigation.Guild_Market();
            if (!BasicTasks.Search("//input[@value='Kup']")) { return 0; }
            int first_iterator = Form1.driver.FindElementsByXPath("//input[@value='Kup']").Count();
            int second_iterator = Form1.driver.FindElementsByXPath("//input[@value='Anuluj']").Count();
            iterator = first_iterator + second_iterator;

            for (int j = 0; j < lines.Length; j++)
            {
                bool by_name = false;
                bool by_amount = false;
                bool by_soulbound = false;
                bool by_level = false;
                bool by_quality = false;
                int price_item_work = Convert.ToInt32(price_items[j]);
                string name_item_work = class_items[j];
                string soulbound_work = soulbound_items[j];
                string level_work = level_items[j];
                string item_quality = quality[j];
                string amount_work = amount[j];
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
                for (int i = 2; i <= iterator + 1; i++)
                {
                    if (BasicTasks.Search("//section[@id='market_table']//tr[position()='" + i + "']/td[@align='center']/input[@value='Kup']"))
                    {
                        IWebElement element = Form1.driver.FindElementByXPath("//section[@id='market_table']//tr[position()='" + i + "']/td[@style]/div[@style]");
                        string soulbound = element.GetAttribute("data-soulbound-to");
                        string name_item = element.GetAttribute("class");
                        string price_item_string = Form1.driver.FindElementByXPath
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

                            total_price = total_price + price_item_work;
                            break;
                        }
                    }
                }
            }
            return total_price;
        }
        public static void TakeGold()
        {
            Form1.currently_running = "Taking out gold..";
            Navigation.Packages();
            BasicTasks.SelectElement("//select[@name='f']", "Złoto");
            BasicTasks.Click("//input[@value='Filtr']");
            if (BasicTasks.Search("//a[@class='paging_button paging_right_full']")) { BasicTasks.Click("//a[@class='paging_button paging_right_full']"); }
            if (!Properties.Settings.Default.gold_limit)
            {
                while (BasicTasks.Search("//div[@class='packageItem']//div[contains(@class,'ui-draggable')]") && Form1.takeGoldAction)
                {
                    BasicTasks.MoveMoveElement("//div[@class='packageItem']//div[contains(@class,'ui-draggable')]", "//div[@id='inv']");
                    if (BasicTasks.Search("//div[contains(@class,'ui-droppable')]")) { BasicTasks.ReleaseElement("//div[contains(@class,'ui-droppable')]"); }
                    else { return; }
                }
            }
            else
            {
                int needed_gold = Convert.ToInt32(Properties.Settings.Default.gold_level);
                while (Gold_Level() < needed_gold)
                {
                    int gold_before = Gold_Level();
                    IWebElement element = Form1.driver.FindElementByXPath("//div[@class='packageItem']//div[contains(@class,'ui-draggable')]");
                    string current_element_string = element.GetAttribute("data-price-gold");
                    int current_element = Convert.ToInt32(current_element_string);
                    if (Gold_Level() + current_element < needed_gold)
                    {
                        BasicTasks.MoveMoveElement("//div[@class='packageItem']//div[@data-price-gold='" + current_element_string + "']", "//div[@id='inv']");
                        if (BasicTasks.Search("//div[contains(@class,'ui-droppable')]"))
                        { BasicTasks.ReleaseElement("//div[contains(@class,'ui-droppable')]"); }
                        else { continue; }
                    }
                    else
                    {
                        IReadOnlyCollection<IWebElement> list = Form1.driver.FindElementsByXPath("//div[@class='packageItem']//div[contains(@class,'ui-draggable')]");
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
                        BasicTasks.MoveMoveElement("//div[@class='packageItem']//div[@data-price-gold='" + best_choose_string + "']", "//div[@id='inv']");
                        if (BasicTasks.Search("//div[contains(@class,'ui-droppable')]"))
                        { BasicTasks.ReleaseElement("//div[contains(@class,'ui-droppable')]"); }
                        else { continue; }
                    }
                    bool changed = false;
                    for (int i = 0; i < 5; i++)
                    {
                        if (Gold_Level() != gold_before)
                        {
                            Thread.Sleep(1000);
                            changed = true;
                        }
                    }
                    if (!changed)
                        Form1.driver.Navigate().Refresh();
                }
            }
        }
    }
}
