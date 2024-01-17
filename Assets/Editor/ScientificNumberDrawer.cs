using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ScientificNumber))]
public class ScientificNumberDrawer : PropertyDrawer
{
    private bool foldout = true;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        foldout = EditorGUI.Foldout(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight), foldout, label, true);
        if (foldout)
        {
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0; // Remettre l'indentation à zéro

            var y = position.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            var height = EditorGUIUtility.singleLineHeight;

            var coefficientProp = property.FindPropertyRelative("Coefficient");
            var exponentProp = property.FindPropertyRelative("Exponent");

            EditorGUI.PropertyField(new Rect(position.x, y, position.width, height), coefficientProp, new GUIContent("Coefficient"));
            y += height + EditorGUIUtility.standardVerticalSpacing;
            EditorGUI.PropertyField(new Rect(position.x, y, position.width, height), exponentProp, new GUIContent("Exponent"));

            EditorGUI.indentLevel = indent; // Restaurer l'indentation
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return foldout ? EditorGUIUtility.singleLineHeight * 3 + EditorGUIUtility.standardVerticalSpacing : EditorGUIUtility.singleLineHeight;
    }
}
