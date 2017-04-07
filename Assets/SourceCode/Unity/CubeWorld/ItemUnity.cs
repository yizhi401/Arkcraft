using UnityEngine;
using System.Collections.Generic;
using Arkcraft.Tiles;

public class ItemUnity : MonoBehaviour
{
    public GameManagerUnity gameManagerUnity;
    public Arkcraft.Items.Item item; 
	
    void Start()
    {
        VisualDefinitionRenderUnity visualDefinitionRenderer = gameObject.AddComponent<VisualDefinitionRenderUnity>();
        visualDefinitionRenderer.world = gameManagerUnity.world;
        visualDefinitionRenderer.material = gameManagerUnity.materialItems;
        visualDefinitionRenderer.visualDefinition = item.itemDefinition.visualDefinition;
    }
}

