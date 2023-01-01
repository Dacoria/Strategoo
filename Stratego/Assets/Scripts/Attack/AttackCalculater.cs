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
            return AttackResult.Draw;
        }
        if (defender.PieceType == PieceType.Castle)
        {
            return AttackResult.AttackerWins;
        }

        var valueAttacker = ((Unit)attacker).Value;
        var valueDefender = ((Unit)defender).Value;

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
            return AttackResult.Draw;
        }
    }
}

public enum AttackResult
{
    AttackerWins,
    DefenderWins,
    Draw
}