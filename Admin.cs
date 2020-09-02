using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat_1
{
    [Serializable()]
    class Admin : Korisnik
    {
        private readonly string uloga = "admin";

        public Admin(string kIme, string loz, string ime, string prezime, bool isAdmin) 
                    : base(kIme, loz, ime, prezime, isAdmin) { }

        public string Uloga { get { return this.uloga; } }

        public override string ToString()
        {
            return base.ToString() + "," + uloga;
        }
    }
}
