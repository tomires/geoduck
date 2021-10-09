using UnityEngine;
using System.Linq;

namespace Geoduck
{
    public class PinSelector : MonoSingleton<PinSelector>
    {
        public Pin SelectedPin { get; private set; }

        void Update()
        {
            if (Input.GetMouseButtonDown(0)
                || (Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Began))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (!Utils.IsMouseOverGUI()
                    && Physics.Raycast(ray, out hit, int.MaxValue, LayerMask.GetMask("Pin")))
                {
                    SelectedPin = hit.transform.GetComponent<Pin>();
                    CacheDetailsPanel.Instance.ShowCacheDetails(SelectedPin.Gpx);
                    HighlightPin();
                }
            }
        }

        private void HighlightPin()
        {
            FindObjectsOfType<Pin>().ToList().ForEach(p => p.SetIcon(false));
            SelectedPin.SetIcon(true);
        }
    }
}
