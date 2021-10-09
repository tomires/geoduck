using UnityEngine;
using UnityEngine.UI;

namespace Geoduck
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CacheDetailsPanel : MonoSingleton<CacheDetailsPanel>
    {
        [SerializeField] private Text cacheName;
        [SerializeField] private Image typeIcon;
        [SerializeField] private Text hint;
        [SerializeField] private Text difficulty;
        [SerializeField] private Text terrain;
        [SerializeField] private Image size;
        [SerializeField] private Sprite micro, small, regular, large, other;
        [SerializeField] private Button expandDetailsButton;

        private CanvasGroup _panel;
        private RectTransform _transform;
        private bool _expanded = false;
        private float _defaultPanelHeight;

        void Start()
        {
            _panel = GetComponent<CanvasGroup>();
            _transform = GetComponent<RectTransform>();
            _defaultPanelHeight = _transform.rect.size.y;
            expandDetailsButton.onClick.AddListener(ToggleExpand);
        }

        public void ToggleExpand()
        {
            _expanded ^= true;
            var size = _transform.sizeDelta;
            size.y = _expanded
                ? Constants.cacheDetailsExpandedHeight
                : _defaultPanelHeight;
            _transform.sizeDelta = size;
        }

        public void HideCacheDetails()
        {
            _panel.interactable = false;
            _panel.blocksRaycasts = false;
            _panel.alpha = 0f;
        }

        public void ShowCacheDetails(GpxStructure cache)
        {
            _panel.interactable = true;
            _panel.blocksRaycasts = true;
            _panel.alpha = 1f;

            cacheName.text = cache.wpt.details.name;
            typeIcon.sprite = IconLibrary.Instance.GetIcon(cache);
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
