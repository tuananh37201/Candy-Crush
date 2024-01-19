using TMPro;
using UnityEngine;

public class TextOfSelectLevel : MonoBehaviour
{
    public static TextOfSelectLevel Instance;
    public TextMeshProUGUI currentLevelText;
    public int currentLevel;
    public GameObject popUpSetting;

    private void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        currentLevelText.text = "Level " + currentLevel.ToString();
    }
}
