using UnityEngine;

namespace TBT.Gameplay.TowerDefenseGameplay.Skills
{
    public class TestSkill : Skill
    {
        public override void ApplyEffects()
        {
            base.ApplyEffects();
        }

        public override void LaunchSkill()
        {
            base.LaunchSkill();
            SkillPlayedEvent();
        }
    }
}
