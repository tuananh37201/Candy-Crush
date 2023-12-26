using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    public Image imageOn;
    public Image imageOff;

    public bool isBtnOn = true;

    private void Start()
    {
        SetImagesState(isBtnOn);
    }

    public void ButtonClick()
    {
        isBtnOn = !isBtnOn;
        SetImagesState(isBtnOn);
        //Debug.Log("Button State: " + isBtnOn);
    }

    public bool IsButtonOn()
    {
        return isBtnOn;
    }

    private void SetImagesState(bool state)
    {
        imageOn.gameObject.SetActive(state);
        imageOff.gameObject.SetActive(!state);
    }
}
