using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AbilityPropertyScoutMove : AbilityProperty
{
    public override AbilityType AbilityType => AbilityType.ScoutMove;
    public override HexAbilityOptionType HexAbilityOptionType => HexAbilityOptionType.NeighboursInLine;
}