using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class AbilityProperty
{
    public abstract AbilityType AbilityType { get; }
    public abstract HexAbilityOptionType HexAbilityOptionType { get; }
}