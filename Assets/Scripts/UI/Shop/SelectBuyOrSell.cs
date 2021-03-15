using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBuyOrSell : UIBase
{
    ShopManager shopManager;
    public ShopManager ShopManager { set => shopManager = value; }

    public void OnBuyClick()
    {
        shopManager.ShopType = ShopManager.SHOP_TYPE.Buy;
    }


    public void OnSellClick()
    {
        shopManager.ShopType = ShopManager.SHOP_TYPE.Sell;
    }
}
