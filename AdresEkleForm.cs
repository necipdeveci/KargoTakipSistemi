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

namespace kargotakipsistemi;

public partial class AdresEkleForm : Form
{
    private int? _referansId;
    private string _referansTipi;

    public AdresEkleForm(int referansId, string referansTipi)
    {
        InitializeComponent();
        _referansId = referansId;
        _referansTipi = referansTipi;
    }


    private void VeriCek<T>(DataGridView dgv, Func<KtsContext, IQueryable<T>> sorgu) where T : class
    {
        using (var context = new KtsContext())
        {
            var liste = sorgu(context).ToList();
            dgv.AutoGenerateColumns = true;
            dgv.DataSource = liste;
        }
    }
    private void ComboBoxVeriCek<T>(ComboBox cb, Func<KtsContext, IQueryable<T>> sorgu, string displayMember, string valueMember) where T : class
    {
        using (var context = new KtsContext())
        {
            cb.DataSource = sorgu(context).ToList();
            cb.DisplayMember = displayMember;
            cb.ValueMember = valueMember;
        }
    }

    private void btn_adresKaydet_Click(object sender, EventArgs e)
    {
        using (var context = new KtsContext())
        {
            Adres adres = new Adres();
            if (_referansId.HasValue)
            {
                if (_referansTipi == "Personel")
                {
                    adres = context.Adresler.SingleOrDefault(a => a.PersonelId == _referansId);
                    if (adres == null)
                    {
                        MessageBox.Show("Personel bulunamadı.");
                        return;
                    }
                }
                else
                {
                    adres = context.Adresler.SingleOrDefault(a => a.MusteriId == _referansId);
                    if (adres == null)
                    {
                        MessageBox.Show("Müşteri bulunamadı.");
                        return;
                    }
                }
            }

            adres.AdresBaslik = tb_adresBaslik.Text;
            adres.AcikAdres = tb_adresAcikAdres.Text;
        }
    }
    private void btn_adresKayitSil_Click(object sender, EventArgs e)
    {

    }
    private void btn_adresFormTemizle_Click(object sender, EventArgs e)
    {

    }

    private void dgv_adresler_SelectionChanged(object sender, EventArgs e)
    {
        if (dgv_adresler.SelectedRows.Count > 0)
        {
            var seciliSatir = dgv_adresler.SelectedRows[0];
        }
    }

    private void AdresEkleForm_Load(object sender, EventArgs e)
    {
        cb_adresTip.Items.Clear();
        cb_adresTip.Items.Add("Ev");
        cb_adresTip.Items.Add("İş");
        cb_adresTip.Items.Add("Diğer");
        cb_adresTip.SelectedIndex = 0;
    }

    private void cb_adresIl_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cb_adresIl.SelectedValue != null)
        {
            int secilenIlId;
            if (cb_adresIl.SelectedValue is int)
                secilenIlId = (int)cb_adresIl.SelectedValue;
            else if (cb_adresIl.SelectedValue is Il il)
                secilenIlId = il.IlId;
            else
                throw new InvalidOperationException("Seçilen değer beklenen türde değil.");
            ComboBoxVeriCek(cb_adresIlce, ctx => ctx.Ilceler.Where(x => x.IlId == secilenIlId), "IlceAd", "IlceId");
        }
    }
}
