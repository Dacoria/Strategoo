using System;
using System.Collections.Generic;
using UnityEngine;

public static class ActionEvents
{
    // network
    public static Action<Piece, Hex, AbilityType> DoPieceAbility;

    public static Action<List<PlayerScript>, PlayerScript> NewRoundStarted;
    public static Action<PlayerScript> NewPlayerTurn;
    public static Action AllPlayersFinishedTurn;
    public static Action<PlayerScript> EndTurn;
    public static Action<PlayerScript> EndRound;

    public static Action<Piece, Hex> PieceMovingFinished;

    public static Action<int, int> UpdatePlayerIndex;
    public static Action<PlayerScript> PlayerIsVictorious;

    public static Action<Piece, Piece> SwapPieces;

    public static Action<GameStatus> NewGameStatus;

    public static Action<PlayerScript> PlayerReadyForGame;



    // local
    public static Action GridLoaded;

    public static Action<Piece, Hex, int> UnitAttackHit;
    public static Action<Animator> DieAnimationFinished;
    public static Action<GameObject> AttackAnimationFinished;

    public static Action<Vector3Int> NewHexSelected;
    public static Action HexDeselected;

    public static Action<Vector3Int, AbilityType, List<Vector3Int>> PieceAbilitySelected;
    public static Action<Vector3Int, List<Vector3Int>> PieceSwapSelected;

    public static Action AiPieceModelAlwaysKnownIsUpdated;

}