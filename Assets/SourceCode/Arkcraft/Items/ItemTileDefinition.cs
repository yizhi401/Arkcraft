using System;
using Arkcraft.World;
using Arkcraft.World.Objects;
using Arkcraft.Tiles;
using Arkcraft.Serialization;

namespace Arkcraft.Items
{
    /**
     * Definition used for "item tiles" that are dropped in the world when a real tile is destroyed
     */
	public class ItemTileDefinition : ItemDefinition
	{
        public ItemTileDefinition()
			: base(DefinitionType.ItemTile)
		{
			
		}
		
		public TileDefinition tileDefinition;

        public override void Serialize(Serializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(ref tileDefinition, "tileDefinition");
        }
    }
}

