using Photon.Pun;
using UnityEngine;

public partial class NetworkAE : MonoBehaviour
{
    public void RoundEnded(PlayerScript pWinner)
    {
        photonView.RPC("RPC_AE_RoundEnded", RpcTarget.All, pWinner.Id);
    }

    [PunRPC]
    public void RPC_AE_RoundEnded(int pWinnerId)
    {
        ActionEvents.EndRound?.Invoke(pWinnerId.GetPlayerById());
    }
}