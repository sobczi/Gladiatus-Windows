using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Threading;

namespace Gladiatus_35
{
    class Tests
    {
        private static ChromeDriver driver;
        private static Tasks _Tasks;
        private static BasicTasks _BasicTasks;

        public Tests(ChromeDriver _driver)
        {
            driver = _driver;
            _Tasks = new Tasks(driver);
            _BasicTasks = new BasicTasks(driver);
        }

        public void RunTests()
        {
            
        }
        public void ReadingNames()
        {
            string helper;
            string path = @"C:\Users\danie\Desktop\arenaFarm.txt";
            IReadOnlyCollection<IWebElement> list = driver.FindElementsByXPath("//tr[@class='alt']//span[@data-tooltip]//a[contains(@href,'index')]");

            foreach (IWebElement element in list)
            {
                helper = element.GetAttribute("textContent");
                File.AppendAllText(path, helper + Environment.NewLine);
            }
        }
        public void SavingGoldLevel(int value)
        {
            Tasks _Tasks = new Tasks(driver);
            string goldLevel = Convert.ToString(_Tasks.Gold_Level());
            string time = DateTime.Now.ToString("h:mm:ss tt");
            string logs = "";

            for (int i = goldLevel.Length - 3; i > 0; i -= 3)
            {
                if (i > 0) { goldLevel = goldLevel.Insert(i," "); }
                else { break; }
            }

            switch(value)
            {
                case 1:
                    logs = "Logging-in";
                    break;
                case 2:
                    logs = "Logging-out";
                    break;
                default:
                    return;
            }

            File.AppendAllText(@"C:\Users\danie\OneDrive\Pulpit\Gold level.txt", Environment.NewLine + "Time: (" + time + ")" + Environment.NewLine  + "Gold level: " + goldLevel + Environment.NewLine + "Status: " + logs + Environment.NewLine);
        }
        public void SellingItems(int value, string goldBefore, string nameOfItem)
        {
            Tasks _Tasks = new Tasks(driver);
            string goldCurrent = Convert.ToString(_Tasks.Gold_Level());
            string time = DateTime.Now.ToString("h:mm:ss tt");
            string logs = "";

            for (int i = goldCurrent.Length - 3; i > 0; i -= 3)
            {
                if (i > 0) { goldCurrent = goldCurrent.Insert(i, " "); }
                else { break; }
            }

            switch(value)
            {
                case 1:
                    logs = "Bought";
                    break;
                case 2:
                    logs = "Sold";
                    break;
                default:
                    return;
            }

            File.AppendAllText(@"C:\Users\danie\OneDrive\Pulpit\Selling Items.txt", Environment.NewLine + "Time: (" + time + ")" + Environment.NewLine + "Name: " + nameOfItem + Environment.NewLine + "Gold before: " + goldBefore + Environment.NewLine + "Gold after: " + goldCurrent + Environment.NewLine + "Status: " + logs + Environment.NewLine);

        }
    }
}
