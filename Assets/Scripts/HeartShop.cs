using TMPro;
using UnityEngine;

public class HeartShop : MonoBehaviour
{
    public static HeartShop instance;
    public TextMeshProUGUI myMoneyText;
    public int moneyAmount;
    public int heartPrice;
    private void Awake()
    {
        instance = this;
    }

    public void BuyHeart()
    {
        if(moneyAmount >= heartPrice)
        {
            moneyAmount -= heartPrice;
            HeartAmountManager.instance.heartAmount += 1;
            PlayerPrefs.SetInt("HeartAmount", HeartAmountManager.instance.heartAmount += 1);
            PlayerPrefs.SetInt("MyMoney", moneyAmount -= heartPrice);
            PlayerPrefs.Save();
        }
    }
    void Update()
    {
        moneyAmount = PlayerPrefs.GetInt("MyMoney");
        myMoneyText.text = moneyAmount.ToString();
    }
}
