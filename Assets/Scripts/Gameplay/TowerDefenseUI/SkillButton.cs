using TBT.Gameplay.TowerDefenseGameplay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TBT.Gameplay.TowerDefenseUI
{
    public class SkillButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Image image;
        [SerializeField]  private TextMeshProUGUI nameText;
        [SerializeField]  private Image[] costImages;
        private Skill associatedSkill;
        
        public void SetUp(Button newButton, TowerDefenseManager manager, Skill skill)
        {
            button = newButton;
            associatedSkill = skill;
            nameText.text = skill.name;
            newButton.onClick.AddListener(() => manager.PlaySkill(associatedSkill));
            foreach (Image costImage in costImages)
            {
                costImage.gameObject.SetActive(false);
            }
            for (int i = 0; i < skill.ressourcesCost; i++)
            {
                costImages[i].gameObject.SetActive(true);
            }
        }

        public void Deactivate()
        {
            button.interactable = false;
        }
    }
}