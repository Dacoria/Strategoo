using Photon.Pun;
using System.Linq;

public partial class GameHandler : BaseEventCallback
{
    private PlayerScript _currentPlayer;
    public void SetCurrentPlayer(PlayerScript player) => _currentPlayer = player;
    public PlayerScript GetCurrentPlayer() => _currentPlayer;

    public void EndTurn()
    {
        ActionEvents.EndTurn(_currentPlayer);
    }

    protected override void OnEndTurn(PlayerScript player)
    {
        if(!PhotonNetwork.IsMasterClient) { return; }
        var nextPlayer = GetNextPlayer();
        ActionEvents.NewPlayerTurn(nextPlayer);
    }

    private PlayerScript GetNextPlayer()
    {
        var allPlayers = NetworkHelper.instance.GetAllPlayers();
        var nextPlayer = allPlayers.First(x => x != _currentPlayer);
        return nextPlayer;
    }

    protected override void OnNewPlayerTurn(PlayerScript player)
    {
        SetCurrentPlayer(player);
    }
}
