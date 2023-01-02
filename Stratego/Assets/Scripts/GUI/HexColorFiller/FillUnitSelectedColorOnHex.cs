using System.Linq;
using UnityEngine;
using System;
using System.Collections.Generic;

public class FillUnitSelectedColorOnHex: BaseEventCallback
{
    [ComponentInject] private Hex hex;

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
                    if(GameHandler.instance.GameStatus == GameStatus.UnitPlacement)
                    {
                        hex.EnableHighlight(HighlightActionType.SwapOption.GetColor());
                    }
                    else
                    {
                        hex.EnableHighlight(HighlightActionType.EnemyOption.GetColor());
                    }                    
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

    protected override void OnNewHexSelected(Vector3Int hexId)
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

    protected override void OnPieceAbilitySelected(Vector3Int hexIdSelected, AbilityType abilityType, List<Vector3Int> hexIdOptions)
    {
        if (hex.HexCoordinates == hexIdSelected)
        {
            UpdateColor(ColorHexSelectionType.Selected);
        }
        if (hexIdOptions.Any(x => x == hex.HexCoordinates))
        {
            UpdateColor(ColorHexSelectionType.ConfirmOption);
        }
    }

    protected override void OnPieceSwapSelected(Vector3Int hexIdSelected, List<Vector3Int> hexIdOptions)
    {
        if (hex.HexCoordinates == hexIdSelected)
        {
            UpdateColor(ColorHexSelectionType.Selected);
        }
        if (hexIdOptions.Any(x => x == hex.HexCoordinates))
        {
            UpdateColor(ColorHexSelectionType.ConfirmOption);
        }
    }

    protected override void OnHexDeselected() => UpdateColor(ColorHexSelectionType.None);

    protected override void OnDoPieceAbility(Piece piece, Hex hexTarget, AbilityType abilityType) => UpdateColor(ColorHexSelectionType.None);
    protected override void OnSwapPieces(Piece piece1, Piece piece2) => UpdateColor(ColorHexSelectionType.None);
}