using System;
using System.Collections;


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
        if (piece == pieceScript && abilType == AbilityType.Movement)
        {
            StartCoroutine(DoMovementAbility(hex));
        }
        if (piece == pieceScript && abilType == AbilityType.ScoutMove)
        {
            StartCoroutine(DoMovementAbility(hex));
        }
    }

    private IEnumerator DoMovementAbility(Hex hex)
    {
        yield return Wait4Seconds.Get(0.05f); // wacht tot de modellen geactiveerd zijn (ook via dit event)
                
        if (hex.HasPiece())
        {
            var pieceToAttack = hex.GetPiece();
            if (pieceScript.Owner == pieceToAttack.Owner)
            {
                throw new Exception("Je valt je eigen units aan!");
            }
            else
            {
                Textt.GameLocal(pieceScript.Owner.PlayerName + " starts attacking!");
                AttackHandler.instance.DoAttack(attacker: pieceScript, defender: pieceToAttack);
            }
        }
        else
        {
            Textt.GameLocal(pieceScript.Owner.PlayerName + " starts moving!");
            pieceMovement.GoToDestination(hex, duration: 1.5f, callbackOnFinished: RotateToOriginalPos);
        }
    }

    private void RotateToOriginalPos()
    {
        var movementAction = pieceScript.gameObject.GetAdd<PieceMovementAction>();
        movementAction.RotateTowardsDestination(pieceScript.transform.position + Utils.GetRotationDir(), callbackOnFinished: RotationToOriginalFinished);
    }

    private void RotationToOriginalFinished()
    {
        GameHandler.instance.EndTurn();
    }
}