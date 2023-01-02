﻿using System.Collections.Generic;
using System.Linq;

public static class PieceSkillManager
{    
    public static List<SkillType> GetSkills(Piece piece)
    {
        var result = new List<SkillType>();
        if(piece.PieceType == PieceType.Unit)
        {
            var unit = (Unit)piece;
            result.Add(SkillType.Movement);
            
            if(unit.Value == 1)
            {
                result.Add(SkillType.AttackKillsTenPiece);
            }
            else if (unit.Value == 2)
            {
                result.Add(SkillType.ScoutMove);
            }
            else if (unit.Value == 3)
            {
                result.Add(SkillType.DiffuseTrap);
            }
        }

        return result;
    }
}