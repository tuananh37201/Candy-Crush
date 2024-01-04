using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonController : MonoBehaviour
{
    public Button button1;
    public GameObject button2;

    void Start()
    {
        // Đăng ký sự kiện cho Button 1
        button1.onClick.AddListener(OnButton1Click);
        
        // Ẩn Button 2 ban đầu (scale = 0)
        button2.transform.localScale = Vector3.zero;
    }

    public void OnButton1Click()
    {
        // Hiển thị Button 2 với hiệu ứng scale từ 0 lên 1 trong 1 giây
        button2.transform.DOScale(Vector3.one, 0.5f);
    }
    public  void OnButton3Click()
    {
        // Hiển thị Button 2 với hiệu ứng scale từ 1 về 0 trong 1 giây
        button2.transform.DOScale(Vector3.zero, 0.5f);
    }
}
