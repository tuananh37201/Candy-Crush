using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using static UnityEngine.GraphicsBuffer;
using UnityEditor;

[JsonConverter(typeof(StringEnumConverter))]
public enum TileKind
{
    Blank,
    Breakable,
    Chocolate,
    Lock,
    Biscuit,
    Normal
}

[CreateAssetMenu(menuName = "Assets/CandyCrushScriptable")]
public class CandyCrushScriptable : ScriptableObject
{
    [Serializable]
    public class GameLevelData
    {
        public BoardData board;
        public MatchData match;
    }

    [Serializable]
    public class CandyCrushData
    {
        public List<GameLevelData> levelDataList;
    }

    public List<GameLevelData> levelDataList = new();

#if UNITY_EDITOR
    [CustomEditor(typeof(CandyCrushScriptable))]
    public class CandyCrushScriptableEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            CandyCrushScriptable candyCrushScriptable = (CandyCrushScriptable)target;

            if (GUILayout.Button("Save"))
            {
                SaveScriptable(candyCrushScriptable);
            }
        }
    }

    public static void SaveScriptable(CandyCrushScriptable candyCrushScriptable)
    {
        if (candyCrushScriptable.levelDataList != null && candyCrushScriptable.levelDataList.Count > 0)
        {
            CandyCrushData candyCrushData = new CandyCrushData
            {
                levelDataList = candyCrushScriptable.levelDataList
            };

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented
            };
            string jsonData = JsonConvert.SerializeObject(candyCrushData, settings);

            try
            {
                string filePath = "Assets/CandyCrushData.json";
                System.IO.File.WriteAllText(filePath, jsonData);
                UnityEditor.EditorUtility.SetDirty(candyCrushScriptable);
                UnityEditor.AssetDatabase.SaveAssets();
                Debug.Log("Level data has been saved.");
            }
            catch (Exception e)
            {
                Debug.LogError("Error saving level data: " + e.Message);
            }
        }
        else
        {
            Debug.LogWarning("Level data list is null or empty. Nothing to save.");
        }
    }
#endif
}
