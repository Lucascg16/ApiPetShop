using System.Security.Cryptography;
using System.Text;

namespace ApiPetShop.Infra
{
    public class CryptoService : ICryptoService
    {
        private readonly string Key = "VhObgvki4PEn8u5J";
        private readonly string IV = "IgEwwq7sW9bhZrza";

        public string Encrypt(string input)
        {
            using Aes aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(Key);
            aes.IV = Encoding.UTF8.GetBytes(IV);

            using MemoryStream ms = new();
            using CryptoStream cs = new(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);

            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            cs.Write(inputBytes, 0, inputBytes.Length);
            cs.FlushFinalBlock();

            return Convert.ToBase64String(ms.ToArray());
        }

        public string Decrypt(string input)
        {
            try
            {
                using Aes aes = Aes.Create();
                aes.Key = Encoding.UTF8.GetBytes(Key);
                aes.IV = Encoding.UTF8.GetBytes(IV);

                using MemoryStream ms = new();
                using CryptoStream cs = new(ms, aes.CreateDecryptor(), CryptoStreamMode.Write);

                byte[] inputBytes = Convert.FromBase64String(input);
                cs.Write(inputBytes, 0, inputBytes.Length);
                cs.FlushFinalBlock();

                return Encoding.UTF8.GetString(ms.ToArray());
            }
            catch
            {
                return input;
            }
        }
    }
}
