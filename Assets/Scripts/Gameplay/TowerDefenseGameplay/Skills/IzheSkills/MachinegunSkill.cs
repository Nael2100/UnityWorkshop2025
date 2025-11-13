using System.Collections;
using UnityEngine;

namespace TBT.Gameplay.TowerDefenseGameplay.Skills.IzheSkills
{
    public class MachinegunSkill : Skill
    {
        [SerializeField] private GameObject damageZonePrefab;
        [SerializeField] private Sprite machinegunSprite;
        
        
        public override void ApplyEffects()
        {
            base.ApplyEffects();
            StartCoroutine(ApplyEffectsDelay());
        }

        IEnumerator ApplyEffectsDelay()
        {
            yield return null;
            GameObject damageZoneObject = Instantiate(damageZonePrefab, transform);
            DamageZone damageZone = damageZoneObject.GetComponent<DamageZone>();
            damageZoneObject.GetComponent<SpriteRenderer>().sprite = machinegunSprite;
            damageZoneObject.transform.localScale = new Vector3(data.size, data.size, 1);
            damageZoneObject.transform.position = transform.position;
            damageZone.SetDamage(data.damages);
            StartCoroutine(DamageLifeTime(damageZoneObject));
            SkillPlayedEvent();
        }

        private IEnumerator DamageLifeTime(GameObject objectToDestroy)
        {
            yield return new WaitForSeconds(data.duration);
            SkillPlayedEvent();
            Destroy(objectToDestroy);
        }
        
    }
}
