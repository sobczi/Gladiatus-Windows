using System.Threading;
using OpenQA.Selenium;

namespace Gladiatus_35
{
    static class Navigation
    {
        public static bool FreeBackpack()
        {
            if (!BasicTasks.Search(Helpers.Switch_Backpack_Free() + "[@data-available='false']"))
            {
                BasicTasks.Click(Helpers.Switch_Backpack_Free());
                return true;
            }
            else
                return false;
        }
        public static bool ExtractBackpack()
        {
            if (!BasicTasks.Search(Helpers.Switch_Backpack_Extract() + "[@data-available='false']"))
            {
                BasicTasks.Click(Helpers.Switch_Backpack_Extract());
                return true;
            }
            else
                return false;
        }
        public static void Review()
        {
            BasicTasks.Click("//a[@title='Podgląd']");
            BasicTasks.Click("//div[@class='charmercpic doll1']");
            FreeBackpack();
        }
        public static bool FoodBackpack()
        {
            if (!BasicTasks.Search(Helpers.Switch_Backpack_Food() + "[@data-available='false']"))
            {
                BasicTasks.Click(Helpers.Switch_Backpack_Food());
                return true;
            }
            else
                return false;
        }
        public static void Packages()
        {
            BasicTasks.Click("//a[@id='menue_packages']");
        }
        public static void Arena()
        {
            bool non_stop = true;
            while (non_stop)
            {
                BasicTasks.Click("//div[@id='cooldown_bar_arena']/a");
                IWebElement element = Form1.driver.FindElementByXPath("//*[@id='mainnav']/li/table/tbody/tr/td[1]/a");
                string element_name = element.GetAttribute("textContent");
                if (element_name == "Arena") { BasicTasks.Click("//*[@id='mainnav']/li/table/tbody/tr/td[1]/a"); return; }
            }
        }
        public static void AuctionHouse()
        {
            Arena();
            BasicTasks.Click("//a[@class='menuitem '][text() = 'Dom aukcyjny']");
        }
        public static void Weapon_Seller()
        {
            BasicTasks.Click("//a[contains(@class,'menuitem')][text() = 'Broń']");
        }
        public static void Breastplates_Seller()
        {
            BasicTasks.Click("//a[contains(@class,'menuitem')][text() = 'Pancerz']");
        }
        public static void Shopkeeper_Seller()
        {
            BasicTasks.Click("//a[contains(@class,'menuitem')][text() = 'Handlarz']");
        }
        public static void Alchemic_Seller()
        {
            BasicTasks.Click("//a[contains(@class,'menuitem')][text() = 'Alchemik']");
        }
        public static void Soldier_Seller()
        {
            BasicTasks.Click("//a[contains(@class,'menuitem')][text() = 'Żołnierz']");
        }
        public static void Malefica_Seller()
        {
            BasicTasks.Click("//a[contains(@class,'menuitem')][text() = 'Malefica']");
        }
        public static void Second_Tab_Sellers()
        {
            int iterator = 0;
            while (!BasicTasks.Search("//div[@class='shopTab'][text() = 'Ⅱ']"))
            {
                Thread.Sleep(1000);
                if (iterator == 5) { return; }
                iterator++;
            }
            BasicTasks.Click("//div[@class='shopTab'][text() = 'Ⅱ']");
        }
        public static void Third_Tab_Sellers()
        {
            int iterator = 0;
            while (!BasicTasks.Search("//div[@class='shopTab dynamic']"))
            {
                Thread.Sleep(1000);
                if (iterator == 5) { return; }
                iterator++;
            }
            BasicTasks.Click("//div[@class='shopTab dynamic']");
        }
        public static void Guild_Market()
        {
            while (!BasicTasks.Search("//a[contains(@href,'guildMarket')][@class='map_label']"))
            { BasicTasks.Click("//a[text() = 'Gildia']"); }
            BasicTasks.Click("//a[contains(@href,'guildMarket')][@class='map_label']");
            Guild_Market_Show();
        }
        private static void Guild_Market_Show()
        {
            while (!BasicTasks.Search("//a[contains(text(),'Cena na rynku')]"))
            {
                BasicTasks.WaitForXPath("//section[@id='market_table']");
                string helper = Form1.driver.FindElementByXPath("//section[@id='market_table']").GetAttribute("style");
                if (helper == "display: none;") { BasicTasks.Click("//h2[contains(text(),'Przedmiot')]"); }
            }
        }
        public static void Guild_Medic()
        {
            while (!BasicTasks.Search("//a[contains(@href,'guild_medic')][@id='doctor_div']"))
                BasicTasks.Click("//a[text() = 'Gildia']");
            BasicTasks.Click("//a[contains(@href,'guild_medic')][@id='doctor_div']");
        }
    }
}
