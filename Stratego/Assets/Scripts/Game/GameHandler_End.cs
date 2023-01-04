public partial class GameHandler : BaseEventCallback
{
    public void EndRound(PlayerScript pWinner)
    {
        ActionEvents.EndRound?.Invoke(pWinner);
    }

    protected override void OnEndRound(PlayerScript pWinner)
    {
        GameStatus = GameStatus.RoundEnded;
    }
}