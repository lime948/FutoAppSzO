using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutoApp.Views
{
    internal class UI
    {
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
    }
}
