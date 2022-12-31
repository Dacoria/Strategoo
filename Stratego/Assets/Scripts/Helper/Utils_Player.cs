using System;
using System.Collections.Generic;
using System.Linq;

public static partial class Utils
{
    public static PlayerScript GetPlayer(this int id) => NetworkHelper.instance.GetAllPlayers().FirstOrDefault(p => p.Id == id);
}