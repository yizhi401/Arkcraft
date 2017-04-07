using System;
using Arkcraft.World;
using Arkcraft.World.Objects;
using Arkcraft.Tiles;
using Arkcraft.Serialization;

namespace Arkcraft.Avatars
{
	public class AvatarDefinition : CWDefinition
	{
        public TilePosition sizeInTiles;
        public AvatarPartDefinition[] parts;

		public AvatarDefinition()
			: base(DefinitionType.Avatar)
		{
			
		}

        public override void Serialize(Serializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(ref sizeInTiles, "sizeInTiles");
            serializer.Serialize(ref parts, "parts");
        }
	}
}

