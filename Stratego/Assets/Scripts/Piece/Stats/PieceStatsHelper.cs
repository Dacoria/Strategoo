using System.Collections.Generic;
using System.Linq;

public static class PieceStatsHelper
{
    public static List<PieceStats> PieceStatsList = new List<PieceStats>
    {
        new PieceStats
        {
            PieceType = PieceType.Unit,
            Abilities = new List<AbilityType>
            {
                AbilityType.Movement
            }
        },
        new PieceStats
        {
            PieceType = PieceType.Castle,
            Abilities = new List<AbilityType>()
        },
        new PieceStats
        {
            PieceType = PieceType.Trap,
            Abilities = new List<AbilityType>()
        },
    };   

    public static List<AbilityType> GetAbilities(PieceType pieceType) =>
        PieceStatsList.Single(x => x.PieceType == pieceType).Abilities;
}