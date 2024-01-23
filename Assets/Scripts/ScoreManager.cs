using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public TextMeshProUGUI scoreText;
    public int score;
    public TextMeshProUGUI loseScoreText;
    public int loseScore;
    private Dictionary<int, string> levelToPlayerPrefsKey = new Dictionary<int, string>
    {
        { 1, "HighestScoreLv1" },
        { 2, "HighestScoreLv2" },
        { 3, "HighestScoreLv3" },
        { 4, "HighestScoreLv4" }
    };

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "" + score;
        loseScoreText.text = "Your Score is " + loseScore.ToString() ;
        loseScore = score;
        // Kiểm tra xem cấp độ hiện tại là bao nhiêu và lưu điểm cao nhất
        int currentLevel = Level_Data.Instance.levelToLoad;
        if (levelToPlayerPrefsKey.ContainsKey(currentLevel)) {
            PlayerPrefs.SetInt(levelToPlayerPrefsKey[currentLevel], score);
        }
    }

    public void IncreaseScore(int amountToIncrease) {
        score += amountToIncrease;
        ProcessBar.instance.slider.value += amountToIncrease;
    }
}
