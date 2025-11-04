using System.Collections.Generic;
using TBT.Gameplay.TowerDefenseGameplay;
using UnityEngine;
using UnityEngine.UI;

namespace TBT.Gameplay.TowerDefenseUI
{
    public class CharacterUI : MonoBehaviour
    {
        private List<GameObject> buttons = new List<GameObject>();
        private Character currentCharacter;
        [SerializeField] private RectTransform buttonsParent;
        [SerializeField] private GameObject buttonPrefab;

        public void Setup(Character character, TowerDefenseManager manager)
        {
            currentCharacter = character;
            CreateButtons(manager);
        }
        
        void CreateButtons(TowerDefenseManager manager)
        {
            for (int i = 0; i < currentCharacter.Skills.Count; i++)
            {
                Vector3 newPosition = new Vector3(buttonsParent.position.x+200*i, buttonsParent.position.y);
                GameObject newButton = Instantiate(buttonPrefab, buttonsParent.transform);
                SkillButton skillButtonRef = newButton.GetComponent<SkillButton>();
                buttons.Add(newButton);
                skillButtonRef.SetUp(skillButtonRef.GetComponentInChildren<Button>(),manager,currentCharacter.Skills[i]);
            }
        }

        public void Reset()
        {
            foreach (GameObject buttonObject in buttons)
            {
                Destroy(buttonObject);
            }
        }
    }
}