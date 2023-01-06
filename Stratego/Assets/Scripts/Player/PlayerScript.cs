using Photon.Pun;
using System.Linq;
using UnityEngine;

public class PlayerScript : BaseEventCallback, IPunInstantiateMagicCallback
{
    public bool IsAi;
    public string PlayerName;

    public int Id { get; private set; }  

    [ComponentInject] private PhotonView photonView;
    [ComponentInject] private PlayerIndex playerIndex;
        
    public int Index => playerIndex.Index;
    public bool IsPunOwner => photonView.Owner.IsLocal;

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

        if (IsPunOwner)
        {
            if (PhotonNetwork.IsMasterClient && !IsAi)
            {
                playerIndex.Index = 1;
            }
            else
            {
                playerIndex.Index = 2;
            }
        }
        else
        {
            if (PhotonNetwork.IsMasterClient)
            {
                playerIndex.Index = 2;
            }
            else
            {
                playerIndex.Index = 1;
            }
        }

        NetworkHelper.instance.RefreshPlayerGos();
        PlayerName = name;

        if(GameHandler.instance.GameStatus != GameStatus.NotStarted)
        {
            if (IsPunOwner)
            {
                PieceManager.instance.CreateNewLevelSetup(Index, true);
            }
        }
    }

    public bool ReadyToStartGame;
    protected override void OnPlayerReadyForGame(PlayerScript playerThatIsReady, HexPieceSetup hexPieceSetup)
    {
        if(playerThatIsReady == this)
        {
            ReadyToStartGame = true;
        }
    }
}