using System.Collections.Generic;

public static class LevelSettings
{
    public static LevelSetting LevelSetting_1 = new LevelSetting
    {
        UnitSettings = new List<UnitsSetting>
        {
            new UnitsSetting{ PieceType = PieceType.Castle, NumberOfPieces = 1},
            new UnitsSetting{ PieceType = PieceType.Unit,   NumberOfPieces = 1, UnitBaseValue = 1},
            new UnitsSetting{ PieceType = PieceType.Unit,   NumberOfPieces = 2, UnitBaseValue = 2},
            new UnitsSetting{ PieceType = PieceType.Unit,   NumberOfPieces = 2, UnitBaseValue = 3},
            new UnitsSetting{ PieceType = PieceType.Unit,   NumberOfPieces = 2, UnitBaseValue = 4},
            new UnitsSetting{ PieceType = PieceType.Unit,   NumberOfPieces = 2, UnitBaseValue = 5},
            new UnitsSetting{ PieceType = PieceType.Unit,   NumberOfPieces = 2, UnitBaseValue = 6},
            new UnitsSetting{ PieceType = PieceType.Unit,   NumberOfPieces = 2, UnitBaseValue = 7},
            new UnitsSetting{ PieceType = PieceType.Unit,   NumberOfPieces = 2, UnitBaseValue = 8},
            new UnitsSetting{ PieceType = PieceType.Unit,   NumberOfPieces = 1, UnitBaseValue = 9},
            new UnitsSetting{ PieceType = PieceType.Unit,   NumberOfPieces = 1, UnitBaseValue = 10},
        }
    };
}

public class LevelSetting
{
    public List<UnitsSetting> UnitSettings;
}

public class UnitsSetting
{
    public PieceType PieceType;
    public int UnitBaseValue;
    public int NumberOfPieces;
}
