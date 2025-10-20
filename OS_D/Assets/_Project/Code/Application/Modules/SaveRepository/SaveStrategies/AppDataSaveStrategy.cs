using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace App
{
    public sealed class AppDataSaveStrategy : ISaveStrategy
    {
        private string FOLDER_PATH = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "HarvestGarden");
        private const string FILE_FORMAT = ".dat";
        private const int BufferSize = 65536;

        async UniTask ISaveStrategy.DeleteRepository(string KEY)
        {
            var fileName = KEY + FILE_FORMAT;
            var filePath = Path.Combine(FOLDER_PATH, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        async UniTask<Dictionary<string, string>> ISaveStrategy.LoadRepository(string KEY)
        {
            var fileName = KEY + FILE_FORMAT;
            var filePath = Path.Combine(FOLDER_PATH, fileName);

            if (!File.Exists(filePath))
            {
                return new();
            }

            byte[] loadedData;
            using (FileStream fs = new FileStream(
                filePath,
                FileMode.Open,
                FileAccess.Read,
                FileShare.Read,
                bufferSize: BufferSize,
                FileOptions.Asynchronous | FileOptions.SequentialScan))
            {
                loadedData = new byte[fs.Length];
                int bytesRead = 0;
                int totalBytes = (int)fs.Length;

                while (bytesRead < totalBytes)
                {
                    int read = await fs.ReadAsync(loadedData, bytesRead, totalBytes - bytesRead);
                    if (read == 0)
                        throw new IOException("Unexpected end of file");
                    bytesRead += read;
                }
            }

            string jsonData = await DataEncryptor.DecryptStringAsync(loadedData);

            return JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonData);
        }

        async UniTask ISaveStrategy.SaveRepository(Dictionary<string, string> repository, string KEY)
        {
            var jsonData = JsonConvert.SerializeObject(repository);

            if (!Directory.Exists(FOLDER_PATH))
            {
                Directory.CreateDirectory(FOLDER_PATH);
            }

            var fileName = KEY + FILE_FORMAT;
            var filePath = Path.Combine(FOLDER_PATH, fileName);

            byte[] encryptedData = await DataEncryptor.EncryptStringAsync(jsonData);

            using (FileStream fs = new FileStream(
                filePath,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None,
                bufferSize: BufferSize,
                FileOptions.Asynchronous | FileOptions.WriteThrough))
            {
                await fs.WriteAsync(encryptedData, 0, encryptedData.Length);
            }
        }
    }
}