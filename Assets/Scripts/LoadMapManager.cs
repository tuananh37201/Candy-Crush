using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMapManager : MonoBehaviour
{
    public static LoadMapManager instance;
    public int selectedMapIndex;
    public GameObject NotEnoughHeartNotice;
    public GameObject playNotice;

    private void Awake()
    {
        instance = this;
    }

    public void LoadMap()
    {
        if(selectedMapIndex <= LevelButton.Instance.nextLevel && 
           HeartAmountManager.instance.heartAmount >0)
        {
            SceneManager.LoadScene("Level Test 1");
        }
        else if(LevelButton.Instance.nextLevel < selectedMapIndex) playNotice.SetActive(true);
    }

    public void NotEnoughHeart() {
        if(HeartAmountManager.instance.heartAmount <= 0) {
            NotEnoughHeartNotice.SetActive(true);
        }
    }
}