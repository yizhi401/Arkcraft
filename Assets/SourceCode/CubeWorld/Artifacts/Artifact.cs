using System.Collections.Generic;
using CubeWorld.Tiles;

namespace CubeWorld.Artifacts
{
    public class Artifact 
    {

        public TilePosition artifactPosition;

        public TilePosition tileOffset;

        public CubeWorld.World.CubeWorld world;

        public IArtifactGraphics artifactGraphics;

        public bool insideInvalidateArtifactQueue;
        public bool insideInvalidateLightQueue;

        public int artifactId;
        public List<Tile> tiles;

        public Artifact(CubeWorld.World.CubeWorld world, TilePosition artifactPosition, TilePosition tileOffset)
        {
            this.world = world;
            this.tileOffset = tileOffset;
            this.artifactPosition = artifactPosition;
        }

        public override int GetHashCode()
        {
            return artifactPosition.GetHashCode();
        }

        public void SetArtifactGraphics(IArtifactGraphics graphics)
        {
            if (this.artifactGraphics != null)
                this.artifactGraphics.SetArtifact(null);
            this.artifactGraphics = graphics;

            if (this.artifactGraphics != null)
                this.artifactGraphics.SetArtifact(this);
        }

        public IArtifactGraphics GetArtifactGraphics()
        {
            return this.artifactGraphics;
        }

        public void UpdateMesh()
        {
            if (artifactGraphics != null)
                artifactGraphics.UpdateMesh();
        }

        public void UpdateAmbientLight()
        {
            if (artifactGraphics != null)
                artifactGraphics.UpdateAmbientLight();
        }

        public void Clear()
        {
            world = null;
            artifactGraphics = null;
            tiles.Clear();
            tiles = null;
        }

        public void AddTile(Tile tile)
        {
            if (tiles == null)
            {
                tiles = new List<Tile>();
            }
            tiles.Add(tile);
            tile.ArtifactId = artifactId;
        }

        public void removeTile(Tile tile)
        {
            tiles.Remove(tile);
        }
    }
}
