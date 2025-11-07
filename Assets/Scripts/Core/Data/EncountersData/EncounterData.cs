using UnityEngine;

namespace TBT.Core.Data.EncountersData
{
    [CreateAssetMenu(fileName = "EncounterData", menuName = "Scriptable Objects/EncounterData")]
    public class EncounterData : ScriptableObject
    {
        public string title;
        public string text;
        public string resolvedText;
        public string[] answers;
        public EncounterEffects[] effects;
        public Sprite icon;
        public Sprite background;
    }
}
