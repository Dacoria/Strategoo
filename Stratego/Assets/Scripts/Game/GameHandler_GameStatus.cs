using UnityEngine;

public partial class GameHandler : BaseEventCallback
{    
    public GameStatus GameStatus;
    protected override void OnNewGameStatus(GameStatus newGameStatus) => GameStatus = newGameStatus;

    protected override void OnGridLoaded()
    {
        ActionEvents.NewGameStatus?.Invoke(GameStatus.UnitPlacement);
    }
}