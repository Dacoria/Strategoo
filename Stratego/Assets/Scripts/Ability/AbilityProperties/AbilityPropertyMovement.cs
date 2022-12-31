using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AbilityPropertyMovement : AbilityProperty
{
    public override AbilityType AbilityType => AbilityType.Movement;
    public override bool EventImmediatelyFinished => false;
    public override float Duration => 2.3f;
    public override bool TargetHexIsRelativeToPlayer => true;
    public override bool NeedsTileTarget => true;
}