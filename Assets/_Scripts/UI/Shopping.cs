using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Shopping : CommandSelect
{
    [SerializeField] Text currentGoldText;
    [SerializeField] Text itemNameText;
    [SerializeField] Text totalPriceText;
    [SerializeField] Button buySelectedButton;
    [SerializeField] Button sellSelectedButton;
    [SerializeField] Dropdown amount;
    [SerializeField] ConversationData conversationData;
    [SerializeField] QuestEventTrigger shopSellEventTrigger;
    [SerializeField] QuestEventTrigger shopBuyEventTrigger;
    [SerializeField] AudioClip shopSound;
    CommandItem selectedItem;
    int selectedPrice;
    bool isBuy;

    IShopMessage shopMessage;

    void ResetConversationData()
    {
        var conversation = conversationData.conversationLines[0];
        conversation.text = "";
        conversation.questEventTrigger = null;
        questManager.QuestUI.Conversation.Open(false);
    }

    public void StartShoppingPlayerBuy(ICommand[] icommands, IShopMessage shopMessage)
    {
        ResetConversationData();
        isBuy = true;
        commandList = icommands;
        buySelectedButton.gameObject.SetActive(true);
        sellSelectedButton.gameObject.SetActive(false);
        this.shopMessage = shopMessage;
        Open();
    }

    public void StartShoppingPlayerSell(IShopMessage shopMessage)
    {
        ResetConversationData();
        isBuy = false;
        buySelectedButton.gameObject.SetActive(false);
        sellSelectedButton.gameObject.SetActive(true);
        this.shopMessage = shopMessage;
        commandList = playerInfo.items.ToArray();

        Open();
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

            button.Button.onClick.AddListener(() => ClickAction(command));
        }
    }

    protected override void ClickAction(ICommand icomand)
    {
        selectedItem = icomand as CommandItem; 
        descriptionText.text = icomand?.GetDescription();
        itemNameText.text = selectedItem.GetNameKana();
 
        selectedPrice = (isBuy)? selectedItem.price: selectedItem.sellPrice;
        totalPriceText.text = CaluculateTotalText(selectedPrice, amount.options[amount.value].text).ToString();
    }

    public override void Open()
    {
        pageNum = 1;
        selectedItem = null;
        selectedPrice = 0;
        itemNameText.text = "";
        amount.value = 0;
        totalPriceText.text = "0";
        currentGoldText.text = playerInfo.status.gold.ToString();
        CreateButton();
        Visible(true);
    }

    public void BuyAmountSelected()
    {
        if(!selectedItem) return;

        var errorMessage = shopMessage.ItemNotEnoughMessage;
        var totalPrice = CaluculateTotalText(selectedItem.price, amount.options[amount.value].text);

        if(totalPrice <= playerInfo.status.gold)
        {
            if(selectedItem.player_possession_count <= selectedItem.player_possession_limit)
            {
                Buy(totalPrice);
                Open();
                return;
            }
            else
            {
                var stringBuilder = CommandUtils.GetStringBuilder();
                errorMessage = stringBuilder.AppendFormat(shopMessage.PosessionMaxMessage, selectedItem.GetNameKana()).ToString();
            }
        }
        canvas.enabled = false;
        SetConversation(errorMessage, shopSellEventTrigger);
    }

    void Buy(int totalPrice)
    {
        selectedItem.player_possession_count++;

        var playerItems = playerInfo.items;
        if(!playerItems.Find( i => i == selectedItem)) playerItems.Add(selectedItem);

        playerInfo.status.gold -= totalPrice;
        questManager.QuestUI.SeAudioSource.PlayOneShot(shopSound);
    }

    // public void SellAmountSelected()
    // {
    //     var totalPrice = CaluculateTotalText(selectedItem.sellPrice, amount.options[amount.value].text);
    // }

    public void OnAmountSelectChaged()
    {
        totalPriceText.text = CaluculateTotalText(selectedPrice, amount.options[amount.value].text).ToString();
    }

    public void CloseMessage()
    {
        SetConversation(shopMessage.CloseMessage);
    }

    void SetConversation(string message, QuestEventTrigger eventTrigger = null)
    {
        var conversationBox = questManager.QuestUI.Conversation;
        conversationData.conversationLines[0].text = message;
        conversationData.conversationLines[0].questEventTrigger = eventTrigger;
        conversationBox.StartConversation(conversationData);
    }

    int CaluculateTotalText(int price, string amountText)
    {
        return (price * int.Parse(amountText));
    }
}
