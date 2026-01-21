using FutoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutoApp.Controllers
{
    internal class Logic
    {
        public static List<Runner> felhasznaloadatok = new List<Runner>();
        public static List<Training> edzesadatok = new List<Training>();
        public static bool bejelentkezve = false;
        public static int bejelentkezettlistapozicio = 0;
        public static string bejelentkezettnev = "";
        static void felhasznalobetoltes()
        {
            felhasznaloadatok.Clear();
            foreach (var v in File.ReadAllLines("Felhasznalok.txt"))
            {
                string[] adatok = v.Split(';');
                Runner futohozzaadas1 = new Runner(adatok[0], adatok[1], int.Parse(adatok[2]), int.Parse(adatok[3]), int.Parse(adatok[4]), TimeSpan.Parse(adatok[5]));
                felhasznaloadatok.Add(futohozzaadas1);

            }
        }
        static void edzesadatokbetoltes()
        {
            edzesadatok.Clear();
            string nev = bejelentkezettnev;
            foreach (var v1 in File.ReadAllLines($"{nev}felhasznalo.txt"))
            {
                string[] adatok2 = v1.Split(';');
                Training adathozzaadas1 = new Training(DateTime.Parse(adatok2[0]), double.Parse(adatok2[1]), TimeSpan.Parse(adatok2[2]), int.Parse(adatok2[3]));
                edzesadatok.Add(adathozzaadas1);
            }
        }


    }
}
