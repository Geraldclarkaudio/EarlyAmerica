using UnityEditor;

namespace PaperKiteStudio.Dangers
{
    [CustomEditor(typeof(MovingObject), true)]
    public class MovingObjectEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            // Draw default fields
            SerializedProperty speedProp = serializedObject.FindProperty("_speed");
            SerializedProperty modeProp = serializedObject.FindProperty("movementMode");
            SerializedProperty targetProp = serializedObject.FindProperty("targetObject");
            SerializedProperty fixedDirProp = serializedObject.FindProperty("fixedDirection");

            EditorGUILayout.PropertyField(speedProp);
            EditorGUILayout.PropertyField(modeProp);

            MovementMode mode = (MovementMode)modeProp.enumValueIndex;

            if (mode == MovementMode.TowardTarget)
            {
                EditorGUILayout.PropertyField(targetProp);
            }
            else if (mode == MovementMode.FixedDirection)
            {
                EditorGUILayout.PropertyField(fixedDirProp);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}