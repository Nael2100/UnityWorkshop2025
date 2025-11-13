using TBT.Gameplay.TowerDefenseGameplay;
using UnityEngine;

namespace TBT.Gameplay.CardsSystem.CardsVariants
{
    public class SpadeDraw : Card
    {
        protected override void ApplyEffects()
        {
            
            base.ApplyEffects();
            TowerDefenseManager.Instance.playerCarriage.AddBonusDamage(data.bonusDamage);
            TowerDefenseManager.Instance.enemiesManager.AddBonusSpeed(data.enemiesAddedSpeed);
        }
    }
}