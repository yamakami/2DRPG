using UnityEngine;
using UnityEngine.UI;

public class MessageSelect : UIBase
{
    [SerializeField] Image uiFrame;
    [SerializeField] Button prefTextButton;
    public void OpenSelectMenu(MessageBox messageBox, ConversationData.Conversation[] subconversationLines)
    {
        Activate();

        foreach (var conversation in subconversationLines)
        {
            var button = CreateButtonUnderPanel();
            var textfield = button.GetComponentInChildren<Text>();
            textfield.text = conversation.text;
            button.gameObject.SetActive(true);
            button.onClick.AddListener(() => ClickButtonAction(messageBox, conversation));
        }
    }

    Button CreateButtonUnderPanel()
    {
        var button = Instantiate(prefTextButton);
        button.transform.SetParent(uiFrame.transform);
        button.transform.localScale = Vector3.one;

        return button;
    }

    void ClickButtonAction(MessageBox messageBox, ConversationData.Conversation conversation)
    {
        if (conversation.conversationData)
            messageBox.PrepareConversation(conversation.conversationData);
        else
            messageBox.Deactivate();

        Deactivate();

        if(conversation.eventTrigger) conversation.eventTrigger.Invoke();
    }

    void OnDisable()
    {
        foreach (Transform child in uiFrame.gameObject.transform)
        {
            Destroy(child.gameObject);
        }
    }
}