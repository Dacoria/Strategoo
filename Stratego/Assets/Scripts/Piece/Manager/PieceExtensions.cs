using System.Linq;
using UnityEngine;

public static class PieceExtensions
{
    public static Piece GetPiece(this Hex hex, bool? isAlive = true)
    {
        var pieces = PieceManager.instance.GetPieces(isAlive: isAlive);
        var pieceOnTile = pieces.FirstOrDefault(x => x.CurrentHexTile.HexCoordinates == hex.HexCoordinates);

        return pieceOnTile;
    }

    public static Piece GetPiece(this Vector3Int tileV3, bool? isAlive = true)
    {
        var hex = HexGrid.instance.GetHexAt(tileV3);
        if (hex != null)
        {
            return hex.GetPiece(isAlive: isAlive);
        }

        return null;
    }


    public static bool HasPiece(this Hex hex, bool? isAlive = true) => hex.GetPiece(isAlive: isAlive) != null;
    public static bool HasPiece(this Vector3Int tileV3, bool? isAlive = true) => tileV3.GetPiece(isAlive) != null;    
}