using Mapbox.Utils;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Mapbox.Unity.Map;

namespace Geoduck
{
    [RequireComponent(typeof(AbstractMap))]
    public class PinSpawner : MonoSingleton<PinSpawner>
    {
        [SerializeField] private GameObject pinPrefab;
        private List<GameObject> _spawnedPins = new List<GameObject>();
        private Vector2d[] _coordinates;
        private AbstractMap _map;

        void Start()
        {
            _map = GetComponent<AbstractMap>();
            RefreshPins();
        }

        public void RefreshPins()
        {
            foreach (var pin in _spawnedPins)
                Destroy(pin);
            StartCoroutine(SpawnPins());
        }

        private IEnumerator SpawnPins()
        {
            Directory.CreateDirectory(Constants.cacheDirectory);
            var gpxList = Directory.GetFiles(Constants.cacheDirectory);
            _spawnedPins = new List<GameObject>();
            _coordinates = new Vector2d[gpxList.Length];

            int i = 0;
            foreach(var path in gpxList)
            {
                var gpx = Utils.LoadGpxByPath(path);
                _coordinates[i] = gpx.Coordinates;
                var pin = Instantiate(pinPrefab);
                pin.transform.localPosition = _map.GeoToWorldPosition(_coordinates[i], true);
                pin.GetComponent<Pin>().Gpx = gpx;
                _spawnedPins.Add(pin);
                yield return null;
                i++;
            }
        }

        private void Update()
        {
            int count = _spawnedPins.Count;
            for (int i = 0; i < count; i++)
            {
                var spawnedPin = _spawnedPins[i];
                var coordinate = _coordinates[i];
                spawnedPin.transform.localPosition = _map.GeoToWorldPosition(coordinate, true);
            }
        }
    }
}
