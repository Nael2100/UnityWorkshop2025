using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TBT.Gameplay.CardsSystem
{
    public class CardsManager : MonoBehaviour
    {
        /*[SerializeField] private GameModeManager gameModeManager;
        [SerializeField] private List<GameObject> cardsPossibilities;
        [SerializeField] private Transform cardsParent;
        [SerializeField] private AnimationCurve callCardAnimation;
        private float callCardAnimationDuration = 2f;
        public List<Card> currentCards { get; private set; } = new List<Card>();
        public List<GameObject> currentCardsObjects { get; private set; } = new List<GameObject>();

        private bool canInteractWithCards;

        private void OnEnable()
        {
            gameModeManager.EnterCardsModeEvent += CallCards;
        }

        private void OnDisable()
        {
            gameModeManager.EnterCardsModeEvent -= CallCards;
            foreach (Card card in currentCards)
            {
                card.cardSelected -= CloseChoice;
            }
        }

        public void CallCards()
        {
            foreach (Card card in currentCards)
            {
                card.cardSelected -= CloseChoice;
            }
            currentCards.Clear();
            for (int i = 0; i < 3; i++)
            {
                GameObject newCartToAdd = cardsPossibilities[Random.Range(0, cardsPossibilities.Count)];
                while (currentCardsObjects.Contains(newCartToAdd))
                {
                    newCartToAdd = cardsPossibilities[Random.Range(0, cardsPossibilities.Count)];
                }
                GameObject cardObject = Instantiate(newCartToAdd, cardsParent);
                cardObject.transform.position = new Vector3(cardObject.transform.position.x, cardObject.transform.position.y, cardObject.transform.position.z+i);
                currentCardsObjects.Add(cardObject);
                currentCards.Add(currentCardsObjects[i].GetComponent<CardInterface>().cardComponent);
                cardObject.GetComponent<CardInterface>().cardComponent.cardSelected += CloseChoice;
                StartCoroutine(CallCardAnimation(cardObject, 0.5f*(i-1)));
            }
        }

        IEnumerator CallCardAnimation(GameObject cardObject, float angle)
        {
            float elapsedTime = 0;
            Vector3 basePos = cardsParent.transform.position;
            while (elapsedTime < callCardAnimationDuration)
            {
                float addedPos = callCardAnimation.Evaluate(elapsedTime);
                cardObject.transform.position = basePos + new Vector3(addedPos*angle, addedPos, 0f);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            canInteractWithCards = true;
        }

        IEnumerator CloseChoiceAnimation(GameObject cardObject, float angle)
        {
            Debug.Log(angle);
            float elapsedTime = callCardAnimationDuration;
            Vector3 basePos = cardsParent.transform.position;
            while (elapsedTime > 0)
            {
                float addedPos = callCardAnimation.Evaluate(elapsedTime);
                cardObject.transform.position = basePos + new Vector3(addedPos*angle, addedPos, 0f);
                elapsedTime -= Time.deltaTime;
                yield return null;
            }
            yield return new WaitForSeconds(1f);
            gameModeManager.EnterMapMode();
        }

        private void CloseChoice(GameObject cardObj)
        {
            if (canInteractWithCards)
            {
                canInteractWithCards = false;
                for (int i = 0; i < currentCardsObjects.Count; i++)
                {
                    if (currentCardsObjects[i] != cardObj)
                    {
                        StartCoroutine(CloseChoiceAnimation(currentCardsObjects[i], 0.5f*(i-1)));
                    }
                }
            }
            
        }*/
    }
}
