using System.Collections.Generic;
using UnityEngine;
using System;

public class PieceSelected
{
    public Vector3Int HexId;
    public DateTime HexSelectionDate;

    public HexPieceSelectionState ActionSelectionState;

    public AbilityType Ability;
    public List<Vector3Int> HexIdAbilityOptions;

    public PieceSelected(Vector3Int hexId)
    {
        HexId = hexId;
        HexSelectionDate = DateTime.Now;
        ActionSelectionState = HexPieceSelectionState.HexSelected;
    }

    public void SetAbilitySelected(AbilityType ability, List<Vector3Int> hexIdAbilityOptions)
    {
        Ability = ability;
        HexIdAbilityOptions = hexIdAbilityOptions;
        ActionSelectionState = HexPieceSelectionState.PieceAbilitySelected;
    }
}