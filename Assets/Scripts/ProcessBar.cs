using UnityEngine;
using UnityEngine.UI;

public class ProcessBar : MonoBehaviour {
    public static ProcessBar instance;
    public Slider slider;

    private void Awake() {
        instance = this;
    }
    // Start is called before the first frame update
    void Start() {
        slider = GetComponent<Slider>();
        slider.value = 0;
        slider.maxValue = Level_Data.Instance.dGoalScore;
    }

    // Update is called once per frame
    void Update() {

    }
}
