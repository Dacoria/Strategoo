using UnityEngine;
using UnityEngine.EventSystems;

public class UiHexLeftClick : MonoBehaviour
{
    private UiHexPieceSelection uiHexPieceSelection;

    private void Awake()
    {
        uiHexPieceSelection = FindObjectOfType<UiHexPieceSelection>();
    }

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
                uiHexPieceSelection.ClickOnHex(UiHoverOverHex.Instance.HexHoveredOver);
            }
            else
            {
                uiHexPieceSelection.ClickOnNothing();
            }
        }
    }
}