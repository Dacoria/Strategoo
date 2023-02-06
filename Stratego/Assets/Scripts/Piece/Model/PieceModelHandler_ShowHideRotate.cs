using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;

public partial class PieceModelHandler : BaseEventCallback
{
    private void ShowModelPieceToPlayer(bool onlyOwnPlayer)
    {
        if (!piece.IsAlive)
        {
            return;
        }

        var currentPlayer = Netw.CurrPlayer();
        var hasAi = Netw.GameHasAiPlayer();

        if (Settings.PieceModelAlwaysShown)
        {
            MakePieceModelKnownIfAlive();
            return;
        }
        if(!piece.Owner.IsOnMyNetwork())
        {
            MakePieceModelUnknown();
            return;
        }
        
        if(hasAi)
        {
            if(currentPlayer == piece.Owner)
            {
                MakePieceModelKnownIfAlive();
                return;
            }
            else
            {
                MakePieceModelUnknown();
                return;
            }
        }
        else
        {
            MakePieceModelKnownIfAlive();
            return;
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

    private void ShowStartParticleEffect()
    {
        PlayParticleEffects.ForEach(effect => effect.Play());
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

    private IEnumerator CR_UpdateModelViewAndRotation(float waitTimeInSeconds)
    {
        yield return Wait4Seconds.Get(waitTimeInSeconds);
        UpdateModelViewAndRotation();
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
            ShowModelPieceToPlayer(onlyOwnPlayer: true);
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