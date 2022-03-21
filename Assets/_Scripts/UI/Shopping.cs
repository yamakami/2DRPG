using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Shopping : CommandSelect
{
    [SerializeField] Text currentGoldText;
    [SerializeField] Text itemNameText;
    [SerializeField] Text priceText;
    [SerializeField] Text totalPriceText;
    [SerializeField] Button buySelectedButton;
    [SerializeField] Button sellSelectedButton;
    [SerializeField] Dropdown amount;
    [SerializeField] ShopTotalAmount shopTotalAmount;
    [SerializeField] string goldShortMessage = "お金が足りないみたいです!";
    [SerializeField] string posessionMaxMessage = "{0}はこれ以上持てません!";

    CommandItem selectedItem;
    int selectedPrice;
    bool isBuy;

    public void StartShoppingPlayerBuy(ICommand[] icommands)
    {
        isBuy = true;
        commandList = icommands;
        buySelectedButton.gameObject.SetActive(true);
        sellSelectedButton.gameObject.SetActive(false);
        Open();
    }

    public void StartShoppingPlayerSell()
    {
        isBuy = false;
        buySelectedButton.gameObject.SetActive(false);
        sellSelectedButton.gameObject.SetActive(true);
        // this.commandItems = playerInfo.items.FindAll( i => i.useForQuest ).ToArray();
        // CreateButton();
    }

   protected override void AddDescriptionEvents(ICommand command, SelectButton button)
    {
        var trigger = button.EventTrigger;

        DescriptionMessageAction(trigger, EventTriggerType.PointerEnter, command);
    }

    void DescriptionMessageAction(EventTrigger trigger, EventTriggerType triggerType, ICommand icomand)
    {
        var entry = new EventTrigger.Entry();
        entry.eventID = triggerType;
        entry.callback.AddListener((data) => RegisterHoverAction(icomand));
        trigger.triggers.Add(entry);
    }

    void RegisterHoverAction(ICommand icomand)
    {
        descriptionText.text = icomand?.GetDescription();
        selectedItem = icomand as CommandItem; 
        selectedPrice = (isBuy)? selectedItem.price: selectedItem.sellPrice;

        itemNameText.text = selectedItem.GetNameKana();
        priceText.text = selectedPrice.ToString();
        totalPriceText.text = (selectedPrice * int.Parse(amount.options[amount.value].text)).ToString();
    }

    public void OnAmountSelectChaged()
    {
        totalPriceText.text = (selectedPrice * int.Parse(amount.options[amount.value].text)).ToString();        
    }

    protected override void RemoveHoverEvent(SelectButton button)
    {
        button.EventTrigger.triggers.RemoveAt(1);
    }

    protected override void CreateButton()
    {
        var startIndex = PageSetting();
        PageNumDisplay();

        for(var i=0; i < pageButtonNum; i++)
        {
            var button = InitializeButton(i);

            if(commandList.Length <= startIndex) continue;

            var command = ActivateButton(startIndex, button);

            startIndex++;

            AddDescriptionEvents(command, button);
        }
    }

    public override void Open()
    {
        pageNum = 1;
        selectedItem = null;
        itemNameText.text = "";
        priceText.text = "0";
        totalPriceText.text = "0";
        amount.value = 0;
        currentGoldText.text = playerInfo.status.gold.ToString();
        CreateButton();
        Visible(true);
    }

    public void BuyAmountSelected()
    {
        if(!selectedItem) return;

        canvas.enabled = false;
        var selectedAmount =  int.Parse(amount.options[amount.value].text);
        var totalPrice = selectedItem.price * selectedAmount;

        if(totalPrice <= playerInfo.status.gold)
        {
            // if(selectedItem.player_possession_count <= selectedItem.player_possession_limit)
                shopTotalAmount.Visible(questManager, playerInfo, selectedItem, selectedAmount, totalPrice);
            // else

        }
    }

    public void SellAmountSelected()
    {
        var selectedAmount =  int.Parse(amount.options[amount.value].text);
        var totalPrice = selectedItem.sellPrice * selectedAmount;
    }
}
