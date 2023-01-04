using System;
using System.Collections.Generic;
using UnityEngine;

public static class NAE
{
    // network
    public static Action<Piece, Hex, AbilityType> DoPieceAbility;
    public static Action<List<PlayerScript>, PlayerScript> NewRoundStarted;
    public static Action<PlayerScript> NewPlayerTurn;
    public static Action<PlayerScript> EndTurn;
    public static Action<PlayerScript> EndRound;
    public static Action<PlayerScript, int> UpdatePlayerIndex;
    public static Action<Piece, Piece> SwapPieces;
    public static Action<GameStatus> NewGameStatus;
    public static Action<PlayerScript> PlayerReadyForGame;
}