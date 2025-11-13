using System;
using TBT.Core.Data.CardsData;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TBT.Gameplay.CardsSystem
{
    public class Card : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] protected CardData data;
        private string cardName;
        private string cardUpText;
        private string cardDownText;
        [SerializeField] protected TextMesh cardNameTextMesh;
        [SerializeField] protected TextMesh cardUpDescriptionTextMesh;
        [SerializeField] protected TextMesh cardDownDescriptionTextMesh;
        
        public event Action<GameObject> cardSelected;

        private void OnEnable()
        {
            cardName = data.name;
            cardNameTextMesh.text = cardName;
            cardUpText = data.cardUpEffect;
            cardDownText = data.cardDownEffect;
            cardUpDescriptionTextMesh.text = cardUpText;
            cardDownDescriptionTextMesh.text = cardDownText;
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
