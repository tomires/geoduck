using UnityEngine;
using System.Linq;

namespace Geoduck
{
    public class PinSelector : MonoSingleton<PinSelector>
    {
        public Pin SelectedPin { get; private set; }

        void Update()
        {
            if (Input.GetMouseButtonDown(0) || Input.touchCount == 1)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, int.MaxValue, LayerMask.GetMask("Pin")))
                {
                    SelectedPin = hit.transform.GetComponent<Pin>();
                    CacheDetailsPanel.Instance.ShowCacheDetails(SelectedPin.Gpx);
                    HighlightPin();
                }
            }
        }

        private void HighlightPin()
        {
            FindObjectsOfType<Pin>().ToList().ForEach(p => p.SetColor(false));
            SelectedPin.SetColor(true);
        }
    }
}
