using UnityEngine;
using UnityEngine.UIElements;

public class ShopTypeSelect : MonoBehaviour
{
    VisualElement shoptypeSelect;
    Button buyButton;
    Button sellButton;
    Shop shop;

    public enum Type {
        Buy,
        Sell,
    }

    Type shoptType;

    public Type ShopType { get => shoptType; }

    public void SetUP(VisualElement rootUI)
    {
        shop = QuestManager.GetQuestManager().QuestUI.Shop;

        shoptypeSelect = rootUI.Q<VisualElement>("shop-type-screen");
        buyButton  = shoptypeSelect.Q<Button>("buy-button");
        sellButton = shoptypeSelect.Q<Button>("sell-button");

        buyButton.clicked += ClickBuy;
        buyButton.RegisterCallback<ClickEvent>(ev => shop.ISelectButton.ClickSound());
        buyButton.RegisterCallback<MouseEnterEvent>(ev => shop.ISelectButton.HoverSound());

        sellButton.clicked += ClickSell;
        sellButton.RegisterCallback<ClickEvent>(ev => shop.ISelectButton.ClickSound());
        sellButton.RegisterCallback<MouseEnterEvent>(ev => shop.ISelectButton.HoverSound());
    }


    public void Open(bool open) => shoptypeSelect.style.display = (open) ? DisplayStyle.Flex : DisplayStyle.None;

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

    void ShopOpen()
    {
        Open(false);
        shop.Open();
    }
}
