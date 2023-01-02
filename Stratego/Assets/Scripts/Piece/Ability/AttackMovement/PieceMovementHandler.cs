using System;
using System.Collections;
using UnityEngine;

public class PieceMovementHandler : BaseEventCallback
{
    [ComponentInject] private Piece pieceScript;

    private PieceMovementAction pieceMovement;

    private new void Awake()
    {
        base.Awake();
        pieceMovement = gameObject.AddComponent<PieceMovementAction>();
    }

    protected override void OnDoPieceAbility(Piece piece, Hex hex, AbilityType abilType)
    {
        if(piece == pieceScript && abilType == AbilityType.Movement)
        {
            StartCoroutine(DoMovementAbility(hex));
        }
    }

    private IEnumerator DoMovementAbility(Hex hex)
    {
        yield return Wait4Seconds.Get(0.05f); // wacht tot de modellen geactiveerd zijn (ook via dit event)

        var isMyPiece = false; // TODO FIXEN
        if(hex.HasPiece())
        {
            if(isMyPiece)
            {
                throw new Exception("piece wil naar ally locatie toe!");
            }
            else
            {
                var pieceToAttack = hex.GetPiece();
                AttackHandler.instance.DoAttack(attacker: pieceScript, defender: pieceToAttack);
            }
        }
        else
        {
            pieceMovement.GoToDestination(hex, duration: 1.5f, callbackOnFinished: RotateToOriginalPos);
        }
    }

    private void RotateToOriginalPos()
    {
        var movementAction = pieceScript.gameObject.GetAdd<PieceMovementAction>();
        movementAction.RotateTowardsDestination(pieceScript.transform.position + new Vector3(0, 0, -1));
    }
}
