using Photon.Pun;
using System.Collections;

public partial class GameHandler : BaseEventCallback
{
    public void EndRound(PlayerScript pWinner)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(CR_EndRound(0.5f, pWinner));
        }
    }

    private IEnumerator CR_EndRound(float waitForSeconds, PlayerScript pWinner)
    {
        yield return Wait4Seconds.Get(waitForSeconds);
        NetworkAE.instance.EndRound(pWinner);
    }

    protected override void OnEndRound(PlayerScript pWinner)
    {
        GameStatus = GameStatus.RoundEnded;
    }

    protected override void OnPlayerDisconnected(PlayerScript player)
    {
        GameStatus = GameStatus.RoundEnded;
    }
}