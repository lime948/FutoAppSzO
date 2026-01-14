using FutoApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutoApp
{
    internal class Program
    {

        public static List<Training> edzesadatok = new List<Training>();
        public static List<Runner> futoadatok = new List<Runner>();

        static void Main(string[] args)
        {
            edzeshozzaadas();
            futohozzaadas();
        }

        static void edzeshozzaadas()
        {
            Console.Clear();
            Console.WriteLine("Edzés hozzáadás: ");
            Console.WriteLine("Add mega dátumot:");
            DateTime datum = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Add meg a távot:");
            Double tav = Double.Parse(Console.ReadLine());
            Console.WriteLine("Add meg az időtartamot:");
            TimeSpan idotartam = TimeSpan.Parse(Console.ReadLine());
            Console.WriteLine("Add meg a max pulzusod:");
            int maxpulzus = int.Parse(Console.ReadLine());

            Training adathozzaadas1 = new Training(datum, tav, idotartam, maxpulzus);
            edzesadatok.Add(adathozzaadas1);

        }

        static void futohozzaadas()
        {
            Console.Clear();
            Console.WriteLine("Futó hozzáadás: ");
            Console.WriteLine("Add meg a magasságod (cm):");
            double magassag = double.Parse(Console.ReadLine());
            Console.WriteLine("Add meg a testtömeged (kg):");
            double testtomeg = double.Parse(Console.ReadLine());
            Console.WriteLine("Add meg a nyugalmi pulzusod:");
            int nyugalmiPulzus = int.Parse(Console.ReadLine());
            Console.WriteLine("Add meg a célidőt 5 km-re (óó:pp:mp):");
            TimeSpan celIdo = TimeSpan.Parse(Console.ReadLine());

            Runner futohozzaadas1 = new Runner(magassag, testtomeg, nyugalmiPulzus, celIdo);
            futoadatok.Add(futohozzaadas1);
        }

    }
}

