using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using System.Threading;
using System.IO;
using OpenQA.Selenium.Interactions;

namespace Gladiatus_35
{
    static class General
    {
        public static void BuyFood(bool need_food)
        {
            Form1.currently_running = "Buying food..";
            if (Properties.Settings.Default.buyFood || need_food)
            {
                Navigation.Packages();
                if (BasicTasks.Search("//section[@style='display: none;']"))
                    BasicTasks.Click("//h2[@class='section-header'][contains(text(), 'Opcje')]");
                BasicTasks.SelectElement("//select[@name='f']", "Jadalne");
                BasicTasks.Click("//input[@value='Filtr']");

                for (int i = Properties.Settings.Default.foodPack; i < Properties.Settings.Default.foodPack * 10; i++)
                {
                    if (Form1.driver.FindElementsByXPath("//div[@class='paging_numbers']//a[text() = '" + i + "']").Count != 0)
                        return;
                }
                string[] auctionForms;
                Navigation.AuctionHouse();
                BasicTasks.SelectElement("//select[@name='itemType']", "Jadalne");
                BasicTasks.Click("//input[@value='Filtr']");

                IReadOnlyCollection<IWebElement> list = Form1.driver.FindElementsByXPath("//div[@id='auction_table']//form[@method='post']");
                auctionForms = new string[list.Count];
                for (int i = 0; i < list.Count; i++)
                    auctionForms[i] = Convert.ToString(list.ElementAt(i).GetAttribute("id"));

                for (int i = 0; i < list.Count; i++)
                {
                    string helperString = "//div[@id='auction_table']//form[@id='" + auctionForms[i] + "']";
                    string noOffers = Form1.driver.FindElementByXPath(helperString + "//div[@class='auction_bid_div']/div").GetAttribute("textContent");
                    noOffers = noOffers.Trim();

                    if (noOffers == "Brak ofert")
                        BasicTasks.Click(helperString + "//input[@value='Licytuj']");
                    if (Form1.driver.FindElementsByXPath("//div[@class='message fail']").Count != 0)
                        return;
                    list = Form1.driver.FindElementsByXPath("//input[@value='Licytuj']");
                }
            }
            Thread.Sleep(2000);
        }
        public static void Buy_Auction_House()
        {
            Form1.currently_running = "Buying from Auction House..";
            if (Properties.Settings.Default.buyRings || Properties.Settings.Default.buyAmulets || Properties.Settings.Default.buyBoosters || Form1.buyRingsAction)
            {
                if (Properties.Settings.Default.buyBoosters || Form1.buyRingsAction)
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
                    Navigation.Packages();
                    if (BasicTasks.Search("//section[@style='display: none;']")) { BasicTasks.Click("//h2[@class='section-header'][contains(text(), 'Opcje')]"); }
                    BasicTasks.SelectElement("//select[@name='f']", "Przyspieszacze");
                    BasicTasks.Click("//input[@value='Filtr']");
                    bool first_loop = true;
                    do
                    {
                        if (!first_loop)
                            BasicTasks.Click("//a[@class='paging_button paging_right_step']");

                        for (int i = 0; i <= 3; i++)
                        {
                            i_strength[i] += Form1.driver.FindElementsByXPath("//div[@class='" + strength[i] + "']").Count;
                            i_mastery[i] += Form1.driver.FindElementsByXPath("//div[@class='" + mastery[i] + "']").Count;
                            i_skill[i] += Form1.driver.FindElementsByXPath("//div[@class='" + skill[i] + "']").Count;
                            i_physic[i] += Form1.driver.FindElementsByXPath("//div[@class='" + physic[i] + "']").Count;
                            i_charisma[i] += Form1.driver.FindElementsByXPath("//div[@class='" + charisma[i] + "']").Count;
                            i_inteligence[i] += Form1.driver.FindElementsByXPath("//div[@class='" + inteligence[i] + "']").Count;
                            if (i != 3)
                                i_health[i] += Form1.driver.FindElementsByXPath("//div[@class='" + health[i] + "']").Count;
                        }

                        first_loop = false;
                    } while (BasicTasks.Search("//a[@class='paging_button paging_right_step']"));

                    bool need_boosters = false;
                    bool changed = false;
                    int boosters_per_type = Properties.Settings.Default.boostersPack;
                    for (int i = 0; i < 3; i++)
                    {
                        changed = false;
                        if (i_strength[i] >= boosters_per_type) 
                        {
                            i_strength[i] = 0; 
                            changed = true; 
                        }

                        if (!changed) 
                        { 
                            i_strength[i] -= boosters_per_type; 
                            i_strength[i] *= -1; 
                            need_boosters = true; 
                        }

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
                    Navigation.AuctionHouse();
                    BasicTasks.SelectElement("//select[@name='itemType']", "Przyspieszacze");
                    BasicTasks.Click("//input[@value='Filtr']");
                    IReadOnlyCollection<IWebElement> list = Form1.driver.FindElementsByXPath("//div[@id='auction_table']//form[@method='post']");
                    auctionForms = new string[list.Count];
                    for (int i = 0; i < list.Count; i++)
                        auctionForms[i] = Convert.ToString(list.ElementAt(i).GetAttribute("id"));
                    for (int i = 0; i < list.Count; i++)
                    {
                        string helper_string = "//div[@id='auction_table']//form[@id='" + auctionForms[i] + "']";
                        for (int j = 0; j < 3; j++)
                        {
                            string no_offers = Form1.driver.FindElementByXPath(helper_string + "//div[@class='auction_bid_div']/div").GetAttribute("textContent");
                            no_offers = no_offers.Trim();
                            if (BasicTasks.Search(helper_string + "//div[contains(@class,'" + strength[j] + "')]") && i_strength[j] != 0 && no_offers == "Brak ofert")
                            { BasicTasks.Click(helper_string + "//input[@value='Licytuj']"); i_strength[j]--; }

                            if (BasicTasks.Search(helper_string + "//div[contains(@class,'" + mastery[j] + "')]") && i_mastery[j] != 0 && no_offers == "Brak ofert")
                            { BasicTasks.Click(helper_string + "//input[@value='Licytuj']"); i_mastery[j]--; }

                            if (BasicTasks.Search(helper_string + "//div[contains(@class,'" + skill[j] + "')]") && i_skill[j] != 0 && no_offers == "Brak ofert")
                            { BasicTasks.Click(helper_string + "//input[@value='Licytuj']"); i_skill[j]--; }

                            if (BasicTasks.Search(helper_string + "//div[contains(@class,'" + physic[j] + "')]") && i_physic[j] != 0 && no_offers == "Brak ofert")
                            { BasicTasks.Click(helper_string + "//input[@value='Licytuj']"); i_physic[j]--; }

                            if (BasicTasks.Search(helper_string + "//div[contains(@class,'" + charisma[j] + "')]") && i_charisma[j] != 0 && no_offers == "Brak ofert")
                            { BasicTasks.Click(helper_string + "//input[@value='Licytuj']"); i_charisma[j]--; }

                            if (BasicTasks.Search(helper_string + "//div[contains(@class,'" + inteligence[j] + "')]") && i_inteligence[j] != 0 && no_offers == "Brak ofert")
                            { BasicTasks.Click(helper_string + "//input[@value='Licytuj']"); i_inteligence[j]--; }

                            if (i != 3)
                                if (BasicTasks.Search(helper_string + "//div[contains(@class,'" + health[j] + "')]") && i_health[j] != 0 && no_offers == "Brak ofert")
                                { BasicTasks.Click(helper_string + "//input[@value='Licytuj']"); i_health[j]--; }

                            if (BasicTasks.Search("//div[@class='message fail']"))
                                break;
                        }
                    }
                    goto exit_boosters;
                }
                exit_boosters:

                if (Properties.Settings.Default.buyRings || Form1.buyRingsAction)
                {
                    string[] auctionForms;
                    Navigation.AuctionHouse();
                    BasicTasks.SelectElement("//select[@name='itemType']", "Pierścienie");
                    BasicTasks.Click("//input[@value='Filtr']");
                    IReadOnlyCollection<IWebElement> list = Form1.driver.FindElementsByXPath("//div[@id='auction_table']//form[@method='post']");
                    auctionForms = new string[list.Count];

                    for (int i = 0; i < list.Count; i++)
                        auctionForms[i] = Convert.ToString(list.ElementAt(i).GetAttribute("id"));
                    for (int i = 0; i < list.Count; i++)
                    {
                        string helperString = "//div[@id='auction_table']//form[@id='" + auctionForms[i] + "']";
                        string startPriceString = BasicTasks.GetElement(helperString + "//input[@name='bid_amount']").GetAttribute("value");
                        int startPrice = Convert.ToInt32(new String(startPriceString.Where(char.IsDigit).ToArray()));

                        ((IJavaScriptExecutor)Form1.driver).ExecuteScript("arguments[0].scrollIntoView(true);", Form1.driver.FindElementByXPath(helperString + "//div[@data-price-gold]"));
                        BasicTasks.MoveTo(helperString + "//div[@data-price-gold]");
                        string valueString = BasicTasks.GetElement("//p[@style='color:#DDDDDD']").GetAttribute("textContent");
                        int value = Convert.ToInt32(new String(valueString.Where(Char.IsDigit).ToArray()));

                        int helper = startPrice - value;

                        if (helper < 0)
                            helper *= -1;


                        string noOffers = Form1.driver.FindElementByXPath(helperString + "//div[@class='auction_bid_div']/div").GetAttribute("textContent");
                        noOffers = noOffers.Trim();

                        if (helper <= Properties.Settings.Default.differencePrice && noOffers == "Brak ofert")
                            BasicTasks.Click(helperString + "//input[@value='Licytuj']");

                        if (BasicTasks.Search("//div[@class='message fail']"))
                            break;
                    }
                    goto exit_rings;
                }
                exit_rings:

                if (Properties.Settings.Default.buyAmulets || Form1.buyRingsAction)
                {
                    string[] auctionForms;
                    Navigation.AuctionHouse();
                    BasicTasks.SelectElement("//select[@name='itemType']", "Amulety");
                    BasicTasks.Click("//input[@value='Filtr']");
                    IReadOnlyCollection<IWebElement> list = Form1.driver.FindElementsByXPath("//div[@id='auction_table']//form[@method='post']");
                    auctionForms = new string[list.Count];

                    for (int i = 0; i < list.Count; i++)
                        auctionForms[i] = Convert.ToString(list.ElementAt(i).GetAttribute("id"));
                    for (int i = 0; i < list.Count; i++)
                    {
                        string helperString = "//div[@id='auction_table']//form[@id='" + auctionForms[i] + "']";
                        string startPriceString = BasicTasks.GetElement(helperString + "//input[@name='bid_amount']").GetAttribute("value");
                        int startPrice = Convert.ToInt32(new String(startPriceString.Where(char.IsDigit).ToArray()));

                        ((IJavaScriptExecutor)Form1.driver).ExecuteScript("arguments[0].scrollIntoView(true);", Form1.driver.FindElementByXPath(helperString + "//div[@data-price-gold]"));
                        BasicTasks.MoveTo(helperString + "//div[@data-price-gold]");
                        string valueString = BasicTasks.GetElement("//p[@style='color:#DDDDDD']").GetAttribute("textContent");
                        int value = Convert.ToInt32(new String(valueString.Where(Char.IsDigit).ToArray()));

                        int helper = startPrice - value;

                        if (helper < 0)
                            helper *= -1;


                        string noOffers = Form1.driver.FindElementByXPath(helperString + "//div[@class='auction_bid_div']/div").GetAttribute("textContent");
                        noOffers = noOffers.Trim();

                        if (helper <= Properties.Settings.Default.differencePrice && noOffers == "Brak ofert")
                            BasicTasks.Click(helperString + "//input[@value='Licytuj']");

                        if (BasicTasks.Search("//div[@class='message fail']"))
                            break;
                    }
                }
            }
        }
        public static void SellItems(bool clickSell)
        {
            Form1.currently_running = "Selling items..";
            if (Properties.Settings.Default.itemsSell || Form1.sellItemsAction)
            {
                #region Variables
                int categoryNumber = 1;
                int shop = 1;
                bool first = true;
                string file_path = @"C:\Users\danie\Documents\Visual Studio 2017\Resources\Gladiatus_bots\Selling items\selling_items" + Helpers.Switch_World() + ".txt";
                int maximum_gold_level = Gold.Get_Maximum_Gold_Avalibe();

                start:

                bool gotAtLeastOne = false;
                bool secondTabWarrior = false;
                string variable = "";
                IReadOnlyCollection<IWebElement> list;
                List<string> collectionReady = new List<string>();
                List<string> collectionSelling = new List<string>();
                List<string> collectionReadyClass = new List<string>();
                #endregion

                if (File.Exists(file_path))
                {
                    BasicTasks.Click("//a[@title='Podgląd']");
                    if (!Navigation.FreeBackpack())
                        return;
                    string[] lines = File.ReadAllLines(file_path);
                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (BasicTasks.Search("//div[@id='inv']//div[@data-hash='" + lines[i] + "']"))
                            collectionSelling.Add(lines[i]);
                    }
                    File.Delete(file_path);
                }

                #region ChooseCategory
                Navigation.Packages();
                if (!BasicTasks.Search("//section[@style='display: none;']"))
                { BasicTasks.Click("//h2[contains(text(),'Opcje')]"); }

                if (!clickSell)
                {
                    if (Gold.Gold_Level() >= maximum_gold_level && !clickSell)
                        return;

                    switch (categoryNumber)
                    {
                        case 1:
                            if (Properties.Settings.Default.bronieChecked) { variable = "Bronie"; }
                            else { categoryNumber++; goto start; }
                            break;
                        case 2:
                            if (Properties.Settings.Default.tarczeChecked) { variable = "Tarcze"; }
                            else { categoryNumber++; goto start; }
                            break;
                        case 3:
                            if (Properties.Settings.Default.napierśnikiChecked) { variable = "Napierśniki"; }
                            else { categoryNumber++; goto start; }
                            break;
                        case 4:
                            if (Properties.Settings.Default.hełmyChecked) { variable = "Hełmy"; }
                            else { categoryNumber++; goto start; }
                            break;
                        case 5:
                            if (Properties.Settings.Default.rękawiceChecked) { variable = "Rękawice"; }
                            else { categoryNumber++; goto start; }
                            break;
                        case 6:
                            if (Properties.Settings.Default.butyChecked) { variable = "Buty"; }
                            else { categoryNumber++; goto start; }
                            break;
                        case 7:
                            if (Properties.Settings.Default.pierścienieChecked) { variable = "Pierścienie"; }
                            else { categoryNumber++; goto start; }
                            break;
                        case 8:
                            if (Properties.Settings.Default.amuletyChecked) { variable = "Amulety"; }
                            else { categoryNumber++; goto start; }
                            break;
                        case 9:
                            if (Properties.Settings.Default.przyspieszaczeChecked) { variable = "Przyspieszacze"; }
                            else { categoryNumber++; goto start; }
                            break;
                        case 10:
                            if (Properties.Settings.Default.bonusyChecked) { variable = "Bonusy"; }
                            else { categoryNumber++; goto start; }
                            break;
                        case 11:
                            if (Properties.Settings.Default.błogosławieństwaChecked) { variable = "Błogosławieństwa"; }
                            else { categoryNumber++; goto start; }
                            break;
                        case 12:
                            if (Properties.Settings.Default.zwójChecked) { variable = "Zwój"; }
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
                    if (Gold.Gold_Level() > Convert.ToInt32(Properties.Settings.Default.gold_level) &&
                        Properties.Settings.Default.gold_limit ||
                        Gold.Gold_Level() >= maximum_gold_level && !clickSell)
                    {
                        return;
                    }

                    switch (categoryNumber)
                    {
                        case 1:
                            if (Properties.Settings.Default.bronieSellChecked) { variable = "Bronie"; }
                            else { categoryNumber++; goto start; }
                            break;
                        case 2:
                            if (Properties.Settings.Default.tarczeSellChecked) { variable = "Tarcze"; }
                            else { categoryNumber++; goto start; }
                            break;
                        case 3:
                            if (Properties.Settings.Default.napierśnikiSellChecked) { variable = "Napierśniki"; }
                            else { categoryNumber++; goto start; }
                            break;
                        case 4:
                            if (Properties.Settings.Default.hełmySellChecked) { variable = "Hełmy"; }
                            else { categoryNumber++; goto start; }
                            break;
                        case 5:
                            if (Properties.Settings.Default.rękawiceSellChecked) { variable = "Rękawice"; }
                            else { categoryNumber++; goto start; }
                            break;
                        case 6:
                            if (Properties.Settings.Default.butySellChecked) { variable = "Buty"; }
                            else { categoryNumber++; goto start; }
                            break;
                        case 7:
                            if (first) { shop = 1; first = false; }
                            if (Properties.Settings.Default.pierścienieSellChecked) { variable = "Pierścienie"; }
                            else { categoryNumber++; goto start; }
                            break;
                        case 8:
                            if (first) { shop = 1; first = false; }
                            if (Properties.Settings.Default.amuletySellChecked) { variable = "Amulety"; }
                            else { categoryNumber++; goto start; }
                            break;
                        case 9:
                            if (first) { shop = 1; first = false; }
                            if (Properties.Settings.Default.przyspieszaceSellChecked) { variable = "Przyspieszacze"; }
                            else { categoryNumber++; goto start; }
                            break;
                        case 10:
                            if (first) { shop = 1; first = false; }
                            if (Properties.Settings.Default.bonusySellChecked) { variable = "Bonusy"; }
                            else { categoryNumber++; goto start; }
                            break;
                        case 11:
                            if (first) { shop = 1; first = false; }
                            if (Properties.Settings.Default.błogosławieństwaSellChecked) { variable = "Błogosławieństwa"; }
                            else { categoryNumber++; goto start; }
                            break;
                        case 12:
                            if (first) { shop = 1; first = false; }
                            if (Properties.Settings.Default.zwójSellChecked) { variable = "Zwój"; }
                            else { categoryNumber++; goto start; }
                            break;
                        default:
                            if (File.Exists(file_path))
                                File.Delete(file_path);
                            return;
                    }
                }

                if (BasicTasks.Search("//section[@style='display: none;']"))
                { BasicTasks.Click("//h2[@class='section-header'][contains(text(), 'Opcje')]"); }
                BasicTasks.SelectElement("//select[@name='f']", variable);
                BasicTasks.Click("//input[@value='Filtr']");
                #endregion

                #region GetItemsFromPackages
                int clickedTimes = 0;
                if (BasicTasks.Search("//a[@class='paging_button paging_right_full']"))
                { BasicTasks.Click("//a[@class='paging_button paging_right_full']"); }
                for (int i = 0; i < 3; i++)
                {
                    if (BasicTasks.Search("//a[@class='paging_button paging_left_step']"))
                    { BasicTasks.Click("//a[@class='paging_button paging_left_step']"); }
                }
                list = Form1.driver.FindElementsByXPath("//div[@id='packages']//div[contains(@class,'ui-draggable')]");
                for (int i = 0; i < 3; i++)
                {
                    if (BasicTasks.Search("//div[@id='packages']//div[contains(@class,'ui-draggable')]"))
                    {
                        list = Form1.driver.FindElementsByXPath("//div[@id='packages']//div[contains(@class,'ui-draggable')]");
                        collectionReady.AddRange(Helpers.FindReadyObjects(list, categoryNumber));
                        collectionReadyClass.AddRange(Helpers.FindReadyObjectsClass(list, categoryNumber));
                    }

                    if (BasicTasks.Search("//a[@class='paging_button paging_right_step']"))
                    { BasicTasks.Click("//a[@class='paging_button paging_right_step']"); clickedTimes++; }
                    else { break; }
                }

                for (int i = 0; i < clickedTimes; i++)
                { BasicTasks.Click("//a[@class='paging_button paging_left_step']"); }

                if (collectionReady.Count == 0)
                { categoryNumber++; goto start; }

                if (!Navigation.FreeBackpack()) { return; }
                if (!BasicTasks.Search("//section[@style='display: none;']")) { BasicTasks.Click("//h2[@class='section-header'][contains(text(), 'Opcje')]"); }
                for (int i = 0; i < collectionReady.Count; i++)
                {
                    bool done = false;
                    bool noPlace = false;
                    while (!done)
                    {
                        if (BasicTasks.Search("//div[@id='packages']//div[@data-hash='" + collectionReady.ElementAt(i) + "']"))
                        { BasicTasks.MoveMoveElement("//div[@id='packages']//div[@data-hash='" + collectionReady.ElementAt(i) + "']", "//input[@name='show-item-info']"); }
                        else { done = true; continue; }

                        if (!BasicTasks.Search("//body[@id='packagesPage']/div[contains(@class,'" + collectionReadyClass.ElementAt(i) + "')]"))
                        {
                            BasicTasks.ReleaseElement("//input[@name='show-item-info']");
                            Form1.driver.Navigate().Refresh();
                            Navigation.FreeBackpack();
                            continue;
                        }

                        if (BasicTasks.Search("//div[@class='ui-droppable grid-droparea image-grayed active']"))
                        {
                            BasicTasks.ReleaseElement("//div[@class='ui-droppable grid-droparea image-grayed active']");
                            if (BasicTasks.Search("//div[@id='inv']//div[@data-hash='" + collectionReady.ElementAt(i) + "']"))
                            {
                                collectionSelling.Add(collectionReady.ElementAt(i));
                                gotAtLeastOne = true;
                                done = true;
                                continue;
                            }
                        }
                        else { BasicTasks.ReleaseElement("//input[@name='show-item-info']"); noPlace = true; break; }
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
                            Navigation.Weapon_Seller();
                            Navigation.Third_Tab_Sellers();
                            break;
                        case 2:
                            Navigation.Breastplates_Seller();
                            Navigation.Third_Tab_Sellers();
                            break;
                        case 3:
                            Navigation.Shopkeeper_Seller();
                            Navigation.Second_Tab_Sellers();
                            break;
                        case 4:
                            Navigation.Shopkeeper_Seller();
                            Navigation.Third_Tab_Sellers();
                            break;
                        case 5:
                            Navigation.Alchemic_Seller();
                            break;
                        case 6:
                            Navigation.Alchemic_Seller();
                            Navigation.Second_Tab_Sellers();
                            break;
                        case 7:
                            Navigation.Alchemic_Seller();
                            Navigation.Third_Tab_Sellers();
                            break;
                        case 8:
                            Navigation.Soldier_Seller();
                            break;
                        case 9:
                            Navigation.Soldier_Seller();
                            Navigation.Second_Tab_Sellers();
                            break;
                        case 10:
                            Navigation.Soldier_Seller();
                            Navigation.Third_Tab_Sellers();
                            break;
                        case 11:
                            Navigation.Malefica_Seller();
                            break;
                        case 12:
                            Navigation.Malefica_Seller();
                            Navigation.Second_Tab_Sellers();
                            break;
                        case 13:
                            Navigation.Malefica_Seller();
                            Navigation.Third_Tab_Sellers();
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
                            Navigation.Weapon_Seller();
                            Navigation.Third_Tab_Sellers();
                            break;
                        case 2:
                            Navigation.Breastplates_Seller();
                            Navigation.Third_Tab_Sellers();
                            break;
                        case 3:
                            Navigation.Shopkeeper_Seller();
                            Navigation.Third_Tab_Sellers();
                            break;
                        case 4:
                            Navigation.Alchemic_Seller();
                            Navigation.Third_Tab_Sellers();
                            break;
                        case 5:
                            Navigation.Soldier_Seller();
                            Navigation.Third_Tab_Sellers();
                            break;
                        case 6:
                            Navigation.Malefica_Seller();
                            Navigation.Third_Tab_Sellers();
                            break;
                        default:
                            return;
                    }
                }
                #endregion

                #region Selling Shop
                Navigation.FreeBackpack();

                string rublesString = BasicTasks.GetElement("//div[@id='sstat_ruby_val']").GetAttribute("textContent");
                int rubles = Convert.ToInt32(rublesString);

                Navigation.FreeBackpack();
                notMovedYet:
                while (collectionSelling.Count != 0)
                {
                    if (BasicTasks.Search("//div[@id='shop']//div[@data-hash='" + collectionSelling.ElementAt(0) + "']")
                        || !BasicTasks.Search("//div[@id='inv']//div[@data-hash='" + collectionSelling.ElementAt(0) + "']"))
                    { collectionSelling.RemoveAt(0); continue; }
                    BasicTasks.MoveMoveElement("//div[@id='inv']//div[@data-hash='" + collectionSelling.ElementAt(0) + "']", "//a[@class='awesome-tabs current']");
                    if (BasicTasks.Search("//div[@id='shop']//div[@class='ui-droppable grid-droparea image-grayed active']"))
                    {
                        BasicTasks.ReleaseElement("//div[@id='shop']//div[@class='ui-droppable grid-droparea image-grayed active']");
                    }
                    else
                    {
                        BasicTasks.ReleaseElement("//a[@class='awesome-tabs current']");
                        if (shop != 13 && categoryNumber >= 7)
                        { shop++; goto changeShop; }
                        else if (rubles != 0 && collectionSelling.Count != 0 && categoryNumber >= 7)
                        { BasicTasks.Click("//input[@value='Nowe towary']"); shop = 1; goto changeShop; }
                        else if (shop == 5 && !secondTabWarrior && collectionSelling.Count != 0)
                        { secondTabWarrior = true; BasicTasks.Click("//div[@class='shopTab'][text() = 'Ⅱ']"); goto notMovedYet; }
                        else if (shop == 6 && collectionSelling.Count != 0 && rubles != 0)
                        { BasicTasks.Click("//input[@value='Nowe towary']"); shop = 1; goto changeShop; }
                        else
                        { shop++; goto changeShop; }
                    }
                }
                collectionSelling.Clear();
                bool found = false;
                for (int i = 0; i < collectionReady.Count; i++)
                {
                    if (BasicTasks.Search("//div[@id='inv']//div[@data-hash='" + collectionReady.ElementAt(i) + "']"))
                    { collectionSelling.Add(collectionReady.ElementAt(i)); found = true; }
                }
                if (found) { goto notMovedYet; }
                #endregion

                goto start;
            }
        }
        public static void MovingFood()
        {
            Form1.currently_running = "Moving food..";
            if (Properties.Settings.Default.moveFood)
            {
                bool firstTime = true;
                #region Filtr
                if (firstTime)
                {
                    firstTime = false;
                    Navigation.Packages();
                    if (BasicTasks.Search("//section[@style='display: none;']")) { BasicTasks.Click("//h2[contains(text(),'Opcje')]"); }
                    BasicTasks.SelectElement("//select[@name='f']", "Jadalne");
                    BasicTasks.Click("//input[@value='Filtr']");
                    if (BasicTasks.Search("//a[@class='paging_button paging_right_full']")) { BasicTasks.Click("//a[@class='paging_button paging_right_full']"); }
                }
                #endregion Filtr
                #region Moving
                if (!Navigation.FoodBackpack())
                    return;
                while (BasicTasks.Search("//div[@class='packageItem']//div[contains(@data-content-type,'64')]"))
                {
                    if (BasicTasks.Search(Helpers.Switch_Backpack_Food() + "[@data-available='false']")) { return; }
                    if (!BasicTasks.Search(Helpers.Switch_Backpack_Food() + "[@class='awesome-tabs current']")) { Navigation.FoodBackpack(); }
                    BasicTasks.MoveMoveElement("//div[@class='packageItem']//div[contains(@data-content-type,'64')]", "//div[@id='inv']");
                    if (BasicTasks.Search("//div[@id='inv']//div[@class='ui-droppable grid-droparea image-grayed active']"))
                    { BasicTasks.ReleaseElement("//div[@id='inv']//div[@class='ui-droppable grid-droparea image-grayed active']"); }
                    else { return; }
                }
                if (!BasicTasks.Search("//div[@class='packageItem']//div[contains(@data-content-type,'64')]"))
                {
                    BuyFood(true);
                    return;
                }
                #endregion
            }
        }
        public static void ExtractItems()
        {
            Form1.currently_running = "Extracting items..";
            if (!Properties.Settings.Default.extractItems)
                return;
            Get_Items_For_Extract();
            Navigation.Arena();
            BasicTasks.Click("//a[@class='menuitem '][text() = 'Roztapiarka']");
            if (!Navigation.ExtractBackpack())
                return;
            string inv_draggable = "//div[@id='inv']//div[contains(@class,'ui-draggable')]";
            if (Form1.driver.FindElementsByXPath(inv_draggable).Count == 0)
                return;
            for(int i=0; i<6; i++)
            {
                string forge_closed = "//div[contains(@class,'forge_closed " + i + "')]";
                string forge_finished = "//div[contains(@class,'forge_finished-succeeded " + i + "')]";
                if (BasicTasks.Search(forge_closed))
                    BasicTasks.Click(forge_closed);
                else if (BasicTasks.Search(forge_finished))
                {
                    BasicTasks.Click(forge_finished);
                    BasicTasks.Click("//div[contains(text(),'Wyślij jako pakiet')]");
                }
                else
                    continue;
                BasicTasks.WaitForXPath("//div[@class='forge_closed " + i + " tabActive']");
                Navigation.ExtractBackpack();
                if (!BasicTasks.Search("//div[@id='inv']//div[contains(@class,'ui-draggable')]"))
                    break;
                BasicTasks.MoveReleaseElement(inv_draggable, "//fieldset[@id='crafting_input']//div[@class='ui-droppable']");
                if(BasicTasks.Search("//fieldset[@id='crafting_input']//div[@id='itembox']/div[contains(@class,'ui-draggable')]"))
                {
                    BasicTasks.WaitForXPath("//div[@class='icon_gold']");
                    BasicTasks.Click("//div[@class='icon_gold']");
                }
                if (!BasicTasks.Search("//div[@class='error'][contains(text(),'Nie masz wystarczającej ilości złota')]"))
                    BasicTasks.WaitForXPath("//div[@class='forge_crafting " + i + " tabActive']");
                else
                    break;
            }
            //Navigation.ExtractBackpack();
            //if (BasicTasks.Search("//div[@id='inv']//div[contains(@class,'ui-draggable')]"))
            //    return;
            //for (int i = 0; i < 6; i++)
            //{
            //    if (BasicTasks.Search("//div[contains(@class,'forge_closed " + i + "')]"))
            //    {
            //        if (!Navigation.ExtractBackpack())
            //            return;
            //        BasicTasks.Click("//div[contains(@class,'forge_closed " + Convert.ToString(i) + "')]");
            //        BasicTasks.MoveReleaseElement("//div[@id='inv']//div[contains(@class,'ui-draggable')]", "//fieldset[@id='crafting_input']//div[@class='ui-droppable']");
            //        BasicTasks.Click("//div[@class='icon_gold']");
            //    }
            //    else if (BasicTasks.Search("//div[contains(@class,'forge_finished-succeeded " + Convert.ToString(i) + "')]"))
            //    {
            //        if (!Navigation.ExtractBackpack())
            //            return;
            //        BasicTasks.Click("//div[contains(@class,'forge_finished-succeeded " + Convert.ToString(i) + "')]");
            //        BasicTasks.Click("//div[@class='awesome-button'][text() = 'Wyślij jako pakiet']");
            //        BasicTasks.MoveReleaseElement("//div[@id='inv']//div[contains(@class,'ui-draggable')]", "//fieldset[@id='crafting_input']//div[@class='ui-droppable']");
            //        BasicTasks.Click("//div[@class='icon_gold']");
            //    }
            //}
        }
        public static void Training()
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

                Navigation.Arena();
                BasicTasks.Click("//a[@class='menuitem '][text() = 'Trening']");
                while (Form1.driver.FindElementsByXPath("//a[contains(@href,'skillToTrain=" + variable + "')]").Count != 0)
                {
                    BasicTasks.Click("//a[contains(@href,'skillToTrain=" + variable + "')]");
                }
            }
        }
        public static void Store_Components()
        {
            if (!Properties.Settings.Default.send_components)
                return;
            Navigation.Arena();
            BasicTasks.Click("//a[contains(@class,'menuitem')][text() = 'Magazyn surowców']");
            while (BasicTasks.Search("//button[@id='store'][@disabled='']"))
                BasicTasks.Click("//input[@id='from-packages']");
            BasicTasks.Click("//button[@id='store'][text() = 'Jazda!']");
        }
        public static void Download_Packages()
        {
            string file_path =
            @"C:\Users\danie\Documents\Visual Studio 2017\Resources\Gladiatus_bots\Items .txt files for Gladiatus_bot" + @"\items(" + Helpers.Switch_World() + ").txt";
            if (File.Exists(file_path))
                File.Delete(file_path);
            bool first = true;
            do
            {
                Navigation.Guild_Market();
                if (!first) { BasicTasks.Click("//a[contains(text(),'Następna strona')]"); }
                int first_iterator = Form1.driver.FindElementsByXPath("//input[@value='Kup']").Count();
                int second_iterator = Form1.driver.FindElementsByXPath("//input[@value='Anuluj']").Count();
                int iterator = first_iterator + second_iterator;
                for (int i = 2; i <= iterator + 1; i++)
                {
                    IWebElement element = Form1.driver.FindElementByXPath("//section[@id='market_table']//tr[position()='" + i + "']/td[@style]/div[@style]");
                    string soulbound = element.GetAttribute("data-soulbound-to");
                    string price_item_string = Form1.driver.FindElementByXPath
                        ("//section[@id='market_table']//tr[position()='" + i + "']/td[position()='3']").GetAttribute("textContent");
                    string level = element.GetAttribute("data-level");
                    string quality_level = element.GetAttribute("data-quality");
                    string amount = element.GetAttribute("data-amount");
                    string price_item = new String(price_item_string.Where(Char.IsDigit).ToArray());
                    string category = element.GetAttribute("data-content-type");
                    string class_name = element.GetAttribute("class");

                    string already_sold = "0";
                    BasicTasks.MoveTo("//section[@id='market_table']//tr[position()='" + i + "']/td[@style]/div[@style]");
                    if (BasicTasks.Search("//p[contains(text(),'Wskazówka')]"))
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
                    string ready_line =
                        "class_name='" + class_name + "' soulbound='" + soulbound + "' price='" + price_item + "' category='" + 
                        category + "' quality='" + quality_level + "' level='" + level + "' amount='" + amount + "' sold='" + already_sold + "'";
                    File.AppendAllText(file_path, ready_line + Environment.NewLine);
                }
                first = false;
            } while (BasicTasks.Search("//a[contains(text(),'Następna strona')]"));
        }
        public static void Get_Items_For_Extract()
        {
            Get_Items_For_Extract_Colours();
            Get_Items_For_Extract_Custom();
        }
        public static void Get_Items_For_Extract_Colours()
        {
            if (Properties.Settings.Default.get_for_extract)
            {
                string colour = "";
                if (Properties.Settings.Default.purple_extracting)
                    colour = "Mars (purpurowy)";
                else if (Properties.Settings.Default.orange_extracing)
                    colour = "Jupiter (pomarańczowy)";
                else if (Properties.Settings.Default.red_extracting)
                    colour = "Olimp (czerwony)";
                else
                    return;

                Navigation.Packages();
                BasicTasks.SelectElement("//select[@name='fq']", colour);
                BasicTasks.Click("//input[@value='Filtr']");

                if (BasicTasks.Search("//a[@class='paging_button paging_right_full']"))
                    BasicTasks.Click("//a[@class='paging_button paging_right_full']");

                string[] invalid_types = new string[4];
                invalid_types[0] = "64";
                invalid_types[1] = "4096";
                invalid_types[2] = "8192";
                invalid_types[3] = "32768";
                bool loop = true;
                while(loop)
                {
                    loop = false;
                    Navigation.ExtractBackpack();
                    IList<IWebElement> all_items = Form1.driver.FindElementsByXPath("//div[@id='packages']//div[contains(@class,'ui-draggable')]");
                    IList<IWebElement> good_items = new List<IWebElement>();
                    for (int i = all_items.Count - 1; i >= 0; i--)
                    {
                        bool good = true;
                        for (int j = 0; j < invalid_types.Count(); j++)
                        {
                            if (all_items.ElementAt(i).GetAttribute("data-content-type") == invalid_types[j])
                            {
                                good = false;
                                break;
                            }
                        }

                        if (good)
                            good_items.Add(all_items.ElementAt(i));
                    }

                    if (good_items.Count == 0 && !BasicTasks.Search("//a[@class='paging_button paging_left_step']"))
                        return;

                    for (int i = good_items.Count - 1; i >= 0; i--)
                    {
                        if (good_items.ElementAt(i).GetAttribute("data-quality") == "4" && Properties.Settings.Default.red_extracting)
                        {
                            if (!Move_Item_For_Extract(good_items.ElementAt(i)))
                                return;
                        }
                        else if (good_items.ElementAt(i).GetAttribute("data-quality") == "3" && Properties.Settings.Default.orange_extracing)
                        {
                            if (!Move_Item_For_Extract(good_items.ElementAt(i)))
                                return;
                        }
                        else if (good_items.ElementAt(i).GetAttribute("data-quality") == "2" && Properties.Settings.Default.purple_extracting)
                        {
                            if (!Move_Item_For_Extract(good_items.ElementAt(i)))
                                return;
                        }
                    }

                    if(BasicTasks.Search("//a[@class='paging_button paging_left_step']"))
                    {
                        BasicTasks.Click("//a[@class='paging_button paging_left_step']");
                        loop = true;
                    }
                }
            }
        }
        public static void Get_Items_For_Extract_Custom()
        {
            string file_path = @"C:\Users\danie\Documents\Visual Studio 2017\Resources\Gladiatus_bots\custom_names_extract.txt";
            string[] lines = null;
            if (File.Exists(file_path))
                lines = File.ReadAllLines(file_path);
            else
                return;
            Navigation.Packages();
            for (int i = 0; i < lines.Length; i++)
            {
                IWebElement passwordElement = BasicTasks.GetElement("//input[@name='qry']");
                passwordElement.SendKeys(lines[i]);
                BasicTasks.Click("//input[@value='Filtr']");
                Navigation.ExtractBackpack();
                string path = "//div[@id='packages']//div[contains(@class,'ui-draggable')]";
                string[] invalid_types = new string[4];
                invalid_types[0] = "64";
                invalid_types[1] = "4096";
                invalid_types[2] = "8192";
                invalid_types[3] = "32768";

                while (true)
                {
                    IReadOnlyCollection<IWebElement> elements = Form1.driver.FindElementsByXPath(path);
                    int x = -1;
                    for(int j=0; j<elements.Count(); j++)
                    {
                        string t = elements.ElementAt(i).GetAttribute("data-content-type");
                        x = j;
                        for(int k=0; k<invalid_types.Length; k++)
                        {
                            if (t == invalid_types[k])
                            {
                                x = -1;
                                break;
                            }
                        }
                        if (x != -1)
                            break;
                    }
                    if (x == -1)
                        return;
                    if (!Move_Item_For_Extract(elements.ElementAt(x)))
                        return;
                    Thread.Sleep(700);
                }
            }
        }
        public static void HealMe(int value)
        {
            if (!Properties.Settings.Default.heal_me)
                return;
            while (Health_Level() < value)
            {
                BasicTasks.Click("//a[@title='Podgląd']");
                BasicTasks.Click("//div[@class='charmercpic doll1']");
                if (!Navigation.FoodBackpack()) { return; }

                IReadOnlyCollection<IWebElement> food_elements = Form1.driver.FindElementsByXPath("//div[@id='inv']/div[@data-content-type='64']");
                if (food_elements.Count == 0)
                {
                    MovingFood();
                    return;
                }
                string[] data_tooltips = new string[food_elements.Count];
                int[] health_food = new int[food_elements.Count];

                for (int i = 0; i < food_elements.Count; i++)
                {
                    data_tooltips[i] = food_elements.ElementAt(i).GetAttribute("data-tooltip");
                }

                int current_health = Convert.ToInt32(Form1.driver.FindElementByXPath("//div[@id='header_values_hp_bar']").GetAttribute("data-value"));
                int current_health_max = Convert.ToInt32(Form1.driver.FindElementByXPath("//div[@id='header_values_hp_bar']").GetAttribute("data-max-value"));
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
                Actions move = new Actions(Form1.driver);
                move.ClickAndHold(best_element);
                move.Release(BasicTasks.GetElement("//div[@id='avatar']/div[@class='ui-droppable']"));
                move.Build().Perform();
                BasicTasks.Click("//a[@title='Podgląd']");
            }
        }
        public static int Health_Level()
        {
            string currentHealth = BasicTasks.GetElement("//div[@id='header_values_hp_percent']").GetAttribute("textContent");
            currentHealth = currentHealth.Substring(0, currentHealth.Length - 1);
            int currentHealthInt = Convert.ToInt32(currentHealth);
            return currentHealthInt;
        }
        private static bool Move_Item_For_Extract(IWebElement element)
        {
            BasicTasks.MoveMoveElement(element, "//input[@name='show-item-info']");
            if (BasicTasks.Search("//div[@class='ui-droppable grid-droparea image-grayed active']"))
            {
                BasicTasks.ReleaseElement("//div[@class='ui-droppable grid-droparea image-grayed active']");
                return true;
            }
            else
                return false;
        }
    }
}
