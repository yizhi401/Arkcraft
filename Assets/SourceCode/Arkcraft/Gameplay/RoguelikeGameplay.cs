using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arkcraft.World.Generator;
using Arkcraft.Tiles;
using Arkcraft.Utils;
using Arkcraft.Items;

namespace Arkcraft.Gameplay
{
    public class RoguelikeGameplay : BaseGameplay
    {
        private TilePosition playerStartPosition;

        public RoguelikeGameplay() :
            base("roguelike")
        {
        }

        public override TilePosition GetPlayerResetPosition()
        {
            return playerStartPosition;
        }

        public override void FillPlayerInventory(Inventory inventory)
        {
            ItemDefinition itemDefinition;
            Item item;
            InventoryEntry ie;

            itemDefinition = world.itemManager.GetItemDefinitionById("item_weapon");
            item = new Item(world, itemDefinition, -1);
            ie = new InventoryEntry();
            ie.position = 0;
            ie.quantity = 1;
            ie.cwobject = item;
            inventory.entries.Add(ie);

            itemDefinition = world.itemManager.GetItemDefinitionById("item_fire");
            item = new Item(world, itemDefinition, -1);
            ie = new InventoryEntry();
            ie.position = 0;
            ie.quantity = 1;
            ie.cwobject = item;

            inventory.entries.Add(ie);
        }

        public override Arkcraft.World.Generator.GeneratorProcess Generate(Arkcraft.Configuration.Config config)
        {
            RoguelikeWorldGenerator generator = new RoguelikeWorldGenerator(world);

            this.playerStartPosition = generator.playerStartPosition;

            return world.tileManager.Generate(generator);
        }
    }
}
