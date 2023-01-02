using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static partial class Utils
{
    public static PlayerScript GetPlayerById(this int id) => NetworkHelper.instance.GetAllPlayers().FirstOrDefault(p => p.Id == id);
    public static PlayerScript GetPlayerByIndex(this int index) => NetworkHelper.instance.GetAllPlayers().FirstOrDefault(p => p.Index == index);

    public static Color GetPlayerColorForIndex(int i) => i switch
    {
        1 => Colorr.Orange,
        2 => Colorr.LightBlue,
        3 => Colorr.Purple,
        4 => Colorr.Yellow,
        _ => Colorr.White,
    };
}