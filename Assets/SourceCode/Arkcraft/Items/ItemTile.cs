using System;
using Arkcraft.Utils;
using Arkcraft.World;
using Arkcraft.World.Objects;
using Arkcraft.Tiles;

namespace Arkcraft.Items
{
    public class ItemTile : Item
    {
        public ItemTileDefinition itemTileDefinition;

        public ItemTile(Arkcraft.World.ArkWorld world, ItemTileDefinition itemTileDefinition, int objectId)
            : base(world, itemTileDefinition, objectId)
        {
            this.itemTileDefinition = itemTileDefinition;
        }
    }
}
