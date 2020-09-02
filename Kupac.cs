using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat_1
{
    [Serializable()]
    class Kupac : Korisnik
    {
        private string jmbg;
        private DateTime datum_rodjenja;
        private string telefon;

        public Kupac(string kIme, string loz, string ime, string prez, bool isAdmin, string jmbg, DateTime dr, string tel) 
                        : base (kIme, loz, ime, prez, isAdmin)
        {
            this.jmbg = jmbg;
            this.datum_rodjenja = dr;
            this.telefon = tel;
        }

        public string Jmbg { get { return this.jmbg; } set { this.jmbg = value; } }
        public DateTime Datum_rodjenja { get { return this.datum_rodjenja; } set { this.datum_rodjenja = value; } }
        public string Telefon { get { return this.telefon; } set { this.telefon = value; } }

        public override string ToString()
        {
            return base.ToString() + "," + jmbg + "," + datum_rodjenja.Date.ToShortDateString() + "," + telefon;
        }
    }
}
