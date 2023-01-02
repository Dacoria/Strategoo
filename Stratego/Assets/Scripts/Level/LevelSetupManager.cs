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
        //levelSetup.Add(levelSetting.GetUnit(4));
        levelSetup.Add(levelSetting.GetCastle());
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

        if(levelSetting.UnitsSettings.Any(x => x.NumberOfPieces != 0))
        {
            //throw new Exception();
        }

        return levelSetup;
    }

    private static UnitSetting GetUnit(this LevelSetting levelSettings, int value)
    {
        var unitSetting = levelSettings.UnitsSettings.First(x => x.UnitSetting.PieceType == PieceType.Unit && x.UnitSetting.UnitBaseValue == value);
        unitSetting.NumberOfPieces--;
        if(unitSetting.NumberOfPieces < 0)
        {
            //throw new Exception();
        }

        return unitSetting.UnitSetting;
    }

    private static UnitSetting GetCastle(this LevelSetting levelSettings)
    {
        var unitSetting = levelSettings.UnitsSettings.First(x => x.UnitSetting.PieceType == PieceType.Castle);
        unitSetting.NumberOfPieces--;
        if (unitSetting.NumberOfPieces < 0)
        {
            //throw new Exception();
        }

        return unitSetting.UnitSetting;
    }

    private static UnitSetting GetTrap(this LevelSetting levelSettings)
    {
        var unitSetting = levelSettings.UnitsSettings.First(x => x.UnitSetting.PieceType == PieceType.Trap);
        unitSetting.NumberOfPieces--;
        if (unitSetting.NumberOfPieces < 0)
        {
            //throw new Exception();
        }

        return unitSetting.UnitSetting;
    }
}

public class LevelSetup
{
    public List<UnitPlacementSetting> UnitPlacementSetting = new List<UnitPlacementSetting>();
    private int indexCounter;

    public void Add(UnitSetting unitSetting)
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
    public UnitSetting UnitSetting;
}