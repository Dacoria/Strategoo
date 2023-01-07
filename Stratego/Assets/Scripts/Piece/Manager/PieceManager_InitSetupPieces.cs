using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;

public partial class PieceManager : BaseEventCallback
{
    public void CreateNewLevelSetup(bool randomizePieces)
    {
        RemoveAllPieces();

        var allPlayers = NetworkHelper.instance.GetAllPlayers();
        foreach (var player in allPlayers)
        {
            if(player.IsPunOwner)
            {
                CreateNewLevelSetup(player.Index, randomizePieces);
            }            
        }
    }

    public void CreateNewLevelSetup(int playerIndex, bool randomizePieces)
    {
        var level1Setup = LevelSetupManager.GetBasicSetup(1);

        var playerStartTiles = HexGrid.instance.GetPlayerStartTiles(playerIndex, level1Setup.UnitPlacementSetting.Count);
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

        NewPieceTileSetupForPlayer(playerIndex.GetPlayerByIndex(), level1Setup, playerStartTilesOrdered);
    }

    private void NewPieceTileSetupForPlayer(PlayerScript player, LevelSetup levelSetup, List<Hex> hexes)
    {
        for (int i = 0; i < levelSetup.UnitPlacementSetting.Count; i++)
        {
            var newUnitSetting = levelSetup.UnitPlacementSetting[i].UnitSetting;
            var relatedHex = hexes[i];

            CreatePiece(newUnitSetting.PieceType, newUnitSetting.UnitBaseValue, relatedHex, player.Id);
        }
    }
}