using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void LoadLevel1() {
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitGame() {
        Application.Quit();
    }
}
