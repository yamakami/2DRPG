using UnityEngine;

public class ShopClerk : QuestEventListener
{
    public enum SHOP_TYPE
    {
        SELL_AND_BUY,
        SELL_ONLY,
        BUY_ONLY,        
    }
    [SerializeField] SHOP_TYPE shopType;
    [SerializeField] CommandItem[] commandItems;
 
    [SerializeField] string moneyNotEnoughMessage = "お金が不足しているみたいです";
    [SerializeField] string itemNotEnoughMessage = "数が足りないみたいです";
    [SerializeField] string needUnEquipMessage = "装備を解除する必要があります";

    [SerializeField] string posessionMaxMessage = "{0}はこれ以上持てません";
    [SerializeField] string closeMessage = "また来てくださいね";
 
    public SHOP_TYPE ShopType { get => shopType; }
    public string MoneyNotEnoughMessage { get => moneyNotEnoughMessage; }
    public string ItemNotEnoughMessage { get => itemNotEnoughMessage; }
    public string NeedUnEquipMessage { get => needUnEquipMessage; }
    public string PosessionMaxMessage { get => posessionMaxMessage; }
    public string CloseMessage { get => closeMessage; }
    public CommandItem[] CommandItems { get => commandItems; }


    Shopping shoppingUI;

    void Start()
    {
        shoppingUI = QuestManager.GetQuestManager().QuestUI.Shopping;
    }

    public override void OnEventRaised()
    {
        shoppingUI.CurrentClerk = this;

        if (shopType == SHOP_TYPE.SELL_AND_BUY)
        {
            shoppingUI.ShopMenuCanvas.enabled = true;
            shoppingUI.ActivateMenuBackButton(true);
            return;
        }

        shoppingUI.ActivateMenuBackButton(false);
        if (shopType == SHOP_TYPE.SELL_ONLY)
            shoppingUI.StartSellShop();
        else
            shoppingUI.StartBuyShop();
    }
}
