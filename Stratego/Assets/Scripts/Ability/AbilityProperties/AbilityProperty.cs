using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class AbilityProperty
{
    public abstract AbilityType AbilityType { get; }

    public abstract HexAbilityOptionType HexAbilityOptionType1 { get; }
    public abstract List<ActionAbilityType> HexAbilityOption1Choices { get; }

    public virtual HexAbilityOptionType? HexAbilityOptionType2 { get; } = null;
    public virtual List<ActionAbilityType> HexAbilityOption2Choices => new List<ActionAbilityType>();

    public bool HasFollowUpTileSelection => HexAbilityOptionType2.HasValue;
}