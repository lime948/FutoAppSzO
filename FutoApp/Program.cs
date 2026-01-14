using FutoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace FutoApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WriteLineCentered("=== FUTÓ EDZÉS NAPLÓ ===");
            WriteLineCentered("1. Regisztráció");
            WriteLineCentered("2. Bejelentkezés");
            WriteLineCentered("3. Kilépés");
            WriteLineCentered("-------------------------");
            WriteCentered("Válassza ki a menüpontot: ");
            int menu = int.Parse(Console.ReadLine());
            switch (menu)
            {
                case 1:
                    // Regisztráció
                    break;
                case 2:
                    // Bejelentkezés
                    break;
                case 3:
                    // Kilépés
                    break;
                default:
                    WriteLineCentered("Érvénytelen menüpont!");
                    break;
            }
            //Runner futo = new Runner(
            //    magassag: 178,
            //    testtomeg: 72,
            //    nyugalmiPulzus: 55,
            //    celIdo: new TimeSpan(0, 22, 0)
            //);

            //Training edzes1 = new Training(
            //    datum: new DateTime(2026, 1, 10),
            //    tav: 5.2,
            //    idotartam: new TimeSpan(0, 27, 45),
            //    maxPulzus: 178
            //);

            //futo.UjEdzes(edzes1);

            //Console.WriteLine("Edzések száma: " + futo.Edzesek.Count);
            //Console.WriteLine("Első edzés dátuma: " + futo.Edzesek[0].Datum.ToShortDateString());
        }

        public static void WriteLineCentered(string text, bool changeColor = true)
        {
            int width = Console.WindowWidth;
            int leftPadding = (width - text.Length) / 2;
            if (leftPadding < 0)
            {
                leftPadding = 0;
            }
            Console.WriteLine(new string(' ', leftPadding) + text);
        }
        public static void WriteCentered(string text, bool changeColor = true)
        {
            int width = Console.WindowWidth;
            int leftPadding = (width - text.Length) / 2;
            if (leftPadding < 0)
            {
                leftPadding = 0;
            }
            Console.Write(new string(' ', leftPadding) + text);
        }
    }
}
