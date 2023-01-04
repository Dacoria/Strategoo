using UnityEngine;

public partial class GameHandler : BaseEventCallback
{
    public static GameHandler instance;

    private HexGrid HexGrid;
    public int CurrentTurn;

    private new void Awake()
    {
        base.Awake();
        instance = this;
        GameStatus = GameStatus.NotStarted;
    }

    private void Start()
    {
        HexGrid = GameObject.FindObjectOfType<HexGrid>();
        SetCurrentPlayer(NetworkHelper.instance.GetMyPlayer());
    }
}