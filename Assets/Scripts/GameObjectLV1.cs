using UnityEngine;
using UnityEngine.SceneManagement;

public class GameObjectLV1 : MonoBehaviour
{
    public static GameObjectLV1 Instance;
    public GameObject shopPanel, closeTabButton, musicButton, soundButton,losePanel;
    public GameObject bombShop, colorBombshop, switchShop, lolipopBomShop;

    private void Awake() {
        Instance = this;
    }

    public void ShopPanelAppear() {
        shopPanel.SetActive(true);
        losePanel.SetActive(false);
    }

    public void ShopPanelDisappear() {
        shopPanel.SetActive(false);
    }

    public void ThreeBtDisappear() {
        closeTabButton.SetActive(false);
        musicButton.SetActive(false);   
        soundButton.SetActive(false);
    }

    public void ThreeBtAppear() {
        closeTabButton.SetActive(true);
        musicButton.SetActive(true);
        soundButton.SetActive(true);
    }

    public void BombShopAppear() {
        bombShop.SetActive(true);
        losePanel.SetActive(false);
    }

    public void BombShopDisappear() {
        bombShop.SetActive(false);
    }

    public void ColorBombshopAppear() {
        colorBombshop.SetActive(true);
        losePanel.SetActive(false);
    }

    public void ColorBombshopDisappear() {
        colorBombshop.SetActive(false);
    }

    public void SwitchShopAppear() {
        switchShop.SetActive(true);
        losePanel.SetActive(false);
    }

    public void SwitchShopDisappear() {
        switchShop.SetActive(false);
    }

    public void LolipopBomShopAppear() {
        lolipopBomShop.SetActive(true);
        losePanel.SetActive(false);
    }

    public void LolipopBomShopDisappear() {
        lolipopBomShop.SetActive(false);
    }

    public void LosePanelAppear() {
        //shopPanel.SetActive(false);
        losePanel.SetActive(true);
        ThreeBtDisappear();
    }

    public void LosePaneDisappear() {
        losePanel.SetActive(false);
    }

    public void LoadLv1() {
        SceneManager.LoadScene("Level1");
    }
    
    public void LoadMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
