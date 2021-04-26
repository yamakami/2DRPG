using UnityEngine;
using UnityEngine.UI;

public class SelectBuyOrSell : UIBase
{
    [SerializeField] Button sellButton;

    ShopManager shopManager;
    public ShopManager ShopManager { set => shopManager = value; }

    void OnEnable()
    {
        if (shopManager.PlayerMove.playerInfo.items.Count < 1)
            sellButton.gameObject.SetActive(false);
    }

    void OnDisable()
    {
        sellButton.gameObject.SetActive(true);
    }

    public void OnBuyClick()
    {
        shopManager.ShopType = ShopManager.SHOP_TYPE.Buy;
    }

    public void OnSellClick()
    {
        shopManager.ShopType = ShopManager.SHOP_TYPE.Sell;
    }
}
