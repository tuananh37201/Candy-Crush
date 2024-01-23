using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopupSetting : MonoBehaviour
{
    public float fadeTime = 0.5f;
    public CanvasGroup canvasGroup;
    public RectTransform rectTransform;
    public List<GameObject> items = new List<GameObject>();
    public bool isFadeIn = true;
    public int movePos;
    public static PopupSetting instance { get; private set; }
    public GameObject yellowStar1;
   //[SerializeField] private Button m_ExitButton;

    //private void OnExit() {
    //    Debug.Log("OnExit");
    //    yellowStar1.SetActive(false);
    //}

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
        //m_ExitButton.onClick.AddListener(OnExit);
    }
    // Phương thức để hiển thị popup và thực hiện animation
    public void PanelFadeIn()
    {
        isFadeIn = true;
        if (isFadeIn)
        {
            canvasGroup.alpha = 0; // Đặt độ đục của canvasGroup về 0
            rectTransform.transform.localPosition = new Vector3(0f, 0f, 0f); // Đặt vị trí ban đầu của panel
            rectTransform.DOAnchorPos(new Vector2(0, movePos), fadeTime, false).SetEase(Ease.OutElastic); // Thực hiện animation dịch chuyển panel vào vị trí mới
            canvasGroup.DOFade(1, fadeTime); // Thực hiện animation làm đậm canvasGroup lên giá trị 1
                                             //StartCoroutine("ItemsAnimation"); // Bắt đầu coroutine để thực hiện animation cho các items
        }
        else return;
    }


    // Phương thức để ẩn popup và thực hiện animation
    public void PanelFadeOut()
    {
        canvasGroup.alpha = 1f; // Đặt độ đục của canvasGroup về 1
        rectTransform.transform.localPosition = new Vector3(0f, 0f, 0f); // Đặt vị trí ban đầu của panel
        rectTransform.DOAnchorPos(new Vector2(0, -2000f), 0.35f, false).SetEase(Ease.InOutQuint); // Thực hiện animation dịch chuyển panel ra khỏi màn hình
        canvasGroup.DOFade(0, 0.35f); // Thực hiện animation làm mờ canvasGroup về giá trị 0
    }

    // Coroutine để thực hiện animation cho các items
    IEnumerator ItemsAnimation()
    {
        // Thiết lập kích thước ban đầu của các items là Vector3.zero (kích thước không)
        foreach (var item in items)
        {
            item.transform.localScale = Vector3.zero;
        }

        // Thực hiện animation mở rộng kích thước của từng item
        foreach (var item in items)
        {
            item.transform.DOScale(1f, fadeTime).SetEase(Ease.OutBounce);
            yield return new WaitForSeconds(0.1f); // Chờ 0.25 giây trước khi thực hiện animation cho item tiếp theo
        }
    }
}
