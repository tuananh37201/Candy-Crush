using UnityEngine;
using TMPro;
using System.IO;
using System.Linq;

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
    public bool setWinGame = false;
    public TextMeshProUGUI goalScoreText;
    public int goalScore;
    public TextMeshProUGUI specialBlockAmountText;
    public int specialBlockAmount;
    public GameObject goalScoreObject, chocolateIcon, iceIcon, biscuitIcon, specialBlockAmountIcon;
    private Level_Data currentLevelData; // Variable to hold the current level data
    //private string filePath = "Assets\\Resources";
    public TileKind tileKind;

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
        CheckTileTypes();
        SetUpGame();
        SetIconsBasedOnTileKind();
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
    void CheckTileTypes()
    {
        bool isChocolate = false;
        bool isBreakable = false;
        bool isBitcuit = false;
        isChocolate = board.boardLayout.Any(tileType => tileType.tileKind == TileKind.Chocolate);
        isBreakable = board.boardLayout.Any(tileType => tileType.tileKind == TileKind.Breakable);
        isBitcuit = board.boardLayout.Any(tileType => tileType.tileKind == TileKind.Biscuit);
        tileKind = isBreakable ? TileKind.Breakable : (isChocolate ? TileKind.Chocolate : (isBitcuit ? TileKind.Biscuit : TileKind.Normal));
    }
    void SetIconsBasedOnTileKind()
    {
        chocolateIcon.SetActive(tileKind == TileKind.Chocolate);
        iceIcon.SetActive(tileKind == TileKind.Breakable);
        goalScoreObject.SetActive(tileKind == TileKind.Normal);
        biscuitIcon.SetActive(tileKind == TileKind.Biscuit);
        specialBlockAmountIcon.SetActive(tileKind == TileKind.Chocolate ||
                                         tileKind == TileKind.Breakable ||
                                         tileKind == TileKind.Biscuit);
    }
    public void DecreaseCountervalue()
    {
        Level_Data.Instance.dMove--;
        counter.text = "" + Level_Data.Instance.dMove;
        if (Level_Data.Instance.dMove == 0)
        {
            board.currentState = GameState.pause;
            Level_Data.Instance.dMove = 0;
            counter.text = "" + Level_Data.Instance.dMove;
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
            PlayerPrefs.SetInt("HeartAmount", HeartAmountManager.instance.heartAmount -= 1);
            PlayerPrefs.Save();
        }
    }
    public void SetWinGame()
    {
        if (!setWinGame)
        {
            FindObjectOfType<PopupSetting>().PanelFadeIn();
            GameObjectLV1.Instance.WinPanelAppear();
            FindObjectOfType<SoundManager>().audioSource.Stop();
            FindObjectOfType<SoundManager>().audioSource.PlayOneShot(FindObjectOfType<SoundManager>().winSound);
            CreateJsonFile();
            if (LevelButton.Instance.nextLevel == Level_Data.Instance.levelToLoad)
            {
                PlayerPrefs.SetInt("next_level", LevelButton.Instance.nextLevel += 1);
            }
            //PlayerPrefs.DeleteKey("next_level");
            //LevelButton.Instance.nextLevel = PlayerPrefs.GetInt("new next_level", Level_Data.Instance.levelToLoad += 1);
            PlayerPrefs.SetInt("HeartAmount", HeartAmountManager.instance.heartAmount);
            PlayerPrefs.SetInt("MyMoney", ItemPriceManager.Instance.myMoney);
            PlayerPrefs.SetInt("ColorBombAmount", ItemPriceManager.Instance.colorBombAmount);
            PlayerPrefs.SetInt("BombAmount", ItemPriceManager.Instance.bombAmount);
            PlayerPrefs.SetInt("ExtraStepAmount", ItemPriceManager.Instance.extraStepAmount);
            PlayerPrefs.Save();
            setWinGame = true;
        }
    }
    public void CreateJsonFile()
    {
        //string fileName = "vipro.json";
        //string jsonData = JsonUtility.ToJson(FindObjectOfType<ScoreManager>().score);
        //string filePath = "Assets/Resources" + "/" + fileName;
        //File.WriteAllText(filePath, jsonData);
        //Debug.Log("Data saved to: " + filePath);
    }
    void Update()
    {
        //GameObject choco = GameObject.FindGameObjectWithTag("Chocolate");

        if (Level_Data.Instance.dMove == 0)
        {
            FindObjectOfType<Board>().currentState = GameState.pause;
            SetEndGame();
        }
        if (FindObjectOfType<ScoreManager>().score >= Level_Data.Instance.dGoalScore)
        {
            FindObjectOfType<Board>().currentState = GameState.pause;
            SetWinGame();
            GetStarManager.instance.SetAmountOfReachedStars();
        }
        goalScoreText.text = Level_Data.Instance.dGoalScore.ToString();
        counter.text = Level_Data.Instance.dMove.ToString();
        if(Level_Data.Instance.dSpecialBlockAmount < 0) Level_Data.Instance.dSpecialBlockAmount = 0;
        specialBlockAmountText.text = Level_Data.Instance.dSpecialBlockAmount.ToString();
    }
}
