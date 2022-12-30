
public enum HexObjectOnTileType
{
    None,
    Bat,
    Boximon,
    EarthElemental,
    FireElemental,
    IceElemental,
    RockGolem,
    DogKnight,
    Slime,
    Turtle,
    Skeleton,
    Mummy,
    Bombie,
    BearTrap,
}

public static class HexObjectOnTileExt
{
    public static bool IsPickup(this HexObjectOnTileType type) => type.ToString().ToUpper().Contains("PICKUP");
}