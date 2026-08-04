using System.Collections;
using UnityEngine;
using UnityEditor;


#if UNITY_EDITOR

[CustomEditor(typeof(FloatVariable))]
[CanEditMultipleObjects]
public class FloatVariableEditor : Editor 
{   
    FloatVariable myScript;

    void OnEnable()
    {
        myScript = (FloatVariable) target;
    }

    public override void OnInspectorGUI()
    {
        //magic
        serializedObject.Update();

        DrawDefaultInspector();

        if(GUILayout.Button("onValueChanged"))
        {
            myScript.currentValue = myScript.currentValue;
        }

        //magic
        serializedObject.ApplyModifiedProperties();
    }

}

#endif