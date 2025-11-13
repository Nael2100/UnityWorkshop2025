using System;
using System.Collections.Generic;
using System.Drawing;
using TBT.Gameplay.MapGameplay;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Color = UnityEngine.Color;

namespace TBT.Gameplay
{
    public class MapPoint : MonoBehaviour, IPointerDownHandler
    {
        public PointTypes type { get; private set; }
        public List<MapPoint> followers; 
        public List<MapPoint> predecessors; //{ get; private set; }
        public event Action<MapPoint, PointTypes> DestinationClicked;
        [SerializeField] private Sprite availableEncounterSprite;
        [SerializeField] private Sprite availableTowerDefenseSprite;
        [SerializeField] private Sprite availableEndSprite;
        [SerializeField] private Sprite towerDefenseSprite;
        [SerializeField] private Sprite encounterSprite;
        [SerializeField] private Sprite unavailableEncounterSprite;
        [SerializeField] private Sprite unavailableTowerDefenseSprite;
        [SerializeField] private Sprite startSprite;
        [SerializeField] private Sprite endSprite;
        private Sprite baseSprite;
        private Sprite availableSprite;
        private Sprite unavailableSprite;

        public bool highlighted { get; private set; } = false;
        public void SetFollower(MapPoint neighbor)
        {
            followers.Add(neighbor);
        }

        public void SetPredecessor(MapPoint neighbor)
        {
            predecessors.Add(neighbor);
        }

        public void GetHighlighted()
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = availableSprite;
            highlighted = true;
        }

        public void GetUnHighlighted()
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = unavailableSprite;
            highlighted = false;
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            OnDestinationClicked(this);
        }

        protected virtual void OnDestinationClicked(MapPoint obj)
        {
            DestinationClicked?.Invoke(obj,type);
        }

        public void SetType(PointTypes pointType)
        {
            this.type = pointType;
            if (type == PointTypes.towerDefense)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = towerDefenseSprite;
                baseSprite = towerDefenseSprite;
                availableSprite = availableTowerDefenseSprite;
                unavailableSprite = unavailableTowerDefenseSprite;
            }
            else if (type == PointTypes.encounter)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = encounterSprite;
                baseSprite = encounterSprite;
                availableSprite = availableEncounterSprite;
                unavailableSprite = unavailableEncounterSprite;
            }
            else if (type == PointTypes.start)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = startSprite;
                baseSprite = startSprite;
                availableSprite = startSprite;
            }
            else if (type == PointTypes.end)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = endSprite;
                baseSprite = endSprite;
                availableSprite = availableEndSprite;
            }
            gameObject.GetComponent<SpriteRenderer>().sprite = baseSprite;
        }
    }
}
