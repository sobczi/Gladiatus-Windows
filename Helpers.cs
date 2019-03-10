using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace Gladiatus_35
{
    public static class Helpers
    {
        public static string Type_Pack(string choose)
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
        public static string Quality_Pack(string choose)
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
        public static List<string> FindReadyObjects(IReadOnlyCollection<IWebElement> list, int categoryNumber)
        {
            string[] filtr = new string[list.Count];
            List<string> collectionReady = new List<string>();
            for (int i = 0; i < list.Count; i++)
            {
                string helperString = Convert.ToString(list.ElementAt(i).GetAttribute("data-quality"));
                filtr[i] = Convert.ToString(list.ElementAt(i).GetAttribute("data-hash"));

                if (helperString == "2" && Properties.Settings.Default.purple_selling ||
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
        public static List<string> FindReadyObjectsClass(IReadOnlyCollection<IWebElement> list, int categoryNumber)
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
                else if (helperString != "2" && helperString != "3" && helperString != "4")
                {
                    temporary_string = Convert.ToString(list.ElementAt(i).GetAttribute("class"));
                    string[] temporary_string_array = temporary_string.Split(' ');
                    temporary_string = temporary_string_array[0];
                    collectionReadyClass.Add(temporary_string);
                }
            }
            return collectionReadyClass;
        }
        public static string Switch_Backpack_Food()
        {
            switch (Properties.Settings.Default.foodBackpack)
            {
                default:
                case 0:
                    return "//a[@data-bag-number='512']";
                case 1:
                    return "//a[@data-bag-number='513']";
                case 2:
                    return "//a[@data-bag-number='514']";
                case 3:
                    return "//a[@data-bag-number='515']";
                case 4:
                    return "//a[@data-bag-number='516']";
                case 5:
                    return "//a[@data-bag-number='517']";
                case 6:
                    return "//a[@data-bag-number='518']";
                case 7:
                    return "//a[@data-bag-number='519']";
            }
        }
        public static string Switch_Backpack_Extract()
        {
            switch (Properties.Settings.Default.extractBackpack)
            {
                default:
                case 0:
                    return "//a[@data-bag-number='512']";
                case 1:
                    return "//a[@data-bag-number='513']";
                case 2:
                    return "//a[@data-bag-number='514']";
                case 3:
                    return "//a[@data-bag-number='515']";
                case 4:
                    return "//a[@data-bag-number='516']";
                case 5:
                    return "//a[@data-bag-number='517']";
                case 6:
                    return "//a[@data-bag-number='518']";
                case 7:
                    return "//a[@data-bag-number='519']";
            }
        }
        public static string Switch_Backpack_Free()
        {
            switch (Properties.Settings.Default.itemsBackpack)
            {
                default:
                case 0:
                    return "//a[@data-bag-number='512']";
                case 1:
                    return "//a[@data-bag-number='513']";
                case 2:
                    return "//a[@data-bag-number='514']";
                case 3:
                    return "//a[@data-bag-number='515']";
                case 4:
                    return "//a[@data-bag-number='516']";
                case 5:
                    return "//a[@data-bag-number='517']";
                case 6:
                    return "//a[@data-bag-number='518']";
                case 7:
                    return "//a[@data-bag-number='519']";
            }
        }
        public static string Switch_World()
        {
            int world_option = Properties.Settings.Default.worldOption;
            switch (world_option)
            {
                case 0:
                    return "Prowincja 1";
                case 1:
                    return "Prowincja 25";
                case 2:
                    return "Prowincja 34";
                case 3:
                    return "Prowincja 35";
                case 4:
                    return "Prowincja 36";
                case 5:
                    return "Prowincja 37";
                case 6:
                    return "Prowincja 38";
                case 7:
                    return "Prowincja 39";
                default:
                    return "0";
            }
        }
    }
}
