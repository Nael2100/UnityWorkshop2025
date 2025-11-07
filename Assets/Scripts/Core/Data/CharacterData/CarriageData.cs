using UnityEngine;

namespace TBT.Core.Data.CharacterData
{
    [CreateAssetMenu(fileName = "CarriageData", menuName = "Scriptable Objects/CarriageData")]
    public class CarriageData : ScriptableObject
    {
        public float health;
        public int ressources;
    }
}
