using UnityEngine;
public class GameManager : MonoBehaviour {
    bool isPlay = false;

    public AudioClip bgAudio;
    public AudioClip closePopup;
    public AudioClip openPopup;
    public AudioClip threeCandy;
    public AudioClip win;
    public AudioClip lose;
    public AudioClip bom;
    public AudioClip fourCandy;
    public AudioClip candyFall;
    public AudioClip coins;

    private AudioSource audioSource;
    public ToggleButton toggleButton;

    public static GameManager instance { get; private set; }
    private void Awake() {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;

    }

    void Start() {
        audioSource = GetComponent<AudioSource>();
        //audioSource.clip = bgAudio; // Đặt âm thanh nền
        //AudioBackground();
    }

    private void Update() {
        //if (!isPlay)
        //{
        //    isPlay = true;
        //    if (toggleButton != null)
        //    {
        //        Debug.Log("Trạng thái nút bật/tắt: " + toggleButton.isBtnOn);
        //        if (toggleButton.isBtnOn) audioSource.Play(); // Phát âm thanh
        //        else audioSource.Stop();
        //    }
        //    else Debug.LogError("Không tìm thấy nút bật/tắt trong scene.");
        //}
    }


    public void PlayBackGroundMusic() {
        audioSource.Play();
    }

    public void StopBackGroundMusic() {
        audioSource.Stop();
    }

    public void AudioClosePopup() {
        audioSource.PlayOneShot(closePopup);
    }
    public void AudioOpenPopup() {
        audioSource.PlayOneShot(openPopup);
    }
    public void AudioThreeCandy() {
        audioSource.PlayOneShot(threeCandy);
    }
    public void AudioWin() {
        audioSource.PlayOneShot(win);
    }
    public void AudioLose() {
        audioSource.PlayOneShot(lose);
    }
    public void AudioBom() {
        audioSource.PlayOneShot(bom);
    }
    public void AudioFourCandy() {
        audioSource.PlayOneShot(fourCandy);
    }
    public void AudioCandyFall() {
        audioSource.PlayOneShot(candyFall);
    }
    public void AudioCoins() {
        audioSource.PlayOneShot(coins);
    }


}
