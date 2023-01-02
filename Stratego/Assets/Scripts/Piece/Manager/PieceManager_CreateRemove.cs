using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;

public partial class PieceManager : BaseEventCallback
{   
    public void CreatePiece(PieceType pieceType, int unitBaseValue, Hex hexToSpawnPiece, int playerIndex)
    {
        var piecePrefab = GetPiecePrefab(pieceType);
        var pieceGo = Instantiate(piecePrefab, hexToSpawnPiece.GetGoStructure().transform);
        pieceGo.Owner = playerIndex.GetPlayerById();
        pieceGo.transform.rotation = new Quaternion(0, 180, 0, 0);

        if (pieceType == PieceType.Unit)
        {
            ((Unit)pieceGo).Value = unitBaseValue;
        }

        _goPieces.Add(pieceGo);
    }

    public void RemoveAllPieces()
    {
        foreach (var piece in this.GoPieces)
        {
            Destroy(piece.gameObject);
        }
        _goPieces.Clear();
    }
}
