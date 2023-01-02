using System.Collections.Generic;
using UnityEngine;

public class PlayerColor : BaseEventCallback
{
    [ComponentInject] private PlayerScript player;

    public Color Color;

    protected override void OnUpdatePlayerIndex(int playerId, int playerIndex)
    {
        if(player.Id == playerId)
        {
            Color = Utils.GetPlayerColorForIndex(playerIndex);
        }
    }    
}
