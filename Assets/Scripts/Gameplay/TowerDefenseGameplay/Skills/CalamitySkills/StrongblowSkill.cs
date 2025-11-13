using System.Collections;
using UnityEngine;

namespace TBT.Gameplay.TowerDefenseGameplay.Skills.CalamitySkills
{
    public class StrongblowSkill : Skill
    {
        [SerializeField] private GameObject damageZonePrefab;
        [SerializeField] private Sprite strongblowSprite;

        public override void LaunchSkill(Vector3 position)
        {
            base.LaunchSkill(position);
            Debug.Log("lauchSkill Blow done");
            GameObject damageZoneObject = Instantiate(damageZonePrefab, transform);
            DamageZone damageZone = damageZoneObject.GetComponent<DamageZone>();
            damageZoneObject.GetComponent<SpriteRenderer>().sprite = strongblowSprite;
            damageZoneObject.transform.localScale = new Vector3(data.size, data.size, 1);
            damageZoneObject.transform.position = position;
            damageZone.SetDamage(data.damages);
            StartCoroutine(DamageLifeTime(damageZoneObject));
            Debug.Log("Coroutine Blow done");
            SkillPlayedEvent();
        }

        private IEnumerator DamageLifeTime(GameObject objectToDestroy)
        {
            yield return new WaitForSeconds(data.duration);
            Destroy(objectToDestroy);
            Debug.Log("Blow destroy");
        }
    }
}
