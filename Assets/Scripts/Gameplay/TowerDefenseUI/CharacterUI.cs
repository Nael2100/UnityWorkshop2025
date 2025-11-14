using System;
using System.Collections;
using System.Collections.Generic;
using TBT.Gameplay.TowerDefenseGameplay;
using UnityEngine;
using UnityEngine.UI;

namespace TBT.Gameplay.TowerDefenseUI
{
    public class CharacterUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canva;
        private List<SkillButton> buttons = new List<SkillButton>();
        private Character currentCharacter;
        [SerializeField] private RectTransform buttonsParent;
        [SerializeField] private GameObject buttonPrefab;
        [SerializeField] private RectTransform bottomBar;
        [SerializeField] private RectTransform icon;
        private bool isMovingPlayerPanel;
        public void Setup(Character character, TowerDefenseManager manager)
        {
            Debug.Log("set up ui");
            currentCharacter = character;
            icon.gameObject.GetComponent<Image>().sprite = character.data.icone;
            bottomBar.anchoredPosition = new Vector2(0, -200);
            icon.anchoredPosition = new Vector2(463, 0);
            SetUpButtons();
            StartCoroutine(EnteringAnimation());
        }

        private void Start()
        {
            CreateButtons(TowerDefenseManager.Instance);
        }

        void CreateButtons(TowerDefenseManager manager)
        {
            for (int i = 0; i < 3; i++)
            {
                Vector3 newPosition = new Vector3(buttonsParent.anchoredPosition.x+(450*i), buttonsParent.anchoredPosition.y,0);
                GameObject newButton = Instantiate(buttonPrefab, buttonsParent);
                newButton.GetComponent<RectTransform>().anchoredPosition = newPosition;
                SkillButton skillButtonRef = newButton.GetComponent<SkillButton>();
                if (buttons.Count <= i)
                {
                    buttons.Add(skillButtonRef);
                }
                else
                {
                    buttons[i] = (skillButtonRef);
                }
                
            }
        }

        void SetUpButtons()
        {
            for (int i = 0; i < currentCharacter.activeSkills.Count; i++)
            {
                buttons[i].SetUp(currentCharacter.activeSkills[i]);
            }
        }

        public void Reset()
        {
            StartCoroutine(ExitAnimation());
        }

        public void BlockAllButtons()
        {
            canva.interactable = false;
        }

        IEnumerator EnteringAnimation()
        {
            while (isMovingPlayerPanel)
            {
                yield return null;
            }
            isMovingPlayerPanel = true;
            bottomBar.anchoredPosition = new Vector2(0, -200);
            icon.anchoredPosition = new Vector2(463, 0);
            float speed= 300f;
            while (bottomBar.anchoredPosition.y < 0)
            {
                bottomBar.position += Vector3.up * (speed * Time.deltaTime);
                yield return null;
            }
            speed = 900f;
            while (icon.anchoredPosition.x > -63)
            {
                icon.position += Vector3.left * (speed * Time.deltaTime);
                yield return null;
            }
            bottomBar.anchoredPosition = new Vector2(0, 0);
            icon.anchoredPosition = new Vector2(-63, 0);
            isMovingPlayerPanel = false;
            canva.interactable = true;
        }
        IEnumerator ExitAnimation()
        {

            while (isMovingPlayerPanel)
            {
                yield return null;
            }           
            canva.interactable = false;
            isMovingPlayerPanel = true;
            bottomBar.anchoredPosition = new Vector2(0, 0);
            icon.anchoredPosition = new Vector2(63, 0);
            float speed = 900f;
            while (icon.anchoredPosition.x <463f)
            {
                icon.position += Vector3.right * (speed * Time.deltaTime);
                yield return null;
            }
            speed= 300f;
            while (bottomBar.anchoredPosition.y > -200f)
            {
                bottomBar.position += Vector3.down * (speed * Time.deltaTime);
                yield return null;
            }
            bottomBar.anchoredPosition = new Vector2(0, -200);
            icon.anchoredPosition = new Vector2(463, 0);
            isMovingPlayerPanel = false;
            
        }

    }
}