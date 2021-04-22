using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geoduck
{
    [RequireComponent(typeof(LineRenderer))]
    public class ProximityLine : MonoSingleton<ProximityLine>
    {
        private LineRenderer _line;

        void Start()
        {
            _line = GetComponent<LineRenderer>();
        }
        void Update()
        {
            if (PinSelector.Instance.SelectedPin == null
                || LocationDot.Instance == null)
                return;

            var dotPosition = LocationDot.Instance.transform.position;
            dotPosition.y = 0.5f;
            var pinPosition = PinSelector.Instance.SelectedPin.transform.position;
            pinPosition.y = 0.5f;
            _line.SetPositions(new Vector3[] { dotPosition, pinPosition });
        }
    }
}
