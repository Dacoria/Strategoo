using Photon.Pun;
using System.Collections.Generic;
using System.Linq;

public partial class GameHandler : BaseEventCallback
{
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
