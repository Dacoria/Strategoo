using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;

public class UiHoverOverHex : MonoBehaviourSlowUpdate
{
    private Camera mainCamera;
    public static UiHoverOverHex Instance;

    private void Awake()
    {
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

        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        var hits = Physics.RaycastAll(ray); 

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