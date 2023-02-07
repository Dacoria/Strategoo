using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AbilityPropertyScoutMove : AbilityProperty
{
    public override AbilityType AbilityType => AbilityType.ScoutMove;

    public override HexAbilityOptionType HexAbilityOptionType1 => HexAbilityOptionType.NeighboursInLine;
    public override List<ActionAbilityType> HexAbilityOption1Choices => new List<ActionAbilityType> { ActionAbilityType.Move };

    public override HexAbilityOptionType? HexAbilityOptionType2 => HexAbilityOptionType.DirectNeighbours;
    public override List<ActionAbilityType> HexAbilityOption2Choices => new List<ActionAbilityType> { ActionAbilityType.Attack };
}