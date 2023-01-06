using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;

public partial class PieceManager : BaseEventCallback
{
    public static PieceManager instance;

    public new void Awake()
    {
        base.Awake();
        instance = this;
        GoPieces = GameObject.FindObjectsOfType<Piece>().ToList();
    }
}
