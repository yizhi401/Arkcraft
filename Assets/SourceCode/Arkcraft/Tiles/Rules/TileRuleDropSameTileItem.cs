using Arkcraft.Tiles;
using Arkcraft.Items;
using Arkcraft.Utils;
using Arkcraft.Serialization;

namespace Arkcraft.Tiles.Rules
{
    public class TileRuleDropSameTileItem : TileRule
    {
        public TileRuleDropSameTileItem()
        {
        }

        public TileRuleDropSameTileItem(TileRuleCondition condition)
            : base(condition)
        {
        }

        public override void Execute(TileManager tileManager, Tile tile, TilePosition pos)
        {
            TileDefinition tileDefinition = tileManager.GetTileDefinition(tile.tileType);

            ItemDefinition itemDefinition = tileManager.world.itemManager.GetItemDefinitionById(tileDefinition.id);

            tileManager.world.gameplay.CreateItem(itemDefinition, Graphics.TilePositionToVector3(pos));
        }

        public override void Serialize(Serializer serializer)
        {
            base.Serialize(serializer);
        }
    }
}