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
        if (IsOverwritten("OnEndRound")) ActionEvents.EndRound += OnEndRound;
        if (IsOverwritten("OnPieceAbilitySelected")) ActionEvents.PieceAbilitySelected += OnPieceAbilitySelected;
        if (IsOverwritten("OnPieceSwapSelected")) ActionEvents.PieceSwapSelected += OnPieceSwapSelected;
        if (IsOverwritten("OnDoPieceAbility")) ActionEvents.DoPieceAbility += OnDoPieceAbility;
        if (IsOverwritten("PieceMovingFinished")) ActionEvents.PieceMovingFinished += OnPieceMovingFinished;
        if (IsOverwritten("OnUpdatePlayerIndex")) ActionEvents.UpdatePlayerIndex += OnUpdatePlayerIndex;
        if (IsOverwritten("OnNewHexSelected")) ActionEvents.NewHexSelected += OnNewHexSelected;
        if (IsOverwritten("OnHexDeselected")) ActionEvents.HexDeselected += OnHexDeselected;
        if (IsOverwritten("OnNewGameStatus")) ActionEvents.NewGameStatus += OnNewGameStatus;
        if (IsOverwritten("OnSwapPieces")) ActionEvents.SwapPieces += OnSwapPieces;
        if (IsOverwritten("OnPlayerReadyForGame")) ActionEvents.PlayerReadyForGame += OnPlayerReadyForGame;
    }

    protected void OnDisable()
    {
        if (IsOverwritten("OnGridLoaded")) ActionEvents.GridLoaded -= OnGridLoaded;
        if (IsOverwritten("OnNewRoundStarted")) ActionEvents.NewRoundStarted -= OnNewRoundStarted;
        if (IsOverwritten("OnNewPlayerTurn")) ActionEvents.NewPlayerTurn -= OnNewPlayerTurn;
        if (IsOverwritten("OnEndRound")) ActionEvents.EndRound -= OnEndRound;
        if (IsOverwritten("OnPieceAbilitySelected")) ActionEvents.PieceAbilitySelected -= OnPieceAbilitySelected;
        if (IsOverwritten("OnPieceSwapSelected")) ActionEvents.PieceSwapSelected += OnPieceSwapSelected;
        if (IsOverwritten("OnDoPieceAbility")) ActionEvents.DoPieceAbility -= OnDoPieceAbility;
        if (IsOverwritten("PieceMovingFinished")) ActionEvents.PieceMovingFinished -= OnPieceMovingFinished;
        if (IsOverwritten("OnUpdatePlayerIndex")) ActionEvents.UpdatePlayerIndex -= OnUpdatePlayerIndex;
        if (IsOverwritten("OnNewHexSelected")) ActionEvents.NewHexSelected -= OnNewHexSelected;
        if (IsOverwritten("OnHexDeselected")) ActionEvents.HexDeselected -= OnHexDeselected;
        if (IsOverwritten("OnNewGameStatus")) ActionEvents.NewGameStatus -= OnNewGameStatus;
        if (IsOverwritten("OnSwapPieces")) ActionEvents.SwapPieces -= OnSwapPieces;
        if (IsOverwritten("OnPlayerReadyForGame")) ActionEvents.PlayerReadyForGame -= OnPlayerReadyForGame;


    }

    protected virtual void OnGridLoaded() { }    
    protected virtual void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript player) { }
    protected virtual void OnNewPlayerTurn(PlayerScript player) { }
    protected virtual void OnEndRound(PlayerScript pWinner) { }
    protected virtual void OnPieceAbilitySelected(Vector3Int hexId, AbilityType ability, List<Vector3Int> hexIdOptions) { }
    protected virtual void OnPieceSwapSelected(Vector3Int hexId, List<Vector3Int> hexIdOptions) { }
    protected virtual void OnDoPieceAbility(Piece piece, Hex hexTarget, AbilityType abilType) { }
    protected virtual void OnPieceMovingFinished(Piece piece, Hex newHex) { }
    protected virtual void OnUpdatePlayerIndex(int playerId, int playerIndex) { }
    protected virtual void OnNewHexSelected(Vector3Int hexSelected) { }
    protected virtual void OnHexDeselected() { }
    protected virtual void OnNewGameStatus(GameStatus newGameStatus) { }
    protected virtual void OnSwapPieces(Piece piece1, Piece piece2) { }
    protected virtual void OnPlayerReadyForGame(PlayerScript playerThatIsReady) { }


    // GEEN ABSTRACTE CLASSES!
    private bool IsOverwritten(string functionName)
    {
        var type = GetType();
        var method = type.GetMember(functionName, BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Instance);
        return method.Length > 0;
    }
}