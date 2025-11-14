using System;
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
        [SerializeField]  public TextMeshProUGUI nameText;
        [SerializeField]  private Image[] costImages;
        private Skill associatedSkill;

        private void OnEnable()
        {
            button = GetComponentInChildren<Button>();
        }

        public void SetUp(Skill skill)
        {
            associatedSkill = skill;
            nameText.text = skill.name;
            image.sprite = skill.iconSprite;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => TowerDefenseManager.Instance.PlaySkill(associatedSkill));
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