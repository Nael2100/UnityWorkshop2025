using System;
using System.Collections;
using System.Numerics;
using TBT.Core.Data.AudioData;
using TBT.Core.Data.EnemyData;
using TBT.Gameplay.Audio;
using UnityEngine;
using UnityEngine.Serialization;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace TBT.Gameplay.TowerDefenseGameplay.Enemies
{
    public class EnemyClass : MonoBehaviour
    {
        protected float damage;
        protected float maxHealth;
        protected float currentHealth;
        protected float speed;
        protected float range;
        public bool enemyIsActive; //{ get; protected set; }
        protected Carriage carriage;
        protected bool isDoingAction ;
        [SerializeField] protected EnemyDataScript data;
        [SerializeField] protected BoxCollider2D damageCollider;
        [SerializeField] private SpriteRenderer damageRenderer;
        [SerializeField] protected GameObject dyingParticles;
        [SerializeField] private AnimationCurve damageAnimationCurve;
        private float damageAnimationCurveDuration = 0.5f;

        public event Action<EnemyClass> OnDying; 
        public event Action<float, float> OnHealthChanged;
        public event Action OnTurnStarted;
        public event Action OnTurnEnded;

        private void OnEnable()
        {
            SetUpData();
            damageCollider.enabled = false;
        }

        public virtual void Act(float timer)
        {
            RotateTowardsCarriage();
            enemyIsActive = true;
            OnTurnStarted?.Invoke();
        }

        private void SetUpData()
        {
            isDoingAction = false;
            damage = data.damage;
            maxHealth = data.health;
            currentHealth = maxHealth;
            speed = data.speed;
            range = data.range;
        }

        public void SetCarriage(Carriage carriage)
        {
            this.carriage = carriage;
            RotateTowardsCarriage();
        }

        public virtual void TakeDamage(float damage)
        {
            currentHealth -= damage;
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
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
            if (currentHealth <= 0)
            {
                Dying();
            }
        }

        protected virtual void Dying()
        {
            AudioManager.Instance.PlaySound(AudioName.explosion);
            GameObject particles = Instantiate(dyingParticles, transform.position, Quaternion.identity);
            particles.GetComponent<ParticleSystem>().Play();
            enemyIsActive = false;
            OnDying?.Invoke(this);
        }

        public virtual void EnableCollider()
        {
            damageCollider.enabled = true;
        }

        public virtual void DisableCollider()
        {
            damageCollider.enabled = false;
        }

        private void RotateTowardsCarriage()
        {
            Vector3 direction = carriage.gameObject.transform.position - transform.position;
            direction.z = 0f;
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = rotation;
        }

        public void AddSpeed(float addedSpeed)
        {
            speed = data.speed + addedSpeed;
        }

        protected void EndTurn()
        {
            enemyIsActive = false;
            OnTurnEnded?.Invoke();
        }
    }
}