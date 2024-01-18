using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.Tilemaps;

public class Level_Data : MonoBehaviour
{
    public static Level_Data Instance;
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
    public int dGoalScore;
    public int dSpecialBlockAmount;
    public TextAsset jsonDataFile;

    private void Awake()
    {
        Instance = this;
        levelToLoad = LoadMapManager.instance.selectedMapIndex;
        LoadData();
    }
    void Start()    
    {
        
    }

    void LoadData()
    {
        if (jsonDataFile != null)
        {
            string jsonData = jsonDataFile.text;

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
                dGoalScore = levelData.match.goalScore;
                dSpecialBlockAmount = levelData.match.specialBlockAmount;

                //Debug.Log("Loaded level data for level " + levelToLoad);
            }
            else
            {
                Debug.LogWarning("Invalid levelToLoad or levelDataList does not contain the specified level.");
            }
        }
        else
        {
            Debug.LogError("File not found: " + jsonDataFile);
        }
    }
}
