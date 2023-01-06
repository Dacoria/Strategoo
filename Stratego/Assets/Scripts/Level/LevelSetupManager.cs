using System;
using System.Linq;
using System.Collections.Generic;

public static class LevelSetupManager
{
    public static LevelSetup GetBasicSetup(int levelSetting)
    {
        if(levelSetting == 1)
        {
            return CreateBasicSetupLevel1();
        }

        return null;
    }

    private static LevelSetup CreateBasicSetupLevel1()
    {
        var levelSetting = LevelSettings.LevelSetting_1_20pcs.Deepcopy();
        var levelSetup = new LevelSetup();

        // van links boven, naar rechts, dan volgende rij etc. tm rechtsonder
        levelSetup.Add(levelSetting.GetUnit(4));
        levelSetup.Add(levelSetting.GetTrap());
        levelSetup.Add(levelSetting.GetUnit(3));
        levelSetup.Add(levelSetting.GetUnit(8));
        levelSetup.Add(levelSetting.GetUnit(2));
        levelSetup.Add(levelSetting.GetUnit(7));
        levelSetup.Add(levelSetting.GetUnit(4));
        levelSetup.Add(levelSetting.GetUnit(5));
        levelSetup.Add(levelSetting.GetUnit(9));
        levelSetup.Add(levelSetting.GetUnit(6));
        levelSetup.Add(levelSetting.GetUnit(6));
        levelSetup.Add(levelSetting.GetUnit(7));
        levelSetup.Add(levelSetting.GetUnit(10));
        levelSetup.Add(levelSetting.GetUnit(3));
        levelSetup.Add(levelSetting.GetUnit(5));
        levelSetup.Add(levelSetting.GetUnit(1));
        levelSetup.Add(levelSetting.GetUnit(8));
        levelSetup.Add(levelSetting.GetUnit(2));
        levelSetup.Add(levelSetting.GetTrap());
        levelSetup.Add(levelSetting.GetCastle());

        if(levelSetting.PiecesSettings.Any(x => x.NumberOfPieces != 0))
        {
            throw new Exception();
        }

        return levelSetup;
    }

    private static PieceSetting GetUnit(this LevelSetting levelSettings, int value)
    {
        var unitSetting = levelSettings.PiecesSettings.First(x => x.PieceSetting.PieceType == PieceType.Unit && x.PieceSetting.UnitBaseValue == value);
        unitSetting.NumberOfPieces--;
        if(unitSetting.NumberOfPieces < 0)
        {
            throw new Exception();
        }

        return unitSetting.PieceSetting;
    }

    private static PieceSetting GetCastle(this LevelSetting levelSettings)
    {
        var unitSetting = levelSettings.PiecesSettings.First(x => x.PieceSetting.PieceType == PieceType.Castle);
        unitSetting.NumberOfPieces--;
        if (unitSetting.NumberOfPieces < 0)
        {
            throw new Exception();
        }

        return unitSetting.PieceSetting;
    }

    private static PieceSetting GetTrap(this LevelSetting levelSettings)
    {
        var unitSetting = levelSettings.PiecesSettings.First(x => x.PieceSetting.PieceType == PieceType.Trap);
        unitSetting.NumberOfPieces--;
        if (unitSetting.NumberOfPieces < 0)
        {
            throw new Exception();
        }

        return unitSetting.PieceSetting;
    }
}

public class LevelSetup
{
    public List<UnitPlacementSetting> UnitPlacementSetting = new List<UnitPlacementSetting>();
    private int indexCounter;

    public void Add(PieceSetting unitSetting)
    {
        var unitPlacement = new UnitPlacementSetting
        {
            HexCounter = indexCounter,
            UnitSetting = unitSetting
        };
        UnitPlacementSetting.Add(unitPlacement);
        indexCounter++;
    }
}

public class UnitPlacementSetting
{
    public int HexCounter;
    public PieceSetting UnitSetting;
}