using UnityEngine;

namespace Geoduck
{
    public class IconLibrary : MonoSingleton<IconLibrary>
    {
        [SerializeField] private Sprite traditional;
        [SerializeField] private Sprite multi;
        [SerializeField] private Sprite mystery;
        [SerializeField] private Sprite found;
        [SerializeField] private Sprite notFound;

        public Sprite GetIcon(GpxStructure gpx)
        {
            return LogHistory.GetLogStatus(gpx) switch
            {
                LogStatus.Found => found,
                LogStatus.NotFound => notFound,
                _ => gpx.wpt.details.type switch
                {
                    Constants.CacheTypes.traditional => traditional,
                    Constants.CacheTypes.multi => multi,
                    Constants.CacheTypes.mystery => mystery,
                    _ => mystery
                }
            };
        }
    }
}
