using System;
using System.Collections.Generic;
using Arkcraft.World;
using UnityEngine;
using Arkcraft.Tiles;
using Arkcraft.World.Objects;
using Arkcraft.Items;
using Arkcraft.Avatars;

public class ACObjectsManagerUnity : IACListener
{
    private List<TileUnity> unityTiles = new List<TileUnity>();
	private List<ItemUnity> unityItems = new List<ItemUnity>();
    private List<ItemTileUnity> unityItemTiles = new List<ItemTileUnity>();
    private List<AvatarUnity> unityAvatars = new List<AvatarUnity>();
	
	private Dictionary<ACObject, GameObject> createdGO = new Dictionary<ACObject, GameObject>();

	private GameManagerUnity gameManagerUnity;
	
	public ACObjectsManagerUnity (GameManagerUnity gameManagerUnity)
	{
		this.gameManagerUnity = gameManagerUnity;
	}
	
	public void Clear()
	{
        foreach (TileUnity unityTile in unityTiles)
            GameObject.DestroyImmediate(unityTile.gameObject);

        unityTiles.Clear();
		
        foreach (ItemUnity unityItem in unityItems)
            GameObject.DestroyImmediate(unityItem.gameObject);

        unityItems.Clear();

        foreach (ItemTileUnity unityItemTile in unityItemTiles)
            GameObject.DestroyImmediate(unityItemTile.gameObject);

        unityItemTiles.Clear();

        foreach (AvatarUnity unityAvatar in unityAvatars)
            GameObject.DestroyImmediate(unityAvatar.gameObject);

        unityAvatars.Clear();
    }

    public GameObject CreateTileGameObject(DynamicTile tile)
    {
        GameObject g = new GameObject();
        TileUnity tileUnity = (TileUnity)g.AddComponent(typeof(TileUnity));
        tileUnity.gameManagerUnity = gameManagerUnity;
        tileUnity.tile = tile;

        unityTiles.Add(tileUnity);

        return g;
    }

    public void RemoveTileGameObject(GameObject g)
    {
        unityTiles.Remove(g.GetComponent<TileUnity>());

        GameObject.Destroy(g);
    }

    public GameObject CreateItemGameObject(Item item)
    {
        GameObject g = new GameObject();
        ItemUnity itemUnity = (ItemUnity)g.AddComponent(typeof(ItemUnity));
        itemUnity.gameManagerUnity = gameManagerUnity;
        itemUnity.item = item;

        unityItems.Add(itemUnity);

        return g;
    }

	public void RemoveItemGameObject(GameObject g)
	{
    	unityItems.Remove(g.GetComponent<ItemUnity>());
		
		GameObject.Destroy(g);
	}

    public GameObject CreateItemTileGameObject(ItemTile item)
    {
        GameObject g = new GameObject();
        ItemTileUnity itemTileUnity = (ItemTileUnity)g.AddComponent(typeof(ItemTileUnity));
        itemTileUnity.gameManagerUnity = gameManagerUnity;
        itemTileUnity.item = item;

        unityItemTiles.Add(itemTileUnity);

        return g;
    }

    public void RemoveItemTileGameObject(GameObject g)
    {
        unityItemTiles.Remove(g.GetComponent<ItemTileUnity>());

        GameObject.Destroy(g);
    }

    public GameObject CreateAvatarGameObject(Arkcraft.Avatars.Avatar avatar)
    {
        GameObject g;
        AvatarUnity avatarUnity;

        if (avatar.definition.id == "player")
        {
            g = (GameObject) GameObject.Instantiate(Resources.Load("Prefabs/Player", typeof(GameObject)));

            avatarUnity = g.GetComponent<PlayerUnity>();

            gameManagerUnity.playerUnity = g.GetComponent<PlayerUnity>();

            gameManagerUnity.playerUnity.gameManagerUnity = gameManagerUnity;
            gameManagerUnity.playerUnity.avatar = avatar;
            gameManagerUnity.playerUnity.Reset();
        }
        else
        {
            g = new GameObject();
            avatarUnity = (NonPlayerAvatarUnity)g.AddComponent(typeof(NonPlayerAvatarUnity));
            avatarUnity.gameManagerUnity = gameManagerUnity;
            avatarUnity.avatar = avatar;
        }

        unityAvatars.Add(avatarUnity);

        return g;
    }

    public void RemoveAvatarGameObject(GameObject g)
    {
        unityAvatars.Remove(g.GetComponent<AvatarUnity>());

        GameObject.Destroy(g);
    }

   
    public GameObject CreateGameObjectFromObject(ACObject cwObject)
	{
		GameObject go = null;
		
		switch(cwObject.definition.type)
		{
            case ACDefinition.DefinitionType.Item:
                go = CreateItemGameObject((Item) cwObject);
				break;
			
			case ACDefinition.DefinitionType.Tile:
				go = CreateTileGameObject((DynamicTile) cwObject);
				break;
			
            case ACDefinition.DefinitionType.ItemTile:
                go = CreateItemTileGameObject((ItemTile) cwObject);
                break;

            case ACDefinition.DefinitionType.Avatar:
                go = CreateAvatarGameObject((Arkcraft.Avatars.Avatar) cwObject);
                break;
           
            default:
				throw new System.Exception("Unknown game object to create");
		}
		
		return go;
	}
	
	public void RemoveGameObject(GameObject go)
	{
        if (go.GetComponent<ItemUnity>())
            RemoveItemGameObject(go);
        else if (go.GetComponent<TileUnity>())
            RemoveTileGameObject(go);
        else if (go.GetComponent<ItemTileUnity>())
            RemoveItemTileGameObject(go);
        else if (go.GetComponent<AvatarUnity>())
            RemoveAvatarGameObject(go);
        else
			throw new System.Exception("Unknown game object to destroy");
	}

    public GameObject FindGameObject(ACObject cwObject)
    {
        if (createdGO.ContainsKey(cwObject))
            return createdGO[cwObject];

        return null;
    }
	
	
	public void CreateObject(ACObject cwobject)
	{
		GameObject go = CreateGameObjectFromObject(cwobject);
		go.transform.position = GraphicsUnity.CubeWorldVector3ToVector3(cwobject.position);
		createdGO[cwobject] = go;
	}
	
	public void UpdateObject(ACObject cwobject)
	{
	}
	
	public void DestroyObject(ACObject cwobject)
	{
		GameObject go = createdGO[cwobject];
		createdGO.Remove(cwobject);
		RemoveGameObject(go);
	}
}

