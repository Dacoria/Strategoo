using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static partial class Utils
{
    public static bool IsEmpty(this Vector2 vector) => vector.x == 0 && vector.y == 0;

    public static Vector3 DefaultEmptyV3 = new Vector3(-1, -1, -1);
    public static Vector3Int DefaultEmptyV3Int = new Vector3Int(-1, -1, -1);
    public static bool IsDefaultEmptyVector(this Vector3 v3) => v3.x == -1 && v3.y == -1 && v3.z == -1;

    public static float xOffset = 2f, yOffset = 1f, zOffset = 1.73f;
    public static Vector3Int ConvertPositionToCoordinates(this Vector3 position)
    {
        var x = Mathf.CeilToInt(position.x / xOffset);
        var y = Mathf.RoundToInt(position.y / yOffset);
        var z = Mathf.RoundToInt(position.z / zOffset);

        return new Vector3Int(x, y, z);
    }

    public static Vector3Int ToV3Int(this Vector3 position)
    {
        var x = Mathf.RoundToInt(position.x);
        var y = Mathf.RoundToInt(position.y);
        var z = Mathf.RoundToInt(position.z);

        return new Vector3Int(x, y, z);
    }

    public static Vector3 ToV3(this Vector3Int position)
    {
        return new Vector3Int(position.x, position.y, position.z);
    }

    public static Vector3 ConvertCoordinatesToPosition(this Vector3Int position)
    {
        if (position.z % 2 == 0)
        {
            var x = position.x * xOffset;
            var y = position.y * yOffset;
            var z = position.z * zOffset;
            return new Vector3(x, y, z);
        }
        else
        {
            var x = position.x * xOffset - 1;
            var y = position.y * yOffset;
            var z = position.z * zOffset;
            return new Vector3(x, y, z);
        }
    }

    public static Vector3 ToVector3(this string sVector)
    {
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        // split the items
        string[] sArray = sVector.Split(',');

        // store as a Vector3
        var result = new Vector3(
            float.Parse(sArray[0]),
            float.Parse(sArray[1]),
            float.Parse(sArray[2]));

        return result;
    }
}