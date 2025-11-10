using System.Collections;
using UnityEngine;

namespace TBT.Gameplay.TowerDefenseGameplay.Skills
{
    public class TestSkill : Skill
    {
        public override void ApplyEffects()
        {
            base.ApplyEffects();
            StartCoroutine(ApplyEffectsDelay());
        }

        private IEnumerator ApplyEffectsDelay()
        {
            yield return null;
            SkillPlayedEvent();
        }

        public override void LaunchSkill()
        {
            base.LaunchSkill();
            SkillPlayedEvent();
        }
    }
}
