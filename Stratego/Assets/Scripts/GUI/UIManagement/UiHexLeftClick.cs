using UnityEngine;
using UnityEngine.EventSystems;

public class UiHexLeftClick : MonoBehaviour
{
    void Update()
    {  
        if(Settings.UserInterfaceIsLocked)
        {
            return;
        }
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (UiHoverOverHex.Instance.HexHoveredOver != null)
            {
                UiActionSelection.instance.ClickOnHex(UiHoverOverHex.Instance.HexHoveredOver);
            }
            else
            {
                UiActionSelection.instance.ClickOnNothing();
            }
        }
    }
}