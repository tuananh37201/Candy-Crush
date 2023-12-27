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
    public EndGameRequirements requirements;
    public TextMeshProUGUI counter;
    public GameObject movesLabels;
    public GameObject timesLabels;
    public int currentCounterValue;
    private float timerSeconds;

    void Start() {
        SetUpGame();
    }

    void SetUpGame() {
        currentCounterValue = requirements.counterValue;
        if (requirements.gameType == GameType.Moves) {
            movesLabels.SetActive(true);
            timesLabels.SetActive(false);
        } else {
            timerSeconds = 1;
            movesLabels.SetActive(false);
            timesLabels.SetActive(true);
        }
        counter.text = "" + currentCounterValue;
    }
    public void DecreaseCountervalue() {
            currentCounterValue--;
            counter.text = "" + currentCounterValue;
        if(currentCounterValue == 0) {
            currentCounterValue = 0;
            counter.text = "" + currentCounterValue;
        }
    }
     
    void Update() {
        if(requirements.gameType == GameType.Times && currentCounterValue  > 0) {
            timerSeconds -= Time.deltaTime;
            if(timerSeconds <= 0) {
                DecreaseCountervalue();
                timerSeconds = 1;
            }
        }
    }
}
