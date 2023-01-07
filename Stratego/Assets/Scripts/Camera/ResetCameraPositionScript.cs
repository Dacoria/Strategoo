using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ResetCameraPositionScript : BaseEventCallback
{
    private Vector3 originalCameraPosition;
    private Quaternion originalCameraRotation;

    public bool Setting_ResetCameraOnNewTurnEvent;

    private List<PlayerForLevelInitCameraPos> PlayerInitCameraPositions = new List<PlayerForLevelInitCameraPos>
    {
        new PlayerForLevelInitCameraPos(1, 1, new Vector3(10,10,-2), new Vector3(50,0,0)),
        new PlayerForLevelInitCameraPos(2, 1, new Vector3(10,10,18), new Vector3(50,180,0)),

        new PlayerForLevelInitCameraPos(1, 2, new Vector3(10,10,-2), new Vector3(50,0,0)),
        new PlayerForLevelInitCameraPos(2, 2, new Vector3(10,10,21), new Vector3(50,180,0)),
    };

    void Start()
    {
        originalCameraPosition = Camera.main.transform.position;
        originalCameraRotation = Camera.main.transform.rotation;
    }

    protected override void OnNewRoundStarted(List<PlayerScript> arg1, PlayerScript arg2)
    {
        // begin het spel bij je eigen speler
        if (Setting_ResetCameraOnNewTurnEvent)
        {
            StartCoroutine(ResetCameraAfterXSeconds(0.15f));
        }
    }

    protected override void OnNewPlayerTurn(PlayerScript player)
    {
        if (Setting_ResetCameraOnNewTurnEvent && player.IsOnMyNetwork() && player.IsAi)
        {
            ResetCameraToPlayer();
        }
    }

    private IEnumerator ResetCameraAfterXSeconds(float seconds)
    {
        // wacht op spel laden
        yield return Wait4Seconds.Get(seconds);
        ResetCameraToPlayer();
    }

    public void ResetCameraToPlayer()
    {
        var allPlayers = NetworkHelper.instance.GetAllPlayers();
        if(allPlayers.Any(x => x.IsAi) && Settings.RotateTowardsMyPlayer)
        {
            ResetCameraToPlayer(Netw.CurrPlayer().Index);
        }
        else
        {
            ResetCameraToPlayer(Netw.MyPlayer().Index);
        }
    }

    public void ResetCameraToPlayer(int playerIndex)
    {
        // AI + aan de beurt? pak die, anders pak je eigen char
        var player = Netw.CurrPlayer().IsOnMyNetwork() ? Netw.CurrPlayer() : Netw.MyPlayer();

        var targetPos = originalCameraPosition;
        var targetRot = originalCameraRotation;

        var levelNr = SceneHandler.instance.GetCurrentSceneNr();
        var playerCameraSettings = GetPlayerCameraSettings(playerIndex, levelNr);
        if (playerCameraSettings != null)
        {
            targetPos = playerCameraSettings.PlayerInitCameraPos.Position;
            targetRot = Quaternion.Euler(playerCameraSettings.PlayerInitCameraPos.Rotation);
        }

        // geleidelijk bewegen + draaien naar target plek+rot
        var lerpMovement = gameObject.GetAdd<LerpMovement>();
        lerpMovement.MoveToDestination(endPosition: targetPos, duration: 0.6f, destroyGoOnFinished: false);

        var lerpRotation = gameObject.GetAdd<LerpRotation>();
        lerpRotation.RotateTowardsAngle(endRotation: targetRot, duration: 0.6f, destroyGoOnFinished: false);
    }

    private PlayerForLevelInitCameraPos GetPlayerCameraSettings(int playerIndex, int level)
    {
        return PlayerInitCameraPositions.FirstOrDefault(x => 
            x.Level == level && 
            x.PlayerInitCameraPos.Index == playerIndex);
    }
}

public class PlayerForLevelInitCameraPos
{
    public int Level;
    public PlayerInitCameraPos PlayerInitCameraPos;

    public PlayerForLevelInitCameraPos(int index, int level, Vector3 position, Vector3 rotation)
    {
        Level = level;
        PlayerInitCameraPos = new PlayerInitCameraPos(index, position, rotation);
    }
}

public class PlayerInitCameraPos
{
    public int Index;
    public Vector3 Position;
    public Vector3 Rotation;

    public PlayerInitCameraPos(int index, Vector3 position, Vector3 rotation)
    {
        Index = index;
        Position = position;
        Rotation = rotation;
    }
}