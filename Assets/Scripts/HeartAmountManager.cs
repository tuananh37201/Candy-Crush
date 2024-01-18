using TMPro;
using UnityEngine;

public class HeartAmountManager : MonoBehaviour
{
    public static HeartAmountManager instance;
    public TextMeshProUGUI heartamountText;
    public int heartAmount;

    private void Awake()
    {
        instance = this;    
    }
    private void Start()
    {
        heartAmount = PlayerPrefs.GetInt("HeartAmount", 2);
    }
    private void Update()
    {
        heartamountText.text = heartAmount.ToString();
    }
}
