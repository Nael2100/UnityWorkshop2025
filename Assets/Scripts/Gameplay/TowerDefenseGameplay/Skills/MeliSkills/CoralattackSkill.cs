using System.Collections;
using UnityEngine;

namespace TBT.Gameplay.TowerDefenseGameplay.Skills.MeliSkills
{
    public class CoralattackSkill : Skill
    {
        [SerializeField] private GameObject damageZonePrefab;
        [SerializeField] private Sprite coralattackSprite;

        public override void LaunchSkill(Vector3 position)
        {
            base.LaunchSkill(position);
            GameObject damageZoneObject = Instantiate(damageZonePrefab, transform);
            DamageZone damageZone = damageZoneObject.GetComponent<DamageZone>();
            damageZoneObject.GetComponent<SpriteRenderer>().sprite = coralattackSprite;
            damageZone.transform.localScale = new Vector3(data.size, data.size, 1);
            damageZoneObject.transform.position = position;
            damageZone.SetDamage(data.damages);
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
