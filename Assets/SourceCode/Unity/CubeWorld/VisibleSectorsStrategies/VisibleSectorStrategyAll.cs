using System;
using System.Collections.Generic;
using Arkcraft.Tiles;
using Arkcraft.Avatars;
using Arkcraft.Utils;
using Arkcraft.Sectors;

namespace Unity.CubeWorld.VisibleSectorsStrategies
{
    public class VisibleSectorsStrategyAll : VisibleSectorsStrategy
    {
        public VisibleSectorsStrategyAll(SectorManagerUnity sectorManagerUnity, SectorManager sectorManager)
        {
            foreach (Sector sector in sectorManager.sectors)
                sectorManagerUnity.GetSectorUnityFromCache(sector);
        }

        public override void Update()
        {
        }

        public override void Clear()
        {
        }
    }
}
