using System;
using System.Collections;
using Arkcraft.Utils;
using Arkcraft.World;
using Arkcraft.World.Objects;
using Arkcraft.Avatars.Components;

namespace Arkcraft.Avatars
{
    public class Avatar : ACObject
    {
        public AvatarInput input;

        public Avatar(Arkcraft.World.ArkWorld world, AvatarDefinition avatarDefinition, int objectId)
            : base(objectId)
        {
            this.world = world;
			this.definition = avatarDefinition;

            this.input = new AvatarInput();

            AddComponent(new AvatarComponentPhysics());
        }
    }
}
