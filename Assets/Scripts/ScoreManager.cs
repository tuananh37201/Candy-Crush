using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public TextMeshProUGUI scoreText;
    public int score;
    public TextMeshProUGUI loseScoreText;
    public int loseScore;


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
    }

    public void IncreaseScore(int amountToIncrease) {
        score += amountToIncrease;
        ProcessBar.instance.slider.value += amountToIncrease;
    }
}
