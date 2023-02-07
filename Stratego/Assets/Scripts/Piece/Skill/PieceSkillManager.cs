using System.Collections.Generic;
using System.Linq;

public static class PieceSkillManager
{    
    public static List<SkillType> GetSkills(Piece piece)
    {
        var result = new List<SkillType>();        

        if (piece.PieceType == PieceType.Unit)
        {
            var unit = (Unit)piece;
            result.Add(SkillType.Movement);

            if (Settings.LightingStratego)
            {
                if (unit.Value == 1)
                {
                    result.Add(SkillType.AttackKillsTenPiece);
                }
                else if (unit.Value == 2)
                {
                    result.Add(SkillType.ScoutMove);
                    result.Add(SkillType.DefuseTrap);
                }
                else if (unit.Value == 6)
                {
                    result.Add(SkillType.CavalryMove);
                }
            }
            else
            {
                if (unit.Value == 1)
                {
                    result.Add(SkillType.AttackKillsTenPiece);
                }
                else if (unit.Value == 2)
                {
                    result.Add(SkillType.ScoutMove);
                }
                else if (unit.Value == 3)
                {
                    result.Add(SkillType.DefuseTrap);
                }
            }

            
        }

        return result;
    }
}