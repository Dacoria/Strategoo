using Photon.Pun;
using System.Linq;
using UnityEngine;

public class PlayerScript : BaseEventCallback, IPunInstantiateMagicCallback
{
    public bool IsAi;
    public string PlayerName;

    public GameObject GameObject => gameObject;
    public Color Color => playerColor.Color;
    public int Id { get; private set; }

    public int Index;

    [ComponentInject] private PlayerColor playerColor;

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        object[] instantiationData = info.photonView.InstantiationData;
        var name = instantiationData[0].ToString();
        IsAi = bool.Parse(instantiationData[1].ToString());

        var hosterCounterId = int.Parse(instantiationData[2].ToString());

        if (PhotonNetwork.OfflineMode || IsAi)
        {
            Id = hosterCounterId + 1000; // forceert dat het anders is dat het photonId
        }
        else
        {
            Id = info.photonView.OwnerActorNr;
        }
        if(IsAi && PhotonNetwork.IsMasterClient)
        {
            // TODO AI TOEVOEGEN
        }

        NetworkHelper.instance.RefreshPlayerGos();
        PlayerName = name;
    }    
}