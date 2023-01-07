using System;
using System.Collections.Generic;
using UnityEngine;

public static class NAE_NoCalling
{
    // network
    public static Action<Piece, Hex, AbilityType, Hex> DoPieceAbility;
    public static Action<List<PlayerScript>, PlayerScript> NewRoundStarted;
    public static Action<PlayerScript> NewPlayerTurn;
    public static Action<PlayerScript> EndTurn;
    public static Action<PlayerScript> EndRound;
    public static Action<PlayerScript, int> UpdatePlayerIndex;
    public static Action<PlayerScript, HexPieceSetup> PlayerReadyForGame;
}