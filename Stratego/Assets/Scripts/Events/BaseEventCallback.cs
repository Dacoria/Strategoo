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
        if (IsOverwritten("OnGridLoaded")) AE.GridLoaded += OnGridLoaded;
        if (IsOverwritten("OnLevelSetupFinished")) AE.LevelSetupFinished += OnLevelSetupFinished;        
        if (IsOverwritten("OnNewRoundStarted")) NAE_NoCalling.NewRoundStarted += OnNewRoundStarted;
        if (IsOverwritten("OnNewPlayerTurn")) NAE_NoCalling.NewPlayerTurn += OnNewPlayerTurn;
        if (IsOverwritten("OnEndTurn")) NAE_NoCalling.EndTurn += OnEndTurn;
        if (IsOverwritten("OnEndRound")) NAE_NoCalling.EndRound += OnEndRound;
        if (IsOverwritten("OnPieceAbilitySelected")) AE.PieceAbilitySelected += OnPieceAbilitySelected;
        if (IsOverwritten("OnPieceSwapSelected")) AE.PieceSwapSelected += OnPieceSwapSelected;
        if (IsOverwritten("OnDoPieceAbility")) NAE_NoCalling.DoPieceAbility += OnDoPieceAbility;
        if (IsOverwritten("OnNewHexSelected")) AE.NewHexSelected += OnNewHexSelected;
        if (IsOverwritten("OnHexDeselected")) AE.HexDeselected += OnHexDeselected;
        if (IsOverwritten("OnSwapPieces")) AE.SwapPieces += OnSwapPieces;
        if (IsOverwritten("OnPieceModelAlwaysShown")) AE.PieceModelAlwaysShown += OnPieceModelAlwaysShown;
        if (IsOverwritten("OnPieceModelAlwaysShown")) NAE_NoCalling.UpdatePlayerIndex += OnUpdatePlayerIndex;
        if (IsOverwritten("OnPlayerReadyForGame")) NAE_NoCalling.PlayerReadyForGame += OnPlayerReadyForGame;
        if (IsOverwritten("OnPlayerDisconnected")) AE.PlayerDisconnected += OnPlayerDisconnected;
    }

    protected void OnDisable()
    {
        if (IsOverwritten("OnGridLoaded")) AE.GridLoaded -= OnGridLoaded;
        if (IsOverwritten("OnLevelSetupFinished")) AE.LevelSetupFinished -= OnLevelSetupFinished;
        if (IsOverwritten("OnNewRoundStarted")) NAE_NoCalling.NewRoundStarted -= OnNewRoundStarted;
        if (IsOverwritten("OnNewPlayerTurn")) NAE_NoCalling.NewPlayerTurn -= OnNewPlayerTurn;
        if (IsOverwritten("OnEndTurn")) NAE_NoCalling.EndTurn -= OnEndTurn;
        if (IsOverwritten("OnEndRound")) NAE_NoCalling.EndRound -= OnEndRound;
        if (IsOverwritten("OnPieceAbilitySelected")) AE.PieceAbilitySelected -= OnPieceAbilitySelected;
        if (IsOverwritten("OnPieceSwapSelected")) AE.PieceSwapSelected += OnPieceSwapSelected;
        if (IsOverwritten("OnDoPieceAbility")) NAE_NoCalling.DoPieceAbility -= OnDoPieceAbility;
        if (IsOverwritten("OnNewHexSelected")) AE.NewHexSelected -= OnNewHexSelected;
        if (IsOverwritten("OnHexDeselected")) AE.HexDeselected -= OnHexDeselected;
        if (IsOverwritten("OnSwapPieces")) AE.SwapPieces -= OnSwapPieces;
        if (IsOverwritten("OnPieceModelAlwaysShown")) AE.PieceModelAlwaysShown -= OnPieceModelAlwaysShown;
        if (IsOverwritten("OnPieceModelAlwaysShown")) NAE_NoCalling.UpdatePlayerIndex -= OnUpdatePlayerIndex;
        if (IsOverwritten("OnPlayerReadyForGame")) NAE_NoCalling.PlayerReadyForGame -= OnPlayerReadyForGame;
        if (IsOverwritten("OnPlayerDisconnected")) AE.PlayerDisconnected -= OnPlayerDisconnected;
    }
    protected virtual void OnGridLoaded() { }
    protected virtual void OnLevelSetupFinished() { }   
    protected virtual void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript player) { }
    protected virtual void OnNewPlayerTurn(PlayerScript player) { }
    protected virtual void OnEndTurn(PlayerScript player) { }
    protected virtual void OnEndRound(PlayerScript pWinner) { }
    protected virtual void OnPieceAbilitySelected(Vector3Int hexId, AbilityType ability, List<Vector3Int> hexIdOptions) { }
    protected virtual void OnPieceSwapSelected(Vector3Int hexId, List<Vector3Int> hexIdOptions) { }
    protected virtual void OnDoPieceAbility(Piece piece, Hex hexTarget, AbilityType abilType, Hex hexTarget2) { }
    protected virtual void OnNewHexSelected(Vector3Int hexSelected) { }
    protected virtual void OnHexDeselected() { }
    protected virtual void OnSwapPieces(Piece piece1, Piece piece2) { }
    protected virtual void OnPieceModelAlwaysShown() { }
    protected virtual void OnUpdatePlayerIndex(PlayerScript player, int newPlayerIndex) { }
    protected virtual void OnPlayerReadyForGame(PlayerScript player, HexPieceSetup hexPieceSetup) { }
    protected virtual void OnPlayerDisconnected(PlayerScript player) { }
    

    // GEEN ABSTRACTE CLASSES!
    private bool IsOverwritten(string functionName)
    {
        var type = GetType();
        var method = type.GetMember(functionName, BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Instance);
        return method.Length > 0;
    }
}