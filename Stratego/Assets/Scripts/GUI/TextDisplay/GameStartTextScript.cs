using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameStartTextScript : BaseEventCallback
{
    [ComponentInject] private TMP_Text text;
    [ComponentInject] private CanvasGroup canvasGroup;

    private new void Awake()
    {
        base.Awake();
        canvasGroup.alpha = 0;
    }

    protected override void OnEndRound(PlayerScript pWinner)
    {
        text.text = "Game Ended!";
        base.OnEndRound(pWinner);
    }

    protected override void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript player)
    {
        text.text = "Game Started!";
        ShowGameText();
    }

    protected override void OnPlayerDisconnected(PlayerScript player)
    {
        text.text = "Player disconnected";
        ShowGameText();
    }

    private void ShowGameText()
    {
        MonoHelper.instance.FadeIn(canvasGroup, 0.5f);
        StartCoroutine(HideGameText(3.5f));
    }

    private IEnumerator HideGameText(float waitInSeconds)
    {
        yield return Wait4Seconds.Get(waitInSeconds);
        MonoHelper.instance.FadeOut(canvasGroup, 0.25f);
    }
}
