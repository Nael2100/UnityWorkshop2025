using System;
using TBT.Core.Data.AudioData;
using TBT.Core.Data.SkillsData;
using TBT.Gameplay.Audio;
using TBT.Gameplay.TowerDefenseUI;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Cursor = UnityEngine.WSA.Cursor;

namespace TBT.Gameplay.TowerDefenseGameplay
{
    public class Skill : MonoBehaviour, IPointerClickHandler

    {
        [SerializeField] protected SkillDataScript data;
        public Sprite iconSprite { get; private set; }
        public Sprite areaSprite { get; private set; }
        public string name { get; private set; } = "nom";
        [SerializeField] private int needsClick;
        public event Action SkillPlayed;
        [SerializeField] private CircleCollider2D rangeCollider2D; 
        private float range;
        private GameObject areaCursor;
        protected bool canLaunch;
        protected float damage;
        protected float size;
        protected float duration;
        protected AudioName audioToPlay;

        private int clicksLefts;
        private Camera cam;
        public int ressourcesCost { get; private set; }
        public event Action<bool> EnemiesNeedToCollide;
        
        private void OnEnable()
        {
            cam = Camera.main;
            areaCursor = GameObject.Find("Cursor");
            iconSprite = data.iconSprite;
            areaSprite = data.areaSprite;
            name = data.name;
            damage = data.damages + TowerDefenseManager.Instance.playerCarriage.bonusDamage;
            size = data.size;
            range = data.range;
            duration = data.duration;
            ressourcesCost = data.ressourcesCost;
            audioToPlay = data.audioPlayed;
            PutCursorAway();
            rangeCollider2D.radius = range;
            rangeCollider2D.enabled = false;
        }

        private void OnDisable()
        {
            PutCursorAway();
        }

        public void Play(bool firstShot = true)
        {
            if (firstShot)
            {
               clicksLefts = needsClick; 
            }
            if (clicksLefts>0)
            {
                clicksLefts--;
                canLaunch = true;
                rangeCollider2D.enabled = true;
                EnemiesNeedToBeDamagedEvent(false);
            }
            else
            {
                ApplyEffects();
            }
        }

        public virtual void ApplyEffects()
        {
            EnemiesNeedToBeDamagedEvent(true);
            AudioManager.Instance.PlaySound(audioToPlay);
        }

        public virtual void LaunchSkill(Vector3 position)
        {
            EnemiesNeedToBeDamagedEvent(true);
            canLaunch = false;
            PutCursorAway();
            rangeCollider2D.enabled = false;
            if (audioToPlay != null)
            {
                AudioManager.Instance.PlaySound(audioToPlay);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (canLaunch)
            { 
                Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
                Vector3 worldPosition =
                    cam.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y,
                        cam.nearClipPlane));
                LaunchSkill(worldPosition);
            }
        }
        
        private void Update()
        {
            if (canLaunch && areaCursor.activeInHierarchy)
            {
                areaCursor.transform.localScale = new Vector3(size, size, 1);
                Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
                Vector3 worldPosition =
                    cam.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y,
                        cam.nearClipPlane));
                areaCursor.transform.position = worldPosition;
                if (Vector2.Distance(areaCursor.transform.position,
                        TowerDefenseManager.Instance.playerCarriage.gameObject.transform.position) > range)
                {
                    areaCursor.GetComponent<SpriteRenderer>().color = Color.red;
                }
                else
                {
                    areaCursor.GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
        }

        protected void SkillPlayedEvent()
        {
            if (clicksLefts == 0)
            {
                SkillPlayed?.Invoke();
            }
            else
            {
                Play(false);
            }
        }

        protected void EnemiesNeedToBeDamagedEvent(bool isDamaged)
        {
            EnemiesNeedToCollide?.Invoke(isDamaged);
        }

        public void AddBonusDamage(float newBonus)
        {
            damage = data.damages + newBonus;
        }

        protected void PutCursorAway()
        {
            
            if (areaCursor != null)
            {
                areaCursor.transform.position = new Vector3(1000,1000,1000);
            }
        }
    }


}