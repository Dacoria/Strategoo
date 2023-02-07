using System;
using System.Collections;


public partial class PieceMovementHandler : BaseEventCallback {
    private IEnumerator DoCavaltyAbility(Hex hex, Hex hex2)
    {
        yield return Wait4Seconds.Get(0.05f); // wacht tot de modellen geactiveerd zijn (ook via dit event)

        if (hex.HexCoordinates == hex2.HexCoordinates)
        {
            Textt.GameLocal(pieceScript.Owner.PlayerName + " starts moving!");
            pieceMovement.GoToDestination(hex, duration: 1f, callbackOnFinished: RotateToOriginalPosAndEnd);
        }
        else if (!hex2.HexCoordinates.HasPiece())
        {
            Textt.GameLocal(pieceScript.Owner.PlayerName + " starts moving twice!");
            pieceMovement.GoToDestination(hex, duration: 0.65f, callbackOnFinished: () =>
                pieceMovement.GoToDestination(hex2, duration: 0.65f, callbackOnFinished: RotateToOriginalPosAndEnd)
            );
            
        }
        else
        {
            Textt.GameLocal(pieceScript.Owner.PlayerName + " starts moving and attack!");
            pieceMovement.GoToDestination(hex, duration: 1f, callbackOnFinished: () => AttackPieceOnHex(hex2));
        }
    }
}