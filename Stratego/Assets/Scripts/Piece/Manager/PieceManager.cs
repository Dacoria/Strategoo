using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class PieceManager : MonoBehaviour
{
    public static PieceManager instance;

    void Awake()
    {
        instance = this;
    }

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

    public Piece GetPiece(int pieceId, bool? isAlive = null, PieceType? pieceType = null)
    {
        var pieces = GetPieces(isAlive: isAlive, pieceType: pieceType);
        return pieces.FirstOrDefault(x => x.Id == pieceId);
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
