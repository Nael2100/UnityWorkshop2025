using System;
using TBT.Gameplay.TowerDefenseGameplay;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace TBT.Gameplay.TowerDefenseGameplay.Skills
{
    public class HealCarriage : MonoBehaviour
    {
        [SerializeField] private float healthAdd;
        public ScriptableObject carriageData;
        public MonoScript carriage;
        private float currentHealth;

        public void SetHealth()
        {
            currentHealth = +healthAdd;
        }
        
    }
}