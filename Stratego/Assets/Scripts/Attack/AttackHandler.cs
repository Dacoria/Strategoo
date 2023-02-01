using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : BaseEventCallback
{
    public static AttackHandler instance;
    public GameObject AttackEffectPrefab;

    private new void Awake()
    {
        base.Awake();
        instance = this;
    }

    private Piece attacker;
    private Piece defender;
    private int rotationCounter = 0;

    public void DoAttack(Piece attacker, Piece defender)
    {
        this.attacker = attacker;
        this.defender = defender;
        this.rotationCounter = 0;

        RotateTowardsTarget(attacker, defender.CurrentHexTile, rotationSpeed: 0.7f);
        RotateTowardsTarget(defender, attacker.CurrentHexTile, rotationSpeed: 0.7f);
    }

    private void RotateTowardsTarget(Piece piece, Hex hex, float rotationSpeed)
    {
        var movementAction = piece.gameObject.GetAdd<PieceMovementAction>();
        movementAction.RotateTowardsDestination(hex.transform.position, callbackOnFinished: RotationFinished);
    }

    private void RotationFinished()
    {
        rotationCounter++;
        if(rotationCounter >= 2)
        {
            StartAttacking();
        }
    }

    private void StartAttacking()
    {
        Instantiate(AttackEffectPrefab, attacker.transform);
        Instantiate(AttackEffectPrefab, defender.transform);
        StartCoroutine(ProcessAttack(2.5f));
    }

    private IEnumerator ProcessAttack(float seconds)
    {
        yield return Wait4Seconds.Get(seconds);
        var attackResult = AttackCalculater.CalculateAttackResult(attacker, defender);

        if (attackResult == AttackResult.AttackerWins)
        {
            Textt.GameLocal("Attacker wins! Defender is destroyed");
            defender.Die(false);            
        }
        else if (attackResult == AttackResult.DefenderWins)
        {
            Textt.GameLocal("Defender wins! Attacker is destroyed");
            attacker.Die(false);
        }
        else
        {
            Textt.GameLocal("Draw! Both pieces are destroyed");
            defender.Die(false);
            attacker.Die(false);
        }
        AttackFinished(attackResult);
    }

    private void AttackFinished(AttackResult attackResult)
    {
        if(attackResult == AttackResult.AttackerWins)
        {
            var movementAction = attacker.gameObject.GetAdd<PieceMovementAction>();
            movementAction.GoToDestination(defender.CurrentHexTile, 1f, callbackOnFinished: () => RotateToOriginalPos(attacker));
        }
        else if (attackResult == AttackResult.DefenderWins)
        {
            RotateToOriginalPos(defender);
        }
        else
        {
            AttackFaseFinished();
        }
    }

    private void RotateToOriginalPos(Piece piece)
    {
        var movementAction = piece.gameObject.GetAdd<PieceMovementAction>();
        movementAction.RotateTowardsDestination(piece.transform.position + Utils.GetRotationDir(), callbackOnFinished: AttackFaseFinished);
    }

    private void AttackFaseFinished()
    {
        if (defender.PieceType == PieceType.Castle)
        {
            GameHandler.instance.EndRound(pWinner: attacker.Owner);
        }
        else
        {
            GameHandler.instance.EndTurn();
        }
    }
}