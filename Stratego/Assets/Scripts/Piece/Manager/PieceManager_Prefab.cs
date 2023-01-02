using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;

public partial class PieceManager : BaseEventCallback
{
    public Unit UnitPrefab;
    public Castle CastlePrefab;
    public Trap TrapPrefab;
    public GameObject UnknownPrefab;

    private Piece GetPiecePrefab(PieceType pieceType) => pieceType switch
    {
        PieceType.Unit => UnitPrefab,
        PieceType.Castle => CastlePrefab,
        PieceType.Trap => TrapPrefab,
        _ => throw new NotImplementedException(),
    };
}
