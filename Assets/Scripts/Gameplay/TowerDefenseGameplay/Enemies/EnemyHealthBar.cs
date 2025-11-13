using System;
using UnityEngine;
using UnityEngine.UI;

namespace TBT.Gameplay.TowerDefenseGameplay.Enemies
{
    public class EnemyHealthBar : MonoBehaviour
    {
        [SerializeField] private EnemyClass enemy;

        private void OnEnable()
        {
            enemy.OnHealthChanged += UpdateHealthbar;
        }

        private void UpdateHealthbar(float currentHealth, float maxHealth)
        {
            gameObject.GetComponent<Image>().fillAmount = currentHealth / maxHealth;
        }

        private void Update()
        {
            Vector3 positionEcran = Camera.main.WorldToScreenPoint(enemy.gameObject.transform.position);
            gameObject.GetComponent<RectTransform>().position = new Vector3(positionEcran.x+-30, positionEcran.y+50, positionEcran.z);
        }
    }
}
