using System.Collections.Generic;
using UnityEngine;

public class GameTextManager : BaseEventCallback
{
    protected override void OnNewRoundStarted(List<PlayerScript> players, PlayerScript currentPlayer)
    {  
        Textt.Reset();
        Textt.GameLocal("== NEW GAME STARTED ==");
        Textt.GameLocal(currentPlayer.PlayerName + " turn!");
    }

    protected override void OnGridLoaded()
    {
        var text = "";
        text += ("== UNIT PLACEMENT ==");
        text += ("\n");
        text += ("Select a unit to start swapping");
        text += ("\n");
        text += ("Press the button ready when you're finished");
        text += ("\n");
        text += ("When both players are ready, the game starts!");
        Textt.GameLocal(text);
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
