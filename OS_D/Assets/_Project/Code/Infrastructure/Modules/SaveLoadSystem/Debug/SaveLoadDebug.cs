using Alchemy.Inspector;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace App
{
    public sealed class SaveLoadDebug : MonoBehaviour
    {
        [Inject]
        private SaveLoadSystem _saveLoadSystem;

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