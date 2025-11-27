using System;

namespace kargotakipsistemi.Yardimcilar
{
    public static class SifreUretici
    {
        private static readonly string[] Alfabe =
        { "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z" };

        public static string BasitKodUret(int blokSayisi = 4)
        {
            var rnd = new Random();
            var parcalar = new string[blokSayisi];
            for (int i = 0; i < blokSayisi; i++)
            {
                parcalar[i] = Alfabe[rnd.Next(0, Alfabe.Length)] + rnd.Next(1, 10);
            }
            return string.Join(string.Empty, parcalar);
        }
    }
}
