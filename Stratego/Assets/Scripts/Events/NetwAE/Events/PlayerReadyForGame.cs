using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class NetworkAE : MonoBehaviour
{
    public void PlayerReadyForGame(PlayerScript playerScript, HexPieceSetup hexPieceSetup)
    {
        var hexPieceSetupJson = JsonUtility.ToJson(hexPieceSetup);
        photonView.RPC("RPC_AE_PlayerReadyForGame", RpcTarget.All, playerScript.Id, hexPieceSetupJson);
    }

    [PunRPC]
    public void RPC_AE_PlayerReadyForGame(int playerId, string hexPieceSetupJson)
    {
        var hexPieceSetup = JsonUtility.FromJson<HexPieceSetup>(hexPieceSetupJson);
        NAE_NoCalling.PlayerReadyForGame?.Invoke(playerId.GetPlayerById(), hexPieceSetup);
    }
}


[Serializable]
public class HexPieceSetup
{
    public List<HexPiecePlacement> HexPiecePlacements;
}

[Serializable]
public class HexPiecePlacement
{
    public Vector3 hexId;
    public PieceSetting PieceSetting;
}