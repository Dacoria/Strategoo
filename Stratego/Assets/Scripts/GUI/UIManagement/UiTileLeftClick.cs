using UnityEngine;
using UnityEngine.EventSystems;

public class UiTileLeftClick : MonoBehaviour
{
    void Update()
    {  
        if(Settings.UserInterfaceIsLocked)
        {
            return;
        }        

        if (Input.GetMouseButtonDown(0))
        {
            if(UiHoverOverHex.Instance.HexHoveredOver != null)
            {
                UiActionSelection.instance.ClickOnTile(UiHoverOverHex.Instance.HexHoveredOver);
            }
            else
            {
                UiActionSelection.instance.ClickOnNothing();
            }
        }
    }
}