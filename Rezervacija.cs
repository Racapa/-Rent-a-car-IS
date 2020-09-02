using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat_1
{
    [Serializable()]
    public class Rezervacija
    {
        private int idAuta;
        private int idKupca;
        private DateTime datum_od;
        private DateTime datum_do;
        private int cena;

        public Rezervacija(int idA, int idK, DateTime dod, DateTime ddo, int cena)
        {
            this.idAuta = idA;
            this.idKupca = idK;
            this.datum_od = dod;
            this.datum_do = ddo;
            this.cena = cena;
        }

        public int IdAuta { get { return this.idAuta; } set { this.idAuta = value; } }
        public int IdKupca { get { return this.idKupca; } set { this.idKupca = value; } }
        public DateTime Datum_od { get { return this.datum_od; } set { this.datum_od = value; } }
        public DateTime Datum_do { get { return this.datum_do; } set { this.datum_do = value; } }
        public int Cena { get { return this.cena; } set { this.cena = value; } }

        public override string ToString()
        {
            return idAuta + "," + idKupca + "," + datum_od.ToString("dd.MM.yyyy.") + "," + datum_do.ToString("dd.MM.yyyy.") + "," + cena;
        }
    }
}
