using System.Collections.Generic;
using UnityEngine;

namespace TBT.Gameplay.MapGameplay
{
    public class MapManager : MonoBehaviour
    {
        [SerializeField] private GameObject startingPoint;
        [SerializeField] private int mapSize = 10;
        [SerializeField] private GameObject mapPointPrefab;
        [SerializeField] private MapCarriage mapCarriage;
        private List<MapPoint> mapPoints;
        private float pointsDistance = 5;
        private float maxBranch = 3;

        void Start()
        {
            mapPoints = new List<MapPoint>();
            CreateMapPoints();
            mapCarriage.HighlighDestinations();
        }

        void CreateMapPoints()
        {
            Vector3 pointPosition = startingPoint.transform.position;
            List<GameObject> pointsToBranch = new List<GameObject>();
            GameObject baseMapPoint = Instantiate(mapPointPrefab, pointPosition, Quaternion.identity);
            mapPoints.Add(baseMapPoint.GetComponent<MapPoint>());
            pointsToBranch.Add(baseMapPoint);
            while (mapPoints.Count < mapSize && pointsToBranch.Count >0)
            {
                GameObject pointToBranch = pointsToBranch[0];
                pointsToBranch.Remove(pointsToBranch[0]);
                for (int i = 0; i < Random.Range(1,maxBranch); i++)
                {
                    
                    pointPosition = pointToBranch.transform.position ; 
                    GameObject mapPointObject = Instantiate(mapPointPrefab, pointPosition, Quaternion.identity);
                    MapPoint mapPoint = mapPointObject.GetComponent<MapPoint>();
                    pointToBranch.GetComponent<MapPoint>().SetNeighbor(mapPoint);
                    pointsToBranch.Add(mapPointObject);
                    mapPoints.Add(mapPoint);
                    float newY = pointPosition.y+Random.Range(-pointsDistance, pointsDistance);
                    float newX = pointPosition.x+Mathf.Abs(newY);
                    mapPoint.transform.position = new Vector3(newX, newY,0);
                }
            }
            mapCarriage.SetStartPoint(baseMapPoint.GetComponent<MapPoint>());
        }
    }
}
