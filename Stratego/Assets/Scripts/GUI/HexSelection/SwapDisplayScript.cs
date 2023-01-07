using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SwapDisplayScript : MonoBehaviour
{
    private Vector3Int hexId;

    private void Awake()
    {
        this.ComponentInject();
    }

    public void SetHexForSwap(Vector3Int hexId)
    {
        this.hexId = hexId;
    }

    public void OnClick()
    {
        if (Settings.UserInterfaceIsLocked)
        {
            return;
        }

        var player = hexId.GetPiece().Owner;
        var allPlayerHexes = PieceManager.instance.GetPieces(playerOwner: player).Select(x => x.CurrentHexTile);
        var hexResult = allPlayerHexes.Where(x => x.HexCoordinates != hexId).Select(x => x.HexCoordinates).ToList();
        AE.PieceSwapSelected?.Invoke(hexId, hexResult);
    }
}