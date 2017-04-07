using System;
using Arkcraft.Configuration;
using Arkcraft.Tiles;
using Arkcraft.Items;
using Arkcraft.Avatars;

namespace Arkcraft.Configuration
{
    public class AvailableConfigurations
    {
        public ConfigWorldSize[] worldSizes;
        public ConfigDayInfo[] dayInfos;

        public ConfigExtraMaterials extraMaterials;
        public ConfigWorldGenerator[] worldGenerators;

        public TileDefinition[] tileDefinitions;
        public ItemDefinition[] itemDefinitions;
        public AvatarDefinition[] avatarDefinitions;
    }
}
