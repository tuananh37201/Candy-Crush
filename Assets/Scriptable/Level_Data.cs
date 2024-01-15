using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.Tilemaps;

public class Level_Data : MonoBehaviour
{
    [Header("Enter number of level:")]
    public int levelToLoad;

    [Header("Board")]
    public int dWidth;
    public int dHeight;
    public TileType[] dBoardLayout;

    [Header("Match")]
    public int dMove;
    public int dScore1;
    public int dScore2;
    public int dScore3;

    public int _move;
    public int _goalScore;

    public string jsonFilePath = "Assets/CandyCrushData.json";

    void Start()
    {
        LoadData();
    }

    void LoadData()
    {
        if (File.Exists(jsonFilePath))
        {
            string jsonData = File.ReadAllText(jsonFilePath);

            // Deserialize JSON to CandyCrushData
            CandyCrushScriptable.CandyCrushData candyCrushData = JsonConvert.DeserializeObject<CandyCrushScriptable.CandyCrushData>(jsonData);

            if (candyCrushData != null && candyCrushData.levelDataList.Count > levelToLoad)
            {
                // Get the specified level data
                CandyCrushScriptable.GameLevelData levelData = candyCrushData.levelDataList[levelToLoad];

                // Extract board information
                dWidth = levelData.board.width;
                dHeight = levelData.board.height;
                dBoardLayout = levelData.board.boardLayout;

                // Extract match information
                dMove = levelData.match.move;
                dScore1 = levelData.match.score1;
                dScore2 = levelData.match.score2;
                dScore3 = levelData.match.score3;

                Debug.Log("Loaded level data for level " + levelToLoad);
            }
            else
            {
                Debug.LogWarning("Invalid levelToLoad or levelDataList does not contain the specified level.");
            }
        }
        else
        {
            Debug.LogError("File not found: " + jsonFilePath);
        }
    }
}
