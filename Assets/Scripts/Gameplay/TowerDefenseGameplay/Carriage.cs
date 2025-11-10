using System;
using System.Collections;
using System.Collections.Generic;
using TBT.Core.Data.CharacterData;
using UnityEngine;

namespace TBT.Gameplay.TowerDefenseGameplay
{
    public class Carriage : MonoBehaviour
    {
        [SerializeField] private CarriageData carriageData;
        private List<GameObject> charactersPrefabs = new List<GameObject>();
        [SerializeField] private AnimationCurve takeDamageAnimationCurve;
        private float takeDamageAnimationDuration =1f;
        public float maxHealth {get; private set;}
        public float currentHealth {get; private set;}
        public int maxRessources {get; private set;}
        public int currentRessources {get; private set;}
        private int currentCharacterPlayingIndex = -1;
        private List<Character> characters = new List<Character>();
        
        public event Action Dying;
        public event Action<float, float> OnHealthChanged;
        public event Action<int> OnRessourcesChanged;
        public event Action NotEnoughRessources;
        private void Start()
        {
            maxHealth = carriageData.health;
            currentHealth = maxHealth;
            maxRessources = carriageData.ressources;
            currentRessources = maxRessources;
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
            OnRessourcesChanged?.Invoke(currentRessources);
        }

        public Character ReturnCharacterToPlay()
        {
            currentCharacterPlayingIndex ++;
            if (currentCharacterPlayingIndex >= characters.Count)
            {
                currentCharacterPlayingIndex = 0;
            }
            return characters[currentCharacterPlayingIndex];
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            if (currentHealth < 0)
            {
                currentHealth = 0;
            }

            OnHealthChanged?.Invoke(currentHealth, maxHealth);
            StartCoroutine(TakeDamageAnimation());
            CheckStillAlive();
        }

        private void CheckStillAlive()
        {
            if (currentHealth <= 0)
            {
                Dying?.Invoke();
            }
        }

        public void AddRessources(int amount)
        {
            currentRessources += amount;
            if (currentRessources > maxRessources)
            {
                currentRessources = maxRessources;
            }
            OnRessourcesChanged?.Invoke(currentRessources);
        }

        IEnumerator TakeDamageAnimation()
        {
            float elapsedTime = 0;
            Vector3 basePos = gameObject.transform.position;
            while (elapsedTime < takeDamageAnimationDuration)
            {
                float addedPos = takeDamageAnimationCurve.Evaluate(elapsedTime);
                transform.position = basePos+ Vector3.up * addedPos;
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            transform.position = basePos;
        }

        public void RefillHealth()
        {
            currentHealth = maxHealth;
        }

        public void TriedToLaunchWithUnsufficientRessources()
        {
            NotEnoughRessources?.Invoke();
        }

        public void AddCharacterPrefab(GameObject prefab)
        {
            charactersPrefabs.Add(prefab);
        }

        public void SetUpCharacters()
        {
            foreach (GameObject characterObj in charactersPrefabs)
            {
                GameObject newCharacter = Instantiate(characterObj, transform);
                characters.Add(newCharacter.GetComponent<Character>());
            }
        }
    }
}