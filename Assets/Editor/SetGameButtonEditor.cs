using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SetButton))]
[CanEditMultipleObjects]
[System.Serializable]

public class SetGameButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SetButton myScript = target as SetButton;
        switch (myScript.ButtonType)
        {
            case SetButton.EButtonType.PairNumberBtn:
                myScript.pairsNumber = (GameSetting.EPairsNumber) EditorGUILayout.EnumPopup(label:"Pair Number", myScript.pairsNumber); 
                break;
            case SetButton.EButtonType.PuzzleCategoryBtn:
                myScript.puzzleCetegories = (GameSetting.EpuzzleCetegories)EditorGUILayout.EnumPopup(label: "Puzzle Categories", myScript.puzzleCetegories); 
                break;
        }
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }
}
