namespace kargotakipsistemi
{
    partial class AdresEkleForm
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
            splitContainer1 = new SplitContainer();
            groupBox1 = new GroupBox();
            dgv_adresler = new DataGridView();
            groupBox2 = new GroupBox();
            label12 = new Label();
            ckb_adresAktif = new CheckBox();
            tb_adresAcikAdres = new TextBox();
            tb_adresAciklama = new TextBox();
            label5 = new Label();
            label11 = new Label();
            label9 = new Label();
            tb_adresDaire = new TextBox();
            label10 = new Label();
            tb_adresBinaAd = new TextBox();
            label8 = new Label();
            tb_adresKat = new TextBox();
            tb_adresKapiNo = new TextBox();
            btn_adresKayitSil = new Button();
            label7 = new Label();
            label2 = new Label();
            label3 = new Label();
            btn_adresKaydet = new Button();
            cb_adresIl = new ComboBox();
            cb_adresTip = new ComboBox();
            cb_adresIlce = new ComboBox();
            btn_adresFormTemizle = new Button();
            cb_adresMahalle = new ComboBox();
            label6 = new Label();
            label4 = new Label();
            tb_adresPostaKodu = new TextBox();
            tb_adresBaslik = new TextBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv_adresler).BeginInit();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Margin = new Padding(3, 2, 3, 2);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(groupBox2);
            splitContainer1.Size = new Size(787, 540);
            splitContainer1.SplitterDistance = 536;
            splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(dgv_adresler);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Margin = new Padding(3, 2, 3, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 2, 3, 2);
            groupBox1.Size = new Size(536, 540);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Kayıtlı Adresler";
            // 
            // dgv_adresler
            // 
            dgv_adresler.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv_adresler.Location = new Point(12, 19);
            dgv_adresler.Margin = new Padding(3, 2, 3, 2);
            dgv_adresler.Name = "dgv_adresler";
            dgv_adresler.Size = new Size(506, 494);
            dgv_adresler.TabIndex = 0;
            dgv_adresler.SelectionChanged += dgv_adresler_SelectionChanged;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label12);
            groupBox2.Controls.Add(ckb_adresAktif);
            groupBox2.Controls.Add(tb_adresAcikAdres);
            groupBox2.Controls.Add(tb_adresAciklama);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(label11);
            groupBox2.Controls.Add(label9);
            groupBox2.Controls.Add(tb_adresDaire);
            groupBox2.Controls.Add(label10);
            groupBox2.Controls.Add(tb_adresBinaAd);
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(tb_adresKat);
            groupBox2.Controls.Add(tb_adresKapiNo);
            groupBox2.Controls.Add(btn_adresKayitSil);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(btn_adresKaydet);
            groupBox2.Controls.Add(cb_adresIl);
            groupBox2.Controls.Add(cb_adresTip);
            groupBox2.Controls.Add(cb_adresIlce);
            groupBox2.Controls.Add(btn_adresFormTemizle);
            groupBox2.Controls.Add(cb_adresMahalle);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(tb_adresPostaKodu);
            groupBox2.Controls.Add(tb_adresBaslik);
            groupBox2.Controls.Add(label1);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(0, 0);
            groupBox2.Margin = new Padding(3, 2, 3, 2);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(3, 2, 3, 2);
            groupBox2.Size = new Size(247, 540);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "Adres Bilgileri";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(23, 309);
            label12.Name = "label12";
            label12.Size = new Size(70, 14);
            label12.TabIndex = 25;
            label12.Text = "Açıklama:";
            // 
            // ckb_adresAktif
            // 
            ckb_adresAktif.AutoSize = true;
            ckb_adresAktif.Location = new Point(173, 431);
            ckb_adresAktif.Name = "ckb_adresAktif";
            ckb_adresAktif.Size = new Size(61, 18);
            ckb_adresAktif.TabIndex = 1;
            ckb_adresAktif.Text = "Aktif";
            ckb_adresAktif.UseVisualStyleBackColor = true;
            // 
            // tb_adresAcikAdres
            // 
            tb_adresAcikAdres.Location = new Point(113, 347);
            tb_adresAcikAdres.Multiline = true;
            tb_adresAcikAdres.Name = "tb_adresAcikAdres";
            tb_adresAcikAdres.Size = new Size(121, 78);
            tb_adresAcikAdres.TabIndex = 8;
            // 
            // tb_adresAciklama
            // 
            tb_adresAciklama.Location = new Point(113, 306);
            tb_adresAciklama.Multiline = true;
            tb_adresAciklama.Name = "tb_adresAciklama";
            tb_adresAciklama.Size = new Size(114, 35);
            tb_adresAciklama.TabIndex = 24;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(23, 350);
            label5.Name = "label5";
            label5.Size = new Size(84, 14);
            label5.TabIndex = 9;
            label5.Text = "Açık Adres:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(23, 281);
            label11.Name = "label11";
            label11.Size = new Size(49, 14);
            label11.TabIndex = 23;
            label11.Text = "Daire:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(23, 225);
            label9.Name = "label9";
            label9.Size = new Size(70, 14);
            label9.TabIndex = 19;
            label9.Text = "Bina Adı:";
            // 
            // tb_adresDaire
            // 
            tb_adresDaire.Location = new Point(113, 278);
            tb_adresDaire.Name = "tb_adresDaire";
            tb_adresDaire.Size = new Size(114, 22);
            tb_adresDaire.TabIndex = 22;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(23, 253);
            label10.Name = "label10";
            label10.Size = new Size(35, 14);
            label10.TabIndex = 21;
            label10.Text = "Kat:";
            // 
            // tb_adresBinaAd
            // 
            tb_adresBinaAd.Location = new Point(113, 222);
            tb_adresBinaAd.Name = "tb_adresBinaAd";
            tb_adresBinaAd.Size = new Size(114, 22);
            tb_adresBinaAd.TabIndex = 18;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(23, 197);
            label8.Name = "label8";
            label8.Size = new Size(63, 14);
            label8.TabIndex = 17;
            label8.Text = "Kapı No:";
            // 
            // tb_adresKat
            // 
            tb_adresKat.Location = new Point(113, 250);
            tb_adresKat.Name = "tb_adresKat";
            tb_adresKat.Size = new Size(114, 22);
            tb_adresKat.TabIndex = 20;
            // 
            // tb_adresKapiNo
            // 
            tb_adresKapiNo.Location = new Point(113, 194);
            tb_adresKapiNo.Name = "tb_adresKapiNo";
            tb_adresKapiNo.Size = new Size(114, 22);
            tb_adresKapiNo.TabIndex = 16;
            // 
            // btn_adresKayitSil
            // 
            btn_adresKayitSil.Location = new Point(137, 455);
            btn_adresKayitSil.Name = "btn_adresKayitSil";
            btn_adresKayitSil.Size = new Size(90, 30);
            btn_adresKayitSil.TabIndex = 14;
            btn_adresKayitSil.Text = "Kaydı Sil";
            btn_adresKayitSil.UseVisualStyleBackColor = true;
            btn_adresKayitSil.Click += btn_adresKayitSil_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(23, 85);
            label7.Name = "label7";
            label7.Size = new Size(77, 14);
            label7.TabIndex = 15;
            label7.Text = "Adres Tip:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(23, 113);
            label2.Name = "label2";
            label2.Size = new Size(28, 14);
            label2.TabIndex = 2;
            label2.Text = "İl:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(23, 141);
            label3.Name = "label3";
            label3.Size = new Size(42, 14);
            label3.TabIndex = 3;
            label3.Text = "İlçe:";
            // 
            // btn_adresKaydet
            // 
            btn_adresKaydet.Location = new Point(23, 455);
            btn_adresKaydet.Name = "btn_adresKaydet";
            btn_adresKaydet.Size = new Size(90, 30);
            btn_adresKaydet.TabIndex = 13;
            btn_adresKaydet.Text = "Kaydet";
            btn_adresKaydet.UseVisualStyleBackColor = true;
            btn_adresKaydet.Click += btn_adresKaydet_Click;
            // 
            // cb_adresIl
            // 
            cb_adresIl.FormattingEnabled = true;
            cb_adresIl.Location = new Point(106, 110);
            cb_adresIl.Name = "cb_adresIl";
            cb_adresIl.Size = new Size(121, 22);
            cb_adresIl.TabIndex = 4;
            cb_adresIl.SelectedIndexChanged += cb_adresIl_SelectedIndexChanged;
            // 
            // cb_adresTip
            // 
            cb_adresTip.FormattingEnabled = true;
            cb_adresTip.Location = new Point(113, 82);
            cb_adresTip.Name = "cb_adresTip";
            cb_adresTip.Size = new Size(114, 22);
            cb_adresTip.TabIndex = 14;
            // 
            // cb_adresIlce
            // 
            cb_adresIlce.FormattingEnabled = true;
            cb_adresIlce.Location = new Point(106, 138);
            cb_adresIlce.Name = "cb_adresIlce";
            cb_adresIlce.Size = new Size(121, 22);
            cb_adresIlce.TabIndex = 5;
            cb_adresIlce.SelectedIndexChanged += cb_adresIlce_SelectedIndexChanged;
            // 
            // btn_adresFormTemizle
            // 
            btn_adresFormTemizle.Location = new Point(63, 491);
            btn_adresFormTemizle.Name = "btn_adresFormTemizle";
            btn_adresFormTemizle.Size = new Size(120, 30);
            btn_adresFormTemizle.TabIndex = 12;
            btn_adresFormTemizle.Text = "Formu Temizle";
            btn_adresFormTemizle.UseVisualStyleBackColor = true;
            btn_adresFormTemizle.Click += btn_adresFormTemizle_Click;
            // 
            // cb_adresMahalle
            // 
            cb_adresMahalle.FormattingEnabled = true;
            cb_adresMahalle.Location = new Point(106, 166);
            cb_adresMahalle.Name = "cb_adresMahalle";
            cb_adresMahalle.Size = new Size(121, 22);
            cb_adresMahalle.TabIndex = 6;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(23, 57);
            label6.Name = "label6";
            label6.Size = new Size(84, 14);
            label6.TabIndex = 11;
            label6.Text = "Posta Kodu:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(23, 169);
            label4.Name = "label4";
            label4.Size = new Size(63, 14);
            label4.TabIndex = 7;
            label4.Text = "Mahalle:";
            // 
            // tb_adresPostaKodu
            // 
            tb_adresPostaKodu.Location = new Point(113, 54);
            tb_adresPostaKodu.Name = "tb_adresPostaKodu";
            tb_adresPostaKodu.Size = new Size(114, 22);
            tb_adresPostaKodu.TabIndex = 10;
            // 
            // tb_adresBaslik
            // 
            tb_adresBaslik.Location = new Point(127, 26);
            tb_adresBaslik.Name = "tb_adresBaslik";
            tb_adresBaslik.Size = new Size(100, 22);
            tb_adresBaslik.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(23, 29);
            label1.Name = "label1";
            label1.Size = new Size(98, 14);
            label1.TabIndex = 0;
            label1.Text = "Adres Başlık:";
            // 
            // AdresEkleForm
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(787, 540);
            Controls.Add(splitContainer1);
            Font = new Font("Consolas", 9F);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "AdresEkleForm";
            Text = "AdresEkleForm";
            Load += AdresEkleForm_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgv_adresler).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private GroupBox groupBox1;
        private DataGridView dgv_adresler;
        private GroupBox groupBox2;
        private Label label1;
        private Label label4;
        private ComboBox cb_adresMahalle;
        private ComboBox cb_adresIlce;
        private ComboBox cb_adresIl;
        private Label label3;
        private Label label2;
        private TextBox tb_adresBaslik;
        private Label label6;
        private TextBox tb_adresPostaKodu;
        private Label label5;
        private TextBox tb_adresAcikAdres;
        private Button btn_adresKayitSil;
        private Button btn_adresKaydet;
        private Button btn_adresFormTemizle;
        private Label label7;
        private CheckBox ckb_adresAktif;
        private ComboBox cb_adresTip;
        private Label label11;
        private Label label9;
        private TextBox tb_adresDaire;
        private Label label10;
        private TextBox tb_adresBinaAd;
        private Label label8;
        private TextBox tb_adresKat;
        private TextBox tb_adresKapiNo;
        private Label label12;
        private TextBox tb_adresAciklama;
    }
}