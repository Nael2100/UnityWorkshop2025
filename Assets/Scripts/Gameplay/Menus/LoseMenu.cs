using UnityEngine;
using UnityEngine.SceneManagement;

namespace TBT.Gameplay.Menus
{
    public class LoseMenu : MonoBehaviour
    {
        public void RestartGame()
        {
            SceneManager.LoadScene(0);
        }
    }
}
