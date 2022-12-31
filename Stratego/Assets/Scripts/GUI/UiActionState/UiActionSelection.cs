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

    public UnitSelected UnitSelected;        

    public void ClickOnTile(Hex tile)
    {
        if (UnitSelected != null && !UnitSelected.HexSelectionDate.EnoughTimeForNewEvent())
        {
            return; // 1 click per 100 ms registeren
        }

        if(UnitSelected == null || UnitSelected.ActionSelectionState.In(ActionSelectionState.HexSelected))
        {
            TrySelectNewTile(tile);
        }
        else if (UnitSelected.ActionSelectionState.In(ActionSelectionState.AbilitySelected))
        {
            TryConfirmAbilityTile(tile);
        }
    }

    private void TryConfirmAbilityTile(Hex hex)
    {
        var tileIsConfirmable = UnitSelected.HexIdAbilityOptions?.Any(x => x == hex.HexCoordinates);
        if (tileIsConfirmable == true)
        {
            // DO ACTION            
            ClearTileSelection();
        }
        else
        {
            ClearTileSelection();
        }
    }

    public void TrySelectNewTile(Hex hex)
    {
        if (UnitSelected == null || UnitSelected.HexId != hex.HexCoordinates)
        {
            // HIGHLIGHT
            var selectedTiles = HexGrid.instance.GetTiles(HighlightColorType.White);
            foreach (var tile in selectedTiles)
            {
                tile.DisableHighlight();
            }
            hex.EnableHighlight(HighlightColorType.White);

            UnitSelected = new UnitSelected(hex.HexCoordinates);
            ActionEvents.NewHexSelected?.Invoke(hex.HexCoordinates);
        }
    }

    public void ClickOnNothing()
    {
        if (UnitSelected != null)
        {
            ClearTileSelection();
        }
    }

    public void ClearTileSelection()
    {
        UnitSelected = null;
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