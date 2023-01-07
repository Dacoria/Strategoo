public partial class PieceMovementHandler : BaseEventCallback
{
    [ComponentInject] private Piece pieceScript;

    private PieceMovementAction pieceMovement;

    private new void Awake()
    {
        base.Awake();
        pieceMovement = gameObject.AddComponent<PieceMovementAction>();
    }    
}