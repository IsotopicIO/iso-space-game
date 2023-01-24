#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SetAnchorsToCornersEditor : EditorWindow
{
    [MenuItem("GameObject/Set Anchors To Fill/Selected Objects Only")]
    [MenuItem("CONTEXT/RectTransform/Set Anchors To Fill")]
    public static void Action(MenuCommand command)
    {
        foreach (var go in Selection.gameObjects)
        {
            InsideAction(go);
        }
    }

    [MenuItem("GameObject/Set Anchors To Fill/Selected Objects and Children")]
    public static void Action2(MenuCommand command)
    {
        foreach (var go in Selection.gameObjects)
        {
            InsideAction(go);
            foreach (var t in go.GetComponentsInChildren<RectTransform>(true))
            {
                InsideAction(t.gameObject);
            }
        }
    }

    private static void InsideAction(GameObject go)
    {
        RectTransform t = go.GetComponent<RectTransform>();
        if (t == null) return;
        RectTransform pt = (t.parent is RectTransform) ? (RectTransform)t.parent : null;

        if (pt == null)
        {
            t.anchorMin = Vector2.zero;
            t.anchorMax = Vector2.one;
            return;
        }


        Vector2 newAnchorsMin = new Vector2(t.anchorMin.x + t.offsetMin.x / pt.rect.width,
                                         t.anchorMin.y + t.offsetMin.y / pt.rect.height);
        Vector2 newAnchorsMax = new Vector2(t.anchorMax.x + t.offsetMax.x / pt.rect.width,
                                         t.anchorMax.y + t.offsetMax.y / pt.rect.height);

        t.anchorMin = newAnchorsMin;
        t.anchorMax = newAnchorsMax;
        t.offsetMin = t.offsetMax = new Vector2(0, 0);
    }
}
#endif
