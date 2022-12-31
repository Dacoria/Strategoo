using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Piece
{
    public override PieceType PieceType => PieceType.Unit;
    public void MoveToNewDestination(Hex tile) => gameObject.GetAdd<UnitMovementAction>().GoToDestination(tile, duration: 1);


}