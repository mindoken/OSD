using Alchemy.Inspector;
using UnityEngine;
using Zenject;

namespace App
{
    internal sealed class GameCycleDebug : MonoBehaviour
    {
        [Inject]
        private GameCycle gameCycle;

        [Button]
        private void StartGame()
        {
            this.gameCycle.StartGame();
        }

        [Button]
        private void PauseGame()
        {
            this.gameCycle.PauseGame();
        }

        [Button]
        private void ResumeGame()
        {
            this.gameCycle.ResumeGame();
        }

        [Button]
        private void FinishGame()
        {
            this.gameCycle.FinishGame();
        }
    }

}