using UnityEngine;
using UnityEngine.UI;

public class Shopping : CommandSelect
{
    [SerializeField] Canvas shopMenuCanvas;
    [SerializeField] Text itemNameText;
    [SerializeField] Text ownItemNumText;
    [SerializeField] Text currentGoldText;
    [SerializeField] Text totalPriceText;
    [SerializeField] Button buySelectedButton;
    [SerializeField] Button sellSelectedButton;
    [SerializeField] GameObject backButton;

    [SerializeField] Dropdown amount;
    [SerializeField] ConversationData conversationData;
    [SerializeField] AudioClip shopSound;
    CommandItem selectedItem;
    int selectedPrice;
    bool isSell;
    ShopClerk currentClerk;

    public Canvas ShopMenuCanvas { get => shopMenuCanvas; }
    public ShopClerk CurrentClerk { get => currentClerk; set => currentClerk = value; }


    public void EndConversation()
    {
        questManager.QuestUI.Conversation.EndConversation();
    }

    void ResetConversationData()
    {
        var conversation = conversationData.conversationLines[0];
        conversation.text = "";
        conversation.questEventTrigger = null;

        var questUI = questManager.QuestUI;
        questUI.Conversation.Open(false);
        questUI.ControlPanelOn(false);
    }

    public void StartSellShop()
    {
        isSell = true;
        buySelectedButton.gameObject.SetActive(true);
        sellSelectedButton.gameObject.SetActive(false);
        commandList = currentClerk.CommandItems;
        CommonStartShop();
     }

    public void StartBuyShop()
    {
        isSell = false;
        buySelectedButton.gameObject.SetActive(false);
        sellSelectedButton.gameObject.SetActive(true);
        commandList = playerInfo.items.ToArray();
        CommonStartShop();
    }

    void CommonStartShop()
    {
        ResetConversationData();
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
        ownItemNumText.text = selectedItem.player_possession_count.ToString();
        selectedPrice = (isSell)? selectedItem.price: selectedItem.sellPrice;
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
        ownItemNumText.text = "0";
        currentGoldText.text = playerInfo.status.gold.ToString();
        CreateButton();
        Visible(true);
    }

    public void CheckToSell()
    {
        if(!selectedItem) return;

        var errorMessage = currentClerk.MoneyNotEnoughMessage;
        var amountText =  amount.options[amount.value].text;
        var totalPrice = CaluculateTotalText(selectedItem.price, amountText);

        if(totalPrice <= playerInfo.status.gold)
        {
            var selectedAmount = int.Parse(amountText);
            if(selectedItem.player_possession_count +  selectedAmount <= selectedItem.player_possession_limit)
            {
                Sell(totalPrice, selectedAmount);
                Open();
                return;
            }
            else
            {
                var stringBuilder = CommandUtils.GetStringBuilder();
                errorMessage = stringBuilder.AppendFormat(currentClerk.PosessionMaxMessage, selectedItem.GetNameKana()).ToString();
            }
        }
        canvas.enabled = false;
        SetConversation(errorMessage, currentClerk.EventTrigger);
    }

    void Sell(int totalPrice, int amount)
    {
        selectedItem.player_possession_count += amount;

        var playerItems = playerInfo.items;
        if(!playerItems.Find( i => i == selectedItem)) playerItems.Add(selectedItem);

        playerInfo.status.gold -= totalPrice;
        questManager.QuestUI.SeAudioSource.PlayOneShot(shopSound);
    }

    public void CheckToBuy()
    {
        if(!selectedItem) return;

        int selectAmount = int.Parse(amount.options[amount.value].text);

        if(selectedItem.player_possession_count <  selectAmount)
        {
            canvas.enabled = false;
            SetConversation(currentClerk.ItemNotEnoughMessage, currentClerk.EventTrigger);
            return;
        }
        Buy(selectedItem, selectAmount);
        Open();
    }

    void Buy(CommandItem item, int amount)
    {
        playerInfo.status.gold += item.sellPrice * amount;
        item.player_possession_count -= amount;

        if(item.player_possession_count < 1)
        {
            playerInfo.items.Remove(item);
            commandList = playerInfo.items.ToArray();
        }

        questManager.QuestUI.SeAudioSource.PlayOneShot(shopSound);
    }

    public void OnAmountSelectChaged()
    {
        totalPriceText.text = CaluculateTotalText(selectedPrice, amount.options[amount.value].text).ToString();
    }

    public void CloseMessage()
    {
        SetConversation(currentClerk.CloseMessage);
    }

    public void ActivateMenuBackButton(bool active)
    {
        backButton.SetActive(active);
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
