using UnityEngine;

namespace TBT.Core.Data.TowerDefenseData
{
    [CreateAssetMenu(fileName = "TowerDefenseData", menuName = "Scriptable Objects/TowerDefenseData")]
    public class TowerDefenseData : ScriptableObject
    {
        public int minWaves, maxWaves;
        public int minEnnemies, maxEnnemies;
        public int addedEnnemiesByWaves, addedEnnemiesByRounds;
    }
}
