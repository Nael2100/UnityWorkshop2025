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
        [SerializeField] private GameObject bottomBar;
        [SerializeField] private GameObject icon;

        public void Setup(Character character, TowerDefenseManager manager)
        {
            currentCharacter = character;
            //icon.GetComponent<Image>().sprite = character.data.icone;
            CreateButtons(manager);
        }
        
        void CreateButtons(TowerDefenseManager manager)
        {
            for (int i = 0; i < currentCharacter.activeSkills.Count; i++)
            {
                Vector3 newPosition = new Vector3(buttonsParent.position.x+200*i, buttonsParent.position.y);
                GameObject newButton = Instantiate(buttonPrefab, newPosition, Quaternion.identity, buttonsParent);
                SkillButton skillButtonRef = newButton.GetComponent<SkillButton>();
                buttons.Add(skillButtonRef);
                skillButtonRef.SetUp(skillButtonRef.GetComponentInChildren<Button>(),manager,currentCharacter.activeSkills[i]);
            }
        }

        public void Reset()
        {
            foreach (SkillButton button in buttons)
            {
                if (button != null)
                {
                    Destroy(button.gameObject);
                }
            }
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


    }
}