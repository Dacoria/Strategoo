using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AbilityPropertyMovement : AbilityProperty
{
    public override AbilityType AbilityType => AbilityType.Movement;

    public override HexAbilityOptionType HexAbilityOptionType1 => HexAbilityOptionType.DirectNeighbours;
    public override List<ActionAbilityType> HexAbilityOption1Choices => new List<ActionAbilityType> { ActionAbilityType.Move, ActionAbilityType.Attack };
}