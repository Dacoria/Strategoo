using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public abstract class BaseEventCallback : MonoBehaviour
{
    protected void Awake()
    {
        this.ComponentInject();
    }

    protected void OnEnable()
    {
        if (IsOverwritten("OnGridLoaded")) ActionEvents.GridLoaded += OnGridLoaded;
        if (IsOverwritten("OnNewRoundStarted")) ActionEvents.NewRoundStarted += OnNewRoundStarted;
        if (IsOverwritten("OnNewPlayerTurn")) ActionEvents.NewPlayerTurn += OnNewPlayerTurn;
        if (IsOverwritten("OnAllPlayersFinishedTurn")) ActionEvents.AllPlayersFinishedTurn += OnAllPlayersFinishedTurn;
        if (IsOverwritten("OnEndRound")) ActionEvents.EndRound += OnEndRound;
        if (IsOverwritten("OnEndGame")) ActionEvents.EndGame += OnEndGame;
        if (IsOverwritten("OnPieceAbility")) ActionEvents.PieceAbility += OnPieceAbility;
    }    

    protected void OnDisable()
    {
        if (IsOverwritten("OnGridLoaded")) ActionEvents.GridLoaded -= OnGridLoaded;
        if (IsOverwritten("OnNewRoundStarted")) ActionEvents.NewRoundStarted -= OnNewRoundStarted;
        if (IsOverwritten("OnNewPlayerTurn")) ActionEvents.NewPlayerTurn -= OnNewPlayerTurn;
        if (IsOverwritten("OnAllPlayersFinishedTurn")) ActionEvents.AllPlayersFinishedTurn -= OnAllPlayersFinishedTurn;
        if (IsOverwritten("OnEndRound")) ActionEvents.EndRound -= OnEndRound;
        if (IsOverwritten("OnEndGame")) ActionEvents.EndGame -= OnEndGame;
        if (IsOverwritten("OnPieceAbility")) ActionEvents.PieceAbility -= OnPieceAbility;

    }

    protected virtual void OnGridLoaded() { }    
    protected virtual void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript player) { }
    protected virtual void OnNewPlayerTurn(PlayerScript player) { }
    protected virtual void OnAllPlayersFinishedTurn() { }
    protected virtual void OnEndRound(PlayerScript pWinner) { }
    protected virtual void OnEndGame() { }
    protected virtual void OnPieceAbility(Piece piece, Hex hex, AbilityType abilType) { }


    // GEEN ABSTRACTE CLASSES!
    private bool IsOverwritten(string functionName)
    {
        var type = GetType();
        var method = type.GetMember(functionName, BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Instance);
        return method.Length > 0;
    }
}