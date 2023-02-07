using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;
using System;

public static class AbilityProperties
{
    public static AbilityProperty GetProperties(this AbilityType abilityType) => abilityPropertiesMap.Single(x => x.AbilityType == abilityType);

    private static List<AbilityProperty> _abilityPropertiesMap;
    private static List<AbilityProperty> abilityPropertiesMap
    {
        get
        {
            if(_abilityPropertiesMap == null)
            {
                _abilityPropertiesMap = CreateAbilityPropertiesMap();
            }
            return _abilityPropertiesMap;
        }
    }

    private static List<AbilityProperty> CreateAbilityPropertiesMap()
    {
        var abilitiesList = new List<AbilityProperty>();
        foreach (Type t in Utils.FindDerivedTypes(Assembly.GetExecutingAssembly(), typeof(AbilityProperty)))
        {
            var instance = (AbilityProperty)Activator.CreateInstance(t);
            abilitiesList.Add(instance);
        }

        return abilitiesList;
    }    
}