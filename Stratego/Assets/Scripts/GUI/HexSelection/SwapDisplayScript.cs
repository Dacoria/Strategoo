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

        var myPlayerIndex = NetworkHelper.instance.GetMyPlayer().Index;
        var startTiles = HexGrid.instance.GetPlayerStartTiles(myPlayerIndex);
        var hexResult = startTiles.Where(x => x.HexCoordinates != hexId).Select(x => x.HexCoordinates).ToList();
        ActionEvents.PieceSwapSelected?.Invoke(hexId, hexResult);
    }
}