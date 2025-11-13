using System.Collections;
using UnityEngine;

namespace TBT.Gameplay.TowerDefenseGameplay.Skills.IzheSkills
{
    public class MolotovSkill : Skill
    {
        [SerializeField] private GameObject damageZonePrefab;
        [SerializeField] private Sprite molotovSprite;

        public override void LaunchSkill(Vector3 position)
        {
            base.LaunchSkill(position);
            Debug.Log("lauchSkill Molotov done");
            GameObject damageZoneObject = Instantiate(damageZonePrefab, transform);
            DamageZone damageZone = damageZoneObject.GetComponent<DamageZone>();
            damageZoneObject.GetComponent<SpriteRenderer>().sprite = molotovSprite;
            damageZoneObject.transform.localScale = new Vector3(data.size, data.size, 1);
            damageZoneObject.transform.position = position;
            damageZone.SetDamage(data.damages);
            StartCoroutine(DamageLifeTime(damageZoneObject));
            Debug.Log("Coroutine Molotov done");
            SkillPlayedEvent();
            Debug.Log("Molotov finished");
        }

        private IEnumerator DamageLifeTime(GameObject objectToDestroy)
        {
            yield return new WaitForSeconds(data.duration);
            Destroy(objectToDestroy);
            Debug.Log("Molotov destroyed");
        }
    }
}
