using System;
using System.Collections.Generic;
using System.Linq;

public static partial class Utils
{
    public static double MsBetweenNow(this DateTime dateTime) => (DateTime.Now - dateTime).TotalMilliseconds;
    public static bool EnoughTimeForNewEvent(this DateTime dateTime, int ms = 100) => dateTime.MsBetweenNow() > ms;
    public static bool EnoughTimeForNewEvent(this DateTime? dateTime)
    {
        if(!dateTime.HasValue)
        {
            return true;
        }

        return dateTime.Value.EnoughTimeForNewEvent();
    }
}