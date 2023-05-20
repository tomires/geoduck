using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Geoduck
{
    public class CacheDetailsView : MonoSingleton<CacheDetailsView>
    {
        [SerializeField] private LayoutElement panel;
        [SerializeField] private Text cacheName;
        [SerializeField] private Image typeIcon;
        [SerializeField] private Text hint;
        [SerializeField] private List<Image> difficultyDots;
        [SerializeField] private List<Image> terrainDots;
        [SerializeField] private List<Image> sizeDots;
        [SerializeField] private Button expandDetailsButton;
        [SerializeField] private Button logStatusButton;

        private bool _expanded = false;
        private float _defaultPanelHeight;
        private GpxStructure _selectedCache;

        void Start()
        {
            _defaultPanelHeight = panel.preferredHeight;
            expandDetailsButton.onClick.AddListener(ToggleExpand);
            logStatusButton.onClick.AddListener(ChangeLogStatus);
        }

        private void ToggleExpand()
        {
            _expanded ^= true;
            panel.preferredHeight = _expanded
                ? Constants.cacheDetailsExpandedHeight
                : _defaultPanelHeight;
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
            panel.gameObject.SetActive(false);
        }

        public void ShowCacheDetails(GpxStructure cache)
        {
            _selectedCache = cache;
            panel.gameObject.SetActive(true);

            difficultyDots.ForEach(dot => dot.color = Constants.Colors.dotInactive);
            terrainDots.ForEach(dot => dot.color = Constants.Colors.dotInactive);
            sizeDots.ForEach(dot => dot.color = Constants.Colors.dotInactive);

            cacheName.text = cache.wpt.details.name;
            typeIcon.sprite = IconLibrary.Instance.GetIcon(cache);
            hint.text = cache.wpt.details.hint;
            for (int d = 0; d < cache.wpt.details.difficulty; d++)
                difficultyDots[d].color = Constants.Colors.dotActive;
            for (int t = 0; t < cache.wpt.details.terrain; t++)
                terrainDots[t].color = Constants.Colors.dotActive;
            var size = cache.wpt.details.container switch
            {
                "Micro" => 1,
                "Small" => 2,
                "Regular" => 3,
                "Large" => 4,
                _ => 0
            };
            for (int s = 0; s < size; s++)
                sizeDots[s].color = Constants.Colors.dotActive;
        }
    }
}
