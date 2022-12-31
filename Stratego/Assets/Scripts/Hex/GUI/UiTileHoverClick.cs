using UnityEngine;
using UnityEngine.EventSystems;

public class UiTileHoverClick : BaseEventCallback, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [ComponentInject] private Hex hex;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnPointerClick -> " + hex.HexCoordinates + " " + hex.gameObject.name);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter -> " + hex.HexCoordinates + " " + hex.gameObject.name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnPointerExit -> " + hex.HexCoordinates + " " + hex.gameObject.name);
    }
}