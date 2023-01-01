using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NetworkHelper : MonoBehaviourPunCallbacks
{
    public static NetworkHelper instance;

    private List<PlayerScript> allPlayers;

    public List<PlayerScript> GetAllPlayers(bool? isAi = null, bool? myNetwork = null)
    {
        var result = allPlayers;        
        if (isAi.HasValue)
        {
            result = result.Where(x => x.IsAi == isAi.Value).ToList();
        }
        if (myNetwork.HasValue)
        {
            result = result.Where(player => player?.GetComponent<PhotonView>().OwnerActorNr == PhotonNetwork.LocalPlayer?.ActorNumber).ToList();
        }

        return result;
    }

    public Player[] PlayerList;

    private void Awake()
    {
        instance = this;
        allPlayers = new List<PlayerScript>();
        this.ComponentInject();
    }

    private void Start()
    {
        PlayerList = PhotonNetwork.PlayerList;
        RefreshPlayerGos();
    }

    public void RefreshPlayerGos()
    {
        this.allPlayers = GameObject.FindGameObjectsWithTag(Statics.TAG_PLAYER).Select(x => x.GetComponent<PlayerScript>()).ToList();
        SyncPlayerIndex();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        
        if(GameHandler.instance.GameStatus == GameStatus.PlayerFase)
        {
            if(GetAllPlayers().Any(x => x.Id == otherPlayer.ActorNumber))
            {
                Textt.GameLocal("An active player has left the game! Reset the current game?");
            }
            else
            {
                Textt.GameLocal("A non-active player has left the game");
            }            
        }
        else
        {
            Textt.GameLocal("A player has left the game");
        }        

        PlayerList = PhotonNetwork.PlayerList;
        StartCoroutine(RefreshPlayerGosInXSeconds(0.1f)); // obj is niet direct weg; heel even wachten
    }


    private IEnumerator RefreshPlayerGosInXSeconds(float seconds)
    {
        yield return Wait4Seconds.Get(seconds);
        RefreshPlayerGos();
    }      

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        PlayerList = PhotonNetwork.PlayerList;
        RefreshPlayerGos();        
    }

    private void SyncPlayerIndex()
    {
        if(!PhotonNetwork.IsMasterClient) { return; }
        if(allPlayers.Count == 0) { return; }

        var masterclientPlayer = GetMyPlayer();
        ActionEvents.UpdatePlayerIndex(masterclientPlayer.Id, 1); // MC == 1 --> NIET SWITCHEN!

        var allPlayersWithIndexes = allPlayers.Where(x => x.Index > 1).ToList();
        for (int i = 0; i < allPlayersWithIndexes.Count; i++)
        {
            var playerWithIndex = allPlayersWithIndexes[i];
            ActionEvents.UpdatePlayerIndex(playerWithIndex.Id, i + 2);
        }

        var allPlayersWithoutIndexes = allPlayers.Where(x => x.Index == 0).ToList();
        var highestIndex = allPlayersWithIndexes.Any() ? allPlayersWithIndexes.Max(x => x.Index) : 1;

        for (int i = 0; i < allPlayersWithoutIndexes.Count; i++)
        {
            var playerWithoutIndex = allPlayersWithoutIndexes[i];
            highestIndex = highestIndex + 1;
            ActionEvents.UpdatePlayerIndex(playerWithoutIndex.Id, highestIndex);
        }        
    }

    public List<PlayerScript> GetMyPlayers(bool? isAi = null) => GetAllPlayers(isAi:isAi, myNetwork: true);

    public PlayerScript GetMyPlayer() => GetMyPlayers(isAi: false).FirstOrDefault();    
}