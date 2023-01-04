using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;

public partial class PieceModelHandler : BaseEventCallback
{
    private void ShowModelPieceToOwnPlayer()
    {
        if (!piece.IsAlive)
        {
            return;
        }

        var currentPlayer = GameHandler.instance.GetCurrentPlayer();

        if(Settings.PieceModelAlwaysShown)
        {
            MakePieceModelKnownIfAlive();
            return;
        }
        if(!piece.Owner.IsOnMyNetwork())
        {
            MakePieceModelUnknown();
            return;
        }
        if(piece.Owner == currentPlayer)
        {
            MakePieceModelKnownIfAlive();
        }
        else
        {
            MakePieceModelUnknown();
        }
    }

    private void MakePieceModelKnownIfAlive()
    {
        if (!piece.IsAlive)
        {
            return;
        }

        modelGo.SetActive(true);
        unknownPieceGo?.SetActive(false);
    }

    private void MakePieceModelUnknown()
    {
        if (!piece.IsAlive)
        {
            return;
        }

        if (!Settings.PieceModelAlwaysShown)
        {
            modelGo.SetActive(false);
            unknownPieceGo?.SetActive(true);
        }
        else
        {
            MakePieceModelKnownIfAlive();
        }        
    }

    public GameObject GetModelGo() => modelGo;

    public void SetToInvisible()
    {
        modelGo.SetActive(false);
        unknownPieceGo?.SetActive(false);
    }

    private void UpdateModelViewAndRotation()
    {
        if (Settings.PieceModelAlwaysShown)
        {
            MakePieceModelKnownIfAlive();
            if (IsKnown())
            {
                RotateTowardsCurrentPlayer();
            }
        }
        else
        {
            StopAllCoroutines();
            ShowModelPieceToOwnPlayer();
            if(IsKnown())
            {
                RotateTowardsCurrentPlayer();
            }
        }
    }    

    private void RotateTowardsCurrentPlayer()
    {
        var movementAction = piece.gameObject.GetAdd<PieceMovementAction>();
        movementAction.RotateTowardsDestination(piece.transform.position + Utils.GetRotationDir());
    }
}