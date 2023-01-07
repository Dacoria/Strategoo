using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static partial class Utils
{
    public static Hex GetHex(this Vector3Int coordinates) => HexGrid.instance.GetHexAt(coordinates);
    public static Hex GetHex(this Vector3 coordinates) => HexGrid.instance.GetHexAt(new Vector3Int((int)coordinates.x, (int)coordinates.y, (int)coordinates.z));

    public static GameObject GetGoStructure(this Hex hex) => GetChildGoByName(hex.gameObject, "Props");

    public static GameObject GetUnderlyingStructureGoFromHex(this Hex hex)
    {
        var props = hex.GetGoStructure();
        return props.transform.childCount > 0 ? props.transform.GetChild(0).gameObject : null;
    }

    public static List<Vector3Int> GetHexOptions(this Vector3Int hexId, HexAbilityOptionType hexOptionsType)
    {
        List<Vector3Int> hexesToSelect;

        if (hexOptionsType == HexAbilityOptionType.DirectNeighbours)
        {
            hexesToSelect = HexGrid.instance.GetNeighboursFor(hexId);
        }
        else if (hexOptionsType == HexAbilityOptionType.NeighboursInLine)
        {
            hexesToSelect = HexGrid.instance.GetNeighboursFor(hexId, range: 100, onlyMoveInOneDirection: true, stopBeforePieceOnTile: true);
        }
        else
        {
            throw new System.Exception("");
        }

        return hexesToSelect;
    }
}