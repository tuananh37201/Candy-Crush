using UnityEngine;
using DG.Tweening;


public class ResizeBtMainMenu : MonoBehaviour
{
    public GameObject playBt;
    public GameObject exitBt;
    public Vector3 sizeAfter;
    public Vector3 sizeBefore;

    // Start is called before the first frame update
    void Start() {
        ResizePlayButton();
        ResizeExitButton();
    }

    // Update is called once per frame
    void Update() {

    }

    private void ResizePlayButton() {
        playBt.transform.DOScale(sizeAfter, 1.5f).OnComplete(() => {
            playBt.transform.DOScale(sizeBefore, 1.5f).SetEase(Ease.OutBounce).OnComplete(ResizePlayButton);
        }
        );
    }
    
    private void ResizeExitButton() {
        exitBt.transform.DOScale(sizeAfter, 1.5f).OnComplete(() => {
            exitBt.transform.DOScale(sizeBefore, 1.5f).SetEase(Ease.OutBounce).OnComplete(ResizeExitButton);
        }
        );
    }

}
