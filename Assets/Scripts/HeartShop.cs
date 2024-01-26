using System.Collections;
using TMPro;
using UnityEngine;

public class HeartShop : MonoBehaviour
{
    public static HeartShop instance;
    public TextMeshProUGUI myMoneyText;
    public int moneyAmount;
    public int heartPrice;
    public TextMeshProUGUI heartPriceText;
    public GameObject moneyNotice;
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
            PlayerPrefs.SetInt("HeartAmount", HeartAmountManager.instance.heartAmount);
            if(moneyAmount<0) moneyAmount = 0;
            PlayerPrefs.SetInt("MyMoney", moneyAmount);
            PlayerPrefs.Save();
        }
        else if(moneyAmount <= heartPrice) {
            moneyNotice.SetActive(true);
            StartCoroutine(DisappearNotice());
        }
    }

    private IEnumerator DisappearNotice() {
        yield return new WaitForSeconds(2f);
        moneyNotice.SetActive(false);
    }

    void Update()
    {
        moneyAmount = PlayerPrefs.GetInt("MyMoney");
        myMoneyText.text = moneyAmount.ToString();
        heartPriceText.text = "$ " + heartPrice.ToString();
    }
}
