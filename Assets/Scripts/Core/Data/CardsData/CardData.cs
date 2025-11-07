using UnityEngine;

namespace TBT.Core.Data.CardsData
{
    [CreateAssetMenu(fileName = "CardData", menuName = "Scriptable Objects/CardData")]
    public class CardData : ScriptableObject
    {
        public string cardName;
        public string cardUpEffect;
        public string cardDownEffect;
        public Sprite cardSprite;
    }
}
