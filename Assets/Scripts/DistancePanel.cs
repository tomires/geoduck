using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Geoduck
{
    [RequireComponent(typeof(CanvasGroup))]
    public class DistancePanel : MonoSingleton<DistancePanel>
    {
        [SerializeField] Text distanceText;
        private CanvasGroup _panel;

        void Start()
        {
            StartCoroutine(UpdateDistance());
            _panel = GetComponent<CanvasGroup>();
        }

        private IEnumerator UpdateDistance()
        {
            while(true)
            {
                yield return new WaitForSeconds(Constants.locationUpdateFrequency);

                if (LocationDot.Instance.CurrentLocation == null
                    || PinSelector.Instance.SelectedPin == null)
                {
                    _panel.alpha = 0f;
                    _panel.interactable = false;
                    continue;
                }

                _panel.alpha = 1f;
                _panel.interactable = true;
                distanceText.text = Utils.GetHumanReadableDistance(
                    LocationDot.Instance.CurrentLocation, 
                    PinSelector.Instance.SelectedPin.Gpx.Coordinates);
            }
        }
    }
}
