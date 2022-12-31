
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public static partial class Utils
{
    public static Color SetAlpha(this Color color, float newAlpha) => new Color(color.r, color.g, color.b, newAlpha);
}