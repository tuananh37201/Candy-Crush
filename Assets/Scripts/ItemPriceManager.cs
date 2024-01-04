using UnityEngine;
using TMPro;

public class ItemPriceManager : MonoBehaviour
{
    public TextMeshProUGUI bombPriceText;
    public int bombPrice;
    public TextMeshProUGUI colorBombPriceText;
    public int colorBombPrice;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bombPriceText.text = bombPrice.ToString();
        
        colorBombPriceText.text = colorBombPrice.ToString();
    }
}
