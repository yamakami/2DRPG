using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : UIBase
{
    [SerializeField] PlayerMove playerMove;
    [SerializeField] SelectBuyOrSell selectBuyOrSell;
    [SerializeField] SelectShopType selectShopType;
    [SerializeField] ItemSelect itemSelect;

    QuestUIManager questUIManager;
    PlayerInfo playerInfo;
    ShopInfo shopInfo;
    bool inShopping;
    bool skipSelectShopType;
    int itemTotalPrice;

    public ShopInfo ShopInfo { get => shopInfo; set => shopInfo = value; }
    public bool InShopping { get => inShopping; set => inShopping = value; }
    public bool SkipSelectShopType { get => skipSelectShopType; }
    public SHOP_TYPE ShopType { get => shopType; set => shopType = value; }
    public DEAL_TYPE DealType { get => dealType; set => dealType = value; }
    public QuestUIManager QuestUIManager { get => questUIManager; }
    public SelectShopType SelectShopType { get => selectShopType; }
    public ItemSelect ItemSelect { get => itemSelect; }
    public SelectBuyOrSell SelectBuyOrSell { get => selectBuyOrSell; }

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

    void OnEnable()
    {
        questUIManager = playerMove.QuestUIManager;
        playerInfo = playerMove.playerInfo;
        SelectBuyOrSell.ShopManager = this;
        SelectShopType.ShopManager = this;
        ItemSelect.ShopManager = this;
    }

    public void ShopStart(ShopInfo shopInfo)
    {
        Activate();

        InShopping = true;
        this.shopInfo = shopInfo;

        playerInfo.freeze = true;
        playerMove.ContactWith.freeze = true;

        if(shopInfo.dealTypeInn)
            Debug.Log("shop inn");
        else
            SelectBuyOrSell.Activate();

        skipSelectShopType = SelectShopTypeSkipable();
    }

    public List<Item> ShopItemList()
    {
        if (dealType == DEAL_TYPE.Weapon) return shopInfo.weaponList;
        if (dealType == DEAL_TYPE.Armor) return shopInfo.armorList;
        return shopInfo.itemList;
    }

    bool SelectShopTypeSkipable()
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

    public void BuyConfirm(Item item, int amount, int price)
    {
        var messgeBox = questUIManager.MessageBox;
        messgeBox.SortOrderFront();

        var conversation = shopInfo.buyConfirm;
        itemTotalPrice = BuyCalculation(amount, price);

        questUIManager.StringBuilder.Clear();
        conversation.conversationLines[0].text = questUIManager.StringBuilder.AppendFormat(
                                                    shopInfo.buyConfirm.dynamicText,
                                                    item.nameKana, amount, itemTotalPrice
                                                 ).ToString();

        messgeBox.PrepareConversation(shopInfo.buyConfirm);
    }

    int BuyCalculation( int amount, int price)
    {
        return amount * price;
    }

    void BuyNG()
    {
        var messgeBox = questUIManager.MessageBox;
        messgeBox.SortOrderFront();
        messgeBox.PrepareConversation(shopInfo.buyNG);
    }

    public void BuyOK()
    {
        Debug.Log("----------buy ok");

        var messgeBox = questUIManager.MessageBox;
        if (itemTotalPrice < playerInfo.status.gold)
        {
            Debug.Log("----------buy ok price ok");
            AddItemToPlayer();
            messgeBox.PrepareConversation(shopInfo.buyComplete);
        }
        else
        {
            Debug.Log("----------buy ok price short");
            messgeBox.PrepareConversation(shopInfo.buyNG);
        }
    }

    public void OnBuyCancel()
    {
        questUIManager.MessageBox.gameObject.SetActive(false);
        ItemSelect.GraphicRaycaster.enabled = true;
    }

    void AddItemToPlayer()
    {
        Debug.Log("----------item added to player");
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
        ShopType = SHOP_TYPE.None;
        inShopping = false;
        shopInfo = null;
        playerInfo.freeze = false;
        playerMove.ContactWith.freeze = false;
    }


    //[SerializeField] Canvas canvas;
    //[SerializeField] ShopStartSelect shopStartSelect;
    //[SerializeField] ShopDealSelect shopDealSelect;
    //[SerializeField] ItemSelect itemSelect;
    //[SerializeField] ShopConversation shopConversation;
    //[SerializeField] MessageBox messageBox;

    //PlayerInfo playerInfo;
    //ShopInfo shopInfo;
    //bool skipDealSelect;

    //public ShopInfo ShopInfo { get => shopInfo; set => shopInfo = value; }
    //public SHOP_TYPE ShopType { get => shopType; set => shopType = value; }
    //public ShopStartSelect ShopStartSelect { get => shopStartSelect; }
    //public ShopDealSelect ShopDealSelect { get => shopDealSelect; }
    //public bool SkipDealSelect { get => skipDealSelect; }
    //public ItemSelect ItemSelect { get => itemSelect; }
    //public PlayerMove PlayerMove { get => playerMove; }
    //public ShopConversation ShopConversation { get => shopConversation; }
    //public MessageBox MessageBox { get => messageBox; }

    //SHOP_TYPE shopType;
    //public enum SHOP_TYPE
    //{
    //    None,
    //    Buy,
    //    Sell
    //}

    //void Start()
    //{
    //    playerInfo = PlayerMove.playerInfo;
    //    playerInfo.ShopManager = this;
    //    ShopStartSelect.ShopManager = this;
    //    ShopDealSelect.ShopManager = this;
    //    ItemSelect.ShopManager = this;
    //    shopConversation.ShopManager = this;
    //}

    //void Update()
    //{
    //    if (!playerInfo.startShopping) return;
    //    if (canvas.isActiveAndEnabled) return;
    //    EnableCanvas();
    //}

    //void EnableCanvas()
    //{
    //    canvas.enabled = true;
    //    ShopStartSelect.Open();
    //    skipDealSelect = DealSelectSkipable();
    //}

    //bool DealSelectSkipable()
    //{
    //    if (shopInfo.shopInn) return true;

    //    var typeCount = 0;
    //    if (shopInfo.shopInn) typeCount++;
    //    if (shopInfo.shopWeapon) typeCount++;
    //    if (shopInfo.shopArmor) typeCount++;

    //    if (1 < typeCount) return false;

    //    return true;
    //}

    //void DisableCanvas()
    //{
    //    canvas.enabled = false;
    //    ShopStartSelect.Open();
    //    ShopDealSelect.Close();
    //}

    //public void OnQuitClick()
    //{
    //    ShopType = SHOP_TYPE.None;

    //    DisableCanvas();
    //    playerInfo.startShopping = false;
    //    shopInfo = null;
    //    playerInfo.freeze = false;
    //    playerInfo.ShopManager.PlayerMove.characterMove.freeze = false;
    //}

    //public void ItemSelectClickable()
    //{    
    //    MessageBox.SortOrderBack();
    //    ItemSelect.ItemSelectClickable();
    //}
}
