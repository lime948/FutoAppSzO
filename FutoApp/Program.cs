using FutoApp.Models;
using System;
using System.Collections.Generic;
using System.IO;

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

        /// <summary>
        /// A főmenü megjelenítése és a navigáció kezelése (Regisztráció, Bejelentkezés, Kilépés).
        /// </summary>
        static void Fomenu()
        {
            felhasznalobetoltes();
            while (true)
            {
                Console.Clear();
                WriteLineCentered("=== FUTÓ EDZÉS NAPLÓ ===");
                WriteLineCentered("1. Regisztráció");
                WriteLineCentered("2. Bejelentkezés");
                WriteLineCentered("3. Kilépés");
                WriteLineCentered("-------------------------");
                WriteCentered("Válassza ki a menüpontot: ");
                string menu = Console.ReadLine();
                Console.WriteLine("");
                switch (menu)
                {
                    case "1":
                        Regisztralas();

                        break;
                    case "2":
                        Bejelentkezes();
                        while (bejelentkezve == true)
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
                            WriteError("Nem adott meg menüpontot!");
                            Console.ReadLine();
                        }
                        else
                        {

                            WriteError("Nincs ilyen menüpont!");
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
                        WriteError("Ez a felhasználónév már létezik!");
                        Console.ReadLine();
                        return;
                    }
                }

                string osszefuzottadatok = $"{nev};{jelszo};{magassag};{testtomeg};{nyugalmiPulzus};{celIdo}\n";

                Runner futohozzaadas1 = new Runner(nev, jelszo, magassag, testtomeg, nyugalmiPulzus, celIdo);
                File.AppendAllText($"{nev}felhasznalo.txt", "");
                File.AppendAllText($"Felhasznalok.txt", osszefuzottadatok);

                felhasznaloadatok.Add(futohozzaadas1);

                WriteSuccess("Sikeres regisztráció!");
                Console.ReadLine();
            }
            catch
            {
                WriteWarning("Hibás input! Nyomj entert a tovább lépéshez!");
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Felhasználó vagy admin bejelentkeztetése. Ellenőrzi a nevet és jelszót.
        /// </summary>
        static void Bejelentkezes()
        {
            try
            {
                Console.Clear();
                WriteLineCentered("=== BEJELENTKEZÉS ===");
                WriteCentered("Add meg a felhasználóneved: ");
                string nev = Console.ReadLine();
                WriteCentered("Add meg a jelszavad: ");
                string jelszo = Console.ReadLine();
                bejelentkezettnev = nev;
                edzesadatokbetoltes();
                Console.WriteLine("");

                bejelentkezve = false;

                if (nev == "admin" && jelszo == "admin")
                {
                    WriteAdmin("Sikeres bejelentkezés adminként!");
                    Console.ReadLine();
                    Admin();
                    return;
                }
                else
                {
                    for (int i = 0; i < felhasznaloadatok.Count; i++)
                    {
                        if (felhasznaloadatok[i].Nev == nev && felhasznaloadatok[i].Jelszo == jelszo)
                        {
                            bejelentkezve = true;
                            bejelentkezettlistapozicio = i;
                            bejelentkezettnev = nev;

                            WriteSuccess("Sikeres bejelentkezés!");
                            Console.ReadLine();
                            return;
                        }
                    }
                    WriteError("Sikertelen bejelentkezés!");
                    Console.ReadLine();
                }
            }
            catch
            {
                WriteLineCentered("Hibás input! Nyomj entert a tovább lépéshez!");
                Console.ReadLine();
            }
}

        /// <summary>
        /// A bejelentkezett felhasználó menüje (Edzések kezelése, beállítások).
        /// </summary>
        static void Bejelentkezve()
        {
            Console.Clear();
            WriteLineCentered("=== FELHASZNÁLÓI MENÜ ===");
            WriteLineCentered("1. Edzés hozzáadása");
            WriteLineCentered("2. Edzések megtekintése");
            WriteLineCentered("3. Edzés módosítása");
            WriteLineCentered("4. Edzés törlése");
            WriteLineCentered("5. Beállítások");
            WriteLineCentered("6. Kijelentkezés");
            WriteLineCentered("-------------------------");
            WriteCentered("Válassza ki a menüpontot: ");
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
                    bejelentkezve = false;
                    bejelentkezettlistapozicio = 0;
                    bejelentkezettnev = "";
                    break;
                default:
                    if (menu == "")
                    {
                        WriteError("Nem adott meg menüpontot!");
                        Console.ReadLine();
                    }
                    else
                    {
                        WriteError("Nincs ilyen menüpont!");
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
                if (!bejelentkezve)
                {
                    WriteError("Előbb jelentkezz be!");
                    Console.ReadLine();
                    return;
                }

                Console.Clear();
                WriteLineCentered("Edzés hozzáadás: ");
                WriteCentered("Add meg a dátumot (éééé-hh-nn): ");
                DateTime datum = DateTime.Parse(Console.ReadLine());
                WriteCentered("Add meg a távot (km): ");
                Double tav = Double.Parse(Console.ReadLine());
                WriteCentered("Add meg az időtartamot (óó:pp:mm): ");
                TimeSpan idotartam = TimeSpan.Parse(Console.ReadLine());
                WriteCentered("Add meg a max pulzusod (pl: 120): ");
                int maxpulzus = int.Parse(Console.ReadLine());

                string osszefuzottadatok = $"{datum};{tav};{idotartam};{maxpulzus}\n";

                Training adathozzaadas1 = new Training(datum, tav, idotartam, maxpulzus);
                edzesadatok.Add(adathozzaadas1);

                File.AppendAllText($"{bejelentkezettnev}felhasznalo.txt", osszefuzottadatok);
                WriteSuccess("Edzés sikeresen hozzáadva");
                Console.ReadLine();

            }
            catch
            {
                WriteWarning("Hibás input! Enter a tovább lépéshez!");
                Console.ReadLine();
            }
        }

        /// <summary>
        /// A rögzített edzések listázása és összesített statisztikák (táv, idő) megjelenítése.
        /// </summary>
        static void EdzesMegtekintese()
        {
            Console.Clear();
            WriteLineCentered("=== ÖSSZES EDZÉS ===");
            int szamlalo = 1;
            double ossztav = 0;
            TimeSpan osszido = new TimeSpan();
            for (int i = 0; i < edzesadatok.Count; i++)
            {
                foreach (var edzes in edzesadatok)
                {
                    ossztav += edzes.Tav;
                    osszido += edzes.Idotartam;
                }
            }
            foreach (var edzes in edzesadatok)
            {
                WriteLineCentered($"{szamlalo}. Dátum: {edzes.Datum.ToShortDateString()}, Táv: {edzes.Tav} km, Időtartam: {edzes.Idotartam}, Max pulzus: {edzes.MaxPulzus}");
                szamlalo++;
            }
            WriteLineCentered($"Össztáv: {ossztav} km");
            WriteLineCentered($"Összidő: {osszido.Days} nap {osszido.Hours} óra {osszido.Minutes} perc {osszido.Seconds} másodperc");
            Console.WriteLine("");
            WriteCentered("Nyomj entert a visszatéréshez!");
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
                WriteLineCentered("=== EDZÉSEK MÓDOSÍTÁSA ===");
                WriteLineCentered("");
                int szamlalo = 1;
                foreach (var edzes in edzesadatok)
                {
                    WriteLineCentered($"{szamlalo}. Dátum: {edzes.Datum.ToShortDateString()}, Táv: {edzes.Tav} km, Időtartam: {edzes.Idotartam}, Max pulzus: {edzes.MaxPulzus}");
                    szamlalo++;
                }

                WriteCentered("Add meg a módosítandó adat sorszámát: ");
                int sorszam = int.Parse(Console.ReadLine());
                WriteCentered("Add meg a dátumot (éééé-hh-nn): ");
                DateTime datum = DateTime.Parse(Console.ReadLine());
                WriteCentered("Add meg a távot (km): ");
                Double tav = Double.Parse(Console.ReadLine());
                WriteCentered("Add meg az időtartamot (óó:pp:mm): ");
                TimeSpan idotartam = TimeSpan.Parse(Console.ReadLine());
                WriteCentered("Add meg a max pulzusod (pl: 120): ");
                int maxpulzus = int.Parse(Console.ReadLine());

                string osszefuzottadatok = $"{datum};{tav};{idotartam};{maxpulzus}\n";

                edzesadatok.RemoveAt(sorszam-1);
                Training adathozzaadas1 = new Training(datum, tav, idotartam, maxpulzus);
                edzesadatok.Insert(sorszam-1, adathozzaadas1);

                
                foreach (var v in felhasznaloadatok)
                {
                    if (v.Nev == bejelentkezettnev)
                    {
                        File.WriteAllText($"{bejelentkezettnev}felhasznalo.txt", "");
                        File.AppendAllText($"{bejelentkezettnev}felhasznalo.txt", $"{v.Nev};{v.Jelszo};{v.Magassag};{v.Testtomeg};{v.NyugalmiPulzus};{v.CelIdo}\n");
                        foreach (var v1 in edzesadatok)
                        {
                            File.AppendAllText($"{bejelentkezettnev}felhasznalo.txt", $"{v1.Datum};{v1.Tav};{v1.Idotartam};{v1.MaxPulzus}\n");
                        }
                    }
                }
                WriteSuccess("Sikeres módosítás!");
                Console.ReadLine();
            }
            catch
            {
                WriteWarning("Hibás input! Enter a tovább lépéshez!");
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
                WriteLineCentered("=== EDZÉS TÖRLÉSE ===");
                WriteLineCentered("");
                int szamlalo = 1;
                foreach (var edzes in edzesadatok)
                {
                    WriteLineCentered($"{szamlalo}. Dátum: {edzes.Datum.ToShortDateString()}, Táv: {edzes.Tav} km, Időtartam: {edzes.Idotartam}, Max pulzus: {edzes.MaxPulzus}");
                    szamlalo++;
                }

                WriteCentered("Add meg a törlendő adat sorszámát: ");
                int sorszam = int.Parse(Console.ReadLine());

                edzesadatok.RemoveAt(sorszam - 1);

                foreach (var v in felhasznaloadatok)
                {
                    if (v.Nev == bejelentkezettnev)
                    {
                        File.WriteAllText($"{bejelentkezettnev}felhasznalo.txt", "");
                        File.AppendAllText($"{bejelentkezettnev}felhasznalo.txt", $"{v.Nev};{v.Jelszo};{v.Magassag};{v.Testtomeg};{v.NyugalmiPulzus};{v.CelIdo}\n");
                        foreach (var v1 in edzesadatok)
                        {
                            File.AppendAllText($"{bejelentkezettnev}felhasznalo.txt", $"{v1.Datum};{v1.Tav};{v1.Idotartam};{v1.MaxPulzus}\n");
                        }
                    }
                }
            }
            catch
            {
                WriteWarning("Hibás input! Enter a tovább lépéshez!");
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Beállítások almenü (Háttérszín, Betűszín, Adatmodosítás).
        /// </summary>
        static void Beallitasok()
        {
            Console.Clear();
            WriteLineCentered("=== BEÁLLÍTÁSOK ===");
            WriteLineCentered("1. Háttér módosítása");
            WriteLineCentered("2. Betűszín módosítása");
            WriteLineCentered("3. Személyes adatok módosítása");
            WriteLineCentered("4. Vissza");
            WriteLineCentered("-------------------------");
            WriteCentered("Válassza ki a menüpontot: ");
            string menu = Console.ReadLine();
            switch (menu)
            {
                case "1":
                    HatterMod();
                    break;
                case "2":
                    BetuszinMod();
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
                        WriteError("Nem adott meg menüpontot!");
                        Console.ReadLine();
                    }
                    else
                    {
                        WriteError("Nincs ilyen menüpont!");
                        Console.ReadLine();
                    }
                    return;
            }
        }

        /// <summary>
        /// Segédfüggvény: Egész szám átalakítása ConsoleColor típussá.
        /// </summary>
        /// <param name="v">A kiválasztott szín sorszáma.</param>
        /// <returns>A megfelelő ConsoleColor érték.</returns>
        static ConsoleColor ValasztasToColor(int v)
        {
            switch (v)
            {
                case 1: return ConsoleColor.Black;
                case 2: return ConsoleColor.White;
                case 3: return ConsoleColor.Gray;
                case 4: return ConsoleColor.Red;
                case 5: return ConsoleColor.Green;
                case 6: return ConsoleColor.Blue;
                case 7: return ConsoleColor.Cyan;
                case 8: return ConsoleColor.Yellow;
                case 9: return ConsoleColor.Magenta;
                default: return ConsoleColor.White;
            }
        }

        /// <summary>
        /// A konzol háttérszínének beállítása a felhasználó választása alapján.
        /// </summary>
        static int HatterMod()
        {
            Console.Clear();
            WriteLineCentered("=== HÁTTÉRSZÍN MÓDOSÍTÁSA ===");
            Console.ForegroundColor = ConsoleColor.Black;
            WriteLineCentered("1. Fekete");
            Console.ForegroundColor = ConsoleColor.White;
            WriteLineCentered("2. Fehér");
            Console.ForegroundColor = ConsoleColor.Gray;
            WriteLineCentered("3. Szürke");
            Console.ForegroundColor = ConsoleColor.Red;
            WriteLineCentered("4. Piros");
            Console.ForegroundColor = ConsoleColor.Green;
            WriteLineCentered("5. Zöld");
            Console.ForegroundColor = ConsoleColor.Blue;
            WriteLineCentered("6. Kék");
            Console.ForegroundColor = ConsoleColor.Cyan;
            WriteLineCentered("7. Cián");
            Console.ForegroundColor = ConsoleColor.Yellow;
            WriteLineCentered("8. Sárga");
            Console.ForegroundColor = ConsoleColor.Magenta;
            WriteLineCentered("9. Magenta");
            Console.ForegroundColor = ConsoleColor.White;
            WriteLineCentered("");
            WriteCentered("Adja meg a kivánt szín számát: ");
            int valasztas = int.Parse(Console.ReadLine());

            ConsoleColor ujHatter = ValasztasToColor(valasztas);

            if (ujHatter == Console.ForegroundColor)
            {
                WriteWarning("A háttér és a betűszín nem lehet ugyanaz!");
                Console.ReadLine();
                return -1;
            }

            Console.BackgroundColor = ujHatter;
            return valasztas;
        }

        /// <summary>
        /// A konzol betűszínének beállítása a felhasználó választása alapján.
        /// </summary>
        static int BetuszinMod()
        {
            Console.Clear();
            WriteLineCentered("=== BETŰSZÍN MÓDOSÍTÁSA ===");
            Console.ForegroundColor = ConsoleColor.Black;
            WriteLineCentered("1. Fekete");
            Console.ForegroundColor = ConsoleColor.White;
            WriteLineCentered("2. Fehér");
            Console.ForegroundColor = ConsoleColor.Gray;
            WriteLineCentered("3. Szürke");
            Console.ForegroundColor = ConsoleColor.Red;
            WriteLineCentered("4. Piros");
            Console.ForegroundColor = ConsoleColor.Green;
            WriteLineCentered("5. Zöld");
            Console.ForegroundColor = ConsoleColor.Blue;
            WriteLineCentered("6. Kék");
            Console.ForegroundColor = ConsoleColor.Cyan;
            WriteLineCentered("7. Cián");
            Console.ForegroundColor = ConsoleColor.Yellow;
            WriteLineCentered("8. Sárga");
            Console.ForegroundColor = ConsoleColor.Magenta;
            WriteLineCentered("9. Magenta");
            Console.ForegroundColor = ConsoleColor.White;
            WriteLineCentered("");
            WriteCentered("Adja meg a kivánt szín számát: ");
            int valasztas = int.Parse(Console.ReadLine());

            ConsoleColor ujBetuszin = ValasztasToColor(valasztas);

            if (ujBetuszin == Console.BackgroundColor)
            {
                WriteWarning("A háttér és a betűszín nem lehet ugyanaz!");
                Console.ReadLine();
                return -1;
            }

            Console.ForegroundColor = ujBetuszin;
            return valasztas;
        }

        /// <summary>
        /// Személyes adatok kezelésére szolgáló almenü.
        /// </summary>
        static void Adatmodositas()
        {
            Console.Clear();
            WriteLineCentered("=== SZEMÉLYES ADATOK MÓDOSÍTÁSA ===");
            WriteLineCentered("1. Adatok megtekintése");
            WriteLineCentered("2. Adatok módosítása");
            WriteLineCentered("3. Jelszó módosítása");
            WriteLineCentered("4. Vissza");
            WriteLineCentered("-------------------------");
            WriteCentered("Válassza ki a menüpontot: ");
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
                        WriteError("Nem adott meg menüpontot!");
                        Console.ReadLine();
                    }
                    else
                    {
                        WriteError("Nincs ilyen menüpont!");
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
                WriteLineCentered("=== SZEMÉLYES ADATOK ===");
                WriteLineCentered($"Név: {felhasznaloadatok[bejelentkezettlistapozicio].Nev}");
                WriteLineCentered($"Magasság: {felhasznaloadatok[bejelentkezettlistapozicio].Magassag} cm");
                WriteLineCentered($"Testtömeg: {felhasznaloadatok[bejelentkezettlistapozicio].Testtomeg} kg");
                WriteLineCentered($"Nyugalmi pulzus: {felhasznaloadatok[bejelentkezettlistapozicio].NyugalmiPulzus}");
                WriteLineCentered($"Célidő 5 km-re: {felhasznaloadatok[bejelentkezettlistapozicio].CelIdo}");
                Console.WriteLine("");
                WriteCentered("Nyomj entert a visszatéréshez!");
                Console.ReadLine();
            }

            /// <summary>
            /// Személyes adatok (magasság, súly, pulzus, célidő) módosítása és mentése fájlba.
            /// </summary>
            void Adatok()
            {
                Console.Clear();
                WriteCentered("Add meg az új magasságot (cm): ");
                double magassag = double.Parse(Console.ReadLine());
                WriteCentered("Add meg az új testtömeget (kg): ");
                double testtomeg = double.Parse(Console.ReadLine());
                WriteCentered("Add meg az új nyugalmi pulzust: ");
                int nyugalmiPulzus = int.Parse(Console.ReadLine());
                WriteCentered("Add meg az új célidőt 5 km-re (óó:pp:mp): ");
                TimeSpan celIdo = TimeSpan.Parse(Console.ReadLine());
                felhasznaloadatok[bejelentkezettlistapozicio].Magassag = magassag;
                felhasznaloadatok[bejelentkezettlistapozicio].Testtomeg = testtomeg;
                felhasznaloadatok[bejelentkezettlistapozicio].NyugalmiPulzus = nyugalmiPulzus;
                felhasznaloadatok[bejelentkezettlistapozicio].CelIdo = celIdo;
                List<string> ujadatok = new List<string>();
                foreach (var f in felhasznaloadatok)
                {
                    ujadatok.Add($"{f.Nev};{f.Jelszo};{f.Magassag};{f.Testtomeg};{f.NyugalmiPulzus};{f.CelIdo}");
                }
                File.WriteAllLines("Felhasznalok.txt", ujadatok);
                WriteSuccess("Sikeres módosítás!");
                Console.ReadLine();
            }

            /// <summary>
            /// Jelszó módosítása és mentése.
            /// </summary>
            void Jelszo()
            {
                Console.Clear();
                WriteCentered("Add meg az új jelszót: ");
                string jelszo = Console.ReadLine();
                felhasznaloadatok[bejelentkezettlistapozicio].Jelszo = jelszo;
                List<string> ujjelszo = new List<string>();
                foreach (var f in felhasznaloadatok)
                {
                    ujjelszo.Add($"{f.Jelszo}");
                }
                File.WriteAllLines("Felhasznalok.txt", ujjelszo);
                WriteSuccess("Sikeres módosítás!");
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Adminisztrátori menü kezelése.
        /// </summary>
        static void Admin()
        {
            Console.Clear();
            WriteAdmin("=== ADMIN MENÜ ===");
            WriteLineCentered("1. Felhasználók listázása");
            WriteLineCentered("2. Felhasználó módosítása");
            WriteLineCentered("3. Felhasználó törlése");
            WriteLineCentered("4. Vissza a főmenübe");
            WriteLineCentered("-------------------------");
            WriteCentered("Válassza ki a menüpontot: ");
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
                        WriteError("Nem adott meg menüpontot!");
                        Console.ReadLine();
                    }
                    else
                    {
                        WriteError("Nincs ilyen menüpont!");
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
                    WriteLineCentered("=== FELHASZNÁLÓK ===");
                    WriteLineCentered("--------------------------");
                    if (felhasznaloadatok.Count == 0)
                    {
                        WriteLineCentered("Nincsenek felhasználók!");
                    }
                    else
                    {
                        foreach (var felhasznalo in felhasznaloadatok)
                        {
                            WriteLineCentered($"Név: {felhasznalo.Nev}, Jelszó: {felhasznalo.Jelszo}, Magasság: {felhasznalo.Magassag} cm, Testtömeg: {felhasznalo.Testtomeg} kg, Nyugalmi pulzus: {felhasznalo.NyugalmiPulzus}, Célidő 5 km-re: {felhasznalo.CelIdo}");
                        }
                    }
                    WriteLineCentered("--------------------------");
                    Console.WriteLine("");
                    WriteCentered("Nyomj entert a visszatéréshez!");
                    Console.ReadLine();

                }
                catch
                {
                    WriteWarning("Hibás input! Nyomj entert a tovább lépéshez!");
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
                    WriteLineCentered("=== FELHASZNÁLÓ MÓDOSÍTÁSA ===");
                    WriteCentered("Add meg a módosítandó felhasználó nevét: ");
                    string nev = Console.ReadLine();
                    if (nev == null)
                    {       
                        WriteError("Nem adott meg felhasználót!");
                    }
                    for (int i = 0; i < felhasznaloadatok.Count; i++)
                    {
                        if (nev == felhasznaloadatok[i].Nev)
                        {
                            WriteCentered("Adja meg az új jelszót: ");
                            string jelszo = Console.ReadLine();
                            WriteCentered("Adja meg az új magasságot (cm): ");
                            double magassag = double.Parse(Console.ReadLine());
                            WriteCentered("Adja meg az új testtömeget (kg): ");
                            double testtomeg = double.Parse(Console.ReadLine());
                            WriteCentered("Adja meg az új nyugalmi pulzust: ");
                            int nyugalmiPulzus = int.Parse(Console.ReadLine());
                            WriteCentered("Adja meg az új célidőt 5 km-re (óó:pp:mp): ");
                            TimeSpan celIdo = TimeSpan.Parse(Console.ReadLine());
                            felhasznaloadatok[i].Jelszo = jelszo;
                            felhasznaloadatok[i].Magassag = magassag;
                            felhasznaloadatok[i].Testtomeg = testtomeg;
                            felhasznaloadatok[i].NyugalmiPulzus = nyugalmiPulzus;
                            felhasznaloadatok[i].CelIdo = celIdo;
                            List<string> ujadatok = new List<string>();
                            foreach (var f in felhasznaloadatok)
                            {
                                ujadatok.Add($"{f.Nev};{f.Jelszo};{f.Magassag};{f.Testtomeg};{f.NyugalmiPulzus};{f.CelIdo}");
                            }
                            File.WriteAllLines("Felhasznalok.txt", ujadatok);
                            WriteSuccess("Sikeres módosítás!");
                            Console.ReadLine();
                            return;
                        }
                        else
                        {
                            WriteError("Nincs ilyen felhasználó!");
                            Console.ReadLine();
                        }
                    }
                }
                catch
                {
                    WriteWarning("Hibás input! Nyomj entert a tovább lépéshez!");
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
                    WriteLineCentered("=== FELHASZNÁLÓ TÖRLÉSE ===");
                    WriteCentered("Add meg a törlendő felhasználó nevét: ");
                    string nev = Console.ReadLine();
                    for (int i = 0; i < felhasznaloadatok.Count; i++)
                    {
                        if (nev == felhasznaloadatok[i].Nev)
                        {
                            felhasznaloadatok.RemoveAt(i);
                            List<string> ujadatok = new List<string>();
                            foreach (var f in felhasznaloadatok)
                            {
                                ujadatok.Add($"{f.Nev};{f.Jelszo};{f.Magassag};{f.Testtomeg};{f.NyugalmiPulzus};{f.CelIdo}");
                            }
                            File.WriteAllLines("Felhasznalok.txt", ujadatok);
                            File.Delete($"{nev}felhasznalo.txt");
                            WriteSuccess("Sikeres törlés!");
                            Console.ReadLine();
                            return;
                        }
                    }
                    WriteError("Nincs ilyen felhasználó!");
                    Console.ReadLine();
                }
                catch
                {
                    WriteWarning("Hibás input! Nyomj entert a tovább lépéshez!");
                    Console.ReadLine();
                }
            }
        }

        /// <summary>
        /// Szöveg kiírása a konzol ablak közepére, sortöréssel a végén.
        /// </summary>
        /// <param name="text">A megjelenítendő szöveg.</param>
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

        /// <summary>
        /// Szöveg kiírása a konzol ablak közepére, sortörés nélkül.
        /// </summary>
        /// <param name="text">A megjelenítendő szöveg.</param>
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

        /// <summary>
        /// Sikerüzenet kiírása sötétzöld színnel, középre igazítva.
        /// </summary>
        /// <param name="text">Az üzenet szövege.</param>
        static void WriteSuccess(string text)
        {
            var oldColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            WriteLineCentered(text);

            Console.ForegroundColor = oldColor;
        }

        /// <summary>
        /// Figyelmeztető üzenet (itt warning-ként használva a kódban, bár a név error) kiírása sötétvörös színnel.
        /// </summary>
        /// <param name="text">A hibaüzenet szövege.</param>
        static void WriteError(string text)
        {
            var oldColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.DarkRed;
            WriteLineCentered(text);

            Console.ForegroundColor = oldColor;
        }

        /// <summary>
        /// Kiírja a megadott szöveget a konzol ablak közepére sötétkék színnel (Admin üzenet),
        /// majd visszaállítja az eredeti szövegszínt.
        /// </summary>
        /// <param name="text">A kiírandó szöveg.</param>
        static void WriteAdmin(string text)
        {
            var oldColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            WriteLineCentered(text);

            Console.ForegroundColor = oldColor;
        }

        /// <summary>
        /// Figyelmeztető üzenet kiírása (az eredeti kódban nem szerepelt definíció, de hívták).
        /// </summary>
        static void WriteWarning(string text)
        {
            var oldColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            WriteLineCentered(text);

            Console.ForegroundColor = oldColor;
        }

        /// <summary>
        /// Beolvassa a regisztrált felhasználókat a 'Felhasznalok.txt' fájlból, 
        /// példányosítja a Runner objektumokat, és feltölti velük a 'felhasznaloadatok' listát.
        /// </summary>
        static void felhasznalobetoltes()
        {
            foreach (var v in File.ReadAllLines("Felhasznalok.txt"))
            {
                string[] adatok = v.Split(';');
                Runner futohozzaadas1 = new Runner(adatok[0], adatok[1], int.Parse(adatok[2]), int.Parse(adatok[3]), int.Parse(adatok[4]), TimeSpan.Parse(adatok[5]));
                felhasznaloadatok.Add(futohozzaadas1);
                
            }
        }

        /// <summary>
        /// Betölti az éppen bejelentkezett felhasználóhoz tartozó korábbi edzéseket 
        /// a felhasználó nevével ellátott fájlból az 'edzesadatok' listába.
        /// </summary>
        static void edzesadatokbetoltes()
        {
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