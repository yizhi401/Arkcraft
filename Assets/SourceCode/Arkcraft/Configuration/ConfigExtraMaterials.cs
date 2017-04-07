using Arkcraft.Serialization;

namespace Arkcraft.Configuration
{
    public class ConfigExtraMaterials : ISerializable
    {
        public int[] damageMaterials;
        public int[] fireMaterials;

        public void Serialize(Serializer serializer)
        {
            serializer.Serialize(ref damageMaterials, "damageMaterials");
            serializer.Serialize(ref fireMaterials, "fireMaterials");
        }
    }
}
