using Mapbox.Unity.Map;
using Mapbox.Utils;
using System.Collections;
using UnityEngine;

namespace Geoduck
{
    [RequireComponent(typeof(Renderer))]
    public class LocationDot : MonoSingleton<LocationDot>
    {
        public Vector2d CurrentLocation { get; private set; }
        [SerializeField] private AbstractMap _map;
        private Renderer _renderer;

        IEnumerator Start()
        {
            _renderer = GetComponent<Renderer>();
            _renderer.enabled = false;

            Input.location.Start();

            while (Input.location.status == LocationServiceStatus.Initializing)
                yield return new WaitForSeconds(1f);

            if (Input.location.status == LocationServiceStatus.Running)
            {
                _renderer.enabled = true;
                CenterMapOnLocation();
                StartCoroutine(UpdateLocation());
            }
        }

        void Update()
        {
            transform.position = _map.GeoToWorldPosition(CurrentLocation);
        }

        public void CenterMapOnLocation()
        {
            _map.SetCenterLatitudeLongitude(GetLocation());
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
