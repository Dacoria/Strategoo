using System.Collections.Generic;
using UnityEngine;

public class PlayerColor : BaseEventCallback
{
    [ComponentInject] private PlayerScript player;

    public Color Color;

    protected override void OnUpdatePlayerIndex(PlayerScript playerToUpdate, int playerIndex)
    {
        if(player.Id == playerToUpdate.Id)
        {
            Color = Utils.GetPlayerColorForIndex(playerIndex);
        }
    }    
}
