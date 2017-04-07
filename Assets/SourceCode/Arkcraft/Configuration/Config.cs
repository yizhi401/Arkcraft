using Arkcraft.World.Generator;
using Arkcraft.Tiles;
using Arkcraft.World.Lights;
using Arkcraft.Configuration;
using Arkcraft.Items;
using Arkcraft.Avatars;
using Arkcraft.Gameplay;

namespace Arkcraft.Configuration
{
    public class Config
    {
        public ConfigWorldSize worldSize;
        public ConfigDayInfo dayInfo;
        public ConfigWorldGenerator worldGenerator;

        public TileDefinition[] tileDefinitions;
		public ItemDefinition[] itemDefinitions;
		public AvatarDefinition[] avatarDefinitions;

        public ConfigExtraMaterials extraMaterials;

        public GameplayDefinition gameplay;
    }
}
