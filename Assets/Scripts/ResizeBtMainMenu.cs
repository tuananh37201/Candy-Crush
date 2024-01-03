using UnityEngine;
using DG.Tweening;


public class ResizeBtMainMenu : MonoBehaviour
{
    public static ResizeBtMainMenu instance;
    public GameObject playBt;
    public Vector3 sizeAfter;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start() {
        ResizePlayButton();
    }

    // Update is called once per frame
    void Update() {

    }

    private void ResizePlayButton() {
        playBt.transform.DOScale(sizeAfter, 1f);
    }

    private void PlayAndExitAppear() {
        playBt.SetActive(true);
        MoveObjectMainMenu.instance.exitPanelMoveBefore();
    }

    public void PlayAndExitDisAppear() {
        playBt.SetActive(false);
        MoveObjectMainMenu.instance.exitPanelMoveAfter();
    }

}
