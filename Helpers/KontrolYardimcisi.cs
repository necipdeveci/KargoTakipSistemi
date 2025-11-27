using System;
using System.Windows.Forms;

namespace kargotakipsistemi.Yardimcilar
{
    public static class KontrolYardimcisi
    {
        public static void Temizle(params Control[] kontroller)
        {
            foreach (var control in kontroller)
            {
                switch (control)
                {
                    case TextBox tb:
                        tb.Clear();
                        break;
                    case ComboBox cb:
                        cb.SelectedIndex = -1;
                        break;
                    case NumericUpDown nud:
                        nud.Value = nud.Minimum;
                        break;
                    case CheckBox ckb:
                        ckb.Checked = false;
                        break;
                    case DateTimePicker dtp:
                        dtp.Value = DateTime.Now;
                        break;
                }
            }
        }
    }
}
