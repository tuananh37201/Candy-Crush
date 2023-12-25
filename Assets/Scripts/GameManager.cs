// Kịch bản GameManager
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioClip bgAudio;
    private AudioSource audioSource;
    bool isPlay=false;
    public ToggleButton toggleButton;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = bgAudio; // Đặt âm thanh nền
        //AudioBackground();
    }

    private void Update()
    {
        if (!isPlay)
        {
            isPlay=true;
            if (toggleButton != null)
            {
                Debug.Log("Trạng thái nút bật/tắt: " + toggleButton.isBtnOn);
                if (toggleButton.isBtnOn)
                {
                    audioSource.Play(); // Phát âm thanh
                }
                // Nếu nút đã tắt, dừng phát âm thanh
                else
                { 
                    audioSource.Stop();
                }
            }
            else
            {
                Debug.LogError("Không tìm thấy nút bật/tắt trong scene.");
            }
        }
    }

    //void AudioBackground()
    //{
    //    // Kiểm tra trạng thái của ToggleButton
    //    ToggleButton toggleButton = FindObjectOfType<ToggleButton>();

    //    if (toggleButton != null)
    //    {
    //        Debug.Log("Trạng thái nút bật/tắt: " + toggleButton.IsButtonOn());

    //        if (toggleButton.IsButtonOn())
    //        {
    //            audioSource.Play(); // Phát âm thanh
    //        }
    //        // Nếu nút đã tắt, dừng phát âm thanh
    //        else
    //        {
    //            audioSource.Stop(); // Dừng âm thanh
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogError("Không tìm thấy nút bật/tắt trong scene.");
    //    }
    //}
}
