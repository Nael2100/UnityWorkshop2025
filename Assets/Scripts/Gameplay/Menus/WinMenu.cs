using UnityEngine;
using UnityEngine.SceneManagement;

namespace TBT.Gameplay.Menus
{
    public class WinMenu : MonoBehaviour
    {
        public void RestartGame()
        {
            SceneManager.LoadScene(1);
        }
    }
}
