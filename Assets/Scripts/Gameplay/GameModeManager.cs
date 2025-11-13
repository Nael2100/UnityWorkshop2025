using System;
using System.Collections;
using System.Collections.Generic;
using TBT.Gameplay.MapGameplay;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TBT.Gameplay
{
    public class GameModeManager : MonoBehaviour
    {
        [SerializeField] private GameObject clicksBlock;
        [SerializeField] private RectTransform rightPanel;
        [SerializeField] private RectTransform leftPanel;
        [SerializeField] private GameObject towerDefensePosRef;
        [SerializeField] private GameObject mapPosRef;
        [SerializeField] private GameObject encounterPosRef;
        [SerializeField] private GameObject cardsPosRef;
        [SerializeField] private Canvas permanentCanvas;
        [SerializeField] private Canvas towerDefenseCanvas;
        [SerializeField] private Canvas encounterCanvas;
        [SerializeField] private Canvas characterSelectionCanvas;
        [SerializeField] private Canvas transitionCanvas;
        private List<Canvas> notPermaCanvas = new List<Canvas>();
        
        private Camera mainCamera;

        public event Action EnterTowerDefenseModeEvent, EnterTowerDefenseModeFinalEvent, EnterEncounterModeEvent, EnterCardsModeEvent; 

        private void Start()
        {
            notPermaCanvas.Add(towerDefenseCanvas);
            notPermaCanvas.Add(encounterCanvas);
            notPermaCanvas.Add(characterSelectionCanvas);
            mainCamera = Camera.main;
            clicksBlock.SetActive(false);
            leftPanel.anchoredPosition = new Vector2(-960,0);
            rightPanel.anchoredPosition = new Vector2(960,0);
            EnterModeProcess(mapPosRef, characterSelectionCanvas);
            permanentCanvas.enabled = false;
        }

        public void EnterTowerDefenseMode()
        {
            StartCoroutine(EnterModeProcess(towerDefensePosRef,towerDefenseCanvas, EnterTowerDefenseModeEvent));
        }

        public void EnterTowerDefenseModeFinal()
        {
            StartCoroutine(EnterModeProcess(towerDefensePosRef,towerDefenseCanvas, EnterTowerDefenseModeFinalEvent));
        }

        public void EnterEncounterMode()
        {
            StartCoroutine(EnterModeProcess(encounterPosRef, encounterCanvas, EnterEncounterModeEvent)); 
        }

        public void EnterMapMode()
        {

            StartCoroutine(EnterModeProcess(mapPosRef, null));
        }

        public void EnterCardsMode()
        {
            StartCoroutine(EnterModeProcess(cardsPosRef, null, EnterCardsModeEvent));
        }

        public void EndGameMode()
        {
            SceneManager.LoadScene(2);
        }

        public void WinGameMode()
        {
            SceneManager.LoadScene(3);
        }

        private IEnumerator EnterModeProcess(GameObject posRef, Canvas canvasToActivate, Action action = null)
        {
            clicksBlock.SetActive(true);
            float speed = 1000f;
            leftPanel.anchoredPosition = new Vector2(-960,0);
            rightPanel.anchoredPosition = new Vector2(960,0);
            while (leftPanel.anchoredPosition.x < 0 && rightPanel.anchoredPosition.x > 0)
            {
                leftPanel.anchoredPosition += new Vector2(speed * Time.deltaTime, 0);
                rightPanel.anchoredPosition -= new Vector2(speed * Time.deltaTime, 0);
                yield return null;
            }
            mainCamera.transform.position = new Vector3(posRef.transform.position.x, posRef.transform.position.y, -10);
            foreach (Canvas canva in notPermaCanvas)
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
            transitionCanvas.enabled = true;
            while (leftPanel.anchoredPosition.x > -960 && rightPanel.anchoredPosition.x < 960)
            {
                leftPanel.anchoredPosition -= new Vector2(speed * Time.deltaTime, 0);
                rightPanel.anchoredPosition += new Vector2(speed * Time.deltaTime, 0);
                yield return null;
            }
            leftPanel.anchoredPosition = new Vector2(-960,0);
            rightPanel.anchoredPosition = new Vector2(960,0);
            clicksBlock.SetActive(false);
            if (action != null)
            {
                action?.Invoke();
            }
        }
    }
}
