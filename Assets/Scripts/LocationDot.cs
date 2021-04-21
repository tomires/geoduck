using Mapbox.Unity.Map;
using Mapbox.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geoduck
{
    [RequireComponent(typeof(Renderer))]
    public class LocationDot : MonoSingleton<LocationDot>
    {
        [SerializeField] private AbstractMap _map;
        private Renderer _renderer;

        IEnumerator Start()
        {
            _renderer = GetComponent<Renderer>();
            _renderer.enabled = false;

            Input.location.Start();
            while (Input.location.status == LocationServiceStatus.Initializing)
            {
                yield return new WaitForSeconds(1f);
            }

            if (Input.location.status == LocationServiceStatus.Running)
            {
                _renderer.enabled = true;
                _map.SetCenterLatitudeLongitude(GetLocation());
                StartCoroutine(UpdateLocation());
            }
        }

        private IEnumerator UpdateLocation()
        {
            while(true)
            {
                if(Input.location.status == LocationServiceStatus.Running)
                {
                    transform.position = _map.GeoToWorldPosition(GetLocation());
                }
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
