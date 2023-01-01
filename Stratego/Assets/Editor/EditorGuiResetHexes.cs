using UnityEngine;
using UnityEditor;
using System.Linq;
using System;

public class EditorGuiResetHexes : UnityEditor.EditorWindow
{

    [MenuItem("Editor/Reset Hexes")]
    static void Init()
    {
        EditorGuiResetHexes window = (EditorGuiResetHexes)EditorWindow.GetWindow(typeof(EditorGuiResetHexes), true, "Reset");
        window.Show();
    }

    void OnGUI()
    {       
        if (GUILayout.Button("Reset surfaces to plain"))
        {
            this.ResetAllHexesToPlain();
        }          
    }

    private void ResetAllHexesToPlain()
    {
        var allHexes = GameObject.FindObjectsOfType<Hex>().ToList();

        allHexes.ForEach(hex =>
        {
            hex.HexSurfaceType = HexSurfaceType.Simple_Plain;
            HexEditorUtil.HexSurfaceTypeChanged(hex, hex.HexSurfaceType);
        });
    }   
}