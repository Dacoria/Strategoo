using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;

public class UiHoverOverHex : MonoBehaviour
{
    private Camera mainCamera;
    public static UiHoverOverHex Instance;

    protected void Awake()
    {
        mainCamera = Camera.main;
        Instance = this;
    }

    public Hex HexHoveredOver;


    private void Update()
    {
        if (Settings.UserInterfaceIsLocked)
        {
            return;
        }
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (!Netw.CurrPlayer().IsOnMyNetwork())
        {
            return;
        }

        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        var hexLayermask = 1 << LayerMask.NameToLayer(Statics.LAYER_MASK_HEXTILE);
        var hits = Physics.RaycastAll(ray, 99999999999999, hexLayermask);
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