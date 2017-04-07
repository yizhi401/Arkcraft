﻿using Arkcraft.Tiles;
using Arkcraft.Utils;
using Arkcraft.Serialization;

namespace Arkcraft.Tiles.Rules
{
    public class TileRulePlayEffect : TileRule
    {
        public TilePosition delta;
        public string effectId;

        public TileRulePlayEffect()
        {
        }

        public TileRulePlayEffect(TilePosition delta, string effectId, TileRuleCondition condition)
            : base(condition)
        {
            this.delta = delta;
            this.effectId = effectId;
        }

        public override void Execute(TileManager tileManager, Tile tile, TilePosition pos)
        {
            pos += delta;

            tileManager.world.fxListener.PlayEffect(effectId, Graphics.TilePositionToVector3(pos));
        }

        public override void Serialize(Serializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(ref delta, "delta");
            serializer.Serialize(ref effectId, "effectId");
        }
    }
}