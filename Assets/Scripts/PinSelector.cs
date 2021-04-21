using UnityEngine;
using System.Linq;

namespace Geoduck
{
    public class PinSelector : MonoSingleton<PinSelector>
    {
        void Update()
        {
            if (Input.GetMouseButtonDown(0) || Input.touchCount == 1)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, int.MaxValue, LayerMask.GetMask("Pin")))
                {
                    var pin = hit.transform.GetComponent<Pin>();
                    CacheDetailsPanel.Instance.ShowCacheDetails(pin.Gpx);
                    HighlightPin(pin);
                }
            }
        }

        private void HighlightPin(Pin selectedPin)
        {
            FindObjectsOfType<Pin>().ToList().ForEach(p => p.SetColor(false));
            selectedPin.SetColor(true);
        }
    }
}
