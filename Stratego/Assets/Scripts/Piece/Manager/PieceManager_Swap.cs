using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;

public partial class PieceManager : BaseEventCallback
{
    protected override void OnSwapPieces(Piece piece1, Piece piece2)
    {
        var hexPiece1 = piece1.CurrentHexTile;
        var hexPiece2 = piece2.CurrentHexTile;

        HexGrid.instance.MovePieceToNewTile(piece1, hexPiece2);
        HexGrid.instance.MovePieceToNewTile(piece2, hexPiece1);
    }
}