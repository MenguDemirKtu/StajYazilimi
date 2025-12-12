using Microsoft.EntityFrameworkCore; // 
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using UniStaj.veri;

namespace UniStaj.GenelIslemler
{
    public class GuvenlikIslemi
    {
        /// <summary>
        /// Çift yönlü doğrulama işlemi için
        /// </summary>
        /// <param name="kiminIcin"></param>
        /// <param name="smsAtilsinmi"></param>
        /// <param name="epostaAtilsinmi"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task dogrulamaKoduGonder(KullaniciAYRINTI kiminIcin, bool smsAtilsinmi, bool epostaAtilsinmi)
        {
            using (veri.Varlik vari = new veri.Varlik())
            {
                Kullanici? kul = await vari.Kullanicis.FirstOrDefaultAsync(p => p.kullaniciKimlik == kiminIcin.kullaniciKimlik);
                if (kul == null)
                    throw new Exception("Kullanıcı bilgisi bulunamadı");

                Random rg = new Random();
                string kod = rg.Next(100000, 900000).ToString();
                kul.ciftOnayKodu = kod;
                await veriTabani.KullaniciCizelgesi.kaydetKos(kul, vari, false);
                if (smsAtilsinmi)
                    await SMSIslemi.dogrulamaKodu(vari, kul);
                if (epostaAtilsinmi)
                    await EPostaIslemi.dogrulamaKodu(vari, kul);



            }
        }
        /// <summary>
        /// Parolanın geçerli olup olmöadığını belirler
        /// </summary>
        /// <param name="parola"></param>
        /// <returns></returns>
        public static bool parolaGecerliMi(string parola, out string gerekce)
        {
            gerekce = "";

            if (parola.Length < 8 || parola.Length > 24)
            {
                gerekce = "Parola en az 8 en fazla 24 karakter olmalıdır";
                return false;
            }

            if (!Regex.IsMatch(parola, @"[A-Z]"))
            {
                gerekce = "En az bir büyük harf içermelidir";
                return false;
            }

            if (!Regex.IsMatch(parola, @"[a-z]"))
            {
                gerekce = "En az bir küçük harf içerlemlidir";
                return false;
            }

            if (!Regex.IsMatch(parola, @"\d"))
            {
                gerekce = "En az bir rakam içermelidir";
                return false;
            }

            if (!Regex.IsMatch(parola, @"[\W_]"))
            {
                gerekce = "En az bir özel karakter içermelidir";
                return false;
            }
            return true;
        }

        public const string anahtar = "yaylayollarindayuruyupgelirallis";
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("9684v10ie9bvpj08"); // 16 byte = 128 bit
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("kev6hvnhdv5u7iud");  // 16 byte = 128 bit


        /// <summary>
        /// Anahtar üretimi işlemi
        /// </summary>
        /// <returns></returns>
        public static string anahtarUret()
        {
            Random rg = new Random();
            string tamami = "abcdefghijklmnoprstuvyz0123456789";
            string sonuc = "";
            for (int i = 0; i < 16; i++)
            {
                int deger = rg.Next(0, tamami.Length);
                sonuc += tamami[deger].ToString();
            }
            return sonuc;
        }

        public static string sifrele(string plainText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using MemoryStream msEncrypt = new MemoryStream();
                using CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(plainText);
                }

                return Convert.ToBase64String(msEncrypt.ToArray());
            }
        }

        public static string sifreCoz(string cipherText)
        {
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using MemoryStream msDecrypt = new MemoryStream(buffer);
                using CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                using StreamReader srDecrypt = new StreamReader(csDecrypt);

                return srDecrypt.ReadToEnd();
            }
        }
    }
}
