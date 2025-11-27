using System;
using System.Linq;
using System.Windows.Forms;
using kargotakipsistemi.Entities;
using Microsoft.EntityFrameworkCore;

namespace kargotakipsistemi.Servisler
{
    public static class VeriBaglamaServisi
    {
        public static void IzgaraBagla<T>(DataGridView dgv, Func<KtsContext, IQueryable<T>> sorgu) where T : class
        {
            using (var context = new KtsContext())
            {
                var liste = sorgu(context).ToList();
                dgv.AutoGenerateColumns = true;
                dgv.DataSource = liste;
            }
        }

        public static void KomboyaBagla<T>(ComboBox cb, Func<KtsContext, IQueryable<T>> sorgu, string gorunenUye, string degerUye) where T : class
        {
            using (var context = new KtsContext())
            {
                cb.DataSource = sorgu(context).ToList();
                cb.DisplayMember = gorunenUye;
                cb.ValueMember = degerUye;
            }
        }
    }
}
