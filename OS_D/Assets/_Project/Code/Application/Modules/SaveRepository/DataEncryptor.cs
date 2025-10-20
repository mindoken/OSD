using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Cysharp.Threading.Tasks;

namespace App
{
    public sealed class DataEncryptor
    {
        private static readonly byte[] EncryptionKey = new byte[] {
             0xA3, 0x1F, 0x8C, 0x72, 0xE9, 0x55, 0x4B, 0x2D,
             0xC6, 0x7A, 0x90, 0xDD, 0x34, 0x12, 0xAB, 0xEF,
             0x56, 0x21, 0x9C, 0x07, 0x78, 0x43, 0xFE, 0x1A,
             0x2B, 0x5D, 0x89, 0x00, 0xBC, 0x67, 0x3F, 0xD2
         };

        public static async UniTask<byte[]> EncryptStringAsync(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = EncryptionKey;
                aes.IV = new byte[16]; 

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        byte[] bytes = Encoding.UTF8.GetBytes(plainText);
                        await cs.WriteAsync(bytes, 0, bytes.Length);
                    }
                    return ms.ToArray();
                }
            }
        }

        public static async UniTask<string> DecryptStringAsync(byte[] cipherText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = EncryptionKey;
                aes.IV = new byte[16];

                using (MemoryStream ms = new MemoryStream(cipherText))
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                using (StreamReader sr = new StreamReader(cs))
                {
                    return await sr.ReadToEndAsync();
                }
            }
        }
    }
}