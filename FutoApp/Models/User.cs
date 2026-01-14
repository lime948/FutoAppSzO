using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutoApp.Models
{
    class Training
    {
        private DateTime datum;
        private double tav;
        private TimeSpan idotartam;
        private int maxPulzus;

        public Training(DateTime datum, double tav, TimeSpan idotartam, int maxPulzus)
        {
            this.datum = datum;
            this.tav = tav;
            this.idotartam = idotartam;
            this.maxPulzus = maxPulzus;
        }

        public Training() { }

        public DateTime Datum { get => datum; set => datum = value; }
        public double Tav { get => tav; set => tav = value; }
        public TimeSpan Idotartam { get => idotartam; set => idotartam = value; }
        public int MaxPulzus { get => maxPulzus; set => maxPulzus = value; }
    }
    class Runner
    {
        private double magassag;
        private double testtomeg;
        private int nyugalmiPulzus;
        private TimeSpan celIdo;

        public double Magassag { get => magassag; set => magassag = value; }
        public double Testtomeg { get => testtomeg; set => testtomeg = value; }
        public int NyugalmiPulzus { get => nyugalmiPulzus; set => nyugalmiPulzus = value; }
        public TimeSpan CelIdo { get => celIdo; set => celIdo = value; }

        public Runner() { }

        public Runner(double magassag, double testtomeg, int nyugalmiPulzus, TimeSpan celIdo)
        {
            this.Magassag = magassag;
            this.Testtomeg = testtomeg;
            this.NyugalmiPulzus = nyugalmiPulzus;
            this.CelIdo = celIdo;
        }

    }
}
