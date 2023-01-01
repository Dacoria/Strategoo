using System.Linq;
using UnityEngine;
using System;
using System.Collections.Generic;

public class FillUnitSelectedColorOnHex: BaseEventCallback
{
    [ComponentInject] private Hex hex;


    void Start()
    {
        ActionEvents.NewHexSelected += OnNewHexSelected;
        ActionEvents.HexDeselected += OnHexDeselected;
        ActionEvents.PieceAbilitySelected += OnPieceAbilitySelected;
        ActionEvents.DoPieceAbility += OnDoPieceAbility;
    }    

    private void UpdateColor(ColorHexSelectionType currentColorTileSelection)
    {
        var hexHasPiece = hex.HasPiece();
        switch (currentColorTileSelection)
        {
            case ColorHexSelectionType.None:
                hex.DisableHighlight();
                break;
            case ColorHexSelectionType.Selected:
                hex.EnableHighlight(HighlightActionType.SelectTile.GetColor());
                break;
            case ColorHexSelectionType.ConfirmOption:                
                if(hexHasPiece)
                {
                    hex.EnableHighlight(HighlightActionType.EnemyOption.GetColor());
                }
                else
                {
                    hex.EnableHighlight(HighlightActionType.MoveOption.GetColor());
                }
                break;
            default:
                throw new Exception("");
        }
    }

    private void OnNewHexSelected(Vector3Int hexId)
    {
        if (hex.HexCoordinates == hexId)
        {
            UpdateColor(ColorHexSelectionType.Selected);
        }
        else
        {
            UpdateColor(ColorHexSelectionType.None);
        }
    }

    private void OnPieceAbilitySelected(Vector3Int hexSelected, AbilityType abilityType, List<Vector3Int> hexOptions)
    {
        if (hex.HexCoordinates == hexSelected)
        {
            UpdateColor(ColorHexSelectionType.Selected);
        }
        if (hexOptions.Any(x => x == hex.HexCoordinates))
        {
            UpdateColor(ColorHexSelectionType.ConfirmOption);
        }
    }

    private void OnHexDeselected() => UpdateColor(ColorHexSelectionType.None);

    private void OnDoPieceAbility(Piece piece, Hex hexTarget, AbilityType abilityType) => UpdateColor(ColorHexSelectionType.None);

    private void OnDestroy()
    {
        ActionEvents.NewHexSelected -= OnNewHexSelected;
        ActionEvents.HexDeselected -= OnHexDeselected;
        ActionEvents.PieceAbilitySelected -= OnPieceAbilitySelected;
        ActionEvents.DoPieceAbility -= OnDoPieceAbility;
    }
}