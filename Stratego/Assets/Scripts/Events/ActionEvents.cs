using System;
using System.Collections.Generic;
using UnityEngine;

public static class ActionEvents
{
    // network
    public static Action<PlayerScript, Hex, Hex, AbilityType, int> PlayerAbility;

    public static Action<List<PlayerScript>, PlayerScript> NewRoundStarted;
    public static Action<PlayerScript> NewPlayerTurn;
    public static Action AllPlayersFinishedTurn;
    public static Action<bool, PlayerScript> EndRound;
    public static Action EndGame;
    public static Action<EnemyScript, Hex> EnemyMove;
    public static Action<EnemyScript, PlayerScript> EnemyAttack;
    public static Action<PlayerScript> PlayerDied;
    public static Action<IUnit, Hex> UnitMovingFinished;


    // afgeleiden
    public static Action<PlayerScript, Hex> PlayerMovingFinished;
    public static Action<EnemyScript, Hex> EnemyMovingFinished;

    // local
    public static Action GridLoaded;

    public static Action<IUnit, Hex, int> UnitAttackHit;
    public static Action<PlayerScript, Hex> PlayerHasTeleported;

    public static Action<Animator> DieAnimationFinished;
    public static Action<GameObject> AttackAnimationFinished;

    public static Action<EnemyScript, Hex, int> EnemyAttackHit;
}