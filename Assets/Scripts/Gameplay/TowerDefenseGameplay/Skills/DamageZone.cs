using System;
using TBT.Gameplay.TowerDefenseGameplay.Enemies;
using UnityEngine;

namespace TBT.Gameplay.TowerDefenseGameplay.Skills
{
    public class DamageZone : MonoBehaviour
    {
        public Collider2D damageCollider;
        private float damage;

        public void SetDamage(float newDamage)
        {
            this.damage = newDamage;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<EnemyInterface>()!= null)
            {
                EnemyClass enemy = other.gameObject.GetComponent<EnemyInterface>().enemyComponent;
                enemy.TakeDamage(damage);
            }
        }
    }
}