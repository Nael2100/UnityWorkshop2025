using System;
using TBT.Core;
using TBT.Core.Data.AudioData;
using TBT.Core.Data.EncountersData;
using TBT.Gameplay.Audio;
using TBT.Gameplay.TowerDefenseGameplay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TBT.Gameplay.EncounterGameplay.EncounterUI
{
    public class EncounterUIManager : MonoBehaviour
    {
        [SerializeField] private Canvas canva;
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private Button[] buttonChoices = new Button[3];
        [SerializeField] private Button closeButton;
        [SerializeField] private Image iconImage;
        [SerializeField] private Image[] charactersInBackground;
        public event Action<EncounterEffects> answerSelected;
        public event Action encounterCompleted;
        private EncounterEffects[] currentEffects;
        private string currentCompletionText;
        private void OnEnable()
        {
            canva.enabled = false;
            closeButton.gameObject.SetActive(false);
            currentEffects = new EncounterEffects[3];
        }

        public void SetUp(EncounterData data)
        {
            canva.enabled = true;
            foreach (var button in buttonChoices)
            {
                button.gameObject.SetActive(true);
            }
            titleText.text = data.title;
            descriptionText.text = data.text;
            iconImage.sprite = data.icon;
            currentCompletionText = data.resolvedText;
            for (int i = 0; i < data.answers.Length; i++)
            {
                buttonChoices[i].GetComponentInChildren<TextMeshProUGUI>().text = data.answers[i];
                currentEffects[i] = data.effects[i];
            }
        }
        
        public void Button1Clicked()
        {
            AudioManager.Instance.PlaySound(AudioName.clicSound);
            OnButtonClicked(currentEffects[0]);
        }
        public void Button2Clicked()
        {
            AudioManager.Instance.PlaySound(AudioName.clicSound);
            OnButtonClicked(currentEffects[1]);
        }
        public void Button3Clicked()
        {
            AudioManager.Instance.PlaySound(AudioName.clicSound);
            OnButtonClicked(currentEffects[2]);
        }

        private void OnButtonClicked(EncounterEffects effect)
        {
            descriptionText.text = currentCompletionText;
            foreach (var button in buttonChoices)
            {
                button.gameObject.SetActive(false);
            }
            closeButton.gameObject.SetActive(true);
            closeButton.onClick.RemoveAllListeners();
            closeButton.onClick.AddListener(CompleteEncounter);
            answerSelected?.Invoke(effect);
        }

        private void CompleteEncounter()
        {
            encounterCompleted?.Invoke();
        }

        public void SetUpBackground(Carriage carriage)
        {
            for (int i = 0; i < carriage.charactersOnCarriage.Length; i++)
            {
                charactersInBackground[i].enabled = carriage.charactersOnCarriage[i];
            }
        }
    }
}