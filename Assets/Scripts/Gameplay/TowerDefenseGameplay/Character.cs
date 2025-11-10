using System;
using System.Collections.Generic;
using TBT.Core.Data.CharacterData;
using UnityEngine;

namespace TBT.Gameplay.TowerDefenseGameplay
{
    public class Character : MonoBehaviour
    {
        [SerializeField] public CharacterDataScript data { get; private set; }
        [SerializeField] public List<GameObject> skillsPrefabs = new List<GameObject>();
        public List<Skill> activeSkills { get; private set; }= new List<Skill>();
        
        private void Start()
        {
            foreach (GameObject prefab in skillsPrefabs)
            {
                GameObject skillObject = Instantiate(prefab, transform);
                activeSkills.Add(skillObject.GetComponent<SkillInterface>().skillComponent);
            }
        }
    }
}