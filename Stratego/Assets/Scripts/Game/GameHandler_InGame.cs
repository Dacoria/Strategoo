using Photon.Pun;
using System.Collections;
using System.Linq;

public partial class GameHandler : BaseEventCallback
{
    private PlayerScript _currentPlayer;
    public void SetCurrentPlayer(PlayerScript player) => _currentPlayer = player;
    public PlayerScript GetCurrentPlayer() => _currentPlayer;

    public void EndTurn()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(CR_EndTurn(0.5f));
        }
    }

    private IEnumerator CR_EndTurn(float waitInSeconds)
    {
        yield return Wait4Seconds.Get(waitInSeconds);
        NetworkAE.instance.EndTurn(_currentPlayer);
    }

    protected override void OnEndTurn(PlayerScript player)
    {
        if(!PhotonNetwork.IsMasterClient) { return; }
        var nextPlayer = GetNextPlayer();
        NetworkAE.instance.NewPlayerTurn(nextPlayer);
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
