using Mapbox.Unity.Map;
using Mapbox.Utils;
using System.Collections;
using UnityEngine;

namespace Geoduck
{
    public class LocationDot : MonoSingleton<LocationDot>
    {
        public Vector2d CurrentLocation { get; private set; }
        [SerializeField] private GameObject graphic;
        [SerializeField] private AbstractMap map;

        IEnumerator Start()
        {
            graphic.SetActive(false);

            Input.location.Start();

            while (Input.location.status == LocationServiceStatus.Initializing)
                yield return new WaitForSeconds(1f);

            if (Input.location.status == LocationServiceStatus.Running)
            {
                graphic.SetActive(true);
                CenterMapOnLocation();
                StartCoroutine(UpdateLocation());
            }
        }

        void Update()
        {
            transform.position = map.GeoToWorldPosition(CurrentLocation);
        }

        public void CenterMapOnLocation()
        {
            map.SetCenterLatitudeLongitude(GetLocation());
            map.UpdateMap();
        }

        private IEnumerator UpdateLocation()
        {
            while(true)
            {
                if(Input.location.status == LocationServiceStatus.Running)
                    CurrentLocation = GetLocation();
                yield return new WaitForSeconds(Constants.locationUpdateFrequency);
            }
        }

        private Vector2d GetLocation()
        {
            var lastData = Input.location.lastData;
            var location = new Vector2d(lastData.latitude, lastData.longitude);
            return location;
        }
    }
}
