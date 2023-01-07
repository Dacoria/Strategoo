using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class AbilityProperty
{
    public abstract AbilityType AbilityType { get; }
    public abstract HexAbilityOptionType HexAbilityOptionType { get; }
    public abstract HexAbilityOptionType? HexAbilityOptionType2 { get; }
    public bool HasFollowUpTileSelection => HexAbilityOptionType2.HasValue;
}