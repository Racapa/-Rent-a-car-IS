using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat_1
{
    [Serializable()]
    class Korisnik
    {
        protected int id;
        protected string korisnicko_ime;
        protected string lozinka;
        protected string ime;
        protected string prezime;
        protected bool isAdmin;
        protected static List<int> niz_id = new List<int>();

        protected Korisnik(string kIme, string loz, string ime, string prezime, bool isAdmin)
        {
            DodajId();
            this.korisnicko_ime = kIme;
            this.lozinka = loz;
            this.ime = ime;
            this.prezime = prezime;
            this.isAdmin = isAdmin;
        }

        public int Id { get { return this.id; } }
        public string Korisnicko_ime { get { return this.korisnicko_ime; } }
        public string Lozinka { get { return this.lozinka; } set { this.lozinka = value; } }
        public string Ime { get { return this.ime; } set { this.ime = value; } }
        public string Prezime { get { return this.prezime; } set { this.prezime = value; } }
        public bool IsAdmin { get { return this.isAdmin; } }

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

        public new virtual string ToString()
        {
            return id + "," + korisnicko_ime + "," + ime + "," + prezime;
        }
    }
}
