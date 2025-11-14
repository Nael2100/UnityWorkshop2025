using System.Collections;
using System.Collections.Generic;
using TBT.Gameplay.TowerDefenseGameplay;
using UnityEngine;
using UnityEngine.UI;

namespace TBT.Gameplay.TowerDefenseUI
{
    public class CharacterUI : MonoBehaviour
    {
        private List<SkillButton> buttons = new List<SkillButton>();
        private Character currentCharacter;
        [SerializeField] private RectTransform buttonsParent;
        [SerializeField] private GameObject buttonPrefab;
        [SerializeField] private RectTransform bottomBar;
        [SerializeField] private RectTransform icon;
        private bool isMovingPlayerPanel;
        public void Setup(Character character, TowerDefenseManager manager)
        {
            currentCharacter = character;
            icon.gameObject.GetComponent<Image>().sprite = character.data.icone;
            bottomBar.anchoredPosition = new Vector2(0, -200);
            icon.anchoredPosition = new Vector2(463, 0);
            CreateButtons(manager);
            StartCoroutine(EnteringAnimation());
        }
        
        void CreateButtons(TowerDefenseManager manager)
        {
            for (int i = 0; i < currentCharacter.activeSkills.Count; i++)
            {
                Vector3 newPosition = new Vector3(buttonsParent.position.x+240*i, buttonsParent.position.y);
                GameObject newButton = Instantiate(buttonPrefab, newPosition, Quaternion.identity, buttonsParent);
                SkillButton skillButtonRef = newButton.GetComponent<SkillButton>();
                buttons.Add(skillButtonRef);
                skillButtonRef.SetUp(skillButtonRef.GetComponentInChildren<Button>(),manager,currentCharacter.activeSkills[i]);
            }
        }

        public void Reset()
        {
            StartCoroutine(ExitAnimation());
        }

        public void BlockAllButtons()
        {
            Debug.Log(buttons);
            if (buttons.Count > 0)
            {
              foreach (SkillButton button in buttons)
              {
                     button.Deactivate();
              }  
            }
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
            while (bottomBar.anchoredPosition.y < -63)
            {
                bottomBar.position += Vector3.up * (speed * Time.deltaTime);
                yield return null;
            }
            speed = 500f;
            while (icon.anchoredPosition.x > -63)
            {
                icon.position += Vector3.left * (speed * Time.deltaTime);
                yield return null;
            }
            bottomBar.anchoredPosition = new Vector2(0, 0);
            icon.anchoredPosition = new Vector2(-63, 0);
            isMovingPlayerPanel = false;
        }
        IEnumerator ExitAnimation()
        {
            while (isMovingPlayerPanel)
            {
                yield return null;
            }
            isMovingPlayerPanel = true;
            bottomBar.anchoredPosition = new Vector2(0, 0);
            icon.anchoredPosition = new Vector2(63, 0);
            float speed = 500f;
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
            foreach (SkillButton button in buttons)
            {
                if (button != null)
                {
                    Destroy(button.gameObject);
                }
            }
            bottomBar.anchoredPosition = new Vector2(0, -200);
            icon.anchoredPosition = new Vector2(463, 0);
            isMovingPlayerPanel = false;
            
        }

    }
}