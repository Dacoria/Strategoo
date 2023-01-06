using System;
using System.Collections.Generic;
using UnityEngine;

public static class AE
{   
    public static Action GridLoaded;
    public static Action<Piece, Hex, int> UnitAttackHit;
    public static Action<Animator> DieAnimationFinished;
    public static Action<GameObject> AttackAnimationFinished;
    public static Action<Vector3Int> NewHexSelected;
    public static Action HexDeselected;
    public static Action<Vector3Int, AbilityType, List<Vector3Int>> PieceAbilitySelected;
    public static Action<Vector3Int, List<Vector3Int>> PieceSwapSelected;
    public static Action PieceModelAlwaysShown;
    public static Action<Piece, Piece> SwapPieces;

}