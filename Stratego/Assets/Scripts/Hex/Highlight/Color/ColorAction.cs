public static class ColorAction
{
    public static HighlightColorType GetColor(this HighlightActionType actionType) => actionType switch
    {
        HighlightActionType.SelectTile => HighlightColorType.White,
        HighlightActionType.MoveOption => HighlightColorType.Orange,
        HighlightActionType.EnemyOption => HighlightColorType.Red,
        HighlightActionType.HoverOption => HighlightColorType.LightGreen,
        HighlightActionType.SwapOption => HighlightColorType.Yellow,
        _ => throw new System.Exception("HighlightActionType " + actionType + " is not supported")
    };
}

public enum HighlightActionType
{
    SelectTile,
    MoveOption,
    EnemyOption,
    HoverOption,
    SwapOption
}
