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
        if (IsOverwritten("OnNewRoundStarted")) NAE.NewRoundStarted += OnNewRoundStarted;
        if (IsOverwritten("OnNewPlayerTurn")) NAE.NewPlayerTurn += OnNewPlayerTurn;
        if (IsOverwritten("OnEndTurn")) NAE.EndTurn += OnEndTurn;
        if (IsOverwritten("OnEndRound")) NAE.EndRound += OnEndRound;
        if (IsOverwritten("OnPieceAbilitySelected")) AE.PieceAbilitySelected += OnPieceAbilitySelected;
        if (IsOverwritten("OnPieceSwapSelected")) AE.PieceSwapSelected += OnPieceSwapSelected;
        if (IsOverwritten("OnDoPieceAbility")) NAE.DoPieceAbility += OnDoPieceAbility;
        if (IsOverwritten("OnUpdatePlayerIndex")) NAE.UpdatePlayerIndex += OnUpdatePlayerIndex;
        if (IsOverwritten("OnNewHexSelected")) AE.NewHexSelected += OnNewHexSelected;
        if (IsOverwritten("OnHexDeselected")) AE.HexDeselected += OnHexDeselected;
        if (IsOverwritten("OnNewGameStatus")) NAE.NewGameStatus += OnNewGameStatus;
        if (IsOverwritten("OnSwapPieces")) NAE.SwapPieces += OnSwapPieces;
        if (IsOverwritten("OnPlayerReadyForGame")) NAE.PlayerReadyForGame += OnPlayerReadyForGame;
        if (IsOverwritten("OnPieceModelAlwaysShown")) AE.PieceModelAlwaysShown += OnPieceModelAlwaysShown;


    }

    protected void OnDisable()
    {
        if (IsOverwritten("OnGridLoaded")) AE.GridLoaded -= OnGridLoaded;
        if (IsOverwritten("OnNewRoundStarted")) NAE.NewRoundStarted -= OnNewRoundStarted;
        if (IsOverwritten("OnNewPlayerTurn")) NAE.NewPlayerTurn -= OnNewPlayerTurn;
        if (IsOverwritten("OnEndTurn")) NAE.EndTurn -= OnEndTurn;
        if (IsOverwritten("OnEndRound")) NAE.EndRound -= OnEndRound;
        if (IsOverwritten("OnPieceAbilitySelected")) AE.PieceAbilitySelected -= OnPieceAbilitySelected;
        if (IsOverwritten("OnPieceSwapSelected")) AE.PieceSwapSelected += OnPieceSwapSelected;
        if (IsOverwritten("OnDoPieceAbility")) NAE.DoPieceAbility -= OnDoPieceAbility;
        if (IsOverwritten("OnUpdatePlayerIndex")) NAE.UpdatePlayerIndex -= OnUpdatePlayerIndex;
        if (IsOverwritten("OnNewHexSelected")) AE.NewHexSelected -= OnNewHexSelected;
        if (IsOverwritten("OnHexDeselected")) AE.HexDeselected -= OnHexDeselected;
        if (IsOverwritten("OnNewGameStatus")) NAE.NewGameStatus -= OnNewGameStatus;
        if (IsOverwritten("OnSwapPieces")) NAE.SwapPieces -= OnSwapPieces;
        if (IsOverwritten("OnPlayerReadyForGame")) NAE.PlayerReadyForGame -= OnPlayerReadyForGame;
        if (IsOverwritten("OnPieceModelAlwaysShown")) AE.PieceModelAlwaysShown -= OnPieceModelAlwaysShown;



    }
        

    protected virtual void OnGridLoaded() { }    
    protected virtual void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript player) { }
    protected virtual void OnNewPlayerTurn(PlayerScript player) { }
    protected virtual void OnEndTurn(PlayerScript player) { }
    protected virtual void OnEndRound(PlayerScript pWinner) { }
    protected virtual void OnPieceAbilitySelected(Vector3Int hexId, AbilityType ability, List<Vector3Int> hexIdOptions) { }
    protected virtual void OnPieceSwapSelected(Vector3Int hexId, List<Vector3Int> hexIdOptions) { }
    protected virtual void OnDoPieceAbility(Piece piece, Hex hexTarget, AbilityType abilType) { }
    protected virtual void OnUpdatePlayerIndex(PlayerScript player, int playerIndex) { }
    protected virtual void OnNewHexSelected(Vector3Int hexSelected) { }
    protected virtual void OnHexDeselected() { }
    protected virtual void OnNewGameStatus(GameStatus newGameStatus) { }
    protected virtual void OnSwapPieces(Piece piece1, Piece piece2) { }
    protected virtual void OnPlayerReadyForGame(PlayerScript playerThatIsReady) { }
    protected virtual void OnPieceModelAlwaysShown() { }



    // GEEN ABSTRACTE CLASSES!
    private bool IsOverwritten(string functionName)
    {
        var type = GetType();
        var method = type.GetMember(functionName, BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Instance);
        return method.Length > 0;
    }
}