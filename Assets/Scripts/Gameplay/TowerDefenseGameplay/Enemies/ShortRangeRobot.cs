using System;
using System.Collections;
using UnityEngine;

namespace TBT.Gameplay.TowerDefenseGameplay.Enemies
{
    public class ShortRangeRobot : EnemyClass
    {
        [SerializeField] private AnimationCurve attackAnimationCurve;
        private float attackAnimationDuration = 1f;
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
                        Attack();
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
            boxCollider.enabled = true;
            StartCoroutine(AttackAnimation());
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

            transform.position = basePos;
            isDoingAction = false;
            boxCollider.enabled = false;
        }

        private void Move()
        {
            transform.position = Vector3.MoveTowards(transform.position, carriage.transform.position, Time.deltaTime * speed);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<Carriage>() != null)
            {
                carriage.TakeDamage(damage);
            }
        }
    }
    
    
}