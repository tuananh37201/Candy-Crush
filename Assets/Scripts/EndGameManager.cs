using UnityEngine;
using TMPro;
using System.IO;

public enum GameType
{
    Moves
}

[System.Serializable]
public class EndGameRequirements
{
    public GameType gameType;
}

public class EndGameManager : MonoBehaviour
{
    public static EndGameManager instance;
    public EndGameRequirements requirements;
    public TextMeshProUGUI counter;
    public GameObject movesLabels;
    //public GameObject timesLabels;
    private Board board;
    private float timerSeconds;
    public int counterValue;
    private bool setEndGame = false;
    private bool setWinGame = false;
    public TextMeshProUGUI goalScoreText;
    public int goalScore;
    private Level_Data currentLevelData; // Variable to hold the current level data
    //private string filePath = "Assets\\Resources";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        board = FindObjectOfType<Board>();

    }
    void Start()
    {
        SetUpGame();
    }


    void SetUpGame()
    {

        if (requirements.gameType == GameType.Moves)
        {
            movesLabels.SetActive(true);
            //timesLabels.SetActive(false);
        }
        else
        {
            timerSeconds = 1;
            movesLabels.SetActive(false);
            // timesLabels.SetActive(true);
        }
        counter.text = Level_Data.Instance.dMove.ToString();
    }
    public void DecreaseCountervalue()
    {
        //if (board.currentState != GameState.pause) {
        Level_Data.Instance.dMove--;
        counter.text = "" + Level_Data.Instance.dMove;
        if (Level_Data.Instance.dMove == 0)
        {
            board.currentState = GameState.pause;
            Level_Data.Instance.dMove = 0;
            counter.text = "" + Level_Data.Instance.dMove;
            //}
        }
    }

    public void SetEndGame()
    {
        if (!setEndGame)
        {
            FindObjectOfType<PopupSetting>().PanelFadeIn();
            GameObjectLV1.Instance.LosePanelAppear();
            setEndGame = true;
            FindObjectOfType<SoundManager>().audioSource.Stop();
            FindObjectOfType<SoundManager>().audioSource.PlayOneShot(FindObjectOfType<SoundManager>().loseSound);
        }
    }

    public void SetWinGame()
    {
        if (!setWinGame)
        {
            FindObjectOfType<PopupSetting>().PanelFadeIn();
            GameObjectLV1.Instance.WinPanelAppear();
            setWinGame = true;
            FindObjectOfType<SoundManager>().audioSource.Stop();
            FindObjectOfType<SoundManager>().audioSource.PlayOneShot(FindObjectOfType<SoundManager>().winSound);
            CreateJsonFile();
        }
    }

    public void CreateJsonFile()
    {
        string fileName = "vipro.json";
        string jsonData = JsonUtility.ToJson(FindObjectOfType<ScoreManager>().score);
        string filePath = "Assets/Resources" + "/" + fileName;
        File.WriteAllText(filePath, jsonData);
        Debug.Log("Data saved to: " + filePath);
    }

    void Update()
    {
        if (Level_Data.Instance.dMove == 0)
        {
            FindObjectOfType<Board>().currentState = GameState.pause;
            SetEndGame();
        }
        if (FindObjectOfType<ScoreManager>().score >= Level_Data.Instance.dGoalScore)
        {
            FindObjectOfType<Board>().currentState = GameState.pause;
            SetWinGame();
        }
        goalScoreText.text = Level_Data.Instance.dGoalScore.ToString();
        counter.text = Level_Data.Instance.dMove.ToString();
    }
}
