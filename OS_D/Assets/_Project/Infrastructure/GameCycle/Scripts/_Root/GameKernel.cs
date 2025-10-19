using Zenject;
using System.Collections.Generic;
using UnityEngine;

namespace App
{
    public sealed class GameKernel: MonoKernel,
        IGameStartListener,
        IGameResumeListener,
        IGamePauseListener,
        IGameFinishListener
    {
        [Inject]
        private GameCycle gameCycle;

        [InjectLocal]
        private GameTickableManager gameTickableManager;

        [Inject(Optional = true, Source = InjectSources.Local)]
        private List<IGameListener> listeners = new();

        private void OnEnable()
        {
            this.gameCycle.AddListener(this);
        }

        private void OnDisable()
        {
            this.gameCycle.RemoveListener(this);
        }

        void IGameStartListener.OnGameStart()
        {
            foreach (var it in listeners)
            {
                if (it is IGameStartListener listener)
                {
                    listener.OnGameStart();
                }
            }
        }

        void IGameFinishListener.OnGameFinish()
        {
            foreach (var it in listeners)
            {
                if (it is IGameFinishListener listener)
                {
                    listener.OnGameFinish();
                }
            }
        }

        void IGamePauseListener.OnGamePause()
        {
            foreach (var it in listeners)
            {
                if (it is IGamePauseListener listener)
                {
                    listener.OnGamePause();
                }
            }
        }

        void IGameResumeListener.OnGameResume()
        {
            foreach (var it in listeners)
            {
                if (it is IGameResumeListener listener)
                {
                    listener.OnGameResume();
                }
            }
        }

        public override void Update()
        {
            base.Update();

            if (this.gameCycle.MainState == GameState.PLAY)
            {
                float deltaTime = Time.deltaTime;
                this.gameTickableManager.OnUpdate(deltaTime);
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (this.gameCycle.MainState == GameState.PLAY)
            {
                float deltaTime = Time.fixedDeltaTime;
                this.gameTickableManager.OnFixedUpdate(deltaTime);
            }
        }

        public override void LateUpdate()
        {
            base.LateUpdate();

            if (this.gameCycle.MainState == GameState.PLAY)
            {
                float deltaTime = Time.deltaTime;
                this.gameTickableManager.OnLateUpdate(deltaTime);
            }
        }
    }
}