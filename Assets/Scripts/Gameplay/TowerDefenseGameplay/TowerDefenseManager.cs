using System;
using System.Collections;
using TBT.Core;
using TBT.Core.Data.AudioData;
using TBT.Core.Data.TowerDefenseData;
using TBT.Gameplay.Audio;
using TBT.Gameplay.TowerDefenseUI;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace TBT.Gameplay.TowerDefenseGameplay
{
    public class TowerDefenseManager : MonoBehaviour
    {
        public static TowerDefenseManager Instance;
        
        private bool playerTurn = false;
        [SerializeField] private GameObject[] mapsPrefabs;
        
        [SerializeField] private TowerDefenseData towerDefenseData;
        [SerializeField] private GameObject playerTurnPanel;
        [SerializeField] public Carriage playerCarriage;
        [SerializeField] private CharacterUI characterUI;
        [SerializeField] private GameModeManager gameModeManager;
        [SerializeField] public EnemiesManager enemiesManager;
        private Character activeCharacter;
        private Skill currentlyPlayingSkill;
        private int currentRound = 0;
        private int currentWave = 0;
        private int maxWaves;
        private bool finalFight;
        private GameObject currentMapObject;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(this);
            }
        }


        private void OnEnable()
        {
            gameModeManager.EnterTowerDefenseModeEvent += StartFight;
            gameModeManager.EnterTowerDefenseModeFinalEvent += StartFinalFight;
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

        private void StartFight()
        {
            if (currentMapObject != null)
            {
                Destroy(currentMapObject);
            }
            currentMapObject = Instantiate(mapsPrefabs[Random.Range(0, mapsPrefabs.Length)]);
            currentMapObject.transform.position = new Vector3(0,0,0);
            currentRound += 1;
            currentWave =1;
            playerCarriage.ResetPosition();
            maxWaves = Random.Range(towerDefenseData.minWaves,towerDefenseData.maxWaves);
            enemiesManager.SpawnEnemies(currentRound,currentWave, finalFight);
            PlayEnemiesTurn();
        }

        private void StartFinalFight()
        {
            finalFight = true;
            StartFight();
        }

        public void PlayPlayerTurn()
        {
            WaveEndCheck();
            playerTurn = true;
            playerTurnPanel.SetActive(true);
            activeCharacter = playerCarriage.ReturnCharacterToPlay();
            characterUI.Setup(activeCharacter,this);
        }

        private void WaveEndCheck()
        {
            if (enemiesManager.AllEnemiesDead())
            {
                if (currentWave >= maxWaves)
                {
                    if (finalFight)
                    {
                        gameModeManager.WinGameMode();
                    }
                    else
                    {
                        EndFight();
                    }
                }
                else
                {
                    currentWave += 1;
                    enemiesManager.SpawnEnemies(currentRound,currentWave, finalFight);
                }
            }
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
            PlayEnemiesTurn();
        }

        public void PlayEnemiesTurn()
        {
            if (enemiesManager.AllEnemiesDead())
            {
                if (currentWave >= maxWaves)
                {
                    if (finalFight)
                    {
                        gameModeManager.WinGameMode();
                    }
                    else
                    {
                        EndFight();
                    }
                }
                else
                {
                    currentWave += 1;
                    enemiesManager.SpawnEnemies(currentRound,currentWave, finalFight);
                }
            }
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
                AudioManager.Instance.PlaySound(AudioName.reload);
                SkillButton[] skillButtons = FindObjectsByType<SkillButton>(0);
                foreach (SkillButton button in skillButtons)
                {
                    if (button != null)
                    {
                        button.Deactivate();
                    }
                }
                EndPlayerTurn();
                playerCarriage.AddRessources(playerCarriage.maxRessources);
                
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

        public void SpawnAdditionalEnnemies(int numberOfEnemies)
        {
            enemiesManager.SpawnAdditionalEnemies(numberOfEnemies);
        }
    }
}
