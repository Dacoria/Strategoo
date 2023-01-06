using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatusTextDisplayScript : BaseEventCallbackSlowUpdate
{
    [ComponentInject] private TMP_Text Text;

    private void Start()
    {
        Text.text = "";
    }

    protected override void SlowUpdate()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        var allPlayers = NetworkHelper.instance.GetAllPlayers().OrderBy(x => x.Index);

        if (GameHandler.instance.GameStatus == GameStatus.NotStarted)
        {
            return;
        }

        Text.text = "Players:";
        foreach (var player in allPlayers)
        {
            Text.text += "\n- " + player.PlayerName + ": " + GetPlayerStatus(player);
        }
    }

    private string GetPlayerStatus(PlayerScript player)
    {
        if (GameHandler.instance.GameStatus.In(GameStatus.NotStarted, GameStatus.UnitPlacement))
        {
            if(player.ReadyToStartGame)
            {
                return "READY";
            }
            else
            {
                return "Placing units";
            }        
        }
        if (GameHandler.instance.GameStatus.In(GameStatus.GameFase))
        {
            var currentPlayer = Netw.CurrPlayer();
            if(currentPlayer == player)
            {
                return "YOUR MOVE";
            }
            else
            {
                return "STANDBY";
            }
        }
        if (GameHandler.instance.GameStatus.In(GameStatus.RoundEnded))
        {
            return "GAME ENDED";
        }

        return "UNKNOWN";
    }
}
