namespace Projekat_1
{
    partial class frmIzmenaRezervacije
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
            this.lsbSpisakRezervacija = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbbKupci = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpDatum_od = new System.Windows.Forms.DateTimePicker();
            this.dtpDatum_do = new System.Windows.Forms.DateTimePicker();
            this.btnIzmeniRez = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lsbSpisakRezervacija
            // 
            this.lsbSpisakRezervacija.FormattingEnabled = true;
            this.lsbSpisakRezervacija.Location = new System.Drawing.Point(12, 46);
            this.lsbSpisakRezervacija.Name = "lsbSpisakRezervacija";
            this.lsbSpisakRezervacija.Size = new System.Drawing.Size(472, 134);
            this.lsbSpisakRezervacija.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Korisnik:";
            // 
            // cbbKupci
            // 
            this.cbbKupci.FormattingEnabled = true;
            this.cbbKupci.Location = new System.Drawing.Point(75, 10);
            this.cbbKupci.Name = "cbbKupci";
            this.cbbKupci.Size = new System.Drawing.Size(409, 21);
            this.cbbKupci.TabIndex = 2;
            this.cbbKupci.SelectionChangeCommitted += new System.EventHandler(this.CbbKupci_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 194);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Od:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 220);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Do:";
            // 
            // dtpDatum_od
            // 
            this.dtpDatum_od.Location = new System.Drawing.Point(43, 188);
            this.dtpDatum_od.Name = "dtpDatum_od";
            this.dtpDatum_od.Size = new System.Drawing.Size(200, 20);
            this.dtpDatum_od.TabIndex = 5;
            // 
            // dtpDatum_do
            // 
            this.dtpDatum_do.Location = new System.Drawing.Point(43, 214);
            this.dtpDatum_do.Name = "dtpDatum_do";
            this.dtpDatum_do.Size = new System.Drawing.Size(200, 20);
            this.dtpDatum_do.TabIndex = 6;
            // 
            // btnIzmeniRez
            // 
            this.btnIzmeniRez.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIzmeniRez.Location = new System.Drawing.Point(162, 260);
            this.btnIzmeniRez.Name = "btnIzmeniRez";
            this.btnIzmeniRez.Size = new System.Drawing.Size(171, 40);
            this.btnIzmeniRez.TabIndex = 7;
            this.btnIzmeniRez.Text = "Izmeni";
            this.btnIzmeniRez.UseVisualStyleBackColor = true;
            this.btnIzmeniRez.Click += new System.EventHandler(this.BtnIzmeniRez_Click);
            // 
            // frmIzmenaRezervacije
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 312);
            this.Controls.Add(this.btnIzmeniRez);
            this.Controls.Add(this.dtpDatum_do);
            this.Controls.Add(this.dtpDatum_od);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbbKupci);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lsbSpisakRezervacija);
            this.Name = "frmIzmenaRezervacije";
            this.Text = "frmIzmenaRezervacije";
            this.Load += new System.EventHandler(this.FrmIzmenaRezervacije_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lsbSpisakRezervacija;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbbKupci;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpDatum_od;
        private System.Windows.Forms.DateTimePicker dtpDatum_do;
        private System.Windows.Forms.Button btnIzmeniRez;
    }
}