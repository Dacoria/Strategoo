using Photon.Pun;
using UnityEngine;

public partial class NetworkAE : MonoBehaviour
{
    public void DoPieceAbility(Piece piece, Hex hexTarget, AbilityType ability, Hex hexTarget2)
    {
        var pieceV3 = piece.CurrentHexTile.HexCoordinates.ToV3();
        var hexIdTarget2V3 = hexTarget2 == null ? new Vector3(-1, -1, -1) : hexTarget2.HexCoordinates.ToV3();
        photonView.RPC("RPC_AE_DoPieceAbility", RpcTarget.All, pieceV3, hexTarget.HexCoordinates.ToV3(), (int)ability, hexIdTarget2V3);
    }

    [PunRPC]
    public void RPC_AE_DoPieceAbility(Vector3 hexIdPiece, Vector3 hexIdTarget, int abilityType, Vector3 hexIdTarget2)
    {
        var hexTarget2 = (hexIdTarget2.x == -1 && hexIdTarget2.y == -1 && hexIdTarget2.z == -1) ? null : hexIdTarget2.ToV3Int().GetHex();
        NAE_NoCalling.DoPieceAbility?.Invoke(hexIdPiece.ToV3Int().GetHex().GetPiece(), hexIdTarget.ToV3Int().GetHex(), (AbilityType)(abilityType), hexTarget2);
    }
}