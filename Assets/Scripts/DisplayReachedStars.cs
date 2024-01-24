using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class DisplayReachedStars : MonoBehaviour {
    public TextMeshProUGUI highestScoreText;
    public int highestScore;
    public GameObject[] yellowStars;
    private Dictionary<int, string> levelToPlayerPrefsKey = new Dictionary<int, string>
    {
        { 1, "HighestScoreLv1" },
        { 2, "HighestScoreLv2" },
        { 3, "HighestScoreLv3" },
        { 4, "HighestScoreLv4" },
        { 5, "HighestScoreLv5" },
        { 6, "HighestScoreLv6" },
        { 7, "HighestScoreLv7" },
        { 8, "HighestScoreLv8" },
        { 9, "HighestScoreLv9" },
        { 10, "HighestScoreLv10" },
        { 11, "HighestScoreLv11" },
        { 12, "HighestScoreLv12" },
        { 13, "HighestScoreLv13" },
        { 14, "HighestScoreLv14" },
        { 15, "HighestScoreLv15" }
        // Thêm các cấp độ khác nếu cần
    };
    // [SerializeField] private Button m_ExitButton;

    // private void Awake() {
    //     m_ExitButton.onClick.AddListener(OnExit);
    // }

    // private void OnExit() {
    //     yellowStars[0].SetActive(false);
    // }

    public void SetStarAmount() {
        int currentLevel = TextOfSelectLevel.Instance.currentLevel;
        if (PlayerPrefs.HasKey($"YellowStarLv{currentLevel}")) {
            int yellowStarAmount = PlayerPrefs.GetInt($"YellowStarLv{currentLevel}");

            for (int i = 0; i < yellowStars.Length; i++) {
                yellowStars[i].SetActive(i < yellowStarAmount);
            }
        }
    }

    void Update() {
        int currentLevel = TextOfSelectLevel.Instance.currentLevel;

        if (levelToPlayerPrefsKey.ContainsKey(currentLevel)) {
            string playerPrefsKey = levelToPlayerPrefsKey[currentLevel];
            highestScore = PlayerPrefs.GetInt(playerPrefsKey);
            highestScoreText.text = "Highest Score " + highestScore.ToString();
        }
    }
}
