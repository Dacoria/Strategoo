public class UnitMovementHandler : BaseEventCallback
{
    [ComponentInject] private Piece pieceScript;

    private UnitMovementAction unitMovement;

    private new void Awake()
    {
        unitMovement = gameObject.AddComponent<UnitMovementAction>();
        base.Awake();   
    }

    //protected override void OnEnemyMove(Piece enemy, Hex tile)
    //{
    //    if (enemy.Id == enemyScript.Id)
    //    {
    //        unitMovement.GoToDestination(tile, 2f);
    //    }
    //}
    //
    //protected override void UnitMovingFinished(Piece enemy, Hex hex)
    //{
    //    if (enemy.Id == enemyScript.Id)
    //    {
    //        enemyScript.ActionFinished();
    //    }
    //}
}
