using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;

public partial class PieceManager : BaseEventCallback
{
    public void CreateNewLevelSetup(bool randomizePieces)
    {
        PieceManager.instance.RemoveAllPieces();

        var allPlayers = NetworkHelper.instance.GetAllPlayers();
        foreach (var player in allPlayers)
        {
            CreateNewLevelSetup(player.Index, randomizePieces);
        }
    }

    public void CreateNewLevelSetup(int playerIndex, bool randomizePieces)
    {
        var level1Setup = LevelSetupManager.GetBasicSetup(1);

        var playerStartTiles = HexGrid.instance.GetPlayerStartTiles(playerIndex);
        if (playerStartTiles.Count != level1Setup.UnitPlacementSetting.Count)
        {
            throw new Exception("Hoort niet");
        }

        List<Hex> playerStartTilesOrdered;

        if (playerIndex == 1)
        {
            playerStartTilesOrdered = playerStartTiles
            .OrderByDescending(hex => hex.HexCoordinates.z)
            .ThenBy(hex => hex.HexCoordinates.x)
            .ToList();
        }
        else if (playerIndex == 2)
        {
            playerStartTilesOrdered = playerStartTiles
            .OrderBy(hex => hex.HexCoordinates.z)
            .ThenByDescending(hex => hex.HexCoordinates.x)
            .ToList();
        }
        else
        {
            throw new Exception();
        }

        if (randomizePieces)
        {
            level1Setup.UnitPlacementSetting.Shuffle();
        }


        for (int i = 0; i < level1Setup.UnitPlacementSetting.Count; i++)
        {
            var newUnitSetting = level1Setup.UnitPlacementSetting[i].UnitSetting;
            var relatedHex = playerStartTilesOrdered[i];

            var player = playerIndex.GetPlayerByIndex();
            PieceManager.instance.CreatePiece(newUnitSetting.PieceType, newUnitSetting.UnitBaseValue, relatedHex, player.Id);
        }
    }
}