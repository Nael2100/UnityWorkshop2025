using System;
using TBT.Core.Data.SkillsData;
using TBT.Gameplay.TowerDefenseUI;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
namespace TBT.Gameplay.TowerDefenseGameplay
{
    public class Skill : MonoBehaviour, IPointerClickHandler

    {
        [SerializeField] private SkillDataScript data;
        public Sprite sprite { get; private set; }
        public string name { get; private set; } = "nom";
        [SerializeField] private bool needsClick;
        public event Action SkillPlayed;
        [SerializeField] private CircleCollider2D rangeCollider2D; 
        private float range;
        [SerializeField] private GameObject areaCursor;
        private bool canLaunch = false;
        private float damage;
        private float size;
   
        private Camera cam;
        public int ressourcesCost { get; private set; }
        
        

        private void OnEnable()
        {
            cam = Camera.main;
            sprite = data.sprite;
            name = data.name;
            damage = data.damages;
            size = data.size;
            range = data.range;
            ressourcesCost = data.ressourcesCost;
            areaCursor.transform.localScale *= size;
            areaCursor.SetActive(false);
            rangeCollider2D.radius = range;
            rangeCollider2D.enabled = false;
        }

        public void Play()
        {

            if (needsClick)
            {
                areaCursor.SetActive(true);
                canLaunch = true;
                rangeCollider2D.enabled = true;
            }
            else
            {
                ApplyEffects();
                SkillPlayed?.Invoke();
            }
        }

        public virtual void ApplyEffects()
        {
            
        }

        public virtual void LaunchSkill()
        {
            canLaunch = false;
            areaCursor.SetActive(false);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (canLaunch)
            { 
                LaunchSkill();
            }
        }
        
        private void Update()
        {
            if (canLaunch && areaCursor.activeInHierarchy)
            {
                Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();

                Vector3 worldPosition =
                    cam.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y,
                        cam.nearClipPlane));
                areaCursor.transform.position = worldPosition;
            }
        }

        protected void SkillPlayedEvent()
        {
            SkillPlayed?.Invoke();
        }
    }


}