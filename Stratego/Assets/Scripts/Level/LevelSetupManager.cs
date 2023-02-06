using System;
using System.Linq;
using System.Collections.Generic;

public static class LevelSetupManager
{
    public static LevelSetup GetBasicSetup()
    {
        var level_nr = SceneHandler.instance.GetCurrentSceneNr();
        if (level_nr == 1) { return CreateBasicSetup(LevelSettings.LevelSetting_1_10pcs); }
        if (level_nr == 2) { return CreateBasicSetup(LevelSettings.LevelSetting_2_20pcs); }

        throw new Exception("");
    }

    private static LevelSetup CreateBasicSetup(LevelSetting levelSetting)
    {
        var levelSettingCopy = levelSetting.Deepcopy();
        var levelSetup = ConvertToLevelSetup(levelSettingCopy);
        return levelSetup;
    }

    private static LevelSetup ConvertToLevelSetup(LevelSetting levelSetting)
    {
        var levelSetup = new LevelSetup();

        foreach (var piecesSetting in levelSetting.PiecesSettings)
        {
            for (int i = 0; i < piecesSetting.NumberOfPieces; i++)
            {
                levelSetup.Add(piecesSetting.PieceSetting);
            }
        }

        return levelSetup;
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