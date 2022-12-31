using Photon.Pun;
using System.Collections.Generic;
using System.Linq;

public partial class GameHandler : BaseEventCallback
{    
    protected override void OnAllPlayersFinishedTurn()
    {
        CurrentTurn++; // Losse event call van maken? eigenlijk is de turn pas geeindigd na de enemy fase...

        if (!PhotonNetwork.IsMasterClient) { return; }

        // 1 doet de monster movements
        StartNewPlayerTurns();
    }

    private void StartNewPlayerTurns()
    {
        // TODO
        if (!PhotonNetwork.IsMasterClient) { return; }
        //NetworkAE.instance.NewPlayerTurn_Simultanious();
    }

    protected override void OnNewPlayerTurn(PlayerScript player)
    {
        if (player.IsOnMyNetwork())
        {
            // alleen op eigen netwerk setten (om zo ook AI te ondersteunen; vandaar zo)
            SetCurrentPlayer(player);
        }
    }
}
