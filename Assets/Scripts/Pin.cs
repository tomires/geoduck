using UnityEngine;

namespace Geoduck
{
    [RequireComponent(typeof(SphereCollider))]
    [RequireComponent(typeof(Renderer))]
    public class Pin : MonoBehaviour
    {
        public GpxStructure Gpx
        {
            get => _gpx;
            set
            {
                _gpx = value;
                SetColor(false);
            }
        }
        private GpxStructure _gpx;
        private Renderer _renderer;

        void Awake()
        {
            _renderer = GetComponent<Renderer>();
        }

        public void SetColor(bool selected)
        {
            _renderer.material = selected
                ? MaterialLibrary.Instance.selected
                : _gpx.wpt.details.type switch
                {
                    Constants.CacheTypes.traditional => MaterialLibrary.Instance.traditional,
                    Constants.CacheTypes.multi => MaterialLibrary.Instance.multi,
                    Constants.CacheTypes.mystery => MaterialLibrary.Instance.mystery,
                    _ => MaterialLibrary.Instance.mystery
                };
        }
    }
}
