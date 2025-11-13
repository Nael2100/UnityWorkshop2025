using UnityEngine;

namespace TBT.Gameplay.TowerDefenseGameplay.Skills
{
    public class HealingZone : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer skillIconRenderer;
        
        public void SetSprites(float scale, Sprite areaSprite, Sprite skillIcon)
        {
            gameObject.transform.localScale = new Vector3(scale, scale, 1);
            gameObject.GetComponent<SpriteRenderer>().sprite = areaSprite;
            skillIconRenderer.sprite = skillIcon;
        }
        

        private void Update()
        {
            float upSpeed = 0.5f;
            float fadingSpeed = 0.5f;
            skillIconRenderer.transform.position += Vector3.up * (upSpeed * Time.deltaTime);
            var color = skillIconRenderer.color;
            color.a -= Time.deltaTime * fadingSpeed;
            skillIconRenderer.color = color;
        }
    }
}
