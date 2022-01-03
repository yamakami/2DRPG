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
     CancellationToken cancellationToken;

    public ConversationData ConversationData { get => conversationData; set => conversationData = value; }

    void Start()
    {
        cancellationToken = new CancellationTokenSource().Token;
        questManager = QuestManager.GetQuestManager();
    }

    public void StartConversation(ConversationData _conversationData)
    {
        canvas.enabled = true;
        PrepareConversation(_conversationData); 
    }

    public void ClickNext()
    {
        if (ConversationCount() == 0)
        {
            // var conversationLines = conversationData.conversationLines;
            // var arrayLast = conversationLines.Length -1;
            // if(conversationLines[arrayLast].eventTrigger)
            // {
            //     conversationLines[arrayLast].eventTrigger.Invoke();
            //     return;
            // }

            EndConversation();
            return;
        }

        ForwardConversation(conversations.Dequeue());
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

    void ForwardConversation(ConversationData.Conversation conversation)
    {
        var tween = messageText.TweenText(conversation.text, audioSource);
        PlaySequence(tween).Forget();
    }

    async UniTask PlaySequence(Tween tweenMessage)
    {
        nextButton.SetActive(false);

        await UniTask.Delay(350, cancellationToken: cancellationToken);

        await tweenMessage.Play();

        await UniTask.Delay(500, cancellationToken: cancellationToken);

        if (ConversationCount() == 0 && 0 < conversationData.options.Length)
        {
            conversationSelect.Open(conversationData.options);
            return;
        }

        nextButton.SetActive(true);
    }

    public void EndConversation()
    {
        Close();
        questManager.Player.EnableMove();
    }

    public void Close()
    {
        canvas.enabled = false;
    }

    public int ConversationCount()
    {
        return conversations.Count;
    }
}
