using System;
using System.Collections;
using TBT.Core.Data.AudioData;
using TBT.Gameplay.Audio;
using UnityEngine;

namespace TBT.Gameplay.MapGameplay
{
    public class MapCarriage : MonoBehaviour
    {
        public MapPoint currentPoint { get; private set; }
        private float speed = 3;
        bool canMove = true;
        public event Action<MapCarriage, PointTypes> DestinationReached;

        public void Move(MapPoint destinationPoint, PointTypes pointType)
        {
            currentPoint = destinationPoint;
            StartCoroutine(MoveToDestination(destinationPoint.gameObject.transform.position, pointType));
        }

        IEnumerator MoveToDestination(Vector3 destinationPoint, PointTypes pointType)
        {
            if (canMove)
            {
                canMove = false;
                AudioManager.Instance.PlaySound(AudioName.moveCarriage);
                while (Vector3.Distance(transform.position, destinationPoint) > 0.01f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, destinationPoint, Time.deltaTime * speed);
                    yield return null;
                }
                DestinationReached?.Invoke(this, pointType);
                HighlightDestinations();
                canMove = true;
            }
            yield return null;
        }

        public void HighlightDestinations()
        {
            if (currentPoint.followers.Count > 0)
            {
                foreach (MapPoint possibleDestinations in currentPoint.followers)
                {
                    possibleDestinations.GetHighlighted();
                }
            }
            
        }

        public void SetStartPoint(MapPoint startPoint)
        {
            currentPoint = startPoint;
            transform.position = currentPoint.gameObject.transform.position;
        }

        public bool CheckIfCanGoTo(MapPoint destinationPoint)
        {
            return currentPoint.followers.Contains(destinationPoint);
        }
        
        
    }
}
