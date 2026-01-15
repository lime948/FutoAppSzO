using FutoApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace FutoApp
{
    internal class Program
    {

        public static List<Training> edzesadatok = new List<Training>();
        public static List<Runner> felhasznaloadatok = new List<Runner>();
        public static bool bejelentkezve = false;
        public static int bejelentkezettlistapozicio = 0;
        public static string bejelentkezettnev = "";

        static void Main(string[] args)
        {
            listafeltoltes();
            futohozzaadas();
            bejelentkezes();
            if (bejelentkezve)
            {
                edzeshozzaadas();
            }

            
        }

        static void edzeshozzaadas()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Edzés hozzáadás: ");
                Console.Write("Add meg a dátumot (éééé-hh-nn):");
                DateTime datum = DateTime.Parse(Console.ReadLine());
                Console.Write("Add meg a távot (km):");
                Double tav = Double.Parse(Console.ReadLine());
                Console.Write("Add meg az időtartamot (óó:pp:mp):");
                TimeSpan idotartam = TimeSpan.Parse(Console.ReadLine());
                Console.Write("Add meg a max pulzusod:");
                int maxpulzus = int.Parse(Console.ReadLine());

                string osszefuzottadatok = $"{datum};{tav};{idotartam};{maxpulzus}\n";

                Training adathozzaadas1 = new Training(datum, tav, idotartam, maxpulzus);
                edzesadatok.Add(adathozzaadas1);

                File.AppendAllText($"{bejelentkezettnev}felhasznalo.txt", osszefuzottadatok);
            }
            catch 
            {
                Console.WriteLine("Hibás input! Enter a tovább lépéshez!");
                Console.ReadLine();
            }
        }

        static void futohozzaadas()
        {
            try
            {
                int darab = felhasznaloadatok.Count;
                Console.Clear();
                Console.WriteLine("Futó hozzáadás: ");
                Console.Write("Add meg a felhasználóneved:");
                string nev = Console.ReadLine();
                Console.Write("Add meg a jelszavad:");
                string jelszo = Console.ReadLine();
                Console.Write("Add meg a magasságod (cm):");
                double magassag = double.Parse(Console.ReadLine());
                Console.Write("Add meg a testtömeged (kg):");
                double testtomeg = double.Parse(Console.ReadLine());
                Console.Write("Add meg a nyugalmi pulzusod:");
                int nyugalmiPulzus = int.Parse(Console.ReadLine());
                Console.Write("Add meg a célidőt 5 km-re (óó:pp:mp):");
                TimeSpan celIdo = TimeSpan.Parse(Console.ReadLine());

                foreach (var v in felhasznaloadatok)
                {
                    if (v.Nev == nev)
                    {
                        Console.WriteLine("Ez a felhasználónév már létezik!");
                        Console.ReadLine();
                        return;
                    }
                }

                string osszefuzottadatok = $"{nev};{jelszo};{magassag};{testtomeg};{nyugalmiPulzus};{celIdo}\n";

                Runner futohozzaadas1 = new Runner(nev, jelszo, magassag, testtomeg, nyugalmiPulzus, celIdo);
                File.AppendAllText($"{nev}felhasznalo.txt", "");
                File.AppendAllText($"Felhasznalok.txt", osszefuzottadatok);
                bejelentkezettnev = nev;
                felhasznaloadatok.Add(futohozzaadas1);
            }
            catch
            {
                Console.WriteLine("Hibás input! Enter a tovább lépéshez!");
                Console.ReadLine();
            }
        }

        static void bejelentkezes()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Bejelentkezés");
                Console.Write("Add meg a felhasználóneved: ");
                string nev = Console.ReadLine();
                Console.Write("Add meg a jelszavad: ");
                string jelszo = Console.ReadLine();

                bejelentkezve = false;

                for (int i = 0; i < felhasznaloadatok.Count; i++)
                {
                    if (felhasznaloadatok[i].Nev == nev && felhasznaloadatok[i].Jelszo == jelszo)
                    {
                        bejelentkezve = true;
                        bejelentkezettlistapozicio = i;
                        bejelentkezettnev = nev;

                        Console.WriteLine("Sikeres bejelentkezés!");
                        Console.ReadLine();
                        return;
                    }
                }

                Console.WriteLine("Sikertelen bejelentkezés!");
                Console.ReadLine();
            }
            catch
            {
                Console.WriteLine("Hibás input! Enter a tovább lépéshez!");
                Console.ReadLine();
            }
        }

        static void listafeltoltes()
        {
            foreach (var v in File.ReadAllLines("Felhasznalok.txt"))
            {
                string[] adatok = v.Split(';');
                Runner futohozzaadas1 = new Runner(adatok[0], adatok[1], int.Parse(adatok[2]), int.Parse(adatok[3]), int.Parse(adatok[4]), TimeSpan.Parse(adatok[5]));
                felhasznaloadatok.Add(futohozzaadas1);
                string nev = adatok[0];
                foreach (var v1 in File.ReadAllLines($"{nev}felhasznalo.txt"))
                {
                    string[] adatok2 = v1.Split(';');
                    Training adathozzaadas1 = new Training(DateTime.Parse(adatok2[0]), double.Parse(adatok2[1]), TimeSpan.Parse(adatok2[2]), int.Parse(adatok2[3]));
                    edzesadatok.Add(adathozzaadas1);
                }
            }
        }
    }
}

