using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class NewSetupPieceButtonScript : BaseEventCallback
{
    [ComponentInject] private Button button;

    private void Start()
    {
        button.interactable = false;
    }

    protected override void OnGridLoaded() => button.interactable = true ;

    protected override void OnPlayerReadyForGame(PlayerScript player, HexPieceSetup hexPieceSetup)
    {
        if(player.IsOnMyNetwork())
        {
            Destroy(gameObject);
        }
    }

    public void OnClick(bool randomizePieces)
    {
        PieceManager.instance.CreateNewLevelSetup(randomizePieces);
    }
}
