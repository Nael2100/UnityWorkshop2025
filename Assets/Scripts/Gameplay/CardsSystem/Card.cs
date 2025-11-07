using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TBT.Gameplay.CardsSystem
{
    public class Card : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] protected string cardName;
        [SerializeField] protected string cardDescription;
        [SerializeField] protected TextMesh cardNameTextMesh;
        [SerializeField] protected TextMesh cardDescriptionTextMesh;
        
        public event Action<GameObject> cardSelected;

        private void OnEnable()
        {
            cardNameTextMesh.text = cardName;
            cardDescriptionTextMesh.text = cardDescription;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            cardSelected?.Invoke(this.gameObject);
        }

        virtual protected void ApplyEffects()
        {
            
        }
    }
}
