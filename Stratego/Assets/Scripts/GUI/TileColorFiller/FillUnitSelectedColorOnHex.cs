using System.Linq;
using UnityEngine;
using System;

public class FillUnitSelectedColorOnHex : MonoBehaviourSlowUpdate
{
    [ComponentInject] private Hex hex;

    private void Awake()
    {
        this.ComponentInject();
    }

    private bool hexBeingHoveredOver;
    private ColorHexSelectionType currentColorTileSelection;

    void Start()
    {
        ActionEvents.NewHexSelected += OnNewHexSelected;
        ActionEvents.HexDeselected += OnHexDeselected;
    }

    protected override void SlowUpdate()
    {
        if (UiActionSelection.instance?.PieceSelected?.HexIdAbilityOptions != null &&
            UiActionSelection.instance.PieceSelected.HexIdAbilityOptions.Any(x => x == hex.HexCoordinates))
        {
            currentColorTileSelection = ColorHexSelectionType.ConfirmOption;
        }
        else
        {
            if(currentColorTileSelection == ColorHexSelectionType.ConfirmOption)
            {
                currentColorTileSelection = ColorHexSelectionType.None;
            }
        }

        UpdateColor();
    }

    private void UpdateColor()
    {
        hexBeingHoveredOver = UiHoverOverHex.Instance.HexHoveredOver == hex;
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
                if(false && hexBeingHoveredOver)
                {
                    hex.EnableHighlight(HighlightActionType.HoverOption.GetColor());
                }
                else if(hexHasPiece)
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
            currentColorTileSelection = ColorHexSelectionType.Selected;
        }
        else
        {
            currentColorTileSelection = ColorHexSelectionType.None;
        }
    }

    private void OnHexDeselected()
    {
        currentColorTileSelection = ColorHexSelectionType.None;
    }

    private void OnDestroy()
    {
        ActionEvents.NewHexSelected -= OnNewHexSelected;
        ActionEvents.HexDeselected -= OnHexDeselected;
    }
}