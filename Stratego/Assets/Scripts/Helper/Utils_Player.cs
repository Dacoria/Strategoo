using System;
using System.Collections.Generic;
using System.Linq;

public static partial class Utils
{
    public static PlayerScript GetPlayerById(this int id) => NetworkHelper.instance.GetAllPlayers().FirstOrDefault(p => p.Id == id);
    public static PlayerScript GetPlayerByIndex(this int index) => NetworkHelper.instance.GetAllPlayers().FirstOrDefault(p => p.Index == index);
}