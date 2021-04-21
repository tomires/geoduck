using UnityEngine;
using UnityEngine.UI;

namespace Geoduck
{
    [RequireComponent(typeof(Button))]
    public class LoadButton : MonoBehaviour
    {
        void Awake()
        {
            GetComponent<Button>().onClick.AddListener(Load);
        }

        public void Load()
        {
            GpxImporter.LoadGpx();
        }
    }
}
