using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class AbilityProperty
{
    public abstract AbilityType AbilityType { get; }
    public abstract HexAbilityOptionType HexAbilityOptionType { get; }
    public virtual HexAbilityOptionType? HexAbilityOptionType2 { get; } = null;
    public virtual bool? HexAbilityOption2CanOnlyAttack { get; } = null;

    public bool HasFollowUpTileSelection => HexAbilityOptionType2.HasValue;
}