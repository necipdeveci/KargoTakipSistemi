namespace kargotakipsistemi.Forms
{
    partial class TarifeYonetimForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            dgv_tarifeler = new DataGridView();
            cb_tarifeTuru = new ComboBox();
            tb_tarifeAdi = new TextBox();
            cb_teslimatTipi = new ComboBox();
            nud_minDeger = new NumericUpDown();
            nud_maxDeger = new NumericUpDown();
            nud_deger = new NumericUpDown();
            cb_birim = new ComboBox();
            ckb_aktif = new CheckBox();
            nud_oncelik = new NumericUpDown();
            dtp_gecerlilikBaslangic = new DateTimePicker();
            dtp_gecerlilikBitis = new DateTimePicker();
            ckb_suresizGecerli = new CheckBox();
            tb_aciklama = new TextBox();
            btn_kaydet = new Button();
            btn_sil = new Button();
            btn_temizle = new Button();
            cb_tarifeTuruFiltre = new ComboBox();
            btn_filtre = new Button();
            ckb_minDegerYok = new CheckBox();
            ckb_maxDegerYok = new CheckBox();
            toolTip1 = new ToolTip(components);
            lbl_tarifeTuru = new Label();
            lbl_tarifeAdi = new Label();
            lbl_teslimatTipi = new Label();
            lbl_minDeger = new Label();
            lbl_maxDeger = new Label();
            lbl_deger = new Label();
            lbl_birim = new Label();
            lbl_oncelik = new Label();
            lbl_gecerlilikBaslangic = new Label();
            lbl_gecerlilikBitis = new Label();
            lbl_aciklama = new Label();
            lbl_filtre = new Label();
            ((System.ComponentModel.ISupportInitialize)dgv_tarifeler).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nud_minDeger).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nud_maxDeger).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nud_deger).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nud_oncelik).BeginInit();
            SuspendLayout();
            // 
            // dgv_tarifeler
            // 
            dgv_tarifeler.AllowUserToAddRows = false;
            dgv_tarifeler.AllowUserToDeleteRows = false;
            dgv_tarifeler.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv_tarifeler.Location = new Point(12, 80);
            dgv_tarifeler.Name = "dgv_tarifeler";
            dgv_tarifeler.ReadOnly = true;
            dgv_tarifeler.Size = new Size(960, 300);
            dgv_tarifeler.TabIndex = 0;
            toolTip1.SetToolTip(dgv_tarifeler, "Mevcut fiyatlandýrma tarifelerinin listesi");
            // 
            // cb_tarifeTuru
            // 
            cb_tarifeTuru.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_tarifeTuru.FormattingEnabled = true;
            cb_tarifeTuru.Location = new Point(120, 400);
            cb_tarifeTuru.Name = "cb_tarifeTuru";
            cb_tarifeTuru.Size = new Size(200, 23);
            cb_tarifeTuru.TabIndex = 1;
            toolTip1.SetToolTip(cb_tarifeTuru, "Tarife kategorisi: Aðýrlýk, Hacim, Teslimat Tipi vb.");
            // 
            // tb_tarifeAdi
            // 
            tb_tarifeAdi.Location = new Point(120, 430);
            tb_tarifeAdi.Name = "tb_tarifeAdi";
            tb_tarifeAdi.Size = new Size(200, 23);
            tb_tarifeAdi.TabIndex = 2;
            toolTip1.SetToolTip(tb_tarifeAdi, "Tarifeyi tanýmlayan benzersiz isim (ör: 0-1 kg Arasý)");
            // 
            // cb_teslimatTipi
            // 
            cb_teslimatTipi.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_teslimatTipi.FormattingEnabled = true;
            cb_teslimatTipi.Location = new Point(120, 460);
            cb_teslimatTipi.Name = "cb_teslimatTipi";
            cb_teslimatTipi.Size = new Size(200, 23);
            cb_teslimatTipi.TabIndex = 3;
            toolTip1.SetToolTip(cb_teslimatTipi, "Teslimat çarpaný için teslimat tipi seçin (Standart, Hýzlý, Ayný Gün vb.)");
            cb_teslimatTipi.Visible = false;
            // 
            // nud_minDeger
            // 
            nud_minDeger.DecimalPlaces = 2;
            nud_minDeger.Location = new Point(120, 490);
            nud_minDeger.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nud_minDeger.Name = "nud_minDeger";
            nud_minDeger.Size = new Size(120, 23);
            nud_minDeger.TabIndex = 4;
            toolTip1.SetToolTip(nud_minDeger, "Tarifeye dahil olan minimum deðer (aðýrlýk/hacim vb.)");
            // 
            // nud_maxDeger
            // 
            nud_maxDeger.DecimalPlaces = 2;
            nud_maxDeger.Location = new Point(120, 520);
            nud_maxDeger.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nud_maxDeger.Name = "nud_maxDeger";
            nud_maxDeger.Size = new Size(120, 23);
            nud_maxDeger.TabIndex = 6;
            toolTip1.SetToolTip(nud_maxDeger, "Tarifeye dahil olan maksimum deðer (üst sýnýr)");
            // 
            // nud_deger
            // 
            nud_deger.DecimalPlaces = 4;
            nud_deger.Location = new Point(120, 550);
            nud_deger.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nud_deger.Name = "nud_deger";
            nud_deger.Size = new Size(120, 23);
            nud_deger.TabIndex = 113;
            toolTip1.SetToolTip(nud_deger, "Uygulanacak ücret miktarý veya çarpan deðeri");
            // 
            // cb_birim
            // 
            cb_birim.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_birim.FormattingEnabled = true;
            cb_birim.Location = new Point(120, 580);
            cb_birim.Name = "cb_birim";
            cb_birim.Size = new Size(120, 23);
            cb_birim.TabIndex = 9;
            toolTip1.SetToolTip(cb_birim, "Deðer birimi (TL/kg, TL, Çarpan, %)");
            // 
            // ckb_aktif
            // 
            ckb_aktif.AutoSize = true;
            ckb_aktif.Checked = true;
            ckb_aktif.CheckState = CheckState.Checked;
            ckb_aktif.Location = new Point(350, 402);
            ckb_aktif.Name = "ckb_aktif";
            ckb_aktif.Size = new Size(51, 19);
            ckb_aktif.TabIndex = 10;
            ckb_aktif.Text = "Aktif";
            toolTip1.SetToolTip(ckb_aktif, "Bu tarife þu anda aktif mi? (Pasif tarifeler hesaplamalarda kullanýlmaz)");
            ckb_aktif.UseVisualStyleBackColor = true;
            // 
            // nud_oncelik
            // 
            nud_oncelik.Location = new Point(450, 430);
            nud_oncelik.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
            nud_oncelik.Name = "nud_oncelik";
            nud_oncelik.Size = new Size(80, 23);
            nud_oncelik.TabIndex = 112;
            toolTip1.SetToolTip(nud_oncelik, "Sýralama önceliði (düþük numara önce kontrol edilir)");
            // 
            // dtp_gecerlilikBaslangic
            // 
            dtp_gecerlilikBaslangic.Format = DateTimePickerFormat.Short;
            dtp_gecerlilikBaslangic.Location = new Point(450, 460);
            dtp_gecerlilikBaslangic.Name = "dtp_gecerlilikBaslangic";
            dtp_gecerlilikBaslangic.Size = new Size(120, 23);
            dtp_gecerlilikBaslangic.TabIndex = 12;
            toolTip1.SetToolTip(dtp_gecerlilikBaslangic, "Tarifeyi geçerlilik baþlangýç tarihi");
            // 
            // dtp_gecerlilikBitis
            // 
            dtp_gecerlilikBitis.Format = DateTimePickerFormat.Short;
            dtp_gecerlilikBitis.Location = new Point(450, 490);
            dtp_gecerlilikBitis.Name = "dtp_gecerlilikBitis";
            dtp_gecerlilikBitis.Size = new Size(120, 23);
            dtp_gecerlilikBitis.TabIndex = 13;
            toolTip1.SetToolTip(dtp_gecerlilikBitis, "Tarifeyi geçerlilik bitiþ tarihi (süresiz ise iþaretlemeyin)");
            // 
            // ckb_suresizGecerli
            // 
            ckb_suresizGecerli.AutoSize = true;
            ckb_suresizGecerli.Checked = true;
            ckb_suresizGecerli.CheckState = CheckState.Checked;
            ckb_suresizGecerli.Location = new Point(580, 492);
            ckb_suresizGecerli.Name = "ckb_suresizGecerli";
            ckb_suresizGecerli.Size = new Size(62, 19);
            ckb_suresizGecerli.TabIndex = 14;
            ckb_suresizGecerli.Text = "Süresiz";
            toolTip1.SetToolTip(ckb_suresizGecerli, "Tarife sonsuz zamana kadar geçerli");
            ckb_suresizGecerli.UseVisualStyleBackColor = true;
            // 
            // tb_aciklama
            // 
            tb_aciklama.Location = new Point(120, 610);
            tb_aciklama.Multiline = true;
            tb_aciklama.Name = "tb_aciklama";
            tb_aciklama.Size = new Size(400, 60);
            tb_aciklama.TabIndex = 15;
            toolTip1.SetToolTip(tb_aciklama, "Tarife hakkýnda ek bilgi veya notlar");
            // 
            // btn_kaydet
            // 
            btn_kaydet.Location = new Point(700, 400);
            btn_kaydet.Name = "btn_kaydet";
            btn_kaydet.Size = new Size(100, 30);
            btn_kaydet.TabIndex = 16;
            btn_kaydet.Text = "Kaydet";
            toolTip1.SetToolTip(btn_kaydet, "Yeni tarife ekle veya mevcut tarifeyi güncelle");
            btn_kaydet.UseVisualStyleBackColor = true;
            btn_kaydet.Click += Btn_kaydet_Click;
            // 
            // btn_sil
            // 
            btn_sil.Location = new Point(700, 440);
            btn_sil.Name = "btn_sil";
            btn_sil.Size = new Size(100, 30);
            btn_sil.TabIndex = 17;
            btn_sil.Text = "Sil";
            toolTip1.SetToolTip(btn_sil, "Seçili tarifeyi veritabanýndan sil");
            btn_sil.UseVisualStyleBackColor = true;
            btn_sil.Click += Btn_sil_Click;
            // 
            // btn_temizle
            // 
            btn_temizle.Location = new Point(700, 480);
            btn_temizle.Name = "btn_temizle";
            btn_temizle.Size = new Size(100, 30);
            btn_temizle.TabIndex = 18;
            btn_temizle.Text = "Temizle";
            toolTip1.SetToolTip(btn_temizle, "Formu temizle ve yeni kayýt moduna geç");
            btn_temizle.UseVisualStyleBackColor = true;
            btn_temizle.Click += Btn_temizle_Click;
            // 
            // cb_tarifeTuruFiltre
            // 
            cb_tarifeTuruFiltre.FormattingEnabled = true;
            cb_tarifeTuruFiltre.Location = new Point(120, 30);
            cb_tarifeTuruFiltre.Name = "cb_tarifeTuruFiltre";
            cb_tarifeTuruFiltre.Size = new Size(200, 23);
            cb_tarifeTuruFiltre.TabIndex = 18;
            toolTip1.SetToolTip(cb_tarifeTuruFiltre, "Tarifeleri türüne göre filtreleyin");
            // 
            // btn_filtre
            // 
            btn_filtre.Location = new Point(350, 28);
            btn_filtre.Name = "btn_filtre";
            btn_filtre.Size = new Size(100, 30);
            btn_filtre.TabIndex = 19;
            btn_filtre.Text = "Filtrele";
            toolTip1.SetToolTip(btn_filtre, "Seçili türe göre tarifeleri filtrele");
            btn_filtre.UseVisualStyleBackColor = true;
            btn_filtre.Click += Btn_filtre_Click;
            // 
            // ckb_minDegerYok
            // 
            ckb_minDegerYok.AutoSize = true;
            ckb_minDegerYok.Location = new Point(250, 492);
            ckb_minDegerYok.Name = "ckb_minDegerYok";
            ckb_minDegerYok.Size = new Size(45, 19);
            ckb_minDegerYok.TabIndex = 5;
            ckb_minDegerYok.Text = "Yok";
            toolTip1.SetToolTip(ckb_minDegerYok, "Alt sýnýr yok (0'dan baþla)");
            ckb_minDegerYok.UseVisualStyleBackColor = true;
            // 
            // ckb_maxDegerYok
            // 
            ckb_maxDegerYok.AutoSize = true;
            ckb_maxDegerYok.Location = new Point(250, 522);
            ckb_maxDegerYok.Name = "ckb_maxDegerYok";
            ckb_maxDegerYok.Size = new Size(45, 19);
            ckb_maxDegerYok.TabIndex = 7;
            ckb_maxDegerYok.Text = "Yok";
            toolTip1.SetToolTip(ckb_maxDegerYok, "Üst sýnýr yok (sonsuz)");
            ckb_maxDegerYok.UseVisualStyleBackColor = true;
            // 
            // lbl_tarifeTuru
            // 
            lbl_tarifeTuru.AutoSize = true;
            lbl_tarifeTuru.Location = new Point(12, 405);
            lbl_tarifeTuru.Name = "lbl_tarifeTuru";
            lbl_tarifeTuru.Size = new Size(67, 15);
            lbl_tarifeTuru.TabIndex = 101;
            lbl_tarifeTuru.Text = "Tarife Türü:";
            // 
            // lbl_tarifeAdi
            // 
            lbl_tarifeAdi.AutoSize = true;
            lbl_tarifeAdi.Location = new Point(12, 435);
            lbl_tarifeAdi.Name = "lbl_tarifeAdi";
            lbl_tarifeAdi.Size = new Size(60, 15);
            lbl_tarifeAdi.TabIndex = 102;
            lbl_tarifeAdi.Text = "Tarife Adý:";
            // 
            // lbl_teslimatTipi
            // 
            lbl_teslimatTipi.AutoSize = true;
            lbl_teslimatTipi.Location = new Point(12, 465);
            lbl_teslimatTipi.Name = "lbl_teslimatTipi";
            lbl_teslimatTipi.Size = new Size(77, 15);
            lbl_teslimatTipi.TabIndex = 103;
            lbl_teslimatTipi.Text = "Teslimat Tipi:";
            lbl_teslimatTipi.Visible = false;
            // 
            // lbl_minDeger
            // 
            lbl_minDeger.AutoSize = true;
            lbl_minDeger.Location = new Point(12, 495);
            lbl_minDeger.Name = "lbl_minDeger";
            lbl_minDeger.Size = new Size(97, 15);
            lbl_minDeger.TabIndex = 104;
            lbl_minDeger.Text = "Minimum Deðer:";
            // 
            // lbl_maxDeger
            // 
            lbl_maxDeger.AutoSize = true;
            lbl_maxDeger.Location = new Point(12, 525);
            lbl_maxDeger.Name = "lbl_maxDeger";
            lbl_maxDeger.Size = new Size(104, 15);
            lbl_maxDeger.TabIndex = 105;
            lbl_maxDeger.Text = "Maksimum Deðer:";
            // 
            // lbl_deger
            // 
            lbl_deger.AutoSize = true;
            lbl_deger.Location = new Point(12, 555);
            lbl_deger.Name = "lbl_deger";
            lbl_deger.Size = new Size(81, 15);
            lbl_deger.TabIndex = 106;
            lbl_deger.Text = "Ücret/Çarpan:";
            // 
            // lbl_birim
            // 
            lbl_birim.AutoSize = true;
            lbl_birim.Location = new Point(12, 585);
            lbl_birim.Name = "lbl_birim";
            lbl_birim.Size = new Size(38, 15);
            lbl_birim.TabIndex = 107;
            lbl_birim.Text = "Birim:";
            // 
            // lbl_oncelik
            // 
            lbl_oncelik.AutoSize = true;
            lbl_oncelik.Location = new Point(350, 435);
            lbl_oncelik.Name = "lbl_oncelik";
            lbl_oncelik.Size = new Size(50, 15);
            lbl_oncelik.TabIndex = 108;
            lbl_oncelik.Text = "Öncelik:";
            // 
            // lbl_gecerlilikBaslangic
            // 
            lbl_gecerlilikBaslangic.AutoSize = true;
            lbl_gecerlilikBaslangic.Location = new Point(350, 465);
            lbl_gecerlilikBaslangic.Name = "lbl_gecerlilikBaslangic";
            lbl_gecerlilikBaslangic.Size = new Size(85, 15);
            lbl_gecerlilikBaslangic.TabIndex = 109;
            lbl_gecerlilikBaslangic.Text = "Geçerlilik Baþl.:";
            // 
            // lbl_gecerlilikBitis
            // 
            lbl_gecerlilikBitis.AutoSize = true;
            lbl_gecerlilikBitis.Location = new Point(350, 495);
            lbl_gecerlilikBitis.Name = "lbl_gecerlilikBitis";
            lbl_gecerlilikBitis.Size = new Size(83, 15);
            lbl_gecerlilikBitis.TabIndex = 110;
            lbl_gecerlilikBitis.Text = "Geçerlilik Bitiþ:";
            // 
            // lbl_aciklama
            // 
            lbl_aciklama.AutoSize = true;
            lbl_aciklama.Location = new Point(12, 615);
            lbl_aciklama.Name = "lbl_aciklama";
            lbl_aciklama.Size = new Size(59, 15);
            lbl_aciklama.TabIndex = 111;
            lbl_aciklama.Text = "Açýklama:";
            // 
            // lbl_filtre
            // 
            lbl_filtre.AutoSize = true;
            lbl_filtre.Location = new Point(12, 35);
            lbl_filtre.Name = "lbl_filtre";
            lbl_filtre.Size = new Size(96, 15);
            lbl_filtre.TabIndex = 100;
            lbl_filtre.Text = "Tarife Türü Filtre:";
            // 
            // TarifeYonetimForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 691);
            Controls.Add(lbl_aciklama);
            Controls.Add(lbl_gecerlilikBitis);
            Controls.Add(lbl_gecerlilikBaslangic);
            Controls.Add(lbl_oncelik);
            Controls.Add(lbl_birim);
            Controls.Add(lbl_deger);
            Controls.Add(lbl_maxDeger);
            Controls.Add(lbl_minDeger);
            Controls.Add(lbl_teslimatTipi);
            Controls.Add(lbl_tarifeAdi);
            Controls.Add(lbl_tarifeTuru);
            Controls.Add(lbl_filtre);
            Controls.Add(btn_filtre);
            Controls.Add(cb_tarifeTuruFiltre);
            Controls.Add(btn_temizle);
            Controls.Add(btn_sil);
            Controls.Add(btn_kaydet);
            Controls.Add(tb_aciklama);
            Controls.Add(ckb_suresizGecerli);
            Controls.Add(dtp_gecerlilikBitis);
            Controls.Add(dtp_gecerlilikBaslangic);
            Controls.Add(nud_oncelik);
            Controls.Add(ckb_aktif);
            Controls.Add(cb_birim);
            Controls.Add(nud_deger);
            Controls.Add(ckb_maxDegerYok);
            Controls.Add(nud_maxDeger);
            Controls.Add(ckb_minDegerYok);
            Controls.Add(nud_minDeger);
            Controls.Add(cb_teslimatTipi);
            Controls.Add(tb_tarifeAdi);
            Controls.Add(cb_tarifeTuru);
            Controls.Add(dgv_tarifeler);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "TarifeYonetimForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Fiyatlandýrma Tarife Yönetimi";
            Load += TarifeYonetimForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgv_tarifeler).EndInit();
            ((System.ComponentModel.ISupportInitialize)nud_minDeger).EndInit();
            ((System.ComponentModel.ISupportInitialize)nud_maxDeger).EndInit();
            ((System.ComponentModel.ISupportInitialize)nud_deger).EndInit();
            ((System.ComponentModel.ISupportInitialize)nud_oncelik).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_tarifeler;
        private System.Windows.Forms.ComboBox cb_tarifeTuru;
        private System.Windows.Forms.TextBox tb_tarifeAdi;
        private System.Windows.Forms.ComboBox cb_teslimatTipi;
        private System.Windows.Forms.NumericUpDown nud_minDeger;
        private System.Windows.Forms.NumericUpDown nud_maxDeger;
        private System.Windows.Forms.NumericUpDown nud_deger;
        private System.Windows.Forms.ComboBox cb_birim;
        private System.Windows.Forms.CheckBox ckb_aktif;
        private System.Windows.Forms.NumericUpDown nud_oncelik;
        private System.Windows.Forms.DateTimePicker dtp_gecerlilikBaslangic;
        private System.Windows.Forms.DateTimePicker dtp_gecerlilikBitis;
        private System.Windows.Forms.CheckBox ckb_suresizGecerli;
        private System.Windows.Forms.TextBox tb_aciklama;
        private System.Windows.Forms.Button btn_kaydet;
        private System.Windows.Forms.Button btn_sil;
        private System.Windows.Forms.Button btn_temizle;
        private System.Windows.Forms.ComboBox cb_tarifeTuruFiltre;
        private System.Windows.Forms.Button btn_filtre;
        private System.Windows.Forms.CheckBox ckb_minDegerYok;
        private System.Windows.Forms.CheckBox ckb_maxDegerYok;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lbl_tarifeTuru;
        private System.Windows.Forms.Label lbl_tarifeAdi;
        private System.Windows.Forms.Label lbl_teslimatTipi;
        private System.Windows.Forms.Label lbl_minDeger;
        private System.Windows.Forms.Label lbl_maxDeger;
        private System.Windows.Forms.Label lbl_deger;
        private System.Windows.Forms.Label lbl_birim;
        private System.Windows.Forms.Label lbl_oncelik;
        private System.Windows.Forms.Label lbl_gecerlilikBaslangic;
        private System.Windows.Forms.Label lbl_gecerlilikBitis;
        private System.Windows.Forms.Label lbl_aciklama;
        private System.Windows.Forms.Label lbl_filtre;
    }
}
