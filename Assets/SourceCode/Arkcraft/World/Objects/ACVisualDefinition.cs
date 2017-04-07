using System;
using System.Collections.Generic;
using Arkcraft.Utils;
using Arkcraft.Serialization;

namespace Arkcraft.World.Objects
{
    public class ACVisualDefinition : ISerializable
    {
        public Vector3 pivot;

        public string material;
        public int scale;
        public string plane;

        public int materialCount;

        public void Serialize(Serializer serializer)
        {
            serializer.Serialize(ref pivot, "pivot");
            serializer.Serialize(ref material, "material");
            serializer.Serialize(ref scale, "scale");
            serializer.Serialize(ref plane, "plane");
            serializer.Serialize(ref materialCount, "materialCount");
        }
    }
}
