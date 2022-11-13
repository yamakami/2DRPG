using UnityEngine.UIElements;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class MessageSelect : MonoBehaviour, ISelectButton
{
    VisualElement selectBox;
    Item[] items;
    Button[] selectButtons = new Button[4];
    MessageBox messageBox;
    ConversationData conversationData;
    ISelectButton iSelectButton;
    Button[] ISelectButton.SelectButtons { get => selectButtons; set => selectButtons = value; }

    public void SetUP()
    {
        var rootUI = QuestManager.GetQuestManager().QuestUI.UiDocument.rootVisualElement;
        messageBox = QuestManager.GetQuestManager().QuestUI.MessageBox;

        iSelectButton = gameObject.GetComponent("ISelectButton") as ISelectButton;

        selectBox = rootUI.Q<VisualElement>("select-screen");
        iSelectButton.InitialButtons(selectBox, "select-button");
    }

    void ISelectButton.ShowButton(int index, bool show) => iSelectButton.SelectButtons[index].style.display = (show) ? DisplayStyle.Flex : DisplayStyle.None;

    void ISelectButton.BindButtonEvent()
    {
        var options = conversationData.options;

        for(var i=0; i < selectButtons.Length; i++)
        {
            iSelectButton.ShowButton(i, false);

            if(i < options.Length)
            {
                selectButtons[i].text = options[i].text;
                iSelectButton.ClickAndHover(new int[]{i, i});
                iSelectButton.ShowButton(i, true);
            }
        }
    }

    async void ISelectButton.ClickAction(ClickEvent ev, int index)
    {
        var conversation = conversationData.options[index];

        messageBox.PlayButtonClick();

        BoxClose();

        if(conversation?.eventTrigger)
        {
            messageBox.Open(false);

            await UniTask.Delay(500, cancellationToken: SystemManager.UnitaskToken());
            conversation.eventTrigger.Invoke();
            return;
        }

        if(conversation?.nextConversationData)
            BackToMainMessage(conversation);
        else
            messageBox.Open(false);
    }

    void BackToMainMessage(ConversationData.Conversation conversation)
    {
        messageBox.InterfaceParent.PrepareMessage(conversation.nextConversationData);
        messageBox.InterfaceParent.ClickNext();
    }

    public void Open(ConversationData _conversationData)
    {
        if(messageBox == null) SetUP();

        conversationData = _conversationData;

        BoxOpen(true);
        iSelectButton.BindButtonEvent();
    }

    void BoxOpen(bool open) => selectBox.style.display = (open) ? DisplayStyle.Flex : DisplayStyle.None;

    void BoxClose()
    {
        iSelectButton.UnregisterCallback();
        BoxOpen(false);
    }
}
