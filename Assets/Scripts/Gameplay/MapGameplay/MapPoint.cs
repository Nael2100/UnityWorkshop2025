using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TBT.Gameplay
{
    public class MapPoint : MonoBehaviour, IPointerDownHandler
    {
        public List<MapPoint> neighbors { get; private set; }

        public void SetNeighbor(MapPoint neighbor)
        {
            neighbors.Add(neighbor);
        }

        public void GetHighlighted()
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("OnPointerDown");
        }
        
    }
}
