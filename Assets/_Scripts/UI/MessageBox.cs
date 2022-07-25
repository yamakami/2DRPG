using UnityEngine;
using UnityEngine.UIElements;
using System.Threading;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;

public class MessageBox : MonoBehaviour
{
    VisualElement box;
    Label messageText;
    Button messageNexButton;
    CancellationTokenSource tokenSource;
    ConversationData conversationData;
    Queue<ConversationData.Conversation> conversations = new Queue<ConversationData.Conversation>(10);

    public void Init(VisualElement ve)
    {
        box = ve.Q<VisualElement>("message-box");
        messageText = ve.Q<Label>("message-text");
        messageNexButton = ve.Q<Button>("next-button");

        messageNexButton.clicked += ClickNext;
    }

    async void ClickNext ()
    {
        if(0 < conversations.Count)
        {
            try
            {
                await ForwardConversation(conversations.Dequeue());
            }
            catch (OperationCanceledException)
            {
                return;
            }
        }
        else
        {
            BoxClose();
        }
    }

    void PrepareConversation(ConversationData _conversationData)
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
            ShowNextButton(false);
            await DisplayText(conversation.text);

            if(SelectOpen()) return;

            ShowNextButton(true);
    }

    bool SelectOpen()
    {
        if(0 < conversations.Count || !conversationData.optionExists()) return false;

        Debug.Log("select opent");

        return true;
    }

    public async UniTask Conversation(ConversationData conversationData)
    {
        tokenSource = new CancellationTokenSource();

        PrepareConversation(conversationData);

        BoxOpen(true);

        try
        {
            await ForwardConversation(conversations.Dequeue());
        }
        catch (OperationCanceledException)
        {
            return;
        }
    }

    async UniTask DisplayText(string messageLine)
    {
        var token = tokenSource.Token;
        messageText.text = "";

        await UniTask.Delay(500, cancellationToken: token);

        var length = messageLine.Length;
        for(var i = 0; i <= length; i++)
        {
            messageText.text = messageLine.Substring(0, i);
            await UniTask.Delay(30, cancellationToken: token);
        }
        await UniTask.Delay(500, cancellationToken: token);
    }

    void ShowNextButton(bool show)
    {
        messageNexButton.style.display = (show)? DisplayStyle.Flex : DisplayStyle.None;
    }

    void BoxOpen(bool open)
    {
        box.style.display = (open)? DisplayStyle.Flex : DisplayStyle.None;
    }

    void BoxClose()
    {
        BoxOpen(false);
        TaskCancel();
        Player.GetPlayer().EnableMove();
    }

    void TaskCancel()
    {
        if(tokenSource == null) return; 

        tokenSource?.Cancel();
        tokenSource?.Dispose();
        tokenSource = null;
    }

    void OnDisable()
    {
        TaskCancel();
    }
}
