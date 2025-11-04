using System;
using TBT.Gameplay.TowerDefenseUI;
using UnityEngine;
using UnityEngine.Events;

namespace TBT.Gameplay.TowerDefenseGameplay
{
    public class TowerDefenseManager : MonoBehaviour
    {
        private bool playerTurn = false;
        [SerializeField] private GameObject playerTurnPanel;
        [SerializeField] private Carriage playerCarriage;
        [SerializeField] private CharacterUI characterUI;
        [SerializeField] private GameModeManager gameModeManager;
        [SerializeField] private EnemiesManager enemiesManager;
        private Character activeCharacter;
        private Skill currentlyPlayingSkill;

        private void OnEnable()
        {
            gameModeManager.EnterTowerDefenseModeEvent += PlayPlayerTurn;
            enemiesManager.EnemiesTurnEnded += EndEnemiesTurn;
            playerTurnPanel.SetActive(playerTurn);
        }

        private void OnDisable()
        {
            gameModeManager.EnterTowerDefenseModeEvent -= PlayPlayerTurn;
            enemiesManager.EnemiesTurnEnded -= EndEnemiesTurn;
            currentlyPlayingSkill.SkillPlayed -= EndPlayerTurn;
        }

        public void PlayPlayerTurn()
        {
            playerTurn = true;
            playerTurnPanel.SetActive(true);
            activeCharacter = playerCarriage.ReturnCharacterToPlay();
            characterUI.Setup(activeCharacter,this);
        }

        public void PlaySkill(Skill skill)
        {
            Debug.Log(skill.name);
            foreach (Skill skillToActivate in activeCharacter.Skills)
            {
                if (skillToActivate == skill)
                {
                    currentlyPlayingSkill = skill;
                    currentlyPlayingSkill.SkillPlayed += EndPlayerTurn;
                    currentlyPlayingSkill.Play();
                }
            }
        }
        public void EndPlayerTurn()
        {
            currentlyPlayingSkill.SkillPlayed -= EndPlayerTurn;
            playerTurn = false;
            playerTurnPanel.SetActive(false);
            characterUI.Reset();
            PlayEnemiesTurn();
        }

        public void PlayEnemiesTurn()
        {
            enemiesManager.SetEnemiesMoving();
        }

        public void EndEnemiesTurn()
        {
            PlayPlayerTurn();
        }
    }
}
