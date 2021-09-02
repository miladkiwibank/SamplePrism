using Prism.Regions;
using SamplePrism.Presentation.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplePrism.Presentation.Common
{

    public class RegionData
    {
        public string RegionName { get; set; }
    }

    public static class RegionService
    {
        public static Dictionary<string, RegionData> Regions = new Dictionary<string, RegionData>();

        public static void ActivateRegion(this IRegionManager regionManager, string regionName, object view)
        {
            if (!Regions.ContainsKey(regionName))
            {
                Regions.Add(regionName, new RegionData { RegionName = regionName });
            }

            regionManager.Regions[regionName].Activate(view);
            Regions[regionName].PublishEvent(EventTopicNames.RegionActivated);
        }
    }
}
