using System;
using System.Collections.Generic;
using UnityEngine;

namespace TBT.Gameplay.TowerDefenseGameplay
{
    public class Carriage : MonoBehaviour
    {
        private List<Character> characters = new List<Character>();
        public float health {get; private set;}
        private int currentCharacterPlayingIndex = 0;
        public Character testCharacter;

        private void Start()
        {
            characters.Add(testCharacter);
        }

        public Character ReturnCharacterToPlay()
        {
            return characters[currentCharacterPlayingIndex];
        }
    }
}