using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;

public partial class PieceManager : BaseEventCallback
{
    private List<Piece> _goPieces;
    private List<Piece> GoPieces
    {
        get
        {
            if(_goPieces == null)
            {
                _goPieces = GameObject.FindObjectsOfType<Piece>().ToList();
            }
            return _goPieces;
        }
    }

    public List<Piece> GetPieces(bool? isAlive = null, PieceType? pieceType = null)
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

        return pieces;
    }
}
