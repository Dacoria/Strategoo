using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AbilityPropertyMovement : AbilityProperty
{
    public override AbilityType AbilityType => AbilityType.Movement;
    public override HexAbilityOptionType HexAbilityOptionType => HexAbilityOptionType.DirectNeighbours;
    public override HexAbilityOptionType? HexAbilityOptionType2 => null;
}