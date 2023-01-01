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
        StartCoroutine(ProcessAttack(3f));
    }

    private IEnumerator ProcessAttack(float seconds)
    {
        yield return Wait4Seconds.Get(seconds);
        var attackResult = AttackCalculater.CalculateAttackResult(attacker, defender);

        if (attackResult == AttackResult.AttackerWins)
        {
            defender.Die(false);
        }
        else if (attackResult == AttackResult.DefenderWins)
        {
            attacker.Die(false);
        }
        else
        {
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
            movementAction.GoToDestination(defender.CurrentHexTile, 1.5f, callbackOnFinished: () => RotateToOriginalPos(attacker));
        }
        if (attackResult == AttackResult.DefenderWins)
        {
            RotateToOriginalPos(defender);
        }
    }

    private void RotateToOriginalPos(Piece piece)
    {
        var movementAction = piece.gameObject.GetAdd<PieceMovementAction>();
        movementAction.RotateTowardsDestination(piece.transform.position + new Vector3(0,0,-1));
    }
}
