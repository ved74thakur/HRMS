using System.Security.Cryptography;
using System.Text;

namespace leaveApplication2.Other
{

  

    public  class EncryptionHelper
    {
        // Change this key to a secure key for your application
        private static readonly string Key = "0123456789abcdef0123456789abcdef";

        public static string Encrypt(string input)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(Key);
                aesAlg.IV = new byte[16]; // Use zero IV (Initialization Vector) for simplicity

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(input);
                        }
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray())
                        .Replace('+', '-')
                        .Replace('/', '_')
                        .TrimEnd('=');
                }
            }
        }

        public static string Decrypt(string input)
        {
            // Convert the URL-safe Base64 to standard Base64
            input = input.Replace('-', '+').Replace('_', '/');

            // Add padding if needed
            int padding = (4 - input.Length % 4) % 4;
            input = input.PadRight(input.Length + padding, '=');

            byte[] cipherText = Convert.FromBase64String(input);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(Key);
                aesAlg.IV = new byte[16]; // Use zero IV (Initialization Vector) for simplicity

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }


    //public class EncryptionHelper
    //{
    //    private static byte[] key = Encoding.UTF8.GetBytes("0123456789abcdef0123456789abcdef");
    //public static string Encrypt(string plaintext)
    //    {
    //        using (Aes aes = Aes.Create())
    //        {
    //            aes.Key = key;
    //            byte[] encryptedBytes = PerformCryptography(aes, Encoding.UTF8.GetBytes(plaintext), aes.CreateEncryptor());
    //            return ToUrlSafeBase64String(encryptedBytes);
    //        }
    //    }
    //    public static string Decrypt(string ciphertext)
    //    {
    //        using (Aes aes = Aes.Create())
    //        {
    //            aes.Key = key;
    //            byte[] decryptedBytes = PerformCryptography(aes, FromUrlSafeBase64String(ciphertext), aes.CreateDecryptor());
    //            return Encoding.UTF8.GetString(decryptedBytes);
    //        }
    //    }
    //    private static byte[] PerformCryptography(SymmetricAlgorithm algorithm, byte[] inputBytes, ICryptoTransform cryptoTransform)
    //    {
    //        using (MemoryStream memoryStream = new MemoryStream())
    //        {
    //            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
    //            {
    //                cryptoStream.Write(inputBytes, 0, inputBytes.Length);
    //                cryptoStream.FlushFinalBlock();
    //                return memoryStream.ToArray();
    //            }
    //        }
    //    }
    //    private static string ToUrlSafeBase64String(byte[] bytes)
    //    {
    //        string base64 = Convert.ToBase64String(bytes);
    //        return base64.Replace('+', '-').Replace('/', '_').TrimEnd('=');
    //    }
    //    private static byte[] FromUrlSafeBase64String(string base64)
    //    {
    //        base64 = base64.Replace('-', '+').Replace('_', '/');
    //        switch (base64.Length % 4)
    //        {
    //            case 2:
    //                base64 += "=="; break;
    //            case 3:
    //                base64 += "="; break;
    //        }
    //        return Convert.FromBase64String(base64);
    //    }
    //}
}
