using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Geoduck
{
    [RequireComponent(typeof(CanvasGroup))]
    [RequireComponent(typeof(Button))]
    public class DistancePanel : MonoSingleton<DistancePanel>
    {
        [SerializeField] Text distanceText;
        private CanvasGroup _panel;
        private Button _button;

        void Start()
        {
            _panel = GetComponent<CanvasGroup>();
            _button = GetComponent<Button>();

            StartCoroutine(UpdateDistance());
            _button.onClick.AddListener(() => LocationDot.Instance.CenterMapOnLocation());
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
