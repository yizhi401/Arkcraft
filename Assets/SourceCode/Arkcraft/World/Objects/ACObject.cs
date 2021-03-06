using System;
using Arkcraft.Utils;
using System.Collections.Generic;
using SourceCode.Arkcraft.Utils;

namespace Arkcraft.World.Objects
{
	public class ACObject
	{
        public int objectId;
		public ArkWorld world;
		public ACDefinition definition;

		public Vector3 position;
        public Vector3 rotation;
        public byte energy;
		public bool destroyed;

        private List<ACComponent> components;

        public ACObject(int objectId)
        {
            this.objectId = objectId;
        }

        public void AddComponent(ACComponent component)
        {
            if (components == null)
                components = new List<ACComponent>();

            components.Add(component);

            component.AddedToObject(this);
        }

        public void RemoveComponent(ACComponent component)
        {
            components.Remove(component);

            component.RemovedFromObject();
        }

        public virtual void Clear()
        {
            if (components != null)
            {
                foreach (ACComponent component in components)
                    component.RemovedFromObject();

                components.Clear();
            }

            world = null;
        }

        public virtual void Update(float deltaTime)
        {
            if (components != null)
                foreach (ACComponent component in components)
                    component.Update(deltaTime);
        }

        public virtual void Save(System.IO.BinaryWriter bw)
        {
            SerializationUtils.Write(bw, position);
            SerializationUtils.Write(bw, rotation);
            bw.Write(energy);
            bw.Write(destroyed);
        }

        public virtual void Load(System.IO.BinaryReader br)
        {
            position = SerializationUtils.ReadVector3(br);
            rotation = SerializationUtils.ReadVector3(br);
            energy = br.ReadByte();
            destroyed = br.ReadBoolean();
        }
    }
}

