
using System;
using System.Linq;
using System.Collections;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Hex)), CanEditMultipleObjects]
public class HexEditorChangeType : Editor
{
    private Hex previousSelectedHex;
    private HexSurfaceType previousHexSurfaceType;
    private HexObjectOnTileType previousHexObjectOnTileType;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var hexes = targets.Select(x => (Hex)x).ToList();

        //return;
        if (!EditorGUILayoutToggle.UseEditorGuiScripts)
        {
            return;
        }
        if (Application.isPlaying)
        {
            return;
        }

        var firstHex = (Hex)target;        
        if (previousSelectedHex == firstHex && previousHexSurfaceType != firstHex.HexSurfaceType)
        {
            hexes.ForEach(hex => HexEditorUtil.HexSurfaceTypeChanged(hex, firstHex.HexSurfaceType));
        }
        if (previousSelectedHex == firstHex && previousHexObjectOnTileType != firstHex.HexObjectOnTileType)
        {
            // voor nu: alleen enemies!
            hexes.ForEach(hex => HexEditorUtil.HexObjectOnTileTypeChanged(hex, firstHex.HexObjectOnTileType));
        }

        previousSelectedHex = firstHex;
        previousHexSurfaceType = firstHex.HexSurfaceType;
        previousHexObjectOnTileType = firstHex.HexObjectOnTileType;
    }
}

