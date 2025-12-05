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
            this.components = new System.ComponentModel.Container();
            this.dgv_tarifeler = new System.Windows.Forms.DataGridView();
            this.cb_tarifeTuru = new System.Windows.Forms.ComboBox();
            this.tb_tarifeAdi = new System.Windows.Forms.TextBox();
            this.cb_teslimatTipi = new System.Windows.Forms.ComboBox();
            this.nud_minDeger = new System.Windows.Forms.NumericUpDown();
            this.nud_maxDeger = new System.Windows.Forms.NumericUpDown();
            this.nud_deger = new System.Windows.Forms.NumericUpDown();
            this.cb_birim = new System.Windows.Forms.ComboBox();
            this.ckb_aktif = new System.Windows.Forms.CheckBox();
            this.nud_oncelik = new System.Windows.Forms.NumericUpDown();
            this.dtp_gecerlilikBaslangic = new System.Windows.Forms.DateTimePicker();
            this.dtp_gecerlilikBitis = new System.Windows.Forms.DateTimePicker();
            this.ckb_suresizGecerli = new System.Windows.Forms.CheckBox();
            this.tb_aciklama = new System.Windows.Forms.TextBox();
            this.btn_kaydet = new System.Windows.Forms.Button();
            this.btn_sil = new System.Windows.Forms.Button();
            this.btn_temizle = new System.Windows.Forms.Button();
            this.cb_tarifeTuruFiltre = new System.Windows.Forms.ComboBox();
            this.btn_filtre = new System.Windows.Forms.Button();
            this.ckb_minDegerYok = new System.Windows.Forms.CheckBox();
            this.ckb_maxDegerYok = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lbl_tarifeTuru = new System.Windows.Forms.Label();
            this.lbl_tarifeAdi = new System.Windows.Forms.Label();
            this.lbl_teslimatTipi = new System.Windows.Forms.Label();
            this.lbl_minDeger = new System.Windows.Forms.Label();
            this.lbl_maxDeger = new System.Windows.Forms.Label();
            this.lbl_deger = new System.Windows.Forms.Label();
            this.lbl_birim = new System.Windows.Forms.Label();
            this.lbl_oncelik = new System.Windows.Forms.Label();
            this.lbl_gecerlilikBaslangic = new System.Windows.Forms.Label();
            this.lbl_gecerlilikBitis = new System.Windows.Forms.Label();
            this.lbl_aciklama = new System.Windows.Forms.Label();
            this.lbl_filtre = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_tarifeler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_minDeger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_maxDeger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_deger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_oncelik)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_tarifeler
            // 
            this.dgv_tarifeler.AllowUserToAddRows = false;
            this.dgv_tarifeler.AllowUserToDeleteRows = false;
            this.dgv_tarifeler.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_tarifeler.Location = new System.Drawing.Point(12, 80);
            this.dgv_tarifeler.Name = "dgv_tarifeler";
            this.dgv_tarifeler.ReadOnly = true;
            this.dgv_tarifeler.Size = new System.Drawing.Size(960, 300);
            this.dgv_tarifeler.TabIndex = 0;
            this.toolTip1.SetToolTip(this.dgv_tarifeler, "Mevcut fiyatlandýrma tarifelerinin listesi");
            // 
            // lbl_filtre
            // 
            this.lbl_filtre.AutoSize = true;
            this.lbl_filtre.Location = new System.Drawing.Point(12, 35);
            this.lbl_filtre.Name = "lbl_filtre";
            this.lbl_filtre.Size = new System.Drawing.Size(78, 15);
            this.lbl_filtre.TabIndex = 100;
            this.lbl_filtre.Text = "Tarife Türü Filtre:";
            // 
            // cb_tarifeTuruFiltre
            // 
            this.cb_tarifeTuruFiltre.FormattingEnabled = true;
            this.cb_tarifeTuruFiltre.Location = new System.Drawing.Point(120, 30);
            this.cb_tarifeTuruFiltre.Name = "cb_tarifeTuruFiltre";
            this.cb_tarifeTuruFiltre.Size = new System.Drawing.Size(200, 23);
            this.cb_tarifeTuruFiltre.TabIndex = 18;
            this.toolTip1.SetToolTip(this.cb_tarifeTuruFiltre, "Tarifeleri türüne göre filtreleyin");
            // 
            // btn_filtre
            // 
            this.btn_filtre.Location = new System.Drawing.Point(350, 28);
            this.btn_filtre.Name = "btn_filtre";
            this.btn_filtre.Size = new System.Drawing.Size(100, 30);
            this.btn_filtre.TabIndex = 19;
            this.btn_filtre.Text = "Filtrele";
            this.btn_filtre.UseVisualStyleBackColor = true;
            this.btn_filtre.Click += new System.EventHandler(this.Btn_filtre_Click);
            this.toolTip1.SetToolTip(this.btn_filtre, "Seçili türe göre tarifeleri filtrele");
            // 
            // lbl_tarifeTuru
            // 
            this.lbl_tarifeTuru.AutoSize = true;
            this.lbl_tarifeTuru.Location = new System.Drawing.Point(12, 405);
            this.lbl_tarifeTuru.Name = "lbl_tarifeTuru";
            this.lbl_tarifeTuru.Size = new System.Drawing.Size(68, 15);
            this.lbl_tarifeTuru.TabIndex = 101;
            this.lbl_tarifeTuru.Text = "Tarife Türü:";
            // 
            // cb_tarifeTuru
            // 
            this.cb_tarifeTuru.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_tarifeTuru.FormattingEnabled = true;
            this.cb_tarifeTuru.Location = new System.Drawing.Point(120, 400);
            this.cb_tarifeTuru.Name = "cb_tarifeTuru";
            this.cb_tarifeTuru.Size = new System.Drawing.Size(200, 23);
            this.cb_tarifeTuru.TabIndex = 1;
            this.toolTip1.SetToolTip(this.cb_tarifeTuru, "Tarife kategorisi: Aðýrlýk, Hacim, Teslimat Tipi vb.");
            // 
            // lbl_tarifeAdi
            // 
            this.lbl_tarifeAdi.AutoSize = true;
            this.lbl_tarifeAdi.Location = new System.Drawing.Point(12, 435);
            this.lbl_tarifeAdi.Name = "lbl_tarifeAdi";
            this.lbl_tarifeAdi.Size = new System.Drawing.Size(64, 15);
            this.lbl_tarifeAdi.TabIndex = 102;
            this.lbl_tarifeAdi.Text = "Tarife Adý:";
            // 
            // tb_tarifeAdi
            // 
            this.tb_tarifeAdi.Location = new System.Drawing.Point(120, 430);
            this.tb_tarifeAdi.Name = "tb_tarifeAdi";
            this.tb_tarifeAdi.Size = new System.Drawing.Size(200, 23);
            this.tb_tarifeAdi.TabIndex = 2;
            this.toolTip1.SetToolTip(this.tb_tarifeAdi, "Tarifeyi tanýmlayan benzersiz isim (ör: 0-1 kg Arasý)");
            // 
            // lbl_teslimatTipi
            // 
            this.lbl_teslimatTipi.AutoSize = true;
            this.lbl_teslimatTipi.Location = new System.Drawing.Point(12, 465);
            this.lbl_teslimatTipi.Name = "lbl_teslimatTipi";
            this.lbl_teslimatTipi.Size = new System.Drawing.Size(80, 15);
            this.lbl_teslimatTipi.TabIndex = 103;
            this.lbl_teslimatTipi.Text = "Teslimat Tipi:";
            this.lbl_teslimatTipi.Visible = false;
            // 
            // cb_teslimatTipi
            // 
            this.cb_teslimatTipi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_teslimatTipi.FormattingEnabled = true;
            this.cb_teslimatTipi.Location = new System.Drawing.Point(120, 460);
            this.cb_teslimatTipi.Name = "cb_teslimatTipi";
            this.cb_teslimatTipi.Size = new System.Drawing.Size(200, 23);
            this.cb_teslimatTipi.TabIndex = 3;
            this.cb_teslimatTipi.Visible = false;
            this.toolTip1.SetToolTip(this.cb_teslimatTipi, "Teslimat çarpaný için teslimat tipi seçin (Standart, Hýzlý, Ayný Gün vb.)");
            // 
            // lbl_minDeger
            // 
            this.lbl_minDeger.AutoSize = true;
            this.lbl_minDeger.Location = new System.Drawing.Point(12, 495);
            this.lbl_minDeger.Name = "lbl_minDeger";
            this.lbl_minDeger.Size = new System.Drawing.Size(97, 15);
            this.lbl_minDeger.TabIndex = 104;
            this.lbl_minDeger.Text = "Minimum Deðer:";
            // 
            // nud_minDeger
            // 
            this.nud_minDeger.DecimalPlaces = 2;
            this.nud_minDeger.Location = new System.Drawing.Point(120, 490);
            this.nud_minDeger.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            this.nud_minDeger.Name = "nud_minDeger";
            this.nud_minDeger.Size = new System.Drawing.Size(120, 23);
            this.nud_minDeger.TabIndex = 4;
            this.toolTip1.SetToolTip(this.nud_minDeger, "Tarifeye dahil olan minimum deðer (aðýrlýk/hacim vb.)");
            //
            // ckb_minDegerYok
            //
            this.ckb_minDegerYok.AutoSize = true;
            this.ckb_minDegerYok.Location = new System.Drawing.Point(250, 492);
            this.ckb_minDegerYok.Name = "ckb_minDegerYok";
            this.ckb_minDegerYok.Size = new System.Drawing.Size(50, 19);
            this.ckb_minDegerYok.TabIndex = 5;
            this.ckb_minDegerYok.Text = "Yok";
            this.ckb_minDegerYok.UseVisualStyleBackColor = true;
            this.toolTip1.SetToolTip(this.ckb_minDegerYok, "Alt sýnýr yok (0'dan baþla)");
            // 
            // lbl_maxDeger
            // 
            this.lbl_maxDeger.AutoSize = true;
            this.lbl_maxDeger.Location = new System.Drawing.Point(12, 525);
            this.lbl_maxDeger.Name = "lbl_maxDeger";
            this.lbl_maxDeger.Size = new System.Drawing.Size(102, 15);
            this.lbl_maxDeger.TabIndex = 105;
            this.lbl_maxDeger.Text = "Maksimum Deðer:";
            // 
            // nud_maxDeger
            // 
            this.nud_maxDeger.DecimalPlaces = 2;
            this.nud_maxDeger.Location = new System.Drawing.Point(120, 520);
            this.nud_maxDeger.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            this.nud_maxDeger.Name = "nud_maxDeger";
            this.nud_maxDeger.Size = new System.Drawing.Size(120, 23);
            this.nud_maxDeger.TabIndex = 6;
            this.toolTip1.SetToolTip(this.nud_maxDeger, "Tarifeye dahil olan maksimum deðer (üst sýnýr)");
            //
            // ckb_maxDegerYok
            //
            this.ckb_maxDegerYok.AutoSize = true;
            this.ckb_maxDegerYok.Location = new System.Drawing.Point(250, 522);
            this.ckb_maxDegerYok.Name = "ckb_maxDegerYok";
            this.ckb_maxDegerYok.Size = new System.Drawing.Size(50, 19);
            this.ckb_maxDegerYok.TabIndex = 7;
            this.ckb_maxDegerYok.Text = "Yok";
            this.ckb_maxDegerYok.UseVisualStyleBackColor = true;
            this.toolTip1.SetToolTip(this.ckb_maxDegerYok, "Üst sýnýr yok (sonsuz)");
            // 
            // lbl_deger
            // 
            this.lbl_deger.AutoSize = true;
            this.lbl_deger.Location = new System.Drawing.Point(12, 555);
            this.lbl_deger.Name = "lbl_deger";
            this.lbl_deger.Size = new System.Drawing.Size(86, 15);
            this.lbl_deger.TabIndex = 106;
            this.lbl_deger.Text = "Ücret/Çarpan:";
            // 
            // nud_deger
            // 
            this.nud_deger.DecimalPlaces = 4;
            this.nud_deger.Location = new System.Drawing.Point(120, 550);
            this.nud_deger.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            this.nud_deger.Name = "nud_deger";
            this.nud_deger.Size = new System.Drawing.Size(120, 23);
            this.toolTip1.SetToolTip(this.nud_deger, "Uygulanacak ücret miktarý veya çarpan deðeri");
            // 
            // lbl_birim
            // 
            this.lbl_birim.AutoSize = true;
            this.lbl_birim.Location = new System.Drawing.Point(12, 585);
            this.lbl_birim.Name = "lbl_birim";
            this.lbl_birim.Size = new System.Drawing.Size(40, 15);
            this.lbl_birim.TabIndex = 107;
            this.lbl_birim.Text = "Birim:";
            // 
            // cb_birim
            // 
            this.cb_birim.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_birim.FormattingEnabled = true;
            this.cb_birim.Location = new System.Drawing.Point(120, 580);
            this.cb_birim.Name = "cb_birim";
            this.cb_birim.Size = new System.Drawing.Size(120, 23);
            this.cb_birim.TabIndex = 9;
            this.toolTip1.SetToolTip(this.cb_birim, "Deðer birimi (TL/kg, TL, Çarpan, %)");
            // 
            // ckb_aktif
            // 
            this.ckb_aktif.AutoSize = true;
            this.ckb_aktif.Checked = true;
            this.ckb_aktif.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckb_aktif.Location = new System.Drawing.Point(350, 402);
            this.ckb_aktif.Name = "ckb_aktif";
            this.ckb_aktif.Size = new System.Drawing.Size(53, 19);
            this.ckb_aktif.TabIndex = 10;
            this.ckb_aktif.Text = "Aktif";
            this.ckb_aktif.UseVisualStyleBackColor = true;
            this.toolTip1.SetToolTip(this.ckb_aktif, "Bu tarife þu anda aktif mi? (Pasif tarifeler hesaplamalarda kullanýlmaz)");
            // 
            // lbl_oncelik
            // 
            this.lbl_oncelik.AutoSize = true;
            this.lbl_oncelik.Location = new System.Drawing.Point(350, 435);
            this.lbl_oncelik.Name = "lbl_oncelik";
            this.lbl_oncelik.Size = new System.Drawing.Size(51, 15);
            this.lbl_oncelik.TabIndex = 108;
            this.lbl_oncelik.Text = "Öncelik:";
            // 
            // nud_oncelik
            // 
            this.nud_oncelik.Location = new System.Drawing.Point(450, 430);
            this.nud_oncelik.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
            this.nud_oncelik.Name = "nud_oncelik";
            this.nud_oncelik.Size = new System.Drawing.Size(80, 23);
            this.toolTip1.SetToolTip(this.nud_oncelik, "Sýralama önceliði (düþük numara önce kontrol edilir)");
            // 
            // lbl_gecerlilikBaslangic
            // 
            this.lbl_gecerlilikBaslangic.AutoSize = true;
            this.lbl_gecerlilikBaslangic.Location = new System.Drawing.Point(350, 465);
            this.lbl_gecerlilikBaslangic.Name = "lbl_gecerlilikBaslangic";
            this.lbl_gecerlilikBaslangic.Size = new System.Drawing.Size(86, 15);
            this.lbl_gecerlilikBaslangic.TabIndex = 109;
            this.lbl_gecerlilikBaslangic.Text = "Geçerlilik Baþl.:";
            // 
            // dtp_gecerlilikBaslangic
            // 
            this.dtp_gecerlilikBaslangic.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_gecerlilikBaslangic.Location = new System.Drawing.Point(450, 460);
            this.dtp_gecerlilikBaslangic.Name = "dtp_gecerlilikBaslangic";
            this.dtp_gecerlilikBaslangic.Size = new System.Drawing.Size(120, 23);
            this.dtp_gecerlilikBaslangic.TabIndex = 12;
            this.toolTip1.SetToolTip(this.dtp_gecerlilikBaslangic, "Tarifeyi geçerlilik baþlangýç tarihi");
            // 
            // lbl_gecerlilikBitis
            // 
            this.lbl_gecerlilikBitis.AutoSize = true;
            this.lbl_gecerlilikBitis.Location = new System.Drawing.Point(350, 495);
            this.lbl_gecerlilikBitis.Name = "lbl_gecerlilikBitis";
            this.lbl_gecerlilikBitis.Size = new System.Drawing.Size(82, 15);
            this.lbl_gecerlilikBitis.TabIndex = 110;
            this.lbl_gecerlilikBitis.Text = "Geçerlilik Bitiþ:";
            // 
            // dtp_gecerlilikBitis
            // 
            this.dtp_gecerlilikBitis.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_gecerlilikBitis.Location = new System.Drawing.Point(450, 490);
            this.dtp_gecerlilikBitis.Name = "dtp_gecerlilikBitis";
            this.dtp_gecerlilikBitis.Size = new System.Drawing.Size(120, 23);
            this.dtp_gecerlilikBitis.TabIndex = 13;
            this.toolTip1.SetToolTip(this.dtp_gecerlilikBitis, "Tarifeyi geçerlilik bitiþ tarihi (süresiz ise iþaretlemeyin)");
            // 
            // ckb_suresizGecerli
            // 
            this.ckb_suresizGecerli.AutoSize = true;
            this.ckb_suresizGecerli.Checked = true;
            this.ckb_suresizGecerli.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckb_suresizGecerli.Location = new System.Drawing.Point(580, 492);
            this.ckb_suresizGecerli.Name = "ckb_suresizGecerli";
            this.ckb_suresizGecerli.Size = new System.Drawing.Size(69, 19);
            this.ckb_suresizGecerli.TabIndex = 14;
            this.ckb_suresizGecerli.Text = "Süresiz";
            this.ckb_suresizGecerli.UseVisualStyleBackColor = true;
            this.toolTip1.SetToolTip(this.ckb_suresizGecerli, "Tarife sonsuz zamana kadar geçerli");
            // 
            // lbl_aciklama
            // 
            this.lbl_aciklama.AutoSize = true;
            this.lbl_aciklama.Location = new System.Drawing.Point(12, 615);
            this.lbl_aciklama.Name = "lbl_aciklama";
            this.lbl_aciklama.Size = new System.Drawing.Size(64, 15);
            this.lbl_aciklama.TabIndex = 111;
            this.lbl_aciklama.Text = "Açýklama:";
            // 
            // tb_aciklama
            // 
            this.tb_aciklama.Location = new System.Drawing.Point(120, 610);
            this.tb_aciklama.Multiline = true;
            this.tb_aciklama.Name = "tb_aciklama";
            this.tb_aciklama.Size = new System.Drawing.Size(400, 60);
            this.tb_aciklama.TabIndex = 15;
            this.toolTip1.SetToolTip(this.tb_aciklama, "Tarife hakkýnda ek bilgi veya notlar");
            // 
            // btn_kaydet
            // 
            this.btn_kaydet.Location = new System.Drawing.Point(700, 400);
            this.btn_kaydet.Name = "btn_kaydet";
            this.btn_kaydet.Size = new System.Drawing.Size(100, 30);
            this.btn_kaydet.TabIndex = 16;
            this.btn_kaydet.Text = "Kaydet";
            this.btn_kaydet.UseVisualStyleBackColor = true;
            this.btn_kaydet.Click += new System.EventHandler(this.Btn_kaydet_Click);
            this.toolTip1.SetToolTip(this.btn_kaydet, "Yeni tarife ekle veya mevcut tarifeyi güncelle");
            // 
            // btn_sil
            // 
            this.btn_sil.Location = new System.Drawing.Point(700, 440);
            this.btn_sil.Name = "btn_sil";
            this.btn_sil.Size = new System.Drawing.Size(100, 30);
            this.btn_sil.TabIndex = 17;
            this.btn_sil.Text = "Sil";
            this.btn_sil.UseVisualStyleBackColor = true;
            this.btn_sil.Click += new System.EventHandler(this.Btn_sil_Click);
            this.toolTip1.SetToolTip(this.btn_sil, "Seçili tarifeyi veritabanýndan sil");
            // 
            // btn_temizle
            // 
            this.btn_temizle.Location = new System.Drawing.Point(700, 480);
            this.btn_temizle.Name = "btn_temizle";
            this.btn_temizle.Size = new System.Drawing.Size(100, 30);
            this.btn_temizle.TabIndex = 18;
            this.btn_temizle.Text = "Temizle";
            this.btn_temizle.UseVisualStyleBackColor = true;
            this.btn_temizle.Click += new System.EventHandler(this.Btn_temizle_Click);
            this.toolTip1.SetToolTip(this.btn_temizle, "Formu temizle ve yeni kayýt moduna geç");
            // 
            // TarifeYonetimForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 691);
            this.Controls.Add(this.lbl_aciklama);
            this.Controls.Add(this.lbl_gecerlilikBitis);
            this.Controls.Add(this.lbl_gecerlilikBaslangic);
            this.Controls.Add(this.lbl_oncelik);
            this.Controls.Add(this.lbl_birim);
            this.Controls.Add(this.lbl_deger);
            this.Controls.Add(this.lbl_maxDeger);
            this.Controls.Add(this.lbl_minDeger);
            this.Controls.Add(this.lbl_teslimatTipi);
            this.Controls.Add(this.lbl_tarifeAdi);
            this.Controls.Add(this.lbl_tarifeTuru);
            this.Controls.Add(this.lbl_filtre);
            this.Controls.Add(this.btn_filtre);
            this.Controls.Add(this.cb_tarifeTuruFiltre);
            this.Controls.Add(this.btn_temizle);
            this.Controls.Add(this.btn_sil);
            this.Controls.Add(this.btn_kaydet);
            this.Controls.Add(this.tb_aciklama);
            this.Controls.Add(this.ckb_suresizGecerli);
            this.Controls.Add(this.dtp_gecerlilikBitis);
            this.Controls.Add(this.dtp_gecerlilikBaslangic);
            this.Controls.Add(this.nud_oncelik);
            this.Controls.Add(this.ckb_aktif);
            this.Controls.Add(this.cb_birim);
            this.Controls.Add(this.nud_deger);
            this.Controls.Add(this.ckb_maxDegerYok);
            this.Controls.Add(this.nud_maxDeger);
            this.Controls.Add(this.ckb_minDegerYok);
            this.Controls.Add(this.nud_minDeger);
            this.Controls.Add(this.cb_teslimatTipi);
            this.Controls.Add(this.tb_tarifeAdi);
            this.Controls.Add(this.cb_tarifeTuru);
            this.Controls.Add(this.dgv_tarifeler);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TarifeYonetimForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Fiyatlandýrma Tarife Yönetimi";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_tarifeler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_minDeger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_maxDeger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_deger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_oncelik)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
