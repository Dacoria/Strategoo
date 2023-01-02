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
        else if (PieceSelected.ActionSelectionState.In(HexPieceSelectionState.PieceAbilitySelected))
        {
            TryConfirmAbilityTile(hex);
        }
    }

    private void TryConfirmAbilityTile(Hex hex)
    {
        var hexIsConfirmable = PieceSelected.HexIdAbilityOptions?.Any(x => x == hex.HexCoordinates);
        if (hexIsConfirmable == true)
        {
            var pieceOnHex = PieceSelected.HexId.GetPiece();
            ActionEvents.DoPieceAbility?.Invoke(pieceOnHex, hex, PieceSelected.Ability);
            ClearHexSelection();
        }
        else
        {
            ClearHexSelection();
        }
    }

    public void TrySelectNewHex(Hex hexSelected)
    {
        if (PieceSelected == null || PieceSelected.HexId != hexSelected.HexCoordinates)
        {
            PieceSelected = new PieceSelected(hexSelected.HexCoordinates);
            ActionEvents.NewHexSelected?.Invoke(hexSelected.HexCoordinates);
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
        ActionEvents.HexDeselected?.Invoke();
    }
}