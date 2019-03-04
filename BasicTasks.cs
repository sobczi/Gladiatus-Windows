using System.Windows.Forms;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System;
using System.Diagnostics;

namespace Gladiatus_35
{
    class BasicTasks
    {
        private static ChromeDriver driver;
        public BasicTasks(ChromeDriver _driver) { driver = _driver; }
        public BasicTasks() { }
        public bool Search(string path)
        {
            if (driver.FindElementsByXPath(path).Count == 0) { return false; }
            else { return true; }
        }
        public void Click(string path)
        {
            //Random rnd = new Random();
            //int number = rnd.Next(3, 3) * 1000;
            //Thread.Sleep(number);
            for (int i = 0; i < 2; i++)
            {
                IWebElement element = driver.FindElementByXPath(path);
                try { Thread.Sleep(300); element.Click(); return; }
                catch { Refresh(); }
            }
            return;
        }
        public void SelectElement(string xpath1, string text)
        {
            var choose = GetElement(xpath1);
            var selectElement = new SelectElement(choose);
            selectElement.SelectByText(text);
        }
        public void MoveReleaseElement(string xpath1, string xpath2)
        {
            Thread.Sleep(500);
            Actions move = new Actions(driver);
            move.ClickAndHold(GetElement(xpath1));
            move.Release(GetElement(xpath2));
            move.Build().Perform();
        }
        public void MoveMoveElement(string xpath1, string xpath2)
        {
            Thread.Sleep(500);
            Actions move = new Actions(driver);
            move.ClickAndHold(GetElement(xpath1));
            move.MoveToElement(GetElement(xpath2));
            move.Build().Perform();
        }
        public void MoveMoveElement(IWebElement element, string xpath2)
        {
            Thread.Sleep(500);
            Actions move = new Actions(driver);
            move.ClickAndHold(element);
            move.MoveToElement(GetElement(xpath2));
            move.Build().Perform();
        }
        public void ReleaseElement(string xpath1)
        {
            Thread.Sleep(500);
            Actions move = new Actions(driver);
            move.Release(GetElement(xpath1));
            move.Build().Perform();
        }
        public void MoveTo(string xpath1)
        {
            Thread.Sleep(500);
            Actions move = new Actions(driver);
            move.MoveToElement(GetElement(xpath1));
            move.Build().Perform();
        }
        public void CheckEvenets()
        {
            string[] paths = new string[4];
            paths[0] = "//input[@id='linkLoginBonus']";
            paths[1] = "//a[contains(@onclick,'MAX_simplepop')]";
            paths[2] = "//*[@id='linkcancelnotification']";
            paths[3] = "//*[@id='linknotification']";

            for (int i = 0; i < paths.Length; i++)
            {
                try { var button = driver.FindElementByXPath(paths[i]); button.Click(); }
                catch { }
            }

            if (Search("//div[@id='title_infobox']"))
            {
                if (driver.FindElementByXPath("//div[@id='title_infobox']").GetAttribute("textContent") == "Prace konserwacyjne")
                { Thread.Sleep(360000); }
            }
        }
        public IWebElement GetElement(string path)
        {
            WaitForXPath(path);
            IWebElement element = driver.FindElementByXPath(path);
            return element;
        }
        public void WaitForXPath(string path)
        {
            int iterator = 0;
            bool found = false;

            while (iterator != 240 && !found)
            {
                if (!Search(path)) { iterator++; Thread.Sleep(500); }
                else { found = true; }
            }
        }
        public void Refresh()
        {
            CheckEvenets();
            driver.Navigate().Refresh();
            return;
        }
        public void Save_Exception(Exception _exception, string _server)
        {
            var time_now = DateTime.Now;
            string time_now_string = time_now.ToString("dd.MM.yyyy HH:mm:ss");
            File.AppendAllText(@"C:\Users\danie\Documents\Visual Studio 2017\Resources\Gladiatus_bots\catched_exceptions.txt",
                Environment.NewLine + Environment.NewLine + "<---------------->" + time_now_string +"<---------------->" + _server + Environment.NewLine + Convert.ToString(_exception));
        }
        public void Exit(bool _turn_off)
        {
            if(driver != null)
            { driver.Close(); driver.Quit(); }
            if (Properties.Settings.Default.sleepModeChecked && _turn_off && Properties.Settings.Default.main_bot)
            { KillChromes(); Application.SetSuspendState(PowerState.Suspend, true, false); }
            foreach (Form f in Application.OpenForms) { f.Close(); }
            Application.Exit();
        }
        public void AlreadyRunning()
        {
            Process current = Process.GetCurrentProcess();
            Process[] proceses = Process.GetProcessesByName(current.ProcessName);
            foreach (Process proces in proceses) { if (proces.Id != current.Id) { proces.Kill(); } }
        }
        public void KillChromes()
        {
            bool found = false;
            string name = "chromedriver";
            Process killing = new Process();
            killing.StartInfo.CreateNoWindow = true;
            killing.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            killing.StartInfo.WorkingDirectory = @"C:\Users\danie\Pulpit\Other";
            killing.StartInfo.FileName = "killChromes.lnk";

            foreach (Process proces in Process.GetProcesses())
            {
                if (proces.ProcessName.Contains(name) && !found)
                { found = true; killing.Start(); }
            }
        }
    }
}