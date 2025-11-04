using System;
using System.Collections.Generic;
using System.Drawing;
using TBT.Gameplay.MapGameplay;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using Color = UnityEngine.Color;

namespace TBT.Gameplay
{
    public class MapPoint : MonoBehaviour, IPointerDownHandler
    {
        public PointTypes type { get; private set; }
        public List<MapPoint> followers; 
        public List<MapPoint> predecessors; //{ get; private set; }
        public event Action<MapPoint, PointTypes> DestinationClicked;
        [SerializeField] private Color availableEncounterColor = new Color(1f, 1f, 1f, 1f);
        [SerializeField] private Color availableTowerDefenseColor = new Color(1f, 1f, 1f, 1f);
        [SerializeField] private Color availableEndColor = new Color(1f, 1f, 1f, 1f);
        [SerializeField] private Color towerDefenseColor= new Color(1f, 1f, 1f, 1f);
        [SerializeField] private Color encounterColor= new Color(1f, 1f, 1f, 1f);
        [SerializeField] private Color unavailableColor= new Color(1f, 1f, 1f, 1f);
        [SerializeField] private Color startColor= new Color(1f, 1f, 1f, 1f);
        [SerializeField] private Color endColor= new Color(1f, 1f, 1f, 1f);
        private Color baseColor;
        private Color availableColor;
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
            gameObject.GetComponent<SpriteRenderer>().color = availableColor;
            highlighted = true;
        }

        public void GetUnHighlighted()
        {
            gameObject.GetComponent<SpriteRenderer>().color = unavailableColor;
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
                gameObject.GetComponent<SpriteRenderer>().color = towerDefenseColor;
                baseColor = towerDefenseColor;
                availableColor = availableTowerDefenseColor;
            }
            else if (type == PointTypes.encounter)
            {
                gameObject.GetComponent<SpriteRenderer>().color = encounterColor;
                baseColor = encounterColor;
                availableColor = availableEncounterColor;
                
            }
            else if (type == PointTypes.start)
            {
                gameObject.GetComponent<SpriteRenderer>().color = startColor;
                baseColor = startColor;
                availableColor = startColor;
            }
            else if (type == PointTypes.end)
            {
                gameObject.GetComponent<SpriteRenderer>().color = endColor;
                baseColor = endColor;
                availableColor = availableEndColor;
            }
            gameObject.GetComponent<SpriteRenderer>().color = baseColor;
        }
    }
}
