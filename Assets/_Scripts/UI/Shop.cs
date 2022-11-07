using UnityEngine.UIElements;
using UnityEngine;

public class Shop : MonoBehaviour, ICustomEventListener, ISelectButton
{
    [SerializeField] CustomEventTrigger shopTrigger;
    [SerializeField] ShopTypeSelect shopTypeSelect;
    NpcData npcData;
    VisualElement shopScreen;
    public NpcData NpcData { get => npcData; set => npcData = value; }

    Button backButton;
    Button closeButton;
    Pagenation pagenation;

    // ISelectButton
    Button[] selectButtons = new Button[6];
    ISelectButton iSelectButton;
    internal ISelectButton ISelectButton { get => iSelectButton; }
    Button[] ISelectButton.SelectButtons { get => selectButtons; set => selectButtons = value; }

    public void SetUP(VisualElement rootUI)
    {
        iSelectButton = gameObject.GetComponent("ISelectButton") as ISelectButton;

        shopScreen = rootUI.Q<VisualElement>("shop-screen");
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

    void ISelectButton.BindButtonEvent()
    {
        var items = npcData.ShopItems;
        if(shopTypeSelect.ShopType == ShopTypeSelect.Type.Sell)
            items =  QuestManager.GetQuestManager().Player.PlayerData.Items.ToArray();
    
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
