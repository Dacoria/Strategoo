using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NewSetupPieceButtonScript : MonoBehaviour
{
    public void OnClick()
    {
        CreateNewLevelSetup();
    }

    private void CreateNewLevelSetup()
    {
        var level1Setup = LevelSetupManager.GetBasicSetup(1);

        PieceManager.instance.RemoveAllPieces();

        var playerStartTiles = HexGrid.instance.GetPlayerStartTiles(1);
        if(playerStartTiles.Count != level1Setup.UnitPlacementSetting.Count)
        {
            throw new Exception("Hoort niet voor nu");
        }

        var playerStartTilesOrdered = playerStartTiles
            .OrderByDescending(hex => hex.HexCoordinates.z)
            .ThenBy(hex => hex.HexCoordinates.x)
            .ToList();


        for (int i = 0; i < level1Setup.UnitPlacementSetting.Count; i ++)
        {
            var newUnitSetting = level1Setup.UnitPlacementSetting[i].UnitSetting;
            var relatedHex = playerStartTilesOrdered[i];

            PieceManager.instance.CreatePiece(newUnitSetting.PieceType, newUnitSetting.UnitBaseValue, relatedHex);
        }
    }
}
