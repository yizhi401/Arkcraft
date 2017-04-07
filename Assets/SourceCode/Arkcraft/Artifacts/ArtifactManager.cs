using System;
using System.Collections.Generic;
using Arkcraft.Tiles;

namespace Arkcraft.Artifacts
{
    public class ArtifactManager
    {
        public const int ARTIFACT_SIZE_BITS = 4;
        public const int ARTIFACT_SIZE = 1 << ARTIFACT_SIZE_BITS;

        public Arkcraft.World.ArkWorld world;

        public int xArtifacts, yArtifacts, zArtifacts;
        public int xArtifactsBits, yArtifactsBits, zArtifactsBits,xyArtifactsBits;

        public Artifact[] artifacts;

        private bool pendingArtifactsUpdateOrderValid = false;
        private List<Artifact> pendingArtifactsUpdate = new List<Artifact>();
        private bool pendingArtifactsUpdateLightOrderValid = false;
        private List<Artifact> pendingArtifactsUpdateLight = new List<Artifact>();

        public ArtifactManager(Arkcraft.World.ArkWorld world)
        {
            this.world = world;
        }

        public void Create()
        {
            xArtifactsBits = world.tileManager.sizeXbits - ARTIFACT_SIZE_BITS;
            yArtifactsBits = world.tileManager.sizeYbits - ARTIFACT_SIZE_BITS;
            zArtifactsBits = world.tileManager.sizeZbits - ARTIFACT_SIZE_BITS;
            xyArtifactsBits = xArtifactsBits + yArtifacts;

            xArtifacts = 1 << xArtifactsBits;
            yArtifacts = 1 << yArtifactsBits;
            zArtifacts = 1 << zArtifactsBits;

            artifacts = new Artifact[xArtifacts * yArtifacts * zArtifacts];

            for (int x = 0; x < xArtifacts; x++)
                for (int y = 0; y < yArtifacts; y++)
                    for (int z = 0; z < zArtifacts; z++)
                        CreateArtifact(new TilePosition(x,y,z), false);

        }

        private void CreateArtifact(TilePosition posArtifact, bool immediate)
        {
            TilePosition artifactOffset = new TilePosition(posArtifact.x << ARTIFACT_SIZE_BITS,posArtifact.y << ARTIFACT_SIZE_BITS, posArtifact.z << ARTIFACT_SIZE_BITS);

            Artifact artifact = new Artifact(world,posArtifact,artifactOffset);

            artifacts[posArtifact.x | (posArtifact.y << xArtifactsBits) | (posArtifact.z << xyArtifactsBits)] = artifact;

            if (immediate == false)
                pendingArtifactsUpdate.Add(artifact);
            else
                artifact.UpdateMesh();
        }

        public Artifact GetArtifactTile(TilePosition pos)
        {
            pos.x >>= ARTIFACT_SIZE_BITS;
            pos.y >>= ARTIFACT_SIZE_BITS;
            pos.z >>= ARTIFACT_SIZE_BITS;
            return artifacts[pos.x | (pos.y << xArtifactsBits) | (pos.z << xyArtifactsBits)];
        }

        public Artifact GetArtifact(TilePosition posArtifact)
        {
            return artifacts[posArtifact.x | (posArtifact.y << xArtifactsBits) | (posArtifact.z << xyArtifactsBits)];
        }

    }
}
