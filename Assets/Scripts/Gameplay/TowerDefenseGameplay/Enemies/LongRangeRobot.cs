using System;
using System.Collections;
using UnityEngine;

namespace TBT.Gameplay.TowerDefenseGameplay.Enemies
{
    public class LongRangeRobot : EnemyClass
    {
        [SerializeField] private AnimationCurve attackAnimationCurve;
        [SerializeField] private LongRangeRobotProjectile projectile;
        private float attackAnimationDuration = 0.3f;
        private bool projectileLaunched;
        private bool isExploding;
        public override void Act(float timer)
        {
            base.Act(timer);
            StartCoroutine(Action(timer));
        }

        IEnumerator Action(float timer)
        {
            float elapsedTime = 0;
            while (elapsedTime < timer)
            {
                if (!isDoingAction)
                {
                    if (Vector2.Distance(gameObject.transform.position, carriage.gameObject.transform.position) < range)
                    {
                        if (!projectileLaunched)
                        {
                            Attack();
                        }
                    }
                    else
                    {
                        Move();
                    }
                }
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            while (isDoingAction)
            {
                yield return null;
            }
            enemyIsActive = false;
        }

        private void Attack()
        {
            isDoingAction = true;
            damageCollider.enabled = true;
            StartCoroutine(AttackAnimation());
        }

        protected override void Dying()
        {
            base.Dying();
            StartCoroutine(DyingExplosion());
        }

        IEnumerator DyingExplosion()
        {
            damageCollider.size *= 3;
            isExploding = true;
            yield return null;
            isExploding = false;
            Destroy(gameObject);
        }
        IEnumerator AttackAnimation()
        {
            float elapsedTime = 0;
            Vector3 basePos = gameObject.transform.position;
            while (elapsedTime < attackAnimationDuration)
            {
                float addedPos = attackAnimationCurve.Evaluate(elapsedTime);
                transform.position = basePos+ Vector3.right * addedPos;
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            LaunchProjectile();
            transform.position = basePos;
            isDoingAction = false;
            damageCollider.enabled = false;
        }

        private void LaunchProjectile()
        {
            projectileLaunched = true;
            GameObject projectile = gameObject.transform.GetChild(0).gameObject;
            projectile.SetActive(true);
            projectile.transform.position = gameObject.transform.position;
        }
        private void Move()
        {
            transform.position = Vector3.MoveTowards(transform.position, carriage.transform.position, Time.deltaTime * speed);
        }
        public Carriage ReturnCarriage()
        {
            return carriage;
        }

        public void ProjectileHit()
        {
            carriage.TakeDamage(damage);
        }

        public void ProjectileExploded()
        {
            projectileLaunched = false;
        }

        public override void EnableCollider()
        {
            base.EnableCollider();
            projectile.GetComponent<BoxCollider2D>().enabled = true;
        }

        public override void DisableCollider()
        {
            base.DisableCollider();
            if (projectile.isActiveAndEnabled)
            {
                projectile.GetComponent<BoxCollider2D>().enabled = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (isExploding && other.gameObject.GetComponent<EnemyInterface>() != null)
            {
                other.gameObject.GetComponent<EnemyInterface>().enemyComponent.TakeDamage(damage);
            }
        }
        
    }
    
    
}
