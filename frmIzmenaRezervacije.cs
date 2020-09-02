using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Projekat_1
{
    public partial class frmIzmenaRezervacije : Form
    {
        List<Automobil> automobili;
        List<Korisnik> korisnici;
        List<Ponuda> ponude;
        List<Rezervacija> rezervacije;
        string putanjaKor = "korisnici.rac";
        string putanjaAuto = "automobili.rac";
        string putanjaPon = "ponude.rac";
        string putanjaRez = "rezervacije.rac";

        public frmIzmenaRezervacije()
        {
            InitializeComponent();
            automobili = new List<Automobil>();
            korisnici = new List<Korisnik>();
            ponude = new List<Ponuda>();
            rezervacije = new List<Rezervacija>();
        }

        private void FrmIzmenaRezervacije_Load(object sender, EventArgs e)
        {
            BinaryFormatter bf = new BinaryFormatter();
            if (File.Exists(putanjaAuto))
            {
                FileStream fsa = File.OpenRead(putanjaAuto);
                automobili = bf.Deserialize(fsa) as List<Automobil>;
                fsa.Dispose();
            }
            if (File.Exists(putanjaKor))
            {
                FileStream fsk = File.OpenRead(putanjaKor);
                korisnici = bf.Deserialize(fsk) as List<Korisnik>;
                fsk.Dispose();
            }
            if (File.Exists(putanjaPon))
            {
                FileStream fsp = File.OpenRead(putanjaPon);
                ponude = bf.Deserialize(fsp) as List<Ponuda>;
                fsp.Dispose();
            }
            if (File.Exists(putanjaRez))
            {
                FileStream fsr = File.OpenRead(putanjaRez);
                rezervacije = bf.Deserialize(fsr) as List<Rezervacija>;
                fsr.Dispose();
            }
            foreach (Kupac k in korisnici.OfType<Kupac>())
                cbbKupci.Items.Add(k.ToString());
        }

        private void CbbKupci_SelectionChangeCommitted(object sender, EventArgs e)
        {
            lsbSpisakRezervacija.Items.Clear();
            bool uspesnoIdKupca = int.TryParse(cbbKupci.SelectedItem.ToString().Substring(0, 4), out int idKupca);
            if (uspesnoIdKupca)
            {
                foreach (Rezervacija r in rezervacije)
                    if (r.IdKupca == idKupca)
                        lsbSpisakRezervacija.Items.Add(r);
            }
            else
            {
                MessageBox.Show("Nepoznata greška. Forma će se zatvoriti.", "Greska");
                this.Close();
            }
        }

        private void BtnIzmeniRez_Click(object sender, EventArgs e)
        {
            BinaryFormatter bf = new BinaryFormatter();
            if (lsbSpisakRezervacija.SelectedIndex > -1)
            {
                if (dtpDatum_od.Value.Date <= dtpDatum_do.Value.Date)
                {
                    foreach (Rezervacija r in rezervacije)
                    {
                        if (r.Equals(lsbSpisakRezervacija.SelectedItem))
                        {
                            bool postojiRez = false;
                            foreach (Ponuda p in ponude)
                            {
                                if (p.IdAuta == r.IdAuta && r.Datum_od.Date >= p.Datum_od.Date && r.Datum_od.Date <= p.Datum_do.Date)
                                {
                                    if (dtpDatum_od.Value.Date >= p.Datum_od.Date && dtpDatum_od.Value.Date <= p.Datum_do.Date &&
                                        dtpDatum_do.Value.Date >= p.Datum_od.Date && dtpDatum_do.Value.Date <= p.Datum_do.Date)
                                    {
                                        DateTime privDatumOd = DateTime.MinValue;
                                        DateTime privDatumDo = DateTime.MaxValue;
                                        foreach(Rezervacija rez in rezervacije.FindAll(rr => ((rr.Datum_od.Date != r.Datum_od.Date) && (rr.Datum_do.Date != r.Datum_do.Date) && (rr.IdAuta == r.IdAuta))))
                                        {
                                            if ((dtpDatum_od.Value.Date >= rez.Datum_od.Date && dtpDatum_od.Value.Date <= rez.Datum_do.Date) ||
                                                (dtpDatum_do.Value.Date >= rez.Datum_od.Date && dtpDatum_do.Value.Date <= rez.Datum_do.Date))
                                            {
                                                privDatumOd = rez.Datum_od.Date;
                                                privDatumDo = rez.Datum_do.Date;
                                                postojiRez = true;
                                            }
                                            if (dtpDatum_od.Value.Date < rez.Datum_od.Date && dtpDatum_do.Value.Date > rez.Datum_do.Date)
                                            {
                                                privDatumOd = rez.Datum_od.Date;
                                                privDatumDo = rez.Datum_do.Date;
                                                postojiRez = true;
                                            }

                                            if (postojiRez)
                                            {
                                                MessageBox.Show("Postoji rezervacija od " + privDatumOd.Date.ToString("dd.MM.yyyy.") + " - " + privDatumDo.Date.ToString("dd.MM.yyyy."), "Greska");
                                                break;
                                            }
                                            else
                                            {
                                                r.Datum_od = dtpDatum_od.Value.Date;
                                                r.Datum_do = dtpDatum_do.Value.Date;
                                                r.Cena = ((dtpDatum_do.Value.Date - dtpDatum_od.Value.Date).Days + 1) * p.CenaDan;
                                                FileStream fs = File.OpenWrite(putanjaRez);
                                                bf.Serialize(fs, rezervacije);
                                                fs.Dispose();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Željeni datum izlazi iz okvira ponude.", "Greska");
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                    MessageBox.Show("Proverite datume.", "Greska");
            }
            else
                MessageBox.Show("Nista izabrali rezervaciju.", "Greska");

            lsbSpisakRezervacija.Items.Clear();
            bool uspesnoIdKupca = int.TryParse(cbbKupci.SelectedItem.ToString().Substring(0, 4), out int idKupca);
            if (uspesnoIdKupca)
            {
                foreach (Rezervacija r in rezervacije)
                    if (r.IdKupca == idKupca)
                        lsbSpisakRezervacija.Items.Add(r);
            }
            else
            {
                MessageBox.Show("Nepoznata greška. Forma će se zatvoriti.", "Greska");
                this.Close();
            }
        }
    }
}
