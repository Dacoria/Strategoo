using System;
using System.Collections.Generic;
using UnityEngine;

public static class ActionEvents
{
    // network
    public static Action<Piece, Hex, AbilityType> PieceAbility;

    public static Action<List<PlayerScript>, PlayerScript> NewRoundStarted;
    public static Action<PlayerScript> NewPlayerTurn;
    public static Action AllPlayersFinishedTurn;
    public static Action<PlayerScript> EndRound;
    public static Action EndGame;

    public static Action<Piece, Hex> PieceMove;
    public static Action<Piece, Hex> PieceMovingFinished;


    // local
    public static Action GridLoaded;

    public static Action<Piece, Hex, int> UnitAttackHit;
    public static Action<Animator> DieAnimationFinished;
    public static Action<GameObject> AttackAnimationFinished;

    public static Action<Vector3Int> NewHexSelected;
    public static Action HexDeselected;
}