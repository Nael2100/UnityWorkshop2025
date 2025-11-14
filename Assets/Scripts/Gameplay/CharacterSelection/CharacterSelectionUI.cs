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
        
        [SerializeField] private GameObject[] characterPrefabs;
        [SerializeField] private Button startButton;
        [SerializeField] private Button[] characterButtons;
        [SerializeField] private Image[] characterOutlines = new Image[5];
        [SerializeField] private GameObject[] names = new GameObject[5];
        [SerializeField] private Image[] namesButtons = new Image[5];
        private bool[] characterSelected = new bool[5];

        private void OnEnable()
        {
            for (int i = 0; i < characterButtons.Length; i++)
            {
                characterOutlines[i].enabled = false;
            }
            startButton.onClick.AddListener(() => StartGame());
            startButton.gameObject.GetComponent<Image>().color = Color.grey;
        }

        public void ClickOnCharacter(int index)
        {
            if (characterSelected[index])
            {
                characterSelected[index] = false;
                characterOutlines[index].enabled = false;
            }
            else if (!characterSelected[index])
            {
                characterSelected[index] = true;
                characterOutlines[index].enabled = true;
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
                        namesButtons[i].color = Color.grey;
                    }
                }
                startButton.gameObject.GetComponent<Image>().color = Color.white;
            }
            else
            {
                for (int i = 0; i < characterButtons.Length; i++)
                {
                        characterButtons[i].gameObject.GetComponent<Image>().color = Color.white;
                        namesButtons[i].color = Color.white;
                }
                startButton.gameObject.GetComponent<Image>().color = Color.grey;
            }
        }
    }
}
