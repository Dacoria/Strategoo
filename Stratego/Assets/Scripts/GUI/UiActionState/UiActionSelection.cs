using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class UiActionSelection : MonoBehaviour
{
    public static UiActionSelection instance;

    private void Awake()
    {
        instance = this;
    }

    public UnitSelected PieceSelected;        

    public void ClickOnHex(Hex hex)
    {
        if (PieceSelected != null && !PieceSelected.HexSelectionDate.EnoughTimeForNewEvent())
        {
            return; // 1 click per 100 ms registeren
        }

        if(PieceSelected == null || PieceSelected.ActionSelectionState.In(ActionSelectionState.HexSelected))
        {
            TrySelectNewHex(hex);
        }
        else if (PieceSelected.ActionSelectionState.In(ActionSelectionState.AbilitySelected))
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
            ActionEvents.PieceAbility?.Invoke(pieceOnHex, hex, PieceSelected.Ability);
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
            PieceSelected = new UnitSelected(hexSelected.HexCoordinates);
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

public class UnitSelected
{
    public Vector3Int HexId;
    public DateTime HexSelectionDate;

    public ActionSelectionState ActionSelectionState;

    public AbilityType Ability;
    public List<Vector3Int> HexIdAbilityOptions;

    public UnitSelected(Vector3Int hexId)
    {
        HexId = hexId;
        HexSelectionDate = DateTime.Now;
        ActionSelectionState = ActionSelectionState.HexSelected;
    }

    public void SetAbilitySelected(AbilityType ability, List<Vector3Int> hexIdAbilityOptions)
    {
        Ability = ability;
        HexIdAbilityOptions = hexIdAbilityOptions;
        ActionSelectionState = ActionSelectionState.AbilitySelected;
    }
}