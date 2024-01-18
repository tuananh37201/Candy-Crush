using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager instance;
    public bool isEnableNextMap;

    private void Awake()
    {
        instance = this;
        //DontDestroyOnLoad(gameObject);
    }
    // private void Start()
    // {
    //     // Đăng ký sự kiện cho tất cả các LevelButton trong scene
    //     LevelButton[] levelButtons = FindObjectsOfType<LevelButton>();
    //     foreach (var button in levelButtons)
    //     {
    //         // Chú ý: Đối số của AddListener cần là một UnityAction, nên sử dụng lambda expression
    //         button.OnButtonPressed.AddListener((int level) => LoadLevel(level));
    //     }
    // }

    // public void LoadLevel(int level)
    // {
    //     Debug.Log("LoadLevel" + level);
    //     SceneManager.LoadScene("Level" + level);
    // }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void SelectLevelSceneAtMenu()
    {
        SceneManager.LoadScene("SelectLevel");
    }
    public void SelectLevelScene()
    {
        isEnableNextMap = true;
        SceneManager.LoadScene("SelectLevel");
    }

    public void DeleteData()
    {
        PlayerPrefs.DeleteKey("next_level");
        PlayerPrefs.DeleteKey("ColorBombAmount");
        PlayerPrefs.DeleteKey("BombAmount");
        PlayerPrefs.DeleteKey("BombAmount");
        PlayerPrefs.DeleteKey("ExtraStepAmount");
        PlayerPrefs.DeleteKey("HeartAmount");
    }
}
