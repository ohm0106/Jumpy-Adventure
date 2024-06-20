using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(Mission))]
public class MissionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Add Item Condition"))
        {
            AddCondition<ItemCollectionCondition>("NewItemCollectionCondition");
        }

        if (GUILayout.Button("Add Enemy Defeat Condition"))
        {
            AddCondition<EnemyDefeatCondition>("NewEnemyDefeatCondition");
        }
    }

    private void AddCondition<T>(string assetName) where T : ScriptableObject, MissionCondition
    {

        Mission mission = (Mission)target;

        MissionCondition newCondition = CreateInstance<T>(); // 예제 조건 클래스
        AssetDatabase.CreateAsset((Object)newCondition, "Assets/Main/Scripts/Mission/SO/{assetName}.asset");
        AssetDatabase.SaveAssets();

        mission.conditions = newCondition;

        EditorUtility.SetDirty(mission);

    }
}
