using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arkcraft.World.Generator;
using System.IO;
using SourceCode.Arkcraft.Utils;

namespace Arkcraft.Configuration
{
    public class ConfigSurroundings
    {
        public int surroundingMaterial;
        public WorldSizeRelativeValue surroundingLevel;
        public float surroundingOffsetY;

        public void Save(BinaryWriter bw)
        {
            bw.Write(surroundingMaterial);
            SerializationUtils.Write(bw, surroundingLevel);
            bw.Write(surroundingOffsetY);
        }

        public void Load(BinaryReader br)
        {
            surroundingMaterial = br.ReadInt32();
            surroundingLevel = SerializationUtils.ReadWorldSizeRelativeValue(br);
            surroundingOffsetY = br.ReadSingle();
        }
    }
}
