using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;
using Photon.Pun;

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

    public void RemoveOwnPieces(PlayerScript ownerToRemovePieceFor)
    {
        var piecesToRemove = new List<Piece>();
        foreach (var piece in this.GoPieces)
        {
            if(piece.Owner == ownerToRemovePieceFor)
            {
                piecesToRemove.Add(piece);
                Destroy(piece.gameObject);
            }            
        }
        foreach (var pieceToRemove in piecesToRemove)
        {
            GoPieces.Remove(pieceToRemove);
        }
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