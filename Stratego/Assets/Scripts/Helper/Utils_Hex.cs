using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static partial class Utils
{
    public static Hex GetHex(this Vector3Int coordinates) => HexGrid.instance.GetTileAt(coordinates);
    public static Hex GetHex(this Vector3 coordinates) => HexGrid.instance.GetTileAt(new Vector3Int((int)coordinates.x, (int)coordinates.y, (int)coordinates.z));

    public static GameObject GetStructureGoFromHex(this Hex hex)
    {
        var props = GetChildGoByName(hex.gameObject, "Props");
        return props.transform.childCount > 0 ? props.transform.GetChild(0).gameObject : null;
    }

    public static GameObject GetStructurePrefabFromRrc(this HexStructureType hexStructure)
    {
        if (hexStructure.HasStructure())
        {
            return Rsc.GoStructureMap.First(x => x.Key == hexStructure.ToString() + "Structure").Value;
        }

        return null;
    }
}