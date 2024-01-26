using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {
    public static ButtonManager instance;
    public bool isEnableNextMap;
    public int mapId;

    private void Awake() {
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

    public void SetMapId() {
        TextOfSelectLevel.Instance.currentLevel = mapId;
        // Sử dụng danh sách ánh xạ giữa mapId và selectedMapIndex
        List<int> selectedMapIndices = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
        // Kiểm tra xem mapId có nằm trong danh sách không
        if (mapId >= 1 && mapId <= selectedMapIndices.Count)
            LoadMapManager.instance.selectedMapIndex = selectedMapIndices[mapId - 1];
    }
    public void BackToMainMenu() {
        //if (LevelButton.Instance.nextLevel == Level_Data.Instance.levelToLoad) {
        //    PlayerPrefs.SetInt("next_level", Level_Data.Instance.levelToLoad += 1);
        //}
        SceneManager.LoadScene("MainMenu");
    }
    public void SelectLevelSceneAtMenu() {
        SceneManager.LoadScene("SelectLevel");
    }
    public void SelectLevelScene() {
        isEnableNextMap = true;
        SceneManager.LoadScene("SelectLevel");
    }

    public void DeleteData() {
        PlayerPrefs.DeleteAll();
    }
}
