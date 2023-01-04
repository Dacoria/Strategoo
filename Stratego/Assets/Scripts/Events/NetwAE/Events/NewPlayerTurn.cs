using Photon.Pun;
using System;
using UnityEngine;

public partial class NetworkAE : MonoBehaviour
{
    public void NewPlayerTurn(PlayerScript playerScript)
    {
        photonView.RPC("RPC_AE_NewPlayerTurn", RpcTarget.All, playerScript.Id);
    }

    [PunRPC]
    public void RPC_AE_NewPlayerTurn(int playerId)
    {
        NAE.NewPlayerTurn?.Invoke(playerId.GetPlayerById());
    }
}