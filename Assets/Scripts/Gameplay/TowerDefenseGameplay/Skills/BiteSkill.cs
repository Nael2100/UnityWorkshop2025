using TBT.Core.Data.SkillsData;
using UnityEngine;

namespace TBT.Gameplay.TowerDefenseGameplay.Skills
{
    public class BiteSkill : Skill
    {
        [SerializeField] private SkillDataScript data;
        [SerializeField] private GameObject damageZonePrefab;
        [SerializeField] private Sprite biteSprite;
        
        public override void LaunchSkill()
        {
            base.LaunchSkill();
            GameObject damageZoneObject = Instantiate(damageZonePrefab, transform);
            DamageZone damageZone = damageZoneObject.GetComponent<DamageZone>();
            damageZoneObject.GetComponent<SpriteRenderer>().sprite = biteSprite;
            damageZoneObject.GetComponent<CircleCollider2D>().radius = data.size;
            
        }
    }
}
