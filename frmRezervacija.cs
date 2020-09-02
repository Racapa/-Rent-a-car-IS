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
    public partial class frmRezervacija : Form
    {
        List<Automobil> automobili;
        List<Ponuda> ponude;
        List<Rezervacija> rezervacije;
        List<Korisnik> korisnici;
        string putanjaAuto = "automobili.rac";
        string putanjaPonude = "ponude.rac";
        string putanjaKupac = "korisnici.rac";
        string putanjaRez = "rezervacije.rac";
        public frmRezervacija()
        {
            InitializeComponent();
            automobili = new List<Automobil>();
            ponude = new List<Ponuda>();
            rezervacije = new List<Rezervacija>();
            korisnici = new List<Korisnik>();
        }

        private void DtpDatumOd_ValueChanged(object sender, EventArgs e) //Prikazivanje ukupne cene rezervacije za izabrano vozilo
        {
            txtUkupnaCena.Text = "";
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fsa;
            FileStream fsp;
            if (File.Exists(putanjaAuto))
            {
                fsa = File.OpenRead(putanjaAuto);
                automobili = bf.Deserialize(fsa) as List<Automobil>; //Preuzimanje automobila iz datoteke
                fsa.Dispose();
            }
            if (File.Exists(putanjaPonude))
            {
                fsp = File.OpenRead(putanjaPonude);
                ponude = bf.Deserialize(fsp) as List<Ponuda>; //Preuzimanje ponuda iz datoteke
                fsp.Dispose();
            }
            int idAuta = 0;
            if (automobili.Count > 0 && ponude.Count > 0)
            {
                foreach (Automobil a in automobili) //Uzimanje ID auta za uporedjivanje
                    if (cbbMarka.Text.Equals(a.Marka) && cbbModel.Text.Equals(a.Model) &&
                        cbbGodiste.Text.Equals(a.Godiste.ToString()) && cbbKubikaza.Text.Equals(a.Kubikaza.ToString()) &&
                        cbbKaroserija.Text.Equals(a.Karoserija) && cbbBrojVrata.Text.Equals(a.BrVrata.ToString()) &&
                        cbbGorivo.Text.Equals(a.Gorivo) && cbbPogon.Text.Equals(a.Pogon) &&
                        cbbMenjac.Text.Equals(a.Menjac))
                    {
                        idAuta = a.Id;
                        break;
                    }
                foreach (Ponuda p in ponude)
                {
                    if (dtpDatumDo.Value.Date >= dtpDatumOd.Value.Date) //Ako datumski okvir ima smisla
                    {
                        if (p.IdAuta == idAuta)
                        {
                            if (dtpDatumOd.Value.Date >= p.Datum_od.Date && dtpDatumOd.Value.Date <= p.Datum_do.Date &&
                                dtpDatumDo.Value.Date >= p.Datum_od.Date && dtpDatumDo.Value.Date <= p.Datum_do.Date)
                            {
                                txtUkupnaCena.Text = (((dtpDatumDo.Value.Date - dtpDatumOd.Value.Date).Days + 1) * p.CenaDan).ToString(); //Racunanje ukupne cene za potencijalnu rezervaciju
                                break;
                            }
                        }
                    }
                    else
                    {
                        txtUkupnaCena.Text = "Nemoguće izračunati";
                        break;
                    }
                }
            }
            if(txtUkupnaCena.Text.Equals(""))
                txtUkupnaCena.Text = "Nemoguće izračunati";
        }

        private void DtpDatumDo_ValueChanged(object sender, EventArgs e) //Prikazivanje ukupne cene rezervacije za izabrano vozilo, vazi isto kao i za prethodno
        {
            txtUkupnaCena.Text = "";
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fsa;
            FileStream fsp;
            if (File.Exists(putanjaAuto))
            {
                fsa = File.OpenRead(putanjaAuto);
                automobili = bf.Deserialize(fsa) as List<Automobil>;
                fsa.Dispose();
            }
            if (File.Exists(putanjaPonude))
            {
                fsp = File.OpenRead(putanjaPonude);
                ponude = bf.Deserialize(fsp) as List<Ponuda>;
                fsp.Dispose();
            }
            
            int idAuta = 0;
            if (automobili.Count > 0 && ponude.Count > 0)
            {
                foreach (Automobil a in automobili)
                    if (cbbMarka.Text.Equals(a.Marka) && cbbModel.Text.Equals(a.Model) &&
                        cbbGodiste.Text.Equals(a.Godiste.ToString()) && cbbKubikaza.Text.Equals(a.Kubikaza.ToString()) &&
                        cbbKaroserija.Text.Equals(a.Karoserija) && cbbBrojVrata.Text.Equals(a.BrVrata.ToString()) &&
                        cbbGorivo.Text.Equals(a.Gorivo) && cbbPogon.Text.Equals(a.Pogon) &&
                        cbbMenjac.Text.Equals(a.Menjac))
                    {
                        idAuta = a.Id;
                        break;
                    }
                foreach (Ponuda p in ponude)
                {
                    if (dtpDatumDo.Value.Date >= dtpDatumOd.Value.Date)
                    {
                        if (p.IdAuta == idAuta)
                        {
                            if (dtpDatumOd.Value.Date >= p.Datum_od.Date && dtpDatumOd.Value.Date <= p.Datum_do.Date &&
                                dtpDatumDo.Value.Date >= p.Datum_od.Date && dtpDatumDo.Value.Date <= p.Datum_do.Date)
                            {
                                txtUkupnaCena.Text = (((dtpDatumDo.Value.Date - dtpDatumOd.Value.Date).Days + 1) * p.CenaDan).ToString();
                                break;
                            }
                        }
                    }
                    else
                    {
                        txtUkupnaCena.Text = "Nemoguće izračunati";
                        break;
                    }
                }
            }
            if (txtUkupnaCena.Text.Equals(""))
                txtUkupnaCena.Text = "Nemoguće izračunati";
        }

        private void FrmRezervacija_Load(object sender, EventArgs e)
        {
            BinaryFormatter bf = new BinaryFormatter();
            if (File.Exists(putanjaAuto))
            {
                FileStream fs = File.OpenRead(putanjaAuto);
                automobili = bf.Deserialize(fs) as List<Automobil>;
                fs.Dispose();
            }

            if (automobili.Count == 0)
                MessageBox.Show("Nema automobila.", "Upozorenje");
            else
            {
                foreach (Automobil a in automobili)
                    if(!cbbMarka.Items.Contains(a.Marka))
                        cbbMarka.Items.Add(a.Marka); //Popunjavanje ComboBox-a sa svim markama iz datoteke
            }
        }

        private void BtnRezervisi_Click(object sender, EventArgs e) //Pravljenje nove rezervacije
        {
            bool uspesnaRezervacija = false;
            int idAuta = 0;
            int idKupca = 0;
            int cenaUkupno = 0;
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fsk = File.OpenRead(putanjaKupac);
            FileStream fsa;
            FileStream fsp;
            FileStream fsr;
            if (File.Exists(putanjaAuto))
            {
                fsa = File.OpenRead(putanjaAuto);
                automobili = bf.Deserialize(fsa) as List<Automobil>;
                fsa.Dispose();
            }
            if (File.Exists(putanjaPonude))
            {
                fsp = File.OpenRead(putanjaPonude);
                ponude = bf.Deserialize(fsp) as List<Ponuda>;
                fsp.Dispose();
            }
            if (File.Exists(putanjaRez))
            {
                fsr = File.OpenRead(putanjaRez);
                rezervacije = bf.Deserialize(fsr) as List<Rezervacija>;
                fsr.Dispose();
            }
            korisnici = bf.Deserialize(fsk) as List<Korisnik>;
            fsk.Dispose();
            foreach (Kupac k in korisnici.OfType<Kupac>())
                if (k.Korisnicko_ime.Equals(frmPocetna.aktivniKorisnik))
                {
                    idKupca = k.Id;
                    break;
                }
            if (automobili.Count > 0 && ponude.Count > 0) //Ako postoje automobili i ponude
            {
                foreach (Automobil a in automobili)
                    if (cbbMarka.Text.Equals(a.Marka) && cbbModel.Text.Equals(a.Model) &&
                        cbbGodiste.Text.Equals(a.Godiste.ToString()) && cbbKubikaza.Text.Equals(a.Kubikaza.ToString()) &&
                        cbbKaroserija.Text.Equals(a.Karoserija) && cbbBrojVrata.Text.Equals(a.BrVrata.ToString()) &&
                        cbbGorivo.Text.Equals(a.Gorivo) && cbbPogon.Text.Equals(a.Pogon) &&
                        cbbMenjac.Text.Equals(a.Menjac))
                    {
                        idAuta = a.Id;
                        break;
                    }
                bool postojiPon = false;
                foreach (Ponuda p in ponude)
                    if (dtpDatumOd.Value.Date >= p.Datum_od.Date && dtpDatumOd.Value.Date <= p.Datum_do.Date &&
                       dtpDatumDo.Value.Date >= p.Datum_od.Date && dtpDatumDo.Value.Date <= p.Datum_do.Date)
                        postojiPon = true;
                foreach (Ponuda p in ponude)
                {
                    if (dtpDatumOd.Value.Date <= dtpDatumDo.Value.Date) //Rezervacija moze i u trajanju od jednog dana
                    {
                        if (p.IdAuta == idAuta)
                        {
                            if (postojiPon) //Ako su zeljeni datumi u datumskom opsegu ponude
                            {
                                if (rezervacije.Count > 0) //Ako postoje rezervacije
                                {
                                    bool postojiRezSaIda = false;
                                    foreach (Rezervacija r in rezervacije) //Da li postoji rezervacija za izabrani auto
                                        if (r.IdAuta == idAuta)
                                        {
                                            postojiRezSaIda = true;
                                            break;
                                        }
                                    if (postojiRezSaIda) //Ako postoji rezervacija za izabrani auto, nova ne sme da se preklapa sa postojecom
                                    {
                                        DateTime privDatumOd = DateTime.MinValue;
                                        DateTime privDatumDo = DateTime.MaxValue;
                                        bool zauzeto = false;
                                        foreach (Rezervacija r in rezervacije)
                                        {
                                            if (idAuta == r.IdAuta)
                                            {
                                                if ((dtpDatumOd.Value.Date >= r.Datum_od.Date && dtpDatumOd.Value.Date <= r.Datum_do.Date) ||
                                                    (dtpDatumDo.Value.Date >= r.Datum_od.Date && dtpDatumDo.Value.Date <= r.Datum_do.Date) ||
                                                    (dtpDatumOd.Value.Date < r.Datum_od.Date && dtpDatumDo.Value.Date > r.Datum_do.Date)) //Ako se zeljena rezervacija poklapa sa postojecom
                                                {
                                                    zauzeto = true;
                                                    privDatumOd = r.Datum_od.Date;
                                                    privDatumDo = r.Datum_do.Date; //Preuzimanje datuma postojece rezervacije sa kojom se preklapa zeljena, radi ispisivanja
                                                    break;
                                                }
                                            }
                                        }
                                        if (!zauzeto) //Ako se zeljena rezervacija ne poklapa sa nekom postojecom 
                                        {
                                            foreach(Ponuda pon in ponude)
                                                if(pon.IdAuta == idAuta)
                                                    if(dtpDatumOd.Value.Date >= pon.Datum_od.Date && dtpDatumDo.Value.Date <= pon.Datum_do.Date)
                                                    {
                                                        cenaUkupno = ((dtpDatumDo.Value.Date - dtpDatumOd.Value.Date).Days + 1)* pon.CenaDan; //Racunanje cene rezervacije ako je zeljeni datum rezervaciju u okviru ponude
                                                    }
                                            Rezervacija rez = new Rezervacija(idAuta, idKupca, dtpDatumOd.Value.Date, dtpDatumDo.Value.Date, cenaUkupno);
                                            rezervacije.Add(rez);
                                            txtUkupnaCena.Text = cenaUkupno.ToString();
                                            uspesnaRezervacija = true;
                                            MessageBox.Show("Uspešno.");
                                            break;
                                        }
                                        else
                                        {
                                            MessageBox.Show("Postoji rezervacija za vozilo od " + privDatumOd.ToString("dd.MM.yyyy.") + " - " + privDatumDo.ToString("dd.MM.yyyy."), "Greska");
                                            break;
                                        }
                                    }
                                    else //Ako ne postoji rezervacija za zeljeni auto
                                    {
                                        foreach (Ponuda pon in ponude)
                                            if (pon.IdAuta == idAuta)
                                                if (dtpDatumOd.Value.Date >= pon.Datum_od.Date && dtpDatumDo.Value.Date <= pon.Datum_do.Date)
                                                {
                                                    cenaUkupno = ((dtpDatumDo.Value.Date - dtpDatumOd.Value.Date).Days + 1) * pon.CenaDan; //Racunanje cene rezervacije ako je zeljeni datum rezervaciju u okviru ponude
                                                }
                                        Rezervacija rez = new Rezervacija(idAuta, idKupca, dtpDatumOd.Value.Date, dtpDatumDo.Value.Date, cenaUkupno);
                                        rezervacije.Add(rez);
                                        txtUkupnaCena.Text = cenaUkupno.ToString();
                                        uspesnaRezervacija = true;
                                        MessageBox.Show("Uspešno.");
                                        break;
                                    }
                                }
                                else //Ako ne postoje rezervacije
                                {
                                    foreach (Ponuda pon in ponude)
                                        if (pon.IdAuta == idAuta)
                                            if (dtpDatumOd.Value.Date >= pon.Datum_od.Date && dtpDatumDo.Value.Date <= pon.Datum_do.Date)
                                            {
                                                cenaUkupno = ((dtpDatumDo.Value.Date - dtpDatumOd.Value.Date).Days + 1) * pon.CenaDan;
                                            }
                                    Rezervacija rez = new Rezervacija(idAuta, idKupca, dtpDatumOd.Value.Date, dtpDatumDo.Value.Date, cenaUkupno);
                                    rezervacije.Add(rez);
                                    txtUkupnaCena.Text = cenaUkupno.ToString();
                                    uspesnaRezervacija = true;
                                    MessageBox.Show("Uspešno.");
                                    break;
                                }
                            }
                            else //Ako zeljeni datumi nisu u datumskom opsegu ponude
                            {
                                MessageBox.Show("Nije moguće rezervisati vozilo.", "Greska");
                                break;
                            }
                        }
                    }
                    else //Ako datumski opseg rezervacije nema smisla
                    {
                        MessageBox.Show("Nije moguće rezervisati vozilo.", "Greska");
                        break;
                    }
                }
                fsr = File.OpenWrite(putanjaRez);
                bf.Serialize(fsr, rezervacije);
                fsr.Dispose();
            }
            else //Ako ne postoje automobili i ponude, nemoguce je napraviti rezervaciju
                MessageBox.Show("Nije moguće rezervisati vozilo.", "Greska");
            rezervacije.Clear();
            if (uspesnaRezervacija) //Ako je izvrsena rezervacija, prelazak na pocetnu formu za kupca
                this.Close();
        }

        private void CbbMarka_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cbbModel.Items.Clear();
            cbbGodiste.Items.Clear();
            cbbKubikaza.Items.Clear();
            cbbKaroserija.Items.Clear();
            cbbBrojVrata.Items.Clear();
            cbbGorivo.Items.Clear();
            cbbPogon.Items.Clear();
            cbbMenjac.Items.Clear();
            cbbModel.Text = "";
            cbbGodiste.Text = "";
            cbbKubikaza.Text = "";
            cbbKaroserija.Text = "";
            cbbBrojVrata.Text = "";
            cbbGorivo.Text = "";
            cbbPogon.Text = "";
            cbbMenjac.Text = "";
            foreach (Automobil a in automobili)
            {
                if (cbbMarka.SelectedItem.Equals(a.Marka))
                    if (!cbbModel.Items.Contains(a.Model))
                        cbbModel.Items.Add(a.Model);
            }
        }

        private void CbbModel_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cbbGodiste.Items.Clear();
            cbbKubikaza.Items.Clear();
            cbbKaroserija.Items.Clear();
            cbbBrojVrata.Items.Clear();
            cbbGorivo.Items.Clear();
            cbbPogon.Items.Clear();
            cbbMenjac.Items.Clear();
            cbbGodiste.Text = "";
            cbbKubikaza.Text = "";
            cbbKaroserija.Text = "";
            cbbBrojVrata.Text = "";
            cbbGorivo.Text = "";
            cbbPogon.Text = "";
            cbbMenjac.Text = "";
            foreach (Automobil a in automobili)
                if (cbbModel.SelectedItem.Equals(a.Model))
                    if (!cbbGodiste.Items.Contains(a.Godiste))
                        cbbGodiste.Items.Add(a.Godiste);
        }

        private void CbbGodiste_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cbbKubikaza.Items.Clear();
            cbbKaroserija.Items.Clear();
            cbbBrojVrata.Items.Clear();
            cbbGorivo.Items.Clear();
            cbbPogon.Items.Clear();
            cbbMenjac.Items.Clear();
            cbbKubikaza.Text = "";
            cbbKaroserija.Text = "";
            cbbBrojVrata.Text = "";
            cbbGorivo.Text = "";
            cbbPogon.Text = "";
            cbbMenjac.Text = "";
            foreach (Automobil a in automobili)
                if (cbbGodiste.SelectedItem.Equals(a.Godiste) && cbbModel.SelectedItem.Equals(a.Model))
                    if (!cbbKubikaza.Items.Contains(a.Kubikaza))
                        cbbKubikaza.Items.Add(a.Kubikaza);
        }

        private void CbbKubikaza_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cbbKaroserija.Items.Clear();
            cbbBrojVrata.Items.Clear();
            cbbGorivo.Items.Clear();
            cbbPogon.Items.Clear();
            cbbMenjac.Items.Clear();
            cbbKaroserija.Text = "";
            cbbBrojVrata.Text = "";
            cbbGorivo.Text = "";
            cbbPogon.Text = "";
            cbbMenjac.Text = "";
            foreach (Automobil a in automobili)
                if(cbbKubikaza.SelectedItem.Equals(a.Kubikaza) && cbbGodiste.SelectedItem.Equals(a.Godiste) && 
                    cbbModel.SelectedItem.Equals(a.Model))
                    if (!cbbKaroserija.Items.Contains(a.Karoserija))
                        cbbKaroserija.Items.Add(a.Karoserija);
        }

        private void CbbKaroserija_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cbbBrojVrata.Items.Clear();
            cbbGorivo.Items.Clear();
            cbbPogon.Items.Clear();
            cbbMenjac.Items.Clear();
            cbbBrojVrata.Text = "";
            cbbGorivo.Text = "";
            cbbPogon.Text = "";
            cbbMenjac.Text = "";
            foreach (Automobil a in automobili)
                if (cbbKubikaza.SelectedItem.Equals(a.Kubikaza) && cbbGodiste.SelectedItem.Equals(a.Godiste) &&
                    cbbModel.SelectedItem.Equals(a.Model) && cbbKaroserija.SelectedItem.Equals(a.Karoserija))
                    if (!cbbBrojVrata.Items.Contains(a.BrVrata))
                        cbbBrojVrata.Items.Add(a.BrVrata);
        }

        private void CbbBrojVrata_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cbbGorivo.Items.Clear();
            cbbPogon.Items.Clear();
            cbbMenjac.Items.Clear();
            cbbGorivo.Text = "";
            cbbPogon.Text = "";
            cbbMenjac.Text = "";
            foreach (Automobil a in automobili)
                if (cbbKubikaza.SelectedItem.Equals(a.Kubikaza) && cbbGodiste.SelectedItem.Equals(a.Godiste) &&
                    cbbModel.SelectedItem.Equals(a.Model) && cbbKaroserija.SelectedItem.Equals(a.Karoserija) &&
                    cbbBrojVrata.SelectedItem.Equals(a.BrVrata))
                    if (!cbbGorivo.Items.Contains(a.Gorivo))
                        cbbGorivo.Items.Add(a.Gorivo);
        }

        private void CbbGorivo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cbbPogon.Items.Clear();
            cbbMenjac.Items.Clear();
            cbbPogon.Text = "";
            cbbMenjac.Text = "";
            foreach (Automobil a in automobili)
                if (cbbKubikaza.SelectedItem.Equals(a.Kubikaza) && cbbGodiste.SelectedItem.Equals(a.Godiste) &&
                    cbbModel.SelectedItem.Equals(a.Model) && cbbKaroserija.SelectedItem.Equals(a.Karoserija) &&
                    cbbBrojVrata.SelectedItem.Equals(a.BrVrata) && cbbGorivo.SelectedItem.Equals(a.Gorivo))
                    if (!cbbPogon.Items.Contains(a.Pogon))
                        cbbPogon.Items.Add(a.Pogon);
        }

        private void CbbPogon_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cbbMenjac.Items.Clear();
            cbbMenjac.Text = "";
            foreach (Automobil a in automobili)
                if (cbbKubikaza.SelectedItem.Equals(a.Kubikaza) && cbbGodiste.SelectedItem.Equals(a.Godiste) &&
                    cbbModel.SelectedItem.Equals(a.Model) && cbbKaroserija.SelectedItem.Equals(a.Karoserija) &&
                    cbbBrojVrata.SelectedItem.Equals(a.BrVrata) && cbbGorivo.SelectedItem.Equals(a.Gorivo) &&
                    cbbPogon.SelectedItem.Equals(a.Pogon))
                    if (!cbbMenjac.Items.Contains(a.Menjac))
                        cbbMenjac.Items.Add(a.Menjac);
        }

        private void BtnPrikazTermina_Click(object sender, EventArgs e) //Prikaz ponuda za izabrani suto
        {
            txtUkupnaCena.Text = "";
            bool uspesno = true;
            int idIzabranog = 0;
            foreach (ComboBox cbb in this.Controls.OfType<ComboBox>())
                if (cbb.Text == "")
                    uspesno = false;
            if(!uspesno)
                MessageBox.Show("Niste odabrali sve kriterijume.", "Greska");
            else
            {
                lsbTermini.Items.Clear();
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = File.OpenRead(putanjaPonude);
                ponude = bf.Deserialize(fs) as List<Ponuda>;
                fs.Dispose();
                foreach (Automobil a in automobili)
                    if (cbbMarka.Text.Equals(a.Marka) && cbbModel.Text.Equals(a.Model) &&
                        cbbGodiste.Text.Equals(a.Godiste.ToString()) && cbbKubikaza.Text.Equals(a.Kubikaza.ToString()) &&
                        cbbKaroserija.Text.Equals(a.Karoserija) && cbbBrojVrata.Text.Equals(a.BrVrata.ToString()) &&
                        cbbGorivo.Text.Equals(a.Gorivo) && cbbPogon.Text.Equals(a.Pogon) &&
                        cbbMenjac.Text.Equals(a.Menjac))
                    {
                        idIzabranog = a.Id;
                        break;
                    }
                bool postojiPonuda = false;
                foreach (Ponuda p in ponude)
                    if (idIzabranog == p.IdAuta)
                    {
                        lsbTermini.Items.Add(p.Datum_od.ToString("dd.MM.yyyy.") + " - " + p.Datum_do.ToString("dd.MM.yyyy.") + " Cena:" + p.CenaDan + " din. po danu"); //Popunjavanje ListBox-a sa svim ponudama za vozilo
                        postojiPonuda = true;
                    }
                if (!postojiPonuda)
                    MessageBox.Show("Ne postoje ponude za izabrano vozilo.", "Upozorenje");

                if (automobili.Count > 0 && ponude.Count > 0)
                {
                    foreach (Automobil a in automobili)
                        if (cbbMarka.Text.Equals(a.Marka) && cbbModel.Text.Equals(a.Model) &&
                            cbbGodiste.Text.Equals(a.Godiste.ToString()) && cbbKubikaza.Text.Equals(a.Kubikaza.ToString()) &&
                            cbbKaroserija.Text.Equals(a.Karoserija) && cbbBrojVrata.Text.Equals(a.BrVrata.ToString()) &&
                            cbbGorivo.Text.Equals(a.Gorivo) && cbbPogon.Text.Equals(a.Pogon) &&
                            cbbMenjac.Text.Equals(a.Menjac))
                        {
                            idIzabranog = a.Id;
                            break;
                        }
                    foreach (Ponuda p in ponude)
                    {
                        if (dtpDatumDo.Value.Date >= dtpDatumOd.Value.Date)
                        {
                            if (p.IdAuta == idIzabranog)
                            {
                                if (dtpDatumOd.Value.Date >= p.Datum_od.Date && dtpDatumOd.Value.Date <= p.Datum_do.Date &&
                                    dtpDatumDo.Value.Date >= p.Datum_od.Date && dtpDatumDo.Value.Date <= p.Datum_do.Date)
                                {
                                    txtUkupnaCena.Text = (((dtpDatumDo.Value.Date - dtpDatumOd.Value.Date).Days + 1) * p.CenaDan).ToString(); //Racunanje ukupne cene potencijalne rezervacije za razlicite datumske opsege
                                    break;
                                }
                            }
                        }
                        else
                        {
                            txtUkupnaCena.Text = "Nemoguće izračunati";
                            break;
                        }
                    }
                }
                if (txtUkupnaCena.Text.Equals(""))
                    txtUkupnaCena.Text = "Nemoguće izračunati";
            }
        }
    }
}
