using UnityEngine;

public class SoundManager : MonoBehaviour {
    public GameObject btMusicOn, btMusicOff;
    public AudioSource audioSource;
    public AudioClip bgMusic;
    public AudioClip winSound;
    public AudioClip loseSound;
    // Start is called before the first frame update
    void Start() {
        audioSource.PlayOneShot(bgMusic);
    }

    // Update is called once per frame
    void Update() {

    }

    public void BtMusicOnAppear() {
        audioSource.PlayOneShot(bgMusic);
        btMusicOn.SetActive(true);
        btMusicOff.SetActive(false);
    }

    public void BtMusicOnDisappear() {
        btMusicOn.SetActive(false);
        btMusicOff.SetActive(true);
        audioSource.Stop();
    }
}
