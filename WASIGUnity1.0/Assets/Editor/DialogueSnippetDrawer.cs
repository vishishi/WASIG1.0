using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(DialogueSnippet))]
public class DialogueSnippetDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Get the parent array/list property
        if (property.serializedObject != null && property.propertyPath.Contains("["))
        {
            string path = property.propertyPath;
            int startIndex = path.IndexOf("[") + 1;
            int endIndex = path.IndexOf("]");
            string indexStr = path.Substring(startIndex, endIndex - startIndex);

            if (int.TryParse(indexStr, out int index))
            {
                label.text = $"Snippet {index}";
            }
        }

        EditorGUI.PropertyField(position, property, label, true);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }
}
