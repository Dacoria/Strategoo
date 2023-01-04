using System.Collections.Generic;
using UnityEngine;

public class PlayerIndex : BaseEventCallback
{
    [ComponentInject] private PlayerScript player;

    public int Index;

    protected override void OnUpdatePlayerIndex(PlayerScript playerToUpdate, int playerIndex)
    {
        if(player.Id == playerToUpdate.Id)
        {
            Index = playerIndex;
        }
    }
}
