using System;
using System.Collections;
using System.Numerics;
using TBT.Core.Data.EnemyData;
using UnityEngine;

namespace TBT.Gameplay.TowerDefenseGameplay.Enemies
{
    public class EnemyClass : MonoBehaviour
    {
        protected float damage;
        protected float health;
        protected float speed;
        protected float range;
        public bool enemyIsActive { get; protected set; }
        protected Carriage carriage;
        protected bool isDoingAction ;
        [SerializeField] protected EnemyDataScript data;
        [SerializeField] protected BoxCollider2D boxCollider;

        private void OnEnable()
        {
            SetUpData();
            boxCollider.enabled = false;
        }

        public virtual void Act(float timer)
        {
            enemyIsActive = true;
        }

        private void SetUpData()
        {
            isDoingAction = false;
            damage = data.damage;
            health = data.health;
            speed = data.speed;
            range = data.range;
        }

        public void SetCarriage(Carriage carriage)
        {
            this.carriage = carriage;
        }

        public virtual void TakeDamage(float damage)
        {
            health -= damage;
            CheckStillAlive();
        }

        private void CheckStillAlive()
        {
            if (health <= 0)
            {
                Dying();
            }
        }

        protected virtual void Dying()
        {
            enemyIsActive = false;
        }
    }
}