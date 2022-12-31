using System;
using UnityEngine;

public class PieceAttackMovementHandler : BaseEventCallback
{
    [ComponentInject] private Piece pieceScript;

    private PieceMovementAction pieceMovement;

    private new void Awake()
    {
        pieceMovement = gameObject.AddComponent<PieceMovementAction>();
        base.Awake();   
    }

    protected override void OnPieceAbility(Piece piece, Hex hex, AbilityType abilType)
    {
        if(piece == pieceScript && abilType == AbilityType.Movement)
        {
            DoPieceAbility(hex);            
        }
    }

    private void DoPieceAbility(Hex hex)
    {
        var isMyPiece = false; // TODO FIXEN
        if(hex.HasPiece())
        {
            if(isMyPiece)
            {
                Debug.Log("BUG! --> piece wil naar ally locatie toe!");
            }
            else
            {
                Debug.Log("Start Attack");
            }            
        }
        else
        {
            pieceMovement.GoToDestination(hex, duration: 1.5f);
        }
    }
}
