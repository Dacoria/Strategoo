using Photon.Pun;
using System;
using UnityEngine;

public partial class NetworkAE : MonoBehaviour
{
    public void PlayerReadyForGame(PlayerScript playerScript)
    {
        photonView.RPC("RPC_AE_PlayerReadyForGame", RpcTarget.All, playerScript.Id);
    }

    [PunRPC]
    public void RPC_AE_PlayerReadyForGame(int playerId)
    {
        NAE.PlayerReadyForGame?.Invoke(playerId.GetPlayerById());
    }
}