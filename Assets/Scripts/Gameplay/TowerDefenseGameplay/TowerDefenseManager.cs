using System;
using TBT.Core;
using TBT.Core.Data.TowerDefenseData;
using TBT.Gameplay.TowerDefenseUI;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace TBT.Gameplay.TowerDefenseGameplay
{
    public class TowerDefenseManager : MonoBehaviour
    {
        private bool playerTurn = false;
        [SerializeField] private TowerDefenseData towerDefenseData;
        [SerializeField] private GameObject playerTurnPanel;
        [SerializeField] private Carriage playerCarriage;
        [SerializeField] private CharacterUI characterUI;
        [SerializeField] private GameModeManager gameModeManager;
        [SerializeField] private EnemiesManager enemiesManager;
        private Character activeCharacter;
        private Skill currentlyPlayingSkill;
        private int currentRound = 0;
        private int currentWave = 0;
        private int maxWaves;
        

        private void OnEnable()
        {
            gameModeManager.EnterTowerDefenseModeEvent += StartFight;
            enemiesManager.EnemiesTurnEnded += EndEnemiesTurn;
            playerCarriage.Dying += EndFightByDeath;
            playerTurnPanel.SetActive(playerTurn);
            enemiesManager.SetUp(towerDefenseData);
        }

        private void OnDisable()
        {
            gameModeManager.EnterTowerDefenseModeEvent -= StartFight;
            enemiesManager.EnemiesTurnEnded -= EndEnemiesTurn;
            playerCarriage.Dying -= EndFightByDeath;
            if (currentlyPlayingSkill != null)
            {
                currentlyPlayingSkill.SkillPlayed -= EndPlayerTurn;
            }
        }

        public void EndFight()
        {
            playerTurn = false;
            enemiesManager.ResetEnemies();
            gameModeManager.EnterMapMode();
        }

        public void EndFightByDeath()
        {
            playerTurn = false;
            gameModeManager.EndGameMode();
        }

        public void StartFight()
        {
            currentRound += 1;
            currentWave =1;
            maxWaves = Random.Range(towerDefenseData.minWaves,towerDefenseData.maxWaves);
            enemiesManager.SpawnEnemies(currentRound,currentWave);
            PlayEnemiesTurn();
        }
        public void PlayPlayerTurn()
        {
            if (enemiesManager.AllEnemiesDead())
            {
                if (currentWave >= maxWaves)
                {
                    EndFight();
                }
                else
                {
                    currentWave += 1;
                    enemiesManager.SpawnEnemies(currentRound,currentWave);
                }
            }
            playerTurn = true;
            playerTurnPanel.SetActive(true);
            activeCharacter = playerCarriage.ReturnCharacterToPlay();
            characterUI.Setup(activeCharacter,this);
        }

        public void PlaySkill(Skill skill)
        {
            if (skill.ressourcesCost <= playerCarriage.currentRessources)
            {
                playerCarriage.AddRessources(-skill.ressourcesCost);
                foreach (Skill skillToActivate in activeCharacter.activeSkills)
                {
                     if (skillToActivate == skill)
                     {
                         currentlyPlayingSkill = skill;
                         currentlyPlayingSkill.Play();
                         characterUI.BlockAllButtons();
                         currentlyPlayingSkill.SkillPlayed += EndPlayerTurn;
                         currentlyPlayingSkill.EnemiesNeedToCollide += EnemiesNeedToCollide;
                         break;
                     }
                }  
            }
            else
            {
                playerCarriage.TriedToLaunchWithUnsufficientRessources();
            }
        }
        public void EndPlayerTurn()
        {
            if (currentlyPlayingSkill != null)
            {
                currentlyPlayingSkill.SkillPlayed -= EndPlayerTurn;
            }
            currentlyPlayingSkill = null;
            playerTurn = false;
            characterUI.Reset();
            playerTurnPanel.SetActive(false);
            PlayEnemiesTurn();
        }

        public void PlayEnemiesTurn()
        {
            EnemiesNeedToCollide(true);
            enemiesManager.SetEnemiesMoving();
        }

        public void EndEnemiesTurn()
        {
            EnemiesNeedToCollide(false);
            PlayPlayerTurn();
        }

        public void Reload()
        {
            if (playerTurn && currentlyPlayingSkill == null)
            {
                characterUI.BlockAllButtons();
                playerCarriage.AddRessources(playerCarriage.maxRessources);
                EndPlayerTurn();
            }
        }

        private void EnemiesNeedToCollide(bool collidersEnabled)
        {
            if (collidersEnabled)
            {
                enemiesManager.AllEnemiesActivateCollider();
            }
            else
            {
                enemiesManager.AllEnemiesDeactivateCollider();
            }
        }
    }
}
