using System;
using System.Collections;


public partial class PieceMovementHandler : BaseEventCallback {    
    
    private IEnumerator DoScoutAbility(Hex hex, Hex hex2)
    {
        yield return Wait4Seconds.Get(0.05f); // wacht tot de modellen geactiveerd zijn (ook via dit event)

        if(hex.HexCoordinates == hex2.HexCoordinates)
        {
            Textt.GameLocal(pieceScript.Owner.PlayerName + " starts moving!");
            pieceMovement.GoToDestination(hex, duration: 1f, callbackOnFinished: RotateToOriginalPosAndEnd);
        }
        else
        {
            Textt.GameLocal(pieceScript.Owner.PlayerName + " starts moving and attack!");
            pieceMovement.GoToDestination(hex, duration: 1f, callbackOnFinished: () => AttackPieceOnHex(hex2));
        }
    }
}