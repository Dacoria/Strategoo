using Photon.Pun;
using UnityEngine;

public partial class NetworkAE : MonoBehaviour
{
    public void EndRound(PlayerScript playerScript)
    {
        photonView.RPC("RPC_AE_EndRound", RpcTarget.All, playerScript.Id);
    }

    [PunRPC]
    public void RPC_AE_EndRound(int playerId)
    {
        NAE.EndRound?.Invoke(playerId.GetPlayerById());
    }
}