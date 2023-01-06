using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Collections;

public partial class GameHandler : BaseEventCallback
{
    protected override void OnPlayerReadyForGame(PlayerScript player, HexPieceSetup hexPieceSetup)
    {        
        StartCoroutine(UpdatePiecesOnHex(player, hexPieceSetup));
    }

    private IEnumerator UpdatePiecesOnHex(PlayerScript player, HexPieceSetup hexPieceSetup)
    {
        yield return Wait4Seconds.Get(0.05f);

        if (!player.HasPiecesOnGrid())
        {
            CreateHexPieceSetupForPlayer(player, hexPieceSetup);
        }

        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(TryToStartGame());
        }        
    }    

    private IEnumerator TryToStartGame()
    {
        yield return Wait4Seconds.Get(0.05f);
        var allPlayers = NetworkHelper.instance.GetAllPlayers(isAi: false);
        if(allPlayers.All(x => x.ReadyToStartGame))
        {
            ResetGame();
        }
    }

    private void CreateHexPieceSetupForPlayer(PlayerScript player, HexPieceSetup hexPieceSetup)
    {
        foreach(var hexPiecePlacement in hexPieceSetup.HexPiecePlacements)
        {
            PieceManager.instance.CreatePiece(hexPiecePlacement.PieceSetting.PieceType, hexPiecePlacement.PieceSetting.UnitBaseValue, hexPiecePlacement.hexId.GetHex(), player.Index);
        }
    }
}