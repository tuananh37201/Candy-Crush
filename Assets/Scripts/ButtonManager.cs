using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    private void Start()
    {
        // Đăng ký sự kiện cho tất cả các LevelButton trong scene
        LevelButton[] levelButtons = FindObjectsOfType<LevelButton>();
        foreach (var button in levelButtons)
        {
            // Chú ý: Đối số của AddListener cần là một UnityAction, nên sử dụng lambda expression
            button.OnButtonPressed.AddListener((int level) => LoadLevel(level));
        }
    }

    public void LoadLevel(int level)
    {
        Debug.Log("LoadLevel" + level);
        SceneManager.LoadScene("Level" + level);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void SelectLevelScene()
    {
        SceneManager.LoadScene("SelectLevel");
    }
}
