using System;
using UnityEngine;

namespace TBT.Gameplay.TowerDefenseGameplay
{
    public class Skill : MonoBehaviour
    {
        public Sprite sprite { get; private set; }
        public string name { get; private set; } = "nom";
        public event Action SkillPlayed;

        public void Play()
        {
            SkillPlayed?.Invoke();
        }
    }
}