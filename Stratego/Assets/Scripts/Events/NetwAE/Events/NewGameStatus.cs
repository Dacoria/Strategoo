using Photon.Pun;
using UnityEngine;

public partial class NetworkAE : MonoBehaviour
{
    public void NewGameStatus(GameStatus newGameStatus)
    {
        photonView.RPC("RPC_AE_NewGameStatus", RpcTarget.All, (int)newGameStatus);
    }

    [PunRPC]
    public void RPC_AE_NewGameStatus(int newGameStatus)
    {
        NAE.NewGameStatus?.Invoke((GameStatus)newGameStatus);
    }
}