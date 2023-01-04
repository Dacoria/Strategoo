using System.Collections.Generic;
using UnityEngine;

public class GameTextManager : BaseEventCallback
{
    protected override void OnNewRoundStarted(List<PlayerScript> players, PlayerScript currentPlayer)
    {  
        Textt.Reset();
        Textt.GameLocal("Game started!");
        Textt.GameLocal(currentPlayer.PlayerName + " turn!");
    }

    protected override void OnNewGameStatus(GameStatus newGameStatus)
    {
        if (newGameStatus == GameStatus.UnitPlacement)
        {
            Textt.GameLocal("Choose your setup - you can swap units");
        }
    }

    protected override void OnNewPlayerTurn(PlayerScript player)
    {
        Textt.GameLocal(player.PlayerName + " turn!");
    }

    protected override void OnEndRound(PlayerScript pWinner)
    {
        Textt.GameLocal("Game has ended! " + pWinner.PlayerName + " wins!!!");
    }
}
