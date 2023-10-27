using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DrawerController))]
public class DrawerControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawerController drawer = (DrawerController)target;

        DrawDefaultInspector();


        if (GUILayout.Button("Set Open Position Offset"))
        {
            drawer.SetOpenOffset();
        }
    }
}
