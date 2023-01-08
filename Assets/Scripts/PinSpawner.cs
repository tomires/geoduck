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
        [SerializeField] private Pin pinPrefab;
        
        public int PinCount => _spawnedPins.Count;

        private Dictionary<GpxStructure, Pin> _spawnedPins
            = new Dictionary<GpxStructure, Pin>();
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
                Destroy(pin.Value.gameObject);
            _spawnedPins.Clear();
            StartCoroutine(SpawnPins());
        }

        public void RefreshPin(GpxStructure gpx)
        {
            _spawnedPins[gpx].SetIcon(true);
        }

        private IEnumerator SpawnPins()
        {
            Directory.CreateDirectory(Constants.cacheDirectory);
            var gpxList = Directory.GetFiles(Constants.cacheDirectory);
            _coordinates = new Vector2d[gpxList.Length];

            int i = 0;
            foreach(var path in gpxList)
            {
                var gpx = Utils.LoadGpxByPath(path);
                _coordinates[i] = gpx.Coordinates;
                var pin = Instantiate(pinPrefab);
                pin.transform.localPosition = _map.GeoToWorldPosition(_coordinates[i], true);
                pin.Gpx = gpx;
                _spawnedPins.Add(gpx, pin);
                yield return null;
                i++;
            }
        }

        private void Update()
        {
            foreach (var pin in _spawnedPins)
                pin.Value.transform.localPosition = _map.GeoToWorldPosition(pin.Key.Coordinates, true);
        }
    }
}
