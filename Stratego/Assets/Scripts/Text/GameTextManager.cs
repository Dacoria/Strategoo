using System.Collections.Generic;
using UnityEngine;

public class GameTextManager : BaseEventCallback
{
    protected override void OnNewRoundStarted(List<PlayerScript> players, PlayerScript currentPlayer)
    {      
        if(GameHandler.instance.GameStatus != GameStatus.PlayerFase)
        {
            return;
        }
        Textt.Reset();
        Textt.GameLocal("New turn! - pick your moves ");
    }
}
