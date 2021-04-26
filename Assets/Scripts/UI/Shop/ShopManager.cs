using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : UIBase
{
    [SerializeField] SelectBuyOrSell selectBuyOrSell;
    [SerializeField] SelectShopType selectShopType;
    [SerializeField] ItemSelect itemSelect;
    [SerializeField] ShopBuy shopBuy;
    [SerializeField] ShopSell shopSell;

    QuestUIManager questUIManager;
    PlayerMove playerMove;
    ShopInfo shopInfo;
    bool inShopping;
    int itemTotalPrice;
    Item selectedItem;
    int selectedItemNum;

    public ShopInfo ShopInfo { get => shopInfo; set => shopInfo = value; }
    public bool InShopping { get => inShopping; set => inShopping = value; }
    public SHOP_TYPE ShopType { get => shopType; set => shopType = value; }
    public DEAL_TYPE DealType { get => dealType; set => dealType = value; }
    public QuestUIManager QuestUIManager { get => questUIManager; }
    public SelectShopType SelectShopType { get => selectShopType; }
    public ItemSelect ItemSelect { get => itemSelect; }
    public SelectBuyOrSell SelectBuyOrSell { get => selectBuyOrSell; }
    public PlayerMove PlayerMove { get => playerMove; set => playerMove = value; }
    public ShopBuy ShopBuy { get => shopBuy; }
    public ShopSell ShopSell { get => shopSell; }
    public Item SelectedItem { get => selectedItem; }
    public int SelectedItemNum { get => selectedItemNum; }
    public int ItemTotalPrice { get => itemTotalPrice; }

    SHOP_TYPE shopType;
    public enum SHOP_TYPE
    {
        None,
        Buy,
        Sell
    }

    DEAL_TYPE dealType;
    public enum DEAL_TYPE
    {
        Weapon,
        Armor,
        Item
    }

    void LateUpdate()
    {
        if (InShopping && !playerMove.playerInfo.freeze) PlayerAndTargetCharFreeze();
    }

    void OnEnable()
    {
        questUIManager = PlayerMove.QuestUIManager;
        SelectBuyOrSell.ShopManager = this;
        SelectShopType.ShopManager = this;
        ItemSelect.ShopManager = this;

        selectBuyOrSell.Activate();
    }

    public void PlayerAndTargetCharFreeze()
    {
        var playerInfo = playerMove.playerInfo;
        playerInfo.freeze = true;
        PlayerMove.ContactWith.freeze = true;
    }

    public void ShopInnStart(MovePoint movePoint, ShopInfo shopInfo)
    {
        movePoint.PlayerMove = PlayerMove;

        var questUIManager = PlayerMove.QuestUIManager;
        var messgeBox = questUIManager.MessageBox;
        var playerInfo = PlayerMove.playerInfo;
        var price = shopInfo.innPrice;

        if(playerInfo.status.gold < price)
        {
            messgeBox.PrepareConversation(shopInfo.innNGShort);
            return;
        }

        playerInfo.status.gold -= price;
        playerInfo.status.hp = playerInfo.status.maxHP;
        playerInfo.status.mp = playerInfo.status.maxMP;

        questUIManager.Fader.FadeOutFadeIn(movePoint);
    }

    public void ShopStart(ShopInfo shopInfo)
    {
        Activate();

        InShopping = true;
        this.shopInfo = shopInfo;

        PlayerAndTargetCharFreeze();

        SelectBuyOrSell.Activate();
    }

    public List<Item> ShopItemList()
    {
        var playerInfo = playerMove.playerInfo;
        if (shopType == SHOP_TYPE.Sell) return playerInfo.items;

        if (dealType == DEAL_TYPE.Weapon) return shopInfo.weaponList;
        if (dealType == DEAL_TYPE.Armor) return shopInfo.armorList;
        return shopInfo.itemList;
    }

    public bool SelectShopTypeSkipable()
    {
        if (shopInfo.dealTypeInn) return true;

        var typeCount = 0;

        typeCount += DealTypeChecked(shopInfo.dealTypeItem, DEAL_TYPE.Item);
        typeCount += DealTypeChecked(shopInfo.dealTypeWeapon, DEAL_TYPE.Weapon);
        typeCount += DealTypeChecked(shopInfo.dealTypeArmor, DEAL_TYPE.Armor);

        if (1 < typeCount) return false;        

        return true;
    }

    int DealTypeChecked(bool dealTypeItem, DEAL_TYPE dealType)
    {
        if (dealTypeItem)
        {
            this.dealType = dealType;
            return 1;
        }

        return 0;
    }

    public void ShopConfirm(Item item, int amount)
    {
        selectedItem = item;
        selectedItemNum = amount;

        var messgeBox = questUIManager.MessageBox;
        messgeBox.SortOrderFront();

        var conversation = ConfirmMessage();        

        var price = (ShopType == SHOP_TYPE.Sell) ? item.sellPrice : item.price;
        itemTotalPrice = Calculation(amount, price);

        var stBuilder = questUIManager.StringBuilder;
        stBuilder.Clear();
        conversation.conversationLines[0].text = stBuilder.AppendFormat(
            conversation.dynamicText, item.nameKana, amount, itemTotalPrice).ToString();

        messgeBox.PrepareConversation(conversation);
    }

   ConversationData ConfirmMessage()
   {
        if (shopType == SHOP_TYPE.Buy) return shopInfo.buyConfirm;
        return shopInfo.sellConfirm;
   }

    int Calculation( int amount, int price)
    {
        return amount * price;
    }

    public void ItemSelectCancel()
    {
        PlayerAndTargetCharFreeze();
        questUIManager.MessageBox.Deactivate();
        ItemSelect.GraphicRaycaster.enabled = true;
    }

    public void OnQuitClick()
    {
        Deactivate();
        SelectBuyOrSell.Deactivate();
        SelectShopType.Deactivate();
        ItemSelect.Deactivate();
    }

    void OnDisable()
    {
        var playerInfo = playerMove.playerInfo;
        ShopType = SHOP_TYPE.None;
        inShopping = false;
        shopInfo = null;
        playerInfo.freeze = false;
        PlayerMove.ContactWith.freeze = false;
    }
}
