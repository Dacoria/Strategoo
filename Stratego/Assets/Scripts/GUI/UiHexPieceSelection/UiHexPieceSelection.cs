using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UiHexPieceSelection : BaseEventCallback
{
    public PieceSelected PieceSelected;     

    protected override void OnPieceAbilitySelected(Vector3Int hexId, AbilityType ability, List<Vector3Int> hexOptions)
    {
        if(PieceSelected.HexId == hexId)
        {
            PieceSelected.SetAbilitySelected(ability, hexOptions);
        }    
    }

    protected override void OnPieceSwapSelected(Vector3Int hexId, List<Vector3Int> hexOptions)
    {
        if (PieceSelected.HexId == hexId)
        {
            PieceSelected.SetSwapSelected(hexOptions);
        }
    }

    public void ClickOnHex(Hex hex)
    {
        if (PieceSelected != null && !PieceSelected.HexSelectionDate.EnoughTimeForNewEvent())
        {
            return; // 1 click per 100 ms registeren
        }

        if(PieceSelected == null || PieceSelected.ActionSelectionState.In(HexPieceSelectionState.HexSelected))
        {
            TrySelectNewHex(hex);
        }

        else if (PieceSelected.ActionSelectionState.In(HexPieceSelectionState.PieceAbilitySelected, HexPieceSelectionState.SwapPieceSelected))
        {
            TryConfirmAbilityTile(hex);
        }
    }

    private void TryConfirmAbilityTile(Hex hex)
    {
        var hexIsConfirmable = PieceSelected.HexIdOptions?.Any(x => x == hex.HexCoordinates);
        if (hexIsConfirmable == true)
        {
            var pieceOnHex = PieceSelected.HexId.GetPiece();
            if(PieceSelected.ActionSelectionState == HexPieceSelectionState.PieceAbilitySelected)
            {
                NAE.DoPieceAbility?.Invoke(pieceOnHex, hex, PieceSelected.Ability);
            }
            else if (PieceSelected.ActionSelectionState == HexPieceSelectionState.SwapPieceSelected)
            {
                NAE.SwapPieces?.Invoke(pieceOnHex, hex.GetPiece());
            }

            ClearHexSelection();
        }
        else
        {
            ClearHexSelection();
        }
    }

    public void TrySelectNewHex(Hex hexSelected)
    {
        var currentPlayer = GameHandler.instance.GetCurrentPlayer();
        if (!currentPlayer.IsOnMyNetwork())
        {
            return;
        }
        if(GameHandler.instance.GameStatus.In(GameStatus.GameFase, GameStatus.RoundEnded))
        {
            if (hexSelected.HasPiece() && hexSelected.GetPiece().Owner != currentPlayer)
            {
                return;
            }
        }        

        if (PieceSelected == null || PieceSelected.HexId != hexSelected.HexCoordinates)
        {
            PieceSelected = new PieceSelected(hexSelected.HexCoordinates);
            AE.NewHexSelected?.Invoke(hexSelected.HexCoordinates);
        }
    }

    public void ClickOnNothing()
    {
        if (PieceSelected != null)
        {
            ClearHexSelection();
        }
    }

    public void ClearHexSelection()
    {
        PieceSelected = null;
        AE.HexDeselected?.Invoke();
    }
}