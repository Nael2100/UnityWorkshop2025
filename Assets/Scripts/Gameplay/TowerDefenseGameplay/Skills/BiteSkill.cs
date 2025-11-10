using System.Collections;
using TBT.Core.Data.SkillsData;
using UnityEngine;

namespace TBT.Gameplay.TowerDefenseGameplay.Skills
{
    public class BiteSkill : Skill
    {
        [SerializeField] private SkillDataScript data;
        [SerializeField] private GameObject damageZonePrefab;
        [SerializeField] private Sprite biteSprite;

        public override void LaunchSkill(Vector3 position)
        {
            base.LaunchSkill(position);
            GameObject damageZoneObject = Instantiate(damageZonePrefab, transform);
            DamageZone damageZone = damageZoneObject.GetComponent<DamageZone>();
            damageZoneObject.GetComponent<SpriteRenderer>().sprite = biteSprite;
            damageZoneObject.GetComponent<CircleCollider2D>().radius = data.size;
            damageZoneObject.transform.position = position;
            damageZone.SetDamage(data.damages);
            StartCoroutine(DamageLifeTime(damageZoneObject));
        }

        private IEnumerator DamageLifeTime(GameObject objectToDestroy)
        {
            yield return new WaitForSeconds(data.duration);
            Destroy(objectToDestroy);
        }
    }
}
