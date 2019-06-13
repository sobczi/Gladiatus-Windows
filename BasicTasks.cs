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
    static class BasicTasks
    {
        public static bool Search(string path)
        {
            if (Form1.driver.FindElementsByXPath(path).Count == 0) { return false; }
            else { return true; }
        }
        public static void Click(string path)
        {
            //Random rnd = new Random();
            //int number = rnd.Next(3, 3) * 1000;
            //Thread.Sleep(number);
            for (int i = 0; i < 2; i++)
            {
                IWebElement element = Form1.driver.FindElementByXPath(path);
                try { Thread.Sleep(300); element.Click(); return; }
                catch { Refresh(); }
            }
            return;
        }
        public static void SelectElement(string xpath1, string text)
        {
            var choose = GetElement(xpath1);
            var selectElement = new SelectElement(choose);
            selectElement.SelectByText(text);
        }
        public static void MoveReleaseElement(string xpath1, string xpath2)
        {
            Thread.Sleep(250);
            Actions move = new Actions(Form1.driver);
            move.ClickAndHold(GetElement(xpath1));
            move.Release(GetElement(xpath2));
            move.Build().Perform();
        }
        public static void MoveMoveElement(string xpath1, string xpath2)
        {
            Thread.Sleep(250);
            Actions move = new Actions(Form1.driver);
            move.ClickAndHold(GetElement(xpath1));
            move.MoveToElement(GetElement(xpath2));
            move.Build().Perform();
        }
        public static void MoveMoveElement(IWebElement element, string xpath2)
        {
            //Thread.Sleep(500);
            Actions move = new Actions(Form1.driver);
            move.ClickAndHold(element);
            move.MoveToElement(GetElement(xpath2));
            move.Build().Perform();
        }
        public static void ReleaseElement(string xpath1)
        {
            Thread.Sleep(250);
            IWebElement element = GetElement(xpath1);
            Actions move = new Actions(Form1.driver);
            move.MoveToElement(element);
            move.Release(element);
            move.Build().Perform();
        }
        public static void MoveTo(string xpath1)
        {
            Thread.Sleep(250);
            Actions move = new Actions(Form1.driver);
            move.MoveToElement(GetElement(xpath1));
            move.Build().Perform();
        }
        public static void CheckEvenets()
        {
            string[] paths = new string[4];
            paths[0] = "//input[@id='linkLoginBonus']";
            paths[1] = "//a[contains(@onclick,'MAX_simplepop')]";
            paths[2] = "//*[@id='linkcancelnotification']";
            paths[3] = "//*[@id='linknotification']";

            for (int i = 0; i < paths.Length; i++)
            {
                try { var button = Form1.driver.FindElementByXPath(paths[i]); button.Click(); }
                catch { }
            }

            if (Search("//div[@id='title_infobox']"))
            {
                if (Form1.driver.FindElementByXPath("//div[@id='title_infobox']").GetAttribute("textContent") == "Prace konserwacyjne")
                { Thread.Sleep(360000); }
            }
        }
        public static IWebElement GetElement(string path)
        {
            WaitForXPath(path);
            IWebElement element = Form1.driver.FindElementByXPath(path);
            return element;
        }
        public static void WaitForXPath(string path)
        {
            int iterator = 0;
            bool found = false;

            while (iterator != 240 && !found)
            {
                if (!Search(path)) { iterator++; Thread.Sleep(500); }
                else { found = true; }
            }
        }
        public static void Refresh()
        {
            CheckEvenets();
            Form1.driver.Navigate().Refresh();
            return;
        }
        public static void Save_Exception(Exception _exception, string _server)
        {
            var time_now = DateTime.Now;
            string time_now_string = time_now.ToString("dd.MM.yyyy HH:mm:ss");
            File.AppendAllText(@"C:\Users\danie\Documents\Visual Studio 2017\Resources\Gladiatus_bots\catched_exceptions.txt",
                Environment.NewLine + Environment.NewLine + "<---------------->" + time_now_string +"<---------------->" + _server + Environment.NewLine + Convert.ToString(_exception));
        }
        public static void Exit(bool _turn_off)
        {
            if(Form1.driver != null)
            { Form1.driver.Close(); Form1.driver.Quit(); }
            if (Properties.Settings.Default.sleepModeChecked && _turn_off && Properties.Settings.Default.main_bot)
            { KillChromes(); Application.SetSuspendState(PowerState.Suspend, true, false); }
            foreach (Form f in Application.OpenForms) { f.Close(); }
            Application.Exit();
        }
        public static void AlreadyRunning()
        {
            Process current = Process.GetCurrentProcess();
            Process[] proceses = Process.GetProcessesByName(current.ProcessName);
            foreach (Process proces in proceses) { if (proces.Id != current.Id) { proces.Kill(); } }
        }
        public static void KillChromes()
        {
            bool found = false;
            string name = "chromeForm1.driver";
            Process killing = new Process();
            killing.StartInfo.CreateNoWindow = true;
            killing.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            killing.StartInfo.WorkingDirectory = @"C:\Users\danie\Desktop\Other";
            killing.StartInfo.FileName = "killChromes.lnk";

            foreach (Process proces in Process.GetProcesses())
            {
                if (proces.ProcessName.Contains(name) && !found)
                { found = true; killing.Start(); }
            }
        }
        public static int ReturnInt(string ścieżka)
        {
            int value = 0;
            for (int i = 0; i < 2; i++)
            {
                IWebElement element = BasicTasks.GetElement(ścieżka);
                try { value = Convert.ToInt32(element.GetAttribute("textContent")); return value; }
                catch { BasicTasks.Refresh(); }
            }
            return value;
        }
        public static string Return_String(string path)
        {
            bool non_stop = true;
            while (non_stop)
            {
                try
                {
                    return BasicTasks.GetElement(path).GetAttribute("textContent");
                }
                catch { }
            }
            return BasicTasks.GetElement(path).GetAttribute("textContent");
        }
        public static void Wait_For_Exit()
        {
            if (Form1.turn_off && Properties.Settings.Default.main_bot)
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

                        if (found_process)
                        {
                            break;
                        }
                    }
                } while (found_process && Form1.turn_off);
            }
            BasicTasks.Exit(Form1.turn_off);
        }
        public static void Catch_Mouse()
        {
            bool non_stop = true;
            while (non_stop)
            {
                int first_variable = Cursor.Position.X;
                Thread.Sleep(100);
                int second_variable = Cursor.Position.X;
                if (first_variable != second_variable)
                {
                    Form1.turn_off_notification = true;
                    return;
                }
            }
        }
        public static bool Check_sold(IWebElement element)
        {
            Actions move = new Actions(Form1.driver);
            move.MoveToElement(element);
            move.Build().Perform();
            if(BasicTasks.Search("//p[contains(text(),'Wskazówka')]"))
                return true;
            else
                return false;
        }

        public static bool Check_sold(string xpath)
        {
            IWebElement element = GetElement(xpath);
            Actions move = new Actions(Form1.driver);
            move.MoveToElement(element);
            move.Build().Perform();
            if (BasicTasks.Search("//p[contains(text(),'Wskazówka')]"))
                return true;
            else
                return false;
        }
    }
}