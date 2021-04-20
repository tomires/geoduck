using UnityEngine;
using UnityEngine.UI;

namespace Geoduck.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CacheDetails : MonoSingleton<CacheDetails>
    {
        [SerializeField] private Text cacheName;
        [SerializeField] private Text hint;
        [SerializeField] private Text difficulty;
        [SerializeField] private Text terrain;
        [SerializeField] private Image size;
        [SerializeField] private Sprite micro, small, regular, large, other;
        private CanvasGroup _panel;

        private void Awake()
        {
            _panel = GetComponent<CanvasGroup>();
        }

        public void HideCacheDetails()
        {
            _panel.interactable = false;
            _panel.alpha = 0f;
        }

        public void ShowCacheDetails(GpxStructure cache)
        {
            _panel.interactable = true;
            _panel.alpha = 1f;

            cacheName.text = cache.wpt.details.name;
            hint.text = cache.wpt.details.hint;
            difficulty.text = cache.wpt.details.difficulty.ToString("0.0");
            terrain.text = cache.wpt.details.terrain.ToString("0.0");
            size.sprite = cache.wpt.details.container switch
            {
                "Micro" => micro,
                "Small" => small,
                "Regular" => regular,
                "Large" => large,
                _ => other
            };
        }
    }
}
