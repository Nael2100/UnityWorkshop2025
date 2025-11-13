using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace TBT.Gameplay.TowerDefenseGameplay.Skills.SkillsFamilies
{
    public class LaunchingSkill : Skill
    {
        [SerializeField] private GameObject damageZonePrefab;
        [SerializeField] private Sprite skillSprite;
        [SerializeField] private Sprite skillArea;

        public override void LaunchSkill(Vector3 position)
        {
            base.LaunchSkill(position);
            GameObject damageZoneObject = Instantiate(damageZonePrefab, transform);
            DamageZone damageZone = damageZoneObject.GetComponent<DamageZone>();
            damageZone.SetSprites(size, areaSprite, iconSprite);
            damageZoneObject.transform.position = position;
            damageZone.SetDamage(damage);
            StartCoroutine(DamageLifeTime(damageZoneObject));
        }

        private IEnumerator DamageLifeTime(GameObject objectToDestroy)
        {
            yield return new WaitForSeconds(data.duration);
            SkillPlayedEvent();
            Destroy(objectToDestroy);
        }
    }
}