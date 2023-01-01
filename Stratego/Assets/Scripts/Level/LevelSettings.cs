using System.Collections.Generic;

public static class LevelSettings
{
    public static LevelSetting LevelSetting_1_20pcs = new LevelSetting
    {
        UnitsSettings = new List<UnitsSetting>
        {
            new UnitsSetting(numberOfPieces: 1, PieceType.Castle),
            new UnitsSetting(numberOfPieces: 2, PieceType.Trap),
            new UnitsSetting(numberOfPieces: 1, PieceType.Unit, unitBaseValue: 1),
            new UnitsSetting(numberOfPieces: 2, PieceType.Unit, unitBaseValue: 2),
            new UnitsSetting(numberOfPieces: 2, PieceType.Unit, unitBaseValue: 3),
            new UnitsSetting(numberOfPieces: 2, PieceType.Unit, unitBaseValue: 4),
            new UnitsSetting(numberOfPieces: 2, PieceType.Unit, unitBaseValue: 5),
            new UnitsSetting(numberOfPieces: 2, PieceType.Unit, unitBaseValue: 6),
            new UnitsSetting(numberOfPieces: 2, PieceType.Unit, unitBaseValue: 7),
            new UnitsSetting(numberOfPieces: 2, PieceType.Unit, unitBaseValue: 8),
            new UnitsSetting(numberOfPieces: 1, PieceType.Unit, unitBaseValue: 9),
            new UnitsSetting(numberOfPieces: 1, PieceType.Unit, unitBaseValue: 10),
        }
    };

    public static LevelSetting Deepcopy(this LevelSetting levelSetting)
    {
        var result = new LevelSetting { UnitsSettings = new List<UnitsSetting> { } };
        foreach(var unitsSetting in levelSetting.UnitsSettings)
        {
            var copyUnitSetting = new UnitsSetting(
                unitsSetting.NumberOfPieces,
                unitsSetting.UnitSetting.PieceType,
                unitsSetting.UnitSetting.UnitBaseValue
            );

            result.UnitsSettings.Add(copyUnitSetting);
        }

        return result;
    }
}

public class LevelSetting
{
    public List<UnitsSetting> UnitsSettings;
}

public class UnitsSetting
{
    public int NumberOfPieces;
    public UnitSetting UnitSetting;

    public UnitsSetting(int numberOfPieces, PieceType pieceType, int unitBaseValue = 0)
    {
        NumberOfPieces = numberOfPieces;
        UnitSetting = new UnitSetting
        {
            PieceType = pieceType,
            UnitBaseValue = unitBaseValue
        };
    }
}

public class UnitSetting
{
    public PieceType PieceType;
    public int UnitBaseValue;
}
