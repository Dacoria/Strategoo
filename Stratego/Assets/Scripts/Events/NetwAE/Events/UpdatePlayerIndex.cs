using Photon.Pun;
using System;
using UnityEngine;

public partial class NetworkAE : MonoBehaviour
{
    public void UpdatePlayerIndex(PlayerScript playerScript, int index)
    {
        photonView.RPC("RPC_AE_UpdatePlayerIndex", RpcTarget.All, playerScript.Id, index);
    }

    [PunRPC]
    public void RPC_AE_UpdatePlayerIndex(int playerId, int index)
    {
        NAE_NoCalling.UpdatePlayerIndex?.Invoke(playerId.GetPlayerById(), index);
    }
}