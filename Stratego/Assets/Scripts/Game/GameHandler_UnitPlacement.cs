using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Collections;

public partial class GameHandler : BaseEventCallback
{
    protected override void OnPlayerReadyForGame(PlayerScript playerThatIsReady)
    {
        if (!PhotonNetwork.IsMasterClient) { return; }
        StartCoroutine(TryToStartGame());
    }
 

    private IEnumerator TryToStartGame()
    {
        yield return Wait4Seconds.Get(0.05f);
        var allPlayers = NetworkHelper.instance.GetAllPlayers(isAi: false);
        if(allPlayers.All(x => x.ReadyToStartGame))
        {
            ResetGame();
        }
    }
}