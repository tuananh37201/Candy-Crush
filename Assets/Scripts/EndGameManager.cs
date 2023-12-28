using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum GameType {
    Moves,
    Times
}

[System.Serializable]
public class EndGameRequirements {
    public GameType gameType;
    public int counterValue;
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

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }
    void Start() {
        currentCounterValue = requirements.counterValue;
        board = FindObjectOfType<Board>();
        SetUpGame();
        
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

    void Update() {
        if (currentCounterValue == 0) {
            //currentState = GameState.pause;
            FindObjectOfType<PopupSetting>().PanelFadeIn();
            GameObjectLV1.Instance.LosePanelAppear();
        }
        if (requirements.gameType == GameType.Times && currentCounterValue > 0) {
            timerSeconds -= Time.deltaTime;
            if (timerSeconds <= 0) {
                DecreaseCountervalue();
                timerSeconds = 1;
            }
        }
    }
}
