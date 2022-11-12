using UnityEngine.UIElements;
using UnityEngine;

public class Shop : MonoBehaviour, ICustomEventListener, ISelectButton
{
    [SerializeField] CustomEventTrigger shopTrigger;
    [SerializeField] ShopTypeSelect shopTypeSelect;
    NpcData npcData;
    VisualElement shopScreen;
    public NpcData NpcData { get => npcData; set => npcData = value; }

    string menuText = "道具屋({0})";
    string playerGoldText = "所持金；{0}G";
    string itemPriceText  = "価格；{0}G";
    string itemNameText   = "名前；{0}";

    Label menuTitle;
    Label playerGold;
    Label itemPrice;
    Label itemName;
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

    public void SetUP(VisualElement rootUI)
    {
        iSelectButton = gameObject.GetComponent("ISelectButton") as ISelectButton;

        shopScreen = rootUI.Q<VisualElement>("shop-screen");

        menuTitle = shopScreen.Q<Label>("menu-title");
        playerGold = shopScreen.Q<Label>("player-gold");
        itemPrice = shopScreen.Q<Label>("item-price");
        itemName  = shopScreen.Q<Label>("item-name");
        itemDetail = shopScreen.Q<Label>("item-detail");

        var itemSelectBox = shopScreen.Q<VisualElement>("item-select");
        iSelectButton.InitialButtons(itemSelectBox, "item-select-button");

        shopTypeSelect.SetUP(rootUI);

        pagenation = new Pagenation(selectButtons.Length, rootUI, iSelectButton);

        BindControllButtons(rootUI);
    }

    void ICustomEventListener.OnEventRaised() => ShopStart();

    void ShopStart() => shopTypeSelect.Open(true);

    void ShopScreenOpen(bool open) => shopScreen.style.display = (open) ? DisplayStyle.Flex : DisplayStyle.None;

    public void Open()
    {
        iSelectButton.BindButtonEvent();
        ShopScreenOpen(true);
    }

    void SetItems()
    {
        items = npcData.ShopItems;
        if(shopTypeSelect.ShopType == ShopTypeSelect.Type.Sell)
            items =  QuestManager.GetQuestManager().Player.PlayerData.Items.ToArray();
    }

    void SetMenuInfo()
    {
        var typeText = (shopTypeSelect.ShopType == ShopTypeSelect.Type.Buy)? "買": "売";
        var gold = QuestManager.GetQuestManager().Player.PlayerData.Gold.ToString();

        menuTitle.text  = string.Format(menuText, typeText);
        playerGold.text = string.Format(playerGoldText, gold);
        itemPrice.text = string.Format(itemPriceText, "0");
        itemName.text  = string.Format(itemNameText, "");
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

            selectButtons[buttonIndex].text = items[itemIndex].nameKana;
            iSelectButton.ClickAndHover(new int[]{buttonIndex, itemIndex});
            iSelectButton.ShowButton(buttonIndex, true);

            itemIndex++;
        }
    }

    void ISelectButton.ClickAction(ClickEvent ev, int itemIndex){
        var item = npcData.ShopItems[itemIndex];

    }

    void ISelectButton.HoverAction(MouseEnterEvent ev, int itemIndex)
    {
        var item = items[itemIndex];

        itemDetail.text = item.description;
        itemName.text = string.Format(itemNameText, item.nameKana);

        var price = item.price.ToString();
        itemPrice.text = string.Format(itemPriceText, price);
    }

    void BindControllButtons(VisualElement rootUI)
    {
        backButton = rootUI.Q<Button>("back-button");
        closeButton = rootUI.Q<Button>("close-button");

        backButton.clicked += ClicikBackButton;
        backButton.RegisterCallback<MouseEnterEvent>( ev => iSelectButton.HoverSound() );

        closeButton.clicked += ClickCloseButton;
        closeButton.RegisterCallback<MouseEnterEvent>( ev => iSelectButton.HoverSound() );
    }

    void ClicikBackButton()
    {
        iSelectButton.ClickSound();
        ShopScreenOpen(false);
        pagenation.Reset();
        shopTypeSelect.Open(true);
    }

    void ClickCloseButton()
    {
        iSelectButton.ClickSound();
        ShopScreenOpen(false);
        pagenation.Reset();
        QuestManager.GetQuestManager().PlayerEnableMove();
    }

    void OnEnable() => shopTrigger.AddEvent(this);

    void OnDisable() => shopTrigger.RemoveEvent(this);
}
