using UnityEngine;
using UnityEngine.UIElements;
using System.Threading;
using Cysharp.Threading.Tasks;
using System;

public class MessageBox : MonoBehaviour
{
    VisualElement box;
    Label messageText;
    Button messageNexButton;
    CancellationTokenSource tokenSource;

    public void Init(VisualElement ve)
    {
        box = ve.Q<VisualElement>("message-box");
        messageText = ve.Q<Label>("message-text");
        messageNexButton = ve.Q<Button>("next-button");

        messageNexButton.clicked += Test;
    }

    void Test ()
    {
        Debug.Log("hello");
    }


    public async void BoxOpen(bool open)
    {
        box.style.display = (open)? DisplayStyle.Flex : DisplayStyle.None;

        var t = "こんにちはあああああ!\nしくよろ四苦八苦!!!!!!!\nまたあしたここで会いましょう";
        tokenSource = new CancellationTokenSource();

        try
        {
            await ClearText(tokenSource.Token);
            await DisplayText(t, tokenSource.Token);
        }
        catch (OperationCanceledException)
        {
            return;
        }

        ShowNextButton(true);
        TaskCancel();
    }

    async UniTask DisplayText(string messageLine, CancellationToken token = default)
    {
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

    async UniTask ClearText(CancellationToken token = default)
    {
        messageText.text = "";
        await UniTask.Delay(500, cancellationToken: token);
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
