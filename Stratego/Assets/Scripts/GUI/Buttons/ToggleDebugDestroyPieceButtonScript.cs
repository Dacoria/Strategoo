using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleDebugDestroyPieceButtonScript : BaseEventCallbackSlowUpdate
{
    [ComponentInject] private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup.alpha = 0;
    }

    protected override void SlowUpdate()
    {
        if (canvasGroup.alpha == 0)
        {
            var hasAI = NetworkHelper.instance.GetAllPlayers(isAi: true).Count > 0;
            if (hasAI)
            {
                canvasGroup.alpha = 1;
            }
        }
    }

    public void OnClick()
    {
        Settings.DebugDestroyPiece = !Settings.DebugDestroyPiece;
        Textt.GameLocal("DebugDestroyPiece is turned " + (Settings.DebugDestroyPiece ? "ON" : "OFF"));
    }
}
