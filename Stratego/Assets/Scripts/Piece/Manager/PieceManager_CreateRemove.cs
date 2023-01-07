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

        GoPieces.Add(pieceGo);
    }

    public void RemovePiece(Piece piece)
    {
        GoPieces.Remove(piece);
        Destroy(piece.gameObject);        
    }

    public void RemoveAllPieces()
    {
        foreach (var piece in this.GoPieces)
        {
            Destroy(piece.gameObject);
        }
        GoPieces.Clear();
    }

    public HexPieceSetup GetHexPieceSetup(PlayerScript playerScript)
    {
        var myPieces = GetPieces().Where(x => x.Owner == playerScript).ToList();
        var result = new HexPieceSetup
        {
            HexPiecePlacements = myPieces.Select(x => new HexPiecePlacement
            {
                hexId = x.CurrentHexTile.HexCoordinates,
                PieceSetting = new PieceSetting
                {
                    PieceType = x.PieceType,
                    UnitBaseValue = x.PieceType == PieceType.Unit ?
                        ((Unit)x).Value :
                        -1
                }
            }).ToList()
        };

        return result;
    }
}