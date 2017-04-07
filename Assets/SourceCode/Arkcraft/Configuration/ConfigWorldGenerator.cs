using System;
using Arkcraft.World.Generator;

namespace Arkcraft.Configuration
{
    public class ConfigWorldGenerator
    {
        public string name;

        public ConfigSurroundings surroundings = new ConfigSurroundings();

        public CubeWorldGenerator generator;
    }
}
