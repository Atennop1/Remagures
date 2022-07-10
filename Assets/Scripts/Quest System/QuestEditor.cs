using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

#if UNITY_EDITOR
[CustomEditor(typeof(Quest))]
public class QuestEditor : Editor
{
    private SerializedProperty m_QuestInfoProperty;
    private SerializedProperty m_QuestGoalListProperty;

    [MenuItem("Assets/Create/Quest System/Quest", priority = 0)]
    public static void CreateQuest()
    {
        var newQuest = CreateInstance<Quest>();
        ProjectWindowUtil.CreateAsset(newQuest, "New Quest.asset");
    }

    private void OnEnable()
    {
        m_QuestInfoProperty = serializedObject.FindProperty(nameof(Quest.Information));
        m_QuestGoalListProperty = serializedObject.FindProperty(nameof(Quest.Goals));
    }

    public override void OnInspectorGUI()
    {
        var child = m_QuestInfoProperty.Copy();
        var depth = child.depth;
        child.NextVisible(true);

        EditorGUILayout.LabelField("Quest Info", EditorStyles.boldLabel);
        while (child.depth > depth)
        {
            EditorGUILayout.PropertyField(child, true);
            child.NextVisible(false);
        }

        EditorGUILayout.Space(20);

        if (GUILayout.Button("Add new Goal", GUILayout.Width(170)))
        {
            var newInstance = ScriptableObject.CreateInstance<QuestGoal>();
            newInstance.name = "Goal";
        
            AssetDatabase.AddObjectToAsset(newInstance, target);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            
            m_QuestGoalListProperty.InsertArrayElementAtIndex(m_QuestGoalListProperty.arraySize);
            m_QuestGoalListProperty.GetArrayElementAtIndex(m_QuestGoalListProperty.arraySize - 1).objectReferenceValue = newInstance;
        }

        EditorGUILayout.Space(10);

        Editor editor = null;
        int toDelete = -1;

        for (int i = 0; i < m_QuestGoalListProperty.arraySize; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();

            var item = m_QuestGoalListProperty.GetArrayElementAtIndex(i);
            SerializedObject obj = new SerializedObject(item.objectReferenceValue);

            Editor.CreateCachedEditor(item.objectReferenceValue, null, ref editor);

            editor.OnInspectorGUI();
            EditorGUILayout.EndVertical();

            if (GUILayout.Button("-", GUILayout.Width(32)))
            {
                toDelete = i;
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(20);
        }

        if (toDelete != -1)
        {
            var item = m_QuestGoalListProperty.GetArrayElementAtIndex(toDelete).objectReferenceValue;

            AssetDatabase.RemoveObjectFromAsset(m_QuestGoalListProperty.GetArrayElementAtIndex(toDelete).objectReferenceValue);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            m_QuestGoalListProperty.DeleteArrayElementAtIndex(toDelete);
            DestroyImmediate(item, true);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif