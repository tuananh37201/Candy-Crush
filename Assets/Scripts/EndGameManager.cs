using UnityEngine;
using TMPro;
using System.IO;

public enum GameType {
    Moves
}

[System.Serializable]
public class EndGameRequirements {
    public GameType gameType;
}

public class EndGameManager : MonoBehaviour {
    public static EndGameManager instance;
    public EndGameRequirements requirements;
    public TextMeshProUGUI counter;
    public GameObject movesLabels;
    //public GameObject timesLabels;
    public int currentCounterValue;
    private Board board;
    private float timerSeconds;
    public int counterValue;
    private bool setEndGame = false;
    private bool setWinGame = false;
    public TextMeshProUGUI goalScoreText;
    public int goalScore;
    private LevelData currentLevelData; // Variable to hold the current level data
    private string filePath = "C:\\Users\\deven\\OneDrive\\Tài liệu\\GitHub\\Candy-Crush\\Assets\\Resources";

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        board = FindObjectOfType<Board>();
        LoadLevelData();

    }
    void Start() {
        SetUpGame();
        
    }

    private void LoadLevelData()
    {
        // Parse JSON data into LevelData
        currentLevelData = JsonUtility.FromJson<LevelData>(board.levelJson.ToString());
        goalScore = currentLevelData._goalScore;
        currentCounterValue = currentLevelData._move;
    }

    void SetUpGame() {
        
        if (requirements.gameType == GameType.Moves) {
            movesLabels.SetActive(true);
            //timesLabels.SetActive(false);
        } else {
            timerSeconds = 1;
            movesLabels.SetActive(false);
           // timesLabels.SetActive(true);
        }
        counter.text =  currentCounterValue.ToString();
    }
    public void DecreaseCountervalue() {
        //if (board.currentState != GameState.pause) {
            currentCounterValue--;
            counter.text = "" + currentCounterValue;
            if (currentCounterValue == 0) {
                board.currentState = GameState.pause;
                currentCounterValue = 0;
                counter.text = "" + currentCounterValue;
            //}
        }
    }

    public void SetEndGame() {
        if (!setEndGame) {
            FindObjectOfType<PopupSetting>().PanelFadeIn();
            GameObjectLV1.Instance.LosePanelAppear();
            setEndGame = true;
            FindObjectOfType<SoundManager>().audioSource.Stop();
            FindObjectOfType<SoundManager>().audioSource.PlayOneShot(FindObjectOfType<SoundManager>().loseSound);
        }
    }

    public void SetWinGame() {
        if (!setWinGame) {
            FindObjectOfType<PopupSetting>().PanelFadeIn();
            GameObjectLV1.Instance.WinPanelAppear();
            setWinGame = true;
            FindObjectOfType<SoundManager>().audioSource.Stop();
            FindObjectOfType<SoundManager>().audioSource.PlayOneShot(FindObjectOfType<SoundManager>().winSound);
            SaveDateToJson();
        }
    }

    public void SaveDateToJson()
    {
        string jsonData = JsonUtility.ToJson(FindObjectOfType<ScoreManager>().score);
        File.WriteAllText(filePath, jsonData);
        Debug.Log("Data saved to: " + filePath);
    }

    void Update() {
        if (currentCounterValue == 0) {
            FindObjectOfType<Board>().currentState = GameState.pause;
            SetEndGame();
        }
        if(FindObjectOfType<ScoreManager>().score >= goalScore) {
            FindObjectOfType<Board>().currentState = GameState.pause;
            SetWinGame();
        }
        goalScoreText.text = goalScore.ToString();
        counter.text = currentCounterValue.ToString();  
    }
}
