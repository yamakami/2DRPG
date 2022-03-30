using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections.Generic;

public class Conversation : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] MessageText messageText;
    [SerializeField] GameObject nextButton;
    [SerializeField] AudioSource audioSource;
    [SerializeField] ConversationSelect conversationSelect;

    QuestManager questManager;
    ConversationData conversationData;
    Queue<ConversationData.Conversation> conversations = new Queue<ConversationData.Conversation>(5);
    int delayTime = 350;

    public ConversationData ConversationData { get => conversationData; set => conversationData = value; }
    public MessageText MessageText { get => messageText; }
    public GameObject NextButton { get => nextButton; }
    public int DelayTime { get => delayTime; }

    void Start()
    {
        questManager = QuestManager.GetQuestManager();
    }

    public void StartConversation(ConversationData _conversationData)
    {
        Open(true);
        PrepareConversation(_conversationData); 
    }

    public void PrepareConversation(ConversationData _conversationData)
    {
        this.conversationData = _conversationData;
        conversations.Clear();

        foreach (var conversation in conversationData.conversationLines)
        {
            conversations.Enqueue(conversation);
        }

        ForwardConversation(conversations.Dequeue());
    }

    async void ForwardConversation(ConversationData.Conversation conversation)
    {
        var tween = messageText.TweenText(conversation.text, audioSource);
        await PlayMessage(tween, QuestManager.CancellationTokenSource.Token);
    }

    async UniTask PlayMessage(Tween tweenMessage, CancellationToken cancellationToken)
    {
        nextButton.SetActive(false);

        await UniTask.Delay(delayTime, cancellationToken: cancellationToken);

        await tweenMessage.Play().ToUniTask(cancellationToken: cancellationToken);

        await UniTask.Delay(delayTime, cancellationToken: cancellationToken);

        if (ConversationCount() == 0 && 0 < conversationData.options.Length)
        {
            conversationSelect.SelectOptions = conversationData.options;
            conversationSelect.Open();
            return;
        }

        nextButton.SetActive(true);
    }

    public void ClickNext()
    {
        if (conversationData && ConversationCount() == 0)
        {
            var conversationLines = conversationData.conversationLines;
            var arrayLast = conversationLines.Length -1;
            if(conversationLines[arrayLast]?.questEventTrigger)
            {
                conversationLines[arrayLast].questEventTrigger.Invoke();
                nextButton.SetActive(false);
                return;
            }

            EndConversation();
            return;
        }
        if(0 < conversations.Count)
            ForwardConversation(conversations.Dequeue());
        else
            EndConversation();
    }

    public void EndConversation()
    {
        Open(false);
        questManager.Player.EnableMove();
    }

    public void Open(bool val)
    {
        canvas.enabled = val;
        questManager.QuestUI.ControlPanelOn(!val);
    }

    public int ConversationCount()
    {
        return conversations.Count;
    }
}
