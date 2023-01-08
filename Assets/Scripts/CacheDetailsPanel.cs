using System.Collections.Generic;
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
        [SerializeField] private Button logStatusButton;
        [SerializeField] private List<RectTransform> scaledTransforms;

        private CanvasGroup _panel;
        private bool _expanded = false;
        private float _defaultPanelHeight;
        private GpxStructure _selectedCache;

        void Start()
        {
            _panel = GetComponent<CanvasGroup>();
            _defaultPanelHeight = scaledTransforms[0].rect.size.y;
            expandDetailsButton.onClick.AddListener(ToggleExpand);
            logStatusButton.onClick.AddListener(ChangeLogStatus);
        }

        private void ToggleExpand()
        {
            _expanded ^= true;
            foreach(var scaledTransform in scaledTransforms)
            {
                var size = scaledTransform.sizeDelta;
                size.y = _expanded
                    ? Constants.cacheDetailsExpandedHeight
                    : _defaultPanelHeight;
                    scaledTransform.sizeDelta = size;
            }
        }

        private void ChangeLogStatus()
        {
            var status = LogHistory.GetLogStatus(_selectedCache);
            var newStatus = status switch
            {
                LogStatus.None => LogStatus.Found,
                LogStatus.Found => LogStatus.NotFound,
                _ => LogStatus.None
            };
            LogHistory.SetLogStatus(_selectedCache, newStatus);
            PinSpawner.Instance.RefreshPin(_selectedCache);
            typeIcon.sprite = IconLibrary.Instance.GetIcon(_selectedCache);
        }

        public void HideCacheDetails()
        {
            _panel.interactable = false;
            _panel.blocksRaycasts = false;
            _panel.alpha = 0f;
        }

        public void ShowCacheDetails(GpxStructure cache)
        {
            _selectedCache = cache;
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
