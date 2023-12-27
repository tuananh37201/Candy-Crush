using UnityEngine;

public class GameObjectLV1 : MonoBehaviour
{
    public static GameObjectLV1 Instance;
    public GameObject shopPanel, closeTabButton, musicButton, soundButton;
    public GameObject bombShop, colorBombshop, switchShop, lolipopBomShop;
    private void Awake() {
        if(Instance == null) {
            Instance = this;
        }
    }

    public void ShopPanelAppear() {
        shopPanel.SetActive(true);
    }

    public void ShopPanelDisappear() {
        shopPanel.SetActive(false);
    }

    public void ThreeBtDisappear() {
        closeTabButton.SetActive(false);
        musicButton.SetActive(false);   
        soundButton.SetActive(false);
    }

    public void BombShopAppear() {
        bombShop.SetActive(true);
    }

    public void BombShopDisappear() {
        bombShop.SetActive(false);
    }

    public void ColorBombshopAppear() {
        colorBombshop.SetActive(true);
    }

    public void ColorBombshopDisappear() {
        colorBombshop.SetActive(false);
    }

    public void SwitchShopAppear() {
        switchShop.SetActive(true);
    }

    public void SwitchShopDisappear() {
        switchShop.SetActive(false);
    }

    public void LolipopBomShopAppear() {
        lolipopBomShop.SetActive(true); 
    }

    public void LolipopBomShopDisappear() {
        lolipopBomShop.SetActive(false);
    }
}
