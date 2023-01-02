
public enum HexObjectOnTileType
{
    None,
    //Boximon,
    Unit,
    Castle,
    Trap,
    UnknownPiece
    //Bat,
    //EarthElemental,
    //FireElemental,
    //IceElemental,
    //RockGolem,
    //DogKnight,
    //Slime,
    //Turtle,
    //Skeleton,
    //Mummy,
    //Bombie,
    //BearTrap,
}

public static class HexObjectOnTileExt
{
    public static bool IsUnit(this HexObjectOnTileType type)
    {
        switch (type)
        {
            case HexObjectOnTileType.Unit:
                return true;
            default:
                return false;
        }
    }
}