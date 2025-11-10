using System;
using System.Collections;
using TBT.Gameplay.TowerDefenseGameplay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TBT.Gameplay.TowerDefenseUI
{
    public class RessourcesUI : MonoBehaviour
    {
        [SerializeField] private Carriage carriage;
        [SerializeField] private Image healthImage;
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private TextMeshProUGUI ressourcesText;
        [SerializeField] private Image ressourcesImage;
        [SerializeField] private AnimationCurve notEnoughRessourcesAnimation;
        private float notEnoughRessourcesAnimationDuration = 0.5f;
        private bool reloadInAnimation;

        private void OnEnable()
        {
            carriage.OnHealthChanged += UpdateHealth;
            carriage.OnRessourcesChanged += UpdateRessources;
            carriage.NotEnoughRessources += AgitateReloadButton;
        }

        private void OnDisable()
        {
            carriage.OnHealthChanged -= UpdateHealth;
            carriage.OnRessourcesChanged -= UpdateRessources;
            carriage.NotEnoughRessources -= AgitateReloadButton;
        }

        private void UpdateHealth(float currentHealth, float maxHealth)
        {
            healthImage.fillAmount = currentHealth / maxHealth;
            healthText.text = Mathf.Round(currentHealth).ToString();
        }

        private void UpdateRessources(int currentRessources)
        {
            ressourcesText.text = currentRessources.ToString();
        }

        private void AgitateReloadButton()
        {
            if (reloadInAnimation == false)
            {
               StartCoroutine(NotEnoughRessourcesAnimation()); 
            }
        }

        IEnumerator NotEnoughRessourcesAnimation()
        {
            reloadInAnimation = true;
            float elapsedTime = 0;
            Vector3 basePos = ressourcesImage.gameObject.transform.position;
            while (elapsedTime < notEnoughRessourcesAnimationDuration)
            {
                float addedPos = notEnoughRessourcesAnimation.Evaluate(elapsedTime);
                ressourcesImage.gameObject.transform.position = basePos+ Vector3.right * addedPos;
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            transform.position = basePos;
            reloadInAnimation = false;
        }
    }
}
