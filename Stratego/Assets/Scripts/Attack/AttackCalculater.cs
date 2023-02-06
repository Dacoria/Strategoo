using System.Linq;

public static class AttackCalculater
{
    public static AttackResult CalculateAttackResult(Piece attacker, Piece defender)
    {
        if(attacker.PieceType == PieceType.Trap)
        {
            throw new System.Exception("KAN NIET");
        }
        if (attacker.PieceType == PieceType.Castle)
        {
            throw new System.Exception("KAN NIET");
        }

        if(defender.PieceType == PieceType.Trap)
        {
            if(attacker.Skills.Any(x => x == SkillType.DiffuseTrap))
            {
                return AttackResult.AttackerWins;
            }

            if (Settings.LightingStratego)
            {
                return AttackResult.Draw;
            }
            else
            {
                return AttackResult.DefenderWins;
            }
        }

        if (defender.PieceType == PieceType.Castle)
        {
            return AttackResult.AttackerWins;
        }

        var valueAttacker = ((Unit)attacker).Value;
        var valueDefender = ((Unit)defender).Value;

        if(valueDefender == 10 && attacker.Skills.Any(x => x == SkillType.AttackKillsTenPiece))
        {
            return AttackResult.AttackerWins;
        }

        if (valueAttacker > valueDefender)
        {
            return AttackResult.AttackerWins;
        }
        else if (valueAttacker < valueDefender)
        {
            return AttackResult.DefenderWins;
        }
        else
        {
            if (Settings.LightingStratego)
            {
                return AttackResult.AttackerWins;
            }
            else
            {
                return AttackResult.Draw;
            }
        }
    }
}

public enum AttackResult
{
    AttackerWins,
    DefenderWins,
    Draw
}