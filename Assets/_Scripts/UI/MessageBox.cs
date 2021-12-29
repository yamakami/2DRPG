using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class MessageBox : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] MessageText messageText;

    QuestManager questManager;

    void Start()
    {
        this.GetCancellationTokenOnDestroy();
        questManager = QuestManager.GetQuestManager();        
    }

    void OnEnable()
    {
       ForwardConversation(); 
    }

     void ForwardConversation()
    {
        TextForwardButton(false);

        var a = "こんにちは\n今夜もお金を溶かしたよ";
        var tweenText =  messageText.TweenText(a);

        PlayTweenText(tweenText, this.GetCancellationTokenOnDestroy()).Forget();
    }

    async UniTaskVoid PlayTweenText(Tween tweenText, CancellationToken token)
    {
        await tweenText.Play().ToUniTask(cancellationToken: token);
        TextForwardButton(true);
    }

    void TextForwardButton(bool value)
    {
        messageText.TextForwardButton.gameObject.SetActive(value);
    }

    public void Active(bool value)
    {
        enabled = value;
        canvas.enabled = value;
    }
}
