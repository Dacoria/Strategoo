using System;
using UnityEngine;

public class PieceMovementAction : BaseEventCallback
{
    [ComponentInject(Required.OPTIONAL)] private Animator animator;
    [ComponentInject] private Piece piece;

    private Hex originalDestinationHex;

    public void GoToDestination(Hex hex, float duration, Action callbackOnFinished = null)
    {
        var unitBoostFactor = new Vector3(0, 1, 0); // UNIT STAAT 1 HOGER ALTIJD
        var endPos = hex.transform.position + unitBoostFactor;
        originalDestinationHex = hex;

        RotateTowardsDestination(endPos, callbackOnFinished: () => MoveToDestination(endPos, duration, callbackOnFinished: () => OnDestinationReached(callbackOnFinished)));
    }

    private void OnDestinationReached(Action callbackOnFinished)
    {
        HexGrid.instance.MovePieceToNewTile(piece, originalDestinationHex);
        callbackOnFinished?.Invoke();
    }

    public void RotateTowardsDestination(Vector3 endPosition, float rotationSpeed = 1, Action callbackOnFinished = null)
    {
        var model = piece.GetModel();

        if (model != null)
        {
            var lerpRotation = piece.GetModel().GetAdd<LerpRotation>(); // rotatie op model (voor unit value weergave)
            lerpRotation.RotateTowardsDestination(endPosition, rotationSpeed: rotationSpeed, callbackOnFinished: callbackOnFinished);
        }
        else
        {
            callbackOnFinished();
        }
    }

    private void MoveToDestination(Vector3 endPosition, float duration, Action callbackOnFinished = null)
    {
        var lerpMovement = gameObject.GetAdd<LerpMovement>();
        lerpMovement.MoveToDestination(endPosition, duration, callbackOnFinished: callbackOnFinished, animator: animator);
    }
}