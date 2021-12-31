using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections.Generic;

public class Conversation : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] MessageText messageText;
    [SerializeField] AudioSource audioSource;
    QuestManager questManager;
    ConversationData conversationData;
    Queue<ConversationData.Conversation> conversations = new Queue<ConversationData.Conversation>(5);
    public ConversationData ConversationData { get => conversationData; }

    GameObject forwardButton;

    void Start()
    {
        this.GetCancellationTokenOnDestroy();
        questManager = QuestManager.GetQuestManager();
        forwardButton = messageText.TextForwardButton.gameObject;
    }

    public void StartConversation(ConversationData conversationData = null)
    {
        canvas.enabled = true;
        this.conversationData = conversationData;

        if(conversationData) PrepareConversation(); 
    }

    public void ClickNext()
    {
        if (conversations.Count == 0)
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

    void PrepareConversation()
    {
        conversations.Clear();

        foreach (var conversation in conversationData.conversationLines)
        {
            conversations.Enqueue(conversation);
        }

        ForwardConversation(conversations.Dequeue());
    }

    void ForwardConversation(ConversationData.Conversation conversation)
    {
        TextForwardButton(false);
        var tweenText =  messageText.TweenText(conversation.text, audioSource);
        PlayTweenText(tweenText, this.GetCancellationTokenOnDestroy()).Forget();
    }

    async UniTaskVoid PlayTweenText(Tween tweenText, CancellationToken token)
    {
        await tweenText.Play().ToUniTask(cancellationToken: token);
        TextForwardButton(true);
    }

    void TextForwardButton(bool value)
    {
        forwardButton.SetActive(true);
    }

    public void EndConversation()
    {
        canvas.enabled = false;
        questManager.Player.EnableMove();
    }
}
