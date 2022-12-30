using System.Collections.Generic;
using System.Collections.ObjectModel;

public enum AbilityType
{
    None,
    Movement
}

public static class AbilitySetup
{
    public static List<AbilitySetting> AbilitySettings = new List<AbilitySetting>
    {
        new AbilitySetting{Type = AbilityType.Movement,EventImmediatelyFinished = false, Duration = 2.3f, TargetHexIsRelativeToPlayer = true },
    };
}