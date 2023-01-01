using System.Collections.Generic;
using System.Linq;

public static class SkillProperty
{
    public static bool IsAbility(this SkillType skillType) => skillType.GetAbility().HasValue;    

    public static AbilityType? GetAbility(this SkillType skillType)
    {
        switch (skillType)
        {
            case SkillType.Movement:
                return AbilityType.Movement;
            case SkillType.ScoutMove:
                return AbilityType.ScoutMove;
            default:
                return null;
        }
    }

    public static List<AbilityType> GetAbilities(this Piece piece)
    {
        var result = new List<AbilityType>();
        foreach(var skill in piece.Skills)
        {
            if (IsAbility(skill))
            {
                result.Add(skill.GetAbility().Value);
            }
        }

        return result;
    }
}