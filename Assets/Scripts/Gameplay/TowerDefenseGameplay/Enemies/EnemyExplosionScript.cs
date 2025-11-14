using System.Collections;
using UnityEngine;

namespace TBT.Gameplay.TowerDefenseGameplay.Enemies
{
    public class EnemyExplosionScript : MonoBehaviour
    {
        private void OnEnable()
        {
            StartCoroutine(Lifetime());
        }

        IEnumerator Lifetime()
        {
            yield return new WaitForSeconds(2f);
            Destroy(gameObject);
        }
    }
}
