using Alchemy.Inspector;
using Cysharp.Threading.Tasks;
using Infrastructure;
using UnityEngine;
using Zenject;

namespace App
{
    public sealed class SaveLoadDebug : MonoBehaviour
    {
        [Inject]
        private ISaveLoadSystem _saveLoadSystem;

        [Button]
        public void SaveGame()
        {
            _saveLoadSystem.Save().Forget();
        }

        [Button]
        public void LoadGame()
        {
            _saveLoadSystem.Load();
        }
    }
}