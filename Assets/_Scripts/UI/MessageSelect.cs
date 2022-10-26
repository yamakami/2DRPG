using UnityEngine.UIElements;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class MessageSelect : MonoBehaviour
{
    VisualElement selectBox;
    Button[] selectButtons = new Button[4];
    MessageBox messageBox;
 
    public void SetUp(VisualElement _rootUI, MessageBox _messageBox)
    {
        messageBox = _messageBox;
        selectBox = _rootUI.Q<VisualElement>("select-screen");

        selectBox.Query<Button>().ForEach(bt => {
            selectButtons[bt.tabIndex] = bt;
        });
    }

    void SetSelectMessage(ConversationData conversationData)
    {
        var options = conversationData.options;

        for(var i=0; i < selectButtons.Length; i++)
        {
            ShowButton(selectButtons[i], false);

            if(i < options.Length)
            {

                selectButtons[i].text = options[i].text;
                selectButtons[i].RegisterCallback<ClickEvent, ConversationData.Conversation>(ClickSelectButton, options[i]);
                selectButtons[i].RegisterCallback<MouseEnterEvent>( ev => messageBox.PlayButtonHover() );

                ShowButton(selectButtons[i], true);
            }
        }
    }

    void ShowButton(Button button, bool show)
    {
        button.style.display = (show)? DisplayStyle.Flex : DisplayStyle.None;
    }

    async void ClickSelectButton(ClickEvent ev, ConversationData.Conversation conversation)
    {
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

    public void Open(ConversationData conversationData)
    {
        BoxOpen(true);
        SetSelectMessage(conversationData);
    }

    void BoxOpen(bool open)
    {
        selectBox.style.display = (open)? DisplayStyle.Flex : DisplayStyle.None;
    }

    void BoxClose()
    {
        UnregisterCallback();
        BoxOpen(false);
    }

    void UnregisterCallback()
    {
        for(var i=0; i < selectButtons.Length; i++)
        {
            selectButtons[i].UnregisterCallback<ClickEvent, ConversationData.Conversation>(ClickSelectButton);
            selectButtons[i].UnregisterCallback<MouseEnterEvent>( ev => messageBox.PlayButtonHover() );
        }
    }
}
