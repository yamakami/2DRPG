using UnityEngine;
using UnityEngine.UIElements;
using System;

public class ShopDeal : MonoBehaviour
{
    [SerializeField] ShopErrorMessage shopErrorMessage;
    [SerializeField] AudioClip shopSuccessSound;
    Shop shop;
    VisualElement shopDealScreen;
    Item selectedItem;
    Label dealTitle;
    Label itemName;
    Label price;
    DropdownField itemAmount;
    Button dealButton;
    Button backButton;

    public Item SelectedItem { get => selectedItem; set => selectedItem = value; }
    public ShopErrorMessage ShopErrorMessage { get => shopErrorMessage; }

    void SetUP()
    {
        var rootUI = QuestManager.GetQuestManager().QuestUI.UiDocument.rootVisualElement;
        shopDealScreen = rootUI.Q<VisualElement>("shop-deal-screen");

        dealTitle = shopDealScreen.Q<Label>("title");
        itemName = shopDealScreen.Q<Label>("item");
        price = shopDealScreen.Q<Label>("price");
        itemAmount = shopDealScreen.Q<DropdownField>("amount");
        itemAmount.RegisterValueChangedCallback((evt) => ItemAmountChangeValue());

        dealButton = shopDealScreen.Q<Button>("deal-button");
        BindButton(ClickButtonDeal, dealButton);

        backButton = shopDealScreen.Q<Button>("back-button");
        BindButton(ClickButtonBack, backButton);
    }

    void ItemAmountChangeValue()
    {
        price.text = PriceMultipleAmount();
    }

    string PriceMultipleAmount()
    {
        var price = (shop.ShopType() == ShopTypeSelect.Type.Buy)? selectedItem.price : selectedItem.sellPrice;

        price =  price * Int32.Parse(itemAmount.value);
        return  $"価格:{price.ToString()}G";
    }

    void initializeText()
    {
        dealTitle.text = $"商品を購入";
        price.text = $"価格:{selectedItem.price.ToString()}G";
        dealButton.text = "買う";

        if(shop.ShopType() == ShopTypeSelect.Type.Sell)
        {
            dealTitle.text = $"商品を売却";
            price.text = $"価格:{selectedItem.sellPrice.ToString()}G";
            dealButton.text = "売る";
        }

        itemName.text = $"商品:{selectedItem.nameKana}";
        itemAmount.value = "1";        
    }

    void BindButton(System.Action action, Button bt)
    {
        var soundManager = SystemManager.SoundManager;

        bt.clicked += action;
        bt.RegisterCallback<ClickEvent>(ev => soundManager.PlayButtonClick());
        bt.RegisterCallback<MouseEnterEvent>(ev => soundManager.PlayButtonHover());
    }

    void ClickButtonDeal()
    {
        var playerData = QuestManager.GetQuestManager().Player.PlayerData;

        if(shop.ShopType() == ShopTypeSelect.Type.Buy)
            BuyCalculation(playerData);
        else
            SellCalculation(playerData);
    }

    void BuyCalculation(PlayerData playerData)
    {
        var playerGold = playerData.Gold;
        var amount = Int32.Parse(itemAmount.value);
        var price = selectedItem.price * amount;
        var possessionCount =  selectedItem.player_possession_count;
        var possessionLimit =  selectedItem.player_possession_limit;

        if(playerGold < price)
            ShopErrorMessage.Open( "所持金がたりません");
        else if(possessionLimit < possessionCount + amount)
            ShopErrorMessage.Open($"それ以上{selectedItem.nameKana}を所持できません");
        else
        {
            playerData.Gold -= price;
            playerData.AddItem(selectedItem.name, amount);
            SystemManager.SoundManager.SubAudio.PlayOneShot(shopSuccessSound);
            SuccessClose();
            return;
        }
        Close();
    }

    void SellCalculation(PlayerData playerData)
    {
        var playerGold = playerData.Gold;
        var amount = Int32.Parse(itemAmount.value);
        var price = selectedItem.sellPrice * amount;

        if(selectedItem.player_possession_count - amount < 0)
            ShopErrorMessage.Open($"{selectedItem.nameKana}は足りません");
        else
        {
            playerData.Gold += price;
            playerData.removeItem(selectedItem.name, amount);
            SystemManager.SoundManager.SubAudio.PlayOneShot(shopSuccessSound);

            if(playerData.Items.Count < 1) 
            {
                Close();
                shop.Close();
                return;
            }

            SuccessClose();
        }
        Close();
    }

    public void Open(Shop _shop)
    {
        shop = _shop;
        if(shopDealScreen == null) SetUP();

        initializeText();

        DealOpen(true);
    }

    void Close()
    {
        DealOpen(false);
    }

    void SuccessClose()
    {
        Close();
        shop.Open();
    }

    void ClickButtonBack() => DealOpen(false);

    void DealOpen(bool open) => shopDealScreen.style.display = (open) ? DisplayStyle.Flex : DisplayStyle.None;
}
