using Photon.Pun;
using UnityEngine;

public partial class NetworkAE : MonoBehaviour
{
    public void DoPieceAbility(Piece piece, Hex hexTarget, AbilityType ability)
    {
        photonView.RPC("RPC_AE_DoPieceAbility", RpcTarget.All, piece.CurrentHexTile.HexCoordinates.ToV3(), hexTarget.HexCoordinates.ToV3(), (int)ability);
    }

    [PunRPC]
    public void RPC_AE_DoPieceAbility(Vector3 hexIdPiece, Vector3 hexIdTarget, int abilityType)
    {
        NAE.DoPieceAbility?.Invoke(hexIdPiece.ToV3Int().GetHex().GetPiece(), hexIdTarget.ToV3Int().GetHex(), (AbilityType)(abilityType));
    }
}