using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace TBT.Gameplay.TowerDefenseGameplay.Enemies
{
    public class LongRangeRobotProjectile : EnemyClass
    {
        [SerializeField] private LongRangeRobot robotParent;

        private void Start()
        {
            Explode();
        }

        private void OnEnable()
        {
            carriage = robotParent.ReturnCarriage();
        }

        private void Update()
        {
            if (robotParent.enemyIsActive)
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, carriage.transform.position, Time.deltaTime * data.speed);
            }
        }

        private void Explode()
        {
            robotParent.ProjectileExploded();
            GameObject particles = Instantiate(dyingParticles, transform.position, Quaternion.identity);
            particles.GetComponent<ParticleSystem>().Play();
            gameObject.SetActive(false);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<Carriage>() != null)
            {
                robotParent.ProjectileHit();
                Explode();
            }
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            Explode();
        }
    }
}