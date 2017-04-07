using System;
using Arkcraft.Serialization;

namespace Arkcraft.World.Objects
{
	public class ACDefinition : ISerializable
	{
		public enum DefinitionType
		{
			Tile,
			Item,
            ItemTile,
			Avatar
		}
		
		public DefinitionType type;
		public string id;
		public string description;
		
		public int energy;

        public ACDefinition()
        {
        }

        public ACDefinition(DefinitionType definitionType)
		{
			this.type = definitionType;
		}

        public virtual void Serialize(Serializer serializer)
        {
            serializer.SerializeEnum(ref type, "type");
            serializer.Serialize(ref id, "id");
            serializer.Serialize(ref description, "description");
            serializer.Serialize(ref energy, "energy");
        }
    }
}

