using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class GameHandler : BaseEventCallback
{
    public List<PlayerScript> AllPlayers;
    private int MAX_PLAYERS = 2;

    private void SetupNewGame()
    {
        var players = NetworkHelper.instance.GetAllPlayers().OrderBy(x => x.Id).Take(MAX_PLAYERS).ToList();
        NetworkAE.instance.NewRoundStarted_Simultanious(players);
    }    

    public void ResetGame()
    {
        StartCoroutine(CR_ResetGame());
    }

    public IEnumerator CR_ResetGame()
    {
        yield return Wait4Seconds.Get(0.1f);
        if (HexGrid.IsLoaded() && NetworkHelper.instance.GetAllPlayers().Count > 0)
        {
            SetupNewGame();
        }
        else
        {
            StartCoroutine(CR_ResetGame());
        }
    }

    protected override void OnNewRoundStarted(List<PlayerScript> players, PlayerScript currPlayer)
    {
        GameStatus = GameStatus.PlayerFase;

        // refresh om te checken
        AllPlayers = NetworkHelper.instance.GetAllPlayers().Take(MAX_PLAYERS).ToList();

        // check
        var playersMatch = players.Select(x => x.Id).All(AllPlayers.Select(x => x.Id).Contains);
        var sameSize = players.Count == AllPlayers.Count;
        if (!playersMatch || !sameSize) { throw new Exception(); }

        // fix order....
        var playersRes = new List<PlayerScript>();
        for (int i = 0; i < players.Count; i++)
        {
            playersRes.Add(AllPlayers.Single(x => x.Id == players[i].Id));
        }
        AllPlayers = playersRes;

        // reset local
        ResetGameLocal();

        SetCurrentPlayer(currPlayer);
        CurrentTurn = 1;
    }

    private void ResetGameLocal()
    {
        for (int i = 0; i < AllPlayers.Count; i++)
        {
            AllPlayers[i].Index = i;
        }
    }
}