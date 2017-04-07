using System;
using Arkcraft.World;
using Arkcraft.World.Objects;
using Arkcraft.Serialization;

namespace Arkcraft.Items
{
	public class ItemDefinition : ACDefinition
	{
		public ItemDefinition()
			: base(DefinitionType.Item)
		{
			
		}

        protected ItemDefinition(DefinitionType type)
            : base(type)
        {

        }

        public ACVisualDefinition visualDefinition;
		
		public int durability;
		public int damage;
		public bool setOnFire;

        public override void Serialize(Serializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(ref visualDefinition, "visualDefinition");
            serializer.Serialize(ref durability, "durability");
            serializer.Serialize(ref damage, "damage");
            serializer.Serialize(ref setOnFire, "setOnFire");
        }
	}
}

