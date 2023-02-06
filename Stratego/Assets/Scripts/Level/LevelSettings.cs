using System;
using System.Collections.Generic;

public static class LevelSettings
{
    public static LevelSetting LevelSetting_1_10pcs = new LevelSetting
    {
        PiecesSettings = new List<PiecesSetting>
        {
            new PiecesSetting(numberOfPieces: 1, PieceType.Castle),
            new PiecesSetting(numberOfPieces: 2, PieceType.Trap),
            new PiecesSetting(numberOfPieces: 1, PieceType.Unit, unitBaseValue: 1),
            new PiecesSetting(numberOfPieces: 2, PieceType.Unit, unitBaseValue: 2),
            new PiecesSetting(numberOfPieces: 2, PieceType.Unit, unitBaseValue: 3),
            new PiecesSetting(numberOfPieces: 1, PieceType.Unit, unitBaseValue: 9),
            new PiecesSetting(numberOfPieces: 1, PieceType.Unit, unitBaseValue: 10),
        }
    };

    public static LevelSetting LevelSetting_2_20pcs = new LevelSetting
    {
        PiecesSettings = new List<PiecesSetting>
        {
            new PiecesSetting(numberOfPieces: 1, PieceType.Castle),
            new PiecesSetting(numberOfPieces: 2, PieceType.Trap),
            new PiecesSetting(numberOfPieces: 1, PieceType.Unit, unitBaseValue: 1),
            new PiecesSetting(numberOfPieces: 2, PieceType.Unit, unitBaseValue: 2),
            new PiecesSetting(numberOfPieces: 2, PieceType.Unit, unitBaseValue: 3),
            new PiecesSetting(numberOfPieces: 2, PieceType.Unit, unitBaseValue: 4),
            new PiecesSetting(numberOfPieces: 2, PieceType.Unit, unitBaseValue: 5),
            new PiecesSetting(numberOfPieces: 2, PieceType.Unit, unitBaseValue: 6),
            new PiecesSetting(numberOfPieces: 2, PieceType.Unit, unitBaseValue: 7),
            new PiecesSetting(numberOfPieces: 2, PieceType.Unit, unitBaseValue: 8),
            new PiecesSetting(numberOfPieces: 1, PieceType.Unit, unitBaseValue: 9),
            new PiecesSetting(numberOfPieces: 1, PieceType.Unit, unitBaseValue: 10),
        }
    };

    public static LevelSetting Deepcopy(this LevelSetting levelSetting)
    {
        var result = new LevelSetting { PiecesSettings = new List<PiecesSetting> { } };
        foreach(var unitsSetting in levelSetting.PiecesSettings)
        {
            var copyUnitSetting = new PiecesSetting(
                unitsSetting.NumberOfPieces,
                unitsSetting.PieceSetting.PieceType,
                unitsSetting.PieceSetting.UnitBaseValue
            );

            result.PiecesSettings.Add(copyUnitSetting);
        }

        return result;
    }
}

public class LevelSetting
{
    public List<PiecesSetting> PiecesSettings;
}

public class PiecesSetting
{
    public int NumberOfPieces;
    public PieceSetting PieceSetting;

    public PiecesSetting(int numberOfPieces, PieceType pieceType, int unitBaseValue = 0)
    {
        PieceSetting = new PieceSetting
        {
            PieceType = pieceType,
            UnitBaseValue = unitBaseValue
        };
        NumberOfPieces = numberOfPieces;
    }
}

[Serializable]
public class PieceSetting
{
    public PieceType PieceType;
    public int UnitBaseValue;
}
