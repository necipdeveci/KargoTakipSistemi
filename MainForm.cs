using Microsoft.EntityFrameworkCore;
using kargotakipsistemi.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.IdentityModel.Tokens;


namespace kargotakipsistemi;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
        cb_subeIl.SelectedIndexChanged += cb_subeIl_SelectedIndexChanged;
    }


    //Dgv veri çekme metodu 
    private void VeriCek<T>(DataGridView dgv, Func<KtsContext, IQueryable<T>> sorgu) where T : class
    {
        using (var context = new KtsContext())
        {
            var liste = sorgu(context).ToList();
            dgv.AutoGenerateColumns = true;
            dgv.DataSource = liste;
        }
    }

    //dgv_personeller_SelectionChanged seçim değiştiğinde yapılacak işlemler
    private int? secilenPersonelId = null;
    private void dgv_personeller_SelectionChanged(object sender, EventArgs e)
    {
        if (dgv_personeller.SelectedRows.Count > 0)
        {
            var seciliSatir = dgv_personeller.SelectedRows[0];
            secilenPersonelId = Convert.ToInt32(seciliSatir.Cells["PersonelId"].Value);

            tb_personelAd.Text = seciliSatir.Cells["Ad"].Value.ToString();
            tb_personelSoyad.Text = seciliSatir.Cells["Soyad"].Value.ToString();
            tb_personelMail.Text = seciliSatir.Cells["Mail"].Value.ToString();
            tb_personelTel.Text = seciliSatir.Cells["Tel"].Value.ToString();
            tb_personelSifre.ReadOnly = true;
            string cinsiyet = seciliSatir.Cells["Cinsiyet"].Value?.ToString();
            if (!string.IsNullOrEmpty(cinsiyet))
                cb_personelCinsiyet.SelectedItem = cinsiyet;
            else
                cb_personelCinsiyet.SelectedIndex = -1; // Seçim yok
            tb_personelEhliyet.Text = seciliSatir.Cells["EhliyetSinifi"].Value.ToString();
            nud_personelMaas.Value = Convert.ToDecimal(seciliSatir.Cells["Maas"].Value);
            dtp_personelIsegiris.Value = Convert.ToDateTime(seciliSatir.Cells["IsegirisTarihi"].Value);
            dtp_personelIstencikis.Value = Convert.ToDateTime(seciliSatir.Cells["IstencikisTarihi"].Value) == null ? DateTime.Now : Convert.ToDateTime(seciliSatir.Cells["IstencikisTarihi"].Value);
            dtp_personelIstencikis.Enabled = true;
            ckb_personelAktif.Checked = Convert.ToBoolean(seciliSatir.Cells["Aktif"].Value);
            btnPersonelAdresYonet.Enabled = true;
        }
        else
        {
            secilenPersonelId = null;
        }
    }

    //bir cb de seçilen indexden diğer bir cbye ilişkili veri çekme



    //combobox veri çekme
    private void ComboBoxVeriCek<T>(ComboBox cb, Func<KtsContext, IQueryable<T>> sorgu, string displayMember, string valueMember) where T : class
    {
        using (var context = new KtsContext())
        {
            cb.DataSource = sorgu(context).ToList();
            cb.DisplayMember = displayMember;
            cb.ValueMember = valueMember;
        }
    }

    private void cb_personelSube_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cb_personelSube != null)
        {
            int secilenSubeId;
            if (cb_personelSube.SelectedValue is int)
                secilenSubeId = (int)cb_personelSube.SelectedValue;
            else if (cb_personelSube.SelectedValue is Sube sube)
                secilenSubeId = sube.SubeId;
            else
                throw new InvalidOperationException("Seçilen değer beklenen türde değil.");
            ComboBoxVeriCek(cb_personelArac, ctx => ctx.Araclar.Where(x => x.SubeId == secilenSubeId), "AracTip", "AracId");
        }
    }

    //bir cb de seçilen indexden diğer bir cbye ilişkili veri çekme
    private void cb_subeIl_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cb_subeIl.SelectedValue != null)
        {
            int secilenIlId;
            if (cb_subeIl.SelectedValue is int)
                secilenIlId = (int)cb_subeIl.SelectedValue;
            else if (cb_subeIl.SelectedValue is Il il)
                secilenIlId = il.IlId;
            else
                throw new InvalidOperationException("Seçilen değer beklenen türde değil.");
            ComboBoxVeriCek(cb_subeIlce, ctx => ctx.Ilceler.Where(x => x.IlId == secilenIlId), "IlceAd", "IlceId");
        }
    }



    //formu temizler
    private void KontrolleriTemizle(params Control[] controls)
    {
        foreach (var control in controls)
        {
            if (control is TextBox tb)
                tb.Clear();
            else if (control is ComboBox cb)
                cb.SelectedIndex = -1;
            else if (control is NumericUpDown nud)
                nud.Value = nud.Minimum;
            else if (control is CheckBox ckb)
                ckb.Checked = false;
            else if (control is DateTimePicker dtp)
                dtp.Value = DateTime.Now;
        }
    }

    //----------------------------------------------------------------
    private void MainForm_Load(object sender, EventArgs e)
    {
        //
        VeriCek(dgv_personeller, ctx => ctx.Personeller);
        ComboBoxVeriCek(cb_personelRol, ctx => ctx.Roller, "RolAd", "RolId");
        ComboBoxVeriCek(cb_personelSube, ctx => ctx.Subeler, "SubeAd", "SubeId");
        cb_personelCinsiyet.Items.Add("Erkek");
        cb_personelCinsiyet.Items.Add("Kadın");
        cb_personelCinsiyet.SelectedIndex = 0;
        //
        VeriCek(dgv_subeler, ctx =>
            ctx.Subeler
                .Include(s => s.Araclar)
                .Select(s => new
                {
                    s.SubeId,
                    s.SubeAd,
                    s.SubeTip,
                    s.IlId,
                    s.IlceId,
                    s.AcikAdres,
                    s.Tel,
                    s.Email,
                    s.CalismaSaatleri,
                    s.Kapasite,
                    Araclar = s.Araclar != null && s.Araclar.Any()
                        ? string.Join(", ", s.Araclar.Select(a => a.AracTip))
                        : ""
                })
        );
        ComboBoxVeriCek(cb_subeIl, ctx => ctx.Iller, "IlIdVeAd", "IlId");
        using (var context = new KtsContext())
        {
            var tipler = context.Subeler
                .Select(a => a.SubeTip)
                .Distinct()
                .ToList();
            cb_subeFiltre.Items.Clear();
            cb_subeFiltre.Items.AddRange(tipler.ToArray());
        }
        cb_subeTip.Items.Add("Merkez");
        cb_subeTip.Items.Add("Şube");
        cb_subeTip.Items.Add("Dağıtım Noktası");
        cb_subeTip.Items.Add("Depo");
        cb_subeTip.Items.Add("Transfer Merkezi");
        cb_subeTip.Items.Add("Kargo Kabul");
        cb_subeTip.Items.Add("Teslimat Noktası");
        cb_subeCalismaSaat.Items.Add("08:00 - 17:00");
        cb_subeCalismaSaat.Items.Add("09:00 - 18:00");
        cb_subeCalismaSaat.Items.Add("10:00 - 19:00");
        cb_subeCalismaSaat.Items.Add("24 Saat Açık");
        cb_subeCalismaSaat.Items.Add("Hafta İçi 09:00 - 18:00");
        cb_subeCalismaSaat.Items.Add("Hafta Sonu Kapalı");
        cb_subeCalismaSaat.Items.Add("Hafta Sonu 10:00 - 16:00");

        //
        VeriCek(dgv_araclar, ctx =>
        ctx.Araclar
        .Include(a => a.Sube)
        .Select(a => new
        {
            a.AracId,
            a.Plaka,
            a.AracTip,
            a.KapasiteKg,
            a.GpsKodu,
            a.Durum,
            a.SubeId,
            SubeAd = a.Sube != null ? a.Sube.SubeAd : ""
        })
        );
        ComboBoxVeriCek(cb_aracSube, ctx => ctx.Subeler, "SubeAd", "SubeId");
        using (var context = new KtsContext())
        {
            var tipler = context.Araclar
                .Select(a => a.AracTip)
                .Distinct()
                .ToList();
            cb_aracFiltre.Items.Clear();
            cb_aracFiltre.Items.AddRange(tipler.ToArray());
        }
        cb_aracTip.Items.Add("Kamyonet");
        cb_aracTip.Items.Add("Panelvan");
        cb_aracTip.Items.Add("Tır");
        cb_aracTip.Items.Add("Minibüs");
        cb_aracTip.Items.Add("Otomobil");
        cb_aracTip.Items.Add("Motosiklet");
        cb_aracTip.Items.Add("Diğer");
        cb_aracDurum.Items.Add("Aktif");
        cb_aracDurum.Items.Add("Bakımda");
        cb_aracDurum.Items.Add("Arızalı");
        cb_aracDurum.Items.Add("Pasif");
        cb_aracDurum.Items.Add("Serviste");
        cb_aracDurum.Items.Add("Kullanımda");
    }

    //subeler dgv seçim değiştiğinde yapılacak işlemler
    private int? secilenSubeId = null;
    private void dgv_subeler_SelectionChanged(object sender, EventArgs e)
    {
        if (dgv_subeler.SelectedRows.Count > 0)
        {
            var seciliSatir = dgv_subeler.SelectedRows[0];
            secilenSubeId = Convert.ToInt32(seciliSatir.Cells["SubeId"].Value);
            tb_subeAd.Text = seciliSatir.Cells["SubeAd"].Value.ToString();
            string tip = seciliSatir.Cells["SubeTip"].Value?.ToString();
            if (!string.IsNullOrEmpty(tip))
                cb_subeTip.SelectedItem = tip;
            else
                cb_subeTip.SelectedIndex = -1; // Seçim yok
            tb_subeTel.Text = seciliSatir.Cells["Tel"].Value.ToString();
            tb_subeMail.Text = seciliSatir.Cells["Email"].Value.ToString();
            tbm_subeAcikAdres.Text = seciliSatir.Cells["AcikAdres"].Value.ToString();
            cb_subeCalismaSaat.SelectedItem = seciliSatir.Cells["CalismaSaatleri"].Value.ToString();
            nud_subeKapasite.Value = seciliSatir.Cells["Kapasite"].Value != null ? Convert.ToDecimal(seciliSatir.Cells["Kapasite"].Value) : 0;
            cb_subeIl.SelectedValue = seciliSatir.Cells["IlId"].Value;
            cb_subeIlce.SelectedValue = seciliSatir.Cells["IlceId"].Value;
        }
        else
        {
            secilenSubeId = null;
        }
    }


    //araclar dgv seçim değiştiğinde yapılacak işlemler
    private int? secilenAracId = null;
    private void dgv_araclar_SelectionChanged(object sender, EventArgs e)
    {
        if (dgv_araclar.SelectedRows.Count > 0)
        {
            var seciliSatir = dgv_araclar.SelectedRows[0];
            secilenAracId = Convert.ToInt32(seciliSatir.Cells["AracId"].Value);
            tb_aracPlaka.Text = seciliSatir.Cells["Plaka"].Value.ToString();
            cb_aracTip.SelectedItem = seciliSatir.Cells["AracTip"].Value?.ToString();
            nud_aracKapasite.Value = seciliSatir.Cells["KapasiteKg"].Value != null ? Convert.ToDecimal(seciliSatir.Cells["KapasiteKg"].Value) : 0;
            tb_aracGps.Text = seciliSatir.Cells["GpsKodu"].Value?.ToString();
            cb_aracDurum.SelectedItem = seciliSatir.Cells["Durum"].Value?.ToString();
            cb_aracSube.SelectedValue = seciliSatir.Cells["SubeId"].Value;
        }
        else
        {
            secilenAracId = null;
        }
    }


    private void btn_personelKaydet_Click(object sender, EventArgs e)
    {
        using (var context = new KtsContext())
        {
            Personel personel;
            if (secilenPersonelId.HasValue)
            {
                personel = context.Personeller.Find(secilenPersonelId.Value);
                if (personel == null)
                {
                    MessageBox.Show("Personel bulunamadı.");
                    return;
                }
            }
            else
            {
                personel = new Personel();
                personel.Sifre = tb_personelSifre.Text;
                context.Personeller.Add(personel);
            }

            personel.Ad = tb_personelAd.Text;
            personel.Soyad = tb_personelSoyad.Text;
            personel.Mail = tb_personelMail.Text;
            personel.Tel = tb_personelTel.Text;
            personel.EhliyetSinifi = tb_personelEhliyet.Text;

            personel.Maas = nud_personelMaas.Value;
            personel.Cinsiyet = cb_personelCinsiyet.SelectedItem.ToString();
            personel.Rol = cb_personelRol.SelectedItem as Rol;
            personel.Arac = cb_personelArac.SelectedItem as Arac;
            personel.Sube = cb_personelSube.SelectedItem as Sube;

            personel.IseGirisTarihi = dtp_personelIsegiris.Value;
            personel.IstenCikisTarihi = null;
            if (tb_personelEhliyet.Text.Length > 5)
            {
                MessageBox.Show("Ehliyet sınıfı en fazla 5 karakter olmalıdır.");
                return;
            }
            personel.Aktif = ckb_personelAktif.Checked;

            context.SaveChanges();
            VeriCek(dgv_personeller, ctx => ctx.Personeller);
        }
    }
    private void btn_personelKayitSil_Click(object sender, EventArgs e)
    {
        if (secilenPersonelId.HasValue)
        {
            using (var context = new KtsContext())
            {
                var personel = context.Personeller.Find(secilenPersonelId.Value);
                if (personel != null)
                {
                    context.Personeller.Remove(personel);
                    context.SaveChanges();
                    VeriCek(dgv_personeller, ctx =>
                    ctx.Personeller
                        .Include(p => p.Rol)
                        .Include(p => p.Arac)
                        .Include(p => p.Sube)
                        .Select(p => new
                        {
                            p.PersonelId,
                            p.Ad,
                            p.Soyad,
                            p.Mail,
                            p.Tel,
                            p.Cinsiyet,
                            p.EhliyetSinifi,
                            p.Maas,
                            p.IseGirisTarihi,
                            p.IstenCikisTarihi,
                            p.Aktif,
                            Rol = p.Rol != null ? p.Rol.RolAd : "",
                            Arac = p.Arac != null ? p.Arac.AracTip : "",
                            Sube = p.Sube != null ? p.Sube.SubeAd : ""
                        })
                );

                    var roller = context.Personeller
                        .Select(p => p.Rol.RolAd)
                        .Distinct()
                        .ToList();
                    cb_subeFiltre.Items.Clear();
                    cb_subeFiltre.Items.AddRange(roller.ToArray());
                }
                else
                {
                    MessageBox.Show("Silinecek şube bulunamadı.");
                }
            }
        }
        else
        {
            MessageBox.Show("Lütfen silmek için bir şube seçin.");
        }
    }

    private void btn_personelFormTemizle_Click(object sender, EventArgs e)
    {
        KontrolleriTemizle(tb_personelAd, tb_personelSoyad, tb_personelMail, tb_personelTel, tb_personelEhliyet, nud_personelMaas, cb_personelCinsiyet, cb_personelRol, cb_personelArac, cb_personelSube, dtp_personelIsegiris);
    }

    private void btnPersonelAdresYonet_Click(object sender, EventArgs e)
    {
        var form = new AdresEkleForm((int)secilenPersonelId, "Personel");
        form.ShowDialog();
    }

    private void btn_subeAra_Click(object sender, EventArgs e)
    {
        if (cb_subeFiltre.SelectedItem != null)
        {
            string seciliTip = cb_subeFiltre.SelectedItem.ToString();
            VeriCek(dgv_subeler, ctx =>
               ctx.Subeler
                   .Include(s => s.Araclar)
                   .Where(a => a.SubeTip == seciliTip)
                   .Select(s => new
                   {
                       s.SubeId,
                       s.SubeAd,
                       s.SubeTip,
                       s.IlId,
                       s.IlceId,
                       s.AcikAdres,
                       s.Tel,
                       s.Email,
                       s.CalismaSaatleri,
                       s.Kapasite,
                       Araclar = s.Araclar != null && s.Araclar.Any()
                           ? string.Join(", ", s.Araclar.Select(a => a.AracTip))
                           : ""
                   })
           );
        }
        else
        {
            VeriCek(dgv_subeler, ctx =>
               ctx.Subeler
                   .Include(s => s.Araclar)
                   .Select(s => new
                   {
                       s.SubeId,
                       s.SubeAd,
                       s.SubeTip,
                       s.IlId,
                       s.IlceId,
                       s.AcikAdres,
                       s.Tel,
                       s.Email,
                       s.CalismaSaatleri,
                       s.Kapasite,
                       Araclar = s.Araclar != null && s.Araclar.Any()
                           ? string.Join(", ", s.Araclar.Select(a => a.AracTip))
                           : ""
                   })
           );
        }
    }
    private void btn_subeKaydet_Click(object sender, EventArgs e)
    {
        using (var context = new KtsContext())
        {
            Sube sube;
            if (secilenSubeId.HasValue)
            {
                sube = context.Subeler.Find(secilenSubeId.Value);
                if (sube == null)
                {
                    MessageBox.Show("Şube bulunamadı.");
                    return;
                }
            }
            else
            {
                sube = new Sube();
                context.Subeler.Add(sube);
            }

            sube.SubeAd = tb_subeAd.Text;
            sube.SubeTip = cb_subeTip.SelectedItem?.ToString();
            sube.Tel = tb_subeTel.Text;
            sube.Email = tb_subeMail.Text;
            sube.AcikAdres = tbm_subeAcikAdres.Text;
            sube.CalismaSaatleri = cb_subeCalismaSaat.SelectedItem?.ToString();
            sube.Kapasite = (int?)nud_subeKapasite.Value;
            if (cb_subeIl.SelectedValue is int ilId)
                sube.IlId = ilId;
            else if (cb_subeIl.SelectedItem is Il il)
                sube.IlId = il.IlId;

            if (cb_subeIlce.SelectedValue is int ilceId)
                sube.IlceId = ilceId;
            else if (cb_subeIlce.SelectedItem is Ilce ilce)
                sube.IlceId = ilce.IlceId;

            context.SaveChanges();
            VeriCek(dgv_subeler, ctx =>
            ctx.Subeler
                .Include(s => s.Araclar)
                .Select(s => new
                {
                    s.SubeId,
                    s.SubeAd,
                    s.SubeTip,
                    s.IlId,
                    s.IlceId,
                    s.AcikAdres,
                    s.Tel,
                    s.Email,
                    s.CalismaSaatleri,
                    s.Kapasite,
                    Araclar = s.Araclar != null && s.Araclar.Any()
                        ? string.Join(", ", s.Araclar.Select(a => a.AracTip))
                        : ""
                })
        );
            var tipler = context.Subeler
                .Select(a => a.SubeTip)
                .Distinct()
                .ToList();
            cb_subeFiltre.Items.Clear();
            cb_subeFiltre.Items.AddRange(tipler.ToArray());

        }

        ComboBoxVeriCek(cb_aracSube, ctx => ctx.Subeler, "SubeAd", "SubeId");
    }

    private void btn_subeKayitSil_Click(object sender, EventArgs e)
    {
        if (secilenSubeId.HasValue)
        {
            using (var context = new KtsContext())
            {
                var sube = context.Subeler.Find(secilenSubeId.Value);
                if (sube != null)
                {
                    context.Subeler.Remove(sube);
                    context.SaveChanges();
                    VeriCek(dgv_subeler, ctx =>
                    ctx.Subeler
                        .Include(s => s.Araclar)
                        .Select(s => new
                        {
                            s.SubeId,
                            s.SubeAd,
                            s.SubeTip,
                            s.IlId,
                            s.IlceId,
                            s.AcikAdres,
                            s.Tel,
                            s.Email,
                            s.CalismaSaatleri,
                            s.Kapasite,
                            Araclar = s.Araclar != null && s.Araclar.Any()
                                ? string.Join(", ", s.Araclar.Select(a => a.AracTip))
                                : ""
                        })
                    );
                    var tipler = context.Subeler
                       .Select(a => a.SubeTip)
                       .Distinct()
                       .ToList();
                    cb_subeFiltre.Items.Clear();
                    cb_subeFiltre.Items.AddRange(tipler.ToArray());
                }
                else
                {
                    MessageBox.Show("Silinecek şube bulunamadı.");
                }
            }
        }
        else
        {
            MessageBox.Show("Lütfen silmek için bir şube seçin.");
        }
    }

    private void btn_subeFormTemizle_Click(object sender, EventArgs e)
    {
        KontrolleriTemizle(tb_subeAd, cb_subeTip, tb_subeTel, tb_subeMail, tbm_subeAcikAdres, cb_subeCalismaSaat, nud_subeKapasite, cb_subeIl, cb_subeIlce, cb_subeFiltre);
    }

    private void btn_aracAra_Click(object sender, EventArgs e)
    {
        if (cb_aracFiltre.SelectedItem != null)
        {
            string seciliTip = cb_aracFiltre.SelectedItem.ToString();
            VeriCek(dgv_araclar, ctx =>
                ctx.Araclar
                    .Include(a => a.Sube)
                    .Where(a => a.AracTip == seciliTip)
                    .Select(a => new
                    {
                        a.AracId,
                        a.Plaka,
                        a.AracTip,
                        a.KapasiteKg,
                        a.GpsKodu,
                        a.Durum,
                        a.SubeId,
                        SubeAd = a.Sube != null ? a.Sube.SubeAd : ""
                    })
            );
        }
        else
        {
            VeriCek(dgv_araclar, ctx =>
        ctx.Araclar
        .Include(a => a.Sube)
        .Select(a => new
        {
            a.AracId,
            a.Plaka,
            a.AracTip,
            a.KapasiteKg,
            a.GpsKodu,
            a.Durum,
            a.SubeId,
            SubeAd = a.Sube != null ? a.Sube.SubeAd : ""
        })
        );
        }
    }

    private void btn_aracKaydet_Click(object sender, EventArgs e)
    {
        using (var context = new KtsContext())
        {
            Arac arac;
            if (secilenAracId.HasValue)
            {
                arac = context.Araclar.Find(secilenAracId.Value);
                if (arac == null)
                {
                    MessageBox.Show("Araç bulunamadı.");
                    return;
                }
            }
            else
            {
                arac = new Arac();
                context.Araclar.Add(arac);
            }

            arac.Plaka = tb_aracPlaka.Text;
            arac.AracTip = cb_aracTip.SelectedItem?.ToString();
            arac.KapasiteKg = nud_aracKapasite.Value;
            arac.GpsKodu = tb_aracGps.Text;
            arac.Durum = cb_aracDurum.SelectedItem?.ToString();
            if (cb_aracSube.SelectedValue is int subeId)
            {
                arac.SubeId = subeId;
                arac.Sube = context.Subeler.Find(subeId);
            }
            context.SaveChanges();
            VeriCek(dgv_araclar, ctx =>
        ctx.Araclar
        .Include(a => a.Sube)
        .Select(a => new
        {
            a.AracId,
            a.Plaka,
            a.AracTip,
            a.KapasiteKg,
            a.GpsKodu,
            a.Durum,
            a.SubeId,
            SubeAd = a.Sube != null ? a.Sube.SubeAd : ""
        })
        ); var tipler = context.Araclar
                .Select(a => a.AracTip)
                .Distinct()
                .ToList();
            cb_aracFiltre.Items.Clear();
            cb_aracFiltre.Items.AddRange(tipler.ToArray());

        }
    }

    private void btn_aracSil_Click(object sender, EventArgs e)
    {
        if (secilenAracId.HasValue)
        {
            using (var context = new KtsContext())
            {
                var arac = context.Araclar.Find(secilenAracId.Value);
                if (arac != null)
                {
                    context.Araclar.Remove(arac);
                    context.SaveChanges();
                    VeriCek(dgv_araclar, ctx =>
                    ctx.Araclar
                    .Include(a => a.Sube)
                    .Select(a => new
                    {
                        a.AracId,
                        a.Plaka,
                        a.AracTip,
                        a.KapasiteKg,
                        a.GpsKodu,
                        a.Durum,
                        a.SubeId,
                        SubeAd = a.Sube != null ? a.Sube.SubeAd : ""
                    })
                    ); var tipler = context.Araclar
                .Select(a => a.AracTip)
                .Distinct()
                .ToList();
                    cb_aracFiltre.Items.Clear();
                    cb_aracFiltre.Items.AddRange(tipler.ToArray());

                }
                else
                {
                    MessageBox.Show("Silinecek araç bulunamadı.");
                }
            }
        }
        else
        {
            MessageBox.Show("Lütfen silmek için bir araç seçin.");
        }
    }

    private void btn_aracFormTemizle_Click(object sender, EventArgs e)
    {
        KontrolleriTemizle(tb_aracPlaka, cb_aracTip, nud_aracKapasite, tb_aracGps, cb_aracDurum, cb_aracSube, cb_aracFiltre);
    }

    private void btn_tempSifre_Click(object sender, EventArgs e)
    {
        Random rnd = new Random();
        string[] alfabe = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        string takipNumarasi = (alfabe[rnd.Next(0, 25)] + rnd.Next(1, 10) + alfabe[rnd.Next(0, 25)] + rnd.Next(1, 10)).ToString();
        tb_personelSifre.Text = takipNumarasi;
    }



    private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void groupBox2_Enter(object sender, EventArgs e)
    {

    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
    }

    private void groupBox1_Enter(object sender, EventArgs e)
    {

    }

    private void label2_Click(object sender, EventArgs e)
    {

    }

    private void label12_Click(object sender, EventArgs e)
    {

    }

    private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
    {

    }

    private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
    {

    }

    private void button2_Click(object sender, EventArgs e)
    {
        Random rnd = new Random();
        string[] alfabe = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        string takipNumarasi = (alfabe[rnd.Next(0, 25)] + rnd.Next(1, 10) + alfabe[rnd.Next(0, 25)] + rnd.Next(1, 10) + alfabe[rnd.Next(0, 25)] + rnd.Next(1, 10) + alfabe[rnd.Next(0, 25)] + rnd.Next(1, 10)).ToString();
        takipNoTb.Text = takipNumarasi;
    }

    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }

    private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
    {

    }

    private void groupBox12_Enter(object sender, EventArgs e)
    {

    }

    private void textBox22_TextChanged(object sender, EventArgs e)
    {

    }

    private void btnMusterAdresYonet_Click(object sender, EventArgs e)
    {
        AdresEkleForm form = new AdresEkleForm(9, "Personel");
        form.ShowDialog();
    }



    private void aliciAdresCb_SelectedIndexChanged_1(object sender, EventArgs e)
    {

    }

    private void button29_Click(object sender, EventArgs e)
    {

    }

    private void label85_Click(object sender, EventArgs e)
    {

    }
}
