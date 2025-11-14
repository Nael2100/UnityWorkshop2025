using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TBT.Gameplay.Menus
{
    public class StartMenu : MonoBehaviour
    {
        [SerializeField] GameObject storyPanel;
        [SerializeField] private Image startImage;
        [SerializeField] private string[] storyText;
        [SerializeField] private TextMeshProUGUI storyTextBox;
        private int storyIndex;

        private void OnEnable()
        {
            storyPanel.SetActive(false);
            StartCoroutine(ButtonAppear(startImage));
        }

        public void StartStory()
        {
            DisplayStory();
        }

        IEnumerator StoryAppear(Image image)
        {
            float speed = 0.4f;
            var imageColor = image.color;
            imageColor.a = 1f;
            image.color = imageColor;
            while (image.color.a > 0)
            {
                var color = image.color;
                color.a = color.a - Time.deltaTime * speed;
                image.color = color;
                yield return null;
            }

            DisplayStory();
        }

        public void DisplayStory()
        {
            storyPanel.SetActive(true);
            storyTextBox.text = storyText[storyIndex];
            storyIndex++;
            if (storyIndex >= storyText.Length)
            {
                storyIndex = 0;
            }
        }

        public void EnterGame()
        {
            SceneManager.LoadSceneAsync(0);
        }

        IEnumerator ButtonAppear(Image image)
        {
            float speed = 0.4f;
            var imageColor = image.color;
            imageColor.a = 0f;
            image.color = imageColor;
            while (image.color.a < 1)
            {
                var color = image.color;
                color.a = color.a + Time.deltaTime * speed;
                image.color = color;
                yield return null;
            }
        }
    }
}
