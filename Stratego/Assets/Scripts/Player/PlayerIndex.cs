using System.Collections.Generic;
using UnityEngine;

public class PlayerIndex : BaseEventCallback
{
    [ComponentInject] private PlayerScript player;

    public int Index;

    protected override void OnUpdatePlayerIndex(int playerId, int playerIndex)
    {
        if(playerId == player.Id)
        {
            Index = playerIndex;
        }
    }
}
