using System;
using Arkcraft.Tiles;
using Arkcraft.World.Objects;

namespace Arkcraft.World
{
	public interface IACListener
	{
		void CreateObject(ACObject cwobject);
		void UpdateObject(ACObject cwobject);
		void DestroyObject(ACObject cwobject);
	}
}

