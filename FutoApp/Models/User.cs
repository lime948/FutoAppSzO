using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutoApp.Models
{
    class Training
    {
        public DateTime Datum { get; set; }
        public double Tav { get; set; }
        public TimeSpan Idotartam { get; set; }
        public int MaxPulzus { get; set; }

        public Training(DateTime datum, double tav, TimeSpan idotartam, int maxPulzus)
        {
            Datum = datum;
            Tav = tav;
            Idotartam = idotartam;
            MaxPulzus = maxPulzus;
        }
    }
    class Runner
    {
        public double Magassag { get; set; }
        public double Testtomeg { get; set; }
        public int NyugalmiPulzus { get; set; }
        public TimeSpan CelIdo { get; set; }

        public List<Training> Edzesek { get; set; }

        public Runner(double magassag, double testtomeg, int nyugalmiPulzus, TimeSpan celIdo)
        {
            Magassag = magassag;
            Testtomeg = testtomeg;
            NyugalmiPulzus = nyugalmiPulzus;
            CelIdo = celIdo;
            Edzesek = new List<Training>();
        }

        public void UjEdzes(Training edzes)
        {
            Edzesek.Add(edzes);
        }
    }
}
