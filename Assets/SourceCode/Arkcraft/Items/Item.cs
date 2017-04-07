using System;
using Arkcraft.Utils;
using Arkcraft.World;
using Arkcraft.World.Objects;

namespace Arkcraft.Items
{
	public class Item : ACObject
	{
		public ItemDefinition itemDefinition;
		
        public Item(Arkcraft.World.ArkWorld world, ItemDefinition itemDefinition, int objectId) 
            : base(objectId)
		{
			this.world = world;
			this.definition = itemDefinition;
			this.itemDefinition = itemDefinition;
		}
    }
}

