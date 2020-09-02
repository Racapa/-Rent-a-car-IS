using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat_1
{
    [Serializable()]
    public class Ponuda
    {
        private int idAuta;
        private DateTime datum_od;
        private DateTime datum_do;
        private int cenaDan;

        public Ponuda (int id, DateTime dod, DateTime ddo, int cena)
        {
            this.idAuta = id;
            this.datum_od = dod;
            this.datum_do = ddo;
            this.cenaDan = cena;
        }

        public int IdAuta { get { return this.idAuta; } set { this.idAuta = value; } }
        public DateTime Datum_od { get { return this.datum_od; } set { this.datum_od = value; } }
        public DateTime Datum_do { get { return this.datum_do; } set { this.datum_do = value; } }
        public int CenaDan { get { return this.cenaDan; } set { this.cenaDan = value; } }

        public override string ToString()
        {
            return idAuta + "," + datum_od.ToString("dd.MM.yyyy.") + "," + datum_do.ToString("dd.MM.yyyy.") + "," + cenaDan + Environment.NewLine;
        }
    }
}
