using System.Collections.Generic;
using UnityEngine;

public class GameTextManager : BaseEventCallback
{
    protected override void OnNewRoundStarted(List<PlayerScript> players, PlayerScript currentPlayer)
    {  
        Textt.Reset();
        Textt.GameLocal("Game started!");
        Textt.GameLocal("New turn! - pick your moves ");
    }

    protected override void OnNewGameStatus(GameStatus newGameStatus)
    {
        if (newGameStatus == GameStatus.UnitPlacement)
        {
            Textt.GameLocal("Choose your setup - you can swap units");
        }
    }    
}
