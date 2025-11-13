using UnityEngine;
using UnityEngine.SceneManagement;

namespace TBT.Gameplay.Menus
{
    public class StartMenu : MonoBehaviour
    {
        [SerializeField] GameObject storyPanel;

        private void OnEnable()
        {
            storyPanel.SetActive(false);
        }

        public void DisplayStory()
        {
            storyPanel.SetActive(true);
            
        }
        public void EnterGame()
        {
            SceneManager.LoadSceneAsync(0);
        }
    }
}
