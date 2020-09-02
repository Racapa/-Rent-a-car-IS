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
    public partial class frmKupac : Form
    {
        string putanjaRez = "rezervacije.rac";
        string putanjaKup = "korisnici.rac";
        List<Rezervacija> rezervacije;
        List<Korisnik> korisnici;
        public frmKupac()
        {
            InitializeComponent();
            rezervacije = new List<Rezervacija>();
            korisnici = new List<Korisnik>();
        }

        private void FrmKupac_Load(object sender, EventArgs e)
        {
            lblUlogovaniKupac.Text = "Ulogovani ste kao: " + frmPocetna.aktivniKorisnik; //Prikaz korisnickog imena kojim se ulogovalo
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fsr, fsk;
            if (File.Exists(putanjaRez) && File.Exists(putanjaKup)) //Ako postoje datoteke sa rezervacijama i kupcima
            {
                fsr = File.OpenRead(putanjaRez);
                fsk = File.OpenRead(putanjaKup);
                rezervacije = bf.Deserialize(fsr) as List<Rezervacija>; //Ucitavanje rezervacija
                korisnici = bf.Deserialize(fsk) as List<Korisnik>; //Ucitavanje kupaca
                foreach (Korisnik k in korisnici.OfType<Kupac>())
                    if (frmPocetna.aktivniKorisnik.Equals(k.Ime))
                    {
                        foreach (Rezervacija r in rezervacije)
                            if (r.IdKupca == k.Id)
                                lsbSpisakRezervacija.Items.Add(r.ToString()); //Dodavanje svih postojecih rezervacija za ulogovanog korisnika
                        break;
                    }
                fsr.Dispose();
                fsk.Dispose();
                rezervacije.Clear();
                korisnici.Clear();
            }
        }

        private void BtnRezervisi_Click(object sender, EventArgs e) //Prelazak na formu za rezervisanje vozila
        {
            frmRezervacija frmRezervacija = new frmRezervacija();
            frmRezervacija.StartPosition = FormStartPosition.CenterScreen;
            frmRezervacija.Show();
            this.Hide();
            frmRezervacija.FormClosed += FrmRezervacija_FormClosed; //Kad se zavrsi rezervaija, preusmeravanje na pocetnu formu za kupca
        }

        private void FrmRezervacija_FormClosed(object sender, FormClosedEventArgs e) //Isto kao i za prvo ucitavanje pocetne forme za kupca, prikazivanje i novonastale rezervacije ako je izvrsena
        {
            lsbSpisakRezervacija.Items.Clear();
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fsr, fsk;
            if (File.Exists(putanjaRez) && File.Exists(putanjaKup))
            {
                fsr = File.OpenRead(putanjaRez);
                fsk = File.OpenRead(putanjaKup);
                rezervacije = bf.Deserialize(fsr) as List<Rezervacija>;
                korisnici = bf.Deserialize(fsk) as List<Korisnik>;
                foreach (Kupac k in korisnici.OfType<Kupac>())
                    if (frmPocetna.aktivniKorisnik.Equals(k.Ime))
                    {
                        foreach (Rezervacija r in rezervacije)
                            if (r.IdKupca == k.Id)
                                lsbSpisakRezervacija.Items.Add(r.ToString());
                        break;
                    }
                fsr.Dispose();
                fsk.Dispose();
                rezervacije.Clear();
                korisnici.Clear();
            }
            this.Show();
        }

        private void BtnBrisiRezervaciju_Click(object sender, EventArgs e)
        {
            if (lsbSpisakRezervacija.SelectedIndex > -1) //Ako je izabrana rezervacija za brisanje
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = File.OpenRead(putanjaRez);
                rezervacije = bf.Deserialize(fs) as List<Rezervacija>;
                fs.Dispose();
                foreach (Rezervacija r in rezervacije)
                    if (r.ToString().Equals(lsbSpisakRezervacija.SelectedItem.ToString()))
                    {
                        rezervacije.Remove(r);
                        lsbSpisakRezervacija.Items.Remove(r.ToString());
                        MessageBox.Show("Uspešno.");
                        break;
                    }
                fs = File.OpenWrite(putanjaRez);
                bf.Serialize(fs, rezervacije);
                fs.Dispose();
                rezervacije.Clear();
            }
            else
                MessageBox.Show("Odaberite rezervaciju.", "Greska");
        }

        private void BtnOdjava_Click(object sender, EventArgs e)
        {
            this.Close(); //Odjavljivanje kupca i povratak na pocetnu formu za prijavljivanje
        }
    }
}
