using UnityEngine;

namespace TBT.Gameplay
{
    public class MapPath : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer; 
        private GameObject start;
        private GameObject end;

        public void SetUp(GameObject startObj, GameObject endObj)
        {
            lineRenderer.positionCount = 2;
            start = startObj;
            end = endObj;
            
        }

        void Update()
        {
            lineRenderer.SetPosition(0, start.transform.position);
            lineRenderer.SetPosition(1, end.transform.position);
        }
        
    }
}
