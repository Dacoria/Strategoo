public partial class GameHandler : BaseEventCallback
{
    public void EndRound(PlayerScript pWinner)
    {
        NAE.EndRound?.Invoke(pWinner);
    }

    protected override void OnEndRound(PlayerScript pWinner)
    {
        GameStatus = GameStatus.RoundEnded;
    }
}