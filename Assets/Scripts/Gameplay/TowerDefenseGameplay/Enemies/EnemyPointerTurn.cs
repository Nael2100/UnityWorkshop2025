using UnityEngine;

namespace TBT.Gameplay.TowerDefenseGameplay.Enemies
{
    public class EnemyPointerTurn : MonoBehaviour
    {
        [SerializeField] private EnemyClass enemy;
        private void OnEnable()
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            enemy.OnTurnEnded += UnSignalTurn;
            enemy.OnTurnStarted += SignalTurn;
        }
        private void SignalTurn()
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }

        private void UnSignalTurn()
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
