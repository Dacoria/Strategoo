public static class ColorAction
{
    public static HighlightColorType GetColor(this HighlightActionType actionType) => actionType switch
    {
        HighlightActionType.SelectTile => HighlightColorType.White,
        HighlightActionType.MoveOption => HighlightColorType.White,
        HighlightActionType.EnemyOption => HighlightColorType.Orange,
        _ => throw new System.Exception("HighlightActionType " + actionType + " is not supported")
    };
}

public enum HighlightActionType
{
    SelectTile,
    MoveOption,
    EnemyOption
}
