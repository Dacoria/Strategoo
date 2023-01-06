using Photon.Pun;
using UnityEngine;

public partial class NetworkAE : MonoBehaviour
{
    public void EndTurn(PlayerScript playerScript)
    {
        photonView.RPC("RPC_AE_EndTurn", RpcTarget.All, playerScript.Id);
    }

    [PunRPC]
    public void RPC_AE_EndTurn(int playerId)
    {
        NAE_NoCalling.EndTurn?.Invoke(playerId.GetPlayerById());
    }
}