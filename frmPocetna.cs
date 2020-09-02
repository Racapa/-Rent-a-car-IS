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
    public partial class frmPocetna : Form
    {
        string putanja = "korisnici.rac";
        List<Korisnik> korisnici = new List<Korisnik>();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs;
        public static string aktivniKorisnik;
        public frmPocetna()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            if (!File.Exists(putanja)) //Ako ne postoji datoteka sa korisnicima, pravi se pocetna sa podrazumevanim administratorom (admin, admin)
            {
                fs = File.OpenWrite(putanja);
                Korisnik k = new Admin("admin", "admin", "-", "-", true);
                korisnici.Add(k as Admin);
                bf.Serialize(fs, korisnici);
                fs.Dispose();
                korisnici.Clear();
            }

        }

        private void FrmPocetna_Load(object sender, EventArgs e)
        {
            if(!File.Exists(putanja))
            {
                DialogResult res = MessageBox.Show("Datoteka \"korisnici.rac\" nedostaje. Aplikacija će biti prekinuta.", "Greska", MessageBoxButtons.OK);
                if (res == DialogResult.OK)
                    Application.Exit();
            }
        }

        private void BtnPrijava_Click(object sender, EventArgs e)
        {
            if (txtUser.Text.Trim().Length != 0 && txtLozinka.Text.Trim().Length != 0) //Da li su popunjena sva polja
            {
                fs = File.OpenRead(putanja);
                korisnici = bf.Deserialize(fs) as List<Korisnik>;
                fs.Dispose();
                bool pogresniPodaci = true;
                foreach (Korisnik k in korisnici)
                {
                    if (k.Korisnicko_ime.Equals(txtUser.Text) && k.Lozinka.Equals(txtLozinka.Text)) //Da li su uneti ispravni podaci
                        if (k.IsAdmin == true)
                        {
                            aktivniKorisnik = txtUser.Text;
                            frmAdmin frmAdmin = new frmAdmin();
                            frmAdmin.StartPosition = FormStartPosition.CenterScreen;
                            frmAdmin.Show();
                            this.Hide();
                            pogresniPodaci = false;
                            frmAdmin.FormClosed += FrmAdmin_FormClosed; //Kad se zatvori Admin forma, pojavljuje se pocetna
                            break;
                        }
                        else if (k.IsAdmin == false)
                        {
                            aktivniKorisnik = txtUser.Text;
                            frmKupac frmKupac = new frmKupac();
                            frmKupac.StartPosition = FormStartPosition.CenterScreen;
                            frmKupac.Show();
                            this.Hide();
                            pogresniPodaci = false;
                            frmKupac.FormClosed += FrmKupac_FormClosed; //Kad se zatvori Kupac forma, pojavljuje se pocetna
                            break;
                        }
                }
                korisnici.Clear();
                if (pogresniPodaci)
                {
                    MessageBox.Show("Uneli ste neispravne podatke, pokušajte ponovo.", "Greska");
                    txtLozinka.Text = "";
                    txtUser.Focus();
                    txtUser.SelectAll();
                }
            }
            else
            {
                MessageBox.Show("Niste uneli sve podatke.", "Greska");
                txtLozinka.Text = "";
                txtUser.Focus();
                txtUser.SelectAll();
            }
        }

        private void FrmKupac_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
            txtUser.Clear();
            txtLozinka.Clear();
            txtUser.Focus();
        }

        private void FrmAdmin_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
            txtUser.Clear();
            txtLozinka.Clear();
            txtUser.Focus();
        }

        private void TxtLozinka_KeyPress(object sender, KeyPressEventArgs e) //Enter kao button click
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnPrijava.PerformClick();
            }
        }
    }
}
