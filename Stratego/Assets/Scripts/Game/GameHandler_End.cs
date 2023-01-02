public partial class GameHandler : BaseEventCallback
{
    protected override void OnEndRound(PlayerScript pWinner)
    {
        GameStatus = GameStatus.RoundEnded;
        Textt.GameLocal("Game has ended! " + pWinner.PlayerName + " wins!!!");
    }
}