using UnityEngine;
using UnityEngine.UI;

public class SelectShopType : UIBase
{
    [SerializeField] Button attackItem;
    [SerializeField] Button protectionItem;
    [SerializeField] Button item;

    ShopManager shopManager;
    public ShopManager ShopManager { set => shopManager = value; }

    void OnEnable()
    {
        if (shopManager.SkipSelectShopType)
        {
            shopManager.ItemSelect.Activate();
            Deactivate();
            return;
        }

        ButtonsActivationChange(true);
    }

    void ButtonsActivationChange(bool onOff)
    {
        var shopInfo = shopManager.ShopInfo;
        if (shopInfo.dealTypeWeapon) ButtonActivation(attackItem, onOff);
        if (shopInfo.dealTypeArmor) ButtonActivation(protectionItem, onOff);
        if (shopInfo.dealTypeItem) ButtonActivation(item, onOff);
    }

    void ButtonActivation(Button bt, bool onOff)
    {
        bt.gameObject.SetActive(onOff);
    }

    public void OnClickAttackItem()
    {
        shopManager.DealType = ShopManager.DEAL_TYPE.Weapon;
    }

    public void OnClickProtectionItem()
    {
        shopManager.DealType = ShopManager.DEAL_TYPE.Armor;
    }

    public void OnClickItem()
    {
        shopManager.DealType = ShopManager.DEAL_TYPE.Item;
    }

    public void OnClickBackFromItemSeelect()
    {
        if (shopManager.SkipSelectShopType)
        {
            shopManager.SelectBuyOrSell.Activate();
            return;
        }

        Activate();    
    }

    void OnDisable()
    {
        ButtonsActivationChange(false);
    }
}
