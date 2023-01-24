#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class SimulateClickEditor : EditorWindow
{
    [MenuItem("CONTEXT/Button/Simulate Click")]
    [MenuItem("CONTEXT/Dropdown/Simulate Click")]
    [MenuItem("CONTEXT/TMP_Dropdown/Simulate Click")]
    [MenuItem("CONTEXT/Dropdown/Simulate Click")]
    public static void Action(MenuCommand command)
    {
        foreach (var go in Selection.gameObjects)
        {
            ExecuteEvents.Execute(go, new BaseEventData(EventSystem.current), ExecuteEvents.submitHandler);
        }
    }
}
#endif