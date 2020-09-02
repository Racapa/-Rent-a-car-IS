using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat_1
{
    [Serializable()]
    public class Automobil
    {
        private int id;
        private string marka;
        private string model;
        private int godiste;
        private int kubikaza;
        private string pogon;
        private string menjac;
        private string karoserija;
        private string gorivo;
        private int brVrata;
        private static List<int> niz_id = new List<int>();
        
        
        public Automobil(string mar, string mod, int god, int kub, string pog, string menj, string kar,
                  string gor, int brV)
        {
            DodajId();
            this.marka = mar;
            this.model = mod;
            this.godiste = god;
            this.kubikaza = kub;
            this.pogon = pog;
            this.menjac = menj;
            this.karoserija = kar;
            this.gorivo = gor;
            this.brVrata = brV;
        }

        public int Id { get { return this.id; } }
        public string Marka { get { return this.marka; } set { this.marka = value; } }
        public string Model { get { return this.model; } set { this.model = value; } }
        public int Godiste { get { return this.godiste; } set { this.godiste = value; } }
        public int Kubikaza { get { return this.kubikaza; } set { this.kubikaza = value; } }
        public string Pogon { get { return this.pogon; } set { this.pogon = value; } }
        public string Menjac { get { return this.menjac; } set { this.menjac = value; } }
        public string Karoserija { get { return this.karoserija; } set { this.karoserija = value; } }
        public string Gorivo { get { return this.gorivo; } set { this.gorivo = value; } }
        public int BrVrata { get { return this.brVrata; } set { this.brVrata = value; } }

        private void DodajId()
        {
            Random rnd = new Random();
            int probId = rnd.Next(1000, 9999);
            if (niz_id.Count == 0)
            {
                this.id = probId;
                niz_id.Add(probId);
            }
            else
            {
                while (true)
                {
                    if (niz_id.Contains(probId))
                        probId = rnd.Next(1000, 9999);
                    else
                    {
                        this.id = probId;
                        niz_id.Add(probId);
                        break;
                    }
                }
            }
        }

        public override string ToString()
        {
            return id + ", " + marka + ", " + model + ", " + godiste + ", " + kubikaza + ", " + pogon + 
                   ", " + menjac + ", " + karoserija + ", " + gorivo + ", " + brVrata;
        }
    }
}
