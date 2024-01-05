using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
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

    public void LoadLevel1(){
        SceneManager.LoadScene("Level1");
    }
    public void LoadLevel2(){
        SceneManager.LoadScene("Level2");
    }
    public void LoadLevel3(){
        SceneManager.LoadScene("Level3");
    }
    public void LoadLevel4(){
        SceneManager.LoadScene("Level4");
    }
    public void LoadLevel5(){
        SceneManager.LoadScene("Level5");
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
