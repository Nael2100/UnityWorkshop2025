using System;
using TBT.Gameplay.MapGameplay;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TBT.Gameplay
{
    public class GameModeManager : MonoBehaviour
    {
        [SerializeField] private GameObject towerDefensePosRef;
        [SerializeField] private GameObject mapPosRef;
        [SerializeField] private GameObject encounterPosRef;
        [SerializeField] private GameObject cardsPosRef;
        [SerializeField] private Canvas permanentCanvas;
        [SerializeField] private Canvas towerDefenseCanvas;
        [SerializeField] private Canvas encounterCanvas;
        
        private Camera mainCamera;

        public event Action EnterTowerDefenseModeEvent, EnterEncounterModeEvent, EnterCardsModeEvent; 

        private void Start()
        {
            mainCamera = Camera.main;
            EnterMapMode();
        }

        public void EnterTowerDefenseMode()
        {
            EnterModeProcess(towerDefensePosRef,towerDefenseCanvas);
            EnterTowerDefenseModeEvent?.Invoke();
        }

        public void EnterEncounterMode()
        {
            EnterModeProcess(encounterPosRef, encounterCanvas);
            EnterEncounterModeEvent?.Invoke();
        }

        public void EnterMapMode()
        {
            EnterModeProcess(mapPosRef,null);
        }

        public void EnterCardsMode()
        {
            EnterModeProcess(cardsPosRef,null);
            EnterCardsModeEvent?.Invoke();
        }

        public void EndGameMode()
        {
            SceneManager.LoadScene("SampleScene");
        }

        private void EnterModeProcess(GameObject posRef, Canvas canvasToActivate)
        {
            mainCamera.transform.position = new Vector3(posRef.transform.position.x, posRef.transform.position.y, -10);
            foreach (Canvas canva in FindObjectsOfType<Canvas>(includeInactive: true))
            {
                if (canva == canvasToActivate)
                {
                    canva.enabled = true;
                }
                else
                {
                    canva.enabled = false;
                }
            }
            permanentCanvas.enabled = true;
        }
    }
}
