using FutoApp.Models;
using FutoApp.Controller;
using FutoApp.View;
using System;
using System.Collections.Generic;
using System.IO;

namespace FutoApp
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Fomenu();
        }

        /// <summary>
        /// A főmenü megjelenítése és a navigáció kezelése (Regisztráció, Bejelentkezés, Kilépés).
        /// </summary>
        static void Fomenu()
        {
            Kontroller.felhasznalobetoltes();
            while (true)
            {
                Console.Clear();
                Ui.WriteLineCentered("=== FUTÓ EDZÉS NAPLÓ ===");
                Ui.WriteLineCentered("1. Regisztráció");
                Ui.WriteLineCentered("2. Bejelentkezés");
                Ui.WriteLineCentered("3. Kilépés");
                Ui.WriteLineCentered("-------------------------");
                Ui.WriteCentered("Válassza ki a menüpontot: ");
                string menu = Console.ReadLine();
                Console.WriteLine("");
                switch (menu)
                {
                    case "1":
                        Regisztralas();

                        break;
                    case "2":
                        Bejelentkezes();
                        while (Kontroller.bejelentkezve == true)
                        {
                            Bejelentkezve();
                        }
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        if (menu == "")
                        {
                            Ui.WriteError("Nem adott meg menüpontot!");
                            Console.ReadLine();
                        }
                        else
                        {

                            Ui.WriteError("Nincs ilyen menüpont!");
                            Console.ReadLine();
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Új felhasználó regisztrálása, adatok bekérése és fájlba mentése.
        /// </summary>
        static void Regisztralas()
        {
            try
            {
                int darab = Kontroller.felhasznaloadatok.Count;
                Console.Clear();
                Ui.WriteLineCentered("=== REGISZTRÁCIÓ ===");
                Ui.WriteCentered("Add meg a felhasználóneved: ");
                string nev = Console.ReadLine();
                Ui.WriteCentered("Add meg a jelszavad: ");
                string jelszo = Console.ReadLine();
                Ui.WriteCentered("Add meg a magasságod (cm): ");
                double magassag = double.Parse(Console.ReadLine());
                Ui.WriteCentered("Add meg a testtömeged (kg): ");
                double testtomeg = double.Parse(Console.ReadLine());
                Ui.WriteCentered("Add meg a nyugalmi pulzusod: ");
                int nyugalmiPulzus = int.Parse(Console.ReadLine());
                Ui.WriteCentered("Add meg a célidőt 5 km-re (óó:pp:mp): ");
                TimeSpan celIdo = TimeSpan.Parse(Console.ReadLine());
                Console.WriteLine("");

                foreach (var v in Kontroller.felhasznaloadatok)
                {
                    if (v.Nev == nev)
                    {
                        Ui.WriteError("Ez a felhasználónév már létezik!");
                        Console.ReadLine();
                        return;
                    }
                }

                string osszefuzottadatok = $"{nev};{jelszo};{magassag};{testtomeg};{nyugalmiPulzus};{celIdo}\n";

                Runner futohozzaadas1 = new Runner(nev, jelszo, magassag, testtomeg, nyugalmiPulzus, celIdo);
                File.AppendAllText($"{nev}felhasznalo.txt", "");
                File.AppendAllText($"Felhasznalok.txt", osszefuzottadatok);

                Kontroller.felhasznaloadatok.Add(futohozzaadas1);

                Ui.WriteSuccess("Sikeres regisztráció!");
                Console.ReadLine();
            }
            catch
            {
                Ui.WriteWarning("Hibás input! Nyomj entert a tovább lépéshez!");
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Felhasználó vagy admin bejelentkeztetése. Ellenőrzi a nevet és jelszót.
        /// </summary>
        static void Bejelentkezes()
        {
            //try
            //{
                Console.Clear();
                Ui.WriteLineCentered("=== BEJELENTKEZÉS ===");
                Ui.WriteCentered("Add meg a felhasználóneved: ");
                string nev = Console.ReadLine();
                Ui.WriteCentered("Add meg a jelszavad: ");
                string jelszo = Console.ReadLine();
                Kontroller.bejelentkezettnev = nev;
                Kontroller.edzesadatokbetoltes();
                Console.WriteLine("");

                Kontroller.bejelentkezve = false;

                if (nev == "admin" && jelszo == "admin")
                {
                    Ui.WriteAdmin("Sikeres bejelentkezés adminként!");
                    Console.ReadLine();
                    Admin();
                    return;
                }
                else
                {
                    for (int i = 0; i < Kontroller.felhasznaloadatok.Count; i++)
                    {
                        if (Kontroller.felhasznaloadatok[i].Nev == nev && Kontroller.felhasznaloadatok[i].Jelszo == jelszo)
                        {
                            Kontroller.bejelentkezve = true;
                            Kontroller.bejelentkezettlistapozicio = i;
                            Kontroller.bejelentkezettnev = nev;

                            Ui.WriteSuccess("Sikeres bejelentkezés!");
                            Console.ReadLine();
                            return;
                        }
                    }
                    Ui.WriteError("Sikertelen bejelentkezés!");
                    Console.ReadLine();
                }
            //}
            //catch
            //{
            //    Ui.WriteLineCentered("Hibás input! Nyomj entert a tovább lépéshez!");
            //    Console.ReadLine();
            //}
        }

        /// <summary>
        /// A bejelentkezett felhasználó menüje (Edzések kezelése, beállítások).
        /// </summary>
        static void Bejelentkezve()
        {
            Console.Clear();
            Ui.WriteLineCentered("=== FELHASZNÁLÓI MENÜ ===");
            Ui.WriteLineCentered("1. Edzés hozzáadása");
            Ui.WriteLineCentered("2. Edzések megtekintése");
            Ui.WriteLineCentered("3. Edzés módosítása");
            Ui.WriteLineCentered("4. Edzés törlése");
            Ui.WriteLineCentered("5. Beállítások");
            Ui.WriteLineCentered("6. Kijelentkezés");
            Ui.WriteLineCentered("-------------------------");
            Ui.WriteCentered("Válassza ki a menüpontot: ");
            string menu = Console.ReadLine();
            Console.WriteLine("");
            switch (menu)
            {
                case "1":
                    EdzesHozzaadas();
                    break;
                case "2":
                    EdzesMegtekintese();
                    break;
                case "3":
                    EdzesModositasa();
                    break;
                case "4":
                    EdzesTorlese();
                    break;
                case "5":
                    Beallitasok();
                    break;
                case "6":
                    Kontroller.bejelentkezve = false;
                    Kontroller.bejelentkezettlistapozicio = 0;
                    Kontroller.bejelentkezettnev = "";
                    break;
                default:
                    if (menu == "")
                    {
                        Ui.WriteError("Nem adott meg menüpontot!");
                        Console.ReadLine();
                    }
                    else
                    {
                        Ui.WriteError("Nincs ilyen menüpont!");
                        Console.ReadLine();
                    }
                    return;
            }
        }

        /// <summary>
        /// Új edzésadatok bekérése és hozzáadása a listához és a felhasználó fájljához.
        /// </summary>
        static void EdzesHozzaadas()
        {
            try
            {
                if (!Kontroller.bejelentkezve)
                {
                    Ui.WriteError("Előbb jelentkezz be!");
                    Console.ReadLine();
                    return;
                }

                Console.Clear();
                Ui.WriteLineCentered("=== EDZÉS HOZZÁADÁSA ===");
                Ui.WriteCentered("Add meg a dátumot (éééé-hh-nn): ");
                DateTime datum = DateTime.Parse(Console.ReadLine());
                Ui.WriteCentered("Add meg a távot (km): ");
                Double tav = Double.Parse(Console.ReadLine());
                Ui.WriteCentered("Add meg az időtartamot (óó:pp:mm): ");
                TimeSpan idotartam = TimeSpan.Parse(Console.ReadLine());
                Ui.WriteCentered("Add meg a max pulzusod (pl: 120): ");
                int maxpulzus = int.Parse(Console.ReadLine());

                string osszefuzottadatok = $"{datum};{tav};{idotartam};{maxpulzus}\n";

                Training adathozzaadas1 = new Training(datum, tav, idotartam, maxpulzus);
                Kontroller.edzesadatok.Add(adathozzaadas1);

                File.AppendAllText($"{Kontroller.bejelentkezettnev}felhasznalo.txt", osszefuzottadatok);
                Ui.WriteSuccess("Edzés sikeresen hozzáadva");
                Console.ReadLine();

            }
            catch
            {
                Ui.WriteWarning("Hibás input! Enter a tovább lépéshez!");
                Console.ReadLine();
            }
        }

        /// <summary>
        /// A rögzített edzések listázása és összesített statisztikák (táv, idő) megjelenítése.
        /// </summary>
        static void EdzesMegtekintese()
        {
            Console.Clear();
            Ui.WriteLineCentered("=== ÖSSZES EDZÉS ===");
            int szamlalo = 1;
            double ossztav = 0;
            TimeSpan osszido = new TimeSpan();
            foreach (var edzes in Kontroller.edzesadatok)
            {
                ossztav += edzes.Tav;
                osszido += edzes.Idotartam;
            }
            foreach (var edzes in Kontroller.edzesadatok)
            {
                Ui.WriteLineCentered($"{szamlalo}. Dátum: {edzes.Datum.ToShortDateString()}, Táv: {edzes.Tav} km, Időtartam: {edzes.Idotartam}, Max pulzus: {edzes.MaxPulzus}");
                szamlalo++;
            }
            Ui.WriteLineCentered($"Össztáv: {ossztav} km");
            Ui.WriteLineCentered($"Összidő: {osszido.Days} nap {osszido.Hours} óra {osszido.Minutes} perc {osszido.Seconds} másodperc");
            Console.WriteLine("");
            Ui.WriteCentered("Nyomj entert a visszatéréshez!");
            Console.ReadLine();
        }

        /// <summary>
        /// Meglévő edzés kiválasztása és adatainak módosítása, fájl frissítése.
        /// </summary>
        static void EdzesModositasa()
        {
            try
            {
                Console.Clear();
                Ui.WriteLineCentered("=== EDZÉSEK MÓDOSÍTÁSA ===");
                Ui.WriteLineCentered("");
                int szamlalo = 1;
                foreach (var edzes in Kontroller.edzesadatok)
                {
                    Ui.WriteLineCentered($"{szamlalo}. Dátum: {edzes.Datum.ToShortDateString()}, Táv: {edzes.Tav} km, Időtartam: {edzes.Idotartam}, Max pulzus: {edzes.MaxPulzus}");
                    szamlalo++;
                }

                Ui.WriteCentered("Add meg a módosítandó adat sorszámát: ");
                int sorszam = int.Parse(Console.ReadLine());
                Ui.WriteCentered("Add meg a dátumot (éééé-hh-nn): ");
                DateTime datum = DateTime.Parse(Console.ReadLine());
                Ui.WriteCentered("Add meg a távot (km): ");
                Double tav = Double.Parse(Console.ReadLine());
                Ui.WriteCentered("Add meg az időtartamot (óó:pp:mm): ");
                TimeSpan idotartam = TimeSpan.Parse(Console.ReadLine());
                Ui.WriteCentered("Add meg a max pulzusod (pl: 120): ");
                int maxpulzus = int.Parse(Console.ReadLine());

                string osszefuzottadatok = $"{datum};{tav};{idotartam};{maxpulzus}\n";

                Kontroller.edzesadatok.RemoveAt(sorszam-1);
                Training adathozzaadas1 = new Training(datum, tav, idotartam, maxpulzus);
                Kontroller.edzesadatok.Insert(sorszam-1, adathozzaadas1);

                
                foreach (var v in Kontroller.felhasznaloadatok)
                {
                    if (v.Nev == Kontroller.bejelentkezettnev)
                    {
                        File.WriteAllText($"{Kontroller.bejelentkezettnev}felhasznalo.txt", "");
                        File.AppendAllText($"{Kontroller.bejelentkezettnev}felhasznalo.txt", $"{v.Nev};{v.Jelszo};{v.Magassag};{v.Testtomeg};{v.NyugalmiPulzus};{v.CelIdo}\n");
                        foreach (var v1 in Kontroller.edzesadatok)
                        {
                            File.AppendAllText($"{Kontroller.bejelentkezettnev}felhasznalo.txt", $"{v1.Datum};{v1.Tav};{v1.Idotartam};{v1.MaxPulzus}\n");
                        }
                    }
                }
                Ui.WriteSuccess("Sikeres módosítás!");
                Console.ReadLine();
            }
            catch
            {
                Ui.WriteWarning("Hibás input! Enter a tovább lépéshez!");
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Meglévő edzés kiválasztása és törlése a listából és a fájlból.
        /// </summary>
        static void EdzesTorlese()
        {
            try
            {
                Console.Clear();
                Ui.WriteLineCentered("=== EDZÉS TÖRLÉSE ===");
                Ui.WriteLineCentered("");
                int szamlalo = 1;
                foreach (var edzes in Kontroller.edzesadatok)
                {
                    Ui.WriteLineCentered($"{szamlalo}. Dátum: {edzes.Datum.ToShortDateString()}, Táv: {edzes.Tav} km, Időtartam: {edzes.Idotartam}, Max pulzus: {edzes.MaxPulzus}");
                    szamlalo++;
                }

                Ui.WriteCentered("Add meg a törlendő adat sorszámát: ");
                int sorszam = int.Parse(Console.ReadLine());

                Kontroller.edzesadatok.RemoveAt(sorszam - 1);

                foreach (var v in Kontroller.felhasznaloadatok)
                {
                    if (v.Nev == Kontroller.bejelentkezettnev)
                    {
                        File.WriteAllText($"{Kontroller.bejelentkezettnev}felhasznalo.txt", "");
                        File.AppendAllText($"{Kontroller.bejelentkezettnev}felhasznalo.txt", $"{v.Nev};{v.Jelszo};{v.Magassag};{v.Testtomeg};{v.NyugalmiPulzus};{v.CelIdo}\n");
                        foreach (var v1 in Kontroller.edzesadatok)
                        {
                            File.AppendAllText($"{Kontroller.bejelentkezettnev}felhasznalo.txt", $"{v1.Datum};{v1.Tav};{v1.Idotartam};{v1.MaxPulzus}\n");
                        }
                    }
                }
                Ui.WriteSuccess("Edzés sikeresen törölve!");
                Console.ReadLine();
            }
            catch
            {
                Ui.WriteWarning("Hibás input! Enter a tovább lépéshez!");
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Beállítások almenü (Háttérszín, Betűszín, Adatmodosítás).
        /// </summary>
        static void Beallitasok()
        {
            Console.Clear();
            Ui.WriteLineCentered("=== BEÁLLÍTÁSOK ===");
            Ui.WriteLineCentered("1. Háttér módosítása");
            Ui.WriteLineCentered("2. Betűszín módosítása");
            Ui.WriteLineCentered("3. Személyes adatok módosítása");
            Ui.WriteLineCentered("4. Vissza");
            Ui.WriteLineCentered("-------------------------");
            Ui.WriteCentered("Válassza ki a menüpontot: ");
            string menu = Console.ReadLine();
            switch (menu)
            {
                case "1":
                    HatterMod();
                    break;
                case "2":
                    Ui.BetuszinMod();
                    break;
                case "3":
                    Adatmodositas();
                    break;
                case "4":
                    Bejelentkezve();
                    break;
                default:
                    if (menu == "")
                    {
                        Ui.WriteError("Nem adott meg menüpontot!");
                        Console.ReadLine();
                    }
                    else
                    {
                        Ui.WriteError("Nincs ilyen menüpont!");
                        Console.ReadLine();
                    }
                    return;
            }
        }

        

        /// <summary>
        /// A konzol háttérszínének beállítása a felhasználó választása alapján.
        /// </summary>
        static int HatterMod()
        {
            Console.Clear();
            Ui.WriteLineCentered("=== HÁTTÉRSZÍN MÓDOSÍTÁSA ===");
            Console.ForegroundColor = ConsoleColor.Black;
            Ui.WriteLineCentered("1. Fekete");
            Console.ForegroundColor = ConsoleColor.White;
            Ui.WriteLineCentered("2. Fehér");
            Console.ForegroundColor = ConsoleColor.Gray;
            Ui.WriteLineCentered("3. Szürke");
            Console.ForegroundColor = ConsoleColor.Red;
            Ui.WriteLineCentered("4. Piros");
            Console.ForegroundColor = ConsoleColor.Green;
            Ui.WriteLineCentered("5. Zöld");
            Console.ForegroundColor = ConsoleColor.Blue;
            Ui.WriteLineCentered("6. Kék");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Ui.WriteLineCentered("7. Cián");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Ui.WriteLineCentered("8. Sárga");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Ui.WriteLineCentered("9. Magenta");
            Console.ForegroundColor = ConsoleColor.White;
            Ui.WriteLineCentered("");
            Ui.WriteCentered("Adja meg a kivánt szín számát: ");
            int valasztas = int.Parse(Console.ReadLine());

            ConsoleColor ujHatter = Ui.ValasztasToColor(valasztas);

            if (ujHatter == Console.ForegroundColor)
            {
                Ui.WriteWarning("A háttér és a betűszín nem lehet ugyanaz!");
                Console.ReadLine();
                return -1;
            }

            Console.BackgroundColor = ujHatter;
            return valasztas;
        }

       

        /// <summary>
        /// Személyes adatok kezelésére szolgáló almenü.
        /// </summary>
        static void Adatmodositas()
        {
            Console.Clear();
            Ui.WriteLineCentered("=== SZEMÉLYES ADATOK MÓDOSÍTÁSA ===");
            Ui.WriteLineCentered("1. Adatok megtekintése");
            Ui.WriteLineCentered("2. Adatok módosítása");
            Ui.WriteLineCentered("3. Jelszó módosítása");
            Ui.WriteLineCentered("4. Vissza");
            Ui.WriteLineCentered("-------------------------");
            Ui.WriteCentered("Válassza ki a menüpontot: ");
            string menu = Console.ReadLine();
            switch (menu)
            {
                case "1":
                    AdatokMegtekintese();
                    break;
                case "2":
                    Adatok();
                    break;
                case "3":
                    Jelszo();
                    break;
                case "4":
                    Beallitasok();
                    break;
                default:
                    if (menu == "")
                    {
                        Ui.WriteError("Nem adott meg menüpontot!");
                        Console.ReadLine();
                    }
                    else
                    {
                        Ui.WriteError("Nincs ilyen menüpont!");
                        Console.ReadLine();
                    }
                    return;
            }

            /// <summary>
            /// Jelenlegi felhasználó adatainak megjelenítése.
            /// </summary>
            void AdatokMegtekintese()
            {
                Console.Clear();
                Ui.WriteLineCentered("=== SZEMÉLYES ADATOK ===");
                Ui.WriteLineCentered($"Név: {Kontroller.felhasznaloadatok[Kontroller.bejelentkezettlistapozicio].Nev}");
                Ui.WriteLineCentered($"Magasság: {Kontroller.felhasznaloadatok[Kontroller.bejelentkezettlistapozicio].Magassag} cm");
                Ui.WriteLineCentered($"Testtömeg: {Kontroller.felhasznaloadatok[Kontroller.bejelentkezettlistapozicio].Testtomeg} kg");
                Ui.WriteLineCentered($"Nyugalmi pulzus: {Kontroller.felhasznaloadatok[Kontroller.bejelentkezettlistapozicio].NyugalmiPulzus}");
                Ui.WriteLineCentered($"Célidő 5 km-re: {Kontroller.felhasznaloadatok[Kontroller.bejelentkezettlistapozicio].CelIdo}");
                Console.WriteLine("");
                Ui.WriteCentered("Nyomj entert a visszatéréshez!");
                Console.ReadLine();
            }

            /// <summary>
            /// Személyes adatok (magasság, súly, pulzus, célidő) módosítása és mentése fájlba.
            /// </summary>
            void Adatok()
            {
                Console.Clear();
                Ui.WriteCentered("Add meg az új magasságot (cm): ");
                double magassag = double.Parse(Console.ReadLine());
                Ui.WriteCentered("Add meg az új testtömeget (kg): ");
                double testtomeg = double.Parse(Console.ReadLine());
                Ui.WriteCentered("Add meg az új nyugalmi pulzust: ");
                int nyugalmiPulzus = int.Parse(Console.ReadLine());
                Ui.WriteCentered("Add meg az új célidőt 5 km-re (óó:pp:mp): ");
                TimeSpan celIdo = TimeSpan.Parse(Console.ReadLine());
                Kontroller.felhasznaloadatok[Kontroller.bejelentkezettlistapozicio].Magassag = magassag;
                Kontroller.felhasznaloadatok[Kontroller.bejelentkezettlistapozicio].Testtomeg = testtomeg;
                Kontroller.felhasznaloadatok[Kontroller.bejelentkezettlistapozicio].NyugalmiPulzus = nyugalmiPulzus;
                Kontroller.felhasznaloadatok[Kontroller.bejelentkezettlistapozicio].CelIdo = celIdo;
                List<string> ujadatok = new List<string>();
                foreach (var f in Kontroller.felhasznaloadatok)
                {
                    ujadatok.Add($"{f.Nev};{f.Jelszo};{f.Magassag};{f.Testtomeg};{f.NyugalmiPulzus};{f.CelIdo}");
                }
                File.WriteAllLines("Felhasznalok.txt", ujadatok);
                Ui.WriteSuccess("Sikeres módosítás!");
                Console.ReadLine();
            }

            /// <summary>
            /// Jelszó módosítása és mentése.
            /// </summary>
            void Jelszo()
            {
                Console.Clear();
                Ui.WriteCentered("Add meg az új jelszót: ");
                string jelszo = Console.ReadLine();
                Kontroller.felhasznaloadatok[Kontroller.bejelentkezettlistapozicio].Jelszo = jelszo;
                List<string> ujjelszo = new List<string>();
                foreach (var f in Kontroller.felhasznaloadatok)
                {
                    ujjelszo.Add($"{f.Jelszo}");
                }
                File.WriteAllLines("Felhasznalok.txt", ujjelszo);
                Ui.WriteSuccess("Sikeres módosítás!");
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Adminisztrátori menü kezelése.
        /// </summary>
        static void Admin()
        {
            Console.Clear();
            Ui.WriteAdmin("=== ADMIN MENÜ ===");
            Ui.WriteLineCentered("1. Felhasználók listázása");
            Ui.WriteLineCentered("2. Felhasználó módosítása");
            Ui.WriteLineCentered("3. Felhasználó törlése");
            Ui.WriteLineCentered("4. Vissza a főmenübe");
            Ui.WriteLineCentered("-------------------------");
            Ui.WriteCentered("Válassza ki a menüpontot: ");
            string menu = Console.ReadLine();
            Console.WriteLine("");
            switch (menu)
            {
                case "1":
                    FelhasznalokListazasa();
                    break;
                case "2":
                    FelhasznaloModositasa();
                    break;
                case "3":
                    FelhasznaloTorlese();
                    break;
                case "4":
                    return;
                default:
                    if (menu == "")
                    {
                        Ui.WriteError("Nem adott meg menüpontot!");
                        Console.ReadLine();
                    }
                    else
                    {
                        Ui.WriteError("Nincs ilyen menüpont!");
                        Console.ReadLine();
                    }
                    return;
            }

            /// <summary>
            /// Az összes regisztrált felhasználó adatainak listázása.
            /// </summary>
            void FelhasznalokListazasa()
            {
                Console.Clear();
                try
                {
                    Ui.WriteLineCentered("=== FELHASZNÁLÓK ===");
                    Ui.WriteLineCentered("--------------------------");
                    if (Kontroller.felhasznaloadatok.Count == 0)
                    {
                        Ui.WriteLineCentered("Nincsenek felhasználók!");
                    }
                    else
                    {
                        foreach (var felhasznalo in Kontroller.felhasznaloadatok)
                        {
                            Ui.WriteLineCentered($"Név: {felhasznalo.Nev}, Jelszó: {felhasznalo.Jelszo}, Magasság: {felhasznalo.Magassag} cm, Testtömeg: {felhasznalo.Testtomeg} kg, Nyugalmi pulzus: {felhasznalo.NyugalmiPulzus}, Célidő 5 km-re: {felhasznalo.CelIdo}");
                        }
                    }
                    Ui.WriteLineCentered("--------------------------");
                    Console.WriteLine("");
                    Ui.WriteCentered("Nyomj entert a visszatéréshez!");
                    Console.ReadLine();
                    Admin();
                }
                catch
                {
                    Ui.WriteWarning("Hibás input! Nyomj entert a tovább lépéshez!");
                    Console.ReadLine();
                }
            }

            /// <summary>
            /// Felhasználó adatainak módosítása adminisztrátori joggal.
            /// </summary>
            void FelhasznaloModositasa()
            {
                Console.Clear();
                try
                {
                    Ui.WriteLineCentered("=== FELHASZNÁLÓ MÓDOSÍTÁSA ===");
                    Ui.WriteCentered("Add meg a módosítandó felhasználó nevét: ");
                    string nev = Console.ReadLine();
                    if (nev == null)
                    {       
                        Ui.WriteError("Nem adott meg felhasználót!");
                    }
                    for (int i = 0; i < Kontroller.felhasznaloadatok.Count; i++)
                    {
                        if (nev == Kontroller.felhasznaloadatok[i].Nev)
                        {
                            Ui.WriteCentered("Adja meg az új jelszót: ");
                            string jelszo = Console.ReadLine();
                            Ui.WriteCentered("Adja meg az új magasságot (cm): ");
                            double magassag = double.Parse(Console.ReadLine());
                            Ui.WriteCentered("Adja meg az új testtömeget (kg): ");
                            double testtomeg = double.Parse(Console.ReadLine());
                            Ui.WriteCentered("Adja meg az új nyugalmi pulzust: ");
                            int nyugalmiPulzus = int.Parse(Console.ReadLine());
                            Ui.WriteCentered("Adja meg az új célidőt 5 km-re (óó:pp:mp): ");
                            TimeSpan celIdo = TimeSpan.Parse(Console.ReadLine());
                            Kontroller.felhasznaloadatok[i].Jelszo = jelszo;
                            Kontroller.felhasznaloadatok[i].Magassag = magassag;
                            Kontroller.felhasznaloadatok[i].Testtomeg = testtomeg;
                            Kontroller.felhasznaloadatok[i].NyugalmiPulzus = nyugalmiPulzus;
                            Kontroller.felhasznaloadatok[i].CelIdo = celIdo;
                            List<string> ujadatok = new List<string>();
                            foreach (var f in Kontroller.felhasznaloadatok)
                            {
                                ujadatok.Add($"{f.Nev};{f.Jelszo};{f.Magassag};{f.Testtomeg};{f.NyugalmiPulzus};{f.CelIdo}");
                            }
                            File.WriteAllLines("Felhasznalok.txt", ujadatok);
                            Ui.WriteSuccess("Sikeres módosítás!");
                            Console.ReadLine();
                            return;
                        }
                        else
                        {
                            Ui.WriteError("Nincs ilyen felhasználó!");
                            Console.ReadLine();
                        }
                    }
                }
                catch
                {
                    Ui.WriteWarning("Hibás input! Nyomj entert a tovább lépéshez!");
                    Console.ReadLine();
                }
            }

            /// <summary>
            /// Felhasználó és a hozzá tartozó fájl törlése adminisztrátori joggal.
            /// </summary>
            void FelhasznaloTorlese()
            {
                Console.Clear();
                try
                {
                    Ui.WriteLineCentered("=== FELHASZNÁLÓ TÖRLÉSE ===");
                    Ui.WriteCentered("Add meg a törlendő felhasználó nevét: ");
                    string nev = Console.ReadLine();
                    for (int i = 0; i < Kontroller.felhasznaloadatok.Count; i++)
                    {
                        if (nev == Kontroller.felhasznaloadatok[i].Nev)
                        {
                            Kontroller.felhasznaloadatok.RemoveAt(i);
                            List<string> ujadatok = new List<string>();
                            foreach (var f in Kontroller.felhasznaloadatok)
                            {
                                ujadatok.Add($"{f.Nev};{f.Jelszo};{f.Magassag};{f.Testtomeg};{f.NyugalmiPulzus};{f.CelIdo}");
                            }
                            File.WriteAllLines("Felhasznalok.txt", ujadatok);
                            File.Delete($"{nev}felhasznalo.txt");
                            Ui.WriteSuccess("Sikeres törlés!");
                            Console.ReadLine();
                            return;
                        }
                    }
                    Ui.WriteError("Nincs ilyen felhasználó!");
                    Console.ReadLine();
                }
                catch
                {
                    Ui.WriteWarning("Hibás input! Nyomj entert a tovább lépéshez!");
                    Console.ReadLine();
                }
            }
        }

    }
}