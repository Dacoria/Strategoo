using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ResetCameraPosition : BaseEventCallback
{
    private Vector3 originalCameraPosition;
    private Quaternion originalCameraRotation;

    public bool Setting_ResetCameraOnNewTurnEvent;

    private List<PlayerInitCameraPos> PlayerInitCameraPositions = new List<PlayerInitCameraPos>
    {
        new PlayerInitCameraPos(0, new Vector3(-5,6,-3), new Vector3(38,48,0)),
        new PlayerInitCameraPos(1, new Vector3(16,6,14), new Vector3(38,-130,0)),
        new PlayerInitCameraPos(2, new Vector3(-5,6,14), new Vector3(38,130,0)),
        new PlayerInitCameraPos(3, new Vector3(16,6,-3), new Vector3(38,-48,0)),
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
        if(Setting_ResetCameraOnNewTurnEvent && player.IsOnMyNetwork() && player.IsAi)
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
        // AI + aan de beurt? pak die, anders pak je eigen char
        var player = Netw.CurrPlayer().IsOnMyNetwork() ? Netw.CurrPlayer() : Netw.MyPlayer();

        var targetPos = originalCameraPosition;
        var targetRot = originalCameraRotation;

        // geleidelijk bewegen + draaien naar target plek+rot
        var lerpMovement = gameObject.GetAdd<LerpMovement>();
        lerpMovement.MoveToDestination(endPosition: targetPos, duration: 0.6f, destroyGoOnFinished: false);

        var lerpRotation = gameObject.GetAdd<LerpRotation>();
        lerpRotation.RotateTowardsAngle(endRotation: targetRot, duration: 0.6f, destroyGoOnFinished: false);
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