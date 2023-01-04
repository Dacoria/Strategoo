using UnityEngine;

public partial class PieceModelHandler : BaseEventCallback
{
    protected override void OnNewHexSelected(Vector3Int hexSelected) => UpdateModelViewAndRotation();
    protected override void OnUpdatePlayerIndex(int playerId, int playerIndex) => StartCoroutine(UpdateColors());

    protected override void OnDoPieceAbility(Piece pieceDoingAbility, Hex hexTarget, AbilityType abilType)
    {
        if (abilType.In(AbilityType.Movement, AbilityType.ScoutMove))
        {
            if (pieceDoingAbility == piece)
            {
                MakePieceModelKnownIfAlive();
            }

            var pieceOnTargetHex = hexTarget.GetPiece();
            if (pieceOnTargetHex != null && pieceOnTargetHex == piece)
            {
                MakePieceModelKnownIfAlive();
            }
        }
    }

    protected override void OnPieceModelAlwaysShown()
    {
        if (!piece.Owner.IsAi)
        {
            return;
        }

        if (piece.IsAlive)
        {
            UpdateModelViewAndRotation();
        }
    }

    protected override void OnNewPlayerTurn(PlayerScript player)
    {
        if (piece.IsAlive)
        {
            UpdateModelViewAndRotation();
        }
    }

    protected override void OnEndRound(PlayerScript winningPlayer)
    {
        if (piece.IsAlive)
        {
            MakePieceModelKnownIfAlive();
            if (piece.Owner == winningPlayer && piece.PieceType == PieceType.Unit)
            {
                animator?.SetBool(Statics.ANIMATION_BOOL_VICTORY_JUMP, true);
            }
            if (piece.Owner != winningPlayer && piece.PieceType == PieceType.Unit)
            {
                animator?.SetBool(Statics.ANIMATION_TRIGGER_DIE, true);
            }
        }
    }
}