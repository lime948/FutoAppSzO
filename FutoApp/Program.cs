using FutoApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutoApp
{
    internal class Program
    {

        public static List<Training> edzesadatok = new List<Training>();
        public static List<Runner> futoadatok = new List<Runner>();
        public static bool bejelentkezve = false;
        public static int bejelentkezettlistapozicio = 0;
        public static string bejelentkezettnev = "";

        static void Main(string[] args)
        {
            futohozzaadas();
            bejelentkezes();
            edzeshozzaadas();
            
        }

        static void edzeshozzaadas()
        {
            try
            {
                if (!bejelentkezve)
                {
                    Console.WriteLine("Előbb jelentkezz be!");
                    Console.ReadLine();
                    return;
                }

                Console.Clear();
                Console.WriteLine("Edzés hozzáadás: ");
                Console.Write("Add mega dátumot:");
                DateTime datum = DateTime.Parse(Console.ReadLine());
                Console.Write("Add meg a távot:");
                Double tav = Double.Parse(Console.ReadLine());
                Console.Write("Add meg az időtartamot:");
                TimeSpan idotartam = TimeSpan.Parse(Console.ReadLine());
                Console.Write("Add meg a max pulzusod:");
                int maxpulzus = int.Parse(Console.ReadLine());

                string osszefuzottadatok = $"{bejelentkezettlistapozicio};{datum};{tav};{idotartam};{maxpulzus}\n";

                Training adathozzaadas1 = new Training(bejelentkezettlistapozicio, datum, tav, idotartam, maxpulzus);
                edzesadatok.Add(adathozzaadas1);

                File.AppendAllText($"{bejelentkezettnev}felhasznalo.txt", osszefuzottadatok);
            }
            catch { }
        }

        static void futohozzaadas()
        {
            try
            {
                int darab = futoadatok.Count;
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

                foreach (var v in futoadatok)
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
                File.AppendAllText($"{nev}felhasznalo.txt", osszefuzottadatok);
                bejelentkezettnev = nev;
                futoadatok.Add(futohozzaadas1);
            }
            catch { }
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

                for (int i = 0; i < futoadatok.Count; i++)
                {
                    if (futoadatok[i].Nev == nev && futoadatok[i].Jelszo == jelszo)
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
            catch { }
        }

    }
}

