using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AbilityPropertyCavalryMove : AbilityProperty
{
    public override AbilityType AbilityType => AbilityType.CavalryMove;
    public override HexAbilityOptionType HexAbilityOptionType => HexAbilityOptionType.DirectNeighbours;
    public override HexAbilityOptionType? HexAbilityOptionType2 => HexAbilityOptionType.DirectNeighbours;
    public override bool? HexAbilityOption2CanOnlyAttack => false;

}