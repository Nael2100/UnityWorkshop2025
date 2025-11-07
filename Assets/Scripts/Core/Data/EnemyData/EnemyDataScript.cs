using UnityEngine;

namespace TBT.Core.Data.EnemyData
{
    [CreateAssetMenu(fileName = "EnemyDataScript", menuName = "Scriptable Objects/EnemyDataScript")]
    public class EnemyDataScript : ScriptableObject
    {
        public float health;
        public float range;
        public float speed;
        public float damage;
    }
}
