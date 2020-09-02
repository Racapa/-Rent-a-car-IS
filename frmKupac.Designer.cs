namespace Projekat_1
{
    partial class frmKupac
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnBrisiRezervaciju = new System.Windows.Forms.Button();
            this.btnRezervisi = new System.Windows.Forms.Button();
            this.lblUlogovaniKupac = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lsbSpisakRezervacija = new System.Windows.Forms.ListBox();
            this.btnOdjava = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnBrisiRezervaciju
            // 
            this.btnBrisiRezervaciju.Location = new System.Drawing.Point(449, 112);
            this.btnBrisiRezervaciju.Name = "btnBrisiRezervaciju";
            this.btnBrisiRezervaciju.Size = new System.Drawing.Size(75, 23);
            this.btnBrisiRezervaciju.TabIndex = 1;
            this.btnBrisiRezervaciju.Text = "Obriši";
            this.btnBrisiRezervaciju.UseVisualStyleBackColor = true;
            this.btnBrisiRezervaciju.Click += new System.EventHandler(this.BtnBrisiRezervaciju_Click);
            // 
            // btnRezervisi
            // 
            this.btnRezervisi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRezervisi.Location = new System.Drawing.Point(165, 230);
            this.btnRezervisi.Name = "btnRezervisi";
            this.btnRezervisi.Size = new System.Drawing.Size(128, 50);
            this.btnRezervisi.TabIndex = 2;
            this.btnRezervisi.Text = "Rezerviši vozilo";
            this.btnRezervisi.UseVisualStyleBackColor = true;
            this.btnRezervisi.Click += new System.EventHandler(this.BtnRezervisi_Click);
            // 
            // lblUlogovaniKupac
            // 
            this.lblUlogovaniKupac.AutoSize = true;
            this.lblUlogovaniKupac.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUlogovaniKupac.Location = new System.Drawing.Point(13, 13);
            this.lblUlogovaniKupac.Name = "lblUlogovaniKupac";
            this.lblUlogovaniKupac.Size = new System.Drawing.Size(46, 17);
            this.lblUlogovaniKupac.TabIndex = 3;
            this.lblUlogovaniKupac.Text = "label1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Vaše rezervacije:";
            // 
            // lsbSpisakRezervacija
            // 
            this.lsbSpisakRezervacija.FormattingEnabled = true;
            this.lsbSpisakRezervacija.Location = new System.Drawing.Point(13, 88);
            this.lsbSpisakRezervacija.Name = "lsbSpisakRezervacija";
            this.lsbSpisakRezervacija.Size = new System.Drawing.Size(421, 134);
            this.lsbSpisakRezervacija.TabIndex = 5;
            // 
            // btnOdjava
            // 
            this.btnOdjava.Location = new System.Drawing.Point(449, 239);
            this.btnOdjava.Name = "btnOdjava";
            this.btnOdjava.Size = new System.Drawing.Size(103, 41);
            this.btnOdjava.TabIndex = 6;
            this.btnOdjava.Text = "Odjava";
            this.btnOdjava.UseVisualStyleBackColor = true;
            this.btnOdjava.Click += new System.EventHandler(this.BtnOdjava_Click);
            // 
            // frmKupac
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 292);
            this.Controls.Add(this.btnOdjava);
            this.Controls.Add(this.lsbSpisakRezervacija);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblUlogovaniKupac);
            this.Controls.Add(this.btnRezervisi);
            this.Controls.Add(this.btnBrisiRezervaciju);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmKupac";
            this.Text = "Kupac";
            this.Load += new System.EventHandler(this.FrmKupac_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnBrisiRezervaciju;
        private System.Windows.Forms.Button btnRezervisi;
        private System.Windows.Forms.Label lblUlogovaniKupac;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lsbSpisakRezervacija;
        private System.Windows.Forms.Button btnOdjava;
    }
}