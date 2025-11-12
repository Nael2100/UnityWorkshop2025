using System;
using TBT.Gameplay.TowerDefenseGameplay;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace TBT.Gameplay.CharacterSelection
{
    public class CharacterSelectionUIManager : MonoBehaviour
    {
        [SerializeField] private GameModeManager gameModeManager;
        [SerializeField] private Carriage carriage;
        [SerializeField] private GameObject[] characterObjects;
        [SerializeField] private GameObject[] characterPrefabs;
        [SerializeField] private Button startButton;
        private Button[] characterButtons = new Button[5];
        private bool[] characterSelected = new bool[5];

        private void OnEnable()
        {
            for (int i = 0; i < characterObjects.Length; i++)
            {
                characterObjects[i].GetComponent<Image>().enabled = false;
                characterButtons[i] = characterObjects[i].GetComponentInChildren<Button>();
            }
            startButton.onClick.AddListener(() => StartGame());
            startButton.gameObject.GetComponent<Image>().color = Color.grey;
        }

        public void ClickOnCharacter(int index)
        {
            if (characterSelected[index])
            {
                characterSelected[index] = false;
                characterObjects[index].GetComponent<Image>().enabled = false;
            }
            else if (!characterSelected[index])
            {
                characterSelected[index] = true;
                characterObjects[index].GetComponent<Image>().enabled = true;
            }
            TurnGreyCheck();
        }

        private void StartGame()
        {
            if (CheckListCompleted())
            {
                for (int i = 0; i < 5; i++)
                {
                    if (characterSelected[i])
                    { 
                        carriage.AddCharacterPrefab(characterPrefabs[i],i);
                    }
                }
                carriage.SetUpCharacters();
                gameModeManager.EnterMapMode();
            }
        }

        private bool CheckListCompleted()
        {
            int currentlySelectedCount = 0;
            foreach (bool selected in characterSelected)
            {
                if (selected)
                {
                    currentlySelectedCount++;
                }
            }
            return currentlySelectedCount == 3;
        }

        private void TurnGreyCheck()
        {
            if (CheckListCompleted())
            {
                for (int i = 0; i < characterButtons.Length; i++)
                {
                    if (!characterSelected[i])
                    {
                        characterButtons[i].gameObject.GetComponent<Image>().color = Color.grey;
                    }
                }
                startButton.gameObject.GetComponent<Image>().color = Color.white;
            }
            else
            {
                for (int i = 0; i < characterButtons.Length; i++)
                {
                        characterButtons[i].gameObject.GetComponent<Image>().color = Color.white;
                }
                startButton.gameObject.GetComponent<Image>().color = Color.grey;
            }
        }
    }
}
