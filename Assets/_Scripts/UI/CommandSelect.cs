using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CommandSelect : CommandPager
{
    [SerializeField] string titleString;
    [SerializeField] protected Canvas canvas;
    [SerializeField] Text titleText;
    [SerializeField] protected Text descriptionText;
    [SerializeField] protected SelectButton[] optionButtons;
    protected QuestManager questManager;
    protected PlayerInfo playerInfo;
    protected ICommand[] commandList;

    void Start()
    {
        questManager = QuestManager.GetQuestManager();
        playerInfo = questManager.Player.PlayerInfo;
        pageButtonNum = optionButtons.Length;
        commandList = new ICommand[playerInfo.itemsMax];
    }

    protected virtual ICommand[] GetCommandList()
    {
        return playerInfo.items.ConvertAll(c => c as ICommand).ToArray();
    }

    protected int PageSetting()
    {
        descriptionText.text = "";
        var startIndex = GetStartIndex();
        SetTotalPageNum(commandList.Length);
        return startIndex;
    }

    protected SelectButton InitializeButton(int index)
    {
        var button = optionButtons[index];
        button.Button.onClick.RemoveAllListeners();
        button.gameObject.SetActive(false);
        button.Button.interactable = true;

        if(1 < button.EventTrigger.triggers.Count)
           RemoveHoverEvent(button);

        button.Text.color = new Color32(255, 255, 255, 255);
        return button;
    }

    protected virtual void RemoveHoverEvent(SelectButton button)
    {
        button.EventTrigger.triggers.RemoveRange(1,2);
    }

    protected ICommand ActivateButton(int index, SelectButton button)
    {
        var command = commandList[index];
        button.gameObject.SetActive(true);
        button.Text.text = command.GetNameKana();
        return command;
    }

    protected virtual void AddDescriptionEvents(ICommand command, SelectButton button)
    {
        var trigger = button.EventTrigger;

        DescriptionMessageAction(trigger, EventTriggerType.PointerEnter, command.GetDescription());
        DescriptionMessageAction(trigger, EventTriggerType.PointerExit);
    }

    protected void DescriptionMessageAction(EventTrigger trigger, EventTriggerType triggerType, string str=null)
    {
        var entry = new EventTrigger.Entry();
        entry.eventID = triggerType;
        entry.callback.AddListener((data) => { descriptionText.text = str; });
        trigger.triggers.Add(entry);
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
            AddDescriptionEvents(command, button);
        }
    }

    protected virtual async void ClickAction(ICommand selected)
    {
        var questUI = questManager.QuestUI;
        Visible(false);
        questUI.MenuOn(false);

        var conversationBox = questUI.Conversation;

        conversationBox.NextButton.SetActive(false);
        conversationBox.Open(true);

        var token = QuestManager.CancellationTokenSource.Token;
        await CommandUtils.CommandExecute(selected, questUI.SeAudioSource, conversationBox, token, playerInfo, playerInfo);
        conversationBox.NextButton.SetActive(true);
    }

    public virtual void Open()
    {
        titleText.text = titleString;
        pageNum = 1;
        commandList =  GetCommandList();
        CreateButton();
        Visible(true);
    }

    protected void Visible(bool value)
    {
        canvas.enabled = value;
    }
}
