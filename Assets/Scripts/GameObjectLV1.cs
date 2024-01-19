using UnityEngine;
using UnityEngine.SceneManagement;
public class GameObjectLV1 : MonoBehaviour
{
    public static GameObjectLV1 Instance;
    public GameObject shopPanel, closeTabButton, musicButton, soundButton, exitButton, losePanel, winPanel, fourBtSettingPanel;
    public GameObject bombShop, extraStepShop, colorBombshop, switchShop, lolipopBomShop;
    public bool isClickBuyRowBomb;
    public bool isClickBuyColorBomb;
    private int clickBuyRowBombCount = 0;
    private int clickBuyColorBombCount = 0;
    private int clickUseRowBomb = 0;
    private int clickUseColorBomb = 0;
    public bool isWinPanel;
    public bool isUseRowBomb = true;
    public bool isUseColorBomb = true;
    public bool isUseExtraStep = true;

    private void Awake()
    {
        Instance = this;
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void ShopPanelAppear()
    {
        shopPanel.SetActive(true);
        losePanel.SetActive(false);
        winPanel.SetActive(false);
    }

    public void ShopPanelDisappear()
    {
        shopPanel.SetActive(false);
    }

    public void FourBtDisappear()
    {
        //closeTabButton.SetActive(false);
        //musicButton.SetActive(false);
        //soundButton.SetActive(false);
        //exitButton.SetActive(false);
        fourBtSettingPanel.SetActive(false);
    }

    public void FourBtAppear()
    {
        //closeTabButton.SetActive(true);
        //musicButton.SetActive(true);
        //soundButton.SetActive(true);
        //exitButton.SetActive(true);
        fourBtSettingPanel.SetActive(true);
    }

    public void BombShopAppear()
    {
        bombShop.SetActive(true);
        losePanel.SetActive(false);
        winPanel.SetActive(false);
        FourBtDisappear();
    }

    public void BuyRowBomb()
    {
        clickBuyRowBombCount++;
        if (clickBuyRowBombCount >= 1 && ItemPriceManager.Instance.myMoney >= ItemPriceManager.Instance.bombPrice)
        {
            ItemPriceManager.Instance.myMoney -= ItemPriceManager.Instance.bombPrice;
            ItemPriceManager.Instance.bombAmount += 1;
            //isClickBuyRowBomb = true;
            //isClickBuyColorBomb = false;
        }
    }

    public void UseRowBomb()
    {
        if (isUseRowBomb)
        {
            clickUseRowBomb++;
            if (clickUseRowBomb >= 1 && ItemPriceManager.Instance.bombAmount >= 1)
            {
                isClickBuyRowBomb = true;
                isClickBuyColorBomb = false;
            }
        }
    }

    public void BuyExtraStep()
    {
        if (ItemPriceManager.Instance.myMoney >= ItemPriceManager.Instance.extraStepPrice)
        {
            ItemPriceManager.Instance.myMoney -= ItemPriceManager.Instance.extraStepPrice;
            ItemPriceManager.Instance.extraStepAmount += 1;
        }
    }

    public void UseExtraStep()
    {
        if (isUseExtraStep == true)
        {
            if (ItemPriceManager.Instance.extraStepAmount >= 1)
            {
                Level_Data.Instance.dMove += 1;
                EndGameManager.instance.counter.text = "" + Level_Data.Instance.dGoalScore;
                ItemPriceManager.Instance.extraStepAmount -= 1;
            }
        }
    }

    public void BuyColorBomb()
    {
        clickBuyColorBombCount++;
        if (clickBuyColorBombCount >= 1 && ItemPriceManager.Instance.myMoney >= ItemPriceManager.Instance.colorBombPrice)
        {
            ItemPriceManager.Instance.myMoney -= ItemPriceManager.Instance.colorBombPrice;
            ItemPriceManager.Instance.colorBombAmount += 1;
            //isClickBuyColorBomb = true;
            //isClickBuyRowBomb = false;
        }
    }

    public void UseColorBomb()
    {
        if (isUseColorBomb)
        {
            clickUseColorBomb++;
            if (clickUseColorBomb >= 1 && ItemPriceManager.Instance.colorBombAmount >= 1)
            {
                isClickBuyRowBomb = false;
                isClickBuyColorBomb = true;
            }
        }
    }

    public void BombShopDisappear()
    {
        bombShop.SetActive(false);
    }

    public void ExtraStepShopAppear()
    {
        extraStepShop.SetActive(true);
        colorBombshop.SetActive(false);
        losePanel.SetActive(false);
        winPanel.SetActive(false);
        FourBtDisappear();
    }

    public void ExtraStepShopDisppear()
    {
        extraStepShop.SetActive(false);
    }

    public void ColorBombshopAppear()
    {
        colorBombshop.SetActive(true);
        losePanel.SetActive(false);
        winPanel.SetActive(false);
        FourBtDisappear();
    }

    public void ColorBombshopDisappear()
    {
        colorBombshop.SetActive(false);
    }

    public void SwitchShopAppear()
    {
        switchShop.SetActive(true);
        losePanel.SetActive(false);
        winPanel.SetActive(false);
        FourBtDisappear();
    }

    public void SwitchShopDisappear()
    {
        switchShop.SetActive(false);
    }

    public void LolipopBomShopAppear()
    {
        lolipopBomShop.SetActive(true);
        losePanel.SetActive(false);
        winPanel.SetActive(false);
        FourBtDisappear();
    }

    public void LolipopBomShopDisappear()
    {
        lolipopBomShop.SetActive(false);
    }

    public void LosePanelAppear()
    {
        //shopPanel.SetActive(false);
        losePanel.SetActive(true);
        winPanel.SetActive(false);
        FourBtDisappear();
    }

    public void WinPanelAppear()
    {
        isWinPanel = true;
        if (isWinPanel)
        {
            winPanel.SetActive(true);
        }
        FourBtDisappear();
    }

    public void WinPanelDisappear()
    {
        winPanel.SetActive(false);
    }

    public void LosePaneDisappear()
    {
        losePanel.SetActive(false);
    }

    public void LoadLv1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ReLoadCurrentMap()
    {
        SceneManager.LoadScene("Level Test 1");
    }
}
