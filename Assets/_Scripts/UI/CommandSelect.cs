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

    protected virtual void Start()
    {
        questManager = QuestManager.GetQuestManager();
        playerInfo =  GameManager.GetPlayerInfo();
        pageButtonNum = optionButtons.Length;
        commandList = new ICommand[playerInfo.itemsMax];
    }

    protected virtual ICommand[] GetCommandList()
    {
        return playerInfo.items.FindAll(i => i.useForQuest).ToArray();
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

        if(1 < button.EventTrigger.triggers.Count) RemoveHoverEvent(button);
        button.Button.onClick.RemoveAllListeners();
        button.EnableButton();
        button.gameObject.SetActive(false);

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

    protected void AddDescriptionEvents(ICommand command, SelectButton button)
    {
        var trigger = button.EventTrigger;

        DescriptionMessageAction(trigger, EventTriggerType.PointerEnter, command);
        DescriptionMessageAction(trigger, EventTriggerType.PointerExit);
    }

    protected virtual void DescriptionMessageAction(EventTrigger trigger, EventTriggerType triggerType, ICommand command=null)
    {
        var entry = new EventTrigger.Entry();
        entry.eventID = triggerType;
        entry.callback.AddListener((data) => { descriptionText.text = command?.GetDescription(); });
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
