public partial class PieceMovementHandler : BaseEventCallback
{
    protected override void OnDoPieceAbility(Piece piece, Hex hex, AbilityType abilType, Hex hex2)
    {
        if (piece == pieceScript)
        {
            switch (abilType)
            {               
                case AbilityType.Movement:
                    StartCoroutine(DoMovementAbility(hex));
                    break;
                case AbilityType.ScoutMove:
                    StartCoroutine(DoScoutAbility(hex, hex2));
                    break;
                case AbilityType.CavalryMove:
                    StartCoroutine(DoCavaltyAbility(hex, hex2));
                    break;
                default:
                    throw new System.Exception();
            }
        }
    }

    private void AttackPieceOnHex(Hex hex)
    {
        AttackHandler.instance.DoAttack(attacker: pieceScript, defender: hex.HexCoordinates.GetPiece());
    }
}