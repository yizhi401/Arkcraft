using System;
using System.Collections.Generic;
using Arkcraft.Tiles;
using Arkcraft.Utils;

namespace Arkcraft.Sectors
{
    public class Sector
    {
        public TilePosition sectorPosition;

        public Arkcraft.World.ArkWorld world;

        //starting point of (0,0,0) in this sector
        //used for calculation
        public TilePosition tileOffset;

        private ISectorGraphics sectorGraphics;

        public bool insideInvalidateSectorQueue;
        public bool insideInvalidateLightQueue;
		
        public override int GetHashCode()
        {
            return sectorPosition.GetHashCode();
        }

        public Sector(Arkcraft.World.ArkWorld world, TilePosition sectorPosition, TilePosition tileOffset)
        {
            this.world = world;
            this.tileOffset = tileOffset;
            this.sectorPosition = sectorPosition;
        }
		
		public void SetSectorGraphics(ISectorGraphics graphics)
		{
			if (this.sectorGraphics != null)
				this.sectorGraphics.SetSector(null);
			
			this.sectorGraphics = graphics;
			
			if (this.sectorGraphics != null)
				this.sectorGraphics.SetSector(this);
		}
		
		public ISectorGraphics GetSectorGraphics()
		{
			return this.sectorGraphics;
		}

        public void UpdateMesh()
        {
            if (sectorGraphics != null)
                sectorGraphics.UpdateMesh();
        }

        public void UpdateAmbientLight()
        {
            if (sectorGraphics != null)
                sectorGraphics.UpdateAmbientLight();
        }

        public void Clear()
        {
            world = null;
            sectorGraphics = null;
        }
    }
}