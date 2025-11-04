using System;
using System.Collections;
using UnityEngine;

namespace TBT.Gameplay.TowerDefenseGameplay.Enemies
{
    public class EnemyClass : MonoBehaviour
    {
        public bool isActive;
        public void Act()
        {
            isActive = true;
            StartCoroutine(Action());
        }

        IEnumerator Action()
        {
            yield return new WaitForSeconds(3f);
            isActive = false;
        }
    }
}