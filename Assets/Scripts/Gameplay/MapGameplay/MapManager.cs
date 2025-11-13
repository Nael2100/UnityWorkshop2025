using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TBT.Gameplay.MapGameplay
{
    public class MapManager : MonoBehaviour
    {
        [SerializeField] private GameObject startingPoint;
        private int[] mapSize;
        [SerializeField] private GameObject mapPointPrefab;
        [SerializeField] private GameObject mapPathPrefab;
        [SerializeField] private MapCarriage mapCarriage;
        [SerializeField] private int roundsCount;
        [SerializeField] GameModeManager gameModeManager ;
        private List<MapPoint> mapPoints;
        private float pointsDistance ;
        private int currentRound = 0;
        private List<List<MapPoint>> mapPointsByRows;
        

        private void OnDisable()
        {
            foreach (MapPoint point in mapPoints)
            {
                point.DestinationClicked -= MoveCarriage;
                mapCarriage.DestinationReached -= EnterGameMode;
            }
        }

        void Start()
        {
            float roundsCountForCalc = roundsCount+1 ;
            pointsDistance =21/roundsCountForCalc;
            mapPoints = new List<MapPoint>();
            mapSize = new int[roundsCount];
            mapSize[0] = 1;
            for (int i = 1; i < roundsCount-1; i++)
            {
                mapSize[i] = Random.Range(2, 5);
            }
            mapSize[roundsCount-1] = 1;
            CreateMapPoints();
            SetPointsTypes();
            mapCarriage.HighlightDestinations();
            mapCarriage.DestinationReached += EnterGameMode;
        }

        void CreateMapPoints()
        {
            mapPointsByRows = new List<List<MapPoint>>();
            for (int i = 0; i < roundsCount; i++)
            {
                mapPointsByRows.Add(new List<MapPoint>());
            }
            int count = 0;
            foreach (int countThisRow in mapSize)
            {
                count+= countThisRow;
            }
                                                                              
            for (int i = 0; i < count; i++)
            {
                GameObject mapPointObject = Instantiate(mapPointPrefab, startingPoint.transform.position, Quaternion.identity);
                MapPoint mapPoint = mapPointObject.GetComponent<MapPoint>();
                mapPoint.DestinationClicked += MoveCarriage;
                mapPoints.Add(mapPoint);
            }

            int pointIndex = 0;
            while (pointIndex < mapPoints.Count)
            {
                
                int currentTestingRow = roundsCount-1;
                bool freeSpace = false;
                while (freeSpace == false)
                {
                    if (mapPointsByRows[currentTestingRow].Count < mapSize[currentTestingRow])
                    {
                        freeSpace = true;
                        MapPoint currentMapPointSet = mapPoints[pointIndex];
                        mapPointsByRows[currentTestingRow].Add(currentMapPointSet);
                        if (currentTestingRow == 0)
                        {
                            int linksCount = mapPointsByRows[currentTestingRow+1].Count;
                            for (int i = 0; i < linksCount; i++)
                            {
                                SetLink(currentMapPointSet,mapPointsByRows[currentTestingRow + 1][i]);
                            }
                        }
                        else if (currentTestingRow < roundsCount - 1)
                        {
                            int linksCount = Random.Range(1, mapPointsByRows[currentTestingRow+1].Count);
                            for (int i = 0; i < linksCount; i++)
                            {
                                SetLink(currentMapPointSet, mapPointsByRows[currentTestingRow + 1][Random.Range(0,mapSize[currentTestingRow + 1])]);
                            }
                        }

                        Vector3 newPosition = startingPoint.transform.position;
                        float offset = 0.6f;
                        newPosition.x += currentTestingRow * pointsDistance+Random.Range(-offset,offset);
                        newPosition.y -= ((10/(mapSize[currentTestingRow]+1))*mapPointsByRows[currentTestingRow].Count)+Random.Range(-offset,offset);
                        currentMapPointSet.gameObject.transform.position = newPosition;
                        pointIndex++;
                    }
                    else
                    {
                        currentTestingRow--;
                    }
                }
            }
            CheckAllLinked(mapPointsByRows);
            mapCarriage.SetStartPoint(mapPointsByRows[0][0].GetComponent<MapPoint>());
        }

        private void MoveCarriage(MapPoint obj, PointTypes pointType)
        {
            
            if (mapCarriage.CheckIfCanGoTo(obj))
            {
                foreach (MapPoint mapPoint in mapPointsByRows[currentRound])
                {
                    mapPoint.GetUnHighlighted();
                }
                currentRound++;
                mapCarriage.Move(obj, pointType);
                foreach (MapPoint mapPoint in mapPointsByRows[currentRound])
                {
                    if (mapPoint !=  mapCarriage.currentPoint)
                    {
                        mapPoint.GetUnHighlighted();
                    }
                }
                
            }
            
        }

        private void CheckAllLinked(List<List<MapPoint>> mapPoints)
        {
            for (int i = 0; i < mapPoints.Count; i++)
            {
                foreach (MapPoint point in mapPoints[i])
                {
                    if (point.predecessors.Count == 0)
                    {
                        if (i > 0)
                        {
                            int neighborIndex = Random.Range(0, mapPoints[i - 1].Count - 1);
                           SetLink(mapPoints[i - 1][neighborIndex],point);
                        }         
                    }
                }
            }
        }

        private void SetLink(MapPoint first, MapPoint second)
        {
            first.SetFollower(second);
            second.SetPredecessor(first);
            GameObject pathObject = Instantiate(mapPathPrefab, first.transform.position, Quaternion.identity);
            pathObject.GetComponent<MapPath>().SetUp(first.gameObject, second.gameObject);
        }

        void EnterGameMode(MapCarriage carriage, PointTypes pointType)
        {
            if (pointType == PointTypes.towerDefense)
            {
                gameModeManager.EnterTowerDefenseMode();
            }
            else if (pointType == PointTypes.encounter)
            {
                gameModeManager.EnterEncounterMode();
            }
            else if (pointType == PointTypes.end)
            {
                gameModeManager.EnterTowerDefenseModeFinal();
            }
        }
        
        void SetPointsTypes()
        {
            foreach (MapPoint mapPoint in mapPoints)
            {
                int randChoice = Random.Range(0, 3);
                if (randChoice == 0)
                {
                    mapPoint.SetType(PointTypes.towerDefense);
                }
                else if (randChoice >= 1)
                {
                    mapPoint.SetType(PointTypes.encounter);
                }
            }
            mapPoints[mapPoints.Count-1].SetType(PointTypes.start);
            mapPoints[0].SetType(PointTypes.end);
            foreach (MapPoint mapPoint in mapPoints)
            {
                if (mapPoint.type == PointTypes.encounter)
                {
                    foreach (MapPoint followerPoint in mapPoint.followers)
                    {
                        if (followerPoint.type == PointTypes.encounter)
                        {
                            followerPoint.SetType(PointTypes.towerDefense);
                        }
                    }
                }
            }
        }
    }
}
