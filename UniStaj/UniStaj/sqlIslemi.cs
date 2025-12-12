using Microsoft.EntityFrameworkCore; // 

namespace UniStaj
{
    public class SqlIslemi
    {

        /// <summary>
        /// Bütün resmi tatil süresi için resmi tarih belirler.
        /// </summary>
        /// <param name="vari"></param>
        /// <returns></returns>
        public static async Task resmiTatileGoreYoklamaBelirle(veri.Varlik vari)
        {
            string sorgu = "exec dbo.resmiTatileGoreYoklamaBelirle";
            await islemYapKos(vari, sorgu);

        }

        /// <summary>
        /// İlgili kullanıcın son gün girişini belirler. Üst sınırı aşarsa kullanıcıya bilgi verir.
        /// </summary>
        /// <param name="vari"></param>
        /// <param name="kullaniciKimlik"></param>
        /// <returns></returns>
        public static async Task sonGunGirisiBelirle(veri.Varlik vari, int kullaniciKimlik)
        {
            string sorgu = String.Format("update Kullanici set sonGunGirisi = (select count(*) from Kayit  where i_kullaniciKimlik = kullaniciKimlik and cizelgeAdi ='Giriş' and convert(date,tarih) = convert(date, getdate()))  where kullanicikimlik = {0}", kullaniciKimlik);
            await islemYapKos(vari, sorgu);
        }
        public static async Task islemYapKos(veri.Varlik vari, string sql)
        {
            await vari.Database.ExecuteSqlRawAsync(sql);
        }
    }


}
