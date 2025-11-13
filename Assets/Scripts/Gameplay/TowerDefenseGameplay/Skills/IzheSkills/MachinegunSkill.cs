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
            Debug.Log("lauchSkill Machine done");
            GameObject damageZoneObject = Instantiate(damageZonePrefab, transform);
            DamageZone damageZone = damageZoneObject.GetComponent<DamageZone>();
            damageZone.SetSprites(size, areaSprite, iconSprite);
            damageZoneObject.transform.position = new Vector3(0,0,0);
            damageZone.SetDamage(damage);
            StartCoroutine(DamageLifeTime(damageZoneObject));
            Debug.Log("Coroutine Machine done");
            SkillPlayedEvent();
            Debug.Log("Machine finished");
        }

        private IEnumerator DamageLifeTime(GameObject objectToDestroy)
        {
            yield return new WaitForSeconds(data.duration);
            SkillPlayedEvent();
            Destroy(objectToDestroy);
            Debug.Log("Machine destroyed");
        }
        
    }
}
