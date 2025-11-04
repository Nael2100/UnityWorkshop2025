using System;
using System.Collections.Generic;
using UnityEngine;

namespace TBT.Gameplay.TowerDefenseGameplay
{
    public class Character : MonoBehaviour
    {
        public List<Skill> Skills { get; private set; } = new List<Skill>();
        public Skill testSkill;

        private void Start()
        {
            Skills.Add(testSkill);
        }
    }
}