// Kịch bản GameManager
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool isPlay=false;
    public AudioClip bgAudio;
    public AudioClip closePopup;
    public AudioClip openPopup;
    private AudioSource audioSource;
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

    public void AudioClosePopup()
    {
        audioSource.PlayOneShot(closePopup);
    } 
    public void AudioOpenPopup()
    {
        audioSource.PlayOneShot(openPopup);

    }


}
