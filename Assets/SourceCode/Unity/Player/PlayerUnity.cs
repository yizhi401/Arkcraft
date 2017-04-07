using UnityEngine;
using System.Collections;
using Arkcraft.Tiles;
using Arkcraft.World.Objects;
using Arkcraft.Avatars;

public class PlayerUnity : AvatarUnity
{
    public Camera mainCamera;
    public PlayerGUI playerGUI;
    public PlayerControllerUnity playerController;

    private bool useExternalCamera;

    public Player player
    {
        get { return (Player) avatar; }
    }

    [HideInInspector]
    public ACObject objectInHand;

    public override void Start()
    {
        base.Start();

        mainCamera.transform.localPosition = GetLocalHeadPosition();

        DisableBodyRender();
    }

    public Vector3 GetLocalHeadPosition()
    {
        return new Vector3(0.0f, Arkcraft.Utils.Graphics.TILE_SIZE * Arkcraft.Avatars.Player.HEAD_POSITION, 0.0f);
    }

    public void ChangeCamera()
    {
        useExternalCamera = !useExternalCamera;

        if (useExternalCamera == false)
        {
            mainCamera.transform.localPosition = GetLocalHeadPosition();
            DisableBodyRender();
        }
        else
        {
            mainCamera.transform.position = transform.position + new Vector3(0.0f, Arkcraft.Utils.Graphics.TILE_SIZE * Arkcraft.Avatars.Player.HEAD_POSITION, 0.0f) - mainCamera.transform.forward * 5.0f;
            EnableBodyRender();
        }

    }

    public void UpdateControlled()
    {
        playerController.UpdateControlled();

        if (gameManagerUnity.State == GameManagerUnity.GameManagerUnityState.GAME ||
            gameManagerUnity.State == GameManagerUnity.GameManagerUnityState.PAUSE)
        {
            UpdatePlayerPosition();
        }

        if (useExternalCamera)
        {
            mainCamera.transform.position = transform.position + new Vector3(0.0f, Arkcraft.Utils.Graphics.TILE_SIZE * Arkcraft.Avatars.Player.HEAD_POSITION, 0.0f) - mainCamera.transform.forward * 5.0f;
        }
    }
	
	private bool firstUpdate = true;
	
	public void Reset()
	{
		firstUpdate = true;
	}

    private void UpdatePlayerPosition()
    {
        if (firstUpdate ||
			transform.position != GraphicsUnity.CubeWorldVector3ToVector3(avatar.position))
        {
			firstUpdate = false;

            transform.position = GraphicsUnity.CubeWorldVector3ToVector3(avatar.position);
            gameManagerUnity.sectorManagerUnity.UpdateVisibleSectors();
        }
    }

    public Texture underWaterTexture;

    public void DrawUnderWaterTexture()
    {
        if (avatar != null && avatar.world != null)
        {
            TilePosition cameraPosition = GraphicsUnity.Vector3ToTilePosition(mainCamera.transform.position);
            if (avatar.world.tileManager.IsValidTile(cameraPosition) && avatar.world.tileManager.GetTileLiquid(cameraPosition) == true)
            {
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), underWaterTexture, ScaleMode.StretchToFill, true);
            }
        }
    }
}
