using UnityEngine;

namespace Geoduck
{
    public class IconLibrary : MonoSingleton<IconLibrary>
    {
        [SerializeField] private Sprite traditional;
        [SerializeField] private Sprite multi;
        [SerializeField] private Sprite mystery;

        public Sprite GetIcon(GpxStructure gpx)
        {
            return gpx.wpt.details.type switch
            {
                Constants.CacheTypes.traditional => traditional,
                Constants.CacheTypes.multi => multi,
                Constants.CacheTypes.mystery => mystery,
                _ => mystery
            };
        }
    }
}
