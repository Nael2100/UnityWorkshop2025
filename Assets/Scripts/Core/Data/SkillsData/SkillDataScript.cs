using TBT.Core.Data.AudioData;
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
        public Sprite areaSprite;
        public Sprite iconSprite;
        public int ressourcesCost;
        public float duration;
        public float heal;
        public AudioName audioPlayed;
    }
}