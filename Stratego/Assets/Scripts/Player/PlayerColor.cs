using System.Collections.Generic;
using UnityEngine;

public class PlayerColor : BaseEventCallback
{
    [ComponentInject] private PlayerScript player;

    public Color Color;

    protected override void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript currentPlayer)
    {
        // Altijd via deze volgorde; vandaar kloppen de kleuren ook
        for (int i = 0; i < allPlayers.Count; i++)
        {
            if (allPlayers[i] == player)
            {
                Color = GetPlayerColorForIndex(i);
                return;
            }
        }
    }

    private Color GetPlayerColorForIndex(int i) => i switch
    {
        0 => Colorr.Orange,
        1 => Colorr.LightBlue,
        2 => Colorr.Purple,
        3 => Colorr.Yellow,
        _ => Colorr.White,
    };
}
