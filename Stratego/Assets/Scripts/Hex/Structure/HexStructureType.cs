
public enum HexStructureType
{
    None,
    Forest,
    Hill,
    Castle,
    Mountain,
    Portal,
}

public static class HexStructureExt
{
    public static bool HasStructure(this HexStructureType type) => type switch
    {
        HexStructureType.None => false,
        _ => true
    };

    public static bool IsObstacle(this HexStructureType structureType) => structureType switch
    {
        HexStructureType.Mountain => true,
        _ => false
    };
}