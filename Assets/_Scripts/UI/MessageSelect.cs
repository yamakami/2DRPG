using UnityEngine;
using UnityEngine.UIElements;

public class MessageSelect : MonoBehaviour
{
    VisualElement box;
    Button[] selectButtons = new Button[4];
    MessageBox messageBox;

    public void Init(VisualElement rootEl)
    {
        box = rootEl.Q<VisualElement>("select-box");
        box.Query<Button>().ForEach(bt => {
            selectButtons[bt.tabIndex] = bt;
        });
    }

    public void Open(MessageBox _messageBox, ConversationData conversationData)
    {
        messageBox = _messageBox;
        BoxOpen();
        SetSelectMessage(conversationData);
    }

    void Close()
    {
        UnregisterCallback();
        BoxOpen(false);

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

                ShowButton(selectButtons[i]);
            }
        }
    }

    void ClickSelectButton(ClickEvent ev, ConversationData.Conversation conversation)
    {
        Close();

        if(conversation?.nextConversationData)
            BackToMainMessage(conversation);
        else
            messageBox.BoxClose();
    }

    void BackToMainMessage(ConversationData.Conversation conversation)
    {
        messageBox.PrepareConversation(conversation.nextConversationData);
        messageBox.ClickNext();
    }

    void BoxOpen(bool open = true)
    {
        box.style.display = (open)? DisplayStyle.Flex : DisplayStyle.None;
    }

    void ShowButton(Button button, bool show = true)
    {
        button.style.display = (show)? DisplayStyle.Flex : DisplayStyle.None;
    }

    void UnregisterCallback()
    {
        for(var i=0; i < selectButtons.Length; i++)
            selectButtons[i].UnregisterCallback<ClickEvent, ConversationData.Conversation>(ClickSelectButton);        
    }
}
