using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arkcraft.Tiles;

namespace Arkcraft.Gameplay
{
    public class BasicEnemiesGameplay : BaseGameplay
    {
        public BasicEnemiesGameplay() :
            base("basicEnemies")
        {
        }

        public override void WorldCreated()
        {
            base.WorldCreated();
            AddRandomEnemies(40);
        }
    }
}
