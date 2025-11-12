using System.Collections;
using UnityEngine;

namespace TBT.Gameplay.TowerDefenseGameplay.Skills.LarkSkills
{
    public class PunkrecitalSkill : Skill
    {
        [SerializeField] private GameObject healZonePrefab;
        [SerializeField] private Sprite healSprite;
        
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
            healZoneObject.GetComponent<SpriteRenderer>().sprite = healSprite;
            healZoneObject.transform.localScale = new Vector3(data.size, data.size, 1);
            healZoneObject.transform.position = transform.position;
            StartCoroutine(HealLifeTime(healZoneObject));
        }
        private IEnumerator HealLifeTime(GameObject objectToDestroy)
        {
            yield return new WaitForSeconds(data.duration);
            Destroy(objectToDestroy);
            SkillPlayedEvent();
        }
    }
}
