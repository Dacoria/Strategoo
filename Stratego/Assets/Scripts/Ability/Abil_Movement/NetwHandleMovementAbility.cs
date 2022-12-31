
public class NetwHandleMovementAbility : BaseEventCallback, IAbilityNetworkHandler
{
    public bool CanDoAbility(PlayerScript playerDoingAbility, Hex target, Hex target2)
    {
        return !target.IsObstacle();
    }

    public void NetworkHandle(PlayerScript playerDoingAbility, Hex target, Hex target2)
    {
        if (target.HasPiece(isAlive: true))
        {
            gameObject.GetAdd<UnitAttackAction>().AttackUnitOnHex(target);
        }
        else
        {
            gameObject.GetAdd<UnitMovementAction>().GoToDestination(target, 1.3f);
        }
    }   
}