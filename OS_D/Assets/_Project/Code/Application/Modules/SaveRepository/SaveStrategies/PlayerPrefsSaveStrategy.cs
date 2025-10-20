using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

namespace App
{
    public sealed class PlayerPrefsSaveStrategy : ISaveStrategy
    {
        async UniTask<Dictionary<string, string>> ISaveStrategy.LoadRepository(string KEY)
        {
            if (PlayerPrefs.HasKey(KEY))
            {
                await UniTask.Delay(1);
                var jsonData = PlayerPrefs.GetString(KEY);
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonData);
            }
            else
            {
                return new();
            }
        }

        async UniTask ISaveStrategy.SaveRepository(Dictionary<string, string> gameState, string KEY)
        {
            var jsonData = JsonConvert.SerializeObject(gameState);
            PlayerPrefs.SetString(KEY, jsonData);
            await UniTask.Delay(1);
        }

        async UniTask ISaveStrategy.DeleteRepository(string KEY)
        {
            await UniTask.Delay(1);
            PlayerPrefs.DeleteKey(KEY);
        }
    }
}