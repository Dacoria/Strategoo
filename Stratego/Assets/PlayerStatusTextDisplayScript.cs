using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatusTextDisplayScript : MonoBehaviourSlowUpdate
{
    [ComponentInject] private TMP_Text Text;

    private void Awake()
    {
        this.ComponentInject();
    }

    protected override void SlowUpdate()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        //var allPlayers = NetworkHelper.instance.GetAllPlayers(isAi: false);
        var allPlayers = NetworkHelper.instance.GetAllPlayers();

        Text.text = "";

        if (allPlayers.Count == 1)
        {
            return;
        }
        if (GameHandler.instance.GameStatus != GameStatus.UnitPlacement)
        {
            return;
        }
      
        Text.text += "Players:";
        foreach (var player in allPlayers)
        {
            Text.text += "\n- " + player.PlayerName + ": " + (player.ReadyToStartGame ? "READY" : "Placing units");
        }
    }
}
