using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox : UIBase
{
    [SerializeField] MessageText messageText;
    [SerializeField] Button nextButton;
    [SerializeField] MessageSelect selectMenu;

    QuestManager questManager;
    ConversationData conversationData;
    Queue<ConversationData.Conversation> conversations = new Queue<ConversationData.Conversation>();

    public QuestManager QuestManager { set => questManager = value; }

    void OnEnable()
    {
        conversationData = questManager.Player.ContactWith.ConversationData;
        PrepareConversation(conversationData);
    }

    public void PrepareConversation(ConversationData _conversationData)
    {
        conversationData = _conversationData;
        conversations.Clear();

        foreach (var conversation in conversationData.conversationLines)
        {
            conversations.Enqueue(conversation);
        }

        ForwardConversation(conversations.Dequeue());
    }

    void ForwardConversation(ConversationData.Conversation conversation)
    {
        messageText.TweenText(conversation.text);
    }

    public void NextMessage()
    {
        if (!messageText.Available) return;

        if (conversations.Count == 0)
        {
            if (0 < conversationData.subConverSationLines.Length)
            {
                selectMenu.OpenSelectMenu(this, conversationData.subConverSationLines);
                return;
            }

            //var lastIndex = conversationData.conversationLines.Length - 1;
            //if (conversationData.conversationLines[lastIndex].eventExec)
            //{
            //    var eventNum = conversationData.conversationLines[lastIndex].eventNum;
            //    conversationData.conversatinEvents[eventNum].Invoke();
            //    base.Deactivate();
            //    return;
            //}


            BoxClose();
            return;
        }

        ForwardConversation(conversations.Dequeue());
    }

    void BoxClose()
    {
        Deactivate();
        questManager.Player.StopConversation();
    }
}