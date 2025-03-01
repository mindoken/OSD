
using Game.Enemy.AI;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Enemy
{
    public class EnemyBase : Actor, IGameFixedTickable
    {
        private Rigidbody2D rb;

        [Header("ENEMY SETTINGS")]
        [SerializeField]
        public float maxSpeed = 1f;

        [SerializeField]
        public float detectRange = 10f;

        [SerializeField]
        public float huntingRange = 20f;

        [Header("DEBUG")]
        [InspectorReadOnly]
        public List<EnemyState> enemyState;

        [HideInInspector]
        public bool detect = false;

        [HideInInspector]
        public Vector2 velocity = Vector2.zero;

        private void Awake()
        {
            this.rb = GetComponent<Rigidbody2D>();
        }

        public virtual void FixedTick(float deltaTime)
        {
            Vector2 position = this.transform.position;
            this.rb.MovePosition(position + this.velocity * deltaTime);
        }



    }
}
 
