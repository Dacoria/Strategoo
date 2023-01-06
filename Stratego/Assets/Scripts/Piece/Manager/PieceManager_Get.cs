using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;

public partial class PieceManager : BaseEventCallback
{
    private List<Piece> GoPieces;

    public List<Piece> GetPieces(bool? isAlive = null, PieceType? pieceType = null, PlayerScript playerOwner = null)
    {
        var pieces = this.GoPieces;
        if (isAlive.HasValue)
        {
            pieces = pieces.Where(x => x.IsAlive == isAlive.Value).ToList();
        }
        if (pieceType.HasValue)
        {
            pieces = pieces.Where(x => x.PieceType == pieceType.Value).ToList();
        }
        if (playerOwner != null)
        {
            pieces = pieces.Where(x => x.Owner == playerOwner).ToList();
        }

        return pieces;
    }
}
