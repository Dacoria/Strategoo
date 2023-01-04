using Photon.Pun;
using UnityEngine;

public partial class NetworkAE : MonoBehaviour
{
    public void SwapPieces(Piece pieceA, Piece pieceB, AbilityType ability)
    {
        photonView.RPC("RPC_AE_SwapPieces", RpcTarget.All, pieceA.CurrentHexTile.HexCoordinates.ToV3(), pieceB.CurrentHexTile.HexCoordinates.ToV3());
    }

    [PunRPC]
    public void RPC_AE_SwapPieces(Vector3 hexIdpieceA, Vector3 hexIdpieceB)
    {
        NAE.SwapPieces?.Invoke(hexIdpieceA.ToV3Int().GetHex().GetPiece(), hexIdpieceB.ToV3Int().GetHex().GetPiece());
    }
}