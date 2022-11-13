using UnityEngine;
using UnityEngine.UIElements;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

public class Conversation : MonoBehaviour, IMessageBox
{
    [SerializeField] AudioClip talkSound;
    QuestManager questManager;
    MessageBox messageBox;
    ConversationData conversationData;
    ConversationData.Conversation lastConversation;
    Queue<ConversationData.Conversation> conversations = new Queue<ConversationData.Conversation>(10);

    public void StartConversation()
    {
        StartTalk().Forget();
    }

    public async UniTask StartTalk()
    {
        if(questManager == null) questManager = QuestManager.GetQuestManager();
        if(messageBox   == null) SetButtonEvent();

        var player = questManager.Player;
        var npcData = player.TalkToNpc.NpcData;

        questManager.QuestUI.Shop.NpcData = npcData;

        PrepareMessage(npcData.ConversationData);
        OpenMessageBox();

        await ForwardConversation(conversations.Dequeue());
    }

    void SetButtonEvent()
    {
        messageBox = questManager.QuestUI.MessageBox;
        messageBox.SetUP();

        var nextButton = messageBox.ConversationNextButton;
        nextButton.clicked += ClickNext;
        nextButton.RegisterCallback<MouseEnterEvent>( ev => messageBox.PlayButtonHover() );       
        messageBox.InterfaceParent = this;
    }

    public async void ClickNext ()
    {
        messageBox.PlayButtonClick();

        if(0 < conversations.Count)
        {
            await ForwardConversation(conversations.Dequeue());
        }
        else
        {
            if(lastConversation?.eventTrigger)
            {
                messageBox.Open(false);
                lastConversation.eventTrigger.Invoke();
                return;
             }

            CloseMessageBox();
        }
    }

    public void PrepareMessage(ConversationData _conversationData)
    {
        conversationData = _conversationData;
        conversations.Clear();

        foreach (var conversation in conversationData.conversations)
        {
            conversations.Enqueue(conversation);
        }
    }

    async UniTask ForwardConversation(ConversationData.Conversation conversation)
    {
            lastConversation = conversation;
            messageBox.NextConversationButton(false);

            await messageBox.DisplayText(conversation.text, talkSound);

            if(SelectOpen()) return;

            messageBox.NextConversationButton(true);
    }

    bool SelectOpen()
    {
        if(0 < conversations.Count || !conversationData.optionExists()) return false;

        messageBox.OpenSelectBox(conversationData);

        return true;
    }

    void OpenMessageBox()
    {
        messageBox.Open(true);
    }

    void CloseMessageBox()
    {
        messageBox.Open(false);
        questManager.PlayerEnableMove();
    }
}
