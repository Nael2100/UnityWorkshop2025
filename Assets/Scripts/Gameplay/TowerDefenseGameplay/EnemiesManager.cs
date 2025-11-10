using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using TBT.Core;
using TBT.Core.Data.TowerDefenseData;
using TBT.Gameplay.TowerDefenseGameplay.Enemies;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace TBT.Gameplay.TowerDefenseGameplay
{
    public class EnemiesManager : MonoBehaviour
    {
        private TowerDefenseData data;
        [SerializeField] private EnemyClass[] possibleEnemies;
        [SerializeField] private GameObject spawningPoint;
        [SerializeField] private Carriage carriage;
        public List<EnemyClass> enemies;
        private float timer = 0.5f;
        public event Action EnemiesTurnEnded;
        
        public void SetEnemiesMoving()
        {
            EnemyClass currentActingEnemy = enemies[0];
            currentActingEnemy.Act(timer);
            StartCoroutine(WaitForTurn(currentActingEnemy));
        }

        void Start()
        {
            enemies = new List<EnemyClass>();
        }

        public void ResetEnemies()
        {
            foreach (EnemyClass enemy in enemies)
            {
                Destroy(enemy.gameObject);
            }
            enemies.Clear();
        }
        IEnumerator WaitForTurn(EnemyClass enemy)
        {
            while (enemy.enemyIsActive)
            {
                yield return null;
            }
            enemies.Add(enemies[0]);
            enemies.RemoveAt(0);
            EnemiesTurnEnded?.Invoke();
        }

        public void SpawnEnemies(int round, int wave)
        {
            int min = data.minEnnemies + (data.addedEnnemiesByWaves * wave) + data.addedEnnemiesByRounds;
            int max = data.maxEnnemies + (data.addedEnnemiesByWaves * wave) + data.addedEnnemiesByRounds;
            for (int i = 0; i < Random.Range(min,max+1 ); i++)
            {
                int chosenIndex = Random.Range(0, possibleEnemies.Length);
                float offset = 5f;
                Vector3 spawnPosition = new Vector3(spawningPoint.transform.position.x - Random.Range(0,offset), spawningPoint.transform.position.y+ Random.Range(-offset, offset),0);
                GameObject newEnemy  = Instantiate(possibleEnemies[chosenIndex].gameObject, spawnPosition, Quaternion.identity);
                enemies.Add(newEnemy.GetComponent<EnemyInterface>().enemyComponent);
                
            }

            foreach (EnemyClass enemy in enemies)
            {
                enemy.SetCarriage(carriage);
            }
        }

        public bool AllEnemiesDead()
        {
            return enemies.Count == 0;
        }

        public void SetUp(TowerDefenseData newData)
        {
            data = newData;
        }

        public void AllEnemiesActivateCollider()
        {
            foreach (EnemyClass enemy in enemies)
            {
                enemy.EnableCollider();
            }
        }

        public void AllEnemiesDeactivateCollider()
        {
            foreach (EnemyClass enemy in enemies)
            {
                enemy.DisableCollider();
            }
        }
    }
}