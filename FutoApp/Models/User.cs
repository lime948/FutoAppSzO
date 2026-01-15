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
        
        public DateTime Datum { get => datum; set => datum = value; }
        public double Tav { get => tav; set => tav = value; }
        public TimeSpan Idotartam { get => idotartam; set => idotartam = value; }
        public int MaxPulzus { get => maxPulzus; set => maxPulzus = value; }

        public Training() { }

        public Training(DateTime datum, double tav, TimeSpan idotartam, int maxPulzus)
        {
            this.Datum = datum;
            this.Tav = tav;
            this.Idotartam = idotartam;
            this.MaxPulzus = maxPulzus;
        }
    }
    class Runner
    {
        private string nev;
        private string jelszo;
        private double magassag;
        private double testtomeg;
        private int nyugalmiPulzus;
        private TimeSpan celIdo;

        public string Nev { get => nev; set => nev = value; }
        public string Jelszo { get => jelszo; set => jelszo = value; }
        public double Magassag { get => magassag; set => magassag = value; }
        public double Testtomeg { get => testtomeg; set => testtomeg = value; }
        public int NyugalmiPulzus { get => nyugalmiPulzus; set => nyugalmiPulzus = value; }
        public TimeSpan CelIdo { get => celIdo; set => celIdo = value; }

        public Runner() { }

        public Runner(string nev, string jelszo, double magassag, double testtomeg, int nyugalmiPulzus, TimeSpan celIdo)
        {
            this.Nev = nev;
            this.Jelszo = jelszo;
            this.Magassag = magassag;
            this.Testtomeg = testtomeg;
            this.NyugalmiPulzus = nyugalmiPulzus;
            this.CelIdo = celIdo;
        }

    }
}
