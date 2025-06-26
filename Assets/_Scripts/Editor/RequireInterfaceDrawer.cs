#if UNITY_EDITOR
using Separated.CustomAttribute;
using UnityEditor;
using UnityEngine;

namespace Separated.CustomDrawer
{
    [CustomPropertyDrawer(typeof(RequireInterface))]
    public class RequireInterfaceDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            RequireInterface requireAttribute = (RequireInterface)attribute;

            EditorGUI.BeginProperty(position, label, property);

            Rect fieldRect = new(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            property.objectReferenceValue = EditorGUI.ObjectField(fieldRect, label, property.objectReferenceValue, typeof(GameObject), false);

            if (property.objectReferenceValue is GameObject go &&
                go.GetComponent(requireAttribute.RequiredType) == null)
            {
                property.objectReferenceValue = null;
                GUI.changed = true;
            }

            EditorGUI.EndProperty();
        }

        // public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        // {
        //     RequireInterface requireAttribute = (RequireInterface)attribute;

        //     if (property.objectReferenceValue is GameObject go &&
        //         go.GetComponent(requireAttribute.RequiredType) == null)
        //     {
        //         return EditorGUIUtility.singleLineHeight * 2.5f + 4;
        //     }

        //     return EditorGUIUtility.singleLineHeight;
        // }
    }
}
#endif