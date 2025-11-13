using System.Collections;
using UnityEngine;

namespace TBT.Gameplay.TowerDefenseGameplay.Skills.SkillsFamilies
{
    public class HealingSkill : Skill
    {
        [SerializeField] private GameObject healZonePrefab;
        
        public override void ApplyEffects()
        {
            base.ApplyEffects();
            TowerDefenseManager.Instance.playerCarriage.Heal(data.heal);
            StartCoroutine(ApplyEffectsDelay());
        }
        IEnumerator ApplyEffectsDelay()
        {
            yield return null;
            GameObject healZoneObject = Instantiate(healZonePrefab, transform);
            HealingZone healZone = healZoneObject.GetComponent<HealingZone>();
            healZone.SetSprites(size, areaSprite, iconSprite);
            healZoneObject.transform.position = transform.position;
            StartCoroutine(HealLifeTime(healZoneObject));
        }
        private IEnumerator HealLifeTime(GameObject objectToDestroy)
        {
            yield return new WaitForSeconds(duration);
            Destroy(objectToDestroy);
            SkillPlayedEvent();
        }
    }
}