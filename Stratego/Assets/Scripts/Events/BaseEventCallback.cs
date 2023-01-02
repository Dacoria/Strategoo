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
        if (IsOverwritten("OnPieceAbilitySelected")) ActionEvents.PieceAbilitySelected += OnPieceAbilitySelected;
        if (IsOverwritten("OnDoPieceAbility")) ActionEvents.DoPieceAbility += OnDoPieceAbility;
        if (IsOverwritten("PieceMovingFinished")) ActionEvents.PieceMovingFinished += OnPieceMovingFinished;
        if (IsOverwritten("OnUpdatePlayerIndex")) ActionEvents.UpdatePlayerIndex += OnUpdatePlayerIndex;
        if (IsOverwritten("OnNewHexSelected")) ActionEvents.NewHexSelected += OnNewHexSelected;
        if (IsOverwritten("OnHexDeselected")) ActionEvents.HexDeselected += OnHexDeselected;
        if (IsOverwritten("OnPlayerIsVictorious")) ActionEvents.PlayerIsVictorious += OnPlayerIsVictorious;
    }

    protected void OnDisable()
    {
        if (IsOverwritten("OnGridLoaded")) ActionEvents.GridLoaded -= OnGridLoaded;
        if (IsOverwritten("OnNewRoundStarted")) ActionEvents.NewRoundStarted -= OnNewRoundStarted;
        if (IsOverwritten("OnNewPlayerTurn")) ActionEvents.NewPlayerTurn -= OnNewPlayerTurn;
        if (IsOverwritten("OnAllPlayersFinishedTurn")) ActionEvents.AllPlayersFinishedTurn -= OnAllPlayersFinishedTurn;
        if (IsOverwritten("OnEndRound")) ActionEvents.EndRound -= OnEndRound;
        if (IsOverwritten("OnEndGame")) ActionEvents.EndGame -= OnEndGame;
        if (IsOverwritten("OnPieceAbilitySelected")) ActionEvents.PieceAbilitySelected -= OnPieceAbilitySelected;
        if (IsOverwritten("OnDoPieceAbility")) ActionEvents.DoPieceAbility -= OnDoPieceAbility;
        if (IsOverwritten("PieceMovingFinished")) ActionEvents.PieceMovingFinished -= OnPieceMovingFinished;
        if (IsOverwritten("OnUpdatePlayerIndex")) ActionEvents.UpdatePlayerIndex -= OnUpdatePlayerIndex;
        if (IsOverwritten("OnNewHexSelected")) ActionEvents.NewHexSelected -= OnNewHexSelected;
        if (IsOverwritten("OnHexDeselected")) ActionEvents.HexDeselected -= OnHexDeselected;
        if (IsOverwritten("OnPlayerIsVictorious")) ActionEvents.PlayerIsVictorious -= OnPlayerIsVictorious;

    }

    protected virtual void OnGridLoaded() { }    
    protected virtual void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript player) { }
    protected virtual void OnNewPlayerTurn(PlayerScript player) { }
    protected virtual void OnAllPlayersFinishedTurn() { }
    protected virtual void OnEndRound(PlayerScript pWinner) { }
    protected virtual void OnEndGame() { }
    protected virtual void OnPieceAbilitySelected(Vector3Int hexId, AbilityType ability, List<Vector3Int> hexOptions) { }
    protected virtual void OnDoPieceAbility(Piece piece, Hex hexTarget, AbilityType abilType) { }
    protected virtual void OnPieceMovingFinished(Piece piece, Hex newHex) { }
    protected virtual void OnUpdatePlayerIndex(int playerId, int playerIndex) { }
    protected virtual void OnNewHexSelected(Vector3Int hexSelected) { }
    protected virtual void OnHexDeselected() { }
    protected virtual void OnPlayerIsVictorious(PlayerScript winningPlayer) { }



    // GEEN ABSTRACTE CLASSES!
    private bool IsOverwritten(string functionName)
    {
        var type = GetType();
        var method = type.GetMember(functionName, BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Instance);
        return method.Length > 0;
    }
}