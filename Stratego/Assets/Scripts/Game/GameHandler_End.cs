public partial class GameHandler : BaseEventCallback
{
    public void EndRound(PlayerScript pWinner)
    {
        if (_currentPlayer.IsOnMyNetwork())
        {
            NetworkAE.instance.EndRound(pWinner);
        }        
    }

    protected override void OnEndRound(PlayerScript pWinner)
    {
        GameStatus = GameStatus.RoundEnded;
    }
}