using UnityEngine;
using UnityEngine.UIElements;

public class ShopTypeSelect : MonoBehaviour
{
    VisualElement shoptypeSelect;
    Button buyButton;
    Button sellButton;
    Button quitButton;
    Shop shop;

    public enum Type {
        Buy,
        Sell,
    }

    Type shoptType;

    public Type ShopType { get => shoptType; }

    void SetUP()
    {
        var rootUI = QuestManager.GetQuestManager().QuestUI.UiDocument.rootVisualElement;
        shop = QuestManager.GetQuestManager().QuestUI.Shop;

        shoptypeSelect = rootUI.Q<VisualElement>("shop-type-screen");
        buyButton  = shoptypeSelect.Q<Button>("buy-button");
        sellButton = shoptypeSelect.Q<Button>("sell-button");
        quitButton = shoptypeSelect.Q<Button>("quit-button");

        SetButtonSound(ClickBuy, buyButton);
        SetButtonSound(ClickSell, sellButton);
        SetButtonSound(ClickQuit, quitButton);
    }

    void SetButtonSound(System.Action click, Button bt)
    {
        bt.clicked += click;
        bt.RegisterCallback<ClickEvent>(ev => shop.ISelectButton.ClickSound());
        bt.RegisterCallback<MouseEnterEvent>(ev => shop.ISelectButton.HoverSound());
    }

    public void Open(bool open)
    {
        if(shoptypeSelect == null) SetUP();
        shoptypeSelect.style.display = (open) ? DisplayStyle.Flex : DisplayStyle.None;
    }

    void ClickBuy()
    {
        shoptType = Type.Buy;
        ShopOpen();
    }

    void ClickSell()
    {
        shoptType = Type.Sell;
        ShopOpen();
    }

    void ClickQuit()
    {
        Open(false);
        shop.ClickCloseButton();
    }

    void ShopOpen()
    {
        // Open(false);
        shop.Open();
    }
}
