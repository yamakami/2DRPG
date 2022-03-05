using System.Text;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using DG.Tweening;


public class CommandUtils : MonoBehaviour
{
    static StringBuilder stringBuilder = new StringBuilder();

    static public StringBuilder GetStringBuilder()
    {
        stringBuilder.Clear();
        return stringBuilder;
    }

    static public async UniTask PlayTwennMessage(CancellationToken token, MessageText messageText, int delayTime, string message)
    {
        var tween = messageText.TweenText(message);
        await UniTask.Delay(delayTime, cancellationToken: token);
        await tween.Play().ToUniTask(cancellationToken: token);
        await UniTask.Delay(delayTime, cancellationToken: token);
    }

    static public async UniTask CommandExecute(
        ICommand command,
        AudioSource seSound,
        Conversation conversationBox,
        CancellationToken token,
        IStatus userStatus,
        IStatus targetStatus
    )
    {
        var messageText = conversationBox.MessageText;
        var delayTime = conversationBox.DelayTime;

        var message = GetStringBuilder().AppendFormat(command.ActionMessage(), userStatus.CharacterName).ToString();
        await CommandUtils.PlayTwennMessage(token,  messageText, delayTime + 500, message);

        if (seSound != null)
        {
            seSound.clip = command.GetAudioClip();
            seSound.Play();
            await UniTask.WaitUntil( () => !seSound.isPlaying, cancellationToken: token);
        }

        command.Consume(userStatus, userStatus);

        message = GetStringBuilder().AppendFormat(command.AffectMessage(), userStatus.CharacterName).ToString();
        await CommandUtils.PlayTwennMessage(token, messageText, delayTime, message);
    }
}
