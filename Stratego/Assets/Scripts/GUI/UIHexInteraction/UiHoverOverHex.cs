using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;

public class UiHoverOverHex : BaseEventCallbackSlowUpdate
{
    private Camera mainCamera;
    public static UiHoverOverHex Instance;

    protected new void Awake()
    {
        base.Awake();
        mainCamera = Camera.main;
        Instance = this;
    }

    public Hex HexHoveredOver;

    //protected override int TicksToUpdate => 1000;

    protected override void SlowUpdate()
    {
        if (Settings.UserInterfaceIsLocked)
        {
            return;
        }
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (!GameHandler.instance.GetCurrentPlayer().IsOnMyNetwork())
        {
            return;
        }

        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        var hexLayermask = 1 << LayerMask.NameToLayer(Statics.LAYER_MASK_HEXTILE);
        var hits = Physics.RaycastAll(ray, hexLayermask);

        HexHoveredOver = GetFirstHexHit(hits);
    }

    private Hex GetFirstHexHit(RaycastHit[] hits)
    {
        for (int i = 0; i < hits.Length; i++)
        {
            var tileHit = hits[i].transform.GetComponentInParent<Hex>();
            if (tileHit != null)
            {
                return tileHit;
            }
        }

        return null;
    }
}