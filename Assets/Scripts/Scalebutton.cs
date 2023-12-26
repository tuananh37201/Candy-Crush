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
        // H�m n�y s? g?i l?i ch�nh n� ?? t?o hi?u ?ng li�n t?c
        yourButton.transform.DOScale(scaleValue, scaleDuration)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() => ReverseScale());
    }

    void ReverseScale()
    {
        // ??o ng??c gi� tr? scale v� g?i l?i h�m hi?u ?ng
        yourButton.transform.DOScale(Vector3.one, scaleDuration)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() => StartScaleEffect());
    }
}
