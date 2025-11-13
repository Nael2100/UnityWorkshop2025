using System;
using System.Collections.Generic;
using TBT.Core;
using TBT.Core.Data.EncountersData;
using TBT.Gameplay.EncounterGameplay.EncounterUI;
using TBT.Gameplay.TowerDefenseGameplay;
using UnityEngine;

namespace TBT.Gameplay
{
    public class EncounterManager : MonoBehaviour
    {
        [SerializeField] private EncounterUIManager UIManager;
        [SerializeField] private List<EncounterData> encounters;
        [SerializeField] private GameModeManager gameModeManager;
        [SerializeField] private Carriage carriage;
        
        public void InitializeEncounter()
        {
            EncounterData selectedData = encounters[UnityEngine.Random.Range(0, encounters.Count)];
            encounters.Remove(selectedData);
            UIManager.SetUp(selectedData);
            UIManager.SetUpBackground(carriage);
            
        }

        private void OnEnable()
        {
            UIManager.answerSelected += ApplyEffects;
            UIManager.encounterCompleted += CompleteEncounter;
            gameModeManager.EnterEncounterModeEvent += InitializeEncounter;
            
        }

        private void OnDisable()
        {
            UIManager.answerSelected -= ApplyEffects;
            UIManager.encounterCompleted -= CompleteEncounter;
            gameModeManager.EnterEncounterModeEvent -= InitializeEncounter;
        }

        private void CompleteEncounter()
        {
            gameModeManager.EnterMapMode();
        }
        private void ApplyEffects(EncounterEffects obj)
        {
            if (obj == EncounterEffects.refillHealth)
            {
                carriage.RefillHealth();
            }
            else if (obj == EncounterEffects.refillRessources)
            {
                TowerDefenseManager.Instance.Reload();
            }
        }
    }
}
