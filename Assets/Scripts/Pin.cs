using UnityEngine;

namespace Geoduck
{
    [RequireComponent(typeof(SphereCollider))]
    public class Pin : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer renderer;

        public GpxStructure Gpx
        {
            get => _gpx;
            set
            {
                _gpx = value;
                SetIcon(false);
            }
        }
        private GpxStructure _gpx;

        public void SetIcon(bool selected)
        {
            renderer.sprite = IconLibrary.Instance.GetIcon(_gpx);
        }
    }
}
