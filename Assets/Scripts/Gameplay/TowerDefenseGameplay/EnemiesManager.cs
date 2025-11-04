using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using TBT.Gameplay.TowerDefenseGameplay.Enemies;
using UnityEngine;
using UnityEngine.Serialization;

namespace TBT.Gameplay.TowerDefenseGameplay
{
    public class EnemiesManager : MonoBehaviour
    {
        private float timer = 3f;
        public List<EnemyClass> enemies;
        [FormerlySerializedAs("testEnemy")] public EnemyClass enemyClass;
        public event Action EnemiesTurnEnded;
        public void SetEnemiesMoving()
        {
            EnemyClass currentActingEnemyClass = enemies[0];
            currentActingEnemyClass.Act();
            StartCoroutine(WaitForTurn(currentActingEnemyClass));
        }

        void Start()
        {
            enemies.Add(enemyClass);
        }
        
        IEnumerator WaitForTurn(EnemyClass enemyClass)
        {
            while (enemyClass.isActive == true)
            {
                yield return null;
            }
            EnemyClass tempEnemyClass = enemies[0];
            enemies.RemoveAt(0);
            enemies.Add(tempEnemyClass);
            EnemiesTurnEnded?.Invoke();
        }
    }
}