using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class AbilityProperty
{
    public abstract AbilityType AbilityType { get; }
    public abstract bool EventImmediatelyFinished { get; }
    public abstract float Duration { get; }
    public abstract bool TargetHexIsRelativeToPlayer { get; }
    public abstract bool NeedsTileTarget { get; }
}