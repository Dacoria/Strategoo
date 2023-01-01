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
            Color = GetPlayerColorForIndex(playerIndex);
        }
    }


    private Color GetPlayerColorForIndex(int i) => i switch
    {
        1 => Colorr.Orange,
        2 => Colorr.LightBlue,
        3 => Colorr.Purple,
        4 => Colorr.Yellow,
        _ => Colorr.White,
    };
}
