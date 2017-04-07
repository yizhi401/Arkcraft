using System;
using Arkcraft.Tiles;
using Arkcraft.World.Objects;
using Arkcraft.Utils;

namespace Arkcraft.World
{
	public interface IACFxListener
	{
        void PlaySound(String soundId, Vector3 position);
        void PlaySound(String soundId, ACObject fromObject);

        void PlayEffect(String effectId, Vector3 position);
        void PlayEffect(String effectId, ACObject fromObject);
	}
}

