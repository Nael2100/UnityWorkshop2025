using System.Collections;
using UnityEngine;

namespace TBT.Gameplay.MapGameplay
{
    public class MapCarriage : MonoBehaviour
    {
        private MapPoint currentPoint;
        private float speed;
        

        public void Move(MapPoint destinationPoint)
        {
            currentPoint = destinationPoint;
            StartCoroutine(MoveToDestination(destinationPoint.gameObject.transform.position));
        }

        IEnumerator MoveToDestination(Vector3 destinationPoint)
        {
            while (Vector3.Distance(transform.position, destinationPoint) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, destinationPoint, Time.deltaTime * speed);
            }
            yield return null;
        }

        public void HighlighDestinations()
        {
            if (currentPoint.neighbors.Count > 0)
            {
                foreach (MapPoint possibleDestinations in currentPoint.neighbors)
                {
                    possibleDestinations.GetHighlighted();
                }
            }
            
        }

        public void SetStartPoint(MapPoint startPoint)
        {
            currentPoint = startPoint;
        }
    }
}
