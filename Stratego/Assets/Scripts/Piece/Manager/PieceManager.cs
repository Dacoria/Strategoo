using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;

public class PieceManager : BaseEventCallback
{
    public static PieceManager instance;

    public Unit UnitPrefab;
    public Castle CastlePrefab;
    public Trap TrapPrefab;

    public new void Awake()
    {
        base.Awake();
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

    private Piece GetPiecePrefab(PieceType pieceType) => pieceType switch
    {
        PieceType.Unit => UnitPrefab,
        PieceType.Castle => CastlePrefab,
        PieceType.Trap => TrapPrefab,
        _ => throw new NotImplementedException(),
    };

    public void CreatePiece(PieceType pieceType, int unitBaseValue, Hex hexToSpawnPiece)
    {
        var piecePrefab = GetPiecePrefab(pieceType);
        var pieceGo = Instantiate(piecePrefab, hexToSpawnPiece.GetGoStructure().transform);
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
