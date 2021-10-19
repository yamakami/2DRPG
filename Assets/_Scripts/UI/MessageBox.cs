using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.EventSystems;

public class MessageBox : UIBase
{
    [SerializeField] MessageText messageText;
    [SerializeField] Button nextButton;
    [SerializeField] MessageSelect selectMenu;

    QuestManager questManager;
    ConversationData conversationData;
    Queue<ConversationData.Conversation> conversations = new Queue<ConversationData.Conversation>();
    GameObject messageEventReceiver;

    public QuestManager QuestManager { set => questManager = value; }
    public Button NextButton { get => nextButton; }
    public GameObject MessageEventReceiver { get => messageEventReceiver; set => messageEventReceiver = value; }

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
        nextButton.gameObject.SetActive(false);

        messageText.TweenText(conversation.text);
        OpenSelectMenu(this.GetCancellationTokenOnDestroy()).Forget();
    }

    async UniTaskVoid OpenSelectMenu(CancellationToken token)
    {
        await UniTask.WaitUntil(() => messageText.Available, cancellationToken: token);

        if (conversations.Count == 0 && 0 < conversationData.subConverSationLines.Length)
        {
            selectMenu.OpenSelectMenu(this, conversationData.subConverSationLines);
            return;
        }
        nextButton.gameObject.SetActive(true);
    }

    public void NextMessage()
    {
        if (conversations.Count == 0)
        {
            var conversationLines = conversationData.conversationLines;
            var arrayLast = conversationLines.Length -1;
            if(conversationLines[arrayLast].eventTrigger)
            {
                conversationLines[arrayLast].eventTrigger.Invoke();
                return;
            }

            BoxClose();
            return;
        }

        ForwardConversation(conversations.Dequeue());
    }

    public void BoxClose()
    {
        Deactivate();
        questManager.Player.EnableMove();
    }
}