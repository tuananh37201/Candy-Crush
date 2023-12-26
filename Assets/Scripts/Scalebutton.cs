using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ScaleButton : MonoBehaviour
{
    public Button yourButton;
    public float scaleDuration = 0.5f;
    public Vector2 scaleValue = new Vector2(1.2f, 0.8f);

    void Start()
    {
        // B?t ??u hi?u ?ng ngay t? ??u
        StartScaleEffect();
    }

    void StartScaleEffect()
    {
        // Hàm này s? g?i l?i chính nó ?? t?o hi?u ?ng liên t?c
        yourButton.transform.DOScale(scaleValue, scaleDuration)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() => ReverseScale());
    }

    void ReverseScale()
    {
        // ??o ng??c giá tr? scale và g?i l?i hàm hi?u ?ng
        yourButton.transform.DOScale(Vector3.one, scaleDuration)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() => StartScaleEffect());
    }
}
