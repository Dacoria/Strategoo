using System;
using UnityEngine;

public partial class PieceModelHandler : BaseEventCallback
{
    protected override void OnNewHexSelected(Vector3Int hexSelected) => UpdateModelViewAndRotation();
    protected override void OnUpdatePlayerIndex(PlayerScript playerToUpdate, int playerIndex) => StartCoroutine(UpdateColors());
    protected override void OnDoPieceAbility(Piece pieceDoingAbility, Hex hexTarget, AbilityType abilType, Hex hex2Target)
    {
        if (pieceDoingAbility == piece)
        {
            ShowStartParticleEffect();
        }

        if (abilType.In(AbilityType.ScoutMove, AbilityType.CavalryMove))
        {
            ShowModelsOnDoubleMove(pieceDoingAbility, hex2Target.GetPiece());
        }
        else if (abilType == AbilityType.Movement)
        {
            ShowModelsOnMovement(pieceDoingAbility, hexTarget.GetPiece());
        }
    }

    private void ShowModelsOnMovement(Piece pieceDoingAbility, Piece targetPiece)
    {        
        if (targetPiece != null)
        {
            if(pieceDoingAbility == piece)
            {
                MakePieceModelKnownIfAlive();
            }
            else if (targetPiece == piece)
            {
                MakePieceModelKnownIfAlive();
            }
        }
    }

    private void ShowModelsOnDoubleMove(Piece movingPiece, Piece attackTarget)
    {
        if(movingPiece == piece)
        {
            MakePieceModelKnownIfAlive();
        }
        else if(attackTarget == piece)
        {
            MakePieceModelKnownIfAlive();
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
            StartCoroutine(CR_UpdateModelViewAndRotation(0.10f));
        }
    }

    protected override void OnNewPlayerTurn(PlayerScript player)
    {
        if(piece.IsAlive && Netw.GameHasAiPlayer())
        {
            StartCoroutine(CR_UpdateModelViewAndRotation(0.05f));
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

    protected override void OnPlayerDisconnected(PlayerScript disconnectedPlayer)
    {
        if (piece.IsAlive)
        {
            MakePieceModelKnownIfAlive();
            if (piece.Owner != disconnectedPlayer && piece.PieceType == PieceType.Unit)
            {
                animator?.SetBool(Statics.ANIMATION_BOOL_VICTORY_JUMP, true);
            }
            if (piece.Owner == disconnectedPlayer && piece.PieceType == PieceType.Unit)
            {
                animator?.SetBool(Statics.ANIMATION_TRIGGER_DIE, true);
            }
        }
    }
}