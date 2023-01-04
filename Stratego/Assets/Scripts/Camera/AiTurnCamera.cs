using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AiTurnCamera : BaseEventCallback
{
    [ComponentInject] private ResetCameraPositionScript resetCameraPositionScript;

    protected override void OnNewPlayerTurn(PlayerScript player)
    {
        if(!Settings.RotateTowardsMyPlayer)
        {
            return;
        }
        var allPlayers = NetworkHelper.instance.GetAllPlayers();
        if(allPlayers.Any(x => x.IsAi))
        {
            resetCameraPositionScript.ResetCameraToPlayer(player.Index);
        }
    }
}
