using System;
using System.Collections;
using System.Numerics;
using TBT.Core.Data.EnemyData;
using UnityEngine;
using UnityEngine.Serialization;
using Vector3 = UnityEngine.Vector3;

namespace TBT.Gameplay.TowerDefenseGameplay.Enemies
{
    public class EnemyClass : MonoBehaviour
    {
        protected float damage;
        protected float health;
        protected float speed;
        protected float range;
        public bool enemyIsActive; //{ get; protected set; }
        protected Carriage carriage;
        protected bool isDoingAction ;
        [SerializeField] protected EnemyDataScript data;
        [SerializeField] protected BoxCollider2D damageCollider;
        [SerializeField] private SpriteRenderer damageRenderer;
        [SerializeField] private AnimationCurve damageAnimationCurve;
        private float damageAnimationCurveDuration = 0.5f;

        private void OnEnable()
        {
            SetUpData();
            damageCollider.enabled = false;
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
            StartCoroutine(TakeDamageAnimation());
            CheckStillAlive();
        }
        
        IEnumerator TakeDamageAnimation()
        {
            damageRenderer.color = Color.red;
            float elapsedTime = 0;
            Vector3 basePos = gameObject.transform.position;
            while (elapsedTime < damageAnimationCurveDuration)
            {
                float addedPos = damageAnimationCurve.Evaluate(elapsedTime);
                transform.position = basePos+ Vector3.left * addedPos;
                damageCollider.offset = Vector3.right * addedPos;
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            transform.position = basePos;
            damageRenderer.color = Color.white;
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

        public virtual void EnableCollider()
        {
            damageCollider.enabled = true;
        }

        public virtual void DisableCollider()
        {
            damageCollider.enabled = false;
        }
    }
}