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
            result = result.Where(player => player != null && player.GetComponent<PhotonView>().OwnerActorNr == PhotonNetwork.LocalPlayer?.ActorNumber).ToList();
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
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        var leavingPlayerGo = GetAllPlayers().FirstOrDefault(x => x.Id == otherPlayer.ActorNumber);
        if (leavingPlayerGo != null)
        {
            Textt.GameLocal("An active player has left the game!");
        }
        else
        {
            Textt.GameLocal("A non-active player has left the game");
        }

        AE.PlayerDisconnected?.Invoke(leavingPlayerGo);
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
        PlayerList = PhotonNetwork.PlayerList;
        RefreshPlayerGos();        
    }
    
    public List<PlayerScript> GetMyPlayers(bool? isAi = null) => GetAllPlayers(isAi:isAi, myNetwork: true);

    public PlayerScript GetMyPlayer() => GetMyPlayers(isAi: false).FirstOrDefault();    
}