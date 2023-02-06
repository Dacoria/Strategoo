﻿using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AbilityPropertyScoutMove : AbilityProperty
{
    public override AbilityType AbilityType => AbilityType.ScoutMove;
    public override HexAbilityOptionType HexAbilityOptionType => HexAbilityOptionType.NeighboursInLine;
    public override HexAbilityOptionType? HexAbilityOptionType2 => HexAbilityOptionType.DirectNeighbours;
    public override bool? HexAbilityOption2CanOnlyAttack => true;
}