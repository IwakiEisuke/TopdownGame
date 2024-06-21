using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DebugMapCreator))]
public class DebugEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DebugMapCreator myScript = (DebugMapCreator)target;
        if (GUILayout.Button("Execute MyFunction"))
        {
            myScript.MyFunction();
        }
    }
}
