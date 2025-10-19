using System;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine.UIElements;
using Zenject;

namespace App
{
    public sealed class GameTickableManager
	{
        [Inject(Optional = true, Source = InjectSources.Local)]
        private List<IGameTickable> tickables = new();

        [Inject(Optional = true, Source = InjectSources.Local)]
        private List<IGameFixedTickable> fixedTickables = new();

        [Inject(Optional = true, Source = InjectSources.Local)]
        private List<IGameLateTickable> lateTickables = new();

        private readonly Queue<IGameTickable> queueTickableAdd = new();
        private readonly Queue<IGameTickable> queueTickableRemove = new();
        private readonly Queue<IGameFixedTickable> queueFixedTickableAdd = new();
        private readonly Queue<IGameFixedTickable> queueFixedTickableRemove = new();
        private readonly Queue<IGameLateTickable> queueLateTickableAdd = new();
        private readonly Queue<IGameLateTickable> queueLateTickableRemove = new();

        public void Add(IGameTickable tickable)
        {
            this.queueTickableAdd.Enqueue(tickable);
        }

        public void Remove(IGameTickable tickable)
        {
            this.queueTickableRemove.Enqueue(tickable);
        }

        public void AddFixed(IGameFixedTickable fixedTickable)
        {
            this.queueFixedTickableAdd.Enqueue(fixedTickable);
        }

        public void RemoveFixed(IGameFixedTickable fixedTickable)
        {
            this.queueFixedTickableRemove.Enqueue(fixedTickable);
        }

        public void AddLate(IGameLateTickable lateTickable)
        {
            this.queueLateTickableAdd.Enqueue(lateTickable);
        }

        public void RemoveLate(IGameLateTickable lateTickable)
        {
            this.queueLateTickableRemove.Enqueue(lateTickable);
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var tickable in this.tickables)
            {
                tickable.Tick(deltaTime);
            }
        }

        public void OnFixedUpdate(float deltaTime)
        {
            foreach (var fixedTickable in this.fixedTickables)
            {
                fixedTickable.FixedTick(deltaTime);
            }

            this.UpdateTickableManager();
        }

        public void OnLateUpdate(float deltaTime)
        {
            foreach (var lateTickable in this.lateTickables)
            {
                lateTickable.LateTick(deltaTime);
            }
        }

        private void UpdateTickableManager()
        {
            while (this.queueTickableAdd.Count > 0)
            {
                IGameTickable tickable = this.queueTickableAdd.Dequeue();
                this.tickables.Add(tickable);
            }
            while (this.queueTickableRemove.Count > 0)
            {
                IGameTickable tickable = this.queueTickableRemove.Dequeue();
                this.tickables.Remove(tickable);
            }

            while (this.queueFixedTickableAdd.Count > 0)
            {
                IGameFixedTickable tickable = this.queueFixedTickableAdd.Dequeue();
                this.fixedTickables.Add(tickable);
            }
            while (this.queueFixedTickableRemove.Count > 0)
            {
                IGameFixedTickable tickable = this.queueFixedTickableRemove.Dequeue();
                this.fixedTickables.Remove(tickable);
            }

            while (this.queueLateTickableAdd.Count > 0)
            {
                IGameLateTickable tickable = this.queueLateTickableAdd.Dequeue();
                this.lateTickables.Add(tickable);
            }
            while (this.queueLateTickableRemove.Count > 0)
            {
                IGameLateTickable tickable = this.queueLateTickableRemove.Dequeue();
                this.lateTickables.Remove(tickable);
            }
        }
    }
}