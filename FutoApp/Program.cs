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


        //static int HatterMod()
        //{
        //    int valasztas = 1;
        //    Console.Clear();
        //    WriteLineCentered("=== HÁTTÉRSZÍN MÓDOSÍTÁSA ===");
        //    Console.ForegroundColor = ConsoleColor.Black;
        //    WriteLineCentered("1. Fekete");
        //    Console.ForegroundColor = ConsoleColor.White;
        //    WriteLineCentered("2. Fehér"); 
        //    Console.ForegroundColor = ConsoleColor.Gray;
        //    WriteLineCentered("3. Szürke");
        //    Console.ForegroundColor = ConsoleColor.Red;
        //    WriteLineCentered("4. Piros");
        //    Console.ForegroundColor = ConsoleColor.Green;
        //    WriteLineCentered("5. Zöld");
        //    Console.ForegroundColor = ConsoleColor.Blue;
        //    WriteLineCentered("6. Kék");
        //    Console.ForegroundColor = ConsoleColor.Cyan;
        //    WriteLineCentered("7. Cián");
        //    Console.ForegroundColor = ConsoleColor.Yellow;
        //    WriteLineCentered("8. Sárga");
        //    Console.ForegroundColor = ConsoleColor.Magenta;
        //    WriteLineCentered("9. Magenta");
        //    Console.ForegroundColor = ConsoleColor.White;
        //    WriteCentered("");
        //    valasztas = int.Parse(Console.ReadLine());
        //    if (BetuszinMod() == valasztas)
        //    {
        //        WriteWarning("A háttér és a betűszín nem lehet ugyanaz!");
        //        Console.ReadLine();
        //        return valasztas;
        //    }
        //    else
        //    {
        //        if (valasztas == 1)
        //        {
        //            Console.BackgroundColor = ConsoleColor.Black;
        //        }
        //        else if (valasztas == 2)
        //        {
        //            Console.BackgroundColor = ConsoleColor.White;
        //        }
        //        else if (valasztas == 3)
        //        {
        //            Console.BackgroundColor = ConsoleColor.Gray;
        //        }
        //        else if (valasztas == 4)
        //        {
        //            Console.BackgroundColor = ConsoleColor.Red;
        //        }
        //        else if (valasztas == 5)
        //        {
        //            Console.BackgroundColor = ConsoleColor.Green;
        //        }
        //        else if (valasztas == 6)
        //        {
        //            Console.BackgroundColor = ConsoleColor.Blue;
        //        }
        //        else if (valasztas == 7)
        //        {
        //            Console.BackgroundColor = ConsoleColor.Cyan;
        //        }
        //        else if (valasztas == 8)
        //        {
        //            Console.BackgroundColor = ConsoleColor.Yellow;
        //        }
        //        else if (valasztas == 9)
        //        {
        //            Console.BackgroundColor = ConsoleColor.Magenta;
        //        }
        //    }
        //    return valasztas;
        //}

        //static int BetuszinMod()
        //{
        //    int valasztas = 1;
        //    Console.Clear();
        //    WriteLineCentered("=== BETŰSZÍN MÓDOSÍTÁSA ===");
        //    Console.ForegroundColor = ConsoleColor.Black;
        //    WriteLineCentered("1. Fekete");
        //    Console.ForegroundColor = ConsoleColor.White;
        //    WriteLineCentered("2. Fehér");
        //    Console.ForegroundColor = ConsoleColor.Gray;
        //    WriteLineCentered("3. Szürke");
        //    Console.ForegroundColor = ConsoleColor.Red;
        //    WriteLineCentered("4. Piros");
        //    Console.ForegroundColor = ConsoleColor.Green;
        //    WriteLineCentered("5. Zöld");
        //    Console.ForegroundColor = ConsoleColor.Blue;
        //    WriteLineCentered("6. Kék");
        //    Console.ForegroundColor = ConsoleColor.Cyan;
        //    WriteLineCentered("7. Cián");
        //    Console.ForegroundColor = ConsoleColor.Yellow;
        //    WriteLineCentered("8. Sárga");
        //    Console.ForegroundColor = ConsoleColor.Magenta;
        //    WriteLineCentered("9. Magenta");
        //    Console.ForegroundColor = ConsoleColor.White;
        //    WriteCentered("");
        //    valasztas = int.Parse(Console.ReadLine());
        //    if (BetuszinMod() == valasztas)
        //    {
        //        WriteWarning("A háttér és a betűszín nem lehet ugyanaz!");
        //        Console.ReadLine();
        //        return valasztas;
        //    }
        //    else
        //    {
        //        if (valasztas == 1)
        //        {
        //            Console.ForegroundColor = ConsoleColor.Black;
        //        }
        //        else if (valasztas == 2)
        //        {
        //            Console.ForegroundColor = ConsoleColor.White;
        //        }
        //        else if (valasztas == 3)
        //        {
        //            Console.ForegroundColor = ConsoleColor.Gray;
        //        }
        //        else if (valasztas == 4)
        //        {
        //            Console.ForegroundColor = ConsoleColor.Red;
        //        }
        //        else if (valasztas == 5)
        //        {
        //            Console.ForegroundColor = ConsoleColor.Green;
        //        }
        //        else if (valasztas == 6)
        //        {
        //            Console.ForegroundColor = ConsoleColor.Blue;
        //        }
        //        else if (valasztas == 7)
        //        {
        //            Console.ForegroundColor = ConsoleColor.Cyan;
        //        }
        //        else if (valasztas == 8)
        //        {
        //            Console.ForegroundColor = ConsoleColor.Yellow;
        //        }
        //        else if (valasztas == 9)
        //        {
        //            Console.ForegroundColor = ConsoleColor.Magenta;
        //        }
        //}
        //        return valasztas;
        //}

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

        static void WriteSuccess(string text)
        {
            var oldColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            WriteLineCentered(text);

            Console.ForegroundColor = oldColor;
        }

        static void WriteError(string text)
        {
            var oldColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.DarkRed;
            WriteLineCentered(text);

            Console.ForegroundColor = oldColor;
        }

        static void WriteAdmin(string text)
        {
            var oldColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            WriteLineCentered(text);

            Console.ForegroundColor = oldColor;
        }

        static void WriteWarning(string text)
        {
            var oldColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            WriteLineCentered(text);

            Console.ForegroundColor = oldColor;
        }

        static void felhasznalobetoltes()
        {
            foreach (var v in File.ReadAllLines("Felhasznalok.txt"))
            {
                string[] adatok = v.Split(';');
                Runner futohozzaadas1 = new Runner(adatok[0], adatok[1], int.Parse(adatok[2]), int.Parse(adatok[3]), int.Parse(adatok[4]), TimeSpan.Parse(adatok[5]));
                felhasznaloadatok.Add(futohozzaadas1);
                
            }
        }
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