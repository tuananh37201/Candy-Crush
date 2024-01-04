using UnityEngine;
using TMPro;

public class ItemPriceManager : MonoBehaviour
{
    public static ItemPriceManager Instance;
    public TextMeshProUGUI myMoneyText;
    public int myMoney;
    public TextMeshProUGUI bombPriceText;
    public int bombPrice;
    public TextMeshProUGUI colorBombPriceText;
    public int colorBombPrice;

    private void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        bombPriceText.text = bombPrice.ToString();
        colorBombPriceText.text = colorBombPrice.ToString();
        myMoneyText.text = myMoney.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
