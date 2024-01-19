using UnityEngine;
using TMPro;

public class ItemPriceManager : MonoBehaviour
{
    public static ItemPriceManager Instance;
    public TextMeshProUGUI myMoneyText;
    public int myMoney;
    public TextMeshProUGUI bombPriceText;
    public int bombPrice;
    public TextMeshProUGUI bombAmountText;
    public int bombAmount;
    public TextMeshProUGUI extraStepPriceText;
    public int extraStepPrice;
    public TextMeshProUGUI extraStepAmountText;
    public int extraStepAmount;
    public TextMeshProUGUI colorBombPriceText;
    public int colorBombPrice;
    public TextMeshProUGUI colorBombAmountText;
    public int colorBombAmount;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        myMoney = PlayerPrefs.GetInt("MyMoney", 0);
        bombAmount = PlayerPrefs.GetInt("BombAmount", 0);
        extraStepAmount = PlayerPrefs.GetInt("ExtraStepAmount", 0);
        colorBombAmount = PlayerPrefs.GetInt("ColorBombAmount", 0);
    }

    // Update is called once per frame
    void Update()
    {
        bombPriceText.text = bombPrice.ToString();
        bombAmountText.text = bombAmount.ToString();
        extraStepPriceText.text = extraStepPrice.ToString();
        extraStepAmountText.text = extraStepAmount.ToString();
        colorBombPriceText.text = colorBombPrice.ToString();
        colorBombAmountText.text = colorBombAmount.ToString();
        myMoneyText.text = myMoney.ToString();
    }
}
