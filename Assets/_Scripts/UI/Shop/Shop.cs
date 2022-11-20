using UnityEngine.UIElements;
using UnityEngine;

public class Shop : MonoBehaviour, ICustomEventListener, ISelectButton
{
    [SerializeField] CustomEventTrigger shopTrigger;
    [SerializeField] ShopTypeSelect shopTypeSelect;
     [SerializeField] ShopDeal shopDeal;
    NpcData npcData;
    VisualElement shopScreen;
    public NpcData NpcData { get => npcData; set => npcData = value; }

    string menuText = "道具屋({0})";
    string playerGoldText = "所持金；{0}G";
    string itemPriceText = "価格；{0}G";
    string itemNameText = "名前；{0}";
    string possessionText = "所有数；{0}/{1}個";

    Label menuTitle;
    Label playerGold;
    Label itemPrice;
    Label itemName;
    Label possession;
    Label itemDetail;
    Button backButton;
    Button closeButton;
    Pagenation pagenation;

    // ISelectButton
    Item[] items = new Item[100];
    Button[] selectButtons = new Button[6];
    ISelectButton iSelectButton;
    internal ISelectButton ISelectButton { get => iSelectButton; }
    public Item[] Items { get => items; set => items = value; }
    Button[] ISelectButton.SelectButtons { get => selectButtons; set => selectButtons = value; }

    void SetUP()
    {
        var rootUI = QuestManager.GetQuestManager().QuestUI.UiDocument.rootVisualElement;

        iSelectButton = gameObject.GetComponent("ISelectButton") as ISelectButton;

        shopScreen = rootUI.Q<VisualElement>("shop-screen");

        menuTitle = shopScreen.Q<Label>("menu-title");
        playerGold = shopScreen.Q<Label>("player-gold");
        itemPrice = shopScreen.Q<Label>("item-price");
        itemName  = shopScreen.Q<Label>("item-name");
        possession = shopScreen.Q<Label>("possession");
        itemDetail = shopScreen.Q<Label>("item-detail");

        var itemSelectBox = shopScreen.Q<VisualElement>("item-select");
        iSelectButton.InitialButtons(itemSelectBox, "item-select-button");

        pagenation = new Pagenation(selectButtons.Length, rootUI, iSelectButton);

        BindControllButtons(rootUI);
    }

    void ICustomEventListener.OnEventRaised() => ShopStart();

    void ShopStart()
    {
        if(iSelectButton == null) SetUP();
        shopTypeSelect.Open(true);
    }

    void ShopScreenOpen(bool open) => shopScreen.style.display = (open) ? DisplayStyle.Flex : DisplayStyle.None;

    public void Open()
    {
        if(SellShopPlayerNoItem()) return; 

        pagenation.Reset();
        iSelectButton.BindButtonEvent();
        ShopScreenOpen(true);
    }

    public void Close()
    {
        ShopScreenOpen(false);
    }

    bool SellShopPlayerNoItem()
    {
        if(shopTypeSelect.ShopType == ShopTypeSelect.Type.Buy) return false;

        var itemCount = QuestManager.GetQuestManager().Player.PlayerData.Items.Count;
        if(itemCount < 1)
        {
            shopDeal.ShopErrorMessage.Open("売れるアイテムがありません");
            return true;
        }

        return false;
    }

    void SetItems()
    {
        items = npcData.ShopItems;
        if(shopTypeSelect.ShopType == ShopTypeSelect.Type.Sell)
            items = QuestManager.GetQuestManager().Player.PlayerData.Items.ToArray();
    }

    void SetMenuInfo()
    {
        var typeText = (shopTypeSelect.ShopType == ShopTypeSelect.Type.Buy)? "買": "売";
        var gold = QuestManager.GetQuestManager().Player.PlayerData.Gold.ToString();

        menuTitle.text  = string.Format(menuText, typeText);
        playerGold.text = string.Format(playerGoldText, gold);
        itemPrice.text = string.Format(itemPriceText, "0");
        itemName.text  = string.Format(itemNameText, "");

        possession.text  = string.Format(possessionText, 0, 0);
        itemDetail.text = "";
    }

    void ISelectButton.BindButtonEvent()
    {
        SetMenuInfo();
        SetItems();

        pagenation.SetMaxPageNumber(items.Length);
        pagenation.DisplayPagerBlockAndPositionText();

        var itemIndex =  pagenation.ItemIndex;

        for(var buttonIndex =0; buttonIndex < selectButtons.Length; buttonIndex++)
        {
            iSelectButton.ShowButton(buttonIndex, false);

            if(items.Length <= itemIndex) continue;

            selectButtons[buttonIndex].text = items[itemIndex].NameKana;
            iSelectButton.ClickAndHover(new int[]{buttonIndex, itemIndex});
            iSelectButton.ShowButton(buttonIndex, true);

            itemIndex++;
        }
    }

    void ISelectButton.ClickAction(ClickEvent ev, int itemIndex)
    {
        shopDeal.SelectedItem = GetItem(itemIndex);
        shopDeal.Open(this);
    }

    void ISelectButton.HoverAction(MouseEnterEvent ev, int itemIndex)
    {
        var item = GetItem(itemIndex);
        itemDetail.text = item.Description;
        itemName.text = string.Format(itemNameText, item.NameKana);
        possession.text  = string.Format(possessionText, item.Player_possession_count, item.Player_possession_limit);

        var price = (shopTypeSelect.ShopType == ShopTypeSelect.Type.Buy) ? item.Price.ToString():  item.SellPrice.ToString();
        itemPrice.text = string.Format(itemPriceText, price);
    }

    Item GetItem(int index)
    {
        var items = npcData.ShopItems;
        if(shopTypeSelect.ShopType == ShopTypeSelect.Type.Sell)
            items =  QuestManager.GetQuestManager().Player.PlayerData.Items.ToArray();

        return items[index];
    }

    void BindControllButtons(VisualElement rootUI)
    {
        backButton = rootUI.Q<Button>("back-button");
        closeButton = rootUI.Q<Button>("close-button");

        SetControlButtonEvent(ClicikBackButton, backButton);
        SetControlButtonEvent(ClickCloseButton, closeButton);
    }

    void SetControlButtonEvent(System.Action click, Button bt)
    {
        bt.clicked += click;
        bt.RegisterCallback<MouseEnterEvent>( ev => iSelectButton.HoverSound() );
    }

    void ClicikBackButton()
    {
        iSelectButton.ClickSound();
        ShopScreenOpen(false);
        shopTypeSelect.Open(true);
    }

    public void ClickCloseButton()
    {
        iSelectButton.ClickSound();
        shopTypeSelect.Open(false);
        ShopScreenOpen(false);
        QuestManager.GetQuestManager().PlayerEnableMove();
    }

    public ShopTypeSelect.Type ShopType() => shopTypeSelect.ShopType;

    void Start() => shopTrigger.AddEvent(this);

    void OnDisable() => shopTrigger.RemoveEvent(this);
}
