using System.Collections;
using UnityEngine;

namespace TBT.Gameplay.TowerDefenseGameplay.Enemies
{
    public class TankRobot : EnemyClass
    {
        [SerializeField] private AnimationCurve attackAnimationCurve;
        [SerializeField] private AnimationCurve pullingAnimationCurve;
        private float attackAnimationDuration = 1f;
        private float pullingAnimationDuration = 1f;
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
                        elapsedTime =timer;
                        PullCarriage(gameObject);
                        PullCarriage(carriage.gameObject);
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

        private void PullCarriage(GameObject objPulled)
        {
            isDoingAction = true;
            StartCoroutine(PullingAnimation(objPulled));
        }
        
        IEnumerator PullingAnimation(GameObject objPulled)
        {
            float elapsedTime = 0;
            Vector3 basePos = objPulled.transform.position;
            while (elapsedTime < pullingAnimationDuration)
            {
                float addedPos = pullingAnimationCurve.Evaluate(elapsedTime);
                objPulled.transform.position = basePos+ Vector3.left * addedPos;
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            isDoingAction = false;
            damageCollider.enabled = false;
        }

        private void Move()
        {
            transform.position = Vector3.MoveTowards(transform.position, carriage.transform.position, Time.deltaTime * speed);
        }

        protected override void Dying()
        {
            base.Dying();
            Destroy(gameObject);
        }
    }
}
