using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Collections;

public class ButtonManager : MonoBehaviour
{
    public GameObject levelButton;
    public Vector3 sizeBeforeClick;
    public Vector3 sizeAfterClick;

    public void SelectLevelScene() {
        SceneManager.LoadScene("SelectLevel");
    }

    public void LoadLevel1() {
        StartCoroutine(DelayAction(0.45f));
    }

    IEnumerator DelayAction(float delayTime) {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene("SampleScene");
    }

    public void BackToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void ResizeButton() {
        levelButton.transform.DOScale(sizeAfterClick, 0.15f).SetEase(Ease.OutBounce).OnComplete(() => {
                levelButton.transform.DOScale(sizeBeforeClick, 0.65f);
            });
    }
}
