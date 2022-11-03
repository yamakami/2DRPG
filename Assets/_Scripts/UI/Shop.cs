using UnityEngine.UIElements;
using UnityEngine;

public class Shop : MonoBehaviour, ICustomEventListener, ISelectButton
{
    [SerializeField] CustomEventTrigger shopTrigger;
    [SerializeField] ShopTypeSelect shopTypeSelect;

    NpcData npcData;
    ISelectButton iSelectButton;

    VisualElement shopScreen;
    Button[] selectButtons = new Button[6];

    public NpcData NpcData { get => npcData; set => npcData = value; }
    internal ISelectButton ISelectButton { get => iSelectButton; }
    Button[] ISelectButton.SelectButtons { get => selectButtons; set => selectButtons = value; }

    public void SetUP(VisualElement rootUI)
    {
        iSelectButton = gameObject.GetComponent("ISelectButton") as ISelectButton;

        shopScreen = rootUI.Q<VisualElement>("shop-screen");
        var itemSelectBox = shopScreen.Q<VisualElement>("item-select");
        iSelectButton.InitialButtons(itemSelectBox, "item-select-button");

        shopTypeSelect.SetUP(rootUI);
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

        if(shopTypeSelect.ShoptType == ShopTypeSelect.Type.Sell)
            items =  QuestManager.GetQuestManager().Player.PlayerData.Items.ToArray();
    
        for(var i=0; i < selectButtons.Length; i++)
        {
            iSelectButton.ShowButton(i, false);

            if(i < items.Length)
            {
                selectButtons[i].text = items[i].nameKana;
                iSelectButton.ClickAndHover(i);
                iSelectButton.ShowButton(i, true);
            }
        }
    }

    void ISelectButton.ClickAction(ClickEvent ev, int index){
        var item = npcData.ShopItems[index];
        iSelectButton.ClickSound();


    }

    void OnEnable() => shopTrigger.AddEvent(this);

    void OnDisable() => shopTrigger.RemoveEvent(this);
}
