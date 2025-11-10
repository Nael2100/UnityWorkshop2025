using UnityEngine;

namespace TBT.Core.Data.SkillsData
{
    [CreateAssetMenu(fileName = "SkillData", menuName = "Scriptable Objects/SkillData")]
    public class SkillDataScript : ScriptableObject
    {
        public string name;
        public float damages;
        public float size;
        public float range;
        public Sprite sprite;
        public int ressourcesCost;
        public float duration;
    }
}