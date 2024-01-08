using UnityEngine;
using TMPro;
public enum GameTypeChoco {
    Moves,
    Times
}

[System.Serializable]
public class EndGameRequirementsChocolate {
    public GameTypeChoco gameType;
    public int counterValue;
}

public class EndGameManagerChocolate : MonoBehaviour {
    public static EndGameManagerChocolate instance;
    public EndGameRequirementsChocolate requirements;
    public TextMeshProUGUI counter;
    public GameObject movesLabels;
    public TextMeshProUGUI chocoText;
    public int chocoAmonutToGet;
    public int movedChoco = 0;
    //public GameObject timesLabels;
    public int currentCounterValue;
    private Board board;
    private float timerSeconds;
    public int counterValue;
    private bool setEndGame = false;
    private bool setWinGame = false;
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

        if (requirements.gameType == GameTypeChoco.Moves) {
            movesLabels.SetActive(true);
            //timesLabels.SetActive(false);
        } else {
            timerSeconds = 1;
            movesLabels.SetActive(false);
            // timesLabels.SetActive(true);
        }
        counter.text = currentCounterValue.ToString();
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
        }
    }

    public void FindMovedChoco() {
        GameObject[] chocoObjects = GameObject.FindGameObjectsWithTag("Choco");
        int tweeningCount = 0;
        foreach (GameObject chocoObject in chocoObjects) {
            if (chocoObject.transform.hasChanged) { }
        }
    }

    void Update() {
        chocoText.text = chocoAmonutToGet.ToString();

        if (currentCounterValue == 0) {
            FindObjectOfType<Board>().currentState = GameState.pause;
            SetEndGame();
        }
        if (chocoAmonutToGet <= 0) {
            FindObjectOfType<Board>().currentState = GameState.pause;
            SetWinGame();
        }
    }
}
