#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ConditionalPropertyAttribute))]
public class ConditionalPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var conditionalAttribute = (ConditionalPropertyAttribute) attribute;
        var name = conditionalAttribute.Name;

        var path = property.propertyPath;
        var prop = property.serializedObject.FindProperty(path.Replace(property.name, name));

        if (prop != null)
        {
            if ((prop.boolValue && !conditionalAttribute.Opposite) || (!prop.boolValue && conditionalAttribute.Opposite))
            {
                EditorGUI.PropertyField(position, property, new GUIContent(SplitCamelCase(property.name)));
            }
        }
    }
    private static string SplitCamelCase(string input)
    {
        return System.Text.RegularExpressions.Regex.Replace(input, "([A-Z])", " $1", System.Text.RegularExpressions.RegexOptions.Compiled).Trim();
    }
}
#endif