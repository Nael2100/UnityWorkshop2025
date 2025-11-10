using UnityEngine;

namespace TBT.Core.Data.CharacterData
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/CharacterData")]
    public class CharacterDataScript : ScriptableObject
    {
        public string characterName;
        public string[] lines;
        public Sprite icone;
    }
}
