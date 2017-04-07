using System;
using System.Collections.Generic;
using Arkcraft.World.Objects;
using Arkcraft.Utils;
using Arkcraft.Serialization;

namespace Arkcraft.Avatars
{
    public class AvatarPartDefinition : ISerializable
    {
        public string id;
        public Vector3 offset;
        public Vector3 rotation;
        public ACVisualDefinition visualDefinition;

        public void Serialize(Serializer serializer)
        {
            serializer.Serialize(ref id, "id");
            serializer.Serialize(ref offset, "offset");
            serializer.Serialize(ref rotation, "rotation");
            serializer.Serialize(ref visualDefinition, "visualDefinition");
        }
    }
}
