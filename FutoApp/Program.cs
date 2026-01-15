using FutoApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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
            Fomenu();
        }

        static void Fomenu()
        {
            while (true)
            {
                Console.Clear();
                WriteLineCentered("=== FUTÓ EDZÉS NAPLÓ ===");
                WriteLineCentered("1. Regisztráció");
                WriteLineCentered("2. Bejelentkezés");
                WriteLineCentered("3. Kilépés");
                WriteLineCentered("-------------------------");
                WriteCentered("Válassza ki a menüpontot: ");
                int menu = int.Parse(Console.ReadLine());
                Console.WriteLine("");
                switch (menu)
                {
                    case 1:
                        futohozzaadas();
                        break;
                    case 2:
                        bejelentkezes();
                        while (bejelentkezve == true)
                        {
                            Bejelentkezve();
                        }
                        break;
                    case 3:
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        WriteLineCentered("Érvénytelen menüpont!");
                        Console.ResetColor();
                        Console.ReadLine();
                        break;
                }
            }
        }

        static void edzeshozzaadas()
        {
            try
            {
                if (!bejelentkezve)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    WriteLineCentered("Előbb jelentkezz be!");
                    Console.ResetColor();
                    Console.ReadLine();
                    return;
                }

                Console.Clear();
                WriteLineCentered("Edzés hozzáadás: ");
                WriteCentered("Add meg a dátumot: ");
                DateTime datum = DateTime.Parse(Console.ReadLine());
                WriteCentered("Add meg a távot: ");
                Double tav = Double.Parse(Console.ReadLine());
                WriteCentered("Add meg az időtartamot: ");
                TimeSpan idotartam = TimeSpan.Parse(Console.ReadLine());
                WriteCentered("Add meg a max pulzusod: ");
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
                WriteLineCentered("=== REGISZTRÁCIÓ ===");
                WriteCentered("Add meg a felhasználóneved: ");
                string nev = Console.ReadLine();
                WriteCentered("Add meg a jelszavad: ");
                string jelszo = Console.ReadLine();
                WriteCentered("Add meg a magasságod (cm): ");
                double magassag = double.Parse(Console.ReadLine());
                WriteCentered("Add meg a testtömeged (kg): ");
                double testtomeg = double.Parse(Console.ReadLine());
                WriteCentered("Add meg a nyugalmi pulzusod: ");
                int nyugalmiPulzus = int.Parse(Console.ReadLine());
                WriteCentered("Add meg a célidőt 5 km-re (óó:pp:mp): ");
                TimeSpan celIdo = TimeSpan.Parse(Console.ReadLine());
                Console.WriteLine("");

                foreach (var v in felhasznaloadatok)
                {
                    if (v.Nev == nev)
                    {

                        WriteLineCentered("Ez a felhasználónév már létezik!");
                        Console.ReadLine();
                        return;
                    }
                }

                string osszefuzottadatok = $"{nev};{jelszo};{magassag};{testtomeg};{nyugalmiPulzus};{celIdo}\n";

                Runner futohozzaadas1 = new Runner(nev, jelszo, magassag, testtomeg, nyugalmiPulzus, celIdo);
                File.AppendAllText($"{nev}felhasznalo.txt", "");
                File.AppendAllText($"Felhasznalok.txt", osszefuzottadatok);
                bejelentkezettnev = nev;
                futoadatok.Add(futohozzaadas1);

                Console.ForegroundColor = ConsoleColor.Green;
                WriteLineCentered("Sikeres regisztráció!");
                Console.ResetColor();
            }
        }

        static void bejelentkezes()
        {
            try
            {
                Console.Clear();
                WriteLineCentered("=== BEJELENTKEZÉS ===");
                WriteCentered("Add meg a felhasználóneved: ");
                string nev = Console.ReadLine();
                WriteCentered("Add meg a jelszavad: ");
                string jelszo = Console.ReadLine();
                Console.WriteLine("");

                bejelentkezve = false;

                if (nev == "admin" && jelszo == "admin")
                {
                    Admin();
                    return;
                }

                for (int i = 0; i < futoadatok.Count; i++)
                {
                    if (felhasznaloadatok[i].Nev == nev && felhasznaloadatok[i].Jelszo == jelszo)
                    {
                        bejelentkezve = true;
                        bejelentkezettlistapozicio = i;
                        bejelentkezettnev = nev;

                        Console.ForegroundColor = ConsoleColor.Green;
                        WriteLineCentered("Sikeres bejelentkezés!");
                        Console.ResetColor();
                        Console.ReadLine();
                        return;
                    }
                }
                Console.ForegroundColor = ConsoleColor.Red;
                WriteLineCentered("Sikertelen bejelentkezés!");
                Console.ResetColor();
                Console.ReadLine();
            }
            catch { }
        }

        static void Bejelentkezve()
        {
            Console.Clear();
            WriteLineCentered("=== FELHASZNÁLÓI MENÜ ===");
            WriteLineCentered("1. Edzés hozzáadás");
            WriteLineCentered("2. Edzések megtekintése");
            WriteLineCentered("3. Kijelentkezés");
            WriteLineCentered("-------------------------");
            WriteCentered("Válassza ki a menüpontot: ");
            int menu = int.Parse(Console.ReadLine());
            Console.WriteLine("");
            switch (menu)
            {
                case 1:
                    edzeshozzaadas();
                    break;
                case 2:
                    // Edzések megtekintése
                    break;
                case 3:
                    bejelentkezve = false;
                    bejelentkezettlistapozicio = 0;
                    bejelentkezettnev = "";
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    WriteLineCentered("Érvénytelen menüpont!");
                    Console.ResetColor();
                    Console.ReadLine();
                    return;
            }
        }

        static void Admin()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            WriteLineCentered("=== ADMIN MENÜ ===");
            WriteLineCentered("1. Felhasználók listázása");
            WriteLineCentered("2. Felhasználó módosítása");
            WriteLineCentered("3. Felhasználó törlése");
            WriteLineCentered("4. Vissza a főmenübe");
            WriteLineCentered("-------------------------");
            WriteCentered("Válassza ki a menüpontot: ");
            int menu = int.Parse(Console.ReadLine());
            Console.WriteLine("");
            switch (menu)
            {
                case 1:
                    // Felhasználók listázása
                    break;
                case 2:
                    // Felhasználó módosítása
                    break;
                case 3:
                    // Felhasználó törlése
                    break;
                case 4:
                    return;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    WriteLineCentered("Érvénytelen menüpont!");
                    Console.ResetColor();
                    Console.ReadLine();
                    return;
            }
            catch { }
        }

        public static void WriteLineCentered(string text)
        {
            int width = Console.WindowWidth;
            int leftPadding = (width - text.Length) / 2;
            if (leftPadding < 0)
            {
                leftPadding = 0;
            }
            Console.WriteLine(new string(' ', leftPadding) + text);
        }
        public static void WriteCentered(string text)
        {
            int width = Console.WindowWidth;
            int leftPadding = (width - text.Length) / 2;
            if (leftPadding < 0)
            {
                leftPadding = 0;
            }
            Console.Write(new string(' ', leftPadding) + text);
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

