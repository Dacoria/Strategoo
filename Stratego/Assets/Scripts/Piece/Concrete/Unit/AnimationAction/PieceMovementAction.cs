using System;
using UnityEngine;

public class PieceMovementAction : BaseEventCallback
{
    [ComponentInject] private Animator animator;
    [ComponentInject] private Piece piece;

    private Hex originalDestinationHex;

    public void GoToDestination(Hex hex, float duration)
    {
        var unitBoostFactor = new Vector3(0, 1, 0); // UNIT STAAT 1 HOGER ALTIJD
        var endPos = hex.transform.position + unitBoostFactor;
        originalDestinationHex = hex;

        RotateTowardsDestination(endPos, callbackOnFinished: () => MoveToDestination(endPos, duration, callbackOnFinished: OnDestinationReached));
    }

    private void OnDestinationReached()
    {
        piece.SetCurrentHexTile(originalDestinationHex);
        ActionEvents.PieceMovingFinished?.Invoke(piece, originalDestinationHex);
    }

    public void RotateTowardsDestination(Vector3 endPosition, Action callbackOnFinished = null)
    {
        var lerpRotation = gameObject.GetAdd<LerpRotation>();
        lerpRotation.RotateTowardsDestination(endPosition, callbackOnFinished: callbackOnFinished);
    }

    private void MoveToDestination(Vector3 endPosition, float duration, Action callbackOnFinished = null)
    {
        var lerpMovement = gameObject.GetAdd<LerpMovement>();
        lerpMovement.MoveToDestination(endPosition, duration, callbackOnFinished: callbackOnFinished, animator: animator);
    }
}