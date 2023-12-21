using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void SelectLevelScene() {
        SceneManager.LoadScene("SelectLevel");
    }

    public void LoadLevel1() {
        SceneManager.LoadScene("Level1");
    }

    public void BackToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
