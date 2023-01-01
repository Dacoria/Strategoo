
public enum HexObjectOnTileType
{
    None,
    Boximon,
    Castle,
    Trap,
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
            case HexObjectOnTileType.Boximon:
                return true;
            default:
                return false;
        }
    }

    public static bool IsCastle(this HexObjectOnTileType type)
    {
        switch (type)
        {
            case HexObjectOnTileType.Boximon:
                return true;
            default:
                return false;
        }
    }

}