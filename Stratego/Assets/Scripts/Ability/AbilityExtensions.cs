using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public static class AbilityExtensions
{
    public static bool GetEventImmediatelyFinished(this AbilityType abilityType) => abilityType == AbilityType.None ? true : AbilitySetup.AbilitySettings.Single(x => x.Type == abilityType).EventImmediatelyFinished;
    public static bool GetTargetHexIsRelativeToPlayer(this AbilityType abilityType) => abilityType == AbilityType.None ? false : AbilitySetup.AbilitySettings.Single(x => x.Type == abilityType).TargetHexIsRelativeToPlayer;
    public static float GetDuration(this AbilityType abilityType) => abilityType == AbilityType.None ? 0f : AbilitySetup.AbilitySettings.Single(x => x.Type == abilityType).Duration;    
}