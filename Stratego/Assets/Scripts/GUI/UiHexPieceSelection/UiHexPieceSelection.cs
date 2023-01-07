using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UiHexPieceSelection : BaseEventCallback
{
    public PieceSelected PieceSelected;
    public PieceSelected FirstPieceSelected;

    protected override void OnPieceAbilitySelected(Vector3Int hexId, AbilityType ability, List<Vector3Int> hexOptions)
    {
        if(PieceSelected?.HexId == hexId)
        {
            PieceSelected.SetAbilitySelected(ability, hexOptions);
        }    
    }

    protected override void OnPieceSwapSelected(Vector3Int hexId, List<Vector3Int> hexOptions)
    {
        if (PieceSelected?.HexId == hexId)
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
        if(Settings.DebugDestroyPiece)
        {
            if(hex.HasPiece())
            {
                var piece = hex.GetPiece();
                PieceManager.instance.RemovePiece(piece);
            }
            return; // DEBUG
        }

        if(PieceSelected == null || PieceSelected.ActionSelectionState.In(HexPieceSelectionState.HexSelected))
        {
            TrySelectNewHex(hex);
        }

        else if (PieceSelected.ActionSelectionState.In(HexPieceSelectionState.PieceAbilitySelected, HexPieceSelectionState.SwapPieceSelected))
        {
            TryConfirmOptionTile(hex);
        }
    }

    private void TryConfirmOptionTile(Hex hexConfirmed)
    {
        var hexIsConfirmable = PieceSelected.HexIdOptions?.Any(x => x == hexConfirmed.HexCoordinates);
        if (hexIsConfirmable == true)
        {
            if(PieceSelected.ActionSelectionState == HexPieceSelectionState.PieceAbilitySelected)
            {
                ConfirmAbility(PieceSelected.Ability, hexConfirmed);
            }
            else if (PieceSelected.ActionSelectionState == HexPieceSelectionState.SwapPieceSelected)
            {
                ConfirmSwap(hexConfirmed);
            }
        }
        else
        {
            ClearHexSelection();
        }
    }

    private void ConfirmSwap(Hex hexConfirmed)
    {
        var pieceOnHex = PieceSelected.HexId.GetPiece();
        AE.SwapPieces?.Invoke(pieceOnHex, hexConfirmed.GetPiece());
        ClearHexSelection();
    }

    private void ConfirmAbility(AbilityType ability, Hex hexConfirmed)
    {
        var abilityProperties = ability.GetProperties();

        if(abilityProperties.HasFollowUpTileSelection)
        {
            if (IsFollowUpPieceSelected())
            {
                var firstHexConfirmed = PieceSelected.HexId.GetHex();
                var pieceDoingAbility = FirstPieceSelected.HexId.GetPiece();
                NetworkAE.instance.DoPieceAbility(pieceDoingAbility, firstHexConfirmed, PieceSelected.Ability, hexConfirmed);
            }
            else
            {                
                StartFollowUpPieceSelection(hexConfirmed);
                return;
            }
        }
        else
        {
            var pieceOnHex = PieceSelected.HexId.GetPiece();
            NetworkAE.instance.DoPieceAbility(pieceOnHex, hexConfirmed, PieceSelected.Ability, hexTarget2: null);
        }

        ClearHexSelection();
    }    

    private bool IsFollowUpPieceSelected()
    {
        return FirstPieceSelected != null;
    }

    private void StartFollowUpPieceSelection(Hex hexConfirmed)
    {
        var originalHexId = PieceSelected.HexId;
        var abilitySelected = PieceSelected.Ability;

        FirstPieceSelected = new PieceSelected(originalHexId);
        FirstPieceSelected.SetAbilitySelected(abilitySelected, PieceSelected.HexIdOptions);

        PieceSelected = new PieceSelected(hexConfirmed.HexCoordinates);

        var hexesToSelect = hexConfirmed.HexCoordinates.GetHexOptions(abilitySelected.GetProperties().HexAbilityOptionType2.Value);
        var hexPieceOwner = originalHexId.GetPiece().Owner;

        var hexesResult = hexesToSelect.Where(x => x.HasPiece() && x.GetPiece().Owner != hexPieceOwner).ToList();
        hexesResult.Add(hexConfirmed.HexCoordinates);

        AE.PieceAbilitySelected?.Invoke(hexConfirmed.HexCoordinates, abilitySelected, hexesResult);
    }

    public void TrySelectNewHex(Hex hexSelected)
    {
        var currentPlayer = Netw.CurrPlayer();
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
        FirstPieceSelected = null;
        AE.HexDeselected?.Invoke();
    }
}