using System.Security.Cryptography;
using System.Text;
using QRCoder;

namespace UniStaj.Models
{
    public class KareKodModel : ModelTabani
    {
        public KareKodModel()
        {
            qrCodeImage = new byte[0];
        }
        public byte[] qrCodeImage { get; set; }

        public static string sifreCoz(string cipherText, string key)
        {
            var fullCipher = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);

                // IV'yi baştan ayır
                var iv = new byte[aes.BlockSize / 8];
                Array.Copy(fullCipher, iv, iv.Length);

                using (var decryptor = aes.CreateDecryptor(aes.Key, iv))
                using (var ms = new MemoryStream(fullCipher, iv.Length, fullCipher.Length - iv.Length))
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (var sr = new StreamReader(cs))
                {
                    return sr.ReadToEnd(); // Deşifrelenmiş metni döndür
                }
            }
        }
        public static string sifrele(string plainText, string key)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.GenerateIV(); // Rastgele bir IV oluştur

                // Şifreleme
                using (var ms = new MemoryStream())
                {
                    ms.Write(aes.IV, 0, aes.IV.Length); // IV'yi başa ekle

                    using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    using (var sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText);
                    }

                    return Convert.ToBase64String(ms.ToArray()); // Şifrelenmiş metni Base64 formatında döndür
                }
            }
        }


        public void veriCekKosut(Yonetici kime)
        {
            string numara = "";


            numara = "0000000000000000000" + numara;
            numara = numara.Substring(numara.Length - 11, 11);
            Random rg = new Random();
            string bas = rg.Next(11111, 99999).ToString();
            string son = rg.Next(11111, 99999).ToString();

            int gun = DateTime.Today.Day;
            int ay = DateTime.Today.Month;


            string ifade1 = "";
            if (gun < 10)
                ifade1 = "0" + gun.ToString();
            else
                ifade1 = gun.ToString();


            if (ay < 10)
                ifade1 += "0" + ay.ToString();
            else
                ifade1 += ay.ToString();

            ifade1 += DateTime.Today.Year.ToString();
            ifade1 = bas + ifade1 + numara + son;
            ifade1 = sifrele(ifade1, "BurdurTekeYoresi");

            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(ifade1, QRCodeGenerator.ECCLevel.Q))
            using (PngByteQRCode qrCode = new PngByteQRCode(qrCodeData))
            {
                qrCodeImage = qrCode.GetGraphic(20);
            }

        }
    }
}
