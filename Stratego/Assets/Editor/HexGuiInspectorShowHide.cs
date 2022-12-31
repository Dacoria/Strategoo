using UnityEditor;

[CustomEditor(typeof(Hex))]
public class HexGuiInspectorShowHide : Editor
{
    public override void OnInspectorGUI()
    {
        // If we call base the default inspector will get drawn too.
        // Remove this line if you don't want that to happen.
        //base.OnInspectorGUI();

        //Hex hex = target as Hex;
        
        //hex.PieceType =  EditorGUILayout.DropdownButton
        //target. = EditorGUILayout.Toggle("myBool", target.myBool);

        //if (target.myBool)
        //{
        //    target.someFloat = EditorGUILayout.FloatField("Some Float:", target.someFloat);
        //
        //}
    }
}