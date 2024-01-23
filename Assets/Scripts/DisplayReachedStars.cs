using UnityEngine;
using UnityEngine.UI;

public class DisplayReachedStars : MonoBehaviour {
    public GameObject[] yellowStars;
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
}
