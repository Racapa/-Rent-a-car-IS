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
    public partial class frmAdmin : Form
    {
        List<Automobil> automobili;
        List<Korisnik> korisnici;
        List<Ponuda> ponude;
        List<Rezervacija> rezervacije;
        private static int brojAutomobila;
        private static float ugao = 0;
        HashSet<int> idBrojevi = new HashSet<int>();
        public frmAdmin()
        {
            InitializeComponent();
            automobili = new List<Automobil>();
            korisnici = new List<Korisnik>();
            ponude = new List<Ponuda>();
            rezervacije = new List<Rezervacija>();
        }

        private void FrmAdmin_Load(object sender, EventArgs e)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs;
            string putanjaAuto = "automobili.rac";
            if (File.Exists(putanjaAuto))
            {
                fs = File.OpenRead(putanjaAuto);
                automobili = bf.Deserialize(fs) as List<Automobil>;
                foreach (Automobil a in automobili)
                {
                    cbbPonuda.Items.Add(a.ToString());
                    cbbAuto.Items.Add(a.ToString());
                }
                fs.Dispose();
                automobili.Clear();
            }

            string putanjaKor = "korisnici.rac";
            if (File.Exists(putanjaKor))
            {
                fs = File.OpenRead(putanjaKor);
                korisnici = bf.Deserialize(fs) as List<Korisnik>;
                foreach (Korisnik k in korisnici)
                    cbbKorisnik.Items.Add(k.ToString());
                foreach (Kupac kup in korisnici.OfType<Kupac>())
                    cbbKupac.Items.Add(kup.ToString());
                fs.Dispose();
                korisnici.Clear();
            }

            string putanjaPon = "ponude.rac";
            if (File.Exists(putanjaPon))
            {
                fs = File.OpenRead(putanjaPon);
                ponude = bf.Deserialize(fs) as List<Ponuda>;
                foreach (Ponuda p in ponude)
                    lsbSpisakPonuda.Items.Add(p);
                fs.Dispose();
                ponude.Clear();
            }
            lblBrojcano.Text = "";
            lblProcentualno.Text = "";
        }

        private void BtnAdmUnesiAuto_Click(object sender, EventArgs e)
        {
            string putanja = "automobili.rac";
            BinaryFormatter bf = new BinaryFormatter();
            bool uspGod = int.TryParse(txtGodiste.Text, out int god);
            bool uspKub = int.TryParse(txtKubikaza.Text, out int kub);
            bool uspBrVrata = int.TryParse(txtBrVrata.Text, out int brVrata);

            if (txtMarka.Text.Trim().Length != 0 && txtModel.Text.Trim().Length != 0 &&
                txtGodiste.Text.Trim().Length != 0 && txtKubikaza.Text.Trim().Length != 0 &&
                txtPogon.Text.Trim().Length != 0 && txtVrstaMenjaca.Text.Trim().Length != 0 &&
                txtKaroserija.Text.Trim().Length != 0 && txtGorivo.Text.Trim().Length != 0 &&
                txtBrVrata.Text.Trim().Length != 0)
            {
                if (uspGod == true && uspKub == true && uspBrVrata == true)
                {
                    if (File.Exists(putanja)) //Ako postoji datoteka sa automobilima
                    {
                        FileStream fs = File.OpenRead(putanja);
                        Automobil a = new Automobil(txtMarka.Text, txtModel.Text, god, kub,
                                                txtPogon.Text, txtVrstaMenjaca.Text,
                                                txtKaroserija.Text, txtGorivo.Text, brVrata);
                        automobili = bf.Deserialize(fs) as List<Automobil>;
                        fs.Dispose();
                        automobili.Add(a);                        
                        fs = File.OpenWrite(putanja);
                        bf.Serialize(fs, automobili);
                        fs.Dispose();
                        txtMarka.Clear();
                        txtModel.Clear();
                        txtGodiste.Clear();
                        txtKubikaza.Clear();
                        txtPogon.Clear();
                        txtVrstaMenjaca.Clear();
                        txtKaroserija.Clear();
                        txtGorivo.Clear();
                        txtBrVrata.Clear();
                        txtMarka.Focus();
                        cbbPonuda.Items.Add(automobili.Last<Automobil>().ToString());
                        cbbAuto.Items.Add(automobili.Last<Automobil>().ToString());
                        automobili.Clear();
                        MessageBox.Show("Uspešno.");
                    }
                    else //Ako ne postoji datoteka sa automobilima
                    {
                        Automobil a = new Automobil(txtMarka.Text, txtModel.Text, god, kub,
                                                txtPogon.Text, txtVrstaMenjaca.Text,
                                                txtKaroserija.Text, txtGorivo.Text, brVrata);
                        automobili.Add(a);
                        FileStream fs = File.OpenWrite(putanja);
                        bf.Serialize(fs, automobili);
                        fs.Dispose();
                        txtMarka.Clear();
                        txtModel.Clear();
                        txtGodiste.Clear();
                        txtKubikaza.Clear();
                        txtPogon.Clear();
                        txtVrstaMenjaca.Clear();
                        txtKaroserija.Clear();
                        txtGorivo.Clear();
                        txtBrVrata.Clear();
                        txtMarka.Focus();
                        cbbPonuda.Items.Add(automobili.Last<Automobil>().ToString());
                        cbbAuto.Items.Add(automobili.Last<Automobil>().ToString());
                        automobili.Clear();
                        MessageBox.Show("Uspešno.");
                    }
                }
                else
                    MessageBox.Show("Unesite ispravan podatak.", "Greska");
            }
            else
                MessageBox.Show("Niste popunili sva polja.", "Greska");
            cbbAuto.Text = "";
        }

        private void BtnAdmIzmeniAuto_Click(object sender, EventArgs e)
        {
            string putanja = "automobili.rac";
            string putanjaRez = "rezervacije.rac";
            BinaryFormatter bf = new BinaryFormatter();
            if (cbbAuto.SelectedIndex > -1) //Da li je selektovan auto za izmenu
            {
                bool uspesno = int.TryParse(cbbAuto.SelectedItem.ToString().Substring(0, 4), out int broj); //Da li moze da se konvertuje ID auta u int
                if (File.Exists(putanja))
                {
                    if (uspesno)
                    {
                        FileStream fs = File.OpenRead(putanja);
                        automobili = bf.Deserialize(fs) as List<Automobil>;
                        fs.Dispose();
                        foreach (Automobil a in automobili)
                        {
                            if (a.Id == broj)
                            {
                                bool imaRez = false; //Ako postoji rezervacija za vozilo
                                if (File.Exists(putanjaRez))
                                {
                                    FileStream fsr = File.OpenRead(putanjaRez);
                                    rezervacije = bf.Deserialize(fsr) as List<Rezervacija>;
                                    fsr.Dispose();
                                    foreach (Rezervacija r in rezervacije)
                                        if (r.IdAuta == a.Id)
                                            imaRez = true;
                                }

                                if (!imaRez) //Ako ne postoji rezervacija za vozilo
                                {
                                    foreach (Control c in gbAutomobil.Controls.OfType<TextBox>())
                                    {
                                        bool moze;
                                        int pom_broj;
                                        if (c.Text.Trim().Length != 0)
                                            switch (c.Name)
                                            {
                                                case "txtMarka": a.Marka = c.Text; break;
                                                case "txtModel": a.Model = c.Text; break;
                                                case "txtGodiste":
                                                    {
                                                        moze = int.TryParse(c.Text, out pom_broj);
                                                        if (moze)
                                                            a.Godiste = pom_broj;
                                                        else
                                                            MessageBox.Show("Pogresan unos za izmenu.", "Greska");
                                                        break;
                                                    }
                                                case "txtKubikaza":
                                                    {
                                                        moze = int.TryParse(c.Text, out pom_broj);
                                                        if (moze)
                                                            a.Kubikaza = pom_broj;
                                                        else
                                                            MessageBox.Show("Pogresan unos za izmenu.", "Greska");
                                                        break;
                                                    }
                                                case "txtPogon": a.Pogon = c.Text; break;
                                                case "txtVrstaMenjaca": a.Menjac = c.Text; break;
                                                case "txtKaroserija": a.Karoserija = c.Text; break;
                                                case "txtGorivo": a.Gorivo = c.Text; break;
                                                case "txtBrVrata":
                                                    {
                                                        moze = int.TryParse(c.Text, out pom_broj);
                                                        if (moze)
                                                            a.BrVrata = pom_broj;
                                                        else
                                                            MessageBox.Show("Pogresan unos za izmenu.", "Greska");
                                                        break;
                                                    }
                                            }
                                    }
                                    MessageBox.Show("Uspešno.");
                                }
                                else
                                    MessageBox.Show("Nemoguće. Postoji rezervacija za vozilo.", "Greska");
                                cbbAuto.Items.Clear();
                                cbbPonuda.Items.Clear();
                                foreach (Automobil a1 in automobili)
                                {
                                    cbbAuto.Items.Add(a1.ToString());
                                    cbbPonuda.Items.Add(a1.ToString());
                                }
                            }
                        }
                        fs = File.OpenWrite(putanja);
                        bf.Serialize(fs, automobili);
                        fs.Dispose();
                        cbbAuto.Text = "";
                        automobili.Clear();
                    }
                }
            }
            else
                MessageBox.Show("Niste odabrali auto sa liste.", "Greska");
            cbbAuto.Text = "";
        }

        private void BtnAdmBrisiAuto_Click(object sender, EventArgs e)
        {
            string putanjaAuto = "automobili.rac";
            string putanjaRez = "rezervacije.rac";
            string putanjaPon = "ponude.rac";
            if (cbbAuto.SelectedIndex > -1)
            {
                if (File.Exists(putanjaAuto))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    FileStream fsa = File.OpenRead(putanjaAuto);
                    FileStream fsp, fsr;
                    if (File.Exists(putanjaPon))
                    {
                        fsp = File.OpenRead(putanjaPon);
                        ponude = bf.Deserialize(fsp) as List<Ponuda>;
                        fsp.Dispose();
                    }
                    if (File.Exists(putanjaRez))
                    {
                        fsr = File.OpenRead(putanjaRez);
                        rezervacije = bf.Deserialize(fsr) as List<Rezervacija>;
                        fsr.Dispose();
                    }
                    automobili = bf.Deserialize(fsa) as List<Automobil>;
                    fsa.Dispose();
                    bool uspesno = int.TryParse(cbbAuto.SelectedItem.ToString().Substring(0, 4), out int broj); //Da li je uspela konverzija ID auta u int
                    bool imaPonuda = false;
                    if (uspesno)
                        foreach (Automobil a in automobili)
                            if (a.Id == broj)
                            {
                                foreach (Ponuda p in ponude)
                                    if (p.IdAuta == a.Id)
                                        imaPonuda = true;
                                if (imaPonuda) //Ako postoji ponuda za brisano vozilo
                                {
                                    DialogResult dr = MessageBox.Show("Postoji ponuda i moguće rezervacije za vozilo, i biće obrisani. Nastavak?", "Upozorenje", MessageBoxButtons.YesNo); //Da li zeli da obrise ponude i rezervacije za vozilo koje se brise
                                    if (dr == DialogResult.Yes) //Ako zeli
                                    {
                                        ponude.RemoveAll(p => p.IdAuta == a.Id); //Brisanje ponuda za vozilo
                                        rezervacije.RemoveAll(r => r.IdAuta == a.Id); //Brisanje rezervacija za vozilo
                                        automobili.Remove(a);
                                        fsp = File.OpenWrite(putanjaPon);
                                        fsr = File.OpenWrite(putanjaRez);
                                        bf.Serialize(fsp, ponude);
                                        bf.Serialize(fsr, rezervacije);
                                        fsp.Dispose();
                                        fsr.Dispose();
                                        lsbSpisakPonuda.Items.Clear();
                                        lsbSpisakPonuda.Refresh();
                                        cbbPonuda.Text = "";
                                        MessageBox.Show("Uspešno.");
                                        break;
                                    }
                                }
                                else //Ako ne postoji ponuda, ne postoji ni rezervacija
                                {
                                    automobili.Remove(a);
                                    MessageBox.Show("Uspešno.");
                                    break;
                                }
                            }
                    cbbAuto.Items.Clear();
                    cbbPonuda.Items.Clear();
                    foreach (Automobil a in automobili)
                    {
                        cbbAuto.Items.Add(a.ToString());
                        cbbPonuda.Items.Add(a.ToString());
                    }
                    fsa.Dispose();
                    fsa = File.OpenWrite(putanjaAuto);
                    bf.Serialize(fsa, automobili);
                    fsa.Dispose();
                    automobili.Clear();
                    ponude.Clear();
                    rezervacije.Clear();
                }
            }
            cbbAuto.Text = "";
        }

        private void BtnAdmUnesiKorisnika_Click(object sender, EventArgs e)
        {
            bool adm = ckbAdmin.Checked;
            string putanja = "korisnici.rac";
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs;
            if (!txtUnosUser.Text.ToLower().Equals("admin")) //Ne moze da se unese novi korisnik sa username-om "admin" u bilo kojoj varijaciji
            {
                if (!adm) //Ako se unosi korisnik koji nije admin
                {
                    if (txtUnosUser.Text.Trim().Length != 0 && txtUnosLozinka.Text.Trim().Length != 0 &&
                        txtIme.Text.Trim().Length != 0 && txtPrezime.Text.Trim().Length != 0 &&
                        txtJMBG.Text.Trim().Length != 0 && txtDan.Text.Trim().Length != 0 &&
                        txtMesec.Text.Trim().Length != 0 && txtGodina.Text.Trim().Length != 0 &&
                        txtTelefon.Text.Trim().Length != 0) //Da li su uneti svi podaci za kupca
                    {

                        string textDatum = txtMesec.Text + "/" + txtDan.Text + "/" + txtGodina.Text; //Cuvanje datuma rodjenja u string obliku
                        bool uspDatum = DateTime.TryParse(textDatum, out DateTime datum); //Konverzija datuma iz string u DateTime
                        if (uspDatum)
                        {

                            if (File.Exists(putanja)) //Ako postoji datoteka sa korisnicima
                            {
                                fs = File.OpenRead(putanja);
                                korisnici = bf.Deserialize(fs) as List<Korisnik>;
                                fs.Dispose();
                                Kupac k = new Kupac(txtUnosUser.Text, txtUnosLozinka.Text, txtIme.Text, txtPrezime.Text,
                                                    adm, txtJMBG.Text, datum, txtTelefon.Text);
                                korisnici.Add(k as Kupac);
                                fs = File.OpenWrite(putanja);
                                bf.Serialize(fs, korisnici);
                                fs.Dispose();
                                txtUnosUser.Clear();
                                txtUnosLozinka.Clear();
                                txtIme.Clear();
                                txtPrezime.Clear();
                                txtJMBG.Clear();
                                txtDan.Clear();
                                txtMesec.Clear();
                                txtGodina.Clear();
                                txtTelefon.Clear();
                                txtUnosUser.Focus();
                                cbbKorisnik.Items.Add(korisnici.Last<Korisnik>().ToString());
                                korisnici.Clear();
                                cbbKorisnik.Text = "";
                                MessageBox.Show("Uspešno.");
                            }
                            else
                                MessageBox.Show("Nedostaje datoteka, korisnik nije unet.", "Greska");
                        }
                        else
                            MessageBox.Show("Uneli ste neispravne podatke.", "Greska");
                    }
                    else
                        MessageBox.Show("Niste popunili sva polja.", "Greska");
                }
                else //Ako se unosi korisnik koji je admin
                {
                    if (txtUnosUser.Text.Trim().Length != 0 && txtUnosLozinka.Text.Trim().Length != 0 &&
                        txtIme.Text.Trim().Length != 0 && txtPrezime.Text.Trim().Length != 0) //Da li su uneti svi podaci za administratora. 
                    {
                        fs = File.OpenRead(putanja);
                        korisnici = bf.Deserialize(fs) as List<Korisnik>;
                        Korisnik k = new Admin(txtUnosUser.Text, txtUnosLozinka.Text, txtIme.Text, txtPrezime.Text, adm);
                        korisnici.Add(k as Admin);
                        fs.Dispose();
                        fs = File.OpenWrite(putanja);
                        bf.Serialize(fs, korisnici);
                        fs.Dispose();
                        txtUnosUser.Clear();
                        txtUnosLozinka.Clear();
                        txtIme.Clear();
                        txtPrezime.Clear();
                        ckbAdmin.Checked = false;
                        cbbKorisnik.Items.Add(korisnici.Last<Korisnik>().ToString());
                        txtUnosUser.Focus();
                        korisnici.Clear();
                        cbbKorisnik.Text = "";
                        MessageBox.Show("Uspešno.");
                    }
                    else
                        MessageBox.Show("Niste popunili sva polja za admina. Korisnicko ime, lozinka, ime i prezime.", "Greska");
                }
            }
            else
                MessageBox.Show("Zabranjeno korisničko ime.", "Greska");
        }

        private void BtnAdmIzmeniKorisnika_Click(object sender, EventArgs e)
        {
            if (cbbKorisnik.SelectedIndex > -1) //Ako je izabran korisnik iz liste postojecih korisnika
            {
                string putanja = "korisnici.rac";
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = File.OpenRead(putanja);
                korisnici = bf.Deserialize(fs) as List<Korisnik>;
                bool uspesno = int.TryParse(cbbKorisnik.SelectedItem.ToString().Substring(0, 4), out int id); //Konverzija ID korisnika u int
                bool promenaAktKorisnika = false; //Ako se vrsi izmena aktivnog korisnika, nakon toga sledi odjava
                foreach (Admin k in korisnici.OfType<Admin>()) //Filtriranje korisnika administratora
                {
                    if (uspesno)
                    {
                        if (k.Id == id) //Ako korisnik jeste administrator
                        {
                            foreach (Control c in gbKorisnik.Controls.OfType<TextBox>())
                            {
                                if (c.Text.Trim().Length != 0)
                                {
                                    if (c.Name.Equals("txtUnosUser")) //Korisnicko ime ne moze da se promeni
                                        MessageBox.Show("Korisnicko ime ne moze biti izmenjeno, ostalo ce biti.", "Upozorenje");
                                    else
                                        switch (c.Name)
                                        {
                                            case "txtUnosLozinka":
                                                {
                                                    k.Lozinka = c.Text;
                                                    if (k.Korisnicko_ime.Equals(frmPocetna.aktivniKorisnik)) //Ako se vrsi izmena aktivnog korisnika
                                                        promenaAktKorisnika = true;
                                                    break;
                                                }
                                            case "txtIme": k.Ime = c.Text; break;
                                            case "txtPrezime": k.Prezime = c.Text; break;
                                            default: break;
                                        }
                                }
                            }
                        }
                    }
                }
                foreach (Kupac k in korisnici.OfType<Kupac>()) //Filtriranje korisnika kupaca
                {
                    if (uspesno)
                    {
                        if (k.Id == id) //Ako korisnik jeste kupac
                        {
                            foreach (Control c in gbKorisnik.Controls.OfType<TextBox>())
                            {
                                if (c.Text.Trim().Length != 0)
                                    if (c.Name.Equals("txtUnosUser"))
                                        MessageBox.Show("Korisnicko ime ne moze biti izmenjeno, ostalo ce biti.", "Upozorenje");
                                    else
                                    {
                                        switch (c.Name)
                                        {
                                            case "txtUnosLozinka": k.Lozinka = c.Text; break;
                                            case "txtIme": k.Ime = c.Text; break;
                                            case "txtPrezime": k.Prezime = c.Text; break;
                                            case "txtJMBG": k.Jmbg = c.Text; break;
                                            case "txtDan":
                                                {
                                                    string datum = c.Text + "/" + k.Datum_rodjenja.Month.ToString() + "/" + k.Datum_rodjenja.Year.ToString();
                                                    bool uspesno1 = DateTime.TryParse(datum, out DateTime datumIzmena);
                                                    if (uspesno1)
                                                        k.Datum_rodjenja = datumIzmena;
                                                    else
                                                        MessageBox.Show("Nesipravan podatak za datum rodjenja.", "Greska");
                                                    break;
                                                }
                                            case "txtMesec":
                                                {
                                                    string datum = k.Datum_rodjenja.Day.ToString() + "/" + c.Text + "/" + k.Datum_rodjenja.Year.ToString();
                                                    bool uspesno1 = DateTime.TryParse(datum, out DateTime datumIzmena);
                                                    if (uspesno1)
                                                        k.Datum_rodjenja = datumIzmena;
                                                    else
                                                        MessageBox.Show("Nesipravan podatak za datum rodjenja.", "Greska");
                                                    break;
                                                }
                                            case "txtGodina":
                                                {
                                                    string datum = k.Datum_rodjenja.Month.ToString() + "/" + k.Datum_rodjenja.Day.ToString() + "/" + c.Text;
                                                    bool uspesno1 = DateTime.TryParse(datum, out DateTime datumIzmena);
                                                    if (uspesno1)
                                                        k.Datum_rodjenja = datumIzmena;
                                                    else
                                                        MessageBox.Show("Nesipravan podatak za datum rodjenja.", "Greska");
                                                    break;
                                                }
                                            case "txtTelefon": k.Telefon = c.Text; break;
                                            default: break;
                                        }
                                    }
                            }
                        }
                    }
                }
                cbbKorisnik.Items.Clear();
                foreach (Korisnik k in korisnici)
                    cbbKorisnik.Items.Add(k.ToString());
                fs.Dispose();
                fs = File.OpenWrite(putanja);
                bf.Serialize(fs, korisnici);
                fs.Dispose();
                korisnici.Clear();
                if (promenaAktKorisnika) //Odjava u slucaju izmene aktivnog korisnika
                {
                    MessageBox.Show("Uspešno i bićete odjavljeni.");
                    this.Close();
                }
                MessageBox.Show("Uspešno.");
                ;           }
            else
                MessageBox.Show("Niste izabrali korisnika.", "Greska");
            cbbKorisnik.Text = "";
        }

        private void BtnAdmBrisiKorisnika_Click(object sender, EventArgs e)
        {
            string putanja = "korisnici.rac";
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.OpenRead(putanja);
            if (cbbKorisnik.SelectedIndex > -1) //Ako je selektovan korisnik za brisanje
            {
                korisnici = bf.Deserialize(fs) as List<Korisnik>;
                fs.Dispose();
                bool uspesno = int.TryParse(cbbKorisnik.SelectedItem.ToString().Substring(0, 4), out int id);
                foreach (Korisnik k in korisnici)
                {
                    if (!cbbKorisnik.SelectedItem.ToString().Substring(5, frmPocetna.aktivniKorisnik.Length).Equals(frmPocetna.aktivniKorisnik)) //Aktivni korisnik ne moze da se obrise
                    {
                        if (uspesno)
                        {
                            if (k.Id == id)
                            {
                                string putanjaRez = "rezervacije.rac";
                                FileStream fsr;
                                bool imaRez = false;
                                if (File.Exists(putanjaRez))
                                {
                                    fsr = File.OpenRead(putanjaRez);
                                    rezervacije = bf.Deserialize(fsr) as List<Rezervacija>;
                                    fsr.Dispose();
                                }
                                foreach (Rezervacija rez in rezervacije)
                                    if (rez.IdKupca == k.Id)
                                        imaRez = true;
                                if (imaRez)
                                {
                                    MessageBox.Show("Kupac ima rezervaciju, ne možete ga obrisati.", "Greska");
                                    break;
                                }
                                else
                                {
                                    korisnici.Remove(k);
                                    MessageBox.Show("Uspešno.");
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ne možete obrisati aktivnog korisnika.", "Greska");
                        break;
                    }
                }
                cbbKorisnik.Items.Clear();
                cbbKupac.Items.Clear();
                foreach (Korisnik k in korisnici)
                    cbbKorisnik.Items.Add(k.ToString());
                foreach (Kupac kup in korisnici.OfType<Kupac>())
                    cbbKupac.Items.Add(kup.ToString());
                fs = File.OpenWrite(putanja);
                bf.Serialize(fs, korisnici);
                fs.Dispose();
                korisnici.Clear();
                rezervacije.Clear();
            }
            else
                MessageBox.Show("Niste izabrali nijednog korisnika.", "Greska");
            cbbKorisnik.Text = "";
        }

        private void BtnAdmDodajPonudu_Click(object sender, EventArgs e)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs;
            string putanja = "ponude.rac";
            bool uspesnoId;
            bool uspesnoCena;
            if (File.Exists(putanja)) //Ako postoji datoteka sa ponudama
            {
                if (cbbPonuda.SelectedIndex > -1) //Ako je izabrana ponuda
                {
                    fs = File.OpenRead(putanja);
                    ponude = bf.Deserialize(fs) as List<Ponuda>;
                    fs.Dispose();
                    uspesnoId = int.TryParse(cbbPonuda.SelectedItem.ToString().Substring(0, 4), out int id); //Konverzija ID auta u int
                    uspesnoCena = int.TryParse(txtPonudaCena.Text, out int cena); //Konverzija cene u int
                    if (uspesnoId == true && uspesnoCena == true)
                    {
                        if (dtpDatumDo.Value.Date >= dtpDatumOd.Value.Date) //Datum pocetka ponude mora biti pre ili istog dana kada se okoncava ponuda
                        {
                            bool postojiPonuda = false;
                            foreach (Ponuda p in ponude) //Da li vec postoji ponuda koja na neki nacin onemogucuje unosenje zeljene
                            {
                                if (p.IdAuta == id)
                                {
                                    if ((dtpDatumOd.Value.Date >= p.Datum_od.Date && dtpDatumOd.Value.Date <= p.Datum_do.Date) ||
                                         (dtpDatumDo.Value.Date >= p.Datum_od.Date && dtpDatumDo.Value.Date <= p.Datum_do.Date)) //Ako postoji ponuda koja ima datumsko preklapanje sa zeljenom
                                    {
                                        postojiPonuda = true;
                                        break;
                                    }
                                    if (dtpDatumOd.Value.Date <= p.Datum_od.Date && dtpDatumDo.Value.Date >= p.Datum_do.Date) //Ako postoji ponuda koja je u datumskom opsegu zeljene
                                    {
                                        postojiPonuda = true;
                                        break;
                                    }
                                }
                            }
                            if(postojiPonuda)
                                MessageBox.Show("Nemoguće, postoji ponuda u okviru željene.", "Greska");
                            else
                            {
                                Ponuda p = new Ponuda(id, dtpDatumOd.Value.Date, dtpDatumDo.Value.Date, cena);
                                ponude.Add(p);
                                lsbSpisakPonuda.Items.Add(p);
                                MessageBox.Show("Uspešno.");
                            }
                        }
                        else
                            MessageBox.Show("Nemoguće, proverite datume.", "Greska");
                    }
                    else
                        MessageBox.Show("Uneli ste neispravne podatke.", "Greska");
                    fs = File.OpenWrite(putanja);
                    bf.Serialize(fs, ponude);
                    fs.Dispose();
                    ponude.Clear();
                }
                else
                {
                    MessageBox.Show("Niste odabrali auto.", "Greska");
                }
            }
            else //Ne postoji datoteka sa ponudama
            {
                if (cbbPonuda.SelectedIndex > -1) //Ako je odabran auto za pravljenje ponude
                {
                    fs = File.OpenWrite(putanja);
                    uspesnoId = int.TryParse(cbbPonuda.SelectedItem.ToString().Substring(0, 4), out int id);
                    uspesnoCena = int.TryParse(txtPonudaCena.Text, out int cena);
                    if (uspesnoId == true && uspesnoCena == true)
                    {
                        Ponuda p = new Ponuda(id, dtpDatumOd.Value.Date, dtpDatumDo.Value.Date, cena);
                        ponude.Add(p);
                        lsbSpisakPonuda.Items.Add(p);
                    }
                    else
                        MessageBox.Show("Uneli ste neispravne podatke.", "Greska");
                    bf.Serialize(fs, ponude);
                    fs.Dispose();
                    ponude.Clear();
                    MessageBox.Show("Uspešno.");
                }
                else
                {
                    MessageBox.Show("Niste odabrali auto.", "Greska");
                }
            }
        }

        private void BtnAdmIzmeniPonudu_Click(object sender, EventArgs e)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fsp, fsr;
            string putanjaPon = "ponude.rac";
            string putanjaRez = "rezervacije.rac";
            if (lsbSpisakPonuda.SelectedIndex > -1) //Ako je izabrana ponuda za izmenu
            {
                ponude = lsbSpisakPonuda.Items.Cast<Ponuda>().ToList(); //Preuzimanje svih ponuda iz ListBox-a i konverzija svake u objekat tipa "Ponuda"
                if (File.Exists(putanjaRez)) //Ako postoje rezervacije za izabranu ponudu i preuzimanje istih
                {
                    fsr = File.OpenRead(putanjaRez);
                    rezervacije = bf.Deserialize(fsr) as List<Rezervacija>;
                    fsr.Dispose();
                }
                foreach(Ponuda p in ponude)
                    if (p.Equals(lsbSpisakPonuda.SelectedItem))
                    {
                        bool postojiPon = false; //Da li vec postoji ponuda u zeljenom datumskom okviru
                        bool rezIzvanPon = false; //Da li ce nastati rezervacija izvan ponude  
                        foreach (Rezervacija r in rezervacije) //Da postoji rezervacija koja ce biti izvan ponude
                        {
                            if (r.IdAuta == p.IdAuta)
                            {
                                if (dtpDatumOd.Value.Date > r.Datum_od.Date || dtpDatumDo.Value.Date < r.Datum_do.Date)
                                {
                                    rezIzvanPon = true;
                                    break;
                                }
                            }
                        }
                        foreach(Ponuda pon in ponude) //Trazenje ponude koja eventualno postoji u datumskom okviru zeljene
                            if(pon.IdAuta == p.IdAuta && pon.Datum_od.Date != p.Datum_od.Date && pon.Datum_do.Date != p.Datum_do.Date)
                                if((dtpDatumOd.Value.Date >= pon.Datum_od.Date && dtpDatumOd.Value.Date <= pon.Datum_do.Date) ||
                                    (dtpDatumDo.Value.Date >= pon.Datum_od.Date && dtpDatumDo.Value <= pon.Datum_do.Date))
                                {
                                    postojiPon = true;
                                    break;
                                }

                        if (rezIzvanPon) //Ako postoji rezervacija koja ce biti van ponude
                        {
                            MessageBox.Show("Postojeća rezervacija će biti izvan ponude, izmena nemoguća.", "Greska");
                            break;
                        }
                        else if (postojiPon) //Ako postoji ponuda koja ce se datumski preklapati sa zeljenom
                        {
                            MessageBox.Show("Desiće se preklapanje ponuda, izmena nije moguća.", "Greska");
                            break;
                        }
                        else
                        {
                            bool uspesnoCena = int.TryParse(txtPonudaCena.Text, out int cena);
                            if ((p.CenaDan != cena) && uspesnoCena)
                            {
                                DialogResult dr = MessageBox.Show("Promena cene odraziće se na sve postojeće rezervacije u okviru ponude. Nastavak?", "Upozorenje", MessageBoxButtons.YesNo); //Ako je izmenjena cena za postojecu ponudu, da li korisnik zeli da promeni cene postojecih rezervacija
                                if (dr == DialogResult.No) //Ako ne zeli
                                    break;
                                else //Ako zeli
                                {
                                    p.CenaDan = cena;
                                    foreach (Rezervacija r in rezervacije)
                                        if (r.IdAuta == p.IdAuta)
                                            r.Cena = ((r.Datum_do.Date - r.Datum_od.Date).Days + 1) * cena;
                                    fsr = File.OpenWrite(putanjaRez);
                                    bf.Serialize(fsr, rezervacije);
                                    fsr.Dispose();
                                    lsbSpisakRezervacija.Items.Clear();
                                    cbbKupac.Text = "";
                                }
                            }
                            lsbSpisakPonuda.Items.Remove(p);
                            if (p.Datum_od.Date != dtpDatumOd.Value.Date)
                                p.Datum_od = dtpDatumOd.Value.Date;
                            if (p.Datum_do.Date != dtpDatumDo.Value.Date)
                                p.Datum_do = dtpDatumDo.Value.Date;
                            
                            lsbSpisakPonuda.Items.Add(p);
                            lsbSpisakPonuda.Refresh();
                            fsp = File.OpenWrite(putanjaPon);
                            bf.Serialize(fsp, ponude);
                            fsp.Dispose();
                            MessageBox.Show("Uspešno.");
                            break;
                        }
                    }
                ponude.Clear();
                rezervacije.Clear();
            }
            else
                MessageBox.Show("Odaberite ponudu za izmenu.", "Greska");
        }

        private void BtnAdmBrisiPonudu_Click(object sender, EventArgs e)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fsp;
            string putanjaPon = "ponude.rac";
            string putanjaRez = "rezervacije.rac";
            if (File.Exists(putanjaRez))
            {
                FileStream fsr = File.OpenRead(putanjaRez);
                rezervacije = bf.Deserialize(fsr) as List<Rezervacija>;
                fsr.Dispose();
            }
            if (lsbSpisakPonuda.SelectedIndex > -1)
            {
                ponude = lsbSpisakPonuda.Items.Cast<Ponuda>().ToList();
                bool postojiRez = false;
                foreach (Ponuda p in ponude)
                    if (p.Equals(lsbSpisakPonuda.SelectedItem))
                    {
                        foreach(Rezervacija r in rezervacije) //Trazenje rezervacije u okviru ponude koja se brise
                            if (p.IdAuta == r.IdAuta)
                                if(r.Datum_od.Date >= p.Datum_od.Date && r.Datum_do.Date <= p.Datum_do.Date)
                                    postojiRez = true;
                        if (postojiRez) //Brisanje ponude nije moguce ako postoji rezervacija
                        {
                            MessageBox.Show("Postoji rezervacija u okviru ove ponude. Brisanje ponude nemoguće.", "Greska");
                            break;
                        }
                        else //Brisanje ponude
                        {
                            ponude.Remove(p);
                            lsbSpisakPonuda.Items.Remove(p);
                            lsbSpisakPonuda.Refresh();
                            MessageBox.Show("Uspešno.");
                            break;
                        }
                    }
                fsp = File.OpenWrite(putanjaPon);
                bf.Serialize(fsp, ponude);
                fsp.Dispose();
                ponude.Clear();
                
            }
            else
                MessageBox.Show("Odaberite ponudu.", "Greska");
            rezervacije.Clear();
        }

        private void LsbSpisakPonuda_Click(object sender, EventArgs e)
        {
            if (lsbSpisakPonuda.SelectedIndex > -1) //Ako se odabere postojeca ponuda u listi, automatski se DateTimePicker kontrole postavljaju na datumski okvir ponude
            {
                dtpDatumOd.Value = ((Ponuda)lsbSpisakPonuda.SelectedItem).Datum_od.Date;
                dtpDatumDo.Value = ((Ponuda)lsbSpisakPonuda.SelectedItem).Datum_do.Date;
            }
        }

        private void CbbKupac_SelectionChangeCommitted(object sender, EventArgs e) //Kad se izabere kupac, prikazuju se sve njegove rezervacije
        {
            lsbSpisakRezervacija.Items.Clear();
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs;
            string putanjaRez = "rezervacije.rac";
            if (File.Exists(putanjaRez))
            {
                fs = File.OpenRead(putanjaRez);
                rezervacije = bf.Deserialize(fs) as List<Rezervacija>;
                fs.Dispose();
            }
            bool uspesno = int.TryParse(cbbKupac.SelectedItem.ToString().Substring(0, 4), out int idKupca); //Konverzija Id kupca u int
            foreach (Rezervacija r in rezervacije)
                if(uspesno)
                    if (r.IdKupca == idKupca)
                        lsbSpisakRezervacija.Items.Add(r.ToString());
            rezervacije.Clear();
        }

        private void BtnAdmBrisiRezervaciju_Click(object sender, EventArgs e)
        {
            string putanjaRez = "rezervacije.rac";
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
                        break;
                    }
                fs = File.OpenWrite(putanjaRez);
                bf.Serialize(fs, rezervacije);
                fs.Dispose();
                rezervacije.Clear();
                MessageBox.Show("Uspešno.");
            }
            else
                MessageBox.Show("Odaberite rezervaciju.", "Greska");
        }

        private void BtnAdmOdjava_Click(object sender, EventArgs e)
        {
            this.Close(); //Odjavljivanje i povratak na pocetnu formu
        }

        private void BtnIzmeniRez_Click(object sender, EventArgs e)
        {
            frmIzmenaRezervacije frmIzmenaRezervacije = new frmIzmenaRezervacije();
            frmIzmenaRezervacije.StartPosition = FormStartPosition.CenterScreen;
            frmIzmenaRezervacije.Show();
            this.Hide();
            frmIzmenaRezervacije.FormClosed += FrmIzmenaRezervacije_FormClosed;
        }

        private void FrmIzmenaRezervacije_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }

        private void BtnStatistika_Click(object sender, EventArgs e)
        {
            brojAutomobila = 0;
            bool uspesnoMesec = int.TryParse(txtMesecStat.Text, out int brojM);
            bool uspesnoGodina = int.TryParse(txtGodinaStat.Text, out int brojG);
            if((uspesnoMesec == true) && (uspesnoGodina == true))
            {
                BinaryFormatter bf = new BinaryFormatter();
                string putanjaPon = "ponude.rac";
                string putanjaRez = "rezervacije.rac";
                FileStream fsp, fsr;
                if (File.Exists(putanjaPon))
                {
                    fsp = File.OpenRead(putanjaPon);
                    ponude = bf.Deserialize(fsp) as List<Ponuda>;
                    fsp.Dispose();
                }
                if (File.Exists(putanjaRez))
                {
                    fsr = File.OpenRead(putanjaRez);
                    rezervacije = bf.Deserialize(fsr) as List<Rezervacija>;
                    fsr.Dispose();
                }
                foreach (Ponuda p in ponude)
                {
                    if(p.Datum_od.Year == brojG && brojM >= p.Datum_od.Month && brojM <= p.Datum_do.Month)
                    {
                        foreach (Rezervacija r in rezervacije)
                        {
                            if(p.IdAuta == r.IdAuta)
                            {
                                idBrojevi.Add(p.IdAuta);
                            }
                        }
                    }
                }
                
            }
            else
            {
                MessageBox.Show("Uneli ste pogresne podatke.", "Greska");
            }
            brojAutomobila = idBrojevi.Count();
            ugao = ((float)idBrojevi.Count / (float)cbbAuto.Items.Count) * 360F;
            lblBrojcano.Text = idBrojevi.Count.ToString() + " od ukupno " + cbbAuto.Items.Count.ToString();
            lblProcentualno.Text = ((float)idBrojevi.Count / (float)cbbAuto.Items.Count * 100F).ToString() + "%";
            gpbStat.Paint += crtanje;
            gpbStat.Invalidate();
            idBrojevi.Clear();
        }

        private void crtanje(object sender, PaintEventArgs e)
        {
            e.Graphics.FillEllipse(Brushes.Blue, new Rectangle(120, 40, 80, 80));
            e.Graphics.FillPie(Brushes.Red, new Rectangle(120, 40, 80, 80), -90F, (float)ugao);
            e.Graphics.DrawEllipse(Pens.Black, new Rectangle(120, 40, 80, 80));
        }

    }
}
