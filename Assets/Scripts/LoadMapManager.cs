using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMapManager : MonoBehaviour
{
    public static LoadMapManager instance;
    public int selectedMapIndex;

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
    }
}