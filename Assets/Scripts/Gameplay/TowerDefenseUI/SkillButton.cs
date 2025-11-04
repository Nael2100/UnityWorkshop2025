using TBT.Gameplay.TowerDefenseGameplay;
using UnityEngine;
using UnityEngine.UI;

namespace TBT.Gameplay.TowerDefenseUI
{
    public class SkillButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Image image;
        private Skill associatedSkill;
        public void SetUp(Button newButton, TowerDefenseManager manager, Skill skill)
        {
            button = newButton;
            associatedSkill = skill;
            newButton.onClick.AddListener(() => manager.PlaySkill(associatedSkill));
            
        }
    }
}