public partial class PieceMovementHandler : BaseEventCallback
{
    protected override void OnDoPieceAbility(Piece piece, Hex hex, AbilityType abilType, Hex hex2)
    {
        if (piece == pieceScript && abilType == AbilityType.Movement)
        {
            StartCoroutine(DoMovementAbility(hex));
        }
        if (piece == pieceScript && abilType == AbilityType.ScoutMove)
        {
            StartCoroutine(DoScoutAbility(hex, hex2));
        }
    }
}